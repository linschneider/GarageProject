using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_Manufacturer;
        private float m_CurrentAirPressure;
        private float m_MaxAirPressure;

        public Wheel(float i_MaxAirPressure)
        {
            if (i_MaxAirPressure <= 0f)
            {
                throw new ArgumentException("Max air pressure must be greater than 0.");
            }

            m_MaxAirPressure = i_MaxAirPressure;
            m_CurrentAirPressure = 0f;
        }

        public float MaxAirPressure
        {
            get
            {
                return m_MaxAirPressure;
            }
        }

        public string Manufacturer
        {
            get
            {
                return m_Manufacturer;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Manufacturer cannot be empty.");
                }

                m_Manufacturer = value;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }
            set
            {
                if (value < 0f || value > m_MaxAirPressure)
                {
                    throw new ValueOutOfRangeException(m_MaxAirPressure, 0f);
                }

                m_CurrentAirPressure = value;
            }
        }

        public void InflateWheel(float i_AirAmount)
        {
            if (i_AirAmount <= 0f)
            {
                throw new ArgumentException("Air amount must be greater than 0.");
            }

            float newPressure = m_CurrentAirPressure + i_AirAmount;

            if (newPressure <= m_MaxAirPressure)
            {
                m_CurrentAirPressure = newPressure;
            }
            else
            {
                throw new ValueOutOfRangeException(m_MaxAirPressure - m_CurrentAirPressure, 0f);
            }
        }

        public void InflateWheelToMaximum()
        {
            m_CurrentAirPressure = m_MaxAirPressure;
        }
    }
}


