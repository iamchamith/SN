using Alpha.Areas.UserAcccount.Models;
using Alpha.Bo;
using Alpha.Bo.Bo.criends;
using Alpha.Bo.Enums;
using Alpha.Bo.Utility;
using Alpha.DbAccess;
using Alpha.Utility;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
        protected UserPreviewPageViewModel GetPreviewObject(UserBo item, RelationCountBo relationcount, bool ismine = true)
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
            result.Followings = relationcount.MyFollowing;
            result.Followers = relationcount.MyFollowers;
            result.FollowersCriends = relationcount.OtherFollowers;
            result.FollowingsCriends = relationcount.OtherFollowing;
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
                    Image img = null;
                    if (imgtype == Enums.Imagetype.profileimages)
                    {
                        img = ImageProcess.ResizeImage(Image.FromStream(ms), 150, 150);
                    }
                    else if (imgtype == Enums.Imagetype.postimages)
                    {
                        img = ImageProcess.ResizeImage(Image.FromStream(ms), 300, 300);
                    }
                    var msimg = new MemoryStream();
                    img.Save(msimg, ImageFormat.Png);
                    UploadImageBlob(msimg, imgtype, imagename);
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

        [NonAction]
        void RemoveImageBlob(Enums.Imagetype imagetype, string fileName)
        {

            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Configs.BlobConnectionString);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference(imagetype.ToString());
                container.CreateIfNotExists();
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
            }
            catch (Exception e) { throw e; }
        }
    }
}
