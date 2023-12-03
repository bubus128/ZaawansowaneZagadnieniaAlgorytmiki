using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Substrings
{
    public static class Functions
    {
        /// <summary>
        /// Check if the pattern exists in the text.
        /// </summary>
        /// <param name="text">Text in which the pattern will be searched.</param>
        /// <param name="pattern">Substring to find.</param>
        /// <returns>Index of the beginning of the first substring if found; otherwise, -1</returns>
        public static int BoyerMoore(string text, string pattern)
        {
            // Create an array with pattern chars
            char[] chars = pattern.ToCharArray();

            // Iterate through the string, jumping by the lenght of serarched pattern
            for (int i = 0; i < text.Length; i += pattern.Length)
            {
                Console.WriteLine($"Checking position {i}:");

                // Continue if the letter is not preset in pattern
                if (!chars.Contains(text[i]))
                {
                    Console.WriteLine($"Character '{text[i]}' is not present in the pattern. Skipping.");
                    continue;
                }
                // If letter is preset in the pattern, then:
                // 1. Find the indexes of the letter in the pattern
                // 2. For each index, check if the pattern matches the substring of the main string when the letters are aligned
                int indexOfPattern = chars.Select((letter, index) => new { letter, index })
                    .Where(item => item.letter == text[i] && item.index <= i && i + pattern.Length - item.index <= text.Length).ToList()
                    .FirstOrDefault(item => text.Substring(i - item.index, pattern.Length) == pattern)?.index ?? -1;
                Console.WriteLine($"Substring comparison at position {i - indexOfPattern}: '{text.Substring(i - indexOfPattern, pattern.Length)}'");
                if (indexOfPattern >= 0)
                {
                    return i - indexOfPattern;
                }
            }
            return -1;
        }


        /// <summary>
        /// Check if the pattern exists in the text.
        /// </summary>
        /// <param name="text">Text in which the pattern will be searched.</param>
        /// <param name="pattern">Substring to find.</param>
        /// <returns>Index of the beginning of the first substring if found; otherwise, -1</returns>
        public static int KunthMorrisPratt(string text, string pattern)
        {
            // Calculate the Longest Proper Prefix which is also a Suffix (LPS) array for the pattern
            int[] lps = CalculateLPSArray(pattern);

            int textIndex = 0;
            int patternIndex = 0;

            while (textIndex < text.Length)
            {
                Console.WriteLine($"Comparing text[{textIndex}] and pattern[{patternIndex}]");

                // If characters match, move both indices forward
                if (pattern[patternIndex] == text[textIndex])
                {
                    patternIndex++;
                    textIndex++;
                }

                // If the entire pattern is found in the text, return the starting index
                if (patternIndex == pattern.Length)
                {
                    return textIndex - patternIndex;
                }
                // If characters mismatch and patternIndex is not at the beginning, update patternIndex using LPS array
                else if (textIndex < text.Length && pattern[patternIndex] != text[textIndex])
                {
                    if (patternIndex != 0)
                    {
                        Console.WriteLine("Mismatch occurred. Updating pattern index using LPS array.");
                        patternIndex = lps[patternIndex - 1];
                    }
                    // If patternIndex is at the beginning, move to the next character in the text
                    else
                    {
                        Console.WriteLine("Mismatch occurred. Moving to the next character in the text.");
                        textIndex++;
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// Calculates the Longest Proper Prefix which is also a Suffix (LPS) array for the given pattern.
        /// </summary>
        /// <param name="pattern">The pattern for which the LPS array is calculated.</param>
        /// <returns>An array representing the LPS array for the given pattern.</returns>
        static int[] CalculateLPSArray(string pattern)
        {
            int[] lps = new int[pattern.Length];
            int len = 0;
            int i = 1;

            while (i < pattern.Length)
            {
                if (pattern[i] == pattern[len])
                {
                    len++;
                    lps[i] = len;
                    i++;
                }
                else
                {
                    if (len != 0)
                    {
                        // If there is a mismatch, update len using LPS array
                        len = lps[len - 1];
                    }
                    else
                    {
                        // If len is already 0, set LPS value to 0 and move to the next character
                        lps[i] = 0;
                        i++;
                    }
                }
            }

            return lps;
        }
    }
}
