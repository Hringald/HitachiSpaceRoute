using System.Text;

class CosmicNavigation
{
    static int M, N, A;
    static string[,] map;
    static int startRow;
    static int startCol;
    static int endRow;
    static int endCol;

    static void Main()
    {
        // Displaying the introduction to the user
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Welcome to the **SPACE 2025 Mission**!");
        Console.WriteLine("Congratulations, astronaut! You've been selected to participate in a thrilling mission that could determine the fate of your journey through the vastness of space.");
        Console.WriteLine("Your goal? To navigate your way from your starting position to the safety of the Space Station, all while avoiding dangerous asteroids and obstacles in your path.");
        Console.ResetColor();

        // Brief about the mission
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("### Mission Briefing:");
        Console.WriteLine("As you embark on this cosmic adventure, you'll be using our cutting-edge navigation system to:");
        Console.WriteLine("- **Find the total number of possible paths** that will guide you from your current location to the Space Station.");
        Console.WriteLine("- **Identify the shortest path** and navigate through it, avoiding asteroids along the way.");
        Console.WriteLine("- **Visualize your path** as you go, with every step of your journey marked with a clear and concise map.\n");
        Console.ResetColor();

        // Instructions on how to use the app
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("### How to Use:");
        Console.WriteLine("1. **Set the Stage**: ");
        Console.WriteLine("   You'll be asked to enter the **map dimensions** (rows and columns) and the **cosmic navigation map** itself. The map will include:");
        Console.WriteLine("   - 'S' for your starting position (the space shuttle).");
        Console.WriteLine("   - 'F' for your final destination (the Space Station).");
        Console.WriteLine("   - 'O' for safe, open space.");
        Console.WriteLine("   - 'X' for deadly asteroids that must be avoided at all costs!");
        Console.WriteLine("    In the following format, example:");
        Console.WriteLine("       S O X O O O O");
        Console.WriteLine("       X O O O O X O");
        Console.WriteLine("       X X O X O X O");
        Console.WriteLine("       O X X O O X O");
        Console.WriteLine("       O X X O O O F\n");

        Console.WriteLine("2. **Generate Your Map**: ");
        Console.WriteLine("   You can either enter your own map or let the system automatically generate a random map for you!");
        Console.WriteLine("   If you'd like to generate a map, simply type 'y' when asked, or type 'n' and enter your own map manually.\n");


        Console.WriteLine("3. **Navigate the Stars**: ");
        Console.WriteLine("   The application will process the map and calculate:");
        Console.WriteLine("   - The **total number of paths** from your start to your destination.");
        Console.WriteLine("   - The **shortest path** to ensure your survival, and display it clearly on the map.\n");

        Console.WriteLine("4. **View Your Journey**: ");
        Console.WriteLine("   As you navigate, the program will highlight your journey on the map, marking the safest route with numbers, so you can see exactly how you made it!\n");
        Console.ResetColor();

        // Ask the user to begin the mission
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("### Ready to Begin?");
        Console.WriteLine("Simply enter the map dimensions and the cosmic map details when asked, and let the program guide you safely back to the Space Station.\n");
        Console.WriteLine("Press Enter to start your mission...");
        Console.ResetColor();
        Console.ReadLine();

        // Read inputs
        while (true)
        {
            Console.Write("Enter map rows (M): ");
            string inputRows = Console.ReadLine();

            if (!int.TryParse(inputRows, out int rows))
            {
                Console.WriteLine("M must be a number");
            }

            if (rows < 2 || rows > 100)
            {
                Console.WriteLine("Number of rows must be between 2 and 100.");
            }
            else
            {
                M = rows;
                break;
            }
        }

        while (true)
        {
            Console.Write("Enter map cols (N): ");
            string inputCols = Console.ReadLine();

            if (!int.TryParse(inputCols, out int Cols))
            {
                Console.WriteLine("N must be a number");
            }

            if (Cols < 2 || Cols > 100)
            {
                Console.WriteLine("Number of cols must be between 2 and 100.");
            }
            else
            {
                N = Cols;
                break;
            }
        }

        string generationFlag = "";

        while (true)
        {
            Console.WriteLine("Would you like to generate a random map? (Y/N)");
            generationFlag = Console.ReadLine().ToLower();

            if (generationFlag == "y" || generationFlag == "n")
            {
                break;
            }
            else
            {
                Console.WriteLine("Answer must be y or n.");
            }
        }

        if (generationFlag == "y")
        {
            while (true)
            {
                Console.Write("Enter asteroid count (N): ");
                string inputAsteroidCount = Console.ReadLine();

                if (!int.TryParse(inputAsteroidCount, out int asteroidCount))
                {
                    Console.WriteLine("Asteroid count must be a number");
                }

                if (asteroidCount <= 0 || asteroidCount >= M * N - 2) // -2 to leave space for "S" and "F"
                {
                    Console.WriteLine($"Asteroid count must be between 0 and {(M * N - 2)}.");
                }
                else
                {
                    A = asteroidCount;
                    break;
                }
            }

            map = MapGenerator.GenerateMap(M, N, A, out startRow, out startCol, out endRow, out endCol);
            MapGenerator.PrintMap(map);
        }
        else if (generationFlag == "n")
        {
            Console.WriteLine("Enter the map:");
            map = MapGenerator.GenerateMapFromInput(M, N, out startRow, out startCol, out endRow, out endCol);
        }

        //Count total possible paths
        int totalPaths = Route.CountRoutes(map, M, N, startRow, startCol, endRow, endCol);
        Console.WriteLine($"Number of possible paths: {totalPaths}");

        //Find the shortest path
        var shortestPath = Route.FindShortestRoute(map, M, N, startRow, startCol, endRow, endCol);
        int shortestPathLength = 0;
        if (shortestPath.Count > 1)
        {
            shortestPathLength = shortestPath.Count - 1;
        }

        string bestRoute = Route.GetBestRouteString(map, shortestPath);
        if (shortestPathLength <= 0)
        {
            Console.WriteLine("There is no safe route.");
        }
        else
        {
            Console.WriteLine($"Shortest path length: {shortestPathLength}");
            Console.WriteLine("The best route is:");
            Console.WriteLine(bestRoute);
        }

        string reportFlag = "";
        while (true)
        {
            Console.WriteLine("Would you like to send the report via email? (Y/N)");
            reportFlag = Console.ReadLine().ToLower();

            if (reportFlag == "y" || reportFlag == "n")
            {
                break;
            }
            else
            {
                Console.WriteLine("Answer must be y or n.");
            }
        }

        if (reportFlag == "y")
        {
            CSV.WriteCsvFile(bestRoute);

            Console.WriteLine($"Please enter your email:");
            string senderEmail = Console.ReadLine();

            Console.WriteLine($"Please enter your password:");
            string senderPassword = Console.ReadLine();

            Console.WriteLine($"Please the email you want to send the best route data:");
            string recieverEmail = Console.ReadLine();

            ForecastEmail.EmailSend(senderEmail, senderPassword, recieverEmail);
        }

            Console.WriteLine("Thank you for using this application! :)");
    }
}