using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Base.Command
{
    public interface ICommandHandler<TCommand> where TCommand : Command
    {
        Task Handle(TCommand command);
    }
}
