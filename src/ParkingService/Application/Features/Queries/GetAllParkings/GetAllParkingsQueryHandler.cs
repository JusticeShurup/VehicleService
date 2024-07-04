using Application.Base.Query;
using Application.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.GetAllParkings
{
    public class GetAllParkingsQueryHandler : IQueryHandler<GetAllParkingsQuery, IEnumerable<Parking>>
    {
        private readonly IParkingRepository _parkingRepository;
        public GetAllParkingsQueryHandler(IParkingRepository parkingRepository) 
        {
            _parkingRepository = parkingRepository;
        }

        public async Task<IEnumerable<Parking>> Handle(GetAllParkingsQuery query)
        {
            return await _parkingRepository.GetAllAsync();
        }
    }
}
