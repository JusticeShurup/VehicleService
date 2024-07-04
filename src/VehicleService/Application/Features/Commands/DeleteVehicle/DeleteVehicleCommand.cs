using Application.Base.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.DeleteVehicle
{
    public class DeleteVehicleCommand : Command
    {
        public Guid Id { get;  }
        public DeleteVehicleCommand(Guid id) 
        {
            Id = id;
        }
    }
}
