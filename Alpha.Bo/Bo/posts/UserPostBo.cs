using System;
using static Alpha.Bo.Enums.Enums;

namespace Alpha.Bo
{
    public class UserPostBo : IBo
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public YesNo IsPrimaryUser { get; set; }
        public DateTime PostDate { get; set; }
        public Guid ParentPostId { get; set; }
        public YesNo Anonymous { get; set; }
    }

    public class UserPostInfoBo
    {
        public PostBo Post { get; set; }
        public UserPostBo UserPost { get; set; }
    }

    public class UserPostSearchRequest
    {
        public string UserId { get; set; }
        public string Topic { get; set; }
        public bool IsDateDesc { get; set; }
        public string[] Tags { get; set; }
        public UserPostSearchRequest()
        {
            IsDateDesc = true;
        }
    }
    public class UserPostSearchResponse
    {
        public int UserPostId { get; set; }
        public DateTime PostDate { get; set; }
        public bool Anonymous { get; set; }
        public Guid ParentPostId { get; set; }
        public Guid PostId { get; set; }
        public string Tags { get; set; }
        public string Titile { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public string ProfileImage { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
