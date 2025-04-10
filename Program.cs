namespace AirlineReservationConsoleSystem
{
    internal class Program
    {
        //Main method ...
        static void Main(string[] args)
        {
            //calling StartSystem() to run the program ...
            StartSystem();
        }

        //System Utilities & Final Flow Function (2)...
        //1. StartSystem() ...
        public static void StartSystem()
        {
            //calling DisplayWelcomeMessage() function ...
            DisplayWelcomeMessage();
        }
        //2. ConfirmAction(string action) ...

        //Startup & Navigation (4) ...
        //1. DisplayWelcomeMessage() ...
        public static void DisplayWelcomeMessage()
        {
            Console.WriteLine("Welcome to Codeline Airlines\nWe hope you have a pleasant time using our services (^0^)");
        }
    }
}
