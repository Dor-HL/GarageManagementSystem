using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Car : Vehicle
    {
        public enum eColor
        {
            Red = 1,
            Silver,
            White,
            Black
        }

        public enum eNumberOfDoors
        { 
            Two = 1, 
            Three, 
            Four, 
            Five
        }

        private eColor m_Color;
        private eNumberOfDoors m_NumberOfDoors; 

        public static Dictionary<string, object> FindParametersOfCar()
        {
            Dictionary<string, object> parameterList = new Dictionary<string, object>();
            parameterList = FindParametersOfVehicle();

            int intToSend = 0;

            string getColor = string.Format(
                "Please enter your car's color: {0} press 1 to enter RED {0} press 2 to enter SILVER {0} press 3 to enter WHITE {0} press 4 to enter BLACK {0}",
                Environment.NewLine);
            parameterList.Add(getColor, intToSend);
            string getDoors = string.Format(
                "Please enter the number of doors in your car: {0} press 1 to enter TWO {0} press 2 to enter THREE {0} press 3 to enter FOUR {0} press 4 to enter FIVE {0}",
                Environment.NewLine);
            parameterList.Add(getDoors, intToSend);

            return parameterList;
        }

        public eNumberOfDoors Doors
        {
            get
            {
                return m_NumberOfDoors;
            }

            set
            { 
                if (value < eNumberOfDoors.Two || value > eNumberOfDoors.Five)
                { 
                    throw new ValueOutOfRangeException(new Exception(), (int)eNumberOfDoors.Five, (int)eNumberOfDoors.Two, "door number");
                } 

                m_NumberOfDoors = value;
            }
        }

        public eColor Color
        {
            get
            {
                return m_Color;
            }

            set
            {
                if (value < eColor.Red || value > eColor.Black)
                {
                    throw new ValueOutOfRangeException(new Exception(), (int)eColor.Black, (int)eColor.Red, "car color");
                }

                m_Color = value;
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

        public abstract override void FillFuel(FuelEngine.eFuelTypes i_FuelType, float i_HowManyLiters);

        public abstract override void FillBattery(float i_HowMuchToCharge);

        protected Car(string i_Model, string i_LicensePlateNumber, string i_OwnerName, string i_OwnerPhoneNumber, string i_TireManufacturer, float i_CurrentAirPressure, int i_Color, int i_NumberOfDoors)
            : base(i_Model, i_LicensePlateNumber, i_OwnerName, i_OwnerPhoneNumber)
        { 
            const int k_NumberOfTires = 4;
            const int k_MaxAirPressure = 32; 

            Color = (eColor)i_Color;
           Doors = (eNumberOfDoors)i_NumberOfDoors;
            m_VehicleTires = new List<Tire>();
            for(int i = 0; i < k_NumberOfTires; i++)
            {
                Tire currentTire = new Tire(i_TireManufacturer, i_CurrentAirPressure, k_MaxAirPressure);
                m_VehicleTires.Add(currentTire);
            }
        }

        public StringBuilder CarDetails()
        {
            StringBuilder carDetails = new StringBuilder();
            carDetails.Append("Car's full details:");
            carDetails.Append(Environment.NewLine);
            carDetails = GeneralDetails();
            string color = string.Format("The car's color is {0} {1}", m_Color, Environment.NewLine);
            string doors = string.Format("The car has {0} doors {1}", m_NumberOfDoors, Environment.NewLine);
            carDetails.Append(color);
            carDetails.Append(doors);
            carDetails.Append("FULL DETAILS OF THE VEHICLE'S TIRES: ");
            carDetails.Append(Environment.NewLine);
            foreach(Tire currentTire in m_VehicleTires)
            {
                carDetails.Append(currentTire.TireDetail());
            }

            return carDetails;
        }
    }
}
