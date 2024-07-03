using Application.Base.Query;
using Application.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Features.Queries.GetParking
{
    public class GetParkingQueryHandler : IQueryHandler<GetParkingQuery, Parking>
    {

        private readonly IParkingRepository _parkingRepository;
        public GetParkingQueryHandler(IParkingRepository parkingRepository) 
        {
            _parkingRepository = parkingRepository;
        }

        public async Task<Parking> Handle(GetParkingQuery query)
        {
            var parking = await _parkingRepository.FindByIdAsync(query.ParkingId);
            if (parking == null)
            {
                throw new Exception("Parking not found");
            }

            return parking;
        }
    }
}
