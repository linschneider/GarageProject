using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class EnergySource 
    {
        private readonly float r_MaxEnergy;
        private float m_CurrentEnergy;

        protected EnergySource(float i_MaxEnergy)
        {
            if (i_MaxEnergy <= 0f)
            {
                throw new ArgumentException("Max energy must be greater than 0.");
            }

            r_MaxEnergy = i_MaxEnergy;
            m_CurrentEnergy = 0f;
        }

        public void FillEnergy(float i_AmountToFill)
        {
            if (i_AmountToFill <= 0f)
            {
                throw new ArgumentException("Energy amount to fill must be greater than 0.");
            }

            float newEnergyAmount = m_CurrentEnergy + i_AmountToFill;

            if (newEnergyAmount <= r_MaxEnergy)
            {
                m_CurrentEnergy = newEnergyAmount;
            }
            else
            {
                throw new ValueOutOfRangeException(r_MaxEnergy - m_CurrentEnergy, 0f);
            }
        }

        public float CurrentEnergy
        {
            get
            {
                return m_CurrentEnergy;
            }
            set
            {
                if (value < 0f || value > r_MaxEnergy)
                {
                    throw new ValueOutOfRangeException(r_MaxEnergy, 0f);
                }

                m_CurrentEnergy = value;
            }
        }

        public float MaxEnergy
        {
            get
            {
                return r_MaxEnergy;
            }
        }

        public float EnergyPercentage
        {
            get
            {
                return (m_CurrentEnergy / r_MaxEnergy) * 100f;
            }
        }
    }
}
