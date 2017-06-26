using Alpha.Areas.Posts.Models;
using Alpha.Areas.UserAcccount.Models;
using Alpha.Bo;
using Alpha.Bo.Bo;
using Alpha.Models;
using Alpha.Poco;
using AutoMapper;

namespace Alpha.Utility
{
    public class AutomapperConfig
    {
        public static void Register()
        {
            Mapper.CreateMap<RegisterViewModel, UserBo>().ReverseMap();
            Mapper.CreateMap<User, UserBo>().ReverseMap();
            Mapper.CreateMap<SessionBo, SessionUser>().ReverseMap();
            Mapper.CreateMap<UserLoginBo, LoginViewModel>().ReverseMap();

            Mapper.CreateMap<UserBo, UserBasicViewModel>().ReverseMap();
            Mapper.CreateMap<UserBasicViewModel, UserBo>().ReverseMap();

            Mapper.CreateMap<DropdownViewModel, DropDownBo>().ReverseMap();

            Mapper.CreateMap<UserContactBo, UserContact>().ReverseMap();
            Mapper.CreateMap<UserContactBo, UserContactsViewModel>().ReverseMap();
            Mapper.CreateMap<CriendsRelationsViewModel, CriendsRelationsBo>().ReverseMap();

            Mapper.CreateMap<UserTagBo, UserTag>().ReverseMap();
            Mapper.CreateMap<UserTagBo, UserTagsViewModel>().ReverseMap();
            Mapper.CreateMap<TagViewModel, TagBo>().ReverseMap();
            Mapper.CreateMap<Tag, TagBo>().ReverseMap();
            Mapper.CreateMap<TagInfoViewModel, TagInfoBo>().ReverseMap();

            Mapper.CreateMap<SearchCriendsRequestViewModel, SearchCriendsRequestBo>().ReverseMap();
            Mapper.CreateMap<SearchCriendsResultBo, SearchCriendsResultViewModel>().ReverseMap();

            Mapper.CreateMap<PostViewModel, PostBo>().ReverseMap();
            Mapper.CreateMap<Post, PostBo>().ReverseMap();
            Mapper.CreateMap<UserPostBo, UserPost>().ReverseMap();
        }
    }
}