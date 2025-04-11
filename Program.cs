using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace AirlineReservationConsoleSystem
{
    internal class Program
    {
        //globle variables and arraies for flight ...
        static int MAX_FLIGHT = 5;
        static int FlightCount = 0;
        static string[] flightCode_array = new string[MAX_FLIGHT];
        static string[] fromCity_array = new string[MAX_FLIGHT];
        static string[] toCity_array = new string[MAX_FLIGHT];
        static DateTime[] departureTime_array = new DateTime[MAX_FLIGHT];
        static int[] duration_array = new int[MAX_FLIGHT];
        static int[] seatsNumber_array = new int[MAX_FLIGHT];
        static bool duration_isEmpty;
        static bool seats_isEmpty;

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
                    case 1://to add new flight ...
                        char choice;
                        // do loop to repeat the process of adding new Flight 
                        //based on the user choice y/n ...
                        do
                        {
                            Console.Clear();
                            //to make such that the user do not enter record more than
                            // the arraies size ...
                            if (FlightCount < MAX_FLIGHT)
                            {
                                Console.WriteLine("Please enter the following info:");
                                //declare variables to holde flight info ...
                                string f_Code;
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
                                try
                                {
                                    Console.WriteLine("Flight duration:");
                                    f_duration = int.Parse(Console.ReadLine());
                                }
                                catch (Exception e)
                                {
                                    duration_isEmpty = true;
                                }
                                try
                                {
                                    Console.WriteLine("Flight seats number:");
                                    f_seatsNumber = int.Parse(Console.ReadLine());
                                }
                                catch(Exception e)
                                {
                                    seats_isEmpty = true;
                                }
                                
                                //calling the AddFlight method ...
                                AddFlight(flightCode: f_Code, fromCity: f_fromCity, 
                                          toCity: f_toCity, departureTime: f_departureTime, 
                                          duration: f_duration, seats: f_seatsNumber);
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
  
                        } while (choice == 'y' || choice == 'Y') ;
                        break;

                    case 2://to display all flight ...
                        DisplayAllFlights();
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
            Console.WriteLine("2. Display All Flights");
            Console.WriteLine("3. Book Flight");
            Console.WriteLine("4. Cancel");
            Console.WriteLine("5. View Flights");

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
        //4. AddFlight(string flightCode, string fromCity, string toCity, DateTime departureTime, int duration, int seats)  ...
        public static void AddFlight(string flightCode, string fromCity, 
                                     string toCity, DateTime departureTime, 
                                     int duration, int seats)
        {

            //flightCode input process code ... 
            bool flag_flightCode;
            do
            {
                flag_flightCode = false;

                // Check for null or empty
                if (string.IsNullOrWhiteSpace(flightCode))
                {
                    Console.WriteLine("Flight code cannot be empty. Please enter a valid flight code:");
                    flightCode = Console.ReadLine();
                    flag_flightCode = true;
                    continue;
                }

                // Check if already exists
                for (int i = 0; i < FlightCount; i++)
                {
                    if (flightCode == flightCode_array[i])
                    {
                        Console.WriteLine("Flight code already exists. Please enter a unique flight code:");
                        flightCode = Console.ReadLine();
                        flag_flightCode = true;
                        break;
                    }
                }

            } while (flag_flightCode);


            //fromCity input process code ... 
            bool flag_fromCity;//to know if the fromCity add or not ...
                    do
                    {
                        flag_fromCity = false;
                        //to check if fromCity has number or not ...
                        bool check_fromCity = IsAlpha(fromCity);
                        if (check_fromCity == false)
                        {
                            Console.WriteLine("From city can not contains number and con not be null ..." +
                                              "please enter from city again");
                            flag_fromCity = true;
                            if (flag_fromCity)
                            {
                                Console.WriteLine("Flight from city:");
                                fromCity = Console.ReadLine();
                            }
                        }

                    } while (flag_fromCity);

            //toCity input process code ... 
            bool flag_toCity;//to know if the toCity add or not ...
            do
                    {
                        flag_toCity = false;
                        //to check if fromCity has number or not ...
                        bool check_toCity = IsAlpha(toCity);
                        if (check_toCity == false)
                        {
                            Console.WriteLine("To city can not contains number and con not be null ..." +
                                              "please enter to city again");
                            flag_toCity = true;
                            if (flag_toCity)
                            {
                                Console.WriteLine("Flight to city:");
                                toCity = Console.ReadLine();
                            }
                        }

                    } while (flag_toCity);

            //duration input process code ... 
            bool flag_duration;//to know if the duration add or not ...
            do
            {
            
                flag_duration = false;
                //it must not be empty ...
                if (duration_isEmpty)
                {
                    Console.WriteLine("Flight duration not vaild it can not be empty");
                    flag_duration = true;
                    if (flag_duration)
                    {
                        try
                        {
                            Console.WriteLine("Flight duration:");
                            duration = int.Parse(Console.ReadLine());
                            duration_isEmpty = false;
                        }
                        catch(Exception e)
                        {
                            duration_isEmpty = true;
                        }
                       
                    }
                }
                //it must be < 0 or not ...
                if(duration < 0)
                {
                    Console.WriteLine("Flight duration not vaild it must be > zero ");
                    flag_duration = true;
                    if (flag_duration)
                    {
                       Console.WriteLine("Flight duration:");
                       duration = int.Parse(Console.ReadLine());
                    }
                }

            } while (flag_duration);

            //seats input process code ... 
            bool flag_seats;//to know if the seats add or not ...
            do
              {
                flag_seats = false;
                //it must not be empty ...
                if (seats_isEmpty)
                {
                    Console.WriteLine("Flight seats not vaild it can not be empty");
                    flag_seats = true;
                    if (flag_seats)
                    {
                        try
                        {
                            Console.WriteLine("Flight seats:");
                            seats = int.Parse(Console.ReadLine());
                            seats_isEmpty = false;
                        }
                        catch (Exception e)
                        {
                            seats_isEmpty = true;
                        }

                    }
                }
                //it must be > 0 or < 100 ...
                if (seats < 0 || seats > 100)
                        {
                            Console.WriteLine("Flight seats not vaild it must be > 0 and < 100 ");
                            flag_seats = true;
                            if (flag_seats)
                            {
                                Console.WriteLine("Flight seats:");
                                seats = int.Parse(Console.ReadLine());
                            }
                        }

                    } while (flag_seats);

            //to store the data in the arraies ...
            flightCode_array[FlightCount] = flightCode;
            fromCity_array[FlightCount] = fromCity;
            toCity_array[FlightCount] = toCity;
            departureTime_array[FlightCount] = departureTime;
            duration_array[FlightCount] = duration;
            seatsNumber_array[FlightCount] = seats;

            // so the system know that there are one more flight added ......
            FlightCount++;
            Console.WriteLine("Flight add successfully ...");
        }

        //Flight and Passenger Management (4) ...
        //1. DisplayAllFlights() ...
        public static void DisplayAllFlights()
        {
            Console.WriteLine("Flight information:");
            Console.WriteLine("Flight Code | From City | To City | Departure Time | Duration | Seats");
            //loop to show all flight info for all records ...
            for(int i = 0; i<FlightCount; i++)
            {
                Console.WriteLine($"{flightCode_array[i]} | {fromCity_array[i]} " +
                                  $"| {toCity_array[i]} | {departureTime_array[i]} " +
                                  $"| {duration_array[i]} | {seatsNumber_array[i]}");
            }
        }

        //ADDITIONAL METHODS ...
        //1. To check of the string contains something other than letters like number and empty space(this methos return true or false)....
        static bool IsAlpha(string input)
        {
            return Regex.IsMatch(input, "^[a-zA-Z]+$");
        }


    }
}
