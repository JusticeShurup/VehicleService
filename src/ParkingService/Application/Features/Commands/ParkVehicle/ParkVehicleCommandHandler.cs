using Application.Base.Command;
using Application.Interfaces;
using Domain;
using ProtosContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.ParkVehicle
{
    public class ParkVehicleCommandHandler : ICommandHandler<ParkVehicleCommand>
    {
        private readonly IParkingRepository _parkingRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly Vehicles.VehiclesClient _vehiclesClient;

        public ParkVehicleCommandHandler(IParkingRepository parkingRepository,  Vehicles.VehiclesClient vehiclesClient, ISubscriptionRepository subscriptionRepository)
        {
            _parkingRepository = parkingRepository;
            _vehiclesClient = vehiclesClient;
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task Handle(ParkVehicleCommand command)
        {
            var parking = await _parkingRepository.FindByIdAsync(command.ParkingId);
            if (parking == null)
            {
                throw new Exception("Not found exception");
            }

            var vehicle = await _vehiclesClient.GetVehicleAsync(new GetVehicleRq()
            {
                Id = command.VehicleId.ToString(),
            });
            
            if (vehicle == null)
            {
                throw new Exception("This vehicle didn't exists");
            }

            if (parking.IsVehicleInParking(command.VehicleId))
            {
                throw new Exception("Vehicle is already parked");
            }

            var parkResult = parking.AddVehicle(command.VehicleId, vehicle.Vehicle.Engine.EngineType == 2);
            _parkingRepository.Update(parking);
            if (parkResult == null)
            {
                throw new Exception("Parking error");
            }
            Subscription subscription = new(parkResult, parkResult.Id, DateTime.UtcNow, DateTime.UtcNow.AddDays(1));
            _subscriptionRepository.Add(subscription);
            await _parkingRepository.UnitOfWork.SaveChangesAsync();
            await _subscriptionRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}
