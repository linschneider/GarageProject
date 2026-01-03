using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class Print
    {
        public static void PrintMainMenu()
        {
            Console.WriteLine("Hello and welcome to our Garage.");
            Console.WriteLine("Please enter an option: ");
            Console.WriteLine("1. Load vehicles from file (VehiclesDB.txt).");
            Console.WriteLine("2. Enter a new vehicle to the garage.");
            Console.WriteLine("3. Show current vehicles license plates in the garage.");
            Console.WriteLine("4. Change vehicle's status.");
            Console.WriteLine("5. Inflate vehicle wheels.");
            Console.WriteLine("6. Refill a vehicle running on fuel.");
            Console.WriteLine("7. Charge a vehicle running on electric.");
            Console.WriteLine("8. Show full information for specific license plate.");
            Console.WriteLine("9. Exit.");
        }

        public static void GetStatusFromUserPrint()
        {
            Console.WriteLine("Please choose to which status you want to change: ");
            Console.WriteLine("1. In Fixings.");
            Console.WriteLine("2. Fixed.");
            Console.WriteLine("3. Paid.");
        }

        public static void PrintVehicleDoesntExist()
        {
            Console.WriteLine("Vehicle doesn't exist. We need to open a new ticket. Please choose the type of vehicle: ");
            Console.WriteLine("1. Fuel motorcycle.");
            Console.WriteLine("2. Electric motorcycle.");
            Console.WriteLine("3. Fuel car.");
            Console.WriteLine("4. Electric car.");
            Console.WriteLine("5. Fuel truck.");
        }

        public static void PrintFuelOptionsToRefill()
        {
            Console.WriteLine("Please enter type of fuel.");
            Console.WriteLine("1. Soler.");
            Console.WriteLine("2. Octan 95.");
            Console.WriteLine("3. Octan 96.");
            Console.WriteLine("4. Octan 98.");
        }

        public static void ShowLicensePlatesPrint()
        {
            Console.WriteLine("Choose an option: ");
            Console.WriteLine("1. Show ALL license plates in the garage.");
            Console.WriteLine("2. Show license plates by current status.");
        }

        public static void PressAnyKeyToReturnToMainMenu()
        {
            Console.WriteLine("Press any key to return to main menu.");
            Console.ReadLine();
        }

        public static void ShowAllLicensePlates(Dictionary<string, Ticket> i_GarageTickets)
        {
            if (i_GarageTickets == null || i_GarageTickets.Count == 0)
            {
                Console.WriteLine("The garage is empty!");
                return;
            }

            foreach (KeyValuePair<string, Ticket> ticket in i_GarageTickets)
            {
                Console.WriteLine("Vehicle plate: {0}. Current status: {1}", ticket.Value.Vehicle.LicenseNumber, ticket.Value.CurrentStatus);
            }
        }

        public static void ShowSpecificLicensePlates(Dictionary<string, Ticket> i_GarageTickets)
        {
            if (i_GarageTickets == null || i_GarageTickets.Count == 0)
            {
                Console.WriteLine("The garage is empty!");
                return;
            }

            printSpecificPlatesMenu();

            while (true)
            {
                string userInput = Console.ReadLine();

                if (userInput == "1" || userInput == "2" || userInput == "3")
                {
                    printSpecificLicensePlateStatus(userInput, i_GarageTickets);
                    break;
                }

                Console.WriteLine("Invalid input. Please try again.");
            }
        }

        private static void printSpecificPlatesMenu()
        {
            Console.WriteLine("Please choose which status to see: ");
            Console.WriteLine("1. In fixings.");
            Console.WriteLine("2. Fixed.");
            Console.WriteLine("3. Paid.");
        }

        private static void printSpecificLicensePlateStatus(string i_UserInput, Dictionary<string, Ticket> i_GarageTickets)
        {
            Ticket.eCurrentStatus statusFromUser;

            switch (i_UserInput)
            {
                case "1":
                    statusFromUser = Ticket.eCurrentStatus.InFixings;
                    break;
                case "2":
                    statusFromUser = Ticket.eCurrentStatus.Fixed;
                    break;
                case "3":
                    statusFromUser = Ticket.eCurrentStatus.Paid;
                    break;
                default:
                    Console.WriteLine("Invalid input.");
                    return;
            }

            int counter = 0;

            foreach (KeyValuePair<string, Ticket> ticket in i_GarageTickets)
            {
                if (ticket.Value.CurrentStatus == statusFromUser)
                {
                    Console.WriteLine("Vehicle plate: {0}. Current status: {1}", ticket.Value.Vehicle.LicenseNumber, ticket.Value.CurrentStatus);
                    counter++;
                }
            }

            if (counter == 0)
            {
                Console.WriteLine("No vehicles found with the chosen status.");
            }
        }

        public static void CarIsNotRunningOnFuel()
        {
            Console.WriteLine("Vehicle is not running on fuel. Can't refill.");
        }

        public static void CarIsNotRunningOnElectricity()
        {
            Console.WriteLine("Vehicle is not running on electricity. Can't charge.");
        }

        public static void PrintVehicleStaticData(Ticket i_VehicleTicket)
        {
            Vehicle vehicle = i_VehicleTicket.Vehicle;

            Console.WriteLine("Vehicle License Plate: {0}.", vehicle.LicenseNumber);
            Console.WriteLine("Vehicle type: {0}.", vehicle.GetType().Name);
            Console.WriteLine("Model Name: {0}.", vehicle.ModelName);
            Console.WriteLine("Owner Name: {0}. Owner Number: {1}.", i_VehicleTicket.OwnerName, i_VehicleTicket.OwnerPhoneNumber);
            Console.WriteLine("Current Status: {0}.", i_VehicleTicket.CurrentStatus);
            Console.WriteLine("Number of wheels: {0}.", vehicle.Wheels.Length);

            if (vehicle.Wheels.Length > 0)
            {
                Console.WriteLine("Manufacturer for each: {0}.", vehicle.Wheels[0].Manufacturer);
                Console.WriteLine("Current air pressure in each: {0}.", vehicle.Wheels[0].CurrentAirPressure);
            }

            Console.WriteLine("Energy percent: {0}.", vehicle.EnergySource.EnergyPercentage);

            string[] uniqueLines = vehicle.PrintUniqueData();
            if (uniqueLines != null)
            {
                foreach (string line in uniqueLines)
                {
                    Console.WriteLine(line);
                }
            }
        }

        public static void PrintLoadSummary(int i_Added, int i_Skipped, int i_Failed)
        {
            Console.WriteLine("Load completed. Added: {0}. Skipped (exists): {1}. Failed: {2}.", i_Added, i_Skipped, i_Failed);
        }

        public static void PrintFileNotFound()
        {
            Console.WriteLine("VehiclesDB.txt was not found in the program folder (bin\\Debug).");
        }
    }
}