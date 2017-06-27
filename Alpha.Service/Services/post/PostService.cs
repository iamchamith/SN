﻿using Alpha.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.Poco;
using Alpha.Service.Infrastructure;
using Alpha.Bo;
using AutoMapper;
using Alpha.Bo.Bo.posts;

namespace Alpha.Service.Services
{
    public class PostService : BaseService, Interfaces.IPost
    {
        IUnitOfWork uow;
        public PostService(IUnitOfWork _uow)
        {
            this.uow = _uow;
        }

        public async Task<Guid> Insert(PostBo item)
        {
            try
            {
                var postid = Guid.NewGuid();
                item.PostId = postid;
                uow.PostRepository.Insert(Mapper.Map<Post>(item));
                if (item is PostQuestionBo)
                {
                    var poco = Mapper.Map<PostQuestion>(item);
                    poco.PostId = postid;
                    uow.Context.PostQuestion.Add(poco);
                }
                else if (item is PostPollBo)
                {
                    var poco = Mapper.Map<PostPoll>(item);
                    poco.PostId = postid;
                    uow.Context.PostPolls.Add(poco);
                }
                else if (item is PostNeedCommentBo)
                {
                    var poco = Mapper.Map<PostNeedComment>(item);
                    poco.PostId = postid;
                    uow.Context.PostNeedComments.Add(poco);
                }
                else
                {
                    throw new ArgumentException("invalied type");
                }
                return postid;
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

        Task<PostBo> IRepository<PostBo, Guid>.Insert(PostBo item)
        {
            throw new NotImplementedException();
        }
    }
}