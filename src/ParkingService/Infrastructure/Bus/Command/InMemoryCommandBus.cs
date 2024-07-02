using Application.Base.Command;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Bus.Command
{
    public class InMemoryCommandBus : ICommandBus
    {
        private readonly IServiceProvider _provider;
        private static readonly ConcurrentDictionary<Type, IEnumerable<CommandHandlerWrapper>> _commandHandlers = new();

        public InMemoryCommandBus(IServiceProvider serviceProvider)
        {
            _provider = serviceProvider;
        }

        public async Task Send(Application.Base.Command.Command command)
        {
            var wrappedHandlers = GetWrappedHandlers(command);

            if (wrappedHandlers == null) throw new Exception("Command is not registered");

            foreach (CommandHandlerWrapper handler in wrappedHandlers)
            {
                await handler.Handle(command, _provider);
            }
        }

        private IEnumerable<CommandHandlerWrapper> GetWrappedHandlers(Application.Base.Command.Command command)
        {
            Type handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            Type wrapperType = typeof(CommandHandlerWrapper<>).MakeGenericType(command.GetType());

            IEnumerable handlers =
                (IEnumerable)_provider.GetService(typeof(IEnumerable<>).MakeGenericType(handlerType));

            var wrappedHandlers = _commandHandlers.GetOrAdd(command.GetType(), handlers.Cast<object>()
                .Select(_ => (CommandHandlerWrapper)Activator.CreateInstance(wrapperType)));

            return wrappedHandlers;
        }
    }
}
