using Domain;

namespace Application.Interfaces
{
    public interface IParkingRepository
        : IRepository<Parking>
    {
        Parking Add(Parking parking);
        Parking Update(Parking parking);

        Task<IEnumerable<Parking>> GetAllAsync();
        Task<Parking?> FindByIdAsync(Guid id);
        void Remove(Parking parking);
    }
}
