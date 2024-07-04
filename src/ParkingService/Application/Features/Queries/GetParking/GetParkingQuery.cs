using Application.Base.Query;

namespace Application.Features.Queries.GetParking
{
    public class GetParkingQuery : Query
    {
        public Guid ParkingId { get; set; }

        public GetParkingQuery(Guid parkingId)
        {
            ParkingId = parkingId;
        }
    }
}
