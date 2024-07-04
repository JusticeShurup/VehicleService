using Application.Base.Command;
using Application.Base.Query;
using Application.Features.Commands.CreateVehicle;
using Application.Features.Commands.DeleteVehicle;
using Application.Features.Queries.GetAllVehicles;
using Application.Features.Queries.GetVehicle;
using Application.Interfaces;
using Domain;
using Grpc.Core;
using ProtosContract;

namespace API.Services
{
    public class VehiclesService : Vehicles.VehiclesBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;

        public VehiclesService(ICommandBus commandBus, IQueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        public override async Task<CreateVehicleRs> CreateVehicle(CreateVehicleRq request, ServerCallContext context)
        {
            try
            {
                await _commandBus.Send(new CreateVehicleCommand(request.Name, (EngineType)request.EngineType));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new CreateVehicleRs()
                {
                    Success = false,
                    Error = ex.Message,
                });
            }


            return await Task.FromResult(new CreateVehicleRs
            {
                Success = true,
                Error = "",
            });
        }

        public override async Task<GetVehicleRs> GetVehicle(GetVehicleRq request, ServerCallContext context)
        {
            if (!Guid.TryParse(request.Id, out Guid vehicleId))
            {
                return new GetVehicleRs()
                {
                    Success = false,
                    Error = "Inccorrect id structure"
                };
            }

            Vehicle vehicle;
            try
            {
                vehicle = await _queryBus.Send<Vehicle>(new GetVehicleQuery(vehicleId));
            }
            catch (Exception ex)
            {
                return new GetVehicleRs()
                {
                    Success = false,
                    Error = ex.Message,
                };
            }
            
            return new GetVehicleRs()
            {
                Success = true,
                Error = "",
                Vehicle = new VehicleDto
                {
                    Id = vehicle.Id.ToString(),
                    Name = vehicle.Name
                }
            };
        }

        public override async Task<GetAllVehiclesRs> GetAllVehicles(GetAllVehiclesRq request, ServerCallContext context)
        {
            var result = await _queryBus.Send<IEnumerable<Vehicle>>(new GetAllVehiclesQuery());

            var response = new GetAllVehiclesRs()
            {
                Success = true,
                Error = ""
            };

            foreach (var vehicle in result)
            {
                response.Vehicles.Add(new VehicleDto() { Id = vehicle.Id.ToString(), Name = vehicle.Name});
            }

            return response;
        }

        public override async Task<DeleteVehicleRs> DeleteVehicle(DeleteVehicleRq request, ServerCallContext context)
        {
            if (!Guid.TryParse(request.Id, out Guid vehicleId))
            {
                return new DeleteVehicleRs()
                {
                    Success = false,
                    Error = "Incorrect Id structure"
                };
            }

            try
            {
                await _commandBus.Send(new DeleteVehicleCommand(vehicleId));
            }
            catch (Exception ex)
            {
                return new DeleteVehicleRs
                {
                    Success = false,
                    Error = ex.ToString()
                };
            }

            return new DeleteVehicleRs { Success = true };
        }
    }
}
