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
    /// TODO: Can I use a TODO tag?
    /// TODO: Error handling
    /// TODO: Unit tests
    /// TODO: Optimize
    /// </remarks>
    class Program
    {
        static void Main(string[] args)
        {
            //Attempt 1: Brute force (use this as benchmark later)
            FileStream fs = new FileStream("testdata.txt", FileMode.Open, FileAccess.Read);

            //Load everything into memory
            char[][] data = new char[20000][];
            for (int j = 0; j < 20000; j++)
            {
                //Each new word is an array of 5
                data[j] = new char[5];
                for (int i=0; i<5; i++)
                {
                    //Consume any whitespace or invalid characters
                    int inByte;
                    do
                        inByte = fs.ReadByte();
                    while (inByte < 97 || inByte > 122);

                    //Save valid characters to array
                    //Note: Line breaks in the input file are mostly cosmetic, this will just read
                    //the next five characters regardless
                    char c = (char) inByte;
                    data[j][i] = c;
                }
            }

            //Since we don't care about letter order, sort them
            for (int j = 0; j < 20000; j++)
            {
                SortWord(data[j]);
            }
            //Sort list to get ready to count duplicates
            String[] newData = SortList(data);

            //List of top strings, expected to be pretty short (maybe one?)
            List<String> output = new List<String>();
            output.Add(newData[0]); //At start, top string is only string
            int topCount = 1; //How many of the top string(s?) there are, tied
            int currentCount = 1; //How many so far of the string under consideration
            String currentString = newData[0];
            for(int i=1; i < 20000; i++)
            {
                Debug.Write(topCount + " each: ");
                foreach(String s in output)
                {
                    Debug.Write(s + "\t");
                }
                Debug.WriteLine("");
                if(newData[i].Equals(currentString))
                {
                    currentCount++;
                    if(currentCount == topCount)
                    {
                        output.Add(currentString);
                    }
                    if(currentCount > topCount)
                    {
                        output.Clear();
                        output.Add(currentString);
                        topCount = currentCount;
                    }
                }
                else
                {
                    currentString = newData[i];
                    currentCount = 1;
                }

            }
            Debug.Write("Never mind."); 
        }

        /// <summary>
        /// Sort the word in alphabetical order
        /// </summary>
        /// <remarks>
        /// TODO: Is this pass-by-reference?
        /// </remarks>
        /// <param name="data">One rows of 5 characters from the input data.</param>
        private static void SortWord(char[] data)
        {
            Array.Sort(data);
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
