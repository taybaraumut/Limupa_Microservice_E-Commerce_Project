﻿using MediatR;

namespace Limupa.Order.Application.Features.Mediator.Commands.OrderingCommands
{
    public class RemoveOrderingCommand:IRequest
    {
        public int Id { get; set; }
        public RemoveOrderingCommand(int id)
        {
            Id = id;
        }
    }
}
