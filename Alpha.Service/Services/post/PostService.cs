using Alpha.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.Poco;
using Alpha.Service.Infrastructure;
using Alpha.Bo;
using AutoMapper;

namespace Alpha.Service.Services
{
    public class PostService : BaseService, IPost
    {
        IUnitOfWork uow;
        public PostService(IUnitOfWork _uow)
        {
            this.uow = _uow;
        }

        public async Task<PostBo> Insert(PostBo item)
        {
            try
            {
                item.PostId = Guid.NewGuid();
                var r = Mapper.Map<Post>(item);
                this.uow.PostRepository.Insert(r);
                await this.uow.SaveAsync();
                return Mapper.Map<PostBo>(r);
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }
        public async Task<PostBo> Read(Guid id)
        {
            try
            {
                return Mapper.Map<PostBo>(this.uow.PostRepository.GetByID(id));
            }
            catch (Exception e)
            {
                throw ExceptionHandler(e);
            }
        }

        public Task Update(PostBo item)
        {
            throw new NotImplementedException();
        }

        Task<List<PostBo>> IRepository<PostBo, Guid>.Read(Guid userid)
        {
            throw new NotImplementedException();
        }

        public Task<List<PostBo>> Read()
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id, Guid userid)
        {
            throw new NotImplementedException();
        }
    }
}
