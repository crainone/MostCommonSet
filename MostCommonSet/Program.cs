using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace MostCommonSet
{
    /// <summary>
    /// If you can see this, this project succeeded!
    /// </summary>
    /// <remarks>
    /// <para>
    /// This program reads a file of 5-character "words" and prints which set of characters appears
    /// most often. The order of the characters doesn't matter.
    /// </para>
    /// <para>
    /// Since the real point of this is the documentation, if the above summary appears in pages,
    /// this project is a success. Still, this should work as specified and be as idiot-proof as
    /// possible.
    /// </para>
    /// <para>
    /// Sample input:
    /// <file>
    /// abdnb
    /// xnuch
    /// pzhen
    /// zphen
    /// </file>
    /// </para>
    /// <para>
    /// Representation in memory:
    /// <table>
    /// <tr><th>a</th><th>b</th><th>c</th><th>...</th><th>z</th></tr>
    /// <tr><td>1</td><td>2</td><td>0</td><td></td><td>0</td></tr>
    /// <tr><td>0</td><td>0</td><td>1</td><td></td><td>0</td></tr>
    /// <tr><td>0</td><td>0</td><td>0</td><td></td><td>1</td></tr>
    /// <tr><td>0</td><td>0</td><td>0</td><td></td><td>1</td></tr>
    /// <tr></tr>
    /// </table>
    /// </para>
    /// <para>
    /// Result:
    /// ehnpz
    /// (since pzhen and zphen count as the same word, and appear collectively twice)
    /// </para>
    /// TODO: Error handling
    /// TODO: Unit tests
    /// </remarks>
    class Program
    {
        static void Main(string[] args)
        {
            FileStream fs = new FileStream("testdata.txt", FileMode.Open, FileAccess.Read);

            //Load everything into memory
            AnalyzedWord[] data = new AnalyzedWord[20000];
            ReadIntoMemory(fs, data);

            //Now look for dupes now that anagrams are handled
            Dictionary<AnalyzedWord, int> countedAnagrams = data
                .GroupBy(x => x, new AnalyzedWordComparer())
                .ToDictionary(x => x.Key, x => x.Count());

            //Find all the max values and print them
            int commonCount = countedAnagrams.Max(x => x.Value);
            Debug.WriteLine("{0} instances of most common words", commonCount);

            String[] mostCommon = countedAnagrams
                .Where(x => x.Value == commonCount)
                .Select(x => x.Key.ToString()).ToArray();

            for (int i = 0; i < mostCommon.Length; i++)
            {
                Debug.WriteLine(mostCommon[i]);
            }
            Debug.WriteLine("Done.");
        }

        private static void ReadIntoMemory(FileStream fs, AnalyzedWord[] data)
        {
            for (int j = 0; j < 20000; j++)
            {
                //Save aggregate of each new word
                data[j] = new AnalyzedWord();

                //Note: Line breaks in the input file are mostly cosmetic, this will just read
                //the next five characters regardless
                for (int i = 0; i < 5; i++)
                {
                    //Consume any whitespace or invalid characters
                    int inByte;
                    do
                        inByte = fs.ReadByte();
                    while (inByte < 97 || inByte > 122);

                    //Save valid characters to array by incrementing at index a=0, b=1 ... z=25
                    data[j].add(inByte);
                }
            }
        }
    }
}
