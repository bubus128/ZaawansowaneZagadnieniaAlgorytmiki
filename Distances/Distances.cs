namespace Distances;

public static class Distances
{
    private static string GetInputUntilValid(string inputName)
    {
        while (true)
        {
            Console.Write($"{inputName}:");
            var input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
        }
    }
    
    public static void Main()
    {
        // Get inputs
        var input1 = GetInputUntilValid("Input1");
        var input2 = GetInputUntilValid("Input2");

        // Get the Levenstein distance
        Console.WriteLine($"{nameof(Functions.Levenstein)}: {Functions.Levenstein(input1, input2)}");

        // Get the longest common subsequence length
        Console.WriteLine($"{nameof(Functions.LCS)}: {Functions.LCS(input1, input2)}");
    }
}