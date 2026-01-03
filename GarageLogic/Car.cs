using System;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private string m_Color;
        private short m_DoorCount;

        public Car(string i_VehicleModel, string i_LicenseNumber, EnergySource i_EnergySource)
            : base(i_VehicleModel, i_LicenseNumber, i_EnergySource)
        {
            m_Wheels = new Wheel[5];

            for (int i = 0; i < m_Wheels.Length; i++)
            {
                m_Wheels[i] = new Wheel(33f);
            }
        }

        private bool isValidColor(string i_Color)
        {
            if (string.IsNullOrEmpty(i_Color))
            {
                return !true;
            }

            bool validColor = !true;

            switch (i_Color.ToLower())
            {
                case "blue":
                case "green":
                case "white":
                case "black":
                    validColor = true;
                    break;
            }

            return validColor;
        }

        private bool isValidDoorCount(string i_DoorCount)
        {
            bool valid = short.TryParse(i_DoorCount, out short doorCount);

            if (valid)
            {
                valid = doorCount >= 2 && doorCount <= 5;
            }

            return valid;
        }

        public override short ValidateUniqueData(string[] i_UniqueData)
        {
            if (i_UniqueData == null || i_UniqueData.Length < 2)
            {
                return 0;
            }

            if (!isValidColor(i_UniqueData[0]))
            {
                return 0;
            }

            if (!isValidDoorCount(i_UniqueData[1]))
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

            m_Color = i_UniqueData[0].ToLower();
            m_DoorCount = Convert.ToInt16(i_UniqueData[1]);
        }

        public override string[] GetUniqueData
        {
            get
            {
                string[] uniqueDataMembers =
                {
                    "Car color (blue/green/white/black): ",
                    "Number of Doors (2/3/4/5): "
                };

                return uniqueDataMembers;
            }
        }

        public override string[] PrintUniqueData()
        {
            string[] uniqueDataMembers =
            {
                string.Format("Car color: {0}. Number Of Doors: {1}.", m_Color, m_DoorCount)
            };

            return uniqueDataMembers;
        }
    }
}



