using Limupa.Comment.Api.Dtos.UserCommentDtos;
using Limupa.Comment.Api.Services.UserCommentServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.Comment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IUserCommentService userCommentService;

        public CommentsController(IUserCommentService userCommentService)
        {
            this.userCommentService = userCommentService;
        }

        [HttpGet]
        public async Task<IActionResult> UserCommentList()
        {
            var values = await userCommentService.GetAllUserCommentAsync();
            return Ok(values);
        }
        [HttpGet("UserCommentByProductIdList/{id}")]
        public async Task<IActionResult> UserCommentByProductIdList(string id)
        {
            var values = await userCommentService.GetUserCommentByProductId(id);
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUserComment(CreateUserCommentDto createUserCommentDto)
        {
            await userCommentService.CreateUserCommentAsync(createUserCommentDto);
            return Ok("Successful");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUserComment(UpdateUserCommentDto updateUserCommentDto)
        {
            await userCommentService.UpdateUserCommentAsync(updateUserCommentDto);
            return Ok("Successful");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserComment(int id)
        {
            await userCommentService.DeleteUserCommentAsync(id);
            return Ok("Successful");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserCommentById(int id)
        {
            var values = await userCommentService.GetByIdUserCommentAsync(id);
            return Ok(values);
        }
    }
}
