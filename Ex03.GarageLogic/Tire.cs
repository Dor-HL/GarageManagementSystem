using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Tire
    {
        private readonly string r_ManufacturerName;
        private float m_CurrentAirPressure;
        private float m_MaxAirPressure;

        public static Dictionary<string, object> GetTireParameterList()
        {
            Dictionary<string, object> tireParameterList = new Dictionary<string, object>();
            string stringToSend = string.Empty;
            float floatToSend = 0;

            tireParameterList.Add("Please enter the Tire's manufacturer Name", stringToSend);
            tireParameterList.Add("Please enter the current air pressure of the tires", floatToSend);
            return tireParameterList;
        }

        public float MaxAirPressure
        {
            get
            {
                return m_MaxAirPressure;
            }

            set
            {
                m_MaxAirPressure = value;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }

            set
            {
                if (value < 0 || value > m_MaxAirPressure)
                {
                    throw new ValueOutOfRangeException(new Exception(), m_MaxAirPressure, 0, "air pressure");
                }

                m_CurrentAirPressure = value;
            }
        }

        public Tire(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            r_ManufacturerName = i_ManufacturerName;
            m_MaxAirPressure = i_MaxAirPressure;
            CurrentAirPressure = i_CurrentAirPressure;
        }

        public StringBuilder TireDetail()
        {
            StringBuilder tireDetail = new StringBuilder();
            string details = string.Format("Manufacturer name: {0}   Current air pressure: {1}    Max air pressure: {2} {3}", r_ManufacturerName, m_CurrentAirPressure, m_MaxAirPressure, Environment.NewLine);
            tireDetail.Append(details);
            return tireDetail;
        }
    }
}
