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
                // Continue if the letter is not preset in pattern
                if (!chars.Contains(text[i]))
                {
                    continue;
                }
                // If letter is preset in the pattern, then:
                // 1. Find the indexes of the letter in the pattern
                // 2. For each index, check if the pattern matches the substring of the main string when the letters are aligned
                int indexOfPattren = chars.Select((letter, index) => new { letter, index })
                    .Where(item => item.letter == text[i] && item.index <= i && i + pattern.Length - item.index <= text.Length).ToList()
                    .FirstOrDefault(item => text.Substring(i - item.index, pattern.Length) == pattern)?.index ?? -1;
                if (indexOfPattren >= 0)
                {
                    return i - indexOfPattren;
                }
            }
            return -1;
        }
    }
}
