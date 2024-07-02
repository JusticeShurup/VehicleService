using Application.Base.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Bus.Command
{
    internal abstract class CommandHandlerWrapper
    {
        public abstract Task Handle(Application.Base.Command.Command command, IServiceProvider provider);
    }

    internal class CommandHandlerWrapper<TCommand> : CommandHandlerWrapper
        where TCommand : Application.Base.Command.Command
    {
        public override async Task Handle(Application.Base.Command.Command command, IServiceProvider provider)
        {
            var handler = (ICommandHandler<TCommand>)provider.GetService(typeof(ICommandHandler<TCommand>));
            await handler.Handle((TCommand)command);
        }
    }
}
