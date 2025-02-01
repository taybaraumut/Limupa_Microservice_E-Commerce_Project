using Limupa.DtoLayer.UserCommentDtos;

namespace Limupa.UI.Services.CommentServices.UserCommentServices
{
    public class UserCommentService : IUserCommentService
    {
        private readonly HttpClient httpClient;

        public UserCommentService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task CreateUserCommentAsync(CreateUserCommentDto createUserCommentDto)
        {
            await httpClient.PostAsJsonAsync("usercomments", createUserCommentDto);
        }

        public async Task DeleteUserCommentAsync(int id)
        {
            await httpClient.DeleteAsync("usercomments/" + id);
        }

        public async Task<List<ResultUserCommentDto>> GetAllUserCommentAsync()
        {
            var responseMessage = await httpClient.GetAsync("usercomments");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultUserCommentDto>>();
            return values;
        }

        public async Task<GetByIdUserCommentDto> GetByIdUserCommentAsync(int id)
        {
            var responseMessage = await httpClient.GetAsync("usercomments/" + id);
            var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdUserCommentDto>();
            return value;
        }

        public async Task<List<ResultUserCommentByProductIdDto>> GetUserCommentByProductId(string id)
        {
            var responseMessage = await httpClient.GetAsync("comments/UserCommentByProductIdList/"+id);
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultUserCommentByProductIdDto>>();
            return values;
        }

        public async Task UpdateUserCommentAsync(UpdateUserCommentDto updateUserCommentDto)
        {
            await httpClient.PutAsJsonAsync("userComments", updateUserCommentDto);
        }
    }
}
