namespace Distances;

public static class Functions
{
    /// <summary>
    /// Calculate the Levenstein distance between given strings.
    /// </summary>
    /// <param name="input1">First string to compare.</param>
    /// <param name="input2">Second string to compare.</param>
    /// <returns>The Levenstein distance between given strings.</returns>
    public static int Levenstein(string input1, string input2, Action<string>? lineWriter = null, Action? styleRow = null, Action? resetStyleRow = null)
    {
        // Create an array
        var rowsCount = input1.Length + 1;
        var colsCount = input2.Length + 1;
        var d = new int[rowsCount, colsCount];

        // Assign indexes into first row and column
        Enumerable.Range(0, colsCount).ToList().ForEach(i => d[0, i] = i);
        Enumerable.Range(0, rowsCount).ToList().ForEach(i => d[i, 0] = i);

        // Calculate the distance
        for (var i = 1; i < rowsCount; ++i)
        {
            for (var j = 1; j < colsCount; ++j)
            {
                // Check if any operation is needed
                var cost = input1[i - 1] == input2[j - 1] ? 0 : 1;
                // Get the lowest cost of operation
                d[i, j] = new int[]
                {
                    d[i - 1, j] + 1, // deletion
                    d[i, j - 1] + 1, // insertion
                    d[i - 1, j - 1] + cost // substitution
                }.Min();
            }

            if (lineWriter != null)
            {
                TableWriter.Print(d, lineWriter, currentRow: i, styleRow: styleRow, resetStyleRow: resetStyleRow);
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
    // ReSharper disable once InconsistentNaming
    public static int LCS(string input1, string input2)
    {
        // Create an array
        var rowsCount = input1.Length + 1;
        var colsCount = input2.Length + 1;
        var d = new int[rowsCount, colsCount];

        for (var i = 1; i < rowsCount; ++i)
        {
            for (var j = 1; j < colsCount; ++j)
            {
                // Check if characters are the same
                d[i, j] = input1[i - 1] == input2[j - 1]
                    ? d[i - 1, j - 1] + 1 // If yes increment the length
                    : Math.Max(d[i, j - 1], d[i - 1, j]); // If no get the longer sequence.
            }
        }

        return d[rowsCount - 1, colsCount - 1];
    }
}