using Alpha.DbAccess;
using Alpha.Poco;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Service.Infrastructure
{
    public interface IUnitOfWork
    {
        GenericRepository<User> UserRepository { get; }
        GenericRepository<Tag> TagRepository { get; }
        GenericRepository<UserTag> UserTagRepository { get; }
        GenericRepository<UserContact> UserContactRepository { get; }
        GenericRepository<Post> PostRepository { get; }
        GenericRepository<UserPost> UserPostRepository { get; }
        GenericRepository<UserPreferences> UserPreferencesRepository { get; }
        GenericRepository<UserMessage> UserMessageRepository { get; }
        GenericRepository<PostLike> PostLikeRepository { get; }
        GenericRepository<PostComment> PostCommentRepository { get; }
        GenericRepository<Notification> NotificationRepository { get; }
        void Save();
        Task SaveAsync();
        AlphaContext Context { get; }

    }
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public UnitOfWork()
        {
            this.context = new AlphaContext();
        }
        private DbContext context;
        private GenericRepository<User> userRepository;
        private GenericRepository<Tag> tagRepository;
        private GenericRepository<UserTag> userTagRepository;
        private GenericRepository<UserContact> userContactRepository;
        private GenericRepository<Post> postRepository;
        private GenericRepository<UserPost> userPostRepository;
        private GenericRepository<UserPreferences> userPreferencesRepository;
        private GenericRepository<UserMessage> userMessageRepository;
        private GenericRepository<PostLike> postLikeRepository;
        private GenericRepository<PostComment> postCommentRepository;
        private GenericRepository<Notification> notificationRepository;
        public AlphaContext Context
        {
            get
            {
                return context as AlphaContext;
            }
        }
        public GenericRepository<User> UserRepository
        {
            get
            {

                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<User>(context);
                }
                return userRepository;
            }
        }
        public GenericRepository<UserPost> UserPostRepository
        {
            get
            {

                if (this.userPostRepository == null)
                {
                    this.userPostRepository = new GenericRepository<UserPost>(context);
                }
                return userPostRepository;
            }
        }
        public GenericRepository<Notification> NotificationRepository
        {
            get
            {

                if (this.notificationRepository == null)
                {
                    this.notificationRepository = new GenericRepository<Notification>(context);
                }
                return notificationRepository;
            }
        }
        public GenericRepository<Post> PostRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.postRepository = new GenericRepository<Post>(context);
                }
                return postRepository;
            }
        }
        public GenericRepository<Tag> TagRepository
        {
            get
            {

                if (this.tagRepository == null)
                {
                    this.tagRepository = new GenericRepository<Tag>(context);
                }
                return tagRepository;
            }
        }
        public GenericRepository<UserTag> UserTagRepository
        {
            get
            {

                if (this.userTagRepository == null)
                {
                    this.userTagRepository = new GenericRepository<UserTag>(context);
                }
                return userTagRepository;
            }
        }
        public GenericRepository<UserContact> UserContactRepository
        {
            get
            {

                if (this.userContactRepository == null)
                {
                    this.userContactRepository = new GenericRepository<UserContact>(context);
                }
                return userContactRepository;
            }
        }

        public GenericRepository<UserPreferences> UserPreferencesRepository {
            get
            {

                if (this.userPreferencesRepository == null)
                {
                    this.userPreferencesRepository = new GenericRepository<UserPreferences>(context);
                }
                return userPreferencesRepository;
            }
        }

        public GenericRepository<UserMessage> UserMessageRepository
        {
            get
            {
                if (this.userMessageRepository == null)
                {
                    this.userMessageRepository = new GenericRepository<UserMessage>(context);
                }
                return userMessageRepository;
            }
        }
        public GenericRepository<PostLike> PostLikeRepository
        {
            get
            {
                if (this.postLikeRepository == null)
                {
                    this.postLikeRepository = new GenericRepository<PostLike>(context);
                }
                return postLikeRepository;
            }
        }

        public GenericRepository<PostComment> PostCommentRepository {
            get
            {
                if (this.postCommentRepository == null)
                {
                    this.postCommentRepository = new GenericRepository<PostComment>(context);
                }
                return postCommentRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
