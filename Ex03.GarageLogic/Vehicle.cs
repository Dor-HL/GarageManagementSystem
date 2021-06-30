using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        public enum eVehicleStatus
        {
            InRepair = 1,
            Repaired,
            Paid
        }

        protected string m_Model;
        protected string m_LicensePlateNumber;
        protected float m_EnergyLevelLeft;
        protected string m_OwnerName;
        protected string m_OwnerPhoneNumber;
        protected eVehicleStatus m_VehicleStatus;
        protected List<Tire> m_VehicleTires;

        public List<Tire> Tires
        {
            get
            {
                return m_VehicleTires;
            }

            set
            {
                m_VehicleTires = value;
            }
        }
        
        public eVehicleStatus CurrentStatus
        {
            get
            {
                return m_VehicleStatus;
            }

            set
            {
                m_VehicleStatus = value;
            }
        }

        public string OwnerPhoneNumber
        {
            get
            {
                return m_OwnerPhoneNumber;
            }

            set
            {
                try
                {
                    int.Parse(value);
                    m_OwnerPhoneNumber = value;
                }
                catch (FormatException)
                {
                    throw new FormatException("ERROR! Phone number must only contain numbers");
                }
            }
        }

        public float EnergyLevel
        {
            get
            {
                return m_EnergyLevelLeft;
            }

            set
            {
                m_EnergyLevelLeft = value;
            }
        }

        public string LicensePlateNumber
        {
            get
            {
                return m_LicensePlateNumber;
            }

            set
            {
                m_LicensePlateNumber = value;
            }
        }

        protected Vehicle(string i_Model, string i_LicensePlateNumber, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            m_Model = i_Model;
            m_LicensePlateNumber = i_LicensePlateNumber;
            m_VehicleStatus = eVehicleStatus.InRepair;
            m_OwnerName = i_OwnerName;
            OwnerPhoneNumber = i_OwnerPhoneNumber;
        }

        public static Dictionary<string, object> FindParametersOfVehicle()
        {
            Dictionary<string, object> parameterList = new Dictionary<string, object>();
            string stringToGet = string.Empty;

            parameterList.Add("Please enter the vehicle's model", stringToGet);
            parameterList.Add("Please enter license plate number", stringToGet);
            parameterList.Add("Please enter the owner's name", stringToGet);
            parameterList.Add("Please enter the owner phone number", stringToGet);
            foreach (KeyValuePair<string, object> currentParameter in Tire.GetTireParameterList())
            {
                parameterList.Add(currentParameter.Key, currentParameter.Value);
            }

            return parameterList;
        }

        public abstract List<Tire> TireList { get; set; }

        public abstract StringBuilder FullVehicleDetails();

        public abstract void FillFuel(FuelEngine.eFuelTypes i_FuelType, float i_HowManyLiters);

        public abstract void FillBattery(float i_HowMuchToCharge);

        public StringBuilder GeneralDetails()
        {
            StringBuilder generalDetails = new StringBuilder();
            string model = string.Format("The vehicle model is: {0} {1}", m_Model, Environment.NewLine);
            string licensePlate = string.Format("The vehicle's license plate is: {0} {1}", m_LicensePlateNumber, Environment.NewLine);
            string energyLevel = string.Format("The energy level left in the vehicle is: {0}% {1}", m_EnergyLevelLeft, Environment.NewLine);
            string ownerDetails = string.Format("The Owner name is: {0} {1} The owner phone number is: {2} {3}", m_OwnerName, Environment.NewLine, m_OwnerPhoneNumber, Environment.NewLine);
            string status = string.Format("The vehicle's status in the garage is: {0} {1}", m_VehicleStatus, Environment.NewLine);
            generalDetails.Append(model);
            generalDetails.Append(licensePlate);
            generalDetails.Append(energyLevel);
            generalDetails.Append(ownerDetails);
            generalDetails.Append(status);

            return generalDetails;
        }
    }
}
