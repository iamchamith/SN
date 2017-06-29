using Alpha.Areas.UserAcccount.Models;
using Alpha.Bo;
using Alpha.Bo.Enums;
using Alpha.DbAccess;
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
            result.ProfileImage = (ismine) ? GCSession.ProfileImage : item.ProfileImage;
            result.IsMine = item.UserId == GCSession.UserGuid;
            return result;
        }


        public string UploadImage(string imagedata, Enums.Imagetype imgtype, string imagename)
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
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    UploadImageBlob(ms, imgtype, imagename);
                }
            }
            catch
            {
                throw new BadImageFormatException();
            }
            return imagename;
        }

        [NonAction]
        void UploadImageBlob(Stream fileStream, Enums.Imagetype imagetype, string fileName)
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Configs.BlobConnectionString);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference(imagetype.ToString());
                container.CreateIfNotExists();
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
                blockBlob.UploadFromStream(fileStream);
            }
            catch (Exception e) { throw e; }
        }
    }
}
