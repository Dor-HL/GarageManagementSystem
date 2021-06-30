using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
   public abstract class Bike : Vehicle
    {
        public enum eLicenseType
        { 
            A = 1,
            B1,
            AA, 
            BB
        }

        private eLicenseType m_LicenseType;
        private int m_EngineCapacity;

        public eLicenseType License
        {
            get
            {
                return m_LicenseType;
            }

            set
            {
                if (value < eLicenseType.A || value > eLicenseType.BB) 
                { 
                    throw new ValueOutOfRangeException(new Exception(), (int)eLicenseType.BB, (int)eLicenseType.A, "license type");
                } 
                
                m_LicenseType = value;
            }
        }

        public override List<Tire> TireList
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

        public int EngineCapacity
        {
            get
            {
                return m_EngineCapacity;
            }

            set
            {
                if(value <= 0)
                {
                    throw new ArgumentException("ERROR! Engine capacity cannot be zero or negative!");
                } 

                m_EngineCapacity = value;
            }
        }
        
        protected Bike(string i_Model, string i_LicensePlateNumber, string i_OwnerName, string i_OwnerPhoneNumber, string i_TireManufacturer, float i_CurrentAirPressure, eLicenseType i_LicenseType, int i_EngineCapacity)
            : base(i_Model, i_LicensePlateNumber, i_OwnerName, i_OwnerPhoneNumber)
        { 
            const int k_NumberOfTires = 2; 
            const float k_MaxAirPressure = 30;
            EngineCapacity = i_EngineCapacity;
            License = i_LicenseType;
            m_VehicleTires = new List<Tire>();
            for(int i = 0; i < k_NumberOfTires; i++)
            {
                Tire currentTire = new Tire(i_TireManufacturer, i_CurrentAirPressure, k_MaxAirPressure);
                m_VehicleTires.Add(currentTire);
            }
        }

        public static Dictionary<string, object> FindParametersOfBike()
        {
            Dictionary<string, object> parameterList = new Dictionary<string, object>();
            parameterList = FindParametersOfVehicle();
            int intToGet = 0;
            string getLicense = string.Format("Please enter your bike's license type: {0} press 1 to enter A {0} press 2 to enter B1 {0} press 3 to enter AA {0} press 4 to enter BB {0}", Environment.NewLine);
            parameterList.Add(getLicense, intToGet);
            parameterList.Add("Please enter bike's engine capacity", intToGet);
       
            return parameterList;
        }

        public abstract override void FillFuel(FuelEngine.eFuelTypes i_FuelType, float i_HowManyLiters);

        public abstract override void FillBattery(float i_HowMuchToCharge);

        public StringBuilder BikeDetails()
        {
            StringBuilder bikeDetails = new StringBuilder();
            bikeDetails.Append("Bike's full details:");
            bikeDetails.Append(Environment.NewLine);
            bikeDetails = GeneralDetails();
            string license = string.Format("The license type is:  {0} {1}", m_LicenseType, Environment.NewLine);
            string capacity = string.Format("The bike's engine capacity is: {0} {1}", m_EngineCapacity, Environment.NewLine);
            bikeDetails.Append(license);
            bikeDetails.Append(capacity);
            bikeDetails.Append("FULL DETAILS OF THE VEHICLE'S TIRES: ");
            bikeDetails.Append(Environment.NewLine);
            foreach (Tire currentTire in m_VehicleTires)
            {
                bikeDetails.Append(currentTire.TireDetail());
            }

            return bikeDetails;
        }
    }
}
