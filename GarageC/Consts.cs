using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageC
{
    internal static class Consts
    {
        internal const string CODE_VERSION_ID = "0.60N"; // N stands for Nordic

        // parking rate constants
        private const double AirplaneDay = 80.0;
        private const double BoatDay = 60.0;
        private const double BusHour = 24.0;
        private const double CarHour = 8.0;
        private const double MotorcycleHour = 4.0;

        internal const string CURRENCY_SIGN = "kr"; // mayby SEK is better

        // Get one of these via Consts.ParkingPrice()
        // parking rate arrays[6] as:
        // id0: Minimi unit (either HourUnit (=1) or DayUnit (=24))
        // id1: Parking rate per hour
        // id2 to id5: 1- and 7- and 30- and 90-days percentages (for example: 7-days rate is calculated as perHour * id3)
        private const double HourUnit = 1.0;
        private const double DayUnit = 24.0;
        internal static double[] AIRPLANE_Min_Hour_Day_7_30_90 = { DayUnit, AirplaneDay / 24.0, 1, 0.7, 0.65, 0.60 };
        internal static double[] BOAT_Min_Hour_Day_7_30_90 = { DayUnit, BoatDay / 24.0, 1, 0.7, 0.65, 0.60 };
        internal static double[] BUS_Min_Hour_Day_7_30_90 = { HourUnit, BusHour, 0.6, 0.5, 0.45, 0.42 };
        internal static double[] CAR_Min_Hour_Day_7_30_90 = { HourUnit, CarHour, 0.6, 0.5, 0.45, 0.42 };
        internal static double[] MOTORCYCLE_Min_Hour_Day_7_30_90 = { HourUnit, MotorcycleHour, 0.6, 0.5, 0.45, 0.42 };

        internal static double[] ParkingPrice(Vehicle v)
        {
            switch (v.ClassTitle)
            {
                case "CAR":
                    return CAR_Min_Hour_Day_7_30_90;
                case "BUS":
                    return CAR_Min_Hour_Day_7_30_90;
                case "BOAT":
                    return CAR_Min_Hour_Day_7_30_90;
                case "MOTORCYCLE":
                    return CAR_Min_Hour_Day_7_30_90;
                case "AIRPLANE":
                    return CAR_Min_Hour_Day_7_30_90;
                default:
                    return Array.Empty<double>();
            }
        }

        // Vehicle class constants
        internal const ConsoleColor TitleColor = ConsoleColor.White;
        internal const ConsoleColor BodytextColor = ConsoleColor.Cyan; // good alternatives: Green or Yellow;

        // Bus class constants
        internal const int MAX_BUS_SEATS = 100;

        // Garage and Vehicle class constants
        // Default or validation values based on an imaginary building
        internal const double MAX_GARAGE_CEILING_HEIGHT_meters = 7d;
        internal const double MAX_GARAGE_LOT_LENGHT_meters = 18d;
        internal const double MAX_GARAGE_VEHICLE_WEIGHT_kg = 25000;
        internal const int MAX_AMOUNT_PARKING_LOTS = 40;
    }
}
