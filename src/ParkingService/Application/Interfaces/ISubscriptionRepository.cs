using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISubscriptionRepository 
        : IRepository<Subscription>
    {
        Subscription Add(Subscription subscription);
        Subscription Update(Subscription subscription);
        Task<Subscription?> FindByIdAsync(Guid id);
        Task<IEnumerable<Subscription>> GetAllAsync();
    }
}
