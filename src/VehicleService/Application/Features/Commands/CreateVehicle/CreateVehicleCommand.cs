using Application.Base.Command;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.CreateVehicle
{
    public class CreateVehicleCommand : Command
    {
        public string Name { get; }
        public EngineType EngineType { get; }

        public CreateVehicleCommand(string name, EngineType engineType) 
        {
            Name = name;
            EngineType = engineType;
        }
    }
}
