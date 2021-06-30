using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class CreateNewVehicle
    {
        public enum eVehicle
        {
            ElectricCar = 1,
            FuelCar,
            ElectricBike,
            FuelBike,
            Truck
        }

        public Dictionary<string, object> CreateParameterList(eVehicle i_VehicleToCreate)
        {
            Dictionary<string, object> listOfParameters = new Dictionary<string, object>();
            switch(i_VehicleToCreate)
            {
                case eVehicle.ElectricCar:
                    {
                        listOfParameters = ElectricCar.GetSpecificParameterList();
                        break;
                    }

                case eVehicle.FuelCar:
                    {
                        listOfParameters = FuelCar.GetSpecificParameterList();
                        break;
                    }

                case eVehicle.ElectricBike:
                    {
                        listOfParameters = ElectricBike.GetSpecificParameterList();
                        break;
                    }

                case eVehicle.FuelBike:
                    {
                        listOfParameters = FuelBike.GetSpecificParameterList();
                        break;
                    }
              
                case eVehicle.Truck:
                    {
                       listOfParameters = Truck.GetSpecificParameterList();
                        break;
                    }
            }

            return listOfParameters;
        }

        public Dictionary<int, string> GetAllVehiclesNamesAndValues()
        {
            string[] arrayOfNames = Enum.GetNames(typeof(eVehicle));
            int[] keysArray = (int[])Enum.GetValues(typeof(eVehicle));
            Dictionary<int, string> namesAndValues = new Dictionary<int, string>();
            int lengthOfArray = arrayOfNames.Length;
            int currentLocationInArray = 0;

            foreach(int currentValue in keysArray)
            {
                namesAndValues.Add(currentValue, arrayOfNames[currentLocationInArray]);
                currentLocationInArray++;
            }

            return namesAndValues;
        }

        public Vehicle CreateVehicle(eVehicle i_VehicleToCreate, params object[] i_ParameterList)
        {
            Vehicle newVehicle = null;
            switch (i_VehicleToCreate)
            {
                case eVehicle.ElectricBike:
                    {
                        newVehicle = new ElectricBike(i_ParameterList);
                        break;
                    }

                case eVehicle.ElectricCar:
                    {
                        newVehicle = new ElectricCar(i_ParameterList);
                        break;
                    }

                case eVehicle.FuelBike:
                    {
                        newVehicle = new FuelBike(i_ParameterList);
                        break;
                    }

                case eVehicle.FuelCar:
                    {
                        newVehicle = new FuelCar(i_ParameterList);
                        break;
                    }

                case eVehicle.Truck:
                    {
                        newVehicle = new Truck(i_ParameterList);
                        break;
                    }
            }

            return newVehicle;
        }
    }
}
