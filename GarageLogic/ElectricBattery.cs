using System;

namespace Ex03.GarageLogic
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

            FillEnergy(i_MinutesToCharge / 60f);
        }
    }
}