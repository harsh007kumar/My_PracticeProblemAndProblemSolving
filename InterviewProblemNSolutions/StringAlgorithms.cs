using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblemNSolutions
{
    public class StringAlgorithms
    {
        // exact string matching algorithms. (p. 656) || Brute Force
        /// <summary>
        /// Returns index in input where given pattern is found for 1st time, else -1 of pattern not found
        /// Time O(n * m), n = length of input, m = length of pattern || Space O(1)
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static int BruteForceStringMatch(string input, string pattern)
        {
            var tArr = input.ToCharArray();         // input text where we need to perform search
            var pArr = pattern.ToCharArray();       // pattern to be searched
            for (int i = 0; i < tArr.Length - pArr.Length + 1; i++)
            {
                var index = 0;
                while (index < pArr.Length && pArr[index] == tArr[index + i])
                    index++;
                if (index == pArr.Length) return i;
            }    
            return -1;
        }
    }
}
