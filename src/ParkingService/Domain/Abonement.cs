using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Abonement
    {
        public Guid Id { get; private set; }  
        public ParkingPlace ParkingPlace { get; private set; }
        public Guid VehicleId { get; private set; }
        public DateTime ParkingStartDate { get; private set; }
        public DateTime ParkingEndDate { get; private set; }
        private Abonement() { }

        public Abonement(ParkingPlace parkingPlace, Guid vehicleId, DateTime parkingStartDate, DateTime parkingEndDate)
        {
            ParkingPlace = parkingPlace;
            VehicleId = vehicleId;
            ParkingStartDate = parkingStartDate;
            ParkingEndDate = parkingEndDate;
        }

        public void RenewAbonement(DateTime endDate)
        {
            ParkingEndDate = endDate;
        }
    }
}
