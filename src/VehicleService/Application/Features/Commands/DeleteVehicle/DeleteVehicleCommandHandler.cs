using Application.Base.Command;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.DeleteVehicle
{
    public class DeleteVehicleCommandHandler : ICommandHandler<DeleteVehicleCommand>
    {
        private readonly IVehicleRepository _vehicleRepository;
        public DeleteVehicleCommandHandler(IVehicleRepository vehicleRepository) 
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task Handle(DeleteVehicleCommand command)
        {

            var vehicle = await _vehicleRepository.FindByIdAsync(command.Id);
            if (vehicle == null)
            {
                throw new ArgumentException("Vehicle doesn't exist");
            }
            _vehicleRepository.Remove(vehicle);
            await _vehicleRepository.UnitOfWork.SaveChangesAsync();   
        }
    }
}
