using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public class ElectricBattery : EnergySource
    {
      public ElectricBattery(float i_MaxHours) : base(i_MaxHours)
            {
            }

            public void Charge(float i_MinutesToCharge)
            {
                if (i_MinutesToCharge <= 0f)
                {
                    throw new ArgumentException("Charging time must be greater than 0.");
                }

                float hoursToCharge = i_MinutesToCharge / 60f;
                FillEnergy(hoursToCharge);
            }
        
    }
}
