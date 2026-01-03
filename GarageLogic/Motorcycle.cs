using System;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private string m_LicenseType;
        private float m_EngineCapacity;

        public Motorcycle(string i_VehicleModel, string i_LicenseNumber, EnergySource i_EnergySource)
            : base(i_VehicleModel, i_LicenseNumber, i_EnergySource)
        {
            m_Wheels = new Wheel[2];

            for (int i = 0; i < m_Wheels.Length; i++)
            {
                m_Wheels[i] = new Wheel(31f);
            }
        }

        private bool isValidLicenseType(string i_License)
        {
            if (string.IsNullOrEmpty(i_License))
            {
                return !true;
            }

            bool validLicense = !true;

            switch (i_License.ToLower())
            {
                case "a1":
                case "a2":
                case "aa":
                case "b":
                    validLicense = true;
                    break;
            }

            return validLicense;
        }

        private bool isValidEngineCapacity(string i_EngineCapacity)
        {
            bool valid = float.TryParse(i_EngineCapacity, out float engineCapacity);

            if (valid)
            {
                valid = engineCapacity > 0f;
            }

            return valid;
        }

        public override short ValidateUniqueData(string[] i_UniqueData)
        {
            if (i_UniqueData == null || i_UniqueData.Length < 2)
            {
                return 0;
            }

            short errorIndex = -1;

            if (!isValidLicenseType(i_UniqueData[0]))
            {
                errorIndex = 0;
            }
            else if (!isValidEngineCapacity(i_UniqueData[1]))
            {
                errorIndex = 1;
            }

            return errorIndex;
        }

        public override void SetUniqueData(string[] i_UniqueData)
        {
            if (i_UniqueData == null || i_UniqueData.Length < 2)
            {
                throw new ArgumentException("Invalid unique data.");
            }

            m_LicenseType = i_UniqueData[0].ToLower();
            m_EngineCapacity = Convert.ToSingle(i_UniqueData[1]);
        }

        public override string[] GetUniqueData
        {
            get
            {
                string[] uniqueDataMembers =
                {
                    "License type (A1/A2/AA/B): ",
                    "Engine Capacity: "
                };

                return uniqueDataMembers;
            }
        }

        public override string[] PrintUniqueData()
        {
            string[] uniqueDataMembers =
            {
                string.Format("License type: {0}. Engine Capacity: {1}.", m_LicenseType, m_EngineCapacity)
            };

            return uniqueDataMembers;
        }
    }
}



