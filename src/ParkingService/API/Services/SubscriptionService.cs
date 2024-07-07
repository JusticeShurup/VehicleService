using Application.Base.Command;
using Application.Base.Query;
using Application.Features.Queries.GetAllSubscriptions;
using Application.Features.Queries.VehicleSubcriptionHistory;
using AutoMapper;
using Domain;
using Grpc.Core;
using ProtosContract;

namespace API.Services
{
    public class SubscriptionService : Subscriptions.SubscriptionsBase
    {
        private readonly IQueryBus _queryBus;
        private readonly ICommandBus _commandBus;
        private readonly IMapper _mapper;

        public SubscriptionService(IQueryBus queryBus, ICommandBus commandBus, IMapper mapper) 
        {
            _queryBus = queryBus;
            _commandBus = commandBus;
            _mapper = mapper;
        }

        public override async Task<GetAllSubscriptionsRs> GetAllSubscriptions(GetAllSubscriptionsRq request, ServerCallContext context)
        {
            var subscriptions = await _queryBus.Send<IEnumerable<Subscription>>(new GetAllSubscriptionsQuery());

            var response = new GetAllSubscriptionsRs()
            {
                Success = true,
                Error = "",
            };

            foreach (var subscription in subscriptions)
            {
                response.Subscriptions.Add(_mapper.Map<SubscriptionDto>(subscription));
            }

            return response;
        }

        public override async Task<GetVehicleSubscriptionHistoryRs> GetVehicleSubcriptionHistory(GetVehicleSubscriptionHistoryRq request, ServerCallContext context)
        {
            if (!Guid.TryParse(request.VehicleId, out Guid vehicleId))
            {
                return new GetVehicleSubscriptionHistoryRs()
                {
                    Success = false,
                    Error = "Incorrect id structure"
                };
            }
            var subscriptions = await _queryBus.Send<IEnumerable<Subscription>>(new GetVehicleSubscriptionHistoryQuery(vehicleId));
            var response = new GetVehicleSubscriptionHistoryRs() 
            {
                Success = true
            };
            foreach (var subscription in subscriptions)
            {
                response.Subscriptions.Add(_mapper.Map<SubscriptionDto>(subscription));
            }
            return response;
        }
    }
}
