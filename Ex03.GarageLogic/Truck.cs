using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
   public class Truck : Vehicle
    {
        private readonly FuelEngine r_Engine;
        private readonly bool r_HazardousMaterial;
        private float m_MaxWeight;

        public static Dictionary<string, object> FindParametersOfTruck()
        {
            Dictionary<string, object> parameterList = new Dictionary<string, object>();
            parameterList = FindParametersOfVehicle();
            int intToSend = 0;
            float floatToSend = 0;
            parameterList.Add("Please press 0 if the truck does not carry Hazardous Material, else press any other number", intToSend);
            parameterList.Add("Please enter the maximum weight the truck can carry", floatToSend);

            return parameterList;
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

        public float MaximumWeight
        {
            get
            {
                return m_MaxWeight;
            }

            set
            {
                if(value < 0) 
                { 
                    throw new ArgumentException("ERROR! maximum weight cannot be a negative number!");
                }

                m_MaxWeight = value;
            }
        }

        public static Dictionary<string, object> GetSpecificParameterList()
        {
            Dictionary<string, object> parameterList = new Dictionary<string, object>();

            foreach (KeyValuePair<string, object> currentParameter in FindParametersOfTruck())
            {
                parameterList.Add(currentParameter.Key, currentParameter.Value);
            }

            foreach (KeyValuePair<string, object> currentParameter in FuelEngine.GetParameterListOfFuelEngine())
            {
                parameterList.Add(currentParameter.Key, currentParameter.Value);
            }

            return parameterList;
        }

        public Truck(params object[] i_ParameterList)
            : base((string)i_ParameterList[0], (string)i_ParameterList[1], (string)i_ParameterList[2], (string)i_ParameterList[3])
        { 
            const int k_NumberOfTires = 16;
            const float k_MaxAirPressure = 26f; 
            const float k_MaxFuelIntake = 120f; 
            EnergyLevel = ((float)i_ParameterList[8] * 100) / k_MaxFuelIntake;
            r_HazardousMaterial = Convert.ToBoolean(i_ParameterList[6]);
            m_VehicleTires = new List<Tire>();
            MaximumWeight = (float)i_ParameterList[7];
            for (int i = 0; i < k_NumberOfTires; i++)
            {
                Tire currentTire = new Tire((string)i_ParameterList[4], (float)i_ParameterList[5], k_MaxAirPressure);
                m_VehicleTires.Add(currentTire);
            }

            r_Engine = new FuelEngine(FuelEngine.eFuelTypes.Soler, (float)i_ParameterList[8], k_MaxFuelIntake);
        }

        public override void FillFuel(FuelEngine.eFuelTypes i_FuelType, float i_HowManyLiters)
        {
            if(i_FuelType != r_Engine.FuelType)
            {
                string message = string.Format("Error! you are trying to use {0} fuel but your vehicle needs {1} fuel!", i_FuelType, r_Engine.FuelType);
                throw new ArgumentException(message);
            }

            r_Engine.FuelVehicle(i_HowManyLiters);
            EnergyLevel = (100 * r_Engine.CurrentFuel) / r_Engine.MaxFuelIntake;
        }

        public override void FillBattery(float i_HowMuchToCharge)
        {
            string message = string.Format("This is a {0}! You cannot charge it, only fuel it.", "Fuel Truck");
            throw new ArgumentException(message);
        }

        public StringBuilder TruckDetails()
        {
            StringBuilder truckDetails = new StringBuilder();
            string hazardousMaterial = string.Format("is carrying HazardousMaterial: {0} {1}", r_HazardousMaterial, Environment.NewLine);
            string maxWeight = string.Format("This truck can carry up to {0} ton {1}", m_MaxWeight, Environment.NewLine);
            truckDetails.Append(hazardousMaterial);
            truckDetails.Append(maxWeight);
            return truckDetails;
        }

        public override StringBuilder FullVehicleDetails()
        {
            StringBuilder truckDetails = new StringBuilder();
            truckDetails.Append("Truck's full details:");
            truckDetails.Append(Environment.NewLine);
            truckDetails.Append(GeneralDetails());
            truckDetails.Append(TruckDetails());
            truckDetails.Append(r_Engine.EngineDetails());
            truckDetails.Append("FULL DETAILS OF THE VEHICLE'S TIRES: ");
            truckDetails.Append(Environment.NewLine);
            foreach(Tire currentTire in m_VehicleTires)
            {
                truckDetails.Append(currentTire.TireDetail());
            }

            return truckDetails;
        }
    }
}
