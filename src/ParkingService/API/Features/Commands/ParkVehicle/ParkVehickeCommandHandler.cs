using Application.Base.Command;
using Application.Interfaces;
using ProtosContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Features.Commands.ParkVehicle
{
    public class ParkVehickeCommandHandler : ICommandHandler<ParkVehicleCommand>
    {
        private readonly IParkingRepository _parkingRepository;
        private readonly Vehicles.VehiclesClient _vehiclesClient;

        public ParkVehickeCommandHandler(IParkingRepository parkingRepository, Vehicles.VehiclesClient vehiclesClient)
        {
            _parkingRepository = parkingRepository;
            _vehiclesClient = vehiclesClient;
        }

        public async Task Handle(ParkVehicleCommand command)
        {
            var parking = await _parkingRepository.FindByIdAsync(command.ParkingId);
            if (parking == null)
            {
                throw new Exception("Not found exception");
            }

            var result = await _vehiclesClient.GetVehicleAsync(new GetVehicleRq()
            {
                VehicleId = command.VehicleId.ToString(),
            });

            if (parking.IsVehicleInParking(command.VehicleId))
            {
                throw new Exception("Vehicle is already parked");
            }
            var parkResult = parking.AddVehicle(command.VehicleId);
            _parkingRepository.Update(parking);
            await _parkingRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}
