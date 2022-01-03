namespace GarageC
{
    internal interface IVehicle
    {
        internal string[] BodyToDisplay(string[] subClass = null, string ParkingText = "");
        internal string TitleText();
        internal string ToText();
    }
}
