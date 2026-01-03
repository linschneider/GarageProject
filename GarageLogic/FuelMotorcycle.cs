namespace Ex03.GarageLogic
{
    public class FuelMotorcycle : Motorcycle
    {
        private const float k_MaxFuelLiters = 6.8f;

        public FuelMotorcycle(string i_LicenseNumber, string i_ModelName) : base(i_ModelName, i_LicenseNumber, new FuelTank(FuelTank.eFuelType.Octan98, k_MaxFuelLiters))
        {
        }
    }
}