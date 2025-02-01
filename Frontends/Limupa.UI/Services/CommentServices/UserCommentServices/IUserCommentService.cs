using Limupa.DtoLayer.UserCommentDtos;

namespace Limupa.UI.Services.CommentServices.UserCommentServices
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
