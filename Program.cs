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
            //just to hold a second ...
            Console.ReadLine();
            // we use while loop to repeat the process and we set true so it will not stop ... 
            while (true)
            {
                //run switch to access the services user want based on user choice ...
                switch (ShowMainMenu())
                {
                    case 0:
                        ExitApplication();
                        //using return to stop the whole method so the whole program stop ...
                        return;

                    default:
                        Console.WriteLine("\n You enter unaccepted option! ... to try again click enter key");
                        break;
                }
                // we add this line just to stop the program from clear 'Console.Clear();'
                // the screen before the user see the result ...
                Console.ReadLine();
            }
        }
        //2. ConfirmAction(string action) ...

        //Startup & Navigation (4) ...
        //1. DisplayWelcomeMessage() ...
        public static void DisplayWelcomeMessage()
        {
            Console.WriteLine("Welcome to Codeline Airlines\nWe hope you have a pleasant time using our services " +
                              "(^0^)\nPress enter key to go to the menu");
        }
        //2. ShowMainMenu() ...
        public static int ShowMainMenu()
        {
            //to store user choice in avriable ...
            int menuChoice = 0;
            //just to clear the screen ...
            Console.Clear();
            Console.WriteLine("System Menu please select option:\n");
            Console.WriteLine("1. Book Flight");
            Console.WriteLine("2. Cancel");
            Console.WriteLine("3. View Flights");

            Console.WriteLine("0. Exit the system");

            Console.Write("\nEnter your option: \n");
            menuChoice = int.Parse(Console.ReadLine());

            return menuChoice;
        }
        //3. ExitApplication() ...
        public static void ExitApplication()
        {
            Console.WriteLine("Have a nice day ..."); 
        }
    }
}
