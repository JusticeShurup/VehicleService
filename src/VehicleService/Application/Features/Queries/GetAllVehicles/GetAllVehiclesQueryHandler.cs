using Application.Base.Query;
using Application.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.GetAllVehicles
{
    public class GetAllVehiclesQueryHandler : IQueryHandler<GetAllVehiclesQuery, IEnumerable<Vehicle>>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public GetAllVehiclesQueryHandler(IVehicleRepository vehicleRepository) 
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<IEnumerable<Vehicle>> Handle(GetAllVehiclesQuery query)
        {
            return await _vehicleRepository.GetAllAsync();
        }
    }
}
