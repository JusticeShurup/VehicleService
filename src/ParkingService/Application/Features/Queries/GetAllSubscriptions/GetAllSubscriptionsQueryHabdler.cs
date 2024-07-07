using Application.Base.Query;
using Application.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.GetAllSubscriptions
{
    public class GetAllSubscriptionsQueryHabdler : IQueryHandler<GetAllSubscriptionsQuery, IEnumerable<Subscription>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public GetAllSubscriptionsQueryHabdler(ISubscriptionRepository subscriptionRepository) 
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<IEnumerable<Subscription>> Handle(GetAllSubscriptionsQuery query)
        {
            return await _subscriptionRepository.GetAllAsync();   
        }
    }
}
