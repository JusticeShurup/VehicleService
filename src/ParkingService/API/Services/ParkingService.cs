using Grpc.Core;
using ProtosContract;

namespace API.Services
{
    public class ParkingService : Parking.ParkingBase
    {
        public ParkingService() 
        {

        }

        public override Task<CreateParkingRs> CreateParking(CreateParkingRq request, ServerCallContext context)
        {
            return base.CreateParking(request, context);
        }

        public override Task<ParkVehicleRs> ParkVehicle(ParkVehicleRq request, ServerCallContext context)
        {
            return base.ParkVehicle(request, context);
        }
    }
}
