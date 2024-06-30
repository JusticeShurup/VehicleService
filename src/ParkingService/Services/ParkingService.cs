using Domain;
using Domain.Interface;
using Grpc.Core;
using Grpc.Net.Client;
using Infrastructure.Repositories;
using ProtosContract;

namespace ParkingService.Services
{
    public class ParkingService : ProtosContract.Parking.ParkingBase
    {
        private readonly IParkingRepository _parkingRepository;
        private readonly IParkingPlaceRepository _parkingPlaceRepository;
         
        public ParkingService(IParkingRepository parkingRepository, IParkingPlaceRepository parkingPlaceRepository)
        {
            _parkingRepository = parkingRepository;
            _parkingPlaceRepository = parkingPlaceRepository;
        }

        public override async Task<CreateParkingRs> CreateParking(CreateParkingRq request, ServerCallContext context)
        {
            try
            {
                Domain.Parking parking = new Domain.Parking(request.MaxFloor, request.PlacesPerFloor, request.Address);
                _parkingRepository.Add(parking);
                await _parkingRepository.UnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new CreateParkingRs() { Success = false };
            }

            return new CreateParkingRs() { Success = true };
        }

        public override async Task<ParkVehicleRs> ParkVehicle(ParkVehicleRq request, ServerCallContext context)
        {
            var parking = await _parkingRepository.FindByIdAsync(Guid.Parse(request.ParkingId));

            if (parking == null)
            {
                return new ParkVehicleRs() { Success = false };
            }

            using var channelVehicle = GrpcChannel.ForAddress("https://localhost:7271");
            var vehicleClient = new Vehicles.VehiclesClient(channelVehicle);
            var vehicleResponse = await vehicleClient.GetVehicleAsync(new GetVehicleRq() { VehicleId = request.VehicleId});

            Vehicle vehicle = new Vehicle(Guid.Parse(vehicleResponse.Id), vehicleResponse.Name, vehicleResponse.MaxSpeed);
            parking.AddVehicle(vehicle);

            _parkingRepository.Update(parking);
            await _parkingRepository.UnitOfWork.SaveChangesAsync();

            return new ParkVehicleRs { Success = true };
        }
    }
}
