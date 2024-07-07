using Application.Base.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.VehicleSubcriptionHistory
{
    public class GetVehicleSubscriptionHistoryQuery : Query
    {
        public Guid VehicleId { get; set; }

        public GetVehicleSubscriptionHistoryQuery(Guid vehicleId) 
        {
            VehicleId = vehicleId;
        }

    }
}
