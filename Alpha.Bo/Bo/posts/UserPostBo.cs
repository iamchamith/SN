using Alpha.Bo.Bo.posts;
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
        public Enums.Enums.PostType posttype { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }

        public bool IsMyAnswers { get; set; }
        public bool IsMyAsks { get; set; }
        public bool IsNeedComments { get; set; }
        public bool IsPoll { get; set; }
        public bool IsQuestions { get; set; }

        public Enums.Enums.PostSearchType PostSearchType { get; set; }
        public UserPostSearchRequest()
        {
            IsDateDesc = true;
            PostSearchType = PostSearchType.Feed;
            IsMyAnswers = IsMyAsks = false;
            IsNeedComments = IsPoll = IsQuestions = true;
        }
    }
    public class UserPostSearchResponse
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public int UserPostId { get; set; }
        public DateTime PostDate { get; set; }
        public bool Anonymous { get; set; }
        public Guid ParentPostId { get; set; }
        public Guid PostId { get; set; }
        public string Tags { get; set; }
        public string Topic { get; set; }
        public string ProfileImage { get; set; }
        public Enums.Enums.PostType PostType { get; set; }
        public PostQuestionBo PostQuestion { get; set; }
        public PostPollBo PostPoll { get; set; }
        public PostNeedCommentBo PostNeedComment { get; set; }
    }
}
