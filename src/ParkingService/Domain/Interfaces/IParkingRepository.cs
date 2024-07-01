using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IParkingRepository
        : IRepository<Parking>
    {
        Parking Add(Parking parking);
        Parking Update(Parking parking);
        Task<Parking?> FindByIdAsync(Guid id);
        void Remove(Parking parking);
    }
}
