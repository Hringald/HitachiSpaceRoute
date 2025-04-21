using System;
using System.Text;

public class MapGenerator
{
    static string[,] map;
    static Random random = new Random();
    public MapGenerator()
    {

    }
    public static string[,] GenerateMapFromInput(int rows, int cols, out int startRow, out int startCol, out int finishRow, out int finishCol)
    {
        startRow = 0;
        startCol = 0;
        finishRow = 0;
        finishCol = 0;

        List<string> validInput = new List<string>() { "S", "O", "F", "X" };

        //Generate map from input
        while (true)
        {
            bool isMapFormatCorrect = true;
            map = new string[rows, cols];
            int fCount = 0;
            int sCount = 0;

            for (int i = 0; i < rows; i++)
            {
                var line = Console.ReadLine().Split();
                for (int j = 0; j < cols; j++)

                {
                    try
                    {
                        map[i, j] = line[j][0].ToString();
                    }
                    catch (Exception)
                    {
                        isMapFormatCorrect = false;
                        break;
                    }

                    if (line[j][0].ToString() == "F")
                    {
                        fCount++;
                    }

                    if (line[j][0].ToString() == "S")
                    {
                        sCount++;
                    }

                    if (!validInput.Contains(line[j][0].ToString()))
                    {
                        isMapFormatCorrect = false;
                        break;
                    }

                    if (fCount > 1 || sCount > 1)
                    {
                        isMapFormatCorrect = false;
                        break;
                    }

                    if (map[i, j] == "S")
                    {
                        startRow = i;
                        startCol = j;
                    }
                    if (map[i, j] == "F")
                    {
                        finishRow = i;
                        finishCol = j;
                    }
                }

                if (!isMapFormatCorrect)
                {
                    break;
                }
            }

            if (isMapFormatCorrect)
            {
                break;
            }
            else
            {
                Console.WriteLine("The provided map is not in the right format, please try again");
            }

        }

        Console.WriteLine();
        return map;
    }
    public static string[,] GenerateMap(int rows, int cols, int asteroidCount, out int startRow, out int startCol, out int finishRow, out int finishCol)
    {

        // Initialize the map with "O" (open space)
        string[,] map = new string[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                map[i, j] = "O";  // Open space
            }
        }

        // Randomly select positions for 'S' (start) and 'F' (finish), ensuring they are not the same
        startRow = random.Next(rows);
        startCol = random.Next(cols);

        do
        {
            finishRow = random.Next(rows);
            finishCol = random.Next(cols);
        }
        while (startRow == finishRow && startCol == finishCol);  // Ensure 'S' and 'F' are not the same

        map[startRow, startCol] = "S";  // Place the start position
        map[finishRow, finishCol] = "F";  // Place the finish position

        // Randomly place 'X' (asteroids) in the map
        int asteroidsPlaced = 0;
        while (asteroidsPlaced < asteroidCount)
        {
            int row = random.Next(rows);
            int col = random.Next(cols);

            // Ensure the start ('S') and finish ('F') positions are not overwritten
            if (map[row, col] == "O" && !(row == startRow && col == startCol) && !(row == finishRow && col == finishCol))
            {
                map[row, col] = "X";  // Place asteroid
                asteroidsPlaced++;
            }
        }

        return map;
    }

    // Method to print the map to the console
    public static void PrintMap(string[,] map)
    {
        int rows = map.GetLength(0);
        int cols = map.GetLength(1);

        Console.WriteLine("The generated map is shown below:");
        // Check if the map is null
        if (map == null)
        {
            Console.WriteLine("Error: The map is null.");
            return;
        }

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(map[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}
