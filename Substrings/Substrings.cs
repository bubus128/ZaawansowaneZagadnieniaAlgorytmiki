namespace Substrings
{
    public class Substrings
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
            var text = GetInputUntilValid("Text");
            var substring = GetInputUntilValid("Substring");

            // Find the substring using Boyer Moore algorithm
            int boyerMooreIndex = Functions.BoyerMoore(text, substring);
            Console.WriteLine(boyerMooreIndex > -1 ?
                $"Substring found at index: {boyerMooreIndex}" :
                "Substring not found");
        }
    }
}