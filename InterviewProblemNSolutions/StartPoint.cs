using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblemNSolutions
{
    class StartPoint
    {
        static void Main(string[] args)
        {
            DutchFlagProblem();
            EventAndDelegate();

            // Learn Dynamic Programming (Memoization & Tabulation)
            MinimumStepsToMinimizeToOne();

            // String Matching Algorithm
            BruteForceWay();
            RabinKarpStringMatchingAlgo();
            KnuthMorrisPratt_KMP_PatternMatchingAlgo();

            ReverseString();
            ReverseSentence();
            StringPermutationAlgorithm();
            CombinationOfCharactersInString();
            RecursivelyRemoveAdjacentCharacters();
            MinWindowContainingAllCharacters();
            FindPatternPresentIn2DCharArray();
            Console.ReadKey();
        }

        /// <summary>
        /// The Dutch national flag
        /// </summary>
        public static void DutchFlagProblem()
        {
            Utility.Print("The Dutch national flag (DNF) problem");
            #region Explanation of Ques
            // Ques.            We have 3 kind of repeating no's/color/characters Ex- 1,2,3 in an array
            // Expected Ans.    Sort array such that each input type is stored together

            // Ex-              1, 3, 2, 1, 2, 1, 2, 1, 1, 1, 3, 2, 1, 3
            // 1st Possible Ans-  2, 2, 2, 2, 1, 1, 2, 1, 1, 1, 1, 3, 3, 3
            // 2nd Possible Ans-  2, 2, 2, 2, 3, 3, 3, 1, 1, 1, 1, 1, 1, 1
            // Sequence in which element are stored is not important its just that each input type should be bunched together.
            #endregion
            int[][] arrays = { new int[]{ 3, 2, 1, 3, 3, 3, 1, 2, 1, 1, 1, 3, 2, 1, 2 }
                               , new int[]{ 1, 3, 2, 1, 2, 1, 2, 1, 1, 1, 3, 2, 1, 3 } };           // Input array
            foreach (var arr in arrays)
            {
                Console.Write("\n\nInput Array : ");
                arr.Print();
                int low, mid, high;         // to save current index of 3 distinct inputs
                //// My Orignal Solution Time O(n+k) 1 Pass
                //low = mid = 0;              // start from left and increment by 1
                //high = arr.Length;          // start from right and decreament by 1
                //int i = 0;
                //while (i < arr.Length)
                //{
                //    i = low > mid ? low : mid;
                //    if (i == high) break;
                //    Console.Write($"For {i} Index Array is : ");
                //    // Arr[i] is low i.e, 1
                //    if (arr[i] == 1)
                //    {
                //        if (low == i)
                //            low++;
                //        else
                //            Utility.Swap(ref arr[low++], ref arr[i]);
                //    }
                //    // Arr[i] is mid i.e, 2
                //    else if (arr[i] == 2)
                //    {
                //        if (mid == i)
                //            mid++;
                //        else
                //            Utility.Swap(ref arr[mid++], ref arr[i]);
                //    }
                //    // Arr[i] is high i.e, 3
                //    else if (arr[i] == 3)
                //        Utility.Swap(ref arr[i], ref arr[--high]);
                //    arr.Print();
                //}

                #region Alternative Best Efficient Solution || Link https://www.educative.io/edpresso/the-dutch-national-flag-problem-in-cpp || Time O(n) 1-Pass solution
                low = mid = 0;              // start from left and increment by 1
                high = arr.Length-1;
                while (mid <= high)
                {
                    if (arr[mid] == 1)
                        Utility.Swap(ref arr[low++], ref arr[mid++]);
                    else if (arr[mid] == 2)
                        mid++;
                    else
                        Utility.Swap(ref arr[mid], ref arr[high--]);
                    arr.Print();
                }
                #endregion
            }
        }
    
        public static void EventAndDelegate()
        {
            Utility.Print("Creating custom Event of custom Delegate type & registering an method which matchs delegate signature with that event");
            new EventDemo();
        }

        // Learn Dynamic Programming (Memoization & Tabulation)
        // KeepOnCoding https://youtu.be/f2xi3c1S95M
        /// <summary>
        /// Calculate the minimum steps required to minimize any given no to '1', using below avaliable rules/steps only
        /// Number - 1 || Number / 3 || Number / 2
        /// </summary>
        public static void MinimumStepsToMinimizeToOne()
        {
            Utility.Print("MinimumStepsToMinimize_NoTo1 || Dynamic Programming");
            Console.WriteLine("\n------------ Recursive approach");
            int[] numArray = { 6, 5, 4, 10, 15, 25, 500, 45123, 75845, 13245, 99589 };
            foreach (var num in numArray)
                Console.WriteLine($"\n Minimum Steps Requied to minimize '{num}' to '1' : \t{DynamicProgramming.GetMinSteps_Recursive(num)}");



            Console.WriteLine("\n------------ Dynamic Programming (Memoization) || Top-Down approach");
            // Memoization
            // create an array to update intermediate value as & when encoutnered while calculating min steps for a given 'num'
            int[] memo = new int[numArray.Max() + 1];
            foreach (var num in numArray)
                Console.WriteLine($" Minimum Steps Requied to minimize '{num}' to '1' : \t{DynamicProgramming.GetMinSteps_DP_Memo(num, ref memo)}");



            Console.WriteLine("\n------------ Dynamic Programming (Tabulation) || Bottom-up approach");
            // Tabulation
            // create an array to store all possible value b/w 1 to largest input num
            int[] tab = DynamicProgramming.GetMinSteps_DP_Tab(numArray.Max() + 1);
            foreach (var num in numArray)
                Console.WriteLine($" Minimum Steps Requied to minimize '{num}' to '1' : \t {tab[num]}");
        }

        public static void BruteForceWay()
        {
            Utility.Print("Brute Force Excat String Match");
            string input = "My name is Harsh";
            string pattern = "Harsh";
            int foudnAt = StringAlgorithms.BruteForceStringMatch(input, pattern);
            if (foudnAt != -1) Console.WriteLine($" Pattern '{pattern}' first found at index '{foudnAt}' in Input string \"{input}\"");
            else Console.WriteLine($" Pattern '{pattern}' Not found in Input string \"{input}\"");
        }

        public static void RabinKarpStringMatchingAlgo()
        {
            Utility.Print("Rabin-Karp String Matching (p. 657)");
            string[] inputArr = { "My name is Harsh", "GEEKS FOR GEEKS" };
            string[] patternArr = { "Harsh", "GEEK" };
            var primeNo = 101;
            foreach (var input in inputArr)
                foreach (var pattern in patternArr)
                    StringAlgorithms.RabinKarpStringMatch(input, pattern, primeNo);
        }

        // Tushar Roy https://youtu.be/GTJr8OvyEVQ
        public static void KnuthMorrisPratt_KMP_PatternMatchingAlgo()
        {
            Utility.Print("Knuth–Morris–Pratt(KMP) Pattern Matching(Substring search)");
            string[] inputArr = { "My name is Harsh", "GEEKS FOR GEEKS" };
            string[] patternArr = { "Harsh", "GEEK" };
            foreach (var input in inputArr)
                foreach (var pattern in patternArr)
                    StringAlgorithms.KMPStringMatch(input, pattern);
        }

        public static void ReverseString()
        {
            Utility.Print("Problem - 5/6/7 Give an algorithm for reversing a string.(p. 689)");
            string[] inputs = { "Harsh", "banana", "lalal" };
            foreach (var str in inputs)
            {
                Console.WriteLine($" \t'{StringAlgorithms.ReverseString(str)}'");
                StringAlgorithms.ReverseStringInPlace(str.ToCharArray());
                Console.WriteLine();
            }
        }

        public static void ReverseSentence()
        {
            Utility.Print("Problem - 9 Give an algorithm for reversing words in a sentence.(p. 691)");
            string input = "This is a Career Monk String";
            Console.WriteLine($" Sentence : \t'{input}'\n In-Reverse : \t'{StringAlgorithms.ReverseSentence(input.ToCharArray())}'");
            Console.WriteLine($" Sentence : \t'{input}'\n In-Reverse : \t'{StringAlgorithms.ReverseSentenceInPlace(input.ToCharArray())}'");
        }

        public static void StringPermutationAlgorithm()
        {
            Utility.Print("Problem - 10 Permutations of a string[anagrams]:(p. 691)");
            string input = "aabc";
            StringAlgorithms.StringPermutation(input);
        }

        public static void CombinationOfCharactersInString()
        {
            // Combination of Characters in String (All subsets of characters)
            Utility.Print("Problem - 11 Combinations Combinations of a String: (p. 692)");
            string input = "aabc";
            StringAlgorithms.CombinationOfCharacters(input);
        }

        public static void RecursivelyRemoveAdjacentCharacters()
        {
            Utility.Print("Problem - 12 Recursively removing the adjacent characters if they are the same.(p. 692)");
            // For example, ABCCBCBAnnnnnn -> ABBCBA ->ACBA
            var input = "AABCCBCBAnnnnnn";
            StringAlgorithms.RecursiveRemoveAdjacentCharacters(input);
        }

        public static void MinWindowContainingAllCharacters()
        {
            Utility.Print("Problem - 13 Minimum window in input string which will contain all the characters given in char array(p. 693)");
            var input = "ABB$ACBAA$";
            var chArray = "A$AB";
            StringAlgorithms.MinWindowContainingAllCharacters(input,chArray);
        }

        public static void FindPatternPresentIn2DCharArray()
        {
            Utility.Print("Problem-14  We are given a 2D array of characters and a character pattern. " +
                "Give an algorithm to find if the pattern is present in the 2D array.(pp. 694 - 695)");
            char[,] input2D = { { 'A', 'C', 'P', 'R', 'C' },
                                { 'X', 'S', 'O', 'P', 'C' },
                                { 'V', 'O', 'V', 'N', 'I' },
                                { 'W', 'G', 'F', 'M', 'N' },
                                { 'Q', 'A', 'T', 'I', 'T' } };
            string pattern = "MICROSOFT";   // for more complex sample pattern use = QWGFINIPPCAX
            input2D.Print();
            pattern.Show();
            StringAlgorithms.FindPatternIn2DCharArray(input2D, pattern);
        }
    }
}
