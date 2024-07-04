using Application.Base.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.ParkVehicle
{
    public class ParkVehicleCommand : Command
    {
        public Guid ParkingId { get; set; }
        public Guid VehicleId { get; set; }

        public ParkVehicleCommand(Guid parkingId, Guid vehicleId)
        {
            ParkingId = parkingId;
            VehicleId = vehicleId;
        }
    }
}
