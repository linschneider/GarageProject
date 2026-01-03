using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public class FuelTank : EnergySource
    {
        public enum eFuelType { Soler, Octan95, Octan96, Octan98 }

        private readonly eFuelType r_FuelType;

        public FuelTank(eFuelType i_FuelType, float i_MaxLiter): base(i_MaxLiter)
        {
            r_FuelType = i_FuelType;
        }

        public eFuelType FuelType
        {
            get
            {
                return r_FuelType;
            }
        }

        public void FillFuel(float i_LitersOfFuel, eFuelType i_FuelType)
        {
            if (i_FuelType != r_FuelType)
            {
                throw new ArgumentException("Wrong fuel type.");
            }

            FillEnergy(i_LitersOfFuel);
        }
    }
}