using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ParkingPlace
    {
        public Guid Id { get; private set; }
        public int Floor { get; private set; }
        public Guid ParkingId { get; private set; }
        public Parking Parking { get; private set; }
        public Guid? VehicleId { get; private set; }
        public virtual Vehicle Vehicle { get; internal set; }

        public ParkingPlace()
        {

        }

        public ParkingPlace(int floor, Parking parking, Vehicle? vehicle = null) 
        {
            Floor = floor;
            Parking = parking;
            Vehicle = vehicle;
        }
    }
}
