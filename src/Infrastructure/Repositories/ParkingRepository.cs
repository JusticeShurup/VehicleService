using Domain;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ParkingRepository
        : IParkingRepository
    {
        private readonly ParkingContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public ParkingRepository(ParkingContext context)
        {
            _context = context;
        }

        public Parking Add(Parking parking)
        {
            return _context.Parkings.Add(parking).Entity;
        }

        public async Task<Parking?> FindByIdAsync(Guid id)
        {
            return await _context.Parkings
                .Include(p => p.ParkingPlaces)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public Parking Update(Parking parking)
        {
            return _context.Parkings.Update(parking).Entity;
        }

        public void Remove(Parking parking)
        {
            _context.Remove(parking);
        }
    }
}
