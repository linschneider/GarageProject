using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelCar : Car
    {
        private const float k_MaxFuelLiters = 47f;

        public FuelCar(string i_LicenseNumber, string i_ModelName)
            : base(i_ModelName, i_LicenseNumber, new FuelTank(Fuel.eFuelType.Octan95, k_MaxFuelLiters))
        {
        }
    }
}