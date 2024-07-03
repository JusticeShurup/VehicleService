using Application.Base.Command;

namespace API.Features.Commands.DeleteParking
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
