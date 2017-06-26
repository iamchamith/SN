using System;
using System.Collections.Generic;

namespace Alpha.Bo
{
    public class PostBo : IBo
    {
        public int Id { get; set; }
        public Guid PostId { get; set; }
        public string Titile { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public PostBo()
        {
            Tags = "default";
        }
    }
}
