using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricCar : Car
    {
        private readonly ElectricEngine r_Engine;

        public static Dictionary<string, object> GetSpecificParameterList()
        {
            Dictionary<string, object> parameterList = new Dictionary<string, object>();

            foreach (KeyValuePair<string, object> currentParameter in FindParametersOfCar())
            {
                parameterList.Add(currentParameter.Key, currentParameter.Value);
            }

            foreach (KeyValuePair<string, object> currentParameter in ElectricEngine.GetElectricEngineParameters())
            {
                parameterList.Add(currentParameter.Key, currentParameter.Value);
            }

            return parameterList;
        }

        public ElectricCar(params object[] i_ParameterList)
            : base((string)i_ParameterList[0], (string)i_ParameterList[1], (string)i_ParameterList[2], (string)i_ParameterList[3], (string)i_ParameterList[4], (float)i_ParameterList[5], (int)i_ParameterList[6], (int)i_ParameterList[7])
        {
            const float k_MaxBatteryTime = 3.2f;
            EnergyLevel = ((float)i_ParameterList[8] * 100) / k_MaxBatteryTime;
            r_Engine = new ElectricEngine((float)i_ParameterList[8], k_MaxBatteryTime);
        }

        public override void FillFuel(FuelEngine.eFuelTypes i_FuelType, float i_HowManyLiters)
        {
            string message = string.Format("This is an {0}! You cannot fuel it, only charge it.", "Electric Car");
            throw new ArgumentException(message);
        }

        public override void FillBattery(float i_HowMuchToCharge)
        {
            try
            {
                r_Engine.FillBattery(i_HowMuchToCharge); 
                EnergyLevel = (100 * r_Engine.CurrentBatteryCharge) / r_Engine.MaxBatteryTime;
            }
            catch (ValueOutOfRangeException ex)
            {
                throw ex;
            }
        }

        public override StringBuilder FullVehicleDetails()
        {
            StringBuilder electricBikeDetails = new StringBuilder();
            electricBikeDetails.Append(CarDetails());
            electricBikeDetails.Append(r_Engine.EngineDetails());
            return electricBikeDetails;
        }
    }
}
