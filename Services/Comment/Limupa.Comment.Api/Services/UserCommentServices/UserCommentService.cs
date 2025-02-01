using AutoMapper;
using Limupa.Comment.Api.Context;
using Limupa.Comment.Api.Dtos.UserCommentDtos;
using Limupa.Comment.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Limupa.Comment.Api.Services.UserCommentServices
{
    public class UserCommentService : IUserCommentService
    {
        private readonly CommentContext commentContext;
        private readonly IMapper mapper;

        public UserCommentService(CommentContext commentContext, IMapper mapper)
        {
            this.commentContext = commentContext;
            this.mapper = mapper;
        }

        public async Task CreateUserCommentAsync(CreateUserCommentDto createUserCommentDto)
        {
            using (var context = commentContext)
            {
                var value = mapper.Map<UserComment>(createUserCommentDto);
                context.UserComments.Add(value);
                await context.SaveChangesAsync();
            }
                        
        }

        public async Task DeleteUserCommentAsync(int id)
        {
            using (var context = commentContext)
            {
                var value = await context.UserComments.FindAsync(id);
                context.UserComments.Remove(value!);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<ResultUserCommentDto>> GetAllUserCommentAsync()
        {
            using (var context = commentContext)
            {
                var values = mapper.Map<List<ResultUserCommentDto>>(await context.UserComments.ToListAsync());
                return values;
            }
        }

        public async Task<GetByIdUserCommentDto> GetByIdUserCommentAsync(int id)
        {
            using (var context = commentContext)
            {
                var value = mapper.Map<GetByIdUserCommentDto>(await context.UserComments.FindAsync(id));
                return value;
            }
        }

        public async Task<List<ResultUserCommentByProductIdDto>> GetUserCommentByProductId(string id)
        {
            using (var context = commentContext)
            {
                var values = mapper.Map<List<ResultUserCommentByProductIdDto>>(await context.UserComments.Where(x=>x.ProductID == id).ToListAsync());
                return values;
            }
        }

        public async Task UpdateUserCommentAsync(UpdateUserCommentDto updateUserCommentDto)
        {
            using (var context = commentContext)
            {
                var value = mapper.Map<UserComment>(updateUserCommentDto);
                context.UserComments.Update(value);
                await context.SaveChangesAsync();
            }
        }
    }
}
