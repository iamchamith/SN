using Alpha.Areas.UserAcccount.Models;
using Alpha.Bo;
using Alpha.Bo.Enums;
using Alpha.Utility;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Alpha.Controllers.Api
{
    [RoutePrefix("api/v1/base")]
    public class BaseApiController : ApiController
    {
        [NonAction]
        protected UserPreviewPageViewModel GetPreviewObject(UserBo item, bool ismine = true)
        {

            var result = new UserPreviewPageViewModel();
            result.Bio = item.Bio;
            result.Dob = item.Dob;
            result.Country = ((Enums.Countries)item.Country).ToString().Replace('_', ' ');
            result.Name = item.Name;
            result.MaritalStatus = ((Enums.MaritalStatus)item.MaritalStatus).ToString().Replace('_', ' ');
            result.Gender = ((Enums.Gender)item.Gender).ToString().Replace('_', ' ');
            result.ProfileImage = (ismine) ? GCSession.ProfileImage :
                $"https://www.gravatar.com/avatar/{Alpha.Bo.Utility.Helper.MD5Hash(item.Email)}";
            return result;
        }


        public string UploadImage(string imagedata,Enums.Imagetype imgtype,string imagename)
        {
            string data = string.Empty;
            try
            {
                data = imagedata.Split(',')[1];
                //data:image/png;base64
                imagename += $@".{imagedata.Split(',')[0].Replace(";base64", "")
                  .Replace("data:image/", "").Trim()}";
            }
            catch
            {
                throw new FileNotFoundException();
            }
            try
            {
                byte[] bytes = Convert.FromBase64String(data);
                Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                    image.Save(HttpContext.Current.Server.MapPath($"~/images/{imgtype}/{imagename}"));
                }
            }
            catch {
                throw new BadImageFormatException();
            }
            return imagename;
        }

        [NonAction]
        protected void UploadImage(Stream fileStream, Enums.Imagetype imagetype, string fileName)
        {
            try
            {
                // Retrieve storage account from connection string.
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(@"DefaultEndpointsProtocol=https;AccountName=criends;AccountKey=7MVUq2WYHnTupLLTADAoqsJlz69n1uIlGncz8dN7yRyeC4Kru8e9wV1Wx7AKjRfs1u0C90sY5CDgxyMyAUjjbw==;EndpointSuffix=core.windows.net");

                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve reference to a previously created container.
                CloudBlobContainer container = blobClient.GetContainerReference("profileImage");
                container.CreateIfNotExists();
                // Retrieve reference to a blob named "myblob".
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

                // Create or overwrite the "myblob" blob with contents from a local file.
                blockBlob.UploadFromStream(fileStream);
            }
            catch (Exception e) { throw e; }
        }
    }
}
