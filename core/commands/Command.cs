using System;
using MediatR;
using core.config;

namespace core.commands
{
    public abstract class Command : IRequest<Response>
    {
        public DateTime Timestamp { get; private set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}
