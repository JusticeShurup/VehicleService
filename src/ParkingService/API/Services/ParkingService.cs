using API.Features.Commands.CreateParking;
using API.Features.Commands.DeleteParking;
using API.Features.Commands.ParkVehicle;
using API.Features.Queries.GetParking;
using Application.Base.Command;
using Application.Base.Query;
using Application.Interfaces;
using Google.Protobuf.Collections;
using Grpc.Core;
using ProtosContract;

namespace API.Services
{
    public class ParkingService : Parking.ParkingBase
    {
        private readonly IParkingRepository _parkingRepository;
        private readonly Vehicles.VehiclesClient _vehiclesClient;
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;

        public ParkingService(IParkingRepository parkingRepository,
            Vehicles.VehiclesClient vehiclesClient,
            ICommandBus commandBus,
            IQueryBus queryBus)
        {
            _parkingRepository = parkingRepository;
            _vehiclesClient = vehiclesClient;
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        public override async Task<CreateParkingRs> CreateParking(CreateParkingRq request, ServerCallContext context)
        {
            try
            {
                await _commandBus.Send(new CreateParkingCommand(request.FloorCount, request.PlacesPerFloor, request.Address));
            }
            catch (Exception ex)
            {
                return new CreateParkingRs() { Success = false, Error = ex.Message };
            }

            return new CreateParkingRs() { Success = true };
        }

        public override async Task<ParkVehicleRs> ParkVehicle(ParkVehicleRq request, ServerCallContext context)
        {
            if (!Guid.TryParse(request.ParkingId, null, out Guid parkingId) || !Guid.TryParse(request.VehicleId, null, out Guid vehicleId))
            {
                return new ParkVehicleRs()
                {
                    Success = false,
                    Error = "Inccorrect Id structure"
                };
            }

            try
            {
                await _commandBus.Send(new ParkVehicleCommand(vehicleId, parkingId));
            }
            catch (Exception ex)
            {
                return new ParkVehicleRs()
                {
                    Success = false, 
                    Error = ex.Message,
                };
            }

            return new ParkVehicleRs()
            {
                Success = true,
                Error = null
            };
        }

        public override async Task<GetParkingRs> GetParking(GetParkingRq request, ServerCallContext context)
        {
            if (!Guid.TryParse(request.Id, null, out Guid parkingId))
            {
                return new GetParkingRs() { Success = false, Error = "Incorrect Id structure" };
            }


            Domain.Parking parking;
            try
            {
                parking = await _queryBus.Send<Domain.Parking>(new GetParkingQuery(parkingId));
            }
            catch (Exception ex) 
            {
                return new GetParkingRs()
                {
                    Success = false,
                    Error = ex.Message,
                };
            }


            return new GetParkingRs()
            {
                Success = true,
                Parking = new ParkingDto() {
                    Id = request.Id, 
                    Address = parking.Address,
                    FloorCount = parking.FloorCount,
                    PlacesPerFloor = parking.PlacesPerFloor,
                }
            };
        }

        public override async Task<GetAllParkingsRs> GetAllParkings(GetAllParkingsRq request, ServerCallContext context)
        {
            var parkings = await _parkingRepository.GetAllAsync();
            var response = new GetAllParkingsRs()
            {
                Success = true,
                Error = ""
            };
            foreach (var parking in parkings)
            {
                response.Parkings.Add(new ParkingDto()
                {
                    Id = parking.Id.ToString(),
                    Address = parking.Address,
                    FloorCount = parking.FloorCount,
                    PlacesPerFloor = parking.PlacesPerFloor
                });
            }
            return response;
        }

        public override async Task<DeleteParkingRs> DeleteParking(DeleteParkingRq request, ServerCallContext context)
        {
            if (!Guid.TryParse(request.Id, null, out Guid parkingId))
            {
                return new DeleteParkingRs()
                {
                    Success = false,
                    Error = "Incorrect Id structure"
                };
            }

            try
            {
                await _commandBus.Send(new DeleteParkingCommand(parkingId));
            }
            catch (Exception ex)
            {
                return new DeleteParkingRs()
                {
                    Success = false,
                    Error = ex.Message,
                };
            }

            return new DeleteParkingRs()
            {
                Success = true,
                Error = ""
            };
        }
    }
}
