using Domain;

namespace Application.Interfaces
{
    public interface IVehicleRepository
        : IRepository<Vehicle>
    {
        Vehicle Add(Vehicle vehicle);
        Vehicle Update(Vehicle vehicle);

        Task<IEnumerable<Vehicle>> GetAllAsync();
        Task<Vehicle?> FindByIdAsync(Guid id);
        void Remove(Vehicle vehicle);
    }
}
