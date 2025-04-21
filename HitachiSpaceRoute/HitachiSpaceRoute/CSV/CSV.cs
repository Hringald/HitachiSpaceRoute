using System.Text;

public class CSV
{
    public CSV()
    {

    }
    public static void WriteCsvFile(string BestRoute)
    {

        StringBuilder FilePath = new StringBuilder();
        FilePath.Append(@"\Output\");

        if (!Directory.Exists(FilePath.ToString()))
        {
            Directory.CreateDirectory(FilePath.ToString());
        }
        FilePath.Append(@"SpaceMission.csv");

        try
        {
            File.WriteAllText(FilePath.ToString(), BestRoute);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Data could not be written to the CSV file.");
            return;
        }
    }
}