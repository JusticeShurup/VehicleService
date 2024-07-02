using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EngineRepository
        : IEngineRepository
    {
        private readonly VehicleContext _context;
        public IUnitOfWork UnitOfWork => _context;


        public EngineRepository(VehicleContext vehicleContext)
        {
            _context = vehicleContext;
        }

        public async Task<Engine?> FindByIdAsync(Guid id)
        {
            return await _context.Engines
                .Where(engine => engine.Id == id)
                .FirstOrDefaultAsync();
        }

        public Engine Update(Engine engine)
        {
            return _context.Engines.Update(engine).Entity;
        }

    }
}
