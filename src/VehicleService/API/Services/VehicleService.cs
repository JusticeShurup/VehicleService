using Domain;
using Domain.Interface;
using Grpc.Core;
using ProtosContract;

namespace API.Services
{
    public class VehiclesService : Vehicles.VehiclesBase
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehiclesService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public override async Task<CreateVehicleRs> CreateVehicle(CreateVehicleRq request, ServerCallContext context)
        {
            try
            {
                var vehicle = new Vehicle(request.Name, request.MaxSpeed);
                _vehicleRepository.Add(vehicle);
                await _vehicleRepository.UnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return await Task.FromResult(new CreateVehicleRs()
                {
                    Success = false,
                });
            }


            return await Task.FromResult(new CreateVehicleRs
            {
                Success = true
            });
        }

        public override async Task<GetVehicleRs> GetVehicle(GetVehicleRq request, ServerCallContext context)
        {
            var vehicle = await _vehicleRepository.FindByIdAsync(Guid.Parse(request.VehicleId));

            return new GetVehicleRs()
            {
                Id = vehicle.Id.ToString(),
                MaxSpeed = vehicle.MaxSpeed,
                Name = vehicle.Name,
            };
        }

        public override async Task<DeleteVehicleRs> DeleteVehicle(DeleteVehicleRq request, ServerCallContext context)
        {
            var parseResult = Guid.TryParse(request.Id, out Guid vehicleId);
            if (!parseResult)
            {
                return new DeleteVehicleRs() { Success = false };
            }
            Vehicle? vehicle = await _vehicleRepository.FindByIdAsync(vehicleId);
            if (vehicle == null)
            {
                return new DeleteVehicleRs() { Success = false };
            }

            _vehicleRepository.Remove(vehicle);
            await _vehicleRepository.UnitOfWork.SaveChangesAsync();

            return new DeleteVehicleRs { Success = true };
        }
    }
}
