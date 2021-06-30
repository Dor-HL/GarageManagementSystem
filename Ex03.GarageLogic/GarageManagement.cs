using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class GarageManagement
    {
        private Dictionary<string, Vehicle> m_VehicleList;

        public GarageManagement()
        {
            m_VehicleList = new Dictionary<string, Vehicle>();
        }

        public Dictionary<string, Vehicle> VehicleList
        {
            get
            {
                return m_VehicleList;
            }

            set
            {
                m_VehicleList = value;
            }
        }

        public void FillAirToMaxInTire(string i_LicensePlateNumber)
        {
            Vehicle vehicleToFixAirPressure = FindVehicle(i_LicensePlateNumber);
             foreach(Tire currentTire in vehicleToFixAirPressure.Tires) 
             { 
                 if (currentTire.CurrentAirPressure < currentTire.MaxAirPressure) 
                 { 
                     currentTire.CurrentAirPressure = currentTire.MaxAirPressure;
                 }
             }
        }

        public void AddNewVehicle(Vehicle i_NewVehicle)
        {
            if(!IsVehicleAlreadyIn(i_NewVehicle.LicensePlateNumber))
            {
                m_VehicleList.Add(i_NewVehicle.LicensePlateNumber, i_NewVehicle);
            }

            i_NewVehicle.CurrentStatus = Vehicle.eVehicleStatus.InRepair;
        }

        public bool IsVehicleAlreadyIn(string i_LicensePlateNumber)
        {
            bool isVehicleAlreadyIn = true;
            Vehicle findVehicle = FindVehicle(i_LicensePlateNumber);
            if(findVehicle != null)
            {
                findVehicle.CurrentStatus = Vehicle.eVehicleStatus.InRepair;
                throw new ArgumentException("Vehicle is already in! status changed to 'inRepair'");
            } 
            
            return !isVehicleAlreadyIn;
        }

        public void ChangeVehicleStatus(string i_LicensePlateNumber, Vehicle.eVehicleStatus i_NewStatus)
        {
            Vehicle toChange = FindVehicle(i_LicensePlateNumber);

            if(toChange.CurrentStatus == i_NewStatus)
            {
                throw new ArgumentException("This is already the current status!");
            }

            toChange.CurrentStatus = i_NewStatus;
        }

        public Vehicle FindVehicle(string i_LicensePlateNumber)
        {
            if (!m_VehicleList.TryGetValue(i_LicensePlateNumber, out Vehicle foundVehicle))
            {
                foundVehicle = null;
            }

            return foundVehicle;
        }

        public void FillFuel(string i_LicensePlate, float i_LitersOfFuel, FuelEngine.eFuelTypes i_FuelType)
        {
            Vehicle vehicleToFuel = FindVehicle(i_LicensePlate);
            vehicleToFuel.FillFuel(i_FuelType, i_LitersOfFuel); 
        }

        public void FillBattery(string i_LicensePlate, float i_HoursToCharge)
        {
            Vehicle vehicleToCharge = FindVehicle(i_LicensePlate); 
            vehicleToCharge.FillBattery(i_HoursToCharge); 
        }

        public StringBuilder CreateStringOfAllLicensesPlates()
        {
            StringBuilder listOfLicensePlates = new StringBuilder();
            foreach (KeyValuePair<string, Vehicle> currentVehicle in m_VehicleList)
            { 
                listOfLicensePlates.Append(currentVehicle.Key); 
                listOfLicensePlates.Append(Environment.NewLine);
            }

            return listOfLicensePlates;
        }

        public StringBuilder CreateStringOfLicenseByStatus(Vehicle.eVehicleStatus i_Status)
        {
            StringBuilder listOfLicensePlates = new StringBuilder();
            foreach (KeyValuePair<string, Vehicle> currentVehicle in m_VehicleList)
            {
                if (currentVehicle.Value.CurrentStatus == i_Status)
                {
                    listOfLicensePlates.Append(currentVehicle.Key); 
                    listOfLicensePlates.Append(Environment.NewLine);
                }
            }

            return listOfLicensePlates;
        }
    }
}
