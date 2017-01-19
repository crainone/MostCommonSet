using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenerateSets
{
    /// <summary>
    /// Writes a file containing 10000 5-character "words", one on each line.
    /// </summary>
    /// <remarks>
    /// First attempt using proper C# documentation.
    /// The file just goes to the working directory.
    /// TODO: When integrating this with MostCommonSet, make sure this output and that input are compatible
    /// </remarks>
    class Program
    {
        /// <summary>
        /// RNG to be used by the entire program.
        /// </summary>
        private static Random rnd = new Random();

        /// <summary>
        /// Writes 10000 instances of <see cref="Program.GetWord">GetWord</see> to a file.
        /// </summary>
        /// <remarks>
        /// Technically also writes an extra newline to the file, but we don't care. Only in
        /// the empty or one-entry case will that be the most common. Adds less overhead than
        /// trying to remove it.
        /// TODO: Worth it to generate them all into memory and write at once?
        /// TODO: Accept N as an argument and generate N instances instead.
        /// TODO: Accept M as an argument and generate M-letter words.
        /// </remarks>
        /// <param name="args">Not used.</param>
        static void Main(string[] args)
        {
            //Write to file
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(@"testdata.txt", true))
            {
                for (int i = 0; i < 10000; i++)
                {
                    //Get a word
                    string word = GetWord();
                    file.WriteLine(word);
                }
            }
        }


        /// <summary>
        /// Generate a random set of 5 characters (AKA a "word").
        /// </summary>
        /// <remarks>
        /// TODO: Can this be optimized?
        /// TODO: Accept N as an argument and generate N-letter words.
        /// </remarks>
        /// <returns>The 5 characters as a string.</returns>
        static private String GetWord()
        {
            String returnString = "";
            for (int i = 0; i < 5; i++)
            {
                returnString += GetCharacter();
            }
            return returnString;
        }


        /// <summary>
        /// Generate one random letter.
        /// </summary>
        /// <remarks>
        /// Generates one random number between 97 and 122, inclusive, and returns that as a character.
        /// </remarks>
        /// <returns>One character from a-z (lowercase)</returns>
        static private Char GetCharacter()
        {
            return (Char) rnd.Next(97, 123);
        }
    }
}
