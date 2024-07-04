using Application.Base.Command;
using Application.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.CreateVehicle
{
    public class CreateVehicleCommandHandler : ICommandHandler<CreateVehicleCommand>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public CreateVehicleCommandHandler(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task Handle(CreateVehicleCommand command)
        {
            var engine = new Engine(100, command.EngineType);
            var vehicle = new Vehicle(command.Name, engine);
            _vehicleRepository.Add(vehicle);
            await _vehicleRepository.UnitOfWork.SaveChangesAsync();

        }
    }
}
