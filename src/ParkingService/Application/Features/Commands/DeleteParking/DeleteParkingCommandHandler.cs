using Application.Base.Command;
using Application.Features.Commands.DeleteParking;
using Application.Interfaces;
using System.Windows.Input;

namespace Application.Features.Commands.DeleteParking
{
    public class DeleteParkingCommandHandler : ICommandHandler<DeleteParkingCommand>
    {
        private readonly IParkingRepository _parkingRepository;

        public DeleteParkingCommandHandler(IParkingRepository parkingRepository) 
        {
            _parkingRepository = parkingRepository;
        }

        public async Task Handle(DeleteParkingCommand command)
        {
            var parking = await _parkingRepository.FindByIdAsync(command.Id);
            if (parking == null)
            {
                throw new Exception("Parking doesn't exists");
            }
            _parkingRepository.Remove(parking);
            await _parkingRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}
