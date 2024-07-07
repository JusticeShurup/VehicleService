using Application.Base.Command;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.TakeVehicle
{
    public class TakeVehicleCommandHandler : ICommandHandler<TakeVehicleCommand>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        
        public TakeVehicleCommandHandler(ISubscriptionRepository subscriptionRepository) 
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task Handle(TakeVehicleCommand command)
        {
            var subscription = _subscriptionRepository.GetAllAsync().Result
                .Where(p => p.VehicleId == command.VehicleId).FirstOrDefault();
            if (subscription == null)
            {
                throw new Exception("Vehicle doesn't exists in parking service");
            }
            if (!subscription.IsActive)
            {
                throw new Exception("Vehicle already taken");
            }
            subscription.StopSubscription();
            _subscriptionRepository.Update(subscription);
            await _subscriptionRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}
