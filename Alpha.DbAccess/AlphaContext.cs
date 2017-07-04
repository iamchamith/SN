using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Alpha.Poco;
using System.Configuration;
using Alpha.Bo.Utility;

namespace Alpha.DbAccess
{
    public interface IAlphaContext
    {
        IDbSet<User> Users { get; set; }
        IDbSet<Post> Posts { get; set; }
        IDbSet<UserPost> UserPosts { get; set; }
        IDbSet<PostComment> PostComments { get; set; }
        IDbSet<Tag> Tags { get; set; }
        IDbSet<UserTag> UserTags { get; set; }
        IDbSet<UserContact> Contacts { get; set; }
        IDbSet<Error> Errors { get; set; }
        IDbSet<UserRelation> UserRelations { get; set; }
        IDbSet<PostNeedComment> PostNeedComments { get; set; }
        IDbSet<PostPoll> PostPolls { get; set; }
        IDbSet<PostQuestion> PostQuestion { get; set; }
        IDbSet<UserPreferences> UserPreferences { get; set; }
        IDbSet<UserMessage> UserMessages { get; set; }
        IDbSet<PostLike> PostLikes { get; set; }
        IDbSet<Notification> Notifications { get; set; }
    }
    public class AlphaContext : DbContext, IAlphaContext
    {
        public AlphaContext() : base(Configs.Conns)
        {

        }
        public IDbSet<User> Users { get; set; }
        public IDbSet<Post> Posts { get; set; }
        public IDbSet<UserPost> UserPosts { get; set; }
        public IDbSet<PostComment> PostComments { get; set; }
        public IDbSet<Tag> Tags { get; set; }
        public IDbSet<UserTag> UserTags { get; set; }
        public IDbSet<UserContact> Contacts { get; set; }
        public IDbSet<Error> Errors { get; set; }
        public IDbSet<UserRelation> UserRelations { get; set; }
        public IDbSet<PostNeedComment> PostNeedComments { get; set; }
        public IDbSet<PostPoll> PostPolls { get; set; }
        public IDbSet<PostQuestion> PostQuestion { get; set; }
        public IDbSet<UserPreferences> UserPreferences { get; set; }
        public IDbSet<UserMessage> UserMessages { get; set; }
        public IDbSet<PostLike> PostLikes { get; set; }
        public IDbSet<Notification> Notifications { get; set; }
    }
}


