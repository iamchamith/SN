using System;

namespace Alpha.Bo
{
    public class TagBo : IBo
    { 
        public int Id { get; set; }
        public string TagName { get; set; }
        public Guid Owner { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
