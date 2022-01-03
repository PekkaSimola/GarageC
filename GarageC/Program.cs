using System;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;

namespace GarageC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tests.SetConsoleWindow();
            // Tests.TimeSpanExampleFromMS(); // Create and display a TimeSpan value of 1 tick.
            // Tests.ReadKeyExample();
            Tests.Vehicles_Garage_Coloring_Tests();
            Tests.OtherTests();

            //Console.ReadKey(true).KeyChar; 

        }
    }

    /// <summary>
    /// Sundry static test methods
    /// </summary>
    internal static class Tests
    {

        internal static void TimeSpanExampleFromMS()
        {
            Console.ForegroundColor = ConsoleColor.White;

            // Create and display a TimeSpan value of 1 tick.
            Console.Write("\n{0,-45}", "TimeSpan( 1 )");
            ShowTimeSpanProperties(new TimeSpan(1));

            // Create a TimeSpan value with a large number of ticks.
            Console.Write("\n{0,-45}", "TimeSpan( 111222333444555 )");
            ShowTimeSpanProperties(new TimeSpan(111222333444555));

            // This TimeSpan has all fields specified.
            Console.Write("\n{0,-45}", "TimeSpan( 10, 20, 30, 40, 50 )");
            ShowTimeSpanProperties(new TimeSpan(10, 20, 30, 40, 50));

            // This TimeSpan has all fields overflowing.
            Console.Write("\n{0,-45}",
                          "TimeSpan( 1111, 2222, 3333, 4444, 5555 )");
            ShowTimeSpanProperties(
               new TimeSpan(1111, 2222, 3333, 4444, 5555));

            // This TimeSpan is based on a number of days.
            Console.Write("\n{0,-45}", "FromDays( 20.84745602 )");
            ShowTimeSpanProperties(TimeSpan.FromDays(20.84745602));
        }

        /// <summary>
        /// Help-method to TimeSpanExampleFromMS
        /// </summary>
        /// <param name="interval"></param>
        internal static void ShowTimeSpanProperties(TimeSpan interval)
        {
            Console.WriteLine("{0,21}", interval);
            Console.WriteLine("{0,-12}{1,8}       {2,-18}{3,21:N3}", "Days",
                              interval.Days, "TotalDays", interval.TotalDays);
            Console.WriteLine("{0,-12}{1,8}       {2,-18}{3,21:N3}", "Hours",
                              interval.Hours, "TotalHours", interval.TotalHours);
            Console.WriteLine("{0,-12}{1,8}       {2,-18}{3,21:N3}", "Minutes",
                              interval.Minutes, "TotalMinutes", interval.TotalMinutes);
            Console.WriteLine("{0,-12}{1,8}       {2,-18}{3,21:N3}", "Seconds",
                              interval.Seconds, "TotalSeconds", interval.TotalSeconds);
            Console.WriteLine("{0,-12}{1,8}       {2,-18}{3,21:N3}", "Milliseconds",
                              interval.Milliseconds, "TotalMilliseconds",
                              interval.TotalMilliseconds);
            Console.WriteLine("{0,-12}{1,8}       {2,-18}{3,21:N0}", null, null,
                "Ticks", interval.Ticks);
        }

        internal static void OtherTests()
        {
            FuelType fuelType = new FuelType();
            fuelType = FuelType.Bensin;
            Console.WriteLine("Enum.GetValues(typeof(FuelType)): " + Enum.GetValues(typeof(FuelType))); // output: ... GarageC.FuelType[]
            Console.WriteLine("Enum.GetName(fuelType): " + Enum.GetName(fuelType)); // output itemName: ... Bensin

            foreach (var item in Enum.GetValues(typeof(FuelType)))
            {
                // these lines get the very same output: ItemName
                Console.WriteLine(item.ToString());              // found out on my own
                Console.WriteLine((FuelType)item);               // example from MS-pages
                Console.WriteLine(Enum.GetName((FuelType)item)); // found out on my own, but this one can be used directly on a FuelType variable (see above this loop)
            }
        }

        internal static void Vehicles_Garage_Coloring_Tests()
        {
            Garage g = new("Pekka's Garage", 10);
            g.City = "Solna";
            g.StreetAddress = "Åbergssonsväg 1";
            g.Zipcode = "s170 77";
            g.Country = "Sweden";
            Console.WriteLine(g.ToText());

            Airplane v1 = new("Air999", 1);
            v1.StartTime = v1.StartTime.AddMinutes(-450);
            v1.Brand = "Cessna";
            v1.Model = "182 Skylane";
            v1.Color = "White-Blue";
            v1.ForeignRegistered = true;
            v1.AirplaneType = AirplaneType.Propeller;
            v1.HeigthMeters = 2.8;
            v1.LengthMeters = 8.84;
            v1.Weight_kg = 1400;
            v1.FuelType = FuelType.Bensin;
            v1.NumberOfEngines = 1;
            v1.Note = "All properties of this airplane are used for testing. First flight 1956 in USA. 23000+ produced by the Cessna Aircraft Company. Empty weight is 894 kg. " +
                      "The single machine (Lycoming IO-540) creates 230 hk or 172 kW. Max. speed: 278 km/h; Max range 1700 km; Max. flight altitude: 5500 meters; " +
                      "Climbing ability: 4.7 m/s; Capacity: 3 passengers.";

            // Default Optional Colors are White (titles) and Cyan (body text)
            // Change via Consts...
            ConsoleUI.DisplayVehicle(v1);

            Boat v2 = new("BOA555", 2);
            v2.StartTime = v2.StartTime.AddMinutes(-123);
            v2.Brand = "Master Line";
            v2.BoatType = BoatType.Sailboat;
            ConsoleUI.DisplayVehicle(v2, ConsoleColor.Yellow, ConsoleColor.Green);

            Bus v3 = new("BUS789", 3);
            v3.StartTime = v3.StartTime.AddMinutes(-112);
            v3.Brand = "penta";
            v3.IsDoubleDecker = true;
            ConsoleUI.DisplayVehicle(v3, ConsoleColor.Green, ConsoleColor.White);

            Motorcycle v4 = new("mCT182", 4);
            v4.StartTime = v4.StartTime.AddMinutes(-5450);
            v4.Brand = "kawasaki";
            v4.SetSpecialRegNo("Superman");
            v4.Color = "brun";
            v4.CylinderVolume = 250;
            Console.WriteLine(v4.ToText());
            ConsoleUI.DisplayVehicle(v4, ConsoleColor.White, ConsoleColor.Green);

            Car v5 = new("TAW242", 5);
            v5.StartTime = v5.StartTime.AddMinutes(-55450);
            v5.Brand = "honda";
            v5.CarType = CarType.Van;
            ConsoleUI.DisplayVehicle(v5, ConsoleColor.White, ConsoleColor.Yellow);

            ConsoleUI.PressKey();
        }

        internal static void ReadKeyExample()
        {
            ConsoleKeyInfo cki;
            // Prevent example from ending if CTL+C is pressed.
            Console.TreatControlCAsInput = true;

            Console.WriteLine("Press any combination of CTL, ALT, and SHIFT, and a console key.");
            Console.WriteLine("Press the Escape (Esc) key to quit: \n");
            do
            {
                cki = Console.ReadKey();
                Console.Write(" --- You pressed ");
                if ((cki.Modifiers & ConsoleModifiers.Alt) != 0) Console.Write("ALT+");
                if ((cki.Modifiers & ConsoleModifiers.Shift) != 0) Console.Write("SHIFT+");
                if ((cki.Modifiers & ConsoleModifiers.Control) != 0) Console.Write("CTL+");
                Console.WriteLine(cki.Key.ToString());
            } while (cki.Key != ConsoleKey.Escape);
        }

        internal static void SetConsoleWindow(bool writeStatusToConsoleWindow = false)
        {
            if (writeStatusToConsoleWindow)
            {
                Console.WriteLine("Console.LargestWindowHeight: " + Console.LargestWindowHeight);
                Console.WriteLine("Console.LargestWindowWidth: " + Console.LargestWindowWidth);
                Console.WriteLine("Console.WindowHeight: " + Console.WindowHeight);
                Console.WriteLine("Console.WindowWidth: " + Console.WindowWidth);
                Console.WriteLine("Console.WindowLeft: " + Console.WindowLeft);
                Console.WriteLine("Console.WindowTop: " + Console.WindowTop);
            }
#pragma warning disable CA1416 // Validate platform compatibility
            Console.SetBufferSize(4 * Console.LargestWindowWidth, 4 * Console.LargestWindowHeight);
            Console.SetWindowSize(230, 62);
            Console.SetWindowPosition(0, 0);
#pragma warning restore CA1416 // Validate platform compatibility
            Console.TreatControlCAsInput = true;
        }

    }
}
