using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SubscriptionRepository
        : ISubscriptionRepository
    {
        private readonly ParkingContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public SubscriptionRepository(ParkingContext context) 
        {
            _context = context;
        }

        public Subscription Add(Subscription subscription)
        {
            return _context.Subscriptions.Add(subscription).Entity;
        }

        public async Task<Subscription?> FindByIdAsync(Guid id)
        {
            return await _context.Subscriptions
                .Include(p => p.ParkingPlace)
                .ThenInclude(p => p.Parking)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Subscription>> GetAllAsync()
        {
            return await _context.Subscriptions
                .Include(p => p.ParkingPlace)
                .ThenInclude(p => p.Parking)
                .ToListAsync();
        }

        public Subscription Update(Subscription subscription)
        {
            return _context.Subscriptions
                .Update(subscription).Entity;
        }
    }
}
