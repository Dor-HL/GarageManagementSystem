using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UserInterface
    {
        private readonly GarageManagement r_GarageManagement;

        public UserInterface()
        {
            r_GarageManagement = new GarageManagement();
        }

        public int GetAndCheckIntInputValue(float i_MaxValue, float i_MinValue) 
        {
            bool isInputValid = false;
            int inputAsInt = 0;
            bool isException = false;

            while (!isInputValid)
            {
                string input = Console.ReadLine();
                try
                {
                    inputAsInt = int.Parse(input);
                }
                catch (FormatException)
                {
                    Console.WriteLine("ERROR! Please choose a number, not a letter or symbol");
                    isException = true;
                }

                try
                {
                    if ((inputAsInt >= i_MinValue && inputAsInt <= i_MaxValue) && !isException) 
                    {
                        isInputValid = true;
                    }
                    else if (!isException)
                    {
                        throw new ValueOutOfRangeException(new Exception(), i_MaxValue, i_MinValue, "input");
                    }
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                isException = false;
            }

            return inputAsInt;
        }

        public int DisplayGarageMenu()
        {
            Console.Clear();
            Console.WriteLine("Hello! Welcome to our garage! How may we help you today?");
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("Please press 1 to enter a new vehicle to the garage");
            Console.WriteLine("Please press 2 to display license plate numbers of vehicles in the garage");
            Console.WriteLine("please press 3 to change status of existing vehicle in the garage");
            Console.WriteLine("Please press 4 to fill vehicle tire's air pressure to maximum");
            Console.WriteLine("Please press 5 to fuel vehicle");
            Console.WriteLine("Please press 6 to charge battery of vehicle");
            Console.WriteLine("Please press 7 to full details of all vehicles in garage");
            Console.WriteLine("Please press 8 to exit garage management program");
            return GetAndCheckIntInputValue(8, 1);
        }

        public void RunGarage()
        {
            bool isProgramStillOn = true;
            while(isProgramStillOn)
            {
                int userInput = DisplayGarageMenu();
                switch(userInput)
                {
                    case 1:
                        {
                            AddVehicleToGarage();
                            break;
                        }

                    case 2:
                        {
                            DisplayCarsLicensePlates();
                            break;
                        }

                    case 3:
                        {
                            ChangeStatusOfVehicle();
                            break;
                        }

                    case 4:
                        {
                            FillMaximumAirPressure();
                            break;
                        }

                    case 5:
                        {
                            FillFuel();
                            break;
                        }

                    case 6:
                        {
                            FillBattery();
                            break;
                        }

                    case 7:
                        {
                            DisplayFullDetails();
                            break;
                        }

                    case 8:
                        {
                            Console.Clear();
                            Console.WriteLine("Thank you for using our garage system. Have a nice day!");
                            isProgramStillOn = false;
                            break;
                        }
                }
            }
        }

        public void AddVehicleToGarage()
        {
            CreateNewVehicle createNewVehicle = new CreateNewVehicle();
            Dictionary<string, object> parameterList = new Dictionary<string, object>();
            Dictionary<int, string> namesAndValues = createNewVehicle.GetAllVehiclesNamesAndValues();
            int minValue = 1;
            int maxValue = namesAndValues.Count;
            Console.Clear();
            string failedCreation = "Vehicle addition was not successful due to the error above";

            foreach (var currentValue in namesAndValues)
            {
                Console.WriteLine("Please press {0} to enter a new {1}", currentValue.Key, currentValue.Value);
            }

            int inputAsInt = GetAndCheckIntInputValue(maxValue, minValue);
            parameterList = createNewVehicle.CreateParameterList((CreateNewVehicle.eVehicle)inputAsInt);
            object[] paramArray = new object[parameterList.Count];
            object[] finalParameterList = new object[parameterList.Count];
            int locationInArray = 0;
            bool isCurrentInputValid = false;
            foreach(var currentParameter in parameterList)
            {
                while(!isCurrentInputValid)
                {
                    Console.WriteLine(currentParameter.Key);
                    string parameter = Console.ReadLine();
                    Type type = currentParameter.Value.GetType();
                    try
                    {
                        paramArray[locationInArray] = parameter;
                        paramArray[locationInArray] = Convert.ChangeType(paramArray[locationInArray], type);
                        isCurrentInputValid = true;
                    }
                    catch(FormatException)
                    {
                        Console.WriteLine("Please enter a valid input!");
                    }
                }

                locationInArray++;
                isCurrentInputValid = false;
            }

            try
            {
                Vehicle newVehicle = createNewVehicle.CreateVehicle((CreateNewVehicle.eVehicle)inputAsInt, paramArray);
                r_GarageManagement.AddNewVehicle(newVehicle);
                Console.WriteLine("Vehicle was added successfully to the garage");
            }
            catch(ValueOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(failedCreation);
            }
            catch(FormatException formatEx)
            {
                Console.WriteLine(formatEx.Message);
                Console.WriteLine(failedCreation);
            }
            catch(ArgumentException argument)
            {
                Console.WriteLine(argument.Message);
                Console.WriteLine(failedCreation);
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine(failedCreation);
            }

            Thread.Sleep(3500);
        }

        public void ChangeStatusOfVehicle()
        {
            Console.WriteLine("Please enter vehicle's license plate");
            string licensePlate = GetAndCheckLicensePlateValidity(); 
            if (licensePlate != "-1")
            { 
                Vehicle.eVehicleStatus currentStatus = r_GarageManagement.FindVehicle(licensePlate).CurrentStatus; 
                Console.WriteLine("Please choose the new vehicle status: "); 
                Console.WriteLine("Press 1 to change status to 'inRepair'"); 
                Console.WriteLine("press 2 to change status to 'repaired'"); 
                Console.WriteLine("press 3 to change status to 'Paid'"); 
                int input = GetAndCheckIntInputValue(3, 1);
           
                Vehicle.eVehicleStatus newStatus;

                switch(input)
                {
                    case 1:
                        {
                            newStatus = Vehicle.eVehicleStatus.InRepair;
                            break;
                        }

                    case 2:
                        {
                            newStatus = Vehicle.eVehicleStatus.Repaired;
                            break;
                        }

                    case 3:
                        {
                            newStatus = Vehicle.eVehicleStatus.Paid;
                            break;
                        }

                    default:
                        {
                            newStatus = Vehicle.eVehicleStatus.InRepair;
                            break;
                        }
                }

                try
                {
                    r_GarageManagement.ChangeVehicleStatus(licensePlate, newStatus);
                    string endOfChange = string.Format(
                        "Vehicle status changed successfully from {0} to {1}!",
                        currentStatus,
                        newStatus);
                    Console.WriteLine(endOfChange);
                }
                catch(ArgumentException argument)
                {
                    Console.WriteLine(argument.Message);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Thread.Sleep(3500);
            }
        }

        public string GetAndCheckLicensePlateValidity()
        {
            bool isInputValid = false;
            bool isException = false;
            string licensePlate = string.Empty;
            int licensePlateAsInt = 0;
            while (!isInputValid)
            {
                licensePlate = Console.ReadLine();
                try
                {
                    licensePlateAsInt = int.Parse(licensePlate);
                    if (licensePlateAsInt == -1)
                    {
                        isInputValid = true;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a valid license plate, the plate should only contain numbers");
                    isException = true;
                }

                if (!isException && licensePlateAsInt != -1)
                {
                    try
                    {
                        if (r_GarageManagement.FindVehicle(licensePlate) == null)
                        {
                            throw new ArgumentException("Vehicle was not found in the system.");
                        }

                        isInputValid = true;
                    }
                    catch (ArgumentException argument)
                    {
                        Console.WriteLine(argument.Message);
                        Console.WriteLine("please re-enter license plate number or enter -1 to go back to the main menu");
                    }
                }

                isException = false;
            }

            return licensePlate;
        }

        public void FillMaximumAirPressure()
        {
            Console.WriteLine("Please enter the license plate of the car in order to fill air pressure to maximum");
            string licensePlate = GetAndCheckLicensePlateValidity();
            if(licensePlate != "-1")
            {
                try
                {
                    r_GarageManagement.FillAirToMaxInTire(licensePlate);
                    string endFillAir = string.Format("Air was filled successfully in vehicle. license Plate: {0}!", licensePlate);
                    Console.WriteLine(endFillAir);
                }
                catch(ArgumentException argument)
                {
                    Console.WriteLine(argument.Message);
                }
                catch(ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Thread.Sleep(3500);
            }
        }

        public void FillFuel()
        {
            Console.WriteLine("Please enter the license plate of the car");
            string licensePlate = GetAndCheckLicensePlateValidity(); 
            if(licensePlate != "-1")
            {
                Console.WriteLine("Please enter Fuel type: ");
                Console.WriteLine("Please Press 1 for Octan98");
                Console.WriteLine("Please press 2 for Octan96");
                Console.WriteLine("Please press 3 for Octan95");
                Console.WriteLine("Please press 4 for Solar");
                int fuelNumber = GetAndCheckIntInputValue(4, 1);
                FuelEngine.eFuelTypes chosenFuel;

                switch(fuelNumber)
                {
                    case 1:
                        {
                            chosenFuel = FuelEngine.eFuelTypes.Octan98;
                            break;
                        }

                    case 2:
                        {
                            chosenFuel = FuelEngine.eFuelTypes.Octan96;
                            break;
                        }

                    case 3:
                        {
                            chosenFuel = FuelEngine.eFuelTypes.Octan95;
                            break;
                        }

                    case 4:
                        {
                            chosenFuel = FuelEngine.eFuelTypes.Soler;
                            break;
                        }

                    default:
                        {
                            chosenFuel = FuelEngine.eFuelTypes.Soler;
                            break;
                        }
                }

                Console.WriteLine("Please enter how much fuel to add (in Liters)");
                float fuelInLitersAsFloat = 0;
                bool isParseSuccessful = false;
                bool didExceptionHappen = false;
                while(!isParseSuccessful)
                {
                    string fuelInLiters = Console.ReadLine();
                    try
                    {
                        float.TryParse(fuelInLiters, out fuelInLitersAsFloat);
                    }
                    catch(FormatException)
                    {
                        Console.WriteLine("Please enter a number only, not a symbol or a character");
                        didExceptionHappen = true;
                    }

                    if(!didExceptionHappen)
                    {
                        isParseSuccessful = true;
                    }
                }

                try
                {
                    r_GarageManagement.FillFuel(licensePlate, fuelInLitersAsFloat, chosenFuel);
                    Console.Write("Vehicle fueled successfully!");
                }
                catch(FormatException formatException)
                {
                    Console.WriteLine(formatException.Message);
                }
                catch(ArgumentException argument)
                {
                    Console.WriteLine(argument.Message);
                }
                catch(ValueOutOfRangeException range)
                {
                    Console.WriteLine(range.Message);
                }

                Thread.Sleep(3500);
            }
        }

        public void FillBattery()
        {
            Console.WriteLine("Please enter the license plate of the car");
            string licensePlate = GetAndCheckLicensePlateValidity(); 
            if(licensePlate != "-1")
            {
                Console.WriteLine("Please enter how many minutes to charge"); 
                float minutesToChargeAsFloat = 0;
                bool isValid = false;
                bool isException = false;
                while(!isValid)
                {
                    string hoursToCharge = Console.ReadLine();
                    try
                    {
                        float.TryParse(hoursToCharge, out minutesToChargeAsFloat);
                    }
                    catch(FormatException)
                    {
                        Console.WriteLine("Please choose a number! not a symbol or a letter");
                        isException = true;
                    }
                    catch(ValueOutOfRangeException range)
                    {
                        Console.WriteLine(range.Message);
                    }

                    if(!isException)
                    {
                        isValid = true;
                    }
                }

                try
                {
                    r_GarageManagement.FillBattery(licensePlate, minutesToChargeAsFloat);
                    Console.WriteLine("Battery filled successfully!");
                }
                catch(FormatException formatException)
                {
                    Console.WriteLine(formatException.Message);
                }
                catch(ArgumentException argument)
                {
                    Console.WriteLine(argument.Message);
                }
                catch(ValueOutOfRangeException range)
                {
                    Console.WriteLine(range.Message);
                }

                Thread.Sleep(3500);
            }
        }

        public int DisplayCarsLicensePlatesMenu()
        {
            Console.WriteLine("Please press 1 to view full list of license plates numbers ");
            Console.WriteLine("Please press 2 to view only 'inRepair' status vehicles ");
            Console.WriteLine("Please press 3 to view only 'repaired' status vehicles ");
            Console.WriteLine("Please press 4 to view only 'paid' status vehicles ");
            Console.WriteLine("Please press 5 to go back to the main menu ");
            int input = GetAndCheckIntInputValue(5, 1);
            return input;
        }

        public void DisplayCarsLicensePlates()
        {
            Console.Clear();
            string displayMessage = string.Empty;
            int input = DisplayCarsLicensePlatesMenu();
            bool exitMenu = false;
            StringBuilder printLicensePlates = new StringBuilder();
            while(!exitMenu)
            {
                switch(input)
                {
                    case 1:
                        {
                            printLicensePlates = r_GarageManagement.CreateStringOfAllLicensesPlates();
                            displayMessage = "List of all current license plates of vehicles in the garage:";
                            break;
                        }

                    case 2:
                        {
                            printLicensePlates = r_GarageManagement.CreateStringOfLicenseByStatus(Vehicle.eVehicleStatus.InRepair);
                            displayMessage = string.Format("List of all Vehicles in '{0}' status: ", Vehicle.eVehicleStatus.InRepair);
                            break;
                        }

                    case 3:
                        {
                            printLicensePlates = r_GarageManagement.CreateStringOfLicenseByStatus(Vehicle.eVehicleStatus.Repaired);
                            displayMessage = string.Format("List of all Vehicles in '{0} status: ", Vehicle.eVehicleStatus.Repaired);

                            break;
                        }

                    case 4:
                        {
                            printLicensePlates = r_GarageManagement.CreateStringOfLicenseByStatus(Vehicle.eVehicleStatus.Paid);
                            displayMessage = string.Format("List of all Vehicles in '{0} status: ", Vehicle.eVehicleStatus.Paid);

                            break;
                        }

                    case 5:
                        {
                            exitMenu = true;
                            break;
                        } 
                }

                if(!exitMenu)
                {
                    if (printLicensePlates.Length == 0)
                    {
                        printLicensePlates.Append("-No vehicles with the requested status found-");
                        printLicensePlates.Append(Environment.NewLine);
                    }

                    Console.Clear();
                    Console.WriteLine(displayMessage);
                    Console.WriteLine(printLicensePlates);
                    input = DisplayCarsLicensePlatesMenu();
                }
            }
        }

        public void DisplayFullDetails() 
        {
            StringBuilder currentDetails = new StringBuilder();
            foreach(Vehicle currentVehicle in r_GarageManagement.VehicleList.Values)
            {
                currentDetails = currentVehicle.FullVehicleDetails();
                Console.Write(currentDetails);
                Console.WriteLine();
            }

            if (currentDetails.Length == 0)
            {
                currentDetails.Append("-No Vehicles found in the garage-");
                Console.WriteLine(currentDetails);
            }

            Console.WriteLine("Press any key to go back to the main menu");
            Console.ReadLine();
        }
    }
}
