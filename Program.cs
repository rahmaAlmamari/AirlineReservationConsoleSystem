namespace AirlineReservationConsoleSystem
{
    internal class Program
    {
        //globle variables and arraies for flight ...
        static int MAX_FLIGHT = 5;
        static int FlightCount = 0;
        string[] flightCode = new string[MAX_FLIGHT];
        string[] fromCity = new string[MAX_FLIGHT];
        string[] toCity = new string[MAX_FLIGHT];
        DateTime[] departureTime = new DateTime[MAX_FLIGHT];
        int[] seatsNumber = new int[MAX_FLIGHT];
        int[] duration = new int[MAX_FLIGHT];

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
                    case 1:
                        Console.WriteLine("Please enter the following info:");
                        //declare variables to holde flight info ...
                        string f_Code = "Null";
                        string f_fromCity = "Null";
                        string f_toCity = "Null";
                        DateTime f_departureTime = DateTime.Now;
                        int f_seatsNumber = 0;
                        int f_duration = 0;

                        Console.WriteLine("Flight code:");
                        f_Code = Console.ReadLine();
                        Console.WriteLine("Flight from city:");
                        f_fromCity = Console.ReadLine();
                        Console.WriteLine("Flight to city:");
                        f_toCity = Console.ReadLine();
                        Console.WriteLine("Flight seats number:");
                        f_seatsNumber = int.Parse(Console.ReadLine());
                        Console.WriteLine("Flight duration:");
                        f_duration = int.Parse(Console.ReadLine());

                        break;
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
            Console.WriteLine("1. Add Flight");
            Console.WriteLine("2. Book Flight");
            Console.WriteLine("3. Cancel");
            Console.WriteLine("4. View Flights");

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
        //4. AddFlight(string flightCode, string fromCity, string toCity, DateTime departureTime, int duration)  ...
        public static void AddFlight(string flightCode, string fromCity, string toCity, DateTime departureTime, int duration)
        {
            char choice;
            // do loop to repeat the process of adding new Flight 
            //based on the user choice y/n ...
            do
            {
                //to make such that the user do not enter record more than
                // the arraies size ...
                if (FlightCount < MAX_FLIGHT)
                {
                   



                    //to store flight info in the arraies ...
                    //flightCode[FlightCount] = f_Code;
                    //fromCity[FlightCount] = f_fromCity;
                    //toCity[FlightCount] = f_toCity;
                    //departureTime[FlightCount] = f_departureTime;
                    //seatsNumber[FlightCount] = f_seatsNumber;
                    //duration[FlightCount] = f_duration;

                    // so the system know that there are one more flight added ......
                    FlightCount++;
                    Console.WriteLine("Flight add successfully ...");
                    Console.WriteLine("Do you want to add anther Flight? y / n");
                    choice = Console.ReadKey().KeyChar;
                    Console.ReadLine();//just to hold second ...
                }
                else
                {
                    Console.WriteLine("Sory you can not add more Flight there are no space remain!");
                    Console.WriteLine();
                    choice = 'n';
                }

            } while (choice == 'y' || choice == 'Y');

        }
    }
}
