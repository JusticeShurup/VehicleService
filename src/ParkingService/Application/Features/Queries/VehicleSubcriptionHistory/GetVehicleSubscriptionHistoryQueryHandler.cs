using Application.Base.Query;
using Application.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.VehicleSubcriptionHistory
{
    public class GetVehicleSubscriptionHistoryQueryHandler : IQueryHandler<GetVehicleSubscriptionHistoryQuery, IEnumerable<Subscription>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public GetVehicleSubscriptionHistoryQueryHandler(ISubscriptionRepository subscriptionRepository) 
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<IEnumerable<Subscription>> Handle(GetVehicleSubscriptionHistoryQuery query)
        {
            return _subscriptionRepository.GetAllAsync().Result.Where(p => p.VehicleId == query.VehicleId);
        }
    }
}
