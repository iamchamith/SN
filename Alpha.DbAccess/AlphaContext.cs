using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Alpha.Poco;
namespace Alpha.DbAccess
{
    public interface IAlphaContext
    {
        IDbSet<User> Users { get; set; }
        IDbSet<Post> Posts { get; set; }
        IDbSet<UserPost> UserPosts { get; set; }
        IDbSet<UserPostComment> UserPostComments { get; set; }
        IDbSet<Tag> Tags { get; set; }
        IDbSet<UserTag> UserTags { get; set; }
        IDbSet<UserContact> Contacts { get; set; }
        IDbSet<Error> Errors { get; set; }
        IDbSet<UserRelation> UserRelations { get; set; }
    }
    public class AlphaContext : DbContext, IAlphaContext
    {
        public AlphaContext() : base(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=Alpha;Integrated Security=True;Pooling=False")
        {

        }
        public IDbSet<User> Users { get; set; }
        public IDbSet<Post> Posts { get; set; }
        public IDbSet<UserPost> UserPosts { get; set; }
        public IDbSet<UserPostComment> UserPostComments { get; set; }
        public IDbSet<Tag> Tags { get; set; }
        public IDbSet<UserTag> UserTags { get; set; }
        public IDbSet<UserContact> Contacts { get; set; }
        public IDbSet<Error> Errors { get; set; }
        public IDbSet<UserRelation> UserRelations { get; set; }
    }
}


