using Limupa.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using Limupa.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers;
using Limupa.Order.Application.Features.CQRS.Queries.OrderDetailQueries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.Order.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly GetOrderDetailQueryHandler getOrderDetailQueryHandler;
        private readonly GetOrderDetailByIdQueryHandler getOrderDetailByIdQueryHandler;
        private readonly CreateOrderDetailCommandHandler createOrderDetailCommandHandler;
        private readonly UpdateOrderDetailCommandHandler updateOrderDetailCommandHandler;
        private readonly RemoveOrderDetailCommandHandler removeOrderDetailCommandHandler;

        public OrderDetailsController(GetOrderDetailQueryHandler getOrderDetailQueryHandler, GetOrderDetailByIdQueryHandler getOrderDetailByIdQueryHandler,
                                      CreateOrderDetailCommandHandler createOrderDetailCommandHandler, UpdateOrderDetailCommandHandler updateOrderDetailCommandHandler,
                                      RemoveOrderDetailCommandHandler removeOrderDetailCommandHandler)
        {
            this.getOrderDetailQueryHandler = getOrderDetailQueryHandler;
            this.getOrderDetailByIdQueryHandler = getOrderDetailByIdQueryHandler;
            this.createOrderDetailCommandHandler = createOrderDetailCommandHandler;
            this.updateOrderDetailCommandHandler = updateOrderDetailCommandHandler;
            this.removeOrderDetailCommandHandler = removeOrderDetailCommandHandler;

        }

        [HttpGet]
        public async Task<IActionResult> OrderDetailList()
        {
            var values = await getOrderDetailQueryHandler.Handle();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetailById(int id)
        {
            var value = await getOrderDetailByIdQueryHandler.Handle(new GetOrderDetailByIdQuery(id));
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderDetail(CreateOrderDetailCommand command)
        {
            await createOrderDetailCommandHandler.Handle(command);
            return Ok("Successful");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrderDetail(UpdateOrderDetailCommand command)
        {
            await updateOrderDetailCommandHandler.Handle(command);
            return Ok("Successful");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveOrderDetail(int id)
        {
            await removeOrderDetailCommandHandler.Handle(new RemoveOrderDetailCommand(id));
            return Ok("Successful");
        }


    }
}
