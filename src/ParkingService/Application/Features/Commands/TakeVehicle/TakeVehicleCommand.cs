using Application.Base.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.TakeVehicle
{
    public class TakeVehicleCommand : Command
    {
        public Guid VehicleId { get; set; }

        public TakeVehicleCommand(Guid vehicleId) 
        {
            VehicleId = vehicleId;
        }
    }
}
