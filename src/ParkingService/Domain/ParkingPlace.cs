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
        public Parking Parking { get; private set; }
        public Guid? VehicleId { get; internal set; }
        public bool IsWithElectricityCharge { get; private set; }

        private ParkingPlace()
        {

        }

        public ParkingPlace(int floor, Parking parking, bool isWithElectrityCharge = false, Guid? vehicleId = null)
        {
            Floor = floor;
            Parking = parking;
            VehicleId = vehicleId;
            IsWithElectricityCharge = isWithElectrityCharge; 
        }
    }
}
