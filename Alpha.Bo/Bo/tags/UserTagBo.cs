using System;

namespace Alpha.Bo
{
    public class UserTagBo : IBo
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public Guid UserId { get; set; }
        public string TagName { get; set; }
        public string Description { get; set; }
    }
}
