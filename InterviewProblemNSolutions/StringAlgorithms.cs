using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Time Complexity O(R*C), R = rows & C = Columns in input 2-D char array || Auxillary Space O(1)
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
    }
}
