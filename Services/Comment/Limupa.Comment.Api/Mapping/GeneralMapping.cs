using AutoMapper;
using Limupa.Comment.Api.Dtos.UserCommentDtos;
using Limupa.Comment.Api.Entities;

namespace Limupa.Comment.Api.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<UserComment, ResultUserCommentDto>().ReverseMap();
            CreateMap<UserComment, ResultUserCommentByProductIdDto>().ReverseMap();
            CreateMap<UserComment,CreateUserCommentDto>().ReverseMap();
            CreateMap<UserComment,GetByIdUserCommentDto>().ReverseMap();
            CreateMap<UserComment,UpdateUserCommentDto>().ReverseMap();
        }
    }
}
