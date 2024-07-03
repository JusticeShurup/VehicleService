using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Base.Command;

namespace API.Features.Commands.CreateParking
{
    public class CreateParkingCommand : Command
    {
        public int FloorCount { get; }
        public int PlacesPerFloor { get; }
        public string Address { get; }

        public CreateParkingCommand(int floorCount, int placesPerFloor, string address) 
        {
            FloorCount = floorCount;
            PlacesPerFloor = placesPerFloor;
            Address = address;
        }
    }
}
