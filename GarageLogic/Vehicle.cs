using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public abstract class Vehicle
    {
        protected string m_Model;
        protected string m_LicenseNumber;
        protected float m_CurrentEnergyPercentage;
        protected Wheel[] m_Wheels;
    }
}
