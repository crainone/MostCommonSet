using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MostCommonSet
{
    /// <summary>
    /// Represents one word.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Can take advantage of one thing, here: the number of letters per word is variable,
    /// and the number of words in the list is variable, but the number of letters is NOT.
    /// Represent each word with an array[26] of ints (nb should be shorts, not expected to
    /// have more than ~100 of any given letter). Expensive in memory but faster to process.
    /// </para>
    /// <para>
    /// TODO: This could be flexible, storing short words as strings and long words as
    /// letter-count arrays.
    /// </para>
    /// </remarks>
    class AnalyzedWord
    {
        /// <summary>
        /// letterCounts[0] equals how many "a"s appear in this word,
        /// letterCounts[1] equals how many "b"s appear in this word,
        /// ...
        /// letterCounts[25] equals how many "z"s appear in this word.
        /// Note that letters are not included in this word until they are explicitly added by
        /// .add(letter).
        /// </summary>
        public int[] letterCounts = new int[26];

        public AnalyzedWord() { }

        /// <summary>
        /// Add a letter to this word.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Given the ASCII value of a letter, increment the value at the appropriate index. If for
        /// some reason the letter can't be represented as a number 0-25, it gets skipped.
        /// </para>
        /// </remarks>
        /// <param name="letter">
        /// An ASCII value, to be converted to A1Z26 (well, A0Z25) and added to the letterCounts.
        /// </param>
        public void add(int letter)
        {
            try
            {
                letterCounts[ASCIIToIndex(letter)]++;
            }
            catch (IndexOutOfRangeException e)
            {
                //More efficient to try and fail than to check every time. Just let invalid
                //characters go.
            }
        }

        /// <summary>
        /// Convert ASCII value to index of array.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Easy to do - subtract 97 from the value.
        /// </para>
        /// <para>
        /// TODO: allow uppercase letters.
        /// </para>
        /// </remarks>
        /// <param name="letter">
        /// ASCII value from 97 to 122 (a-z)
        /// </param>
        /// <returns>
        /// 0 for a through 25 for z, but may return outside that range when given other characters.
        /// </returns>
        private int ASCIIToIndex(int letter)
        {
            return letter - 97;
        }

        /// <summary>
        /// Convert index of array to ASCII value.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Should always be inverse of ASCIIToIndex.
        /// </para>
        /// <para>
        /// TODO: allow uppercase letters.
        /// </para>
        /// </remarks>
        /// <param name="index">
        /// Array index from 0 to 25
        /// </param>
        /// <returns>
        /// a for 0 through z for 25, but may return another character when given some outside index.
        /// </returns>
        private int IndexToASCII(int index)
        {
            return index + 97;
        }

        /// <summary>
        /// Equal if all elements in the array letterCounts are equal.
        /// </summary>
        /// <param name="other">
        /// The other AnalyzedWord to check against this one.
        /// </param>
        /// <returns>
        /// True if the same number of letters appear in both words.
        /// </returns>
        public bool Equals(AnalyzedWord other)
        {
            return letterCounts.SequenceEqual(other.letterCounts);
        }

        /// <summary>
        /// Hash based on letterCounts.
       /// </summary>
        /// <returns>
        /// hash value
        /// </returns>
        public override int GetHashCode()
        {
            int hc = letterCounts.Length;
            for (int i = 0; i < letterCounts.Length; ++i)
            {
                hc = unchecked(hc * 314159 + letterCounts[i]);
            }
            return hc;
        }

        /// <summary>
        /// Converts letter counts back to normal string.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Letter order isn't preserved, but the string that represents this word and its
        /// anagrams is recreated, with letters in alphabetical order.
        /// </para>
        /// <para>
        /// For example, "diwge"/"gweid" are returned as "degiw".
        /// </para>
        /// </remarks>
        /// <returns>
        /// Word string.
        /// </returns>
        public override String ToString()
        {
            string word = "";
            for (int i = 0; i < letterCounts.Length; i++)
            {
                char appendedChar = (char) IndexToASCII(i);
                for (int j = 0; j < letterCounts[i]; j++)
                {
                    word += appendedChar;
                }
            }
            return word;
        }
    }

    /// <summary>
    /// Wrapper for comparison functions in AnalyzedWord.
    /// </summary>
    /// <see cref="AnalyzedWord"/>
    class AnalyzedWordComparer : IEqualityComparer<AnalyzedWord>
    {
        /// <summary>
        /// Return true if the two words are the same, or anagrams.
        /// </summary>
        /// <param name="x">Word 1</param>
        /// <param name="y">Word 2</param>
        /// <returns>x.Equals(y)</returns>
        /// <see cref="AnalyzedWord.Equals"/>
        public bool Equals(AnalyzedWord x, AnalyzedWord y)
        {
            return x.Equals(y);
        }

        /// <summary>
        /// Return hashcode of the given word.
        /// </summary>
        /// <param name="obj">Word</param>
        /// <returns>obj.GetHashCode()</returns>
        /// <see cref="AnalyzedWord.GetHashCode"/> 
        public int GetHashCode(AnalyzedWord obj)
        {
            return obj.GetHashCode();
        }
    }
}
