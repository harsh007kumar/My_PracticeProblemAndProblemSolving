﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace InterviewProblemNSolutions
{
    public class StringAlgorithms
    {
        static readonly int StandardASCII = 128;                // characters(0-127)
        static readonly int ExtendedASCII = StandardASCII * 2;  // characters(128-255)

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
                while (index < pArr.Length && index + i < tArr.Length && pArr[index] == tArr[index + i])
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
        /// s = leading character in current hash freq
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
            int pHash = 0;                          // hash freq for pattern
            int tHash = 0;                          // hash freq for subset of input

            // calculate freq of h                 // h = d^(m-1)
            for (int i = 0; i < m - 1; i++)
                h = (h * d) % q;

            // calculate the hash freq for pattern and first subset in input
            for (int i = 0; i < m; i++)
            {
                pHash = (pHash * d + pArr[i]) % q;
                tHash = (tHash * d + tArr[i]) % q;
            }

            for (int i = 0; i < n - m + 1; i++)
            {
                // if Hash freq matches, check entire pattern with current input subset
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

                // We might get negative freq of tHash, converting it to positive 
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
                    j = lps[j - 1];                 // while j>0 go back to prev index freq in lps and start matching again
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
                    // mark the occurance of current char at lps[i] as the freq of j + 1, which indicates where to start matching again in lps
                    lps[i] = j + 1;                 // can be done in single line => lps[i++] = 1 + j++;
                    i++;
                    j++;
                }
                else if (j > 0)
                    j = lps[j - 1];                 // while j>0 go back to prev index freq in lps and start matching again
                else i++;
            }
        }




        // Time O(n/2) ~n || Space O(1)
        public static void ReverseStringInPlace(char[] str, int startIndex = -1, int endIndex = -1, bool silent = false)
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
                    ReverseStringInPlace(sentence, lastDelimiterFoundAt, i - 1);  // reverse each word when delimiter is encountered
                    lastDelimiterFoundAt = i + 1;
                }
            ReverseStringInPlace(sentence, lastDelimiterFoundAt, i - 1);        // reverse last word
            return new string(sentence);
        }

        // Tushar Roy https://youtu.be/nYFd7VHKyWQ
        /// <summary>
        /// String Permutation Algorithm, which prints all the permutation/anagrams in lexographical order, also handle duplicates
        /// Time Complxity O(factorial time), if K unique charactes in input than O(K!)
        /// else if out of unqiue 'k', 2 charactes repeat a & b times than O(k! / (a! * b!))
        /// Space Complexity O(k), K unique charactes
        /// </summary>
        /// <param name="input"></param>
        public static void StringPermutation(string input)
        {
            // assumption we are only working with large Caps in English
            input = input.ToUpper();
            var len = input.Length;
            var charArray = input.ToCharArray();

            // create a HashMap which stores occurence of each alphabet in in lexographical order
            SortedDictionary<char, int> dict = new SortedDictionary<char, int>();
            foreach (var ch in charArray)
                if (dict.ContainsKey(ch))
                    dict[ch] += 1;
                else
                    dict.Add(ch, 1);

            // char array to hold all unqiue characters
            char[] character = new char[len];
            // int array to hold all unqiue characters count
            int[] count = new int[len];

            int i = 0;
            foreach (var ch in dict)
            {
                character[i] = ch.Key;
                count[i] = ch.Value;            // update the count for each character from input
                i++;
            }

            // create a result datastruture of length same as input
            char[] result = new char[len];

            // call recursive func on HashMap
            StringPermutation_Util(character, count, result, 0);
        }

        protected static void StringPermutation_Util(char[] map, int[] count, char[] result, int depth)
        {
            // if count for all is 0 print the result
            if (depth == result.Length)
                Console.WriteLine(new string(result));
            else
            {
                for (int i = 0; i < map.Length; i++)
                {
                    // skip character with count = 0
                    if (count[i] == 0) continue;

                    result[depth] = map[i];     // update character in result array, at index = depth of recursion
                    count[i] -= 1;              // decreament the count of current character in map

                    StringPermutation_Util(map, count, result, depth + 1);
                    count[i] += 1;              // restore the count of current character after exiting recursion
                }
            }
        }

        // Tushar Roy https://youtu.be/xTNFs5KRV_g
        /// <summary>
        /// String Combination Algorithm, which prints all the combinateion in lexographical order,
        /// Combination 'AB' is same as 'BA' hence we can print this combo/set just once unlike permutation where we print both
        /// Time Complexity O(2^N), i.e, exponantional where N = length of input
        /// Space Complexity O(k), K unique charactes
        /// </summary>
        /// <param name="input"></param>
        public static void CombinationOfCharacters(string input)
        {
            // assumption we are only working with large Caps from English dictonary
            input = input.ToUpper();
            var len = input.Length;

            // create a HashMap which stores occurence of each alphabet in in lexographical order
            SortedDictionary<char, int> hashMap = new SortedDictionary<char, int>();
            foreach (var ch in input.ToCharArray())
                if (hashMap.ContainsKey(ch))
                    hashMap[ch] += 1;
                else
                    hashMap.Add(ch, 1);

            // create a result datastruture of length same as input
            char[] charArray = new char[hashMap.Count];
            // int array to hold all unqiue characters count
            int[] count = new int[hashMap.Count];

            int i = 0;
            foreach (var keyvalue in hashMap)
            {
                charArray[i] = keyvalue.Key;
                count[i] = keyvalue.Value;
                i++;
            }

            // create a result datastruture of length same as input
            char[] output = new char[len];

            // call recursive func
            CombinationOfCharacters_Util(charArray, count, output);
        }

        protected static void CombinationOfCharacters_Util(char[] map, int[] count, char[] output, int startFrom = 0, int depth = 0)
        {
            Console.WriteLine(new string(output, 0, depth));
            for (int i = startFrom; i < map.Length; i++)
            {
                // skip character with count = 0
                if (count[i] == 0) continue;

                output[depth] = map[i];     // update character in result array, at index = depth of recursion
                count[i] -= 1;              // decreament the count of current character in map

                CombinationOfCharacters_Util(map, count, output, i, depth + 1);
                count[i] += 1;              // restore the count of current character after exiting recursion
            }
        }

        /// <summary>
        /// Function take string input and recursively removes all adjacent duplicate characters
        /// Time O(n), n = length of input || Space Complexity O(1)
        /// </summary>
        /// <param name="input"></param>
        public static void RecursiveRemoveAdjacentCharacters(string input)
        {
            // read first index of input and store its index
            // read next character if it matches with input[index] remove both index and continue in loop also set index--
            // else if next char not same as input[index] index++
            Console.Write($" Input '{input}'");

            int i = 1, lastCharIndex = -1;
            while (i < input.Length)
            {
                if (lastCharIndex == -1)
                    lastCharIndex = 0;

                if (input[i] == input[lastCharIndex])
                {
                    input = input.Remove(lastCharIndex, i - lastCharIndex + 1);     // remove same adjacent characters from input
                    i = lastCharIndex--;
                }
                else
                    lastCharIndex = i++;                                            // if characters don't match increament both last index & i by 1
            }
            Console.WriteLine($" \tTransforms =>> '{input}'\n after recursively removing same adjacent characters");
        }

        public static void FixedWindowContainingAllCharacters(string input, string charSet)
        {
            /* set the window size as charSet.Length
             * store all the characters & their count in Dictonary/hashMap
             * precompute 1st window for input string and add characters & their count in second dictonary/hashMap
             * if both hashMap match, print the characters/index i to (i + charSetLength - 1), EXIT
             * else increament i++
             */

            if (charSet.Length > input.Length) return;

            var slideSize = charSet.Length;
            // Dictonary to store characters & count from 'charSet'
            Dictionary<char, int> dictChars = new Dictionary<char, int>();
            foreach (var ch in charSet)
                if (dictChars.ContainsKey(ch))
                    dictChars[ch]++;
                else
                    dictChars.Add(ch, 1);

            // Dictonary to store characters & count from 'input' for initial window
            Dictionary<char, int> dictSlideWindow = new Dictionary<char, int>();
            int i = 0;
            while (i < slideSize)
            {
                var ch = input[i];
                if (dictSlideWindow.ContainsKey(ch))
                    dictSlideWindow[ch]++;
                else
                    dictSlideWindow.Add(ch, 1);
                i++;
            }

            // start sliding the window and keep matching the hashMap if true break and print the index i
            i = 0;
            bool mapMatch = false;
            while (i < input.Length - slideSize + 1)
            {
                // match hashMap/Dictonary if they contain same characters with same count
                mapMatch = dictSlideWindow.OrderBy(kvp => kvp.Key).SequenceEqual(dictChars.OrderBy(kvp => kvp.Key));
                if (mapMatch)
                    break;

                var windowStartChar = input[i];
                if (dictSlideWindow[windowStartChar] == 1) dictSlideWindow.Remove(windowStartChar);
                else dictSlideWindow[windowStartChar]--;

                if (i < input.Length - slideSize)
                {
                    var windowEndChar = input[i + slideSize];
                    if (dictSlideWindow.ContainsKey(windowEndChar)) dictSlideWindow[windowEndChar]++;
                    else dictSlideWindow.Add(windowEndChar, 1);
                }
                i++;
            }
            if (mapMatch) Console.WriteLine($" First Min window containing all characters '{charSet}'" +
                 $"\n In input '{input}' \n Start at index '{i}' \n Ends at index '{i + slideSize - 1}'");
            else Console.WriteLine($"No Window Containing All Characters '{charSet}' found in input '{input}'");
        }

        /// <summary>
        /// Time Complexity O(2n) ~ O(n), worst case when we adding characters 2n times, i.e, each window has same length and m possilbe windows are present in array
        /// Space Compexity O(ExtendedASCII) = O(256) ~O(1)
        /// </summary>
        /// <param name="input"></param>
        /// <param name="charSet"></param>
        public static void MinWindowContainingAllCharacters(string input, string charSet)
        {
            if (charSet.Length > input.Length) return;
            var StandardASCII = 128;                    // characters(0-127)
            var ExtendedASCII = StandardASCII * 2;      // characters(128-255)

            // int array to store each unquie characters count from 'charSet'
            int[] expected = new int[ExtendedASCII];
            // update the count of characters found in charSet
            foreach (var ch in charSet)
                expected[(int)ch]++;

            // int array to store each unquie characters count from 'input'
            int[] actual = new int[ExtendedASCII];

            int startIndex = 0, endIndex = -1;     // to store sliding window start & end index
            int i = 0, j = 0, MinWindowLen = int.MaxValue, count = 0;
            for (i = 0; i < input.Length; i++)
            {
                var chInt = (int)input[i];
                if (expected[chInt] == 0)           // characters which is not present in charSet found skip forward
                    continue;

                actual[chInt]++;                    // increment count in actual array

                if (actual[chInt] <= expected[chInt])
                    count++;

                if (count >= charSet.Length)        // all required characters found
                {
                    // now try reducing the size of the window
                    // traverse thru each character from input string & try to find if it doesnt exsits in expected set or it actual has more count than expected
                    while (expected[input[j]] == 0 || actual[input[j]] > expected[input[j]])
                    {
                        if (actual[input[j]] > expected[input[j]])
                            actual[input[j]]--;
                        j++;
                    }
                    if (MinWindowLen > i - j + 1)
                    {
                        MinWindowLen = i - j + 1;   // update the Sliding window size
                        startIndex = j;
                        endIndex = i;
                    }
                }
            }
            if (endIndex != -1)
                Console.WriteLine($" Min window containing all characters \t'{charSet}'" +
                 $"\n Input string \t'{input}' \n Start Index \t'{startIndex}' \n End Index \t'{endIndex}'");
            else
                Console.WriteLine($"No Window Containing All Characters \t'{charSet}' found in input \t'{input}'");
        }
        // Time O(m+n) Space O(1), m,n = length of string 's' & 't' respectively
        public static string MinWindowContainingAllCharFaster(string s, string t)
        {
            if (t.Length > s.Length) return "";  // base case
            int[] charFreq = new int[128], currWindow = new int[128];   // Standard ASCII

            // get all unique characters & their frequency in 't'
            for (int i = 0; i < t.Length; i++)
                charFreq[t[i]]++;

            // using sliding window technique,
            // we keep moving the rt pointer till we get all char with freq >= one present in 'charFreq'
            // once we get it we keep moving lt pointer frwd and at every point we check this substring still contains all the req chars & freq we update the 'ans' substring with this new smaller substring till its no longer contains all req chars
            // than we again start moving the rt pointer till its <s.Length

            int lt = -1, rt = -1, ansLen = int.MaxValue, ansStartIdx = -1, validCharCount = 0;
            while (++rt < s.Length)
                // if curr char is part of target string and its count is <= req count
                if (++currWindow[s[rt]] <= charFreq[s[rt]])
                    // found all req characters of 'target'
                    if (++validCharCount == t.Length)
                        while (validCharCount == t.Length)
                        {
                            if (rt - lt < ansLen)
                            {
                                ansLen = rt - lt;
                                ansStartIdx = lt + 1;
                            }
                            if (--currWindow[s[++lt]] < charFreq[s[lt]])
                                validCharCount--;
                        }
            // we atleast one valid SubString found return the ans else return empty string
            return ansStartIdx > -1 ? s.Substring(ansStartIdx, ansLen) : "";
        }


        /// <summary>
        /// Returns True if given 'pattern' exists in 2-D 'input' char array, else returns false
        /// Time Complexity O(RightOfPalindrome*CentreOfPalindrome), RightOfPalindrome = rows & CentreOfPalindrome = Columns in input 2-D char array || Auxillary Space O(1)
        /// can also check GFG https://www.geeksforgeeks.org/search-a-word-in-a-2d-grid-of-characters/ , below is personnel implementation after referring the book
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool FindPatternIn2DCharArray(char[,] input, string pattern)
        {
            /* start from first index check if it matches first character of pattern
             * if yes than 
             *      a) Mark current Node as visited
             *      c) Recursively find other remaining characters(in any order) in all possible 8 directions
             *      d) maintain a 'count' to indicate no of characters found at every step
             *      e) if count equals length of pattern, return true and break out
             * also remeber if during recursion next characters is not found
             *      a) Mark back the current Node as un-visited
             *      c) decrement the 'count' by one
             * if No than check at next index in 2D array
             * if u reach end of the 2D array return false
             */

            var noOfRow = input.GetLength(0);               // no of rows
            var noOfCol = input.GetLength(1);               // no of cols

            // visited array to mark which Nodes are visited
            bool[,] isVisited = new bool[noOfRow, noOfCol];

            // call recusrive function from 1st row's 1st column
            return FindPatternIn2DCharArrayUtil(input, pattern, isVisited, noOfRow, noOfCol, 0, 0, 0);
        }

        protected static bool FindPatternIn2DCharArrayUtil(char[,] input, string pattern, bool[,] isVisited, int maxRow, int maxCol, int currentRow = 0, int currentCol = 0, int level = 0)
        {
            if (level == pattern.Length)
            {
                Console.WriteLine($" Pattern '{pattern}' found in given 2D char array");
                return true;
            }
            else if (isVisited[currentRow, currentCol])
                return false;

            // check if first character of pattern doesn't matches any given node of input 2D array
            if (input[currentRow, currentCol] != pattern[level] && level == 0)
            {
                if (currentCol + 1 < maxCol)                // if next column exists in current row search it
                    return FindPatternIn2DCharArrayUtil(input, pattern, isVisited, maxRow, maxCol, currentRow, currentCol + 1, level);
                else if (currentRow + 1 < maxRow)           // else if next row is avaliable search it and reset the column to 0
                    return FindPatternIn2DCharArrayUtil(input, pattern, isVisited, maxRow, maxCol, currentRow + 1, 0, level);
                else return false;                          // reached end of the 2D array
            }
            else if (input[currentRow, currentCol] == pattern[level])   // character matched
            {
                isVisited[currentRow, currentCol] = true;   // Mark current Node Visited

                int upperRowLimit = currentRow, lowerRowLimit = currentRow, leftmostColLimit = currentCol, rtmostColLimit = currentCol;
                /*      Now start matching all the remaing characters of pattern in all 8 directions
                 *      1  2  3
                 *      4 [*] 5
                 *      6  7  8
                 */
                // Set upper & lower bound of the rows and columns where we can search for next character in pattern
                if (currentRow > 0) upperRowLimit--;
                if (currentRow + 1 < maxRow) lowerRowLimit++;
                if (leftmostColLimit > 0) leftmostColLimit--;
                if (rtmostColLimit + 1 < maxCol) rtmostColLimit++;

                for (int i = upperRowLimit; i <= lowerRowLimit; i++)
                    for (int j = leftmostColLimit; j <= rtmostColLimit; j++)
                    {
                        var foundNext = FindPatternIn2DCharArrayUtil(input, pattern, isVisited, maxRow, maxCol, i, j, level + 1);
                        if (foundNext)
                            return true;
                    }
                isVisited[currentRow, currentCol] = false;  // mark current Node back as Not-Visited
            }
            return false;
        }


        public static void PrintInterleavings(string firstString, string secondString)
        {
            var output = new char[firstString.Length + secondString.Length];
            PrintInterleavingsUtil(firstString, secondString, output, 0, 0);
        }

        protected static void PrintInterleavingsUtil(string firstString, string secondString, char[] output, int firstIndex = 0, int secondIndex = 0)
        {
            var len1 = firstString.Length;
            var len2 = secondString.Length;

            // we have read all the characters from both input strings
            if (firstIndex == len1 && secondIndex == len2)
            {
                Console.WriteLine($" {new string(output)}"); 
                return;
            }
            if (firstIndex < len1)
            {
                output[firstIndex + secondIndex] = firstString[firstIndex];
                PrintInterleavingsUtil(firstString, secondString, output, firstIndex + 1, secondIndex);
            }
            if (secondIndex < len2)
            {
                output[firstIndex + secondIndex] = secondString[secondIndex];
                PrintInterleavingsUtil(firstString, secondString, output, firstIndex, secondIndex + 1);
            }
        }

        /// <summary>
        /// Time O(2n) ~ O(n) || Space O(n+m), n = length of input, m = noOfSpaces * replaceWithString length
        /// </summary>
        /// <param name="input"></param>
        /// <param name="replaceWith"></param>
        public static void ReplaceSpaceWithGivenChars(string input, string replaceWith)
        {
            Console.WriteLine($" Input String : '{input}'\n replace with : '{replaceWith}'");
            int countSpace = 0, i = 0;
            var space = ' ';
            for (i = 0; i < input.Length; i++)
                if (input[i] == space) countSpace++;
            var outputLen = input.Length + (countSpace * replaceWith.Length);
            var output = new char[outputLen];
            outputLen--;
            for (i = input.Length - 1; i >= 0; i--)
            {
                if (input[i] == space)
                    for (int j = replaceWith.Length - 1; j >= 0; j--)
                        output[outputLen--] = replaceWith[j];
                else
                    output[outputLen--] = input[i];
            }
            Console.WriteLine($" Final output is : {new string(output)}");
        }

        // Time O(n^2) || Space O(256) ~O(1), 256 no of Extended ASCII characters
        public static void LongestSubStringNonRepeatingCharacters(string str)
        {
            HashSet<int> charCount = new HashSet<int>();    // instead of HashSet we can also use array to represent all characters int[256]
            int len = str.Length;
            int longestSubstring = 1, i = 0, j = 0;
            for (i = 0; i < len; i++)
            {
                charCount.Clear();
                charCount.Add(str[i]);                      // add character to count
                // now traverse thru the input string till be dont find any character which is alredy present in the hashset
                for (j = i + 1; j < len; j++)
                {
                    if (charCount.Contains(str[j]))
                        break;
                    charCount.Add(str[j]);
                }
                longestSubstring = Math.Max(longestSubstring, charCount.Count);     // can also use 'j - i' here 
            }
            Console.WriteLine($" Longest substring in input : {str}\n Without any repeating character was of length : {longestSubstring}");
        }

        // Time O(n) || Space O(256) ~O(1), 256 no of Extended ASCII characters
        public static void LongestSubStringNonRepeatingChar(string str)
        {
            bool[] charArr = new bool[ExtendedASCII];       // instead of array we can also use HashSet to represent all characters
            int len = str.Length;
            int longestSubstring = 1;
            int i, j = 0;
            for (i = 0; i < len; i++)
            {
                if (!charArr[str[i]])                       // char not found yet
                {
                    charArr[str[i]] = true;                 // add character to count
                    continue;
                }
                longestSubstring = Math.Max(longestSubstring, i - j);
                while (str[j] != str[i])                    // find the index which was the 1st occurance of the repeated character
                    charArr[str[j++]] = false;              // also keep removing all characters on the way as they can't be utilized in further sub-strings
                charArr[str[j++]] = false;                  // mark not found for the duplicate character
                i--;
            }
            longestSubstring = Math.Max(longestSubstring, i - j);
            Console.WriteLine($" Longest substring in input : {str}\n Without any repeating character was of length : {longestSubstring}");
        }


        // Time O(n) || Space O(26) ~O(1)
        public static bool PermutationInString(string s1, string s2)
        {
            #region ALGO
            //Get the count of each character in s1
            //Now traverse thru s2 and start updating the count of characters encountered
            //    if we get character which is not present in s1 then reset the counter for all characters
            //    bcoz we want continous chars ie. substring of s1 & even one unwated chars will unstify condition

            //    another thing to keep in mind is if we get more than k (present in s1) req character for any letter in s2
            //    then we must remove that character & any character which comes before  that character is removed
            #endregion
            int[] expected = new int[26], actual = new int[26];
            foreach (var ch in s1)          // O(n)
                expected[ch - 'a']++;

            int totalCharInS1 = s1.Length, validCharFoundInS2 = 0, startIdx = 0, l = s2.Length;
            for (int i = 0; i < l; i++)     // O(m)
            {
                var ch = s2[i];
                if (expected[ch - 'a'] == 0)// character which is not present in s1 encoutered reset count and 'actual' set
                {
                    actual = new int[26];   // reset characters found till now
                    validCharFoundInS2 = 0;
                    startIdx = i + 1;
                }
                else                        // char present in s1 found
                {
                    if (++actual[ch - 'a'] > expected[ch - 'a'])  // if frequency of current character is more than expected
                    {
                        while (startIdx < i)
                        {
                            actual[s2[startIdx] - 'a']--;
                            if (s2[startIdx++] == ch) break;   // extra count of current char is removed, hence break now
                            validCharFoundInS2--;
                        }
                    }
                    else
                    {
                        if (++validCharFoundInS2 == totalCharInS1)
                            return true;    // all required characters found in continuation
                    }
                }
            }
            return false;

            #region Prv Approach O(n) Approach
            /*
            int[] actual = new int[26];
            int[] expected = new int[26];
            for (int i = 0; i < s1.Length; i++)
                expected[s1[i] - 'a']++;

            int count = 0, start = 0;
            for (int i = 0; i < s2.Length; i++)
                // character which is not present in s1 encoutered reset 'count' and 'actual' set
                if (expected[s2[i] - 'a'] == 0)
                {
                    count = 0;
                    actual = new int[26];
                    // if a permutation exists definatly it will start after the unmatched character
                    start = i + 1;
                }
                // if frequency of current char is less than equals to expected, than increament 'count'
                else if (++actual[s2[i] - 'a'] <= expected[s2[i] - 'a'])
                {
                    // once we have all required character with their matching frequency return true
                    if (++count == s1.Length)
                        return true;
                }
                // if frequency of current character is more than expected
                else
                {
                    // than remove all characters from from left till we dont get to current 'i' th character
                    while (s2[start] != s2[i])
                        actual[s2[start++] - 'a']--;
                    // now remove 1 more time to match actual and required curr Char frequency
                    actual[s2[start++] - 'a']--;
                    
                    // update the count i.e. length of characters which are still matching the required set
                    count = i - start + 1;
                }

            return false;
            */
            #endregion
        }



        // Time O(n) || Space O(1)
        public static bool IsOneEditDistance(string s, string t)
        {
            /* A string s is said to be one distance apart from a string t if you can:
             * Insert exactly one character into s to get t.
             * Delete exactly one character from s to get t.
             * Replace exactly one character of s with a different character to get t.
             */

            int sl = s.Length, tl = t.Length;
            if (Math.Abs(sl - tl) > 1) return false;

            // we want to keep shorter string as 's'
            if (sl > tl) return IsOneEditDistance(t, s);

            int diff = 0, j = 0;
            if (sl == tl)
            {
                // Compare both same length strings, if at any point diff in characters is more than 1 return false
                for (int i = 0; i < sl; i++)
                    if (s[i] != t[i] && ++diff > 1)
                        return false;
                // if no difference was found means string of same length are excatly same hence not excat 1 dist apart
                if (diff == 0)
                    return false;
            }
            else
                for (int i = 0; i < sl; i++)
                    if (s[i] == t[j + diff])
                        j++;
                    else // s[i] != t[j]
                    {
                        // when characters of 1 length different string mismatch, try comparing next character from longer string with same character from 's' string
                        i--;
                        if (++diff > 1)
                            return false;
                    }
            return true;
        }


        // Time O(n) || Space O(1)
        public static int LengthOfLongestSubstringKDistinct(string s, int k)
        {
            int[] charMap = new int[256];
            int i = 0, j = 0, max = -1, distinct = 0;
            for (i = 0; i < s.Length; i++)
            {
                if (charMap[s[i]]++ == 0)       // New Char found
                    if (++distinct > k)         // check if increase distinct count has crossed 'k' threshold
                    {
                        // remove characters from start till we dont find a character whos total count now becomes 0
                        while (--charMap[s[j++]] > 0)
                        { }
                        --distinct;
                    }
                max = Math.Max(max, 1 + i - j); // update max
            }
            return max;
        }


        // Time O(n) || Space O(1)
        public static bool IsStrobogrammatic(string num)
        {
            // 0 : 0, 1 : 1, 6 : 9, 8: 8, 9 : 6
            int start = 0, last = num.Length - 1;
            while (start <= last)
                if (!Strobogrammatic(num[start++], num[last--]))
                    return false;
            return true;

            // local func
            bool Strobogrammatic(char c1, char c2)  // Time O(1)
            {
                if (c1 == '1' && c2 == '1') return true;
                else if (c1 == '0' && c2 == '0') return true;
                else if (c1 == '8' && c2 == '8') return true;
                else if (c1 == '6' && c2 == '9') return true;
                else if (c1 == '9' && c2 == '6') return true;
                else return false;
            }
        }


        // Time = Space = O(n)
        public static string RemoveAdjacentDuplicatesII(string s, int k)
        {
            Stack<Pair<char, int>> st = new Stack<Pair<char, int>>();
            for (int i = 0; i < s.Length; i++)
                if (st.Count > 0 && st.Peek().key == s[i])  // new 'char' same as last one
                {
                    if (++st.Peek().val == k)               // count of consecutive chars equals 'k'
                        st.Pop();
                }
                else                                        // different 'char' found
                    st.Push(new Pair<char, int>(s[i], 1));


            StringBuilder sb = new StringBuilder();
            foreach (var chars in st.Reverse())
                for (int i = 0; i < chars.val; i++)
                    sb.Append(chars.key);
            return sb.ToString();
        }


        // Time = Space = O(n) Soln
        public static bool CheckValidParenthesisString(string s)
        {
            Stack<int> open = new Stack<int>(), star = new Stack<int>();
            for (int i = 0; i < s.Length; i++)
                if (s[i] == '(')
                    open.Push(i);
                else if (s[i] == '*')
                    star.Push(i);
                else //if(s[i]==')')
                    if (open.Count > 0)
                    open.Pop();
                else if (star.Count > 0)
                    star.Pop();
                else
                    return false;
            // Now process leftover opening brackets
            while (open.Count > 0)
                if (star.Count == 0 || star.Pop() < open.Pop())
                    return false;
            return true;
        }
        // NeetCode https://youtu.be/QhPdNS143Qg
        // Time = O(n) | Space = O(1) Soln
        public static bool CheckValidParenthesisStringConstantSpace(string s)
        {
            int openBracketMin = 0, openBracketMax = 0;
            foreach (var ch in s)
            {
                if (ch == '(')
                {
                    openBracketMin++;
                    openBracketMax++;
                }
                else if (ch == ')')
                {
                    --openBracketMin;
                    --openBracketMax;

                }
                else
                {
                    --openBracketMin;                       // * can close )
                    openBracketMax++;                       // * can be open (
                }
                // if max possible left goes below zero we can never recover from it
                if (openBracketMax < 0) return false;
                // we only want to consider valid possibilities of min open brackets
                if (openBracketMin < 0) openBracketMin = 0;
            }
            return openBracketMin == 0;
        }


        // Time = Space = O(n)
        public static string ReverseOnlyLetters(string s)
        {
            int left = 0, right = s.Length - 1;
            char[] rev = s.ToCharArray();
            while (left < right)
            {
                while (left < right && !Char.IsLetter(rev[left]))
                    left++;
                while (left < right && !Char.IsLetter(rev[right]))
                    right--;

                if (left >= right) break;
                // swap
                var temp = rev[left];
                rev[left++] = rev[right];
                rev[right--] = temp;
            }
            return new string(rev);
        }


        // Time O(n*m) Space O(m), n = length of 'words', m = length of 'pattern'
        public static IList<string> FindAndReplacePattern(string[] words, string pattern)
        {
            Dictionary<char, char> wordPatternMap = new Dictionary<char, char>();
            HashSet<char> seen = new HashSet<char>();
            IList<string> ans = new List<string>();
            foreach (var word in words)  // O(n)
                if (IsMatch(word))       // O(m) , m = 20
                    ans.Add(word);
            return ans;

            // local helper func
            bool IsMatch(string word)
            {
                wordPatternMap.Clear();
                seen.Clear();
                for (int i = 0; i < word.Length; i++)
                    if (wordPatternMap.ContainsKey(word[i]))
                    {
                        // ecountered this character from 'word' before, return if its corrosponding char doesn't matches pattern curr idx
                        if (wordPatternMap[word[i]] != pattern[i]) return false;
                    }
                    else
                    {
                        // char from pattern was seen before, meaing it has been associated with a char from word before
                        // not also linked with new character from word making duplicate mapping, hence return false
                        if (seen.Contains(pattern[i])) return false;
                        wordPatternMap[word[i]] = pattern[i]; // add word[i]->pattern[i] mapping
                        seen.Add(pattern[i]);               // mark seen
                    }
                return true;
            }
        }


        // Time O(n) Space O(1)
        public static bool ValidPalindromeII(string s)
        {
            int start = 0, last = s.Length - 1;
            while (start < last)
                if (s[start] == s[last])
                {
                    start++;
                    last--;
                }
                else
                    return IsPalindrome(start + 1, last) || IsPalindrome(start, last - 1);
            
            return true;

            // helper func
            bool IsPalindrome(int lt, int rt)
            {
                while (lt < rt)
                    if (s[lt++] != s[rt--])
                        return false;
                return true;
            }
        }


        // Time O(n) Space O(1)
        public static int LongestBeautifulSubstringOfAllVowels(string w)
        {
            // to check the order
            char[] prvVowel = new char[26];
            prvVowel['e' - 'a'] = 'a';
            prvVowel['i' - 'a'] = 'e';
            prvVowel['o' - 'a'] = 'i';
            prvVowel['u' - 'a'] = 'o';

            int start, max = 0, l = w.Length, i = 0;
            while (i < l)           // O(n)
                if (w[i++] == 'a')
                    max = Math.Max(max, GetMax());
            return max;

            // helper func
            int GetMax()
            {
                start = --i;
                char curr = 'a';
                while (i < l)
                {
                    if (w[i] != curr)   // next char found
                        // if next Vowel's prv->char is not from the Vowel order
                        if (prvVowel[w[i] - 'a'] != curr) break;
                        // if order is maintained, than update 'curr' to next vowel in the order
                        else curr = w[i];
                    i++;
                }
                // if all Vowel's found, than curr must be pointing to last Vowel
                // else return 0 if not all vowels found
                return (curr == 'u') ? i - start : 0;
            }
        }


        // Time O(n^2) Space O(1), n = length of 'strs'
        public static int FindLUSlength(string[] strs)
        {
            int LUCS = -1;
            for (int i = 0; i < strs.Length; i++)
            {
                bool isSubsequence = false;
                for (int j = 0; j < strs.Length; j++)
                    if (i != j && Check(strs[i], strs[j]))
                    {
                        isSubsequence = true;
                        break;
                    }

                if (!isSubsequence)
                    LUCS = Math.Max(LUCS, strs[i].Length);
            }
            return LUCS;
            // local helper func
            bool Check(string a, string b)
            {
                int i = 0, j = 0;
                if (a.Length > b.Length) return false;
                while (i < a.Length && j < b.Length)
                    if (a[i] == b[j++])
                        i++;
                return i == a.Length; // all characters of 'a' found in 'b'
            }
        }


        // Time O(2^n) Space O(1), n = length of 's'
        public static int NumDistinctSubsequences_Recursive(string s, string t)
        {
            int count = 0;
            GetSequences(0, 0);
            return count;
            // local helper func
            void GetSequences(int idxS, int idxT)
            {
                if (idxT == t.Length)
                    count++;
                else
                    while (idxS < s.Length)
                    {
                        if (s[idxS] == t[idxT])    // req character from 't' match
                            GetSequences(idxS + 1, idxT + 1);
                        idxS++;
                    }
            }
        }


        // Time = Space = O(n*l), n = len of emails & l = avg length of each email
        public static int NumUniqueEmails(String[] emails)
        {
            Dictionary<string, HashSet<string>> uniqEmails = new Dictionary<string, HashSet<string>>();
            for (int i = 0; i < emails.Length; i++)
            {
                var (localName, domainName) = ProcessEmail(emails[i]);
                if (!uniqEmails.ContainsKey(localName))
                    uniqEmails[localName] = new HashSet<string>() { domainName };
                else
                    uniqEmails[localName].Add(domainName);
            }
            int uniCount = 0;
            foreach (var uniqDomain in uniqEmails.Values)
                uniCount += uniqDomain.Count;
            return uniCount;

            // local helper func
            (string, string) ProcessEmail(string email)
            {
                var eSplit = email.Split('@');  // split email into local & domain name

                StringBuilder local = new StringBuilder();
                foreach (var ch in eSplit[0])
                    if (char.IsLetter(ch))
                        local.Append(ch);
                    else if (ch == '.') continue;
                    else break;
                
                return (local.ToString(), eSplit[1]);
            }
        }


        public static bool WildcardMatching(string s, string p)
        {
            // Step1: remove multiple *** from pattern, ex: reduce a***b**c*? -> a*b*c*?
            StringBuilder sb = new StringBuilder();
            bool lastCharWasStar = false;
            foreach (var ch in p)
                if (ch != '*')
                {
                    sb.Append(ch);
                    lastCharWasStar = false;
                }
                else if (!lastCharWasStar)
                {
                    lastCharWasStar = true;
                    sb.Append(ch);
                }
            p = sb.ToString();


            int sI = 0, pI = 0, sLength = s.Length, pLength = p.Length;

            // Step2: check & remove characters from end if possible
            while (pLength > pI && sLength > sI)
            {
                if (p[pLength - 1] == '*') break;
                else
                    // '?' matches to any character in 's'
                    if (p[pLength - 1] == '?')
                    { pLength--; sLength--; }
                    // last characters match
                    else if (p[pLength - 1] == s[sLength - 1])
                    { pLength--; sLength--; }
                    // last characters don't match return false
                    else return false;
            }

            // Step3: check if matches
            return IsMatch(sI, pI, sLength, pLength);

            // local helper func
            bool IsMatch(int sIdx, int pIdx, int sLen, int pLen)
            {
                while (pIdx < pLen)
                {
                    // reached end of string 's', but more wild-characters from patterms is left to be matched hence return false
                    if (sIdx >= sLen && p[pIdx] != '*')
                        return false;

                    if (p[pIdx] == '?')
                        sIdx++;
                    else if (p[pIdx] == '*')
                    {
                        // '*' Matches any sequence of characters (including the empty sequence).
                        for (int i = sIdx; i < s.Length + 1; i++)   //   try skipping 0 to all remaining characters in 's'
                        //for (int i = sLen; i >= sIdx; i--)   //   try skipping 0 to all remaining characters in 's'
                            if (IsMatch(i, pIdx + 1, sLen, pLen))
                                return true;
                        // if after skpping some 'x' characters we didn't got the result than return False
                        break;
                    }
                    else // Alphabet to be matched
                    {
                        if (s[sIdx] == p[pIdx]) sIdx++;
                        else return false;
                    }

                    pIdx++;
                }

                return pIdx == pLen && sIdx == sLen;
            }
        }

        // Time O(n), Space O(m), n = length of 's', m = total unquie chars in 's'
        public static int LengthOfLongestSubstring_Faster(string s)
        {
            HashSet<char> uniqueChars = new HashSet<char>();
            Queue<MyCharNode> charOrder = new Queue<MyCharNode>();
            int maxLen = 0, currLen = 0;
            for (int i = 0; i < s.Length; i++)
                if (!uniqueChars.Contains(s[i]))                            // unique char found
                {
                    uniqueChars.Add(s[i]);                                  // add to the hashset   
                    charOrder.Enqueue(new MyCharNode(s[i], i));             // add char to the queue along with its index
                    maxLen = Math.Max(maxLen, ++currLen);
                }
                else
                {
                    while (charOrder.Count > 0)
                    {
                        var currNode = charOrder.Dequeue();
                        if (currNode.ch != s[i])
                            uniqueChars.Remove(currNode.ch);
                        else
                        {
                            charOrder.Enqueue(new MyCharNode(s[i], i));     // add current char back to the queue end
                            break;
                        }
                    }
                    currLen = i - (charOrder.Peek().idx) + 1;               // update currLen
                }
            return maxLen;
        }

        // Time O(n), Space O(n), n = length of 's', m = total unquie chars in 's'
        public static int LengthOfLongestSubstring_SecondApproach(string s)
        {
            Dictionary<char, int> charIdx = new Dictionary<char, int>();
            int start = 0, i = -1, maxLen = 0;
            while (++i < s.Length)
            {
                if (charIdx.ContainsKey(s[i]))
                {
                    var lastOccurenceIndex = charIdx[s[i]];
                    // update start only if start index is smaller than last Occurence Index of current character
                    start = lastOccurenceIndex >= start ? lastOccurenceIndex + 1 : start;
                    // update dictionary with latest index of current char
                    charIdx[s[i]] = i;
                }
                else
                    charIdx[s[i]] = i;
                maxLen = Math.Max(maxLen, 1 + i - start);
            }
            return maxLen;
        }

        // Time = O(n*m), Space = O(t), n length of strs array & m is avg length of each string in strs array, t is length of first string in array
        public static string LongestCommonPrefix(string[] strs)
        {
            char[] commPrefix = strs[0].ToCharArray();
            int matchedIdx = commPrefix.Length;
            for (int i = 1; i < strs.Length; i++)
            {
                matchedIdx = Math.Min(matchedIdx, strs[i].Length);
                for (int idx = 0; idx < matchedIdx; idx++)
                    if (commPrefix[idx] != strs[i][idx])    // exit as soon as 1st non-matching chars appear & update idx till where characters matched
                        matchedIdx = idx;
            }
            return new string(commPrefix,0,matchedIdx);
        }

        // Time = Space = O(n), n is length of 'title' string
        public static string CapitalizeTitle(string title)
        {
            char[] ans = new char[title.Length];
            int currWordLen = 0;
            for (int i = 0; i < title.Length; i++)
                if (title[i] == ' ')
                {
                    currWordLen = 0;
                    ans[i] = ' ';
                }
                else
                {
                    ans[i] = Char.ToLower(title[i]);
                    if (++currWordLen == 3)    // current word is longer than 2 chars which means First letter should be UpperCase
                        ans[i - 2] = Char.ToUpper(ans[i - 2]);
                }
            return new string(ans);
        }


        // Time = Space = O(n), n = of of characters in input string 's'
        public static string ZigzagConversion(string s, int numRows)
        {
            if (numRows == 1) return s;

            StringBuilder[] sbArr = new StringBuilder[numRows];
            for (int i = 0; i < numRows; i++) sbArr[i] = new StringBuilder();  // initialize the StringBuilder array

            int r = 0;
            bool goingDown = true;
            foreach (var ch in s)    // iterate thru all the characters in the input string
            {
                sbArr[r].Append(ch);
                // update the direction for next character
                if (r == 0) goingDown = true;
                else if (r + 1 == numRows) goingDown = false;
                r = goingDown ? r + 1 : r - 1;
            }

            string zigZag = "";
            foreach (var sb in sbArr) zigZag += sb.ToString();
            return zigZag;
        }


        // Time O(26*n) Space O(n), n = length of input string 's'
        public static int LongestRepeatingCharacterReplacement(string s, int k)
        {
            /*
            For each unique character present in 's'
            we run a loop in which we try to find the max possible len of string with one character only after making atmost k replacements
            we do this maximum of 26 times if all characters r present in the input string

            for calculating max Length with each characters we count how many diff character other than one we r trying to maximize we have got till now
            till its <=k we can keep maximizing string length

            post hitting k+1 diff characters we update the starting point => 1st different character that was found on idx + 1
            */

            HashSet<char> unqChars = new HashSet<char>();
            foreach (var ch in s) unqChars.Add(ch);

            int maxRepeatingChar = 0;
            foreach (var unqChar in unqChars)           // O(26)
            {
                int start = 0;
                Queue<MyCharNode> q = new Queue<MyCharNode>();
                for (int i = 0; i < s.Length; i++)      // O(n)
                {
                    if (s[i] != unqChar)   // diff character than one we are trying to maximize
                    {
                        q.Enqueue(new MyCharNode(s[i], i));
                        if (q.Count > k)   // need to update starting point
                        {
                            start = q.Dequeue().idx + 1;
                        }
                    }
                    maxRepeatingChar = Math.Max(maxRepeatingChar, 1 + i - start);

                    // no point in continuing if we already have got the max possible length
                    if (s.Length - start < maxRepeatingChar) break;
                }
            }
            return maxRepeatingChar;
        }

        // Time = O(n^2) | Space O(1), n = length of input string 's'
        public static int NumberOfSubstringsContainingAllThreeCharacters(string s)
        {

            #region ALGO
            //Brute force way is to check for each substring possible and see if it has atleast 1 of each req character a,b,c
            //we try having a counter for each char and once we found that we have atleast 1 of each

            //this is valid substring, now for all the remaining characters post this point will also yield a valid string // optimization
            //hence we can simple add length - currentIdx to final ans
            #endregion
            int l = s.Length, ans = 0;
            int[] counter = new int[3];
            for (int startIdx = 0; startIdx < l; startIdx++)
            {
                counter[0] = counter[1] = counter[2] = 0;
                for (int j = startIdx; j < l; j++)
                {
                    counter[s[j] - 'a']++;
                    if (counter[0] > 0 && counter[1] > 0 && counter[2] > 0)
                    {
                        ans += l - j;
                        break;  // breakout of inner loop as we can know each subtring from this point onwards will have atleast 1 of each char
                    }
                }
            }
            return ans;
        }

        // Time = O(n) | Space O(1), n = length of input string 's'
        public static int NumberOfSubstringsContainingAllThreeCharacters_Faster(string s)
        {
            #region ALGO
            //Not optimizing on below approach we see that we are uncessary starting from the start once we have a valid substring
            //better approach wud be once we get a valid substring we just see how many character from the start we can remove
            //till curr substring is no longer valid

            //now we add this count to the final ans
            //& we start the process again

            //again traversing thru characters till we found a valid string and than removing characters till string is no longer active
            //and adding counter to ans

            //& repeat
            #endregion
            int[] counter = new int[3];
            int ans = 0, i = -1, validTill = 0, l = s.Length;
            while (++i < l)
            {
                counter[s[i] - 'a']++;
                // we got valid string, now we try to see how many characters from start we can remove before its no longer valid
                while (counter[0] > 0 && counter[1] > 0 && counter[2] > 0)
                    --counter[s[validTill++] - 'a']; // increasing validTill counter tells us how many characters from start have been removed
                ans += validTill; // all characters till valid-1 wud yield in valid string when combined with characters till idx i
            }
            return ans;
        }


        // Time = Space = O(n), n = length of 'words'
        public static IList<string> FullJustify(string[] words, int maxWidth)
        {
            IList<string> justified = new List<string>();
            StringBuilder sb = new();
            List<string> curLine = new List<string>() { words[0] };
            int curLineLen = words[0].Length, gapCount, paddingReq, spaceBwWords, spacesRequiredForWords, extraSpacesWhichNeedToBeAddedToLeft;
            for (int i = 1; i < words.Length; i++)         // O(n)
            {
                string curWord = words[i];
                // adding word to exisiting line with a single space b/w them
                if (curLineLen + 1 + curWord.Length <= maxWidth)
                {
                    curLineLen += 1 + curWord.Length;
                    curLine.Add(curWord);
                }
                // no more words can be added to current without excedding maxWidth
                else
                {
                    // append curLine to ans before adding new word to fresh new line
                    gapCount = curLine.Count - 1;
                    if (gapCount > 0)
                    {
                        spacesRequiredForWords = curLineLen - gapCount;
                        paddingReq = maxWidth - spacesRequiredForWords;

                        spaceBwWords = paddingReq / gapCount;
                        while (spaceBwWords-- > 0) sb.Append(' ');
                        string minSpace = sb.ToString();
                        sb.Clear();

                        extraSpacesWhichNeedToBeAddedToLeft = paddingReq % gapCount;
                        sb.Append(curLine[0]);
                        for (int j = 1; j < curLine.Count; j++)
                        {
                            sb.Append(minSpace);
                            if (extraSpacesWhichNeedToBeAddedToLeft-- > 0) sb.Append(' ');
                            sb.Append(curLine[j]);
                        }
                        justified.Add(sb.ToString());
                        sb.Clear();
                    }
                    // take care of single word in line
                    else
                    {
                        sb.Append(curLine[0]);
                        for (int k = maxWidth - curLine[0].Length; k > 0; k--) sb.Append(' ');
                        justified.Add(sb.ToString());
                        sb.Clear();
                    }

                    // now add new word to the fresh new line
                    curLineLen = curWord.Length;
                    curLine = new List<string>() { curWord };
                }
            }

            sb.Append(curLine[0]);
            for (int j = 1; j < curLine.Count; j++)
                sb.Append(' ').Append(curLine[j]);

            // fill all remaining space on right with spaces
            for (int k = maxWidth - sb.Length; k > 0; k--) sb.Append(' ');
            // append last Line to ans
            justified.Add(sb.ToString());

            return justified;
        }


        // Time O(Max(n*m,l*n)) | Space O(n*m). n = length of 'wordDict', m = avg length of each word in wordDict, l = length of string 's'
        public static IList<string> WordBreakII(string s, IList<string> wordDict)
        {
            /* ALGO
            We create a Trie and add all the words from wordDict to trie
            now we start creating sentence for that we start from 0th index
            and for each word we try to see if given char is present in trie we move to next index if at any char we see we have form a word from trie
            we add that to sentence & start creating next word
            we stop at any point and go back it at any points we can't find next word in Trie

            also once we have found 1 word we try to go further to possibly find even larger word from trie
            once we reach end of the string 's' we add the current sentence to the list of answers.
            */
            Trie t = new();
            // add all words to Trie data-structure
            foreach (var word in wordDict)   // O(n)
                t.Add(word.ToCharArray());            // O(m)

            int l = s.Length;
            // now start build all possible sentence from 's'
            IList<string> ans = new List<string>();
            Create(0, "");                   // O(l*n)
            return ans;

            // local helper func
            void Create(int i, string sentence)
            {
                if (i == l) ans.Add(sentence);
                else
                {
                    TrieNode cur = t.root;
                    StringBuilder sb = new();
                    if (sentence.Length > 0) sb.Append(' ');   // add a space if current word is not the 1st word we are trying to add to sentence
                                                               // start build a word is next char is present in Trie else return
                    while (i < l)
                    {
                        cur = Trie.ValidPath(cur, s[i]);
                        if (cur != null)
                        {
                            sb.Append(s[i++]);
                            if (cur.isWord)  // found one valid word now recursively call the func to find other words
                                Create(i, sentence + sb.ToString());
                        }
                        // if valid path exists in Trie no points moving frwd hence return sentence cannot  be formed with current set of words
                        else return;
                    }
                }
            }
        }


        // Time O(n^2) | Space O(n), n = length of string 's'
        public static string ShortestPalindrome(string s)
        {
            /* ALGO
            1. for each char from the rtMostIdx we try to match with char from 0th idx
            2. if we are able to find a palindrome great
            3. else we add the last most char to a StringBuilder & move the --rtIdx
            4. as we have forcibly match the last char or remaining string
            5. Now repeat from step#1 till we find a palindrome or rt index reaches 0
            6. return whatever is in StringBuilder + input string 's'
            */
            int rtIdx = s.Length - 1, start, last;
            StringBuilder charAdded = new();
            while (rtIdx >= 0)
            {
                start = 0;
                last = rtIdx;
                // check if remaining string is palindrome
                while (start < last)
                    if (s[start] != s[last])
                        break;
                    else
                    {
                        start++;
                        last--;
                    }

                if (start >= last)     // palindrome found
                    break;
                else    // add last char and start matching again with reduced rt boundry
                    charAdded.Append(s[rtIdx--]);
            }
            return charAdded.ToString() + s;
        }


        // Time O(2^n) | Space O(n) | Auxillary Space O(n), n = length of 's'
        public static List<string> RemoveInvalidParentheses(string s)
        {
            /* ALGO
            1. Get the count of minNo of remvals required to make 's' valid
            - take min of (get minRemoval when iterating from left & n than right)
            2. Now we use Stack to keep track of all characters pushed so far, also easier when backtracking
            3. for each char there are 3 possibilities
                - if Letter, push to stack
                - if bracket
                - 1# use bracket
                - 2# don't use bracket
            5. once u reach end of string check is string is balanced and no of removals are <= minReqRemovals, append to HashSet
            6. at the end convert the set to List and return as we only want unique strings
             */
            Stack<char> st = new();
            // removing leading closing bracket
            for (int i = 0; i < s.Length; i++)                     // O(n)
                if (char.IsLetter(s[i])) st.Push(s[i]);
                else if (s[i] == '(')    // stop once u find a opening bracket
                {
                    // all prefix letters + get the entire string from current idx to end
                    s = new string(st.Reverse().ToArray()) + s.Substring(i);
                    break;
                }

            // removing open bracket from the end
            st.Clear();
            for (int i = s.Length - 1; i >= 0; i--)                  // O(n)
                if (char.IsLetter(s[i])) st.Push(s[i]);
                else if (s[i] == ')')    // stop once u find a closing bracket
                {
                    // get entire string from 0th index till current idx + all letter suffix
                    s = s.Substring(0, 1 + i) + new string(st.ToArray());
                    break;
                }

            // if after trimming nothing left return
            if (s.Length == 0) return new List<string>();

            int invalidBracketsLt = 0, invalidBracketsRt = 0, open = 0, minRemovals;
            for (int i = 0; i < s.Length; i++)                     // O(n)
                if (!char.IsLetter(s[i]))
                    if (s[i] == '(')
                        open++;
                    else if (--open < 0)
                    {
                        invalidBracketsLt++;
                        open = 0;   // reset
                    }
            // also append any left open bracket which did not find their closing
            invalidBracketsLt += open;
            open = 0;
            for (int i = s.Length - 1; i >= 0; i--)                  // O(n)
                if (!char.IsLetter(s[i]))
                    if (s[i] == ')')
                        open++;
                    else if (--open < 0)
                    {
                        invalidBracketsRt++;
                        open = 0;   // reset
                    }
            invalidBracketsRt += open;
            minRemovals = Math.Min(invalidBracketsLt, invalidBracketsRt);


            HashSet<string> set = new();
            st.Clear();
            Recursion(0, 0, 0);         // O(2^n)
            return set.ToList();

            // local helper func
            void Recursion(int i, int openBrackets, int removedSoFar)
            {
                if (i == s.Length)
                {
                    if (openBrackets == 0 && removedSoFar <= minRemovals) // balance
                        set.Add(new string(st.Reverse().ToArray()));
                }
                else if (char.IsLetter(s[i]))
                {
                    st.Push(s[i]);
                    Recursion(i + 1, openBrackets, removedSoFar);
                    st.Pop();
                }
                else if (openBrackets >= 0)    // balance should never go -ve
                {
                    // use
                    st.Push(s[i]);
                    Recursion(i + 1, openBrackets + (s[i] == '(' ? 1 : -1), removedSoFar);
                    st.Pop();

                    // remove
                    if (removedSoFar < minRemovals)
                        Recursion(i + 1, openBrackets, removedSoFar + 1);
                }
            }
        }


        // Time O(n^2) | Space O(n), n = length of string 's' | TLE
        public static long MaxProductOfTwoPalindromicSubstringsBruteForce(string s)
        {
            /* ALGO
            1. Find the longest odd length palidrome possible for each index in 's'
            2. Now in first loop divide the s into 2 parts from idx 1...len-2 so we kept a left and right part
            3. Now for each part check all the index and get the longest possible palindrome within the substring limit/bounder
            4. Multiply the length of longest from each part and if its greather than update maxProduct.
            */
            int l = s.Length, lt, rt;
            long maxPorductOfOddLengthPalindromes = 1;
            if (l == 2) return maxPorductOfOddLengthPalindromes;

            // find the longest odd pali from each idx
            int[] longestPali = new int[l];
            for (int centre = 0; centre < l; centre++)                 // O(n^2)
            {
                longestPali[centre]++;   // each single char is pali of len 1
                lt = centre - 1;
                rt = centre + 1;
                while (lt >= 0 && rt < l && s[lt--] == s[rt++])
                    longestPali[centre] += 2;     // as we increase length by 2 chars
            }
            // Divide into 2 parts, dividing idx is considered part of left
            for (int divideFrom = 0; divideFrom < l - 1; divideFrom++) // O(n^2)
            {
                int cur = -1, longestLt = 1, longestRt = 1, paliLenAtCurIdx, half, maxExtent;

                // find longest palidrome in left half
                while (++cur <= divideFrom)
                {
                    paliLenAtCurIdx = longestPali[cur];
                    half = (paliLenAtCurIdx - 1) / 2;
                    // take min of the half length of pali or the boundry/division point
                    maxExtent = Math.Min(half, divideFrom - cur);
                    // update longest pali found so far
                    longestLt = Math.Max(longestLt, 1 + (2 * maxExtent));
                }
                // find longest palidrome in right half
                while (cur < l)
                {
                    paliLenAtCurIdx = longestPali[cur];
                    half = (paliLenAtCurIdx - 1) / 2;
                    // take min of the half length of pali or the boundry/division point
                    maxExtent = Math.Min(half, -1 + cur - divideFrom);
                    // update longest pali found so far
                    longestRt = Math.Max(longestRt, 1 + (2 * maxExtent));

                    cur++;
                }
                // update the maximum product of the 2 odd length pali found of each side
                maxPorductOfOddLengthPalindromes = Math.Max(longestLt * longestRt, maxPorductOfOddLengthPalindromes);
            }
            return maxPorductOfOddLengthPalindromes;
        }

        // Time O(n) | Space O(n), n = length of string 's'
        // Ref https://youtu.be/fWFrAnNQKH8
        public static long MaxProductOfTwoPalindromicSubstrings(string s)
        {
            /* ALGO
            1. Use ManacherAlgorithm to find in linear time all the longest possible palindrome length for each index in 's' of odd length
            2. Now transfer o/p of manacher's into 2 arrays:
            - 1st contains length of longest palindrome on left of each index
            - 2nd contains length of longest palindrome on right of each index
            3. Iterate thru 0..l-2 indicies and for each index update the 'maxProduct' if greater freq of LongestPaliOnleft[i]*LongestPaliOnright[i+1] is found
             */
            int l = s.Length;
            if (l == 2) return 1;       // base case

            // find the longest odd pali from each idx
            int[] longestPali = ManacherAlgorithm(s);           // O(n)
            // s   a b a c a b a d e
            // Ex: 1 3 1 7 1 3 1 1 1

            // From Manacher's we find the longest palindrome avaliable on left for each idx moving left..right
            long[] longestPaliOnLeft = new long[l];
            for (int i = 0; i < l; i++)
            {
                var paliLen = longestPali[i];
                // update cur idx
                longestPaliOnLeft[i] = Math.Max(1, longestPaliOnLeft[i]);

                // if we have a pali of len 3 we know what's the no of idx we need to move before we can have that Palindrome under
                var half = paliLen / 2;
                var j = i + half;
                longestPaliOnLeft[j] = Math.Max(longestPaliOnLeft[j], paliLen);
            }
            // update for shorter palindrome so for large palindrome the index before show have -2 freq atleast
            for (int i = l - 2; i >= 0; i--)
                longestPaliOnLeft[i] = Math.Max(longestPaliOnLeft[i], longestPaliOnLeft[i + 1] - 2);
            // Now we have
            // Ex: 1 1 3 1 3 5 7 1 1


            // update the max seen so far for each idx left..right
            var max = longestPaliOnLeft[0];
            for (int i = 1; i < l; i++)
            {
                max = Math.Max(max, longestPaliOnLeft[i]);
                longestPaliOnLeft[i] = max;
            }
            // after above operation we have
            // Ex: 1 1 3 3 3 3 7 1 1

            //-----------------------------------------------------------//
            // From Manacher's we find the longest palindrome avaliable on right for each idx moving right..left
            // Ex: 1 3 1 7 1 3 1 1 1
            long[] longestPaliOnRight = new long[l];

            for (int i = l - 1; i >= 0; i--)
            {
                var paliLen = longestPali[i];
                // update cur idx
                longestPaliOnRight[i] = Math.Max(1, longestPaliOnRight[i]);

                // if we have a pali of len 3 we know what's the no of idx we need to move before we can have that Palindrome under
                var half = paliLen / 2;
                var j = i - half;
                longestPaliOnRight[j] = Math.Max(longestPaliOnRight[j], paliLen);

            }
            // update for shorter palindrome so for large palindrome the index before show have -2 freq atleast
            for (int i = 1; i < l; i++)
                longestPaliOnRight[i] = Math.Max(longestPaliOnRight[i], longestPaliOnRight[i - 1] - 2);
            // Now we have
            // Ex: 7 5 3 1 3 1 1 1 1

            // update the max seen so far for each idx right..left
            max = longestPaliOnRight[l - 1];
            for (int i = l - 2; i >= 0; i--)
            {
                max = Math.Max(max, longestPaliOnRight[i]);
                longestPaliOnRight[i] = max;
            }
            // after above operation we have
            // Ex: 7 5 3 3 3 1 1 1 1

            // Finally update the maximum product of the 2 odd length pali found of each side
            long maxProduct = 1;
            for (int i = 0; i < l - 1; i++)
                maxProduct = Math.Max(maxProduct, longestPaliOnLeft[i] * longestPaliOnRight[i + 1]);
            return maxProduct;

            ///////// local helper func
            // Time = Space = O(n), n = length of input string 's'
            // ref https://leetcode.com/problems/longest-palindromic-substring/solutions/4212241/98-55-manacher-s-algorithm
            static int[] ManacherAlgorithm(string s)
            {
                string T = "^#" + string.Join("#", s.ToCharArray()) + "#$";
                int n = T.Length;
                int[] P = new int[n];
                int CentreOfPalindrome = 0, RightOfPalindrome = 0;

                for (int i = 1; i < n - 1; i++)
                {
                    P[i] = (RightOfPalindrome > i) ? Math.Min(RightOfPalindrome - i, P[2 * CentreOfPalindrome - i]) : 0;
                    while (T[i + 1 + P[i]] == T[i - 1 - P[i]])
                        P[i]++;

                    if (i + P[i] > RightOfPalindrome)
                    {
                        CentreOfPalindrome = i;
                        RightOfPalindrome = i + P[i];
                    }
                }

                // convert palindrome length array back to original length of input string 's'
                int[] mana = new int[s.Length];
                int j = 0;
                for (int i = 2; i < n - 2; i += 2)
                    mana[j++] = P[i];
                return mana;
            }
        }


        // Time O(n) | Space O(1), n = length of input string 'gene'
        public static int SteadyGene(string gene)
        {
            int l = gene.Length, reqFreq = l / 4;
            Dictionary<char, int> geneCount = new()
            {
                {'C',0},
                {'G',0},
                {'A',0},
                {'T',0},
            };

            // get the frequency for all 4 diff genes
            foreach (var g in gene)                              // O(n)
                geneCount[g]++;

            int unwantedGenesCount = 0;
            Dictionary<char, int> genesInExcess = new();
            foreach (var geneFreq in geneCount)                  // O(4)
                if (geneFreq.Value > reqFreq)
                {
                    unwantedGenesCount += geneFreq.Value - reqFreq;
                    genesInExcess[geneFreq.Key] = geneFreq.Value - reqFreq;
                }

            // no modification req
            if (genesInExcess.Count == 0) return 0;

            // need to find the smallest substring which has all the required Excess gene which are in surplus
            int i = -1, left = 0, allExtraGeneFound = 0, minReplaceLen = int.MaxValue;
            int[] curGeneCount = new int[26];
            while (++i < l)                                        // O(n)
                if (genesInExcess.TryGetValue(gene[i], out int freq) && ++curGeneCount[gene[i] - 'A'] <= freq)
                {
                    ++allExtraGeneFound;

                    // we have found all extra genes which can be replace with what we want to get all genes in 4's multiple
                    while (allExtraGeneFound == unwantedGenesCount)
                    {
                        minReplaceLen = Math.Min(minReplaceLen, 1 + i - left);
                        // smaller substring to be replaced found
                        if (minReplaceLen == unwantedGenesCount)
                            return minReplaceLen;

                        var ch = gene[left++];
                        if (genesInExcess.TryGetValue(ch, out int excessValue) && --curGeneCount[ch - 'A'] < excessValue)
                            allExtraGeneFound--;
                    }
                }
            return minReplaceLen;
        }

        // Time O(n) | Space O(26), n = length of 's'
        public static string LastNonEmptyString(string s)
        {
            /* ALGO
            Count the freq of each char
            Now go thru the freq of each and create a HashSet which only has characters with freq == maxFreq
            Now since we know only max freq char wud be left after all removal
            from the end iterate and add to Char stack if that char is present in above hashset (& remove from set after adding to stack)
            at end return string formed by Stack chars
             */
            int[] charFreq = new int[26];
            int maxFreq = 0, l = s.Length;

            for (int i = 0; i < l; i++)
                charFreq[s[i] - 'a']++;

            HashSet<char> charsWithMaxFreq = new();
            for (int i = 0; i < 26; i++)
                if (charFreq[i] > maxFreq)
                {
                    maxFreq = charFreq[i];
                    charsWithMaxFreq = new HashSet<char>() { (char)(i + 'a') };
                }
                else if (charFreq[i] == maxFreq)
                    charsWithMaxFreq.Add((char)(i + 'a'));

            Stack<char> st = new();
            // find 1st occurrences from the end and add to final ans for each max freq char
            for (int i = l - 1; i >= 0; i--)
                if (charsWithMaxFreq.Contains(s[i]))
                {
                    st.Push(s[i]);
                    charsWithMaxFreq.Remove(s[i]);
                }
            return new string(st.ToArray());
        }


        // Time = Space = O(n), n = length of string 'word'
        public static long WonderfulSubstrings(string word)
        {
            /* ALGO
            1. problem can be broken down into 2 parts
            2. we need to find no of prefix which has appeared before current index which have excatly same words parity for all the 10 characters,
                i.e. if cur string as even no of 'a' there exist a prefix which also has even no of 'a'
            3. also we need to find no of occurence where a prefix is different by from current string parity for just one word
                we can do so by left shiting 1 by 0..9 times to test parity for all character 1 by 1.
                i.e. if cur string has even 'a' and odd 'b' and if there exist a prefix which has (even'a' and even 'b') OR (odd 'a' and odd 'b')
                this ensure the string in b/w the prefix and cur index will have all characters in even parity and just 1 in odd
            4. we use BitMask to store the even/odd parity as there are only 10 characters
                1st/rightMost bit indiciates parity for 'a'
                2nd bit from rt shows parity for 'b'
                3rd bit from rt shows parity for 'c'
                ...
                10th bit from rt show parity for 'j'
            5. use XOR operator we update parity for a given character if its on it gets changed to 0 and vice-versa
            6. All keep updating the Dictionary which stores freq of all diff bitMask seen till now.
             */
            Dictionary<int, int> maskFreq = [];
            int bitMask = 0;
            long ans = 0;
            // default entry with all characters even times    
            maskFreq[bitMask] = 1;
            foreach (var ch in word)         // O(n)
            {
                bitMask ^= (1 << (ch - 'a'));
                // check if bitmask which differes by atmost 1 bit has ever appeared before means one characters b/w then & now have odd occurence
                for (int i = 0; i < 10; i++)       // O(10)
                    if (maskFreq.TryGetValue(bitMask ^ (1 << i), out int oneBitDiffFreq))
                        ans += oneBitDiffFreq;

                // check if same bitmask has appear before means all characters b/w then & now have even occurence
                if (maskFreq.TryGetValue(bitMask, out int freq))
                {
                    ans += freq;
                    maskFreq[bitMask]++;
                }
                else maskFreq[bitMask] = 1;
            }
            return ans;
        }


        // Time O(Max(n*l,m)) | Space O(n*l+m), n = length of 'dictionary', l = avg length of each word in 'dictionary' & m = length of 'sentence'
        public static string ReplaceWords(List<string> dictionary, string sentence)
        {
            /* ALGO
            1. Create a trie and all the words in 'dictionary' list
            2. Now split the 'sentence' by white-space
            3. Iterate thru list of words in sentence
            4. For each word start from the root of the trie and see if valid path exists
            5. if at any point/at any char we found the prefix (by checking isWord)
                we dont need to iterate further and can return true to signify a prefix
                is found (its automatically the smallest because we are using tried
                and moving from 0th node)
            6. if func FindPrefix() return true add the prefix in StringBuilder
                to final result, else add the original word meaning no prefix was found.
             */
            StringBuilder sb = new(), curWordPrefix = new();
            Trie t = new();
            foreach (var word in dictionary)     // O(n)
                t.Add(word.ToCharArray());                    // O(l)

            var words = sentence.Split(' ');    // O(m)
            for (int i = 0; i < words.Length; i++)     // O(m)
            {
                if (i > 0) sb.Append(" ");

                // found a prefix for cur word appending it to final List
                if (t.FindPrefix(t.root, words[i], 0, curWordPrefix))
                    sb.Append(curWordPrefix.ToString());
                // no prefix found append the entire word
                else sb.Append(words[i]);

                curWordPrefix.Clear();
            }
            return sb.ToString();
        }


        // Time O(n) | Space O(1), n = length of 'word'
        public static int MinimumPushes(string word)
        {
            /* ALGO
            1. Since any character can be mapped to any key
            2. it makes sense to map most freq characters such that we
                can get them in 1 push.
            3. since we only have 8 distinct keys so 1st set of eight most
                frequent characters get mapped 1st & 1 push we can get them
                a. second set of most freq get mapped to 2-push
                b. 3rd set get mapped to 3-push
                c. last 2 characters least freq get mapped to 4-push
            7. Now we can simply count the freq of all characters in input
            8. sort the characters by non-increasing frequencies
            9. not we multiply each character freq * by no pushed required
                to get it & add it to grand total
             */
            int[] charFreq = new int[26];
            // fetch the freq of all distinct characters
            foreach (var c in word)  // O(n)
                charFreq[c - 'a']++;
            // sort in Non-increasing order
            Array.Sort(charFreq, (a, b) => b.CompareTo(a));   // O(26log26) ~O(1)
            int unqKeys = 8, pushCount = 0;
            // first 8 most freq character get mapped to 1st place on 8 different keys
            // second set of 8 characters get mapped to 2nd place on all keys and so on ...
            // if any character has 0 freq it does add to pushCount
            for (int i = 0; i < 26; i++)   // O(26)
                pushCount += charFreq[i] * (1 + (i / unqKeys)); // freq of char * times key has to be pushed to get the character
            return pushCount;
        }


        // Time = Space = O(n* k), n = length of 'folder' and k = avg length of each folder in array
        public static IList<string> RemoveSubfolders(string[] folder)
        {
            /* ALGO
            1. Add all folder to the Trie data-structure (each folder split by '/') is new sub-folder
            2. also remeber to mark the last sub-folder as 'isFolder'
            3. Once all folder are added we can simply iterate thru Trie to only add root folder which is identified by isFolder=true
            4. and stop iterating down-stream as anything under it is sub-folder
             */
            IList<string> ans = new List<string>();
            TrieFileSystem t = new();
            // add all the folder in the 'Trie' data-structure
            foreach (var fol in folder) t.Add(fol);
            // now iterate over the trie to only add Root folders
            t.FindRootOnly(ans, t.root, new Stack<string>());
            return ans;

        }


        // Time O(n^2) | Space O(n), n = length of 's'
        internal static int MaxUniqueSplit(string s)
        {
            /* ALGO
            1. Try all possible words starting from 0th idx of length 1..max
            2. if curWord is not present in HashTable, add it and recursively
                call the func to see how many unq words we can get before
                reaching last idx
            3. during backtracking if word was added remove it so that next char can be included as steps and be repeated.
            4. at the end when idx==l, update maxUniqueSplits with hashset count indicating count of unq words
             */
            HashSet<string> unq = new();
            int maxUniqueSplits = 0, l = s.Length;
            Try(0);
            return maxUniqueSplits;

            // local helper func
            void Try(int idx)
            {
                if (idx == l) maxUniqueSplits = Math.Max(maxUniqueSplits, unq.Count);
                else
                {
                    StringBuilder sb = new();
                    for (int i = idx; i < l; i++)
                    {
                        sb.Append(s[i]);
                        var curWord = sb.ToString();
                        if (!unq.Contains(curWord))
                        {
                            unq.Add(curWord);
                            Try(i + 1);
                            unq.Remove(curWord);
                        }
                    }
                }
            }
        }
    }
}
