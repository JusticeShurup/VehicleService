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
    public class ParkingPlaceRepository
        : IParkingPlaceRepository
    {
        private readonly ParkingContext _context; 
        public IUnitOfWork UnitOfWork => _context;

        public ParkingPlaceRepository(ParkingContext parkingContext)
        {
            _context = parkingContext;
        }

        public ParkingPlace Add(ParkingPlace parkingPlace)
        {
            return _context.ParkingPlaces.Add(parkingPlace).Entity;
        }

        public async Task<ParkingPlace?> FindByIdAsync(Guid id)
        {
            return await _context.ParkingPlaces
                .Include(p => p.Parking)
                .Include(p => p.Vehicle)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public ParkingPlace Update(ParkingPlace parkingPlace)
        {
            return _context.ParkingPlaces.Update(parkingPlace).Entity;
        }

        public void Remove(ParkingPlace parkingPlace)
        {
            _context.Remove(parkingPlace);
        }
    }
}
