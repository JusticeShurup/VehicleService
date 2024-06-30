using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IVehicleRepository
        : IRepository<Vehicle>
    {
        Vehicle Add(Vehicle vehicle);
        Vehicle Update(Vehicle vehicle);
        Task<Vehicle?> FindByIdAsync(Guid id);
        void Remove(Vehicle vehicle);
    }
}
