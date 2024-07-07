using Application.Base.Command;
using Application.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.UpdateParkingPlace
{
    public class UpdateParkingPlaceCommandHandler : ICommandHandler<UpdateParkingPlaceCommand>
    {
        private readonly IParkingPlaceRepository _parkingPlaceRepository;

        public UpdateParkingPlaceCommandHandler(IParkingPlaceRepository parkingPlaceRepository) 
        {
            _parkingPlaceRepository = parkingPlaceRepository;
        }

        public async Task Handle(UpdateParkingPlaceCommand command)
        {
            var parkingPlace = await _parkingPlaceRepository.FindByIdAsync(command.Id);
            if (parkingPlace == null) 
            {
                throw new Exception("Parking place wasn't found");
            }
            var updatedParkingPlace = new ParkingPlace(parkingPlace.Floor, parkingPlace.Parking, command.IsWithElecticityCharge, parkingPlace.VehicleId);
            _parkingPlaceRepository.Update(updatedParkingPlace);
            await _parkingPlaceRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}
