using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using ProtosContract;
using VehicleApi.Services;

namespace VehicleApi.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    public class VehicleController : ControllerBase
    {
        private readonly VehiclesService _vehiclesService;
        private readonly ServerCallContext _callContext;

        public VehicleController(VehiclesService vehiclesService, ServerCallContext callContext) 
        {
            _vehiclesService = vehiclesService;
            _callContext = callContext;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<bool> CreateVehicle([FromBody] CreateVehicleRq request)
        {
            var result = await _vehiclesService.CreateVehicle(request, _callContext);
            return result.Success;
        }
    }
}
