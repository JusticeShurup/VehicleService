using Application.Base.Command;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.CreateParking
{
    public class CreateParkingHandler : ICommandHandler<CreateParkingCommand>
    {
        private readonly IParkingRepository _parkingRepository;
        public CreateParkingHandler(IParkingRepository parkingRepository)
        {
            _parkingRepository = parkingRepository;
        }

        public async Task Handle(CreateParkingCommand command)
        {
            Domain.Parking parking = new Domain.Parking(command.FloorCount, command.PlacesPerFloor, command.Address);
            _parkingRepository.Add(parking);
            await _parkingRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}
