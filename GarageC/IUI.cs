using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageC
{
    internal interface IUI
    {
        internal abstract (bool, int, string?) GetConsoleInt(string prompt, int min = int.MinValue, int max = int.MaxValue, int cancelInt = -1);
        internal abstract (bool, double, string?) GetConsoleDouble(string prompt, double min = double.MinValue, double max = double.MaxValue, double cancelDouble = -1.0);
        internal abstract (bool, string?) GetConsoleSentence(string prompt, uint minLength = uint.MinValue, uint maxLength = uint.MaxValue, bool startSentenceWithUpperCase = true, string cancelString = "#cancel!");

    }
}
