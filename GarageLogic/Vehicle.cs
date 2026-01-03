using GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected string m_Model;
        protected string m_LicenseNumber;
        protected Wheel[] m_Wheels;
        protected EnergySource m_EnergySource;

        protected Vehicle(string i_VehicleModel, string i_LicenseNumber, EnergySource i_EnergySource)
        {
            if (string.IsNullOrEmpty(i_VehicleModel))
            {
                throw new ArgumentException("Vehicle model cannot be empty.");
            }

            if (string.IsNullOrEmpty(i_LicenseNumber))
            {
                throw new ArgumentException("License number cannot be empty.");
            }

            if (i_EnergySource == null)
            {
                throw new ArgumentNullException("i_EnergySource");
            }

            m_Model = i_VehicleModel;
            m_LicenseNumber = i_LicenseNumber;
            m_EnergySource = i_EnergySource;
        }

        public EnergySource EnergySource
        {
            get
            {
                return m_EnergySource;
            }
        }

        public Wheel[] Wheels
        {
            get
            {
                return m_Wheels;
            }
        }

        public string ModelName
        {
            get
            {
                return m_Model;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }
        }

        public float CurrentEnergyPercentage
        {
            get
            {
                return m_EnergySource.EnergyPercentage;
            }
        }

        public void SetDynamicData(string i_WheelManufacturer, float i_WheelCurrentPressure, float i_CurrentEngineEnergy)
        {
            if (m_Wheels == null || m_Wheels.Length == 0)
            {
                throw new InvalidOperationException("Wheels were not initialized.");
            }

            if (string.IsNullOrEmpty(i_WheelManufacturer))
            {
                throw new ArgumentException("Wheel manufacturer cannot be empty.");
            }

            foreach (Wheel wheel in m_Wheels)
            {
                wheel.Manufacturer = i_WheelManufacturer;
                wheel.CurrentAirPressure = i_WheelCurrentPressure;
            }

            m_EnergySource.CurrentEnergy = i_CurrentEngineEnergy;
        }

        public abstract short ValidateUniqueData(string[] i_UniqueData);

        public abstract void SetUniqueData(string[] i_UniqueData);

        public abstract string[] GetUniqueData
        {
            get;
        }

        public abstract string[] PrintUniqueData();
    }
}


