using Application.Base.Command;
using Application.Base.Query;
using Application.Features.Queries.GetParking;
using Application.Features.Commands.CreateParking;
using Application.Interfaces;
using Grpc.Core;
using ProtosContract;
using Application.Features.Commands.ParkVehicle;
using Application.Features.Commands.DeleteParking;
using Application.Features.Queries.GetAllParkings;
using Application.Features.Commands.TakeVehicle;
using Application.Features.Commands.UpdateParkingPlace;
using Domain;
using Application.Features.Queries.GetAllParkingPlaces;
using AutoMapper;

namespace API.Services
{
    public class ParkingService : ProtosContract.Parking.ParkingBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;
        private readonly IMapper _mapper;

        public ParkingService(ICommandBus commandBus, IQueryBus queryBus, IMapper mapper)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
            _mapper = mapper;
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
                await _commandBus.Send(new ParkVehicleCommand(parkingId, vehicleId));
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
                Error = ""
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
            var parkings = await _queryBus.Send<IEnumerable<Domain.Parking>>(new GetAllParkingsQuery());
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

        public override async Task<TakeVehicleRs> TakeVehicle(TakeVehicleRq request, ServerCallContext context)
        {
            if (!Guid.TryParse(request.VehicleId, out Guid vehicleId))
            {
                return new TakeVehicleRs()
                {
                    Success = false,
                    Error = "Incorrect vehicle Id"
                };
            }

            try
            {
                await _commandBus.Send(new TakeVehicleCommand(vehicleId));
            }
            catch (Exception ex)
            {
                return new TakeVehicleRs()
                {
                    Success = false,
                    Error = ex.Message
                };
            }

            return new TakeVehicleRs()
            {
                Success = true
            };
        }

        public override async Task<GetAllParkingPlacesRs> GetAllParkingPlaces(GetAllParkingPlacesRq request, ServerCallContext context)
        {
            var parkingPlaces = await _queryBus.Send<IEnumerable<ParkingPlace>>(new GetAllParkingPlacesQuery());
            var response = new GetAllParkingPlacesRs()
            {
                Success = true,
                Error = "",
            };

            foreach (var parkingPlace in parkingPlaces)
            {
                response.ParkingPlaces.Add(_mapper.Map<ParkingPlaceDto>(parkingPlace));
            }

            return response;
        }

        public override async Task<UpdateParkingPlaceRs> UpdateParkingPlace(UpdateParkingPlaceRq request, ServerCallContext context)
        {
            if (!Guid.TryParse(request.Id, out Guid parkingPlaceId))
            {
                return new UpdateParkingPlaceRs()
                {
                    Success = false,
                    Error = "Incorrect parking place id"
                };
            }

            try
            {
                await _commandBus.Send(new UpdateParkingPlaceCommand(parkingPlaceId, request.IsWithElectricityCharge));
            }
            catch (Exception ex)
            {
                return new UpdateParkingPlaceRs()
                {
                    Success = false,
                    Error = ex.Message
                };
            }

            return new UpdateParkingPlaceRs() { Success = true, Error = "" };
        }
    }
}
