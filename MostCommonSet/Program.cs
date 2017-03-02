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
    /// Then:
    /// 0 -
    ///   0 -
    ///     0 -
    ///       ...
    ///         1 = 2 (from 000...1: "pzhen" and "zphen")
    ///     1 -
    ///       ...
    ///         0 = 1 (from 001...0: "xnuch")
    /// 1 -
    ///   2 -
    ///     0 -
    ///       ...
    ///         0 = 1 (from 120...0: "abdnb")
    /// </para>
    /// <para>
    /// Result:
    /// ehnpz
    /// (since pzhen and zphen count as the same word, and appear collectively twice)
    /// </para>
    /// TODO: Error handling
    /// TODO: Unit tests
    /// TODO: Optimize
    /// </remarks>
    class Program
    {
        static void Main(string[] args)
        {
            //Attempt 2:
            //Can take advantage of one thing, here: the number of letters per word is variable,
            //and the number of words in the list is variable, but the number of letters is NOT.
            //Represent each word with an array[26] of ints (nb should be shorts, not expected to
            //have more than ~100 of any given letter). Expensive in memory but faster to process.
            FileStream fs = new FileStream("testdata.txt", FileMode.Open, FileAccess.Read);

            //Load everything into memory
            int[][] data = new int[20000][];
            for (int j = 0; j < 20000; j++)
            {
                //Each new word is an array of 26
                data[j] = new int[26];

                //Note: Line breaks in the input file are mostly cosmetic, this will just read
                //the next five characters regardless
                for (int i=0; i<5; i++)
                {
                    //Consume any whitespace or invalid characters
                    int inByte;
                    do
                        inByte = fs.ReadByte();
                    while (inByte < 97 || inByte > 122);

                    //Save valid characters to array by incrementing at index a=0, b=1 ... z=25
                    char c = (char) inByte;
                    data[j][inByte-97]++;
                }
            }
            
            //Sort the result
            
            //Now look for dupes now that anagrams are handled
            
            //Traverse the heap and print out values that are topCount-ed
            data.printAll(topCount);

            Debug.Write("Never mind."); 
        }

        //This is the bottleneck remove ASAP
        private static String[] SortList(char[][] data)
        {
            String[] newData = new String[20000];
            for(int i=0; i< 20000; i++)
            {
                newData[i] = new String(data[i]);
            }
            Array.Sort(newData);
            return newData;
        }
    }
}
