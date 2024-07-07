using Application.Base.Query;
using Application.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.GetVehicle
{
    public class GetVehicleQueryHandler : IQueryHandler<GetVehicleQuery, Vehicle>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public GetVehicleQueryHandler(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Vehicle> Handle(GetVehicleQuery query)
        {
            var vehicle = await _vehicleRepository.FindByIdAsync(query.Id);
            if (vehicle == null)
            {
                throw new Exception("Vehicle didn't found");
            }

            return vehicle;
        }
    }
}
