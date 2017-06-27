using System;
using System.Collections.Generic;

namespace Alpha.Bo
{
    public class PostBo : IBo
    {
        public int Id { get; set; }
        public Guid PostId { get; set; }
        public string Topic { get; set; }
        public string Tags { get; set; }
        public Enums.Enums.PostType PostType { get; set; }
        public PostBo()
        {
            Tags = "default";
        }
    }
}
