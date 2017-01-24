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
                data[j] = new char[5];
                for (int i=0; i<5; i++)
                {
                    data[j][i] = (char) fs.ReadByte();
                }
            }

            for (int j = 0; j < 20000; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    Debug.Write(data[j][i]);
                }
                Debug.WriteLine("");
            }
            Debug.Write("Never mind.");
        }

        /// <summary>
        /// Given the entire chunk of data, rearranges the row specified by rowNumber to alphabetic order. 
        /// </summary>
        /// <remarks>
        /// TODO: Is this pass-by-reference?
        /// </remarks>
        /// <param name="data">All input data, 20000 rows of 5 characters.</param>
        /// <param name="rowNum">Which row to sort this time.</param>
        private static void SortWord(char[,] data, int rowNum)
        {
            
        }
    }
}
