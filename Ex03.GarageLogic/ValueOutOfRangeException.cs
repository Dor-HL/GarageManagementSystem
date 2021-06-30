using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception 
    {
        private float m_MaxValue;
        private float m_MinValue;

        public float MaxValue
        {
            get
            {
                return m_MaxValue;
            }

            set
            {
                m_MaxValue = value;
            }
        }

        public float MinValue
        {
            get
            {
                return m_MinValue;
            }

            set
            {
                m_MinValue = value;
            }
        }

        public ValueOutOfRangeException(Exception i_InnerException, float i_MaxValue, float i_MinValue, string i_ProblemLocation) : base(string.Format("ERROR! Value of {0} is out of range, please enter a value between {1} and {2}", i_ProblemLocation, i_MinValue, i_MaxValue), i_InnerException)
        {
            m_MaxValue = i_MaxValue;
            m_MinValue = i_MinValue;
        }
    }
}
