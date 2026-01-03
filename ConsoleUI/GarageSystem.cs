using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageSystem
    {
        private static readonly Dictionary<string, Ticket> sr_GarageTickets = new Dictionary<string, Ticket>();

        public void RunGarage()
        {
            bool runGarage = true;

            while (runGarage)
            {
                short menuOption = getMainMenuOption();

                if (menuOption == 9)
                {
                    runGarage = !true;
                    continue;
                }

                runMenuOption(menuOption);
                Print.PressAnyKeyToReturnToMainMenu();
                Console.Clear();
            }

            Console.WriteLine("Exiting program. Have a wonderful day.");
        }

        private static short getMainMenuOption()
        {
            while (true)
            {
                Console.Clear();
                Print.PrintMainMenu();

                string userInput = Console.ReadLine();

                if (Scan.CheckUserInput(userInput))
                {
                    return Convert.ToInt16(userInput);
                }

                Console.Clear();
                Console.WriteLine("Please enter VALID input!");
                Print.PressAnyKeyToReturnToMainMenu();
            }
        }

        private void runMenuOption(short i_MenuOption)
        {
            switch (i_MenuOption)
            {
                case 1:
                    loadVehiclesFromFileAppend();
                    break;
                case 2:
                    enterNewVehicle();
                    break;
                case 3:
                    showLicensePlates();
                    break;
                case 4:
                    changeStatus();
                    break;
                case 5:
                    inflateWheels();
                    break;
                case 6:
                    refillVehicle();
                    break;
                case 7:
                    chargeElectricVehicle();
                    break;
                case 8:
                    showCarFullDetails();
                    break;
                default:
                    break;
            }
        }

        private static void loadVehiclesFromFileAppend()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "VehiclesDB.txt");

            if (!File.Exists(filePath))
            {
                Print.PrintFileNotFound();
                return;
            }

            int added = 0;
            int skipped = 0;
            int failed = 0;

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                if (line.StartsWith("*****"))
                {
                    break;
                }

                string[] parts = line.Split(',');

                if (parts.Length < 10)
                {
                    failed++;
                    continue;
                }

                string vehicleType = parts[0].Trim();
                string licenseNumber = parts[1].Trim();
                string modelName = parts[2].Trim();

                if (string.IsNullOrEmpty(vehicleType) || string.IsNullOrEmpty(licenseNumber) || string.IsNullOrEmpty(modelName))
                {
                    failed++;
                    continue;
                }

                if (!VehicleCreator.SupportedTypes.Contains(vehicleType))
                {
                    failed++;
                    continue;
                }

                if (sr_GarageTickets.ContainsKey(licenseNumber))
                {
                    skipped++;
                    continue;
                }

                if (!float.TryParse(parts[3].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out float energyPercentage))
                {
                    failed++;
                    continue;
                }

                string wheelManufacturer = parts[4].Trim();

                if (!float.TryParse(parts[5].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out float wheelCurrentPressure))
                {
                    failed++;
                    continue;
                }

                string ownerName = parts[6].Trim();
                string ownerPhone = parts[7].Trim();

                Vehicle vehicle;

                try
                {
                    vehicle = VehicleCreator.CreateVehicle(vehicleType, licenseNumber, modelName);
                    if (vehicle == null)
                    {
                        failed++;
                        continue;
                    }
                }
                catch
                {
                    failed++;
                    continue;
                }

                float currentEnergy = (energyPercentage / 100f) * vehicle.EnergySource.MaxEnergy;

                try
                {
                    vehicle.SetDynamicData(wheelManufacturer, wheelCurrentPressure, currentEnergy);
                }
                catch
                {
                    failed++;
                    continue;
                }

                string[] uniqueData = new string[] { parts[8].Trim(), parts[9].Trim() };

                try
                {
                    vehicle.SetUniqueData(uniqueData);
                }
                catch
                {
                    failed++;
                    continue;
                }

                Ticket ticket = new Ticket(vehicle, ownerName, ownerPhone);
                sr_GarageTickets.Add(licenseNumber, ticket);
                added++;
            }

            Print.PrintLoadSummary(added, skipped, failed);
        }

        private static void enterNewVehicle()
        {
            string licenseNumber = Scan.GetNonEmptyString("Please enter vehicle's license number: ");

            if (sr_GarageTickets.ContainsKey(licenseNumber))
            {
                Console.WriteLine("Vehicle exists. Changing status to IN FIXINGS.");
                sr_GarageTickets[licenseNumber].CurrentStatus = Ticket.eCurrentStatus.InFixings;
                return;
            }

            Print.PrintVehicleDoesntExist();
            string choice = Scan.GetVehicleTypeByMenu();
            string modelName = Scan.GetNonEmptyString("Enter model name: ");

            string vehicleType = Scan.MapMenuChoiceToVehicleType(choice);

            if (vehicleType == null)
            {
                Console.WriteLine("Invalid vehicle choice.");
                return;
            }

            Vehicle vehicle;

            try
            {
                vehicle = VehicleCreator.CreateVehicle(vehicleType, licenseNumber, modelName);
                if (vehicle == null)
                {
                    Console.WriteLine("Vehicle type is not supported.");
                    return;
                }
            }
            catch
            {
                Console.WriteLine("Failed creating vehicle.");
                return;
            }

            Scan.GetDetailsForOtherVehicle(vehicle);
            Ticket ticket = Scan.GetDetailsForTicket(vehicle);
            sr_GarageTickets.Add(vehicle.LicenseNumber, ticket);
        }

        private void showLicensePlates()
        {
            Print.ShowLicensePlatesPrint();

            while (true)
            {
                string userInput = Console.ReadLine();

                if (userInput == "1")
                {
                    Print.ShowAllLicensePlates(sr_GarageTickets);
                    break;
                }

                if (userInput == "2")
                {
                    Print.ShowSpecificLicensePlates(sr_GarageTickets);
                    break;
                }

                Console.WriteLine("Invalid input, please try again.");
            }
        }

        private void changeStatus()
        {
            if (Scan.GetPlateAndCheckIfInGarageTickets(out string licenseNumber, sr_GarageTickets))
            {
                Print.GetStatusFromUserPrint();
                Ticket.eCurrentStatus wantedStatus = Scan.GetStatusFromUserScan();
                sr_GarageTickets[licenseNumber].CurrentStatus = wantedStatus;
            }
        }

        private void inflateWheels()
        {
            if (Scan.GetPlateAndCheckIfInGarageTickets(out string licensePlate, sr_GarageTickets))
            {
                bool fillToMax = Scan.GetTypeOfWheelInflation();
                Wheel[] wheels = sr_GarageTickets[licensePlate].Vehicle.Wheels;

                if (fillToMax)
                {
                    foreach (Wheel wheel in wheels)
                    {
                        wheel.InflateWheelToMaximum();
                    }
                }
                else
                {
                    float amountToFill = Scan.GetAirPressureToFill();

                    try
                    {
                        foreach (Wheel wheel in wheels)
                        {
                            wheel.InflateWheel(amountToFill);
                        }
                    }
                    catch (ValueOutOfRangeException)
                    {
                        Console.WriteLine("You tried to fill more air than maximum.");
                    }
                }
            }
        }

        private void showCarFullDetails()
        {
            if (Scan.GetPlateAndCheckIfInGarageTickets(out string licensePlate, sr_GarageTickets))
            {
                Print.PrintVehicleStaticData(sr_GarageTickets[licensePlate]);
            }
        }

        private void chargeElectricVehicle()
        {
            if (!Scan.GetPlateAndCheckIfInGarageTickets(out string licensePlate, sr_GarageTickets))
            {
                return;
            }

            Ticket ticket = sr_GarageTickets[licensePlate];
            ElectricBattery battery = ticket.Vehicle.EnergySource as ElectricBattery;

            if (battery == null)
            {
                Print.CarIsNotRunningOnElectricity();
                return;
            }

            Scan.GetDetailsForRecharge(out float minutesToCharge);

            try
            {
                battery.Charge(minutesToCharge);
            }
            catch (ValueOutOfRangeException)
            {
                Console.WriteLine("You tried to charge above maximum.");
            }
        }

        private void refillVehicle()
        {
            if (!Scan.GetPlateAndCheckIfInGarageTickets(out string licensePlate, sr_GarageTickets))
            {
                return;
            }

            Ticket ticket = sr_GarageTickets[licensePlate];
            FuelTank fuelTank = ticket.Vehicle.EnergySource as FuelTank;

            if (fuelTank == null)
            {
                Print.CarIsNotRunningOnFuel();
                return;
            }

            Scan.GetDetailsForRefill(out float refillAmount, out FuelTank.eFuelType fuelTypeFromUser);

            try
            {
                fuelTank.FillFuel(refillAmount, fuelTypeFromUser);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Fuel type mismatch.");
            }
            catch (ValueOutOfRangeException)
            {
                Console.WriteLine("You tried to fill above maximum.");
            }
        }
    }
}
