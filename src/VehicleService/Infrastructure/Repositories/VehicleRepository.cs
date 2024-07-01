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
    public class VehicleRepository
        : IVehicleRepository
    {
        private readonly VehicleContext _context;
        public IUnitOfWork UnitOfWork => _context;


        public VehicleRepository(VehicleContext vehicleContext)
        {
            _context = vehicleContext;
        }


        public Vehicle Add(Vehicle vehicle)
        {
            return _context.Vehicles.Add(vehicle).Entity;
        }

        public async Task<Vehicle?> FindByIdAsync(Guid id)
        {
            return await _context.Vehicles
                .Where(vehicle => vehicle.Id == id)
                .FirstOrDefaultAsync();
        }

        public Vehicle Update(Vehicle vehicle)
        {
            return _context.Vehicles.Update(vehicle).Entity;
        }

        public void Remove(Vehicle vehicle)
        {
            _context.Remove(vehicle);
        }
    }
}
