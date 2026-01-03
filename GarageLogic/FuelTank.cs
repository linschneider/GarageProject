using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
namespace Ex03.GarageLogic
{
    public class FuelTank : EnergySource
    {
        public enum eFuelType { Soler, Octan95, Octan96, Octan98 }

        private readonly eFuelType m_FuelType;

        public FuelTank(eFuelType i_FuelType, float i_MaxLiter) : base(i_MaxLiter)
        {
            m_FuelType = i_FuelType;
        }

        public eFuelType FuelType
        {
            get
            {
                return m_FuelType;
            }
        }

        public void FillFuel(float i_LitersOfFuel, eFuelType i_FuelType)
        {
            if (i_FuelType != m_FuelType)
            {
                throw new ArgumentException("Wrong fuel type.");
            }

            FillEnergy(i_LitersOfFuel);
        }
    }
}