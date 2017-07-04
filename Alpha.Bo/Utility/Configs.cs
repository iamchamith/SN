using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Bo.Utility
{
    public class Configs
    {
        public static string ImagePrefixBlob = "https://criends.blob.core.windows.net/";
        public static string Conns = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Alpha;Integrated Security=True;Pooling=False";
        //public static string Conns = @"Server=tcp:criends.database.windows.net,1433;Initial Catalog=criends;Persist Security Info=False;User ID=iamchamith;Password=janson@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static string BlobConnectionString = @"DefaultEndpointsProtocol=https;AccountName=criends;AccountKey=7MVUq2WYHnTupLLTADAoqsJlz69n1uIlGncz8dN7yRyeC4Kru8e9wV1Wx7AKjRfs1u0C90sY5CDgxyMyAUjjbw==;EndpointSuffix=core.windows.net";
    }
}
