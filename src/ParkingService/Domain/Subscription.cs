using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Subscription
    {
        public Guid Id { get; private set; }  
        public ParkingPlace ParkingPlace { get; private set; }
        public Guid VehicleId { get; private set; }
        public DateTime ParkingStartDate { get; private set; }
        public DateTime ParkingEndDate { get; private set; }
        public bool IsActive { get; private set; }

        private Subscription() { }

        public Subscription(ParkingPlace parkingPlace, Guid vehicleId, DateTime parkingStartDate, DateTime parkingEndDate)
        {
            ParkingPlace = parkingPlace;
            VehicleId = vehicleId;
            ParkingStartDate = parkingStartDate;
            ParkingEndDate = parkingEndDate;
            IsActive = true;
        }

        public void RenewAbonement(DateTime endDate)
        {
            ParkingEndDate = endDate;
        }


        /// <summary>
        /// Stop subcription and remove vehicle from parking place
        /// </summary>
        public void StopSubscription()
        {
            IsActive = false;
            ParkingPlace.VehicleId = null;
        }
    }
}
