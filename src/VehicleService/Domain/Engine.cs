using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Engine
    {
        public Guid Id { get; private set; }
        public int Capacity { get; private set; }
        public int FuelQuantity { get; set; }
        public virtual Vehicle Vehicle { get; internal set; }
        public EngineType EngineType { get; private set; }

        private Engine() { }
        public Engine(int capacity, EngineType engineType)
        {

            Capacity = capacity;
            FuelQuantity = 100;
            EngineType = engineType;
        }
    }
}
