using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Alpha.Bo.Exceptions;

namespace Alpha.Service.Services
{
    public class BaseService
    {
        public Exception ExceptionHandler(Exception ex)
        {
            ex = ex.GetBaseException();
            if (ex is SqlException)
            {
                var e = ex as SqlException;
                if (e.Number == 2627)
                {
                    return new PrimaryKeyViolationException("already exist");
                }
                else if (e.Number == 547)
                {
                    return new PrimaryKeyViolationException("fk excist");
                }
                return ex;
            }
            else
            {
                return ex;
            }
        }

        public string ImagePostBlobPrefix
        {
            get
            {
                return Bo.Utility.Configs.ImagePrefixBlob + Bo.Enums.Enums.Imagetype.postimages.ToString() + "/";
            }
        }
        public string ImageProfileBlobPrefix
        {
            get
            {
                return Bo.Utility.Configs.ImagePrefixBlob + Bo.Enums.Enums.Imagetype.profileimages.ToString() + "/";
            }
        }

        public string DateShow(DateTime dt)
        {
            TimeSpan difference = DateTime.UtcNow - dt;
            double days = Math.Floor(difference.TotalDays);
            double hr = Math.Floor(difference.TotalHours);
            double min = Math.Floor(difference.TotalMinutes);
            var caption = new StringBuilder();
            if (days > 0)
            {
                hr = hr - days * 24;
                caption.Append($"{days} days and {hr} hours ago");
            }
            else
            {
                min = min = hr * 60;
                caption.Append($"{hr} hours and {min} minits ago");
            }
            return caption.ToString();
        }
    }
}
