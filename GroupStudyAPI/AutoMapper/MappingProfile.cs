using AutoMapper;
using BusinessObject.Models;
using BusinessObject.Sub_Model;

namespace GroupStudyAPI.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<JoinRequest, JoinRequestModel>()
           .ForMember(dest => dest.Group, opt => opt.MapFrom(src => src.Group))
           .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
           .ReverseMap();
            CreateMap<User, UserModel>()
           .ReverseMap();

            CreateMap<Group, GroupModel>()
           .ReverseMap();

            CreateMap<Post, PostModel>().ReverseMap()
            .ForMember(dest => dest.Group, opt => opt.MapFrom(src => src.Group))
           .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
           ;

            CreateMap<Task, TaskModel>().ReverseMap()
                .ForMember(dest => dest.Group, opt => opt.MapFrom(src => src.Group))
           .ForMember(dest => dest.AssignedToUser, opt => opt.MapFrom(src => src.AssignedToUser))
           ;

        }
    }
}
