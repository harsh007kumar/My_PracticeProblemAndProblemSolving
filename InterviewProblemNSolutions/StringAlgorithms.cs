using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Activation;
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

    }
}
