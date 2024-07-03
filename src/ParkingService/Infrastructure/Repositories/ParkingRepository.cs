using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<Parking>> GetAllAsync()
        {
            return await _context.Parkings
                .Include(p => p.ParkingPlaces)
                .ToListAsync();
        }
        public async Task<Parking?> FindByIdAsync(Guid id)
        {
            return await _context.Parkings
                .Include(p => p.ParkingPlaces)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public void Remove(Parking parking)
        {
            _context.Parkings.Remove(parking);
        }

        public Parking Update(Parking parking)
        {
            return _context.Parkings.Update(parking).Entity;
        }
    }
}
