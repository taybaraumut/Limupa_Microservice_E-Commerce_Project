using Limupa.DtoLayer.UserCommentDtos;
using Limupa.UI.Services.CommentServices.UserCommentServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Limupa.UI.ViewComponents.ProductDetailComponents
{
    public class ProductDetailCommentComponentPartial : ViewComponent
    {
        private readonly IUserCommentService userCommentService;

        public ProductDetailCommentComponentPartial(IUserCommentService userCommentService)
        {
            this.userCommentService = userCommentService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var values = await userCommentService.GetUserCommentByProductId(id);
            return View(values);
        }      
    }
}
