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
        /// Time O((n – m + 1) × m) ~O(n × m), n = length of input, m = length of pattern || Space O(1)
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

        /// <summary>
        /// Rabin-karn formula: hash( txt[s+1 .. s+m] ) = ( d ( hash( txt[s .. s+m-1]) – txt[s]*h ) + txt[s + m] ) mod q
        /// here d = no of ASCII characters, q is largest prime smaller than 10^m, h = d^(m-1)
        /// m = length of pattern
        /// n = length of input
        /// s = leading character in current hash value
        /// Time Best case O(n+m) worst O(n×m)|| Space O(1)
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static void RabinKarpStringMatch(string input, string pattern, int primeNo, int noOfASCII = 256)
        {
            var tArr = input.ToCharArray();         // input text where we need to perform search
            var pArr = pattern.ToCharArray();       // pattern to be searched

            var d = noOfASCII;                      // range of ASCII characters(default 256 for alphabets & 10 if using digits[0..9])
            var n = input.Length;
            var m = pattern.Length;
            var q = primeNo;
            var h = 1;
            int pHash = 0;                          // hash value for pattern
            int tHash = 0;                          // hash value for subset of input

            // calculate value of h                 // h = d^(m-1)
            for (int i = 0; i < m - 1; i++)
                h = (h * d) % q;

            // calculate the hash value for pattern and first subset in input
            for (int i = 0; i < m; i++)
            {
                pHash = (pHash * d + pArr[i]) % q;
                tHash = (tHash * d + tArr[i]) % q;
            }

            for (int i = 0; i < n - m + 1; i++)
            {
                // if Hash value matches, check entire pattern with current input subset
                if (pHash == tHash)
                {
                    var j = 0;
                    while (j < m && pArr[j] == tArr[j + i])
                        j++;
                    if (j == m)
                        Console.WriteLine($" Pattern '{pattern}' found at index '{i}' in Input string \"{input}\"");
                }
                // calculate next HashValue for input subset
                if (i < n - m)
                    tHash = (d * (tHash - (tArr[i] * h)) + tArr[i + m]) % q;

                // We might get negative value of tHash, converting it to positive 
                if (tHash < 0)
                    tHash = tHash + q;

            }
        }

        /// <summary>
        /// Time O(n+m) || Space O(m), here n = lenth of input string & m = length of pattern being searched
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        public static void KMPStringMatch(string input, string pattern)
        {
            var tArr = input.ToCharArray();         // input text where we need to perform search
            var pArr = pattern.ToCharArray();       // pattern to be searched
            int[] lps = new int[pattern.Length];    // lps = longest suffice length same as prefix in pattern
            PrefixForKMP(pArr, ref lps);            // do preprocessing on pattern and update lps || Time O(m)

            // KMP algo
            int i = 0, j = 0, n = input.Length, m = pattern.Length;
            while (i < n)                           // Time O(n)
            {
                if (tArr[i] == pArr[j])             // characters match
                {
                    i++;
                    j++;
                    if (j == m)                     // match found, now reset j = 0 to keep looking for pattern again
                    {
                        Console.WriteLine($" Pattern '{pattern}' found at index '{i - m}' in Input string \"{input}\"");
                        j = 0;
                    }
                }
                else if (j > 0)
                    j = lps[j - 1];                 // while j>0 go back to prev index value in lps and start matching again
                else i++;                           // characters didn't match, start comparing from next character in input
            }
        }

        public static void PrefixForKMP(char[] pArr, ref int[] lps)
        {
            var m = pArr.Length;
            lps[0] = 0;                             // Set the count for lps[0] = 0
            int i = 1, j = 0;
            while (i < m)
            {
                if (pArr[i] == pArr[j])             // characters match
                {
                    // mark the occurance of current char at lps[i] as the value of j + 1, which indicates where to start matching again in lps
                    lps[i] = j + 1;                 // can be done in single line => lps[i++] = 1 + j++;
                    i++;
                    j++;
                }
                else if (j > 0)
                    j = lps[j - 1];                 // while j>0 go back to prev index value in lps and start matching again
                else i++;
            }
        }




        // Time O(n/2) ~n || Space O(1)
        public static void ReverseStringInPlace(char[] str, int startIndex = -1, int endIndex = -1, bool silent=false)
        {
            if (!silent) Console.Write($"Reversing(In-Place) input string : '{new string(str)}'");
            int start = 0, len = str.Length - 1;
            if (startIndex != -1 && startIndex <= endIndex)     // if provied start & end Index use them
            {
                start = startIndex;
                len = endIndex;
            }
            while (start < len)
                Utility.SwapXOR(ref str[start++], ref str[len--]);
            if (!silent) Console.WriteLine($" \t'{new string(str)}'");
        }

        // Time O(n/2) ~n || Space O(n)
        public static string ReverseString(string str)
        {
            Console.Write($"Reversing input string : \t'{str}' ");
            char[] reverse = new char[str.Length];          // object to store reversed string
            int len = str.Length - 1, i = 0;
            while (i <= len)
            {
                reverse[len] = str[i];
                reverse[i++] = str[len--];
            }
            return new string(reverse);
        }

        // Time O(n) || Space O(n)
        public static string ReverseSentence(char[] sentence, char delimeter = ' ')
        {
            Stack<string> words = new Stack<string>();
            string currentWord = "";
            foreach (char ch in sentence)       // Time O(n)
                if (ch == delimeter)
                {
                    words.Push(currentWord);
                    words.Push(delimeter.ToString());
                    currentWord = "";
                }
                else
                    currentWord += ch;
            
            words.Push(currentWord);            // push last word

            string reverseString = "";
            foreach (var word in words)         // read from stack top and construct reverse string
                reverseString += word;
            return reverseString;
        }

        // Time O(n) || Space O(1)
        public static string ReverseSentenceInPlace(char[] sentence, char delimeter = ' ')
        {
            ReverseStringInPlace(sentence);                                     // reverse entire sentence

            int lastDelimiterFoundAt = 0, i;
            for (i = 0; i < sentence.Length; i++)
                if (sentence[i] == delimeter)
                {
                    ReverseStringInPlace(sentence, lastDelimiterFoundAt, i-1);  // reverse each word when delimiter is encountered
                    lastDelimiterFoundAt = i + 1;
                }
            ReverseStringInPlace(sentence, lastDelimiterFoundAt, i - 1);        // reverse last word
            return new string(sentence);
        }

        [Obsolete("use => StringPermutation() instead",true)]
        public static void PrintStringPermutation(char[] input)
        {
            /* 1) fix first character of the array >> than print the rest character in sliding window pattern(rotate the array)
             * 2) now left rotate the array and repeat step 1
             * keep repeating 2nd step until all the characters have been at been fixed at 1st index atleast once.
             */
            var len = input.Length;
            int startIndex = 0;
            while (startIndex < len)
            {
                Utility.Swap(ref input[0], ref input[startIndex]);
                Console.WriteLine(input[0]);
                PrintCurrentWindow(input, 1, len);  // remove this hardcoding for 1, instead pass something like range which increase with length of input
                Console.WriteLine("\n\n");
                startIndex++;
            }

            void PrintCurrentWindow(char[] str, int startFromIndex, int endAtIndex)
            {
                int counter = 0;
                while (counter < endAtIndex - startFromIndex)
                {
                    int j = 0;
                    while (j < startFromIndex) Console.Write(str[j++]);
                    while (j < endAtIndex)
                    {
                        var toPrint = counter + j++;
                        if (toPrint >= endAtIndex) toPrint -= (endAtIndex - startFromIndex);
                        Console.Write(str[toPrint]);
                    }
                    counter++;
                    Console.Write(" ");
                }
                ReverseStringInPlace(str, 1, endAtIndex - 1, true);
                counter = 0;
                while (counter < endAtIndex - startFromIndex)
                {
                    int j = 0;
                    while (j < startFromIndex) Console.Write(str[j++]);
                    while (j < endAtIndex)
                    {
                        var toPrint = counter + j++;
                        if (toPrint >= endAtIndex) toPrint -= (endAtIndex - startFromIndex);
                        Console.Write(str[toPrint]);
                    }
                    counter++;
                    Console.Write(" ");
                }
                //for (int i = startIndex; i < endAtIndex; i++)
                //{
                //    int j = 0;
                //    while (j < endAtIndex)
                //        Console.Write($"{str[(i + j++) % endAtIndex]}");
                //    Console.Write(" ");
                //}
                ReverseStringInPlace(str, 1, endAtIndex - 1, true);
            }
            
        }

        // Tushar Roy https://youtu.be/nYFd7VHKyWQ
        /// <summary>
        /// String Permutation Algorithm, which prints all the permutation in lexographical order, also handles duplicates
        /// Time Complxity O(factorial time), if K unique charactes in input than O(K!)
        /// else if out of unqiue k, 2 charactes repeat a & b times than O(k! / (a! * b!))
        /// Space Complexity O(english characters) = O(26)
        /// </summary>
        /// <param name="input"></param>
        public static void StringPermutation(string input)
        {
            // assumption we are only working with large Caps in English
            input = input.ToUpper();

            // create a dataset/HashMap which stores occurence of each alphabet in in lexographical order
            int[] characterCount = Enumerable.Repeat(-1, 26).ToArray();
            
            var charArray = input.ToCharArray();

            // update the count for each character from input in the map
            foreach (var ch in charArray)
                characterCount[ch - 'A'] = characterCount[ch - 'A'] == -1 ? 1 : characterCount[ch - 'A'] + 1;

            // create a result datastruture of length same as input
            char[] result = new char[input.Length];

            // call recursive func on HashMap
            StringPermutation_Util(characterCount, result, 0);
        }

        protected static void StringPermutation_Util(int[] map, char[] result, int depth)
        {
            for (int i = 0; i < map.Length; i++)
            {
                // skip character with count = 0
                if (map[i] == 0 || map[i] == -1) continue;

                result[depth] = (char)(i + 'A');    // add character at current depth of recursion
                map[i] -= 1;                        // decreament the count of current character in dictonary

                StringPermutation_Util(map, result, depth + 1);
                map[i] += 1;                        // restore the count of current character after exiting recursion
            }
            // if count for all is 0 print the result
            bool allZero = true;
            foreach (var ch in map)
                if (ch == -1) continue;
                else if (ch != 0) allZero = false;

            if (allZero) Console.WriteLine(new string(result));
        }

    }
}
