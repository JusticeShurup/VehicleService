using Application.Base.Command;

namespace Application.Features.Commands.DeleteParking
{
    public class DeleteParkingCommand : Command
    {
        public Guid Id { get; set; }

        public DeleteParkingCommand(Guid id)
        {
            Id = id;
        }
    }
}
