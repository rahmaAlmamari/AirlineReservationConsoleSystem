﻿using System.Globalization;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace AirlineReservationConsoleSystem
{
    internal class Program
    {
        //Array Syistem ...
        //globle varables and arraies for flight ...
        static int MAX_FLIGHT = 5;
        static int FlightCount = 0;
        static string[] flightCode_array = new string[MAX_FLIGHT];
        static string[] fromCity_array = new string[MAX_FLIGHT];
        static string[] toCity_array = new string[MAX_FLIGHT];
        static DateTime[] departureTime_array = new DateTime[MAX_FLIGHT];
        static int[] duration_array = new int[MAX_FLIGHT];
        static int[] seatsNumber_array = new int[MAX_FLIGHT];
        static bool departureTime_isEmpty;
        static int departureTime_index;
        static bool duration_isEmpty;
        static bool seats_isEmpty;
        //globle variables and arraies for passenger ...
        static int MAX_PASSENGER = 100;
        static int PassengerCount = 0;
        static string[] passengerNames_array = new string[MAX_PASSENGER];
        static int[] passenger_BookingFlightIndex = new int[MAX_PASSENGER];
        static string[] passengerBookingID_array = new string[MAX_PASSENGER];
        static bool Book_fCodeIsEmpty;
        //globle variable for calculate total fare ...
        static bool price_isEmpty;
        static bool TicketNumber_isEmpty;
        //.......................................................................................
        //List System ...
        //globle varables and lists for flight ...
        static List<string> flightCode_List = new List<string>();
        static List<string> fromCity_List = new List<string>();
        static List<string> toCity_List = new List<string>();
        static List<DateTime> departureTime_List = new List<DateTime>();
        static List<int> duration_List = new List<int>();
        static List<int> seatsNumber_List = new List<int>();
        //globle variables and arraies for passenger ...
        static List<string> passengerNames_List = new List<string>();
        static List<int> passenger_BookingFlightIndex_List = new List<int>();
        static List<string> passengerBookingID_List = new List<string>();

        //Main method ...
        static void Main(string[] args)
        {
            //calling DisplayWelcomeMessage() function ...
            DisplayWelcomeMessage();
            //just to hold a second ...
            Console.ReadLine();
            bool systemRepeat;
            do
            {
                systemRepeat = false;
                //to get user chioce if he want to use the system with array or list ...
                int system = 0;
                Console.WriteLine("System run chioces:\n 1. Array\n 2. List");
                Console.WriteLine("Please enter your chioces:");
                try
                {
                    system = int.Parse(Console.ReadLine());
                }catch(Exception e)
                {
                    system = 3;
                }
                
                if (system == 1)
                {
                    //Array System ....................................
                    //calling StartSystem() to run the program as array ...
                    StartSystem();
                }
                else if (system == 2)
                {
                    //List System ...................................
                    //calling StartSystemList() to run the program as array ...
                    StartSystemList();
                }
                else
                {
                    Console.WriteLine("Sorry ... you enter a wrong input! enter 1 or 2");
                    systemRepeat = true;
                }
            } while (systemRepeat);
        }

        //System Utilities & Final Flow Function (2)...
        //1. StartSystem() ...
        public static void StartSystem()
        {
            // we use while loop to repeat the process and we set true so it will not stop ... 
            while (true)
            {
                //run switch to access the services user want based on user choice ...
                switch (ShowMainMenu())
                {
                    case 1://to add new flight ...
                        char choice1;
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
                                DateTime f_departureTime;
                                int f_seatsNumber = 0;
                                int f_duration = 0;
                                Console.WriteLine("Flight code:");
                                f_Code = Console.ReadLine();
                                Console.WriteLine("Flight from city:");
                                f_fromCity = Console.ReadLine();
                                Console.WriteLine("Flight to city:");
                                f_toCity = Console.ReadLine();
                                f_departureTime = InputFlightDepartureTimeValide();
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
                                choice1 = Console.ReadKey().KeyChar;
                                Console.ReadLine();//just to hold second ...
                            }
                            else
                            {
                                Console.WriteLine("Sory you can not add more Flight there are no space remain!");
                                Console.WriteLine();
                                choice1 = 'n';
                            }
  
                        } while (choice1 == 'y' || choice1 == 'Y') ;
                        break;

                    case 2://to display all flight ...
                        DisplayAllFlights();
                        break;

                    case 3://to find flight by code ...
                        string f_code;
                        Console.WriteLine("Enter flight code you want to search for:");
                        f_code = Console.ReadLine();
                        bool result = FindFlightByCode(f_code);
                        if (result)
                        {
                            Console.WriteLine("Flight code is found");
                        }
                        else
                        {
                            Console.WriteLine("Flight code not found");
                        }
                        break;

                    case 4://to update flight departure ...
                        char choice4;
                        // do loop to repeat the process of update flight departure 
                        //based on the user choice y/n ...
                        do
                        {
                        //to display all flights wtih details ...
                        DisplayAllFlights();
                        //update process start here ...
                        string updateFcode;
                        //DateTime updateDeparture;
                        Console.WriteLine("Enter flight code to update departure:");
                        updateFcode = Console.ReadLine();
                        //to check if flight code exis or not ...
                        bool flag_fCode = false;
                        for(int i = 0; i < FlightCount; i++)
                        {
                            if(updateFcode == flightCode_array[i])
                            {
                                flag_fCode = false;
                                Console.WriteLine("This flight code has departure time as: " 
                                                  + departureTime_array[i]);
                                departureTime_index = i;
                                    ValidateFlightDepartureTime_Update(i);
                               
                                break;
                            }
                            else
                            {
                                flag_fCode = true;
                            }
                        }
                        if (flag_fCode)
                        {
                            Console.WriteLine("Sorry flight code you enter dose not exit!");
                        }
                            Console.WriteLine("Do you want to update anther" +
                                              " flight departure time? y / n");
                            choice4 = Console.ReadKey().KeyChar;
                            Console.ReadLine();//just to hold second ...
                        } while (choice4 == 'y' || choice4 == 'Y');
                        break;

                    case 5://to Book Flight ...
                        char choice5;
                        // do loop to repeat the process of book flight 
                        //based on the user choice y/n ...
                        do
                        {
                            //to display all flights wtih details ...
                            DisplayAllFlights();

                            //booking process ...
                            string passengerName_Book;
                            string f_code_book;
                            Console.WriteLine("Please enter booking details:");
                            Console.WriteLine("Flight code (press ENTER to skip):");
                            f_code_book = Console.ReadLine();
                            Console.WriteLine("Passenger Name:");
                            passengerName_Book = Console.ReadLine();
                            //calling BookFlight based on flight code input ...
                            if (string.IsNullOrWhiteSpace(f_code_book))
                            {
                                // No flight code entered, use default
                                BookFlight(passengerName_Book);
                            }
                            else
                            {
                                // Use entered flight code
                                BookFlight(passengerName_Book, f_code_book);
                            }


                            Console.WriteLine("Do you want to book anther" +
                                              " flight? y / n");
                            choice5 = Console.ReadKey().KeyChar;
                            Console.ReadLine();//just to hold second ...
                        } while (choice5 == 'y' || choice5 == 'Y');
                        break;

                    case 6://to display flight details ...
                        char choice6;
                        // do loop to repeat the process of display flight details
                        //based on the user choice y/n ...
                        do
                        {
                            //to display all flights wtih details ...
                            DisplayAllFlights();
                            //display flight details process start here ...
                            string fCode_displayDetails;
                            bool fCodeIsExist;
                            Console.WriteLine("Please enter flight code to display it's details:");
                            fCode_displayDetails = Console.ReadLine();
                            //flightCode input process code ... 
                            bool flag_flightCode;
                            do
                            {
                                flag_flightCode = false;

                                // Check for null or empty
                                if (string.IsNullOrWhiteSpace(fCode_displayDetails))
                                {
                                    Console.WriteLine("Flight code cannot be empty. Please enter a valid flight code:");
                                    fCode_displayDetails = Console.ReadLine();
                                    flag_flightCode = true;
                                    continue;
                                }
                            } while (flag_flightCode);
                            //calling validate flight code ...
                            fCodeIsExist = ValidateFlightCode(fCode_displayDetails);
                            if (fCodeIsExist)
                            {
                                DisplayFlightDetails(fCode_displayDetails);
                            }
                            else
                            {
                                Console.WriteLine("Sorry flight code dose not exist!");
                            }

                            Console.WriteLine("Do you want to display anther" +
                                                " flight code details? y / n");
                            choice6 = Console.ReadKey().KeyChar;
                            Console.ReadLine();//just to hold second ...
                        } while (choice6 == 'y' || choice6 == 'Y');
                        break;

                    case 7://to search bookings by destination ...
                        char choice7;
                        // do loop to repeat the process of search bookings by destination 
                        //based on the user choice y/n ...
                        do
                        {
                            string toCity_search;
                            Console.WriteLine("Please enter the destination city you search for:");
                            toCity_search = Console.ReadLine();
                            //to validate toCity input ...
                            bool flag_toCity;//to know if the toCity add or not ...
                            do
                            {
                                flag_toCity = false;
                                //to check if fromCity has number or not ...
                                bool check_toCity = IsAlpha(toCity_search);
                                if (check_toCity == false)
                                {
                                    Console.WriteLine("Destination city can not contains number and con not be null ..." +
                                                      "please enter to city again");
                                    flag_toCity = true;
                                    if (flag_toCity)
                                    {
                                        Console.WriteLine("Flight to city:");
                                        toCity_search = Console.ReadLine();
                                    }
                                }

                            } while (flag_toCity);
                            //search bookings by destination process start here ...
                            SearchBookingsByDestination(toCity_search);

                            Console.WriteLine("Do you want to search for anther bookings by " +
                                               " destination? y / n");
                            choice7 = Console.ReadKey().KeyChar;
                            Console.ReadLine();//just to hold second ...
                        } while (choice7 == 'y' || choice7 == 'Y');
                        break;

                    case 8://to cancel flight booking ...
                        char choice8;
                        // do loop to repeat the process of cancel flight booking 
                        //based on the user choice y/n ...
                        do
                        {
                            //to display all passenger details ...
                            Console.WriteLine("All passenger details:");
                            Console.WriteLine("Passenger ID | Passenger Name | Flight Code");
                            for(int i = 0; i < PassengerCount; i++)
                            {
                                int index = passenger_BookingFlightIndex[i];
                                Console.WriteLine($"{passengerBookingID_array[i]} | " +
                                                  $"{passengerNames_array[i]} | " +
                                                  $"{flightCode_array[index]}");
                            }
                            bool flage_cancel;
                            bool flage_nameFound = false;
                            string passengerNameToCansel;
                            Console.WriteLine("Please enter passsenger name you want to cansel his booking:");
                            passengerNameToCansel = Console.ReadLine();
                            do
                            {
                                flage_cancel = false;
                                // Check for null or empty
                                if (string.IsNullOrWhiteSpace(passengerNameToCansel))
                                {
                                    Console.WriteLine("Passenger name cannot be empty. Please enter a valide passenger name:");
                                    passengerNameToCansel = Console.ReadLine();
                                    flage_cancel = true;
                                    continue;
                                }
                                //to check if passenger name exist or not ...
                                for (int i = 0; i < PassengerCount; i++)
                                {
                                    if(passengerNameToCansel == passengerNames_array[i])
                                    {
                                        flage_cancel = false;
                                        flage_nameFound = true;
                                        break;
                                    }
                                }
                                if (!flage_nameFound)
                                {
                                    Console.WriteLine("Passenger name not found. Please enter a valid name:");
                                    passengerNameToCansel = Console.ReadLine();
                                    flage_cancel = true;
                                }

                            } while (flage_cancel);
                           
                            //to get index for passenger name ...
                            int nameIndex = Array.IndexOf(passengerNames_array, passengerNameToCansel);
                            //cancel flight booking process start here ....
                            CancelFlightBooking(out passengerNameToCansel, nameIndex);

                            Console.WriteLine("Do you want to search for anther bookings by " +
                                           " destination? y / n");
                            choice8 = Console.ReadKey().KeyChar;
                            Console.ReadLine();//just to hold second ...
                        } while (choice8 == 'y' || choice8 == 'Y');
                        break;

                    case 9://to calculate total fare ...
                        char choice9;
                        // do loop to repeat the process of calculate total fare 
                        //based on the user choice y/n ...
                        do
                        {
                            double price = 0;
                            int TicketNumber = 0;
                            int discount;
                            char thereIsDiscount;
                            Console.WriteLine("Plesae enter the following details to calculate total fare:");
                            //to know if the user enter price or not ...
                            try
                            {
                                Console.WriteLine("Price:");
                                price = double.Parse(Console.ReadLine());
                            }
                            catch (Exception e)
                            {
                                price_isEmpty = true;
                            }
                            //to validate the price input ...
                            bool flag_price = false;
                            do
                            {
                                flag_price = false;
                                //it must not be empty ...
                                if (price_isEmpty)
                                {
                                    Console.WriteLine("Price not vaild it can not be empty");
                                    flag_price = true;
                                    if (flag_price)
                                    {
                                        try
                                        {
                                            Console.WriteLine("Price:");
                                            price = double.Parse(Console.ReadLine());
                                            price_isEmpty = false;
                                        }
                                        catch (Exception e)
                                        {
                                            price_isEmpty = true;
                                        }

                                    }
                                }
                                //it must be > 0 ...
                                if (price < 0)
                                {
                                    Console.WriteLine("Price is not vaild it must be > 0 ");
                                    flag_price = true;
                                    if (flag_price)
                                    {
                                        Console.WriteLine("Price:");
                                        price = double.Parse(Console.ReadLine());
                                    }
                                }

                            } while (flag_price);



                            //to know if the user enter TicketNumber or not ...
                            try
                            {
                                Console.WriteLine("Ticket number:");
                                TicketNumber = int.Parse(Console.ReadLine());
                            }
                            catch(Exception e)
                            {
                                TicketNumber_isEmpty = true;
                            }
                            //to validate ticket number input ...
                            bool flag_ticket = false;
                            do
                            {
                                flag_ticket = false;
                                //it must not be empty ...
                                if (TicketNumber_isEmpty)
                                {
                                    Console.WriteLine("Ticket number not vaild it can not be empty");
                                    flag_ticket = true;
                                    if (flag_ticket)
                                    {
                                        try
                                        {
                                            Console.WriteLine("Ticket number:");
                                            TicketNumber = int.Parse(Console.ReadLine());
                                            TicketNumber_isEmpty = false;
                                        }
                                        catch (Exception e)
                                        {
                                            TicketNumber_isEmpty = true;
                                        }

                                    }
                                }
                                //it must be > 0 ...
                                if (TicketNumber < 0)
                                {
                                    Console.WriteLine("Ticket number is not vaild it must be > 0 ");
                                    flag_ticket = true;
                                    if (flag_ticket)
                                    {
                                        Console.WriteLine("Ticket number:");
                                        TicketNumber = int.Parse(Console.ReadLine());
                                    }
                                }

                            } while (flag_ticket);



                            //to enter discount ...
                            Console.WriteLine("Do you want to add discount y/n?");
                            thereIsDiscount = char.Parse(Console.ReadLine());
                            if(thereIsDiscount == 'y' || thereIsDiscount == 'Y')
                            {
                                Console.WriteLine("Enter discount:");
                                discount = int.Parse(Console.ReadLine());
                                //calling CalculateFare method ...
                                Console.WriteLine("Your total fare: " + CalculateFare(price, TicketNumber, discount));
                            }
                            else
                            {
                                //calling CalculateFare method ...
                                Console.WriteLine("Your total fare: " + CalculateFare(price, TicketNumber));
                            }


                            Console.WriteLine("Do you want to calculate total fare again? y / n");
                            choice9 = Console.ReadKey().KeyChar;
                            Console.ReadLine();//just to hold second ...
                        } while (choice9 == 'y' || choice9 == 'Y');
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
        //1. StartSystemList() ...
        public static void StartSystemList()
        {
            // we use while loop to repeat the process and we set true so it will not stop ... 
            while (true)
            {
                //run switch to access the services user want based on user choice ...
                switch (ShowMainMenuList())
                {
                    case 1://to add new flight ...
                        char choice1;
                        // do loop to repeat the process of adding new Flight 
                        //based on the user choice y/n ...
                        do
                        {
                            Console.Clear();
                            //to make such that the user do not enter record more than
                            // the arraies size ...
                            Console.WriteLine("Please enter the following info:");
                            //declare variables to holde flight info ...
                            string f_Code;
                            string f_fromCity = "Null";
                            string f_toCity = "Null";
                            DateTime f_departureTime;
                            int f_seatsNumber = 0;
                            int f_duration = 0;
                            Console.WriteLine("Flight code:");
                            f_Code = Console.ReadLine();
                            Console.WriteLine("Flight from city:");
                            f_fromCity = Console.ReadLine();
                            Console.WriteLine("Flight to city:");
                            f_toCity = Console.ReadLine();
                            f_departureTime = InputFlightDepartureTimeValide();
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
                            catch (Exception e)
                            {
                               seats_isEmpty = true;
                            }
                            //calling the AddFlight method ...
                            AddFlightList(flightCode: f_Code, fromCity: f_fromCity,
                                      toCity: f_toCity, departureTime: f_departureTime,
                                      duration: f_duration, seats: f_seatsNumber);
                            Console.WriteLine("Do you want to add anther Flight? y / n");
                            choice1 = Console.ReadKey().KeyChar;
                            Console.ReadLine();//just to hold second ...
                        } while (choice1 == 'y' || choice1 == 'Y');
                        break;

                    case 2://to display all flight ...
                        DisplayAllFlightsList();
                        break;

                    case 3://to find flight by code ...
                        string f_code;
                        Console.WriteLine("Enter flight code you want to search for:");
                        f_code = Console.ReadLine();
                        bool result = FindFlightByCodeList(f_code);
                        if (result)
                        {
                            Console.WriteLine("Flight code is found");
                        }
                        else
                        {
                            Console.WriteLine("Flight code not found");
                        }
                        break;

                    case 4://to update flight departure ...
                        char choice4;
                        // do loop to repeat the process of update flight departure 
                        //based on the user choice y/n ...
                        do
                        {
                            //to display all flights wtih details ...
                            DisplayAllFlightsList();
                            //update process start here ...
                            string updateFcode;
                            //DateTime updateDeparture;
                            Console.WriteLine("Enter flight code to update departure:");
                            updateFcode = Console.ReadLine();
                            //to check if flight code exis or not ...
                            bool flag_fCode = false;
                            for (int i = 0; i < flightCode_List.Count; i++)
                            {
                                if (updateFcode == flightCode_List[i])
                                {
                                    flag_fCode = false;
                                    Console.WriteLine("This flight code has departure time as: "
                                                      + departureTime_List[i]);
                                    departureTime_index = i;
                                    ValidateFlightDepartureTime_UpdateList(i);

                                    break;
                                }
                                else
                                {
                                    flag_fCode = true;
                                }
                            }
                            if (flag_fCode)
                            {
                                Console.WriteLine("Sorry flight code you enter dose not exit!");
                            }
                            Console.WriteLine("Do you want to update anther" +
                                              " flight departure time? y / n");
                            choice4 = Console.ReadKey().KeyChar;
                            Console.ReadLine();//just to hold second ...
                        } while (choice4 == 'y' || choice4 == 'Y');
                        break;

                    case 5://to Book Flight ...
                        char choice5;
                        // do loop to repeat the process of book flight 
                        //based on the user choice y/n ...
                        do
                        {
                            //to display all flights wtih details ...
                            DisplayAllFlightsList();

                            //booking process ...
                            string passengerName_Book;
                            string f_code_book;
                            Console.WriteLine("Please enter booking details:");
                            Console.WriteLine("Flight code (press ENTER to skip):");
                            f_code_book = Console.ReadLine();
                            Console.WriteLine("Passenger Name:");
                            passengerName_Book = Console.ReadLine();
                            //calling BookFlight based on flight code input ...
                            if (string.IsNullOrWhiteSpace(f_code_book))
                            {
                                // No flight code entered, use default
                                BookFlightList(passengerName_Book);
                            }
                            else
                            {
                                // Use entered flight code
                                BookFlightList(passengerName_Book, f_code_book);
                            }


                            Console.WriteLine("Do you want to book anther" +
                                              " flight? y / n");
                            choice5 = Console.ReadKey().KeyChar;
                            Console.ReadLine();//just to hold second ...
                        } while (choice5 == 'y' || choice5 == 'Y');
                        break;

                    case 6://to display flight details ...
                        char choice6;
                        // do loop to repeat the process of display flight details
                        //based on the user choice y/n ...
                        do
                        {
                            //to display all flights wtih details ...
                            DisplayAllFlightsList();
                            //display flight details process start here ...
                            string fCode_displayDetails;
                            bool fCodeIsExist;
                            Console.WriteLine("Please enter flight code to display it's details:");
                            fCode_displayDetails = Console.ReadLine();
                            //flightCode input process code ... 
                            bool flag_flightCode;
                            do
                            {
                                flag_flightCode = false;

                                // Check for null or empty
                                if (string.IsNullOrWhiteSpace(fCode_displayDetails))
                                {
                                    Console.WriteLine("Flight code cannot be empty. Please enter a valid flight code:");
                                    fCode_displayDetails = Console.ReadLine();
                                    flag_flightCode = true;
                                    continue;
                                }
                            } while (flag_flightCode);
                            //calling validate flight code ...
                            fCodeIsExist = ValidateFlightCodeList(fCode_displayDetails);
                            if (fCodeIsExist)
                            {
                                DisplayFlightDetails_List(fCode_displayDetails);
                            }
                            else
                            {
                                Console.WriteLine("Sorry flight code dose not exist!");
                            }

                            Console.WriteLine("Do you want to display anther" +
                                                " flight code details? y / n");
                            choice6 = Console.ReadKey().KeyChar;
                            Console.ReadLine();//just to hold second ...
                        } while (choice6 == 'y' || choice6 == 'Y');
                        break;

                    case 7://to search bookings by destination ...
                        char choice7;
                        // do loop to repeat the process of search bookings by destination 
                        //based on the user choice y/n ...
                        do
                        {
                            string toCity_search;
                            Console.WriteLine("Please enter the destination city you search for:");
                            toCity_search = Console.ReadLine();
                            //to validate toCity input ...
                            bool flag_toCity;//to know if the toCity add or not ...
                            do
                            {
                                flag_toCity = false;
                                //to check if fromCity has number or not ...
                                bool check_toCity = IsAlpha(toCity_search);
                                if (check_toCity == false)
                                {
                                    Console.WriteLine("Destination city can not contains number and con not be null ..." +
                                                      "please enter to city again");
                                    flag_toCity = true;
                                    if (flag_toCity)
                                    {
                                        Console.WriteLine("Flight to city:");
                                        toCity_search = Console.ReadLine();
                                    }
                                }

                            } while (flag_toCity);
                            //search bookings by destination process start here ...
                            SearchBookingsByDestination_List(toCity_search);

                            Console.WriteLine("Do you want to search for anther bookings by " +
                                               " destination? y / n");
                            choice7 = Console.ReadKey().KeyChar;
                            Console.ReadLine();//just to hold second ...
                        } while (choice7 == 'y' || choice7 == 'Y');
                        break;

                    case 8://to cancel flight booking ...
                        char choice8;
                        // do loop to repeat the process of cancel flight booking 
                        //based on the user choice y/n ...
                        do
                        {
                            //to display all passenger details ...
                            Console.WriteLine("All passenger details:");
                            Console.WriteLine("Passenger ID | Passenger Name | Flight Code");
                            for (int i = 0; i < passengerNames_List.Count; i++)
                            {
                                int index = passenger_BookingFlightIndex_List[i];
                                Console.WriteLine($"{passengerBookingID_List[i]} | " +
                                                  $"{passengerNames_List[i]} | " +
                                                  $"{flightCode_List[index]}");
                            }
                            bool flage_cancel;
                            bool flage_nameFound = false;
                            string passengerNameToCansel;
                            Console.WriteLine("Please enter passsenger name you want to cansel his booking:");
                            passengerNameToCansel = Console.ReadLine();
                            do
                            {
                                flage_cancel = false;
                                // Check for null or empty
                                if (string.IsNullOrWhiteSpace(passengerNameToCansel))
                                {
                                    Console.WriteLine("Passenger name cannot be empty. Please enter a valide passenger name:");
                                    passengerNameToCansel = Console.ReadLine();
                                    flage_cancel = true;
                                    continue;
                                }
                                //to check if passenger name exist or not ...
                                for (int i = 0; i < passengerNames_List.Count; i++)
                                {
                                    if (passengerNameToCansel == passengerNames_List[i])
                                    {
                                        flage_cancel = false;
                                        flage_nameFound = true;
                                        break;
                                    }
                                }
                                if (!flage_nameFound)
                                {
                                    Console.WriteLine("Passenger name not found. Please enter a valid name:");
                                    passengerNameToCansel = Console.ReadLine();
                                    flage_cancel = true;
                                }

                            } while (flage_cancel);

                            //to get index for passenger name ...
                            int nameIndex = passengerNames_List.IndexOf(passengerNameToCansel);
                            //cancel flight booking process start here ....
                            CancelFlightBooking_List(out passengerNameToCansel, nameIndex);

                            Console.WriteLine("Do you want to search for anther bookings by " +
                                           " destination? y / n");
                            choice8 = Console.ReadKey().KeyChar;
                            Console.ReadLine();//just to hold second ...
                        } while (choice8 == 'y' || choice8 == 'Y');
                        break;

                    case 9://to calculate total fare ...
                        char choice9;
                        // do loop to repeat the process of calculate total fare 
                        //based on the user choice y/n ...
                        do
                        {
                            double price = 0;
                            int TicketNumber = 0;
                            int discount;
                            char thereIsDiscount;
                            Console.WriteLine("Plesae enter the following details to calculate total fare:");
                            //to know if the user enter price or not ...
                            try
                            {
                                Console.WriteLine("Price:");
                                price = double.Parse(Console.ReadLine());
                            }
                            catch (Exception e)
                            {
                                price_isEmpty = true;
                            }
                            //to validate the price input ...
                            bool flag_price = false;
                            do
                            {
                                flag_price = false;
                                //it must not be empty ...
                                if (price_isEmpty)
                                {
                                    Console.WriteLine("Price not vaild it can not be empty");
                                    flag_price = true;
                                    if (flag_price)
                                    {
                                        try
                                        {
                                            Console.WriteLine("Price:");
                                            price = double.Parse(Console.ReadLine());
                                            price_isEmpty = false;
                                        }
                                        catch (Exception e)
                                        {
                                            price_isEmpty = true;
                                        }

                                    }
                                }
                                //it must be > 0 ...
                                if (price < 0)
                                {
                                    Console.WriteLine("Price is not vaild it must be > 0 ");
                                    flag_price = true;
                                    if (flag_price)
                                    {
                                        Console.WriteLine("Price:");
                                        price = double.Parse(Console.ReadLine());
                                    }
                                }

                            } while (flag_price);



                            //to know if the user enter TicketNumber or not ...
                            try
                            {
                                Console.WriteLine("Ticket number:");
                                TicketNumber = int.Parse(Console.ReadLine());
                            }
                            catch (Exception e)
                            {
                                TicketNumber_isEmpty = true;
                            }
                            //to validate ticket number input ...
                            bool flag_ticket = false;
                            do
                            {
                                flag_ticket = false;
                                //it must not be empty ...
                                if (TicketNumber_isEmpty)
                                {
                                    Console.WriteLine("Ticket number not vaild it can not be empty");
                                    flag_ticket = true;
                                    if (flag_ticket)
                                    {
                                        try
                                        {
                                            Console.WriteLine("Ticket number:");
                                            TicketNumber = int.Parse(Console.ReadLine());
                                            TicketNumber_isEmpty = false;
                                        }
                                        catch (Exception e)
                                        {
                                            TicketNumber_isEmpty = true;
                                        }

                                    }
                                }
                                //it must be > 0 ...
                                if (TicketNumber < 0)
                                {
                                    Console.WriteLine("Ticket number is not vaild it must be > 0 ");
                                    flag_ticket = true;
                                    if (flag_ticket)
                                    {
                                        Console.WriteLine("Ticket number:");
                                        TicketNumber = int.Parse(Console.ReadLine());
                                    }
                                }

                            } while (flag_ticket);



                            //to enter discount ...
                            Console.WriteLine("Do you want to add discount y/n?");
                            thereIsDiscount = char.Parse(Console.ReadLine());
                            if (thereIsDiscount == 'y' || thereIsDiscount == 'Y')
                            {
                                Console.WriteLine("Enter discount:");
                                discount = int.Parse(Console.ReadLine());
                                //calling CalculateFare method ...
                                Console.WriteLine("Your total fare: " + CalculateFare(price, TicketNumber, discount));
                            }
                            else
                            {
                                //calling CalculateFare method ...
                                Console.WriteLine("Your total fare: " + CalculateFare(price, TicketNumber));
                            }


                            Console.WriteLine("Do you want to calculate total fare again? y / n");
                            choice9 = Console.ReadKey().KeyChar;
                            Console.ReadLine();//just to hold second ...
                        } while (choice9 == 'y' || choice9 == 'Y');
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
        public static bool ConfirmAction(string action)
        {
            //confirm process ...
            bool flag_action;//to know if the user enter choice or not
            char actionChoice = 'y';
            bool actionStatus;//to set the confirm action status true/false ...
            do
            {
                flag_action = false;
                try
                {
                    Console.WriteLine($"“Are you sure to {action} ? Y/N");
                    actionChoice = char.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Please confirm your action");
                    flag_action = true;
                }
            } while (flag_action);
       
            if(actionChoice == 'Y' || actionChoice == 'y')
            {
                actionStatus = true;
            }
            else
            {
                actionStatus = false;
            }

            return actionStatus;
        }




        //Startup & Navigation (4) ....................................................................
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
            Console.WriteLine("System array menu please select option:\n");
            Console.WriteLine("1. Add Flight");
            Console.WriteLine("2. Display All Flights");
            Console.WriteLine("3. Find Flight By Code");
            Console.WriteLine("4. Update Flight Departure");
            Console.WriteLine("5. Book Flight");
            Console.WriteLine("6. Display Flight Details");
            Console.WriteLine("7. Search Bookings By Destination");
            Console.WriteLine("8. Cancel Flight Booking");
            Console.WriteLine("9. Calculate Total Fare");

            Console.WriteLine("0. Exit the system");

            Console.Write("\nEnter your option: \n");
            menuChoice = int.Parse(Console.ReadLine());

            return menuChoice;
        }
        //2. ShowMainMenu() ...
        public static int ShowMainMenuList()
        {
            //to store user choice in avriable ...
            int menuChoice = 0;
            //just to clear the screen ...
            Console.Clear();
            Console.WriteLine("System list menu please select option:\n");
            Console.WriteLine("1. Add Flight");
            Console.WriteLine("2. Display All Flights");
            Console.WriteLine("3. Find Flight By Code");
            Console.WriteLine("4. Update Flight Departure");
            Console.WriteLine("5. Book Flight");
            Console.WriteLine("6. Display Flight Details");
            Console.WriteLine("7. Search Bookings By Destination");
            Console.WriteLine("8. Cancel Flight Booking");
            Console.WriteLine("9. Calculate Total Fare");

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
                bool exists_result = ValidateFlightCode(flightCode);
                if (exists_result)
                {
                    Console.WriteLine("Flight code already exists. Please enter a unique flight code:");
                    flightCode = Console.ReadLine();
                    flag_flightCode = true;
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
        //4. AddFlightList(string flightCode, string fromCity, string toCity, DateTime departureTime, int duration, int seats)  ...
        public static void AddFlightList(string flightCode, string fromCity,
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
                bool exists_result = ValidateFlightCodeList(flightCode);
                if (exists_result)
                {
                    Console.WriteLine("Flight code already exists. Please enter a unique flight code:");
                    flightCode = Console.ReadLine();
                    flag_flightCode = true;
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
                        catch (Exception e)
                        {
                            duration_isEmpty = true;
                        }

                    }
                }
                //it must be < 0 or not ...
                if (duration < 0)
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
            //  numbers.Add(10);
            flightCode_List.Add(flightCode);
            fromCity_List.Add(fromCity);
            toCity_List.Add(toCity);
            departureTime_List.Add(departureTime);
            duration_List.Add(duration);
            seatsNumber_List.Add(seats); 

            // so the system know that there are one more flight added ......
            //FlightCount++;
            Console.WriteLine("Flight add successfully ...");
        }





        //Flight and Passenger Management (4) .................................................
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
        //1. DisplayAllFlightsList() ...
        public static void DisplayAllFlightsList()
        {
            //to check if there are flight or not ...
            if(flightCode_List.Count == 0)
            {
                Console.WriteLine("Sorry there is no flights added to the system yet!");
            }
            //to print flight in the system ...
            else
            {
                Console.WriteLine("Flight information:");
                Console.WriteLine("Flight Code | From City | To City | Departure Time | Duration | Seats");
                //loop to show all flight info for all records ...
                for (int i = 0; i < flightCode_List.Count; i++)
                {
                    Console.WriteLine($"{flightCode_List[i]} | {fromCity_List[i]} " +
                                      $"| {toCity_List[i]} | {departureTime_List[i]} " +
                                      $"| {duration_List[i]} | {seatsNumber_List[i]}");
                }
            }
        }
        //2. FindFlightByCode(string code) ...
        public static bool FindFlightByCode(string code)
        {
            bool found = false;
            //flightCode search process code ... 
            bool flag_flightCode;
            do
            {
                flag_flightCode = false;

                //to check for null or empty
                if (string.IsNullOrWhiteSpace(code))
                {
                    Console.WriteLine("Flight code cannot be empty. Please enter a valid flight code:");
                    code = Console.ReadLine();
                    flag_flightCode = true;
                    continue;
                }

                //to search for flight code ...
                for (int i = 0; i < FlightCount; i++)
                {
                    if (code == flightCode_array[i])
                    {
                        found = true;
                        break;
                    }
                    else
                    {
                        found = false;
                    }
                }

            } while (flag_flightCode);

            return found;
            
        }
        //2. FindFlightByCode(string code) ...
        public static bool FindFlightByCodeList(string code)
        {
            bool found = false;
            //flightCode search process code ... 
            bool flag_flightCode;
            do
            {
                flag_flightCode = false;

                //to check for null or empty
                if (string.IsNullOrWhiteSpace(code))
                {
                    Console.WriteLine("Flight code cannot be empty. Please enter a valid flight code:");
                    code = Console.ReadLine();
                    flag_flightCode = true;
                    continue;
                }

                //to search for flight code ...
                for (int i = 0; i < flightCode_List.Count; i++)
                {
                    if (code == flightCode_List[i])
                    {
                        found = true;
                        break;
                    }
                    else
                    {
                        found = false;
                    }
                }

            } while (flag_flightCode);

            return found;

        }
        //3. UpdateFlightDeparture(ref DateTime departure) ...
        public static void UpdateFlightDeparture(ref DateTime OriginalDeparture, 
                                                     DateTime FinalDeparture)
        {
            //to confirm the update process ...
            bool Confirm = ConfirmAction("Update Flight Departure");
            if (Confirm)
            {
                //to update departure ...
                OriginalDeparture = FinalDeparture;
                Console.WriteLine("Flight departure update successfully");
            }
            else
            {
                Console.WriteLine("Flight departure update stoped");
            }
           
        }
        //4. CancelFlightBooking(out string passengerName) ...

        public static void CancelFlightBooking(out string passengerName, int nameIndex)
        {
            passengerName = passengerNames_array[nameIndex];
            //to confirm the action of cancel the flight booking ...
            bool result = ConfirmAction("Cancel Flight Booking");
            if (result)
            {
                //to loop through all passengerNames_array and passengerBookingID_array
                //elements after passengerName we want to cancel ...
                for (int i = nameIndex; i < PassengerCount; i++)
                {
                    //to move passengerNames_array and passengerBookingID_array
                    //elements one step forwad to cancel passengerName ...
                    passengerNames_array[i] = passengerNames_array[i + 1];
                    passengerBookingID_array[i] = passengerBookingID_array[i + 1];
                }
                //to let the system know that there is passenger booking cancel ...
                PassengerCount--;
                Console.WriteLine("Cancel flight booking process done successfully");
            }
            else
            {
                Console.WriteLine("Cancel flight booking process stoped!");
            }
        }
        //4. CancelFlightBooking_List(out string passengerName) ...

        public static void CancelFlightBooking_List(out string passengerName, int nameIndex)
        {
            passengerName = passengerNames_List[nameIndex];
            //to confirm the action of cancel the flight booking ...
            bool result = ConfirmAction("Cancel Flight Booking");
            if (result)
            {
                //to loop through all passengerNames_array and passengerBookingID_array
                //elements after passengerName we want to cancel ...
                //for (int i = nameIndex; i < passengerNames_List.Count; i++)
                //{
                //    //to move passengerNames_array and passengerBookingID_array
                //    //elements one step forwad to cancel passengerName ...
                //    passengerNames_array[i] = passengerNames_array[i + 1];
                //    passengerBookingID_array[i] = passengerBookingID_array[i + 1];
                //}
                //numbers.Remove(20);
                passengerNames_List.Remove(passengerName);
                passengerBookingID_List.Remove(passengerBookingID_List[nameIndex]);
                //to let the system know that there is passenger booking cancel ...
                //PassengerCount--;
                Console.WriteLine("Cancel flight booking process done successfully");
            }
            else
            {
                Console.WriteLine("Cancel flight booking process stoped!");
            }
        }



        //Passenger Booking Functions (5) ...........................................................
        //1. BookFlight(string passengerName, string flightCode = "Default001") ...
        public static void BookFlight(string passengerName, string flightCode = "Default001")
        {
            //Book Flight process code ... 
            bool flag_BookFlight;
            int booking_index = 0;
            int count_seat = 0;
            do
            {
                flag_BookFlight = false;

                //to check for null or empty
                if (string.IsNullOrWhiteSpace(passengerName))
                {
                    Console.WriteLine("Passenger name cannot be empty. Please enter a valid passenger name:");
                    passengerName = Console.ReadLine();
                    flag_BookFlight = true;
                    continue;
                }
                // If using the default flight code, accept it without searching
                if (flightCode == "Default001")
                {
                    booking_index = MAX_FLIGHT - 1;
                    break; // Exit the loop, default is accepted
                }

                // Try to find the flight code in the array
                bool foundFlight = false;
                for (int i = 0; i < FlightCount; i++)
                {
                    if (flightCode == flightCode_array[i])
                    {
                        booking_index = i;
                        foundFlight = true;
                        break;
                    }
                }

                if (!foundFlight)
                {
                    Console.WriteLine("Flight code not found. Please enter a valid flight code:");
                    flightCode = Console.ReadLine();
                    flag_BookFlight = true;
                }

                } while (flag_BookFlight);
            //to check if there is seat left to book in the flight ...
            if(flightCode == "Default001")
            {
                //to set a seat number for the default flight code ...
                count_seat = 2;
            }
            else
            {
                //to get flight seats number of the flight code ...
                for (int i = 0; i < PassengerCount; i++)
                {
                    if (booking_index == passenger_BookingFlightIndex[i])
                    {
                        count_seat++;
                    }
                }
            }
            //to store booking detalis ...
            //to know if flight code is a default or not and store based on that ...
            int seat = 0;
            if(flightCode == "Default001")
            {
                seat = 5;
            }
            else
            {
                seat = seatsNumber_array[booking_index];
            }
            //store process ...
            if (!flag_BookFlight)
            {
                if(count_seat >= seat)
                {
                    Console.WriteLine("Sorry there is no more seats in this flight!");
                    //return;
                }
                else
                {
                    string passengerBookingID = GenerateBookingID(passengerName);
                    passengerNames_array[PassengerCount] = passengerName;
                    passenger_BookingFlightIndex[PassengerCount] = booking_index;
                    passengerBookingID_array[PassengerCount] = passengerBookingID;
                    //to let the system know that there is one more passenger added ...
                    PassengerCount++;
                    Console.WriteLine("Book flight process done successfully");
                }
            }
        }
        //1. BookFlightList(string passengerName, string flightCode = "Default001") ...
        public static void BookFlightList(string passengerName, string flightCode = "Default001")
        {
            //Book Flight process code ... 
            bool flag_BookFlight;
            int booking_index = 0;
            int count_seat = 0;
            do
            {
                flag_BookFlight = false;

                //to check for null or empty
                if (string.IsNullOrWhiteSpace(passengerName))
                {
                    Console.WriteLine("Passenger name cannot be empty. Please enter a valid passenger name:");
                    passengerName = Console.ReadLine();
                    flag_BookFlight = true;
                    continue;
                }
                //// If using the default flight code, accept it without searching
                //if (flightCode == "Default001")
                //{
                //    booking_index = MAX_FLIGHT - 1;
                //    break; // Exit the loop, default is accepted
                //}

                // Try to find the flight code in the array
                bool foundFlight = false;
                for (int i = 0; i < flightCode_List.Count; i++)
                {
                    if (flightCode == flightCode_List[i])
                    {
                        booking_index = i;
                        foundFlight = true;
                        break;
                    }
                }

                if (!foundFlight)
                {
                    Console.WriteLine("Flight code not found. Please enter a valid flight code:");
                    flightCode = Console.ReadLine();
                    flag_BookFlight = true;
                }

            } while (flag_BookFlight);
            //to check if there is seat left to book in the flight ...
            if (flightCode == "Default001")
            {
                //to set a seat number for the default flight code ...
                count_seat = 2;
            }
            else
            {
                //to get flight seats number of the flight code ...
                for (int i = 0; i < passengerNames_List.Count; i++)
                {
                    if (booking_index == passenger_BookingFlightIndex_List[i])
                    {
                        count_seat++;
                    }
                }
            }
            //to store booking detalis ...
            //to know if flight code is a default or not and store based on that ...
            int seat = 0;
            if (flightCode == "Default001")
            {
                seat = 5;
            }
            else
            {
                seat = seatsNumber_List[booking_index];
            }
            //store process ...
            if (!flag_BookFlight)
            {
                if (count_seat >= seat)
                {
                    Console.WriteLine("Sorry there is no more seats in this flight!");
                    //return;
                }
                else
                {
                    string passengerBookingID = GenerateBookingID(passengerName);
                    passengerNames_List.Add(passengerName);
                    passenger_BookingFlightIndex_List.Add(booking_index);
                    passengerBookingID_List.Add(passengerBookingID);

                    //to let the system know that there is one more passenger added ...
                    //PassengerCount++;
                    Console.WriteLine("Book flight process done successfully");
                }
            }
        }
        //2. ValidateFlightCode(string flightCode)  ...
        public static bool ValidateFlightCode(string flightCode)
        {
            bool isExists = false;
            for (int i = 0; i < FlightCount; i++)
            {
                if (flightCode == flightCode_array[i])
                {
                    isExists = true;
                    break;
                }
            }
            return isExists;
        }
        //2. ValidateFlightCodeList(string flightCode)  ...
        public static bool ValidateFlightCodeList(string flightCode)
        {
            bool isExists = false;
            for (int i = 0; i < flightCode_List.Count; i++)
            {
                if (flightCode == flightCode_List[i])
                {
                    isExists = true;
                    break;
                }
            }
            return isExists;
        }
        //3. GenerateBookingID(string passengerName) ...
        public static string GenerateBookingID(string passengerName)
        {
            string bookingID;
            //to generate a random number ...
            Random random = new Random();
            string randomNumber = random.Next(1, 100).ToString();
            bookingID = passengerName + randomNumber;
            return bookingID;
        }
        //4.DisplayFlightDetails(string code) ...
        public static void DisplayFlightDetails(string code)
        {
            for(int i = 0; i < FlightCount; i++)
            {
                if(code == flightCode_array[i])
                {
                    int countPassengerNumber = 0;
                    for(int j = 0; j < PassengerCount; j++)
                    {
                        if (passenger_BookingFlightIndex[j] == i)
                        {
                            countPassengerNumber++;
                        }
                    }
                    Console.WriteLine("Flight code details:");
                    Console.WriteLine("Flight Code | From City | To City | Departure Time | Duration | Seats | Number of Passenger");
                    Console.WriteLine($"{code} | {fromCity_array[i]} " +
                                      $"| {toCity_array[i]} | {departureTime_array[i]} " +
                                      $"| {duration_array[i]} | {seatsNumber_array[i]} | {countPassengerNumber}");
                    break;
                }
            }
        }
        //4.DisplayFlightDetails_List(string code) ...
        public static void DisplayFlightDetails_List(string code)
        {
            for (int i = 0; i < flightCode_List.Count; i++)
            {
                if (code == flightCode_List[i])
                {
                    int countPassengerNumber = 0;
                    for (int j = 0; j < passengerNames_List.Count; j++)
                    {
                        if (passenger_BookingFlightIndex_List[j] == i)
                        {
                            countPassengerNumber++;
                        }
                    }
                    Console.WriteLine("Flight code details:");
                    Console.WriteLine("Flight Code | From City | To City | Departure Time | Duration | Seats | Number of Passenger");
                    Console.WriteLine($"{code} | {fromCity_List[i]} " +
                                      $"| {toCity_List[i]} | {departureTime_List[i]} " +
                                      $"| {duration_List[i]} | {seatsNumber_List[i]} | {countPassengerNumber}");
                    break;
                }
            }
        }
        //5. SearchBookingsByDestination(string toCity) ...
        public static void SearchBookingsByDestination(string toCity)
        {
            //to get toCity_search index ...
            int index = Array.IndexOf(toCity_array, toCity);
            Console.WriteLine($"Dooking details based {toCity} as destination are:");
            Console.WriteLine("Passenger ID | Passenger Name");
            for (int i = 0; i < PassengerCount; i++)
            {
                if (index == passenger_BookingFlightIndex[i])
                {
                    Console.WriteLine($"{passengerBookingID_array[i]} | {passengerNames_array[i]}");
                }
            }
        }
        //5. SearchBookingsByDestination_List(string toCity) ...
        public static void SearchBookingsByDestination_List(string toCity)
        {
            //to get toCity_search index ...
            int index = toCity_List.IndexOf(toCity);
            Console.WriteLine($"Dooking details based {toCity} as destination are:");
            Console.WriteLine("Passenger ID | Passenger Name");
            for (int i = 0; i < passengerNames_List.Count; i++)
            {
                if (index == passenger_BookingFlightIndex_List[i])
                {
                    Console.WriteLine($"{passengerBookingID_List[i]} | {passengerNames_List[i]}");
                }
            }
        }




        //Function Overloading (4) ........................................................
        //1. CalculateFare(int basePrice, int numTickets)  ...
        public static int CalculateFare(int basePrice, int numTickets)
        {
            int totalFare;
            totalFare = basePrice * numTickets;
            return totalFare;
        }
        //2. CalculateFare(double basePrice, int numTickets) ...
        public static double CalculateFare(double basePrice, int numTickets)
        {
            double totalFare;
            totalFare = basePrice * numTickets;
            return totalFare;
        }
        //3. CalculateFare(int basePrice, int numTickets, int discount) ...
        public static double CalculateFare(int basePrice, int numTickets, int discount)
        {
            double totalFare;
            double discountAmount = (basePrice / 100) * discount;
            totalFare = (basePrice * numTickets) - discountAmount;
            return totalFare;
        }
        //4. CalculateFare(int basePrice, int numTickets, int discount) ...
        public static double CalculateFare(double basePrice, int numTickets, int discount)
        {
            double totalFare;
            double discountAmount = (basePrice / 100) * discount;
            totalFare = (basePrice * numTickets) - discountAmount;
            return totalFare;
        }




        //ADDITIONAL METHODS .............................................................
        //1. To check of the string contains something other than letters like number and empty space(this methos return true or false)....
        static bool IsAlpha(string input)
        {
            return Regex.IsMatch(input, "^[a-zA-Z]+$");
        }
        //2. Input flight departure time ...
        public static DateTime InputFlightDepartureTimeValide()
        {
            DateTime Departure = DateTime.MinValue;
            DateTime final_departure;
            bool ValideDateTime;
            bool flag_departure;
            string string_time;
            do
            {
                flag_departure = false;
                try
                {
                    Console.WriteLine("Flight departure time " +
                                      "in the format (MM/dd/yy HH:mm:ss):");
                    Departure = DateTime.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    departureTime_isEmpty = true;
                }
                //to convert flight departure time from DateTime to string ...
                string_time = Departure.ToString("MM/dd/yy HH:mm:ss");
                //to check if string_time is empty or not ...
                if (departureTime_isEmpty)
                {
                    Console.WriteLine("Flight departure time not vaild it can not be empty");
                    flag_departure = true;
                    if (flag_departure)
                    {
                        try
                        {
                            Console.WriteLine("Please enter flight departure time again" +
                                              "in the format (MM/dd/yy HH:mm:ss):");
                            string_time = Console.ReadLine();
                            departureTime_isEmpty = false;
                        }
                        catch (Exception e)
                        {
                            departureTime_isEmpty = true;
                        }

                    }
                }
                //to check if string_time is in the right formate or not ...
                string format = "MM/dd/yy HH:mm:ss";
                CultureInfo provider = CultureInfo.InvariantCulture;
                ValideDateTime = DateTime.TryParseExact(

                                 string_time,
                                 format,
                                 provider,
                                 DateTimeStyles.None,
                                 out final_departure
                                 );
                // Check if the parsed time is valid and in the future
                if (ValideDateTime)
                {
                    if (final_departure >= DateTime.Now)  // Ensure departure time is >= current time
                    {
                        flag_departure = false;
                    }
                    else
                    {
                        Console.WriteLine("The departure time must be greater than or equal to the current date and time.");
                        flag_departure = true;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid date/time format. Please try again.");
                    flag_departure = true;
                }

            } while (flag_departure);

            return final_departure;
        }
        //3. Validate flight departure time ...
        public static void ValidateFlightDepartureTime_Update(int index)
        {
            DateTime final_departure = InputFlightDepartureTimeValide();

            //call UpdateFlightDeparture() method to update the valide DateTime ...
            UpdateFlightDeparture(ref departureTime_array[index], final_departure);
        }
        //3. Validate flight departure time ...
        public static void ValidateFlightDepartureTime_UpdateList(int index)
        {
            DateTime final_departure = InputFlightDepartureTimeValide();
            /*
             * A List<T> uses an indexer (list[index]) that returns a copy of the element,
             * not a reference. Since it's not a ref return,
             * C# doesn't allow you to pass it as ref or out.
             * How to fix:
             * Copy the value out and assign it back after manipulation:
             * int temp = list[0];
             * SomeMethod(ref temp);
             * list[0] = temp; // Save it back if needed
             */
            DateTime originalDepartureTimeList = departureTime_List[index];

            //call UpdateFlightDeparture() method to update the valide DateTime ...
            UpdateFlightDeparture(ref originalDepartureTimeList, final_departure);

            departureTime_List[index] = originalDepartureTimeList;

        }


    }
}
