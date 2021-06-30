using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class FuelEngine
    {
        public enum eFuelTypes
        {
            Soler = 1,
            Octan95,
            Octan96,
            Octan98
        }

        private eFuelTypes m_FuelType;
        private float m_CurrentFuelInLiters;
        private float m_MaximumFuelIntake;

        public static Dictionary<string, object> GetParameterListOfFuelEngine()
        {
            Dictionary<string, object> parameterList = new Dictionary<string, object>();
            float floatToGet = 0;
            parameterList.Add("Please enter the current amount of fuel in your vehicle in liters", floatToGet);

            return parameterList;
        }

        public eFuelTypes FuelType
        {
            get
            {
                return m_FuelType;
            }

            set
            {
                m_FuelType = value;
            }
        }

        public float CurrentFuel
        {
            get
            {
                return m_CurrentFuelInLiters;
            }

            set
            { 
                float maxValue = m_MaximumFuelIntake; 
                float minValue = 1;
                if (m_CurrentFuelInLiters == m_MaximumFuelIntake)
                {
                    minValue = 0;
                } 

                if(value > m_MaximumFuelIntake || value < 0) 
                { 
                    throw new ValueOutOfRangeException(new Exception(), maxValue, minValue, "fuel");
                }
                
                m_CurrentFuelInLiters = value;
            }
        }

        public float MaxFuelIntake
        {
            get
            {
                return m_MaximumFuelIntake;
            }

            set
            {
                m_MaximumFuelIntake = value;
            }
        }

        public FuelEngine(eFuelTypes i_FuelType, float i_CurrentFuelInLiters, float i_MaximumFuelIntake)
        {
            m_MaximumFuelIntake = i_MaximumFuelIntake;
            m_FuelType = i_FuelType;
            CurrentFuel = i_CurrentFuelInLiters;
        }

        public void FuelVehicle(float i_HowMuchFuelToAdd)
        {
            float maxValue = m_MaximumFuelIntake - m_CurrentFuelInLiters;
            float minValue = 1;
            if (m_CurrentFuelInLiters == m_MaximumFuelIntake)
            {
                minValue = 0;
            }

            if (i_HowMuchFuelToAdd > maxValue || i_HowMuchFuelToAdd < minValue)
            {
                throw new ValueOutOfRangeException(new Exception(), maxValue, minValue, "fuel");
            } 

            CurrentFuel += i_HowMuchFuelToAdd;
        }

        public StringBuilder EngineDetails()
        {
            StringBuilder engineDetail = new StringBuilder();
            engineDetail.Append("Engine type: Fuel");
            engineDetail.Append(Environment.NewLine);
            string fuelType = string.Format("The fuel type for this vehicle is: {0} {1}", m_FuelType, Environment.NewLine);
            string currentFuel = string.Format("The vehicle has currently {0} liters of fuel {1}", m_CurrentFuelInLiters, Environment.NewLine);
            string maxFuel = string.Format("The vehicle can contain a maximum of {0} liters of fuel {1}", m_MaximumFuelIntake, Environment.NewLine);
            engineDetail.Append(fuelType);
            engineDetail.Append(currentFuel);
            engineDetail.Append(maxFuel);
            return engineDetail;
        }
    }
}
