using System;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_IsCarryingDangerousMaterials;
        private float m_TrunkCapacity;

        public Truck(string i_VehicleModel, string i_LicenseNumber, EnergySource i_EnergySource)
            : base(i_VehicleModel, i_LicenseNumber, i_EnergySource)
        {
            m_Wheels = new Wheel[12];

            for (int i = 0; i < m_Wheels.Length; i++)
            {
                m_Wheels[i] = new Wheel(26f);
            }
        }

        private bool isValidAnswer(string i_Input)
        {
            if (string.IsNullOrEmpty(i_Input))
            {
                return !true;
            }

            bool validInput = !true;

            switch (i_Input.ToLower())
            {
                case "yes":
                case "no":
                    validInput = true;
                    break;
            }

            return validInput;
        }

        private bool isValidCapacity(string i_TrunkCapacity)
        {
            bool valid = float.TryParse(i_TrunkCapacity, out float trunkCapacity);

            if (valid)
            {
                valid = trunkCapacity > 0f;
            }

            return valid;
        }

        public override short ValidateUniqueData(string[] i_UniqueData)
        {
            if (i_UniqueData == null || i_UniqueData.Length < 2)
            {
                return 0;
            }

            if (!isValidAnswer(i_UniqueData[0]))
            {
                return 0;
            }

            if (!isValidCapacity(i_UniqueData[1]))
            {
                return 1;
            }

            return -1;
        }

        public override void SetUniqueData(string[] i_UniqueData)
        {
            if (i_UniqueData == null || i_UniqueData.Length < 2)
            {
                throw new ArgumentException("Invalid unique data.");
            }

            m_IsCarryingDangerousMaterials = i_UniqueData[0].ToLower() == "yes";
            m_TrunkCapacity = Convert.ToSingle(i_UniqueData[1]);
        }

        public override string[] PrintUniqueData()
        {
            string[] uniqueDataMembers =
            {
                string.Format(
                    "Carrying dangerous materials: {0}.{2}Trunk capacity: {1}.",
                    m_IsCarryingDangerousMaterials,
                    m_TrunkCapacity,
                    Environment.NewLine)
            };

            return uniqueDataMembers;
        }

        public override string[] GetUniqueData
        {
            get
            {
                string[] uniqueDataMembers =
                {
                    "Carrying dangerous materials (YES/NO): ",
                    "Trunk Capacity: "
                };

                return uniqueDataMembers;
            }
        }
    }
}



