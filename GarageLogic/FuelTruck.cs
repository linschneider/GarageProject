namespace Ex03.GarageLogic
{
    public class FuelTruck : Truck
    {
        private const float k_MaxFuelLiters = 140f;

        public FuelTruck(string i_LicenseNumber, string i_ModelName) : base(i_ModelName, i_LicenseNumber, new FuelTank(FuelTank.eFuelType.Soler, k_MaxFuelLiters))
        {
        }
    }
}