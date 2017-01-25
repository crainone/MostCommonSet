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

            //Don't need to actually sort, just count. Make a heap of the strings and increment when you reach 5 deep.
            //Keep track of the highest value that turns up
            int topCount = 0;
            Heap newData = new Heap();
            for(int i=0; i < 20000; i++)
            {
                int currentCount = newData.put(data[i]);
                if(currentCount > topCount)
                {
                    topCount = currentCount;
                } 
            }

            //Traverse the heap and print out values that are topCount-ed
            newData.printAll(topCount);

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
