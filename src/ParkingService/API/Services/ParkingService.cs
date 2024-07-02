using Application.Base.Command;
using Application.Features.Commands.CreateParking;
using Application.Interfaces;
using Grpc.Core;
using ProtosContract;

namespace API.Services
{
    public class ParkingService : Parking.ParkingBase
    {
        private readonly IParkingRepository _parkingRepository;
        private readonly Vehicles.VehiclesClient _vehiclesClient;
        private readonly ICommandBus _commandBus;

        public ParkingService(IParkingRepository parkingRepository,
            Vehicles.VehiclesClient vehiclesClient,
            ICommandBus commandBus)
        {
            _parkingRepository = parkingRepository;
            _vehiclesClient = vehiclesClient;
            _commandBus = commandBus;
        }

        public override async Task<CreateParkingRs> CreateParking(CreateParkingRq request, ServerCallContext context)
        {
            try
            {
                await _commandBus.Send(new CreateParkingCommand(request.MaxFloor, request.PlacesPerFloor, request.Address));
            }
            catch (Exception ex)
            {
                return new CreateParkingRs() { Success = false };
            }

            return new CreateParkingRs() { Success = true };
        }

        public override async Task<ParkVehicleRs> ParkVehicle(ParkVehicleRq request, ServerCallContext context)
        {
            if (!Guid.TryParse(request.ParkingId, null, out Guid parkingId) || !Guid.TryParse(request.VehicleId, null, out Guid vehicleId))
            {
                return new ParkVehicleRs() { ParkingPlaceId = null };
            }

            var parking = await _parkingRepository.FindByIdAsync(parkingId);
            if (parking == null)
            {
                return new ParkVehicleRs() { ParkingPlaceId = null };
            }

            GetVehicleRs result;
            try
            {   
                result = await _vehiclesClient.GetVehicleAsync(new GetVehicleRq()
                {
                    VehicleId = request.VehicleId,
                });
            } 
            catch (Exception ex)
            {
                return new ParkVehicleRs() { ParkingPlaceId = null };
            }
            if (parking.IsVehicleInParking(vehicleId))
            {
                return new ParkVehicleRs() { ParkingPlaceId = null };
            }
            var parkResult = parking.AddVehicle(vehicleId);
            _parkingRepository.Update(parking);
            await _parkingRepository.UnitOfWork.SaveChangesAsync();

            return new ParkVehicleRs() { ParkingPlaceId = parkResult?.ToString()};
        }
    }
}
