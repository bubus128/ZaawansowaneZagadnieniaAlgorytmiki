
using System;
using System.Diagnostics;
public static class Distances
{
    public static void Main()
    {
        // Get inputs
        string input1 = Console.ReadLine();
        string input2 = Console.ReadLine();

        // Get the Levenstein distance
        Console.WriteLine($"Levenstein: {Levenstein(input1, input2)}");

        // Get the longest common subsequence length
        Console.WriteLine($"LCS: {Lcs(input1, input2)}");
    }

    /// <summary>
    /// Calculate the Levenstein distance between given strings.
    /// </summary>
    /// <param name="input1">First string to compare.</param>
    /// <param name="input2">Second string to compare.</param>
    /// <returns>The Levenstein distance between given strings.</returns>
    public static int Levenstein(string input1, string input2)
    {
        // Create an array
        int rowsCount = input1.Length + 1;
        int colsCount = input2.Length + 1;
        int[,] d = new int[rowsCount, colsCount];

        // Assign indexes into first row and column
        Enumerable.Range(0, colsCount).ToList().ForEach(i => d[0, i] = i);
        Enumerable.Range(0, rowsCount).ToList().ForEach(i => d[i, 0] = i);

        for (int i = 1; i < rowsCount; i++)
        {
            for (int j = 1; j < colsCount; j++)
            {
                int cost = input1[i - 1] == input2[j - 1] ? 0 : 1;
                d[i, j] = new int[] { d[i - 1, j] + 1, d[i, j - 1] + 1, d[i - 1, j - 1] + cost }.Min();
            }
        }

        return d[rowsCount - 1, colsCount - 1];
    }

    /// <summary>
    /// Calculate the length of the longest common subsequence.
    /// </summary>
    /// <param name="input1">First string to compare.</param>
    /// <param name="input2">Second string to compare.</param>
    /// <returns>Length of the longest common subsequence.</returns>
    public static int Lcs(string input1, string input2)
    {
        // Create an array
        int rowsCount = input1.Length + 1;
        int colsCount = input2.Length + 1;
        int[,] d = new int[rowsCount, colsCount];

        for (int i = 1; i < rowsCount; i++)
        {
            for (int j = 1; j < colsCount; j++)
            {
                d[i,j] = input1[i - 1] == input2[j - 1] ? d[i-1,j-1]+1 : Math.Max(d[i,j-1],d[i-1,j]);
            }
        } 
        return d[rowsCount-1, colsCount-1];
    }
}