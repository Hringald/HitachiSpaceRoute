using System.Text;

public class Route
{

    static int[] rowDirections = { -1, 1, 0, 0 };
    static int[] colDirections = { 0, 0, -1, 1 };

    public Route()
    {

    }


    // Count the number of paths using Breadth-First Search algorithm
    public static int CountRoutes(string[,] grid, int M, int N, int row, int col, int destRow, int destCol)
    {
        if (row < 0 || row >= M || col < 0 || col >= N || grid[row, col] == "X")
            return 0;

        if (row == destRow && col == destCol)
            return 1;

        if (grid[row, col] != "S")
        {
            grid[row, col] = "X"; // Mark as visited
        }

        int totalPaths = 0;

        for (int i = 0; i < 4; i++)
        {
            int newRow = row + rowDirections[i];
            int newCol = col + colDirections[i];

            totalPaths += CountRoutes(grid, M, N, newRow, newCol, destRow, destCol);
        }

        if (grid[row, col] != "S")
        {
            grid[row, col] = "O"; // Unmark as visited
        }

        return totalPaths;
    }

    // Find the shortest path using Breadth-First Search algorithm
    public static List<(int, int)> FindShortestRoute(string[,] grid, int M, int N, int startRow, int startCol, int endRow, int endCol)
    {
        var queue = new Queue<(int, int)>();
        var visited = new bool[M, N];
        var parent = new (int, int)[M, N];
        var path = new List<(int, int)>();

        visited[startRow, startCol] = true;
        queue.Enqueue((startRow, startCol));
        parent[startRow, startCol] = (-1, -1); // No parent for the start

        while (queue.Count > 0)
        {
            var (row, col) = queue.Dequeue();

            if (row == endRow && col == endCol)
            {
                // Trace the Route
                while (row != -1 && col != -1)
                {
                    path.Add((row, col));
                    var (prevRow, prevCol) = parent[row, col];
                    row = prevRow;
                    col = prevCol;
                }
                path.Reverse();
                return path;
            }

            for (int i = 0; i < 4; i++)
            {
                int newRow = row + rowDirections[i];
                int newCol = col + colDirections[i];

                if (newRow >= 0 && newRow < M && newCol >= 0 && newCol < N && !visited[newRow, newCol] && grid[newRow, newCol] != "X")
                {
                    visited[newRow, newCol] = true;
                    queue.Enqueue((newRow, newCol));
                    parent[newRow, newCol] = (row, col);
                }
            }
        }
        return path;
    }

    // Display the grid with the shortest path
    public static string GetBestRouteString(string[,] grid, List<(int, int)> path)
    {
        StringBuilder sb = new StringBuilder();

        int pathCount = 1;

        foreach (var (row, col) in path)
        {
            if (grid[row, col] != "S")
                grid[row, col] = pathCount++.ToString(); // Replace with path number

        }

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                sb.Append(grid[i, j] + " ");
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }
}
