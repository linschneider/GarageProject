namespace Ex03.GarageLogic
{
    public class ElectricCar : Car
    {
        private const float k_MaxBatteryHours = 4.2f;

        public ElectricCar(string i_LicenseNumber, string i_ModelName) : base(i_ModelName, i_LicenseNumber, new ElectricBattery(k_MaxBatteryHours))
        {
        }
    }
}