using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Base.Command;

namespace Application.Features.Commands.CreateParking
{
    public class CreateParkingCommand : Command
    {
        public int MaxFloor { get; }
        public int PlacesPerFloor { get; }
        public string Address { get; }

        public CreateParkingCommand(int maxFloor, int placesPerFloor, string address) 
        {
            MaxFloor = maxFloor;
            PlacesPerFloor = placesPerFloor;
            Address = address;
        }
    }
}
