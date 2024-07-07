using Application.Base.Query;
using Application.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.GetAllParkingPlaces
{
    public class GetAllParkingPlacesQueryHandler : IQueryHandler<GetAllParkingPlacesQuery, IEnumerable<ParkingPlace>>
    {
        private readonly IParkingPlaceRepository _parkingPlaceRepository;

        public GetAllParkingPlacesQueryHandler(IParkingPlaceRepository parkingPlaceRepository)
        {
            _parkingPlaceRepository = parkingPlaceRepository;
        }

        public async Task<IEnumerable<ParkingPlace>> Handle(GetAllParkingPlacesQuery query)
        {
            return await Task.FromResult(_parkingPlaceRepository.GetAll());
        }
    }
}
