using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class FuelBike : Bike
    {
        private readonly FuelEngine r_Engine;

        public static Dictionary<string, object> GetSpecificParameterList()
        {
            Dictionary<string, object> parameterList = new Dictionary<string, object>();

            foreach (KeyValuePair<string, object> currentParameter in FindParametersOfBike())
            {
                parameterList.Add(currentParameter.Key, currentParameter.Value);
            }

            foreach (KeyValuePair<string, object> currentParameter in GarageLogic.FuelEngine.GetParameterListOfFuelEngine())
            {
                parameterList.Add(currentParameter.Key, currentParameter.Value);
            }

            return parameterList;
        }

        public FuelBike(params object[] i_ParameterList)
            : base((string)i_ParameterList[0], (string)i_ParameterList[1], (string)i_ParameterList[2], (string)i_ParameterList[3], (string)i_ParameterList[4], (float)i_ParameterList[5], (eLicenseType)i_ParameterList[6], (int)i_ParameterList[7])
        {
            const float k_MaximumFuelIntake = 6f;
            EnergyLevel = ((float)i_ParameterList[8] * 100) / k_MaximumFuelIntake;
            r_Engine = new FuelEngine(FuelEngine.eFuelTypes.Octan98, (float)i_ParameterList[8], k_MaximumFuelIntake);
        }

        public override void FillFuel(FuelEngine.eFuelTypes i_FuelType, float i_HowManyLiters)
        {
            if(i_FuelType != r_Engine.FuelType)
            {
                string message = string.Format("ERROR! you are trying to use {0} fuel but your vehicle needs {1} fuel!", i_FuelType, r_Engine.FuelType);
                throw new ArgumentException(message);
            }

            r_Engine.FuelVehicle(i_HowManyLiters);
            EnergyLevel = (100 * r_Engine.CurrentFuel) / r_Engine.MaxFuelIntake;
        }

        public override void FillBattery(float i_HowMuchToCharge)
        {
            string message = string.Format("This is a {0}! You cannot charge it, only fuel it.", "Fuel Bike");
           throw new ArgumentException(message);
        }

        public override StringBuilder FullVehicleDetails()
        {
            StringBuilder electricBikeDetails = new StringBuilder();
            electricBikeDetails.Append(BikeDetails());
            electricBikeDetails.Append(r_Engine.EngineDetails());
            return electricBikeDetails;
        }
    }
}
