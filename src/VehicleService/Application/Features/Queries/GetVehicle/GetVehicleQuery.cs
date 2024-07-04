using Application.Base.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.GetVehicle
{
    public class GetVehicleQuery : Query
    {
        public Guid Id { get; }
    
        public GetVehicleQuery(Guid id)
        {
            Id = id;
        }
    }

}
