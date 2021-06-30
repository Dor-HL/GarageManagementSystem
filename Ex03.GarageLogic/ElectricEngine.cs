using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricEngine
    {
        private float m_BatteryTimeLeftInHours;
        private float m_MaximalBatteryTimeInHours;

        public static Dictionary<string, object> GetElectricEngineParameters()
        {
            Dictionary<string, object> parameterList = new Dictionary<string, object>();
            float getFloat = 0;
            parameterList.Add("Please enter the current battery time left in hours", getFloat);

            return parameterList;
        }

        public float MaxBatteryTime
        {
            get
            {
                return m_MaximalBatteryTimeInHours;
            }

            set
            {
                m_MaximalBatteryTimeInHours = value;
            }
        }

        public float CurrentBatteryCharge
        {
            get
            {
                return m_BatteryTimeLeftInHours;
            }

            set
            {
                float maxValue = m_MaximalBatteryTimeInHours;
                float minValue = 1;
                if (m_BatteryTimeLeftInHours == m_MaximalBatteryTimeInHours)
                {
                    minValue = 0;
                }

                if ((value > m_MaximalBatteryTimeInHours) || value < 0)
                {
                    throw new ValueOutOfRangeException(new Exception(), maxValue, minValue, "charge time");
                }

                m_BatteryTimeLeftInHours = value;
            }
        }

        public ElectricEngine(float i_BatteryTimeLeftInHours, float i_MaximalBatteryTimeInHours)
        {
            m_MaximalBatteryTimeInHours = i_MaximalBatteryTimeInHours;
            CurrentBatteryCharge = i_BatteryTimeLeftInHours;
        }

        public void FillBattery(float i_HowManyMinutesToCharge)
        {
            float minutesToHours = i_HowManyMinutesToCharge / 60;
            float currentTimeLeftInMinutes = m_BatteryTimeLeftInHours * 60;
            float maxTimeInMinutes = m_MaximalBatteryTimeInHours * 60;
            float maxValue = maxTimeInMinutes - currentTimeLeftInMinutes;
            float minValue = 1;
            if(m_BatteryTimeLeftInHours == m_MaximalBatteryTimeInHours)
            {
                minValue = 0;
            }

            if (i_HowManyMinutesToCharge > maxValue || i_HowManyMinutesToCharge < minValue)
            { 
                throw new ValueOutOfRangeException(new Exception(), maxValue, minValue, "charge time");
            } 
            
            CurrentBatteryCharge += minutesToHours;
        }

        public StringBuilder EngineDetails()
        {
            StringBuilder engineDetail = new StringBuilder();
            engineDetail.Append("Engine type: Electric");
            engineDetail.Append(Environment.NewLine);
            string currentCharge = string.Format("The vehicle's battery will last {0} hours {1}", m_BatteryTimeLeftInHours, Environment.NewLine);
            string maxCharge = string.Format("The vehicle can be charged a maximum of {0} hours {1}", m_MaximalBatteryTimeInHours, Environment.NewLine);
            engineDetail.Append(currentCharge);
            engineDetail.Append(maxCharge);
            return engineDetail;
        }
    }
}
