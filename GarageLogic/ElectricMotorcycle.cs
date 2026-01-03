using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : Motorcycle
    {
        private const float k_MaxBatteryHours = 2.6f;

        public ElectricMotorcycle(string i_LicenseNumber, string i_ModelName)
            : base(i_ModelName, i_LicenseNumber, new ElectricBattery(k_MaxBatteryHours))
        {
        }
    }
}