using Domain;

namespace Application.Interfaces
{
    public interface IParkingPlaceRepository
        : IRepository<ParkingPlace>
    {
        ParkingPlace Add(ParkingPlace parkingPlace);
        ParkingPlace Update(ParkingPlace parkingPlace);
        IEnumerable<ParkingPlace> GetAll();
        Task<ParkingPlace?> FindByIdAsync(Guid id);
        void Remove(ParkingPlace parkingPlace);
    }
}
