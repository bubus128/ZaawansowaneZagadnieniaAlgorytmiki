using System.Text;

namespace Distances;

public static class TableWriter
{
    private static void PrintHorizontalTableLine(StringBuilder sb, int width, Action<string> lineWriter)
    {
        sb.Clear()
            .Append(new string('-', width + 1));
        lineWriter(sb.ToString());
        sb.Clear();
    }

    public static IEnumerable<int> EnumerateMultidimensionalTable(int[,] table)
    {
        foreach (var i in table)
        {
            yield return i;
        }
    }

    public static void Print(int[,] table, Action<string> lineWriter, int? currentRow = null, int? currentCol = null, Action? styleRow = null, Action? resetStyleRow = null)
    {
        var numberOfCharacters = EnumerateMultidimensionalTable(table).Max().ToString().Length;

        var sb = new StringBuilder();
        var rowsLength = table.GetLength(0);
        var columnsLength = table.GetLength(1);

        PrintHorizontalTableLine(sb, columnsLength * (numberOfCharacters + 1), lineWriter);
        
        for (var row = 0; row < rowsLength; ++row)
        {
            if (currentRow != null && styleRow != null && resetStyleRow != null && currentRow == row)
            {
                styleRow();
            }
            
            sb.Clear().Append(' ');
            for (var col = 0; col < columnsLength; ++col)
            {
                sb.Append($"{table[row, col].ToString().PadLeft(numberOfCharacters)} ");
            }

            lineWriter(sb.ToString());

            resetStyleRow?.Invoke();
        }
        
        PrintHorizontalTableLine(sb, columnsLength * (numberOfCharacters + 1), lineWriter);
        lineWriter("");
    }
}