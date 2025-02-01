using Limupa.Comment.Api.Dtos.UserCommentDtos;
using System.Xml.Linq;

namespace Limupa.Comment.Api.Services.UserCommentServices
{
    public interface IUserCommentService
    {
        Task<List<ResultUserCommentDto>> GetAllUserCommentAsync();
        Task CreateUserCommentAsync(CreateUserCommentDto createUserCommentDto);
        Task UpdateUserCommentAsync(UpdateUserCommentDto updateUserCommentDto);
        Task DeleteUserCommentAsync(int id);
        Task<GetByIdUserCommentDto> GetByIdUserCommentAsync(int id);
        Task<List<ResultUserCommentByProductIdDto>> GetUserCommentByProductId(string id);

    }
}
