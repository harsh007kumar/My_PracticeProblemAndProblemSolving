using Microsoft.Win32;
using Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

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
            PrintInterLeavingsOfGivenTwoString();
            ReplaceSpaceInStringWithGivenCharacters();

            // Sorting Problem
            ElectionWinner();
            Merge_BinA_WhichHasExtraSpaceEqualToB();

            // Searching Problem
            DetectDuplicate();
            FindMaxRecurrence();
            FindMissingNo();
            FindPairWhoseSumIsClosetToGivenValue();
            FindTripletWhoseSumIsClosetToGivenValue();

            Console.ReadKey();
        }

        /// <summary>
        /// The Dutch national flag || Time Complexity O(n) || Space O(1)
        /// can also be solved by 3-way QuickSort for implementation check : 'Sort' class >> QuickSort3Way() [avaliable under Sorting project]
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
                #region My Orignal Solution Time O(n+k) 1 Pass
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
                #endregion

                #region Alternative Best Efficient Solution || Link https://www.educative.io/edpresso/the-dutch-national-flag-problem-in-cpp || Time O(n) 1-Pass solution
                low = mid = 0;              // start from left and increment by 1
                high = arr.Length-1;
                var pivot = 2;              // middle no of any 3 given no's in input
                while (mid <= high)         // similar approach used in 3-Way QuickSort solution
                {
                    if (arr[mid] < pivot)
                        Utility.Swap(ref arr[low++], ref arr[mid++]);
                    else if (arr[mid] == pivot)
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

        public static void PrintInterLeavingsOfGivenTwoString()
        {
            // explanation of algo https://www.youtube.com/watch?v=jspbN5bNPqM
            // GFG https://www.geeksforgeeks.org/print-all-interleavings-of-given-two-strings/
            Utility.Print("Problem-15  Given two strings str1 and str2, write a function that prints all interleavings of the given two strings.(p. 697)");
            var firstString = "AB";
            var secondString = "12";
            StringAlgorithms.PrintInterleavings(firstString, secondString);
        }

        public static void ReplaceSpaceInStringWithGivenCharacters()
        {
            Utility.Print("Problem-17  Write a method to replace all spaces in a string with ‘%20’." +
                "Assume string has sufficient space at end of string to hold additional characters.(pp. 698 - 699)");
            var input = "Harsh is my name";
            var replaceWith = "%20";
            StringAlgorithms.ReplaceSpaceWithGivenChars(input,replaceWith);
        }

        public static void ElectionWinner()
        {
            Utility.Print("Problem-3&4  Given an array A[0 ...n – 1], where each element of the array represents a vote in the election." +
                "Assume that each vote is given as an integer representing the ID of the chosen candidate." +
                "Give an algorithm for determining who wins the election.(p. 532)");
            Console.WriteLine();

            int[] input = { 2, 5, 8, 9, 4, 6, 7, 12, 5, 4, 3, 8, 9, 6, 6, 3, 5, 5, 2, 3, 1, 0 };
            input.Print("Input Array");
            #region Handling corner cases no votes or just 1 or 2 votes (candidate who got 1st vote wins)
            if (input == null) return;
            else if (input.Length <= 2)
            {
                Console.WriteLine($" Winning candidate of the election is '{input[0]}'");
                return;
            }
            #endregion

            
            // if we are allowed to use little extra space & Min - Max ID's of all candidate are known can use 'Counting Sort' 
            input = Sorting.Sort.Countsort(input, 0, 12);   // Time O(n), N = no of votes || Space O(k), k = no of candidates

            // To Sort the array without using extra space
            //Sorting.Sort.Heapsort(ref input);               // Time O(nLogn) || Space O(1)
            input.Print("after Sorting Using COUNT SORT ");

            // check for reapted ID's and if found one update its count and at last update WinnerCount and WinnerID
            int winnerCount = 0, winnerID = -1, count = 1;
            for (int i = 0; i < input.Length - 1; i++)      // Time O(n)
            {
                if (input[i] == input[i + 1])
                    count++;
                else
                {
                    if (count > winnerCount)
                    {
                        winnerCount = count;
                        winnerID = input[i];
                    }
                    count = 1;
                }
            }
            if (winnerID != -1) Console.WriteLine($" Winning candidate of the election is '{winnerID}' and got max votes : {winnerCount}");
        }

        public static void Merge_BinA_WhichHasExtraSpaceEqualToB()
        {
            Utility.Print("Problem-33  There are two sorted arrays A and B. The first one is of size m + n containing only m elements." +
                "Another one is of size n and contains n elements. Merge these two arrays into the first array of size m + n such that the output is sorted.(p. 543)");
            int[] a = new int[10];      // size m + n
            int[] b = new int[4];       // size n

            #region Initialize Input arrays
            Console.WriteLine();
            int i = 10, j;
            for (j = 0; j < b.Length; j++)
            {
                b[j] = j + 20;
                a[j] = (i % 2 == 0) ? b[j] + i : b[j] - i;
                i++;
            }
            for (i = j; i < a.Length - b.Length; i++)
                a[i] = i + 10;
            
            var index = a.Length - 1;   // last index in bigger array i.e, 'a'
            j = b.Length - 1;           // index of largest element present in 'b'
            i = index - b.Length;       // index of largest element present in 'a'

            // sort & print
            Sort.Quicksort(a, 0, i);
            Sort.Heapsort(ref b);
            a.Print("First Array");
            b.Print("Second Array");
            #endregion

            // logic is to start filling from back as its empty
            while (index>=0)
            {
                if (i < 0)
                    a[index] = b[j--];
                else if (j < 0)
                    a[index] = a[i--];
                else if (a[i] > b[j])
                    a[index] = a[i--];
                else if(a[i] <= b[j])
                    a[index] = b[j--];
                index--;
            }
            a.Print("After Merging First and Second array into First");
        }

        public static void DetectDuplicate()
        {
            Utility.Print("Problem-4 Detect Duplicates in Array containing Positive values in the range [0 .. N-1] (p. 562)");
            int[] input = { 3, 2, 1, 3, 4, 5, 1 };
            input.Print("Input Array");
            SearchAlgorithms.DetectDuplicate(input);
        }

        public static void FindMaxRecurrence()
        {
            Utility.Print("Problem-5  Given an array of n numbers. Give an algorithm for finding the element which appears the max times(p. 564)");
            int[] input = { 3, 5, 1, 3, 3, 5, 1, 5, 5 };
            input.Print("Input Array");
            SearchAlgorithms.MaxRecurrence(input);
        }

        public static void FindMissingNo()
        {
            Utility.Print("Finding the Missing Number: (p. 568)");
            int[] input = new int[15];
            for (int i = 0; i < input.Length - 1; i++)
                input[i] = i + 1;
            input.Print("Input Array");
            SearchAlgorithms.MissingNo(input);
        }

        // GFG https://www.geeksforgeeks.org/given-sorted-array-number-x-find-pair-array-whose-sum-closest-x/
        public static void FindPairWhoseSumIsClosetToGivenValue()
        {
            Utility.Print("Problem - 25 Given an array of n elements. Find two elements in the array such that their sum is equal to given element K.(p. 572)");
            int[] input = { 3, 8, 2, 5, 9, 1, 15, 7, 45 };
            input.Print("Input");
            int sum = 18;
            Console.WriteLine($"Find Pair whose Sum matches or closet to {sum}");
            Sort.Heapsort(ref input);   // Time O(nLogn) can skip this step if array is already sorted

            SearchAlgorithms.PairWhoseSumIsClosestToGivenValue(input, sum);     // O(n)
        }

        public static void FindTripletWhoseSumIsClosetToGivenValue()
        {
            Utility.Print("Problem-36  Given an array of n integers, find three integers whose sum is closest to GivenNo/zero.(p. 578)");
            int[] input = { 3, -8, 2, 5, 9, -1, 15, 7, 45, -5, -15 };
            input.Print("Input");
            int sum = 0;
            Console.WriteLine($"Find Pair whose Sum matches or closet to {sum}");
            Sort.Heapsort(ref input);   // Time O(nLogn) can skip this step if array is already sorted

            SearchAlgorithms.TripletWhoseSumIsClosetToGivenValue(input, sum);   // O(n^2)
        }
    }
}
