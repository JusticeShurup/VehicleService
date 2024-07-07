using Application.Base.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.UpdateParkingPlace
{
    public class UpdateParkingPlaceCommand : Command
    {
        public Guid Id { get; set; }
        public bool IsWithElecticityCharge { get; set; }

        public UpdateParkingPlaceCommand(Guid id, bool isWithElectricityCharge) 
        {
            Id = id;
            IsWithElecticityCharge = isWithElectricityCharge;   
        }
    }
}
