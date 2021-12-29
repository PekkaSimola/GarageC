using System;
using System.Linq;
using System.Text;

namespace GarageC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string? userInput = "";
            //int userIntInput = 0;
            //double userDoubleInput = 0d;
            //bool success = false;

            //ConsoleUI ui = new();
            //int i = ui.E
            
           // Vehicle v = new Vehicle();
            //Vehicle.CODE_VERSION_ID = "kalle";
            //Console.WriteLine(Vehicle.CODE_VERSION_ID);

            //double dd = ConsoleUI.ESC_DOUBLE;

            Airplane b = new("ABC123", "volvo");
            b.Color = "White";
            //b.BoatType = BoatType.Sailboat; 
            //b.IsDoubleDecker = true;
            //b.CylinderVolume = 250;
            b.AirplaneType = AirplaneType.Propeller;
            //b.CarType = CarType.Van;
            string ss = b.ToText() + b.ToText();
            Console.WriteLine(ss);

            //Console.WriteLine(b.RegNo);
            //Console.WriteLine(b.Brand);


            //StringBuilder ss = new();
            //ss.AppendLine(string.Concat (Enumerable.Repeat('¯', 5)));
            //Console.WriteLine(ss.ToString());



            //do
            //{
            //    success = Tools.GetConsoleInt("Ange ett heltal: ", ref userIntInput, 0, 50, -1);
            //    if (!success) Console.WriteLine($"{userIntInput} är ett ogiltigt nummer! Ange ett heltal mellan 0 och 50.");

            //} while (!success);
            //if (userIntInput == -1)
            //    Console.WriteLine($"Avbryten av användaren.");
            //else
            //    Console.WriteLine($"Du gav ett heltal ({userIntInput}), vilket är inom det giltiga intervallet: 0 – 50.");

            //do
            //{
            //    success = Tools.GetConsoleDouble("Ange ett tal: ", ref userDoubleInput, 0, 50, -1d);
            //    if (!success) Console.WriteLine($"{userDoubleInput} är ett ogiltigt nummer! Ange ett tal mellan 0 och 50.");

            //} while (!success);
            //if (userDoubleInput == -1d)
            //    Console.WriteLine($"Avbryten av användaren.");
            //else
            //    Console.WriteLine($"Du gav ett tal ({userDoubleInput}), vilket är inom det giltiga intervallet: 0 – 50.");

            //do
            //{
            //    success = Tools.GetConsoleSentence("Ange en mening: ", ref userInput, 1, 50, true);
            //    if (!success) Console.WriteLine($"Fel längd: {userInput} • Ange en mening mellan 1 och 50 tecken.");

            //} while (!success);
            //if (userInput == "")
            //    Console.WriteLine($"Avbryten av användaren.");
            //else
            //    Console.WriteLine($"Du gav en mening:  {userInput} ; vilket ligger inom giltig längd: 1 – 50.");


            //string? reg = "  abcd   123       ";
            //if (!Tools.FixRegNo(ref reg))
            //{
            //    Console.WriteLine($"RegNo nix: {reg}");

            //}
            //else
            //{
            //    Console.WriteLine($"RegNo ok: {reg}");
            //};
            //string dd = "  123. f33 ";
            //Console.WriteLine(double.Parse(dd));
            //bool bb = Tools.DoubleString(ref dd);
            //Console.WriteLine($"bb = {bb} dd = |{dd}|");

            //double ddd = 3.5;

            //userInput = Console.ReadLine();
            //int ii;
            //bool b = int.TryParse(userInput, out ii);
            //if (b)
            //    Console.WriteLine($"TryParse: {userInput} to {ii}");
            //else
            //    Console.WriteLine($"error: {userInput} to {ii}");

        }

        private static void InputSomeTestData()
        {

        }
    }
}
