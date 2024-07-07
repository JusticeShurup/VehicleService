using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ParkingPlaceRepository
        : IParkingPlaceRepository
    {
        private readonly ParkingContext _context; 
        public IUnitOfWork UnitOfWork => _context;

        
        public ParkingPlaceRepository(ParkingContext context)
        {
            _context = context;
        }

        public ParkingPlace Add(ParkingPlace parkingPlace)
        {
            return _context.ParkingPlaces.Add(parkingPlace).Entity;
        }

        public IEnumerable<ParkingPlace> GetAll()
        {
            return _context.ParkingPlaces;
        }

        public async Task<ParkingPlace?> FindByIdAsync(Guid id)
        {
            return await _context.ParkingPlaces
                .Include(p => p.Parking)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public void Remove(ParkingPlace parkingPlace)
        {
            _context.ParkingPlaces.Remove(parkingPlace); 
        }

        public ParkingPlace Update(ParkingPlace parkingPlace)
        {
            return _context.ParkingPlaces.Update(parkingPlace).Entity;
        }
    }
}
