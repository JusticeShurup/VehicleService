using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IParkingPlaceRepository
        : IRepository<ParkingPlace>
    {
        ParkingPlace Add(ParkingPlace parkingPlace);
        ParkingPlace Update(ParkingPlace parkingPlace);
        Task<ParkingPlace?> FindByIdAsync(Guid id);
        void Remove(ParkingPlace parkingPlace);
    }
}
