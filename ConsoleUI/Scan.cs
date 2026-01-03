using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class Scan
    {
        public static bool CheckUserInput(string i_UserInput)
        {
            return i_UserInput == "1" || i_UserInput == "2" || i_UserInput == "3" || i_UserInput == "4"
                   || i_UserInput == "5" || i_UserInput == "6" || i_UserInput == "7" || i_UserInput == "8"
                   || i_UserInput == "9";
        }

        public static string GetNonEmptyString(string i_Prompt)
        {
            Console.WriteLine(i_Prompt);
            string input = Console.ReadLine();

            while (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Invalid input. Please try again.");
                Console.WriteLine(i_Prompt);
                input = Console.ReadLine();
            }

            return input;
        }

        public static bool GetTypeOfWheelInflation()
        {
            Console.WriteLine("Enter '1' for custom air pressure or any other key for maximum possible: ");
            return Console.ReadLine() != "1";
        }

        public static float GetAirPressureToFill()
        {
            Console.WriteLine("Please enter wanted air pressure: ");

            while (true)
            {
                string input = Console.ReadLine();

                if (float.TryParse(input, out float airAmount) && airAmount > 0f)
                {
                    return airAmount;
                }

                Console.WriteLine("Invalid input. Please enter a number greater than 0.");
                Console.WriteLine("Please enter wanted air pressure: ");
            }
        }

        public static Ticket.eCurrentStatus GetStatusFromUserScan()
        {
            while (true)
            {
                string userInput = Console.ReadLine();

                if (userInput == "1")
                {
                    return Ticket.eCurrentStatus.InFixings;
                }

                if (userInput == "2")
                {
                    return Ticket.eCurrentStatus.Fixed;
                }

                if (userInput == "3")
                {
                    return Ticket.eCurrentStatus.Paid;
                }

                Console.WriteLine("Invalid input. Please try again.");
            }
        }

        public static bool GetPlateAndCheckIfInGarageTickets(out string o_UserInput, Dictionary<string, Ticket> i_GarageTickets)
        {
            string userInput = GetNonEmptyString("Please enter vehicle license plate: ");

            if (i_GarageTickets != null && i_GarageTickets.ContainsKey(userInput))
            {
                o_UserInput = userInput;
                return true;
            }

            Console.WriteLine("License plate not found. Returning to main menu.");
            o_UserInput = string.Empty;
            return !true;
        }

        public static void GetDetailsForRefill(out float o_RefillAmount, out FuelTank.eFuelType o_FuelType)
        {
            o_RefillAmount = 0f;
            o_FuelType = FuelTank.eFuelType.Soler;

            Console.WriteLine("Please enter your refill amount (liters): ");

            while (true)
            {
                string userInput = Console.ReadLine();

                if (!float.TryParse(userInput, out float refillAmount) || refillAmount <= 0f)
                {
                    Console.WriteLine("Invalid input. Please enter a number greater than 0.");
                    continue;
                }

                Print.PrintFuelOptionsToRefill();
                o_FuelType = getFuelTypeToRefillFromUser();
                o_RefillAmount = refillAmount;
                break;
            }
        }

        private static FuelTank.eFuelType getFuelTypeToRefillFromUser()
        {
            while (true)
            {
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        return FuelTank.eFuelType.Soler;
                    case "2":
                        return FuelTank.eFuelType.Octan95;
                    case "3":
                        return FuelTank.eFuelType.Octan96;
                    case "4":
                        return FuelTank.eFuelType.Octan98;
                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }
            }
        }

        public static void GetDetailsForRecharge(out float o_MinutesToCharge)
        {
            o_MinutesToCharge = 0f;

            Console.WriteLine("Please enter minutes to charge: ");

            while (true)
            {
                string userInput = Console.ReadLine();

                if (!float.TryParse(userInput, out float minutes) || minutes <= 0f)
                {
                    Console.WriteLine("Invalid input. Please enter a number greater than 0.");
                    continue;
                }

                o_MinutesToCharge = minutes;
                break;
            }
        }

        public static string GetVehicleTypeByMenu()
        {
            string userInput = Console.ReadLine();

            while (true)
            {
                if (userInput == "1" || userInput == "2" || userInput == "3" || userInput == "4" || userInput == "5")
                {
                    return userInput;
                }

                Console.WriteLine("Invalid input. Please choose 1-5.");
                userInput = Console.ReadLine();
            }
        }

        public static string MapMenuChoiceToVehicleType(string i_Choice)
        {
            switch (i_Choice)
            {
                case "1":
                    return "FuelMotorcycle";
                case "2":
                    return "ElectricMotorcycle";
                case "3":
                    return "FuelCar";
                case "4":
                    return "ElectricCar";
                case "5":
                    return "FuelTruck";
                default:
                    return null;
            }
        }

        public static Ticket GetDetailsForTicket(Vehicle i_Vehicle)
        {
            string ownerName = GetNonEmptyString("Please enter owner's name: ");
            string ownerNumber = getPhoneNumber("Please enter owner phone number: ");
            return new Ticket(i_Vehicle, ownerName, ownerNumber);
        }

        private static string getPhoneNumber(string i_Prompt)
        {
            Console.WriteLine(i_Prompt);
            string input = Console.ReadLine();

            while (string.IsNullOrEmpty(input) || !double.TryParse(input, out _))
            {
                Console.WriteLine("Invalid input. Please enter digits only.");
                Console.WriteLine(i_Prompt);
                input = Console.ReadLine();
            }

            return input;
        }

        private static void getEnergyDetailsForVehicle(out float o_CurrentEnergyAmount, Vehicle i_Vehicle)
        {
            o_CurrentEnergyAmount = 0f;

            while (true)
            {
                Console.WriteLine("Enter current liters of fuel / hours of battery left: ");
                string userInput = Console.ReadLine();

                if (!float.TryParse(userInput, out float energyResult))
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    continue;
                }

                if (energyResult < 0f || energyResult > i_Vehicle.EnergySource.MaxEnergy)
                {
                    Console.WriteLine("Invalid input. Range is 0 to {0}.", i_Vehicle.EnergySource.MaxEnergy);
                    continue;
                }

                o_CurrentEnergyAmount = energyResult;
                break;
            }
        }

        private static void getDetailsForGenericWheel(out string o_ManufacturerName, out float o_CurrentAirPressure, float i_MaxAirPressure)
        {
            o_ManufacturerName = GetNonEmptyString("Enter wheel manufacturer name: ");
            o_CurrentAirPressure = 0f;

            Console.WriteLine("Please enter current air pressure: ");

            while (true)
            {
                string airPressure = Console.ReadLine();

                if (!float.TryParse(airPressure, out float userAirPressure))
                {
                    Console.WriteLine("Could not read your air pressure. Please make sure you use only numbers.");
                    continue;
                }

                if (userAirPressure < 0f || userAirPressure > i_MaxAirPressure)
                {
                    Console.WriteLine("Invalid input. Range is 0 to {0}.", i_MaxAirPressure);
                    continue;
                }

                o_CurrentAirPressure = userAirPressure;
                break;
            }
        }

        private static string[] getUniqueDataFromAnyVehicle(Vehicle i_Vehicle)
        {
            while (true)
            {
                string[] uniqueData = new string[i_Vehicle.GetUniqueData.Length];

                for (int i = 0; i < i_Vehicle.GetUniqueData.Length; i++)
                {
                    Console.WriteLine("Enter {0}", i_Vehicle.GetUniqueData[i]);
                    uniqueData[i] = Console.ReadLine();
                }

                if (i_Vehicle.ValidateUniqueData(uniqueData) != -1)
                {
                    Console.WriteLine("Unique data was not entered properly. Please try again.");
                    continue;
                }

                return uniqueData;
            }
        }

        public static void GetDetailsForOtherVehicle(Vehicle i_UserVehicle)
        {
            getEnergyDetailsForVehicle(out float currentEnergyAmount, i_UserVehicle);
            getDetailsForGenericWheel(out string manufacturerName, out float currentAirPressure, i_UserVehicle.Wheels[0].MaxAirPressure);

            i_UserVehicle.SetDynamicData(manufacturerName, currentAirPressure, currentEnergyAmount);

            string[] uniqueData = getUniqueDataFromAnyVehicle(i_UserVehicle);
            i_UserVehicle.SetUniqueData(uniqueData);
        }
    }
}
