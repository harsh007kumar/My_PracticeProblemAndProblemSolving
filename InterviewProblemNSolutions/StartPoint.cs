using Sorting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;

namespace InterviewProblemNSolutions
{
    class StartPoint
    {
        static void Main(string[] args)
        {
            // Daily Problem
            DutchFlagProblem();
            EventAndDelegate();
            CircularPetrolPumpProblem();
            AddBinary();
            MultiplyLargeNumbersRepresentedAsString();
            IsomorphicStrings();
            SlidingWindowMaximum();
            SortArrayByParityII();
            ShortestDistanceFromAllBuildings();
            AlienDictionary();
            ProductOfArrayExceptSelf();
            ContinuousSubarraySum();
            TwoSum();
            ThreeSum();
            FourSum();
            MostCommonWord();
            ValidSudoku();
            ComplimentBase10();
            SpiralMatrix();
            WinnerOfTicTacToe();
            ReorderDataInLogFiles();
            MajorityElement();
            MajorityElementII();
            LRUCache();
            CompressedStringIterator();
            MeetingRoomsII();
            NumberOfIslands();
            TrapRainWater();
            FindTheCelebrity();
            IsPalindrome();
            WordSearch();
            WordSearchII();
            ImplementIndexOf();
            UniquePaths();
            UniquePathsII();
            RotateImage();
            DesignSearchAutocompleteSystem();
            FirstMissingPositive();
            PartitionLabels();
            IntegerToRoman();


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
            LongestSubStringOfNonRepeatingCharacters();

            // Sorting Problem
            ElectionWinner();
            Merge_BinA_WhichHasExtraSpaceEqualToB();

            // Searching Problem
            DetectDuplicate();
            FindMaxRecurrence();
            FindMissingNo();
            FindTwoRepeatingElements();
            FindPairWhoseSumIsClosetToGivenValue();
            FindTripletWhoseSumIsClosetToGivenValue();
            FindPeakOfIncreasingSequence();
            BinarySearchInRotatedSortedArray();
            FindFirstAndLastOccurenceOfNumInSortedArrayWithDuplicates();
            SeperateEvenNOdds();
            MaxIndexDiffInArray();
            CountFrequenciesOfNElements();

            // Selection Algorithms [Medians]
            FindKSmallestElements();
            KSmallestWorstCaseMedianOfMedians();
            MedianOfTwoSortedArraysSameSize();
            MedianOfTwoSortedArraysDifferentSize();

            // Divide and Conquer Algo
            MaximumValueContinousSubsequence();
            CalculateKtoPowerN();
            ClosetPairOfPoints();
            SkyLineProblem();


            // Learn Dynamic Programming (Memoization & Tabulation)
            MinimumStepsToMinimizeToOne();
            HappyNumbers();
            FibonacciAndFactorial();
            LongestCommonSubsequenceLength();
            ConvertGivenRecurrenceToCode();
            MaxValueContinousSubsequence();
            FindCatalanNumber();
            FindOrderToPerformMatrixMultiplications();
            ZeroOneKnapSackProblem();
            CoinChangingMinimumNoOfCoins();
            FindLongestIncreasingSubsequence();
            BuildingBridgesOverRiver();
            SubsetSumProblem();
            ParitionArrayInEqualHalf();
            FindOptimalBinarySearchTree();
            MinOperationToConvertStringAtoB();
            AllPairsShortestPathFloydWarshall();
            OptimalStrategyForACoinGame();
            LongestPalindromicSubsequence();
            LongestPalindromicSubString();
            NoOfTimesStringOccursAsSubsequenceInAnotherString();
            MaxSizeSquareSUbMatrix();
            MaximumSumRectangle();
            FindingOptimalJumpsToReachLast();
            CircusTowerRoutine();
            WordBreakProblem();

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

        /// <summary>
        /// GFG https://www.geeksforgeeks.org/find-a-tour-that-visits-all-stations/?ref=lbp
        /// Find the starting index/pump so that we can complete entire loop before running out of fuel, else return -1 (loop traversal not possible)
        /// </summary>
        public static void CircularPetrolPumpProblem()
        {
            Utility.Print("Find the first circular tour that visits all petrol pumps");
            int[][,] petrolsPumpsArr = { new int[,] { { 4, 6 }, { 6, 5 }, { 7, 3 }, { 4, 5 } }, new int[,] { { 6, 4 }, { 3, 6 }, { 7, 3 } } };  // multiple array inputs
            foreach (var petrolsPumps in petrolsPumpsArr)
            {
                List<PetrolPump> pumps = new List<PetrolPump>();
                for (int i = 0; i < petrolsPumps.GetLength(0); i++)
                {
                    pumps.Add(new PetrolPump(petrolsPumps[i, 0], petrolsPumps[i, 1]));
                    Console.WriteLine($" index:{i} \tPetrol: {petrolsPumps[i, 0]} \tDistanceToNext Pump: {petrolsPumps[i, 1]}");
                }
                var startAtIndex = DailyProblem.PetrolPump(pumps, pumps.Count);
                Console.WriteLine($" To Complete Entire circle Start at Pump with Index: \t{startAtIndex}\n");
            }
        }

        public static void AddBinary()
        {
            Utility.Print("");
            string[] str = { "11", "1" };
            var sumOfBinary = DailyProblem.AddBinary(str[0], str[1]);
            Console.WriteLine($" Adding Binary nums \'{str[0]}\' & \'{str[1]}\' results in: \'{sumOfBinary}\'");
        }

        public static void MultiplyLargeNumbersRepresentedAsString()
        {
            Utility.Print("Given two very big numbers (each more than 500 digits), multiply them.");
            string[][] numbers = { new string[] { "987", "321" }, new string[] { "4567", "123" }, new string[] { "654154154151454545415415454", "63516561563156316545145146514654" } };
            foreach (var pair in numbers)
            {
                var num1 = pair[0];
                var num2 = pair[1];
                var result = DailyProblem.MultiplyLargeNumbersRepresentedAsString(num1, num1.Length, num2, num2.Length);
                Console.WriteLine($" Multiplication of below numbers\n \tNum1: \t{num1}\n \tNum2: \t{num2}\n \tResult: \t{result}\n");
            }
        }

        public static void IsomorphicStrings()
        {
            // https://leetcode.com/problems/isomorphic-strings/submissions/
            Utility.Print("LeetCode#205. Isomorphic Strings");
            string[][] inputArr = { new string[] { "ab", "aa" }, new string[] { "egg", "add" }, new string[] { "foo", "bar" }, new string[] { "paper", "title" } };
            foreach (var pair in inputArr)
            {
                var input = pair[0];
                var pattern = pair[1];
                var isISO = DailyProblem.IsomorphicStrings(input, pattern);
                Console.WriteLine($"Str1: {input} & Str2: {pattern} \t are IsoMorhpic: {isISO}");
            }
        }

        public static void SlidingWindowMaximum()
        {
            Utility.Print("LeetCode #239. Sliding Window Maximum");
            int[][] inputArr = { new int[] { 1, 3, -1, -3, 5, 3, 6, 7 },
                                new int[] { 1,-1},
                                new int[] { 7,2,4},
                                new int[] { 1,3,1,2,0,5},
                                new int[] {-7,-8,7,5,7,1,6,0}};
            int[] k = { 3, 1, 2, 3, 4 };
            for (int i = 0; i < inputArr.Length; i++)
            {
                inputArr[i].Print("Input");
                var result = DailyProblem.SlidingWindowMaximum(inputArr[i], k[i]);
                Console.Write($" For K = {k[i]}");
                result.Print("Output");
                Console.WriteLine();
            }
        }

        public static void SortArrayByParityII()
        {
            // https://leetcode.com/problems/sort-array-by-parity-ii/
            Utility.Print("922. Sort Array By Parity II");
            int[] input = { 4, 7, 5, 2 };
            input.Print("Input");
            DailyProblem.SortArrayByParityII(input);
            input.Print("Output");
        }

        public static void ShortestDistanceFromAllBuildings()
        {
            // https://leetcode.com/explore/interview/card/facebook/52/trees-and-graphs/3026/
            Utility.Print("Shortest Distance from All Buildings");
            /* Problem Statement: You want to build a house on an empty land which reaches all buildings in the shortest amount of distance.
             * You can only move up, down, left and right. You are given a 2D grid of values 0, 1 or 2, where:
             * Each 0 marks an empty land which you can pass by freely.
             * Each 1 marks a building which you cannot pass through.
             * Each 2 marks an obstacle which you cannot pass through.
             * 
             * Example:
             * Input: [[1,0,2,0,1],[0,0,0,0,0],[0,0,1,0,0]]
             * 1 - 0 - 2 - 0 - 1
             * |   |   |   |   |
             * 0 - 0 - 0 - 0 - 0
             * |   |   |   |   |
             * 0 - 0 - 1 - 0 - 0
             * Output: 7 
             * Explanation: Given three buildings at (0,0), (0,4), (2,2), and an obstacle at (0,2),
             * the point (1,2) is an ideal empty land to build a house, as the total 
             * travel distance of 3+3+1=7 is minimal. So return 7.
             * 
             * Note:
             * There will be at least one building. If it is not possible to build such house according to the above rules, return -1.
             */
            int[,] input = { { 1, 0, 2, 0, 1 }, { 0, 0, 0, 0, 0 }, { 0, 0, 1, 0, 0 } };
            input.Print();
            var minTravelDistance = DailyProblem.ShortestDistanceFromAllBuildings(input, input.GetLength(0), input.GetLength(1));
            Console.WriteLine($" Minimum Travel Distance to visit all buildings marked as '1' in above Matrix with obstacles marked as '2' is: {minTravelDistance}");
        }

        public static void AlienDictionary()
        {
            // https://leetcode.com/problems/alien-dictionary/
            Utility.Print("269. Alien Dictionary");
            string[][] inputArr = { new string[] { "wrt", "wrf", "er", "ett", "rftt" },
                                    new string[] { "z", "x", "z" },
                                    new string[] { "z", "z" },
                                    new string[] { "aac", "aabb", "aaba" },
                                    new string[] { "aa","abb","aba"},
                                    new string[] { "abc","ab"},
                                    new string[] { "bsusz","rhn","gfbrwec","kuw","qvpxbexnhx","gnp","laxutz","qzxccww"} };
            foreach (var input in inputArr)
            {
                input.Print("Alien Dictionary");
                Console.WriteLine($" Derived Order of characters in above language is: \'{DailyProblem.AlienDictionary(input)}\'\n");
            }
        }

        public static void ProductOfArrayExceptSelf()
        {
            // https://leetcode.com/problems/product-of-array-except-self/
            Utility.Print("238. Product of Array Except Self");
            int[] nums = { 1, 2, 3, 4 };
            nums.Print("Input");
            var result = DailyProblem.ProductOfArrayExceptSelf(nums, nums.Length);
            result.Print("Product of above Array except self");
        }

        public static void ContinuousSubarraySum()
        {
            // https://leetcode.com/problems/continuous-subarray-sum/
            Utility.Print("523. Continuous Subarray Sum");
            int[][] numArr = { new int[] { 23, 2, 4, 6, 7 },
                             new int[] {23, 2, 6, 4, 7}};
            int[] kArr = { 6, 11, 0 };
            foreach (var nums in numArr)
            {
                foreach (var k in kArr)
                {
                    nums.Print("Input");
                    var ans = DailyProblem.ContinuousSubarraySum(nums, k);
                    Console.WriteLine($" In Above array subArray whose sum is in multiple of \'{k}\' present: \'{ans.Item1}\' b/w index [{ans.Item2}...{ans.Item3}]");
                }
            }
        }
        public static void TwoSum()
        {
            // https://leetcode.com/problems/two-sum/
            Utility.Print("1. Two Sum");
            var nums = new int[] { 230, 863, 916, 585, 981, 404, 316, 785, 88, 12, 70, 435, 384, 778, 887, 755, 740, 337, 86, 92, 325, 422, 815, 650, 920, 125, 277, 336, 221, 847, 168, 23, 677, 61, 400, 136, 874, 363, 394, 199, 863, 997, 794, 587, 124, 321, 212, 957, 764, 173, 314, 422, 927, 783, 930, 282, 306, 506, 44, 926, 691, 568, 68, 730, 933, 737, 531, 180, 414, 751, 28, 546, 60, 371, 493, 370, 527, 387, 43, 541, 13, 457, 328, 227, 652, 365, 430, 803, 59, 858, 538, 427, 583, 368, 375, 173, 809, 896, 370, 789 };
            nums.Print("Input");
            var target = 542;
            var result = DailyProblem.TwoSum(nums, target);
            Console.WriteLine($" Nums in above array which evaluate to {target}: {result[0]} + {result[1]}");
        }

        public static void ThreeSum()
        {
            // https://leetcode.com/problems/3sum/
            Utility.Print("15. 3Sum");
            int[][] numsArr = { new int[] { -1, 0, 1, 2, -1, -4 }, new int[] { 0, 0, 0, 0 }, new int[] { 1, -1, -1, 0 }, new int[] { -2, 0, 0, 2, 2 } };
            foreach (var nums in numsArr)
            {
                nums.Print("Input");
                var results = DailyProblem.ThreeSum(nums);
                foreach (var pair in results)
                    Console.WriteLine($" Nums in above array which evaluate to Zero: {pair[0]} + {pair[1]} + {pair[2]} == '0'\n");
            }
        }

        public static void FourSum()
        {
            // https://leetcode.com/problems/4sum/
            Utility.Print("18. 4Sum");
            int[][] numsArr = { new int[] { 1, 0, -1, 0, -2, 2 }, new int[] { }, new int[] { 0, 0, 0, 0 }, new int[] { -2, -1, -1, 1, 1, 2, 2 },
                                new int[] { 5, 5, 3, 5, 1, -5, 1, -2 }, new int[] { -1, 0, 1, 2, -1, -4 }, new int[] { 1, -2, -5, -4, -3, 3, 3, 5 } };
            int[] target = { 0, 0, 0, 0, 4, -1, -11 };
            for (int i = 0; i < target.Length; i++)
            {
                if (i == 6)
                    Console.WriteLine();
                numsArr[i].Print("Input");
                var results = DailyProblem.FourSum(numsArr[i],target[i]);
                foreach (var pair in results)
                    Console.WriteLine($" Nums in above array which evaluate to \'{target[i]}\': \t{pair[0]} + {pair[1]} + {pair[2]} + {pair[3]} = {target[i]}");
                Console.WriteLine();
            }
        }

        public static void MostCommonWord()
        {
            // https://leetcode.com/problems/most-common-word/
            Utility.Print("819. Most Common Word");
            string[] paragraph = { "a, a, a, a, b,b,b,c, c", "Bob hit a ball, the hit BALL flew far after it was hit." };
            string[][] banned = { new string[] { "a" }, new string[] { "hit" } };

            for (int i = 0; i < paragraph.Length; i++)
            {
                Console.WriteLine($" Input paragraph: \"{paragraph[i]}\"");
                Console.Write($" List of Banned words: ");
                foreach (var bannedWord in banned[i])
                    Console.Write($" \'{bannedWord}\'");

                var mostCommonWord = DailyProblem.MostCommonWord(paragraph[i], banned[i]);
                Console.WriteLine($"\n Most Common Word which isn't banned is: \'{mostCommonWord}\'\n");
            }
        }

        public static void ValidSudoku()
        {
            // https://leetcode.com/problems/valid-sudoku/
            Utility.Print("36. Valid Sudoku");
            char[][] board = { new char[]{'5','3','.','.','7','.','.','.','.'},
                                new char[]{'6','.','.','1','9','5','.','.','.'},
                                new char[]{'.','9','8','.','.','.','.','6','.'},
                                new char[]{'8','.','.','.','6','.','.','.','3'},
                                new char[]{'4','.','.','8','.','3','.','.','1'},
                                new char[]{'7','.','.','.','2','.','.','.','6'},
                                new char[]{'.','6','.','.','.','.','2','8','.'},
                                new char[]{'.','.','.','4','1','9','.','.','5'},
                                new char[]{'.','.','.','.','8','.','.','7','9'} };
            board.Print("SUDOKU BOARD");
            Console.WriteLine($" Above partial filed SUDOKU is valid? : {DailyProblem.ValidSudoku(board)}");
        }

        public static void SudokuSolver()
        {
            // https://leetcode.com/problems/sudoku-solver/
            Utility.Print("37. Sudoku Solver");
            char[][] board = { new char[]{'5','3','.','.','7','.','.','.','.'},
                                new char[]{'6','.','.','1','9','5','.','.','.'},
                                new char[]{'.','9','8','.','.','.','.','6','.'},
                                new char[]{'8','.','.','.','6','.','.','.','3'},
                                new char[]{'4','.','.','8','.','3','.','.','1'},
                                new char[]{'7','.','.','.','2','.','.','.','6'},
                                new char[]{'.','6','.','.','.','.','2','8','.'},
                                new char[]{'.','.','.','4','1','9','.','.','5'},
                                new char[]{'.','.','.','.','8','.','.','7','9'} };
            board.Print("SUDOKU BOARD");
            char[][] expected= { new char[]{'5','3','4','6','7','8','9','1','2'},
                                new char[] {'6','7','2','1','9','5','3','4','8'},
                                new char[] {'1','9','8','3','4','2','5','6','7' },
                                new char[] {'8','5','9','7','6','1','4','2','3' },
                                new char[] {'4','2','6','8','5','3','7','9','1' },
                                new char[] {'7','1','3','9','2','4','8','5','6' },
                                new char[] {'9','6','1','5','3','7','2','8','4' },
                                new char[] {'2','8','7','4','1','9','6','3','5' },
                                new char[] {'3','4','5','2','8','6','1','7','9' } };
            //var result = DailyProblem.SudokuSolver(board);
            //ValidateResult(result, expected);

            bool ValidateResult(char[][] c1, char[][] c2)
            {
                int row = c1.Length;
                int col = c1[0].Length;
                for (int r = 0; r < row; r++)
                    for (int c = 0; c < col; c++)
                        if (c1[r][c] != c2[r][c])
                            return false;
                return true;
            }
        }

        public static void ComplimentBase10()
        {
            // https://leetcode.com/problems/complement-of-base-10-integer/
            Utility.Print("1009. Complement of Base 10 Integer");
            int[] inputArr = { 5, 7, 10 };
            foreach (var num in inputArr)
                Console.WriteLine($" Compliment of Base10 num: \'{num}\' is: \'{DailyProblem.ComplimenetBase10Fastest(num)}\'");
        }

        public static void SpiralMatrix()
        {
            // https://leetcode.com/problems/spiral-matrix/
            Utility.Print("54. Spiral Matrix");
            int[][][] matrixArr = { new int[][] { new int[] { 1, 2, 3, }, new int[] { 4, 5, 6 }, new int[] { 7, 8, 9 } },               // 1st Input
                                    new int[][] { new int[] { 1, 2, 3, 4 }, new int[] { 5, 6, 7, 8 }, new int[] { 9, 10, 11, 12 } } };  // 2nd Input
            foreach (var matrix in matrixArr)
            {
                matrix.Print("Input Matrix");
                var spiralOrder = DailyProblem.SpiralMatrix(matrix);
                spiralOrder.Print("Spiral Order");
            }
        }

        public static void WinnerOfTicTacToe()
        {
            // https://leetcode.com/problems/find-winner-on-a-tic-tac-toe-game/
            Utility.Print("1275. Find Winner on a Tic Tac Toe Game");
            int[][] moves = { new int[] { 0, 0 }, new int[] { 2, 0 }, new int[] { 1, 1 }, new int[] { 2, 1 }, new int[] { 2, 2 } };
            moves.Print("TicTac Moves");
            Console.WriteLine($" Above moves result in : {DailyProblem.WinnerOfTicTacToe(moves)}");
        }

        public static void ReorderDataInLogFiles()
        {
            // https://leetcode.com/problems/reorder-data-in-log-files/
            Utility.Print("937. Reorder Data in Log Files");
            string[][] logs = { new string[] { "dig1 8 1 5 1", "let1 art can", "dig2 3 6", "let2 own kit dig", "let3 art zero" },
                                new string[] { "a1 9 2 3 1","g1 act car","zo4 4 7","ab1 off key dog","a8 act zoo"} };
            string[][] expectedOutput = { new string[] {"let1 art can", "let3 art zero", "let2 own kit dig", "dig1 8 1 5 1", "dig2 3 6" },
                                          new string[] { "g1 act car","a8 act zoo","ab1 off key dog","a1 9 2 3 1","zo4 4 7"} };
            for(int i=0;i<logs.Length;i++)
                ValidateResult(DailyProblem.ReorderDataInLogFilesByDividingDigitsAndWordLogs(logs[i]), expectedOutput[i]);

            void ValidateResult(string[] arr1,string[] arr2)
            {
                for (int i = 0; i < arr1.Length; i++)
                    if (arr1[i] != arr2[i]) { Console.WriteLine($" Result doesnt matches expected output"); return; };
                Console.WriteLine(" Result matches expected output");
            }
        }

        public static void MajorityElement()
        {
            // https://leetcode.com/problems/majority-element/
            Utility.Print("169. Majority Element");
            int[][] numsArr = { new int[] { 3, 2, 3 }, new int[] { 2, 2, 1, 1, 1, 2, 2 } };
            foreach (var nums in numsArr)
            {
                nums.Print("Input Array");
                Console.WriteLine($" Majority Element with more than n/2 majority is: {DailyProblem.MajorityElement(nums)}\n");
            }
        }

        public static void MajorityElementII()
        {
            // https://leetcode.com/problems/majority-element-ii/
            Utility.Print("229. Majority Element II");
            int[][] numArr = { new int[] { 3, 2, 3 }, new int[] { 1 }, new int[] { 1, 2 }, new int[] { 1, 1, 1, 2, 3, 7, 8, 1, 6, 9 } };
            foreach (var nums in numArr)
            {
                nums.Print("Input Array");
                var result = DailyProblem.MajorityElementII(nums);
                foreach (var element in result)
                    Console.WriteLine($" Majority Element with more than n/3 majority is: {element}\n");
            }
        }

        public static void LRUCache()
        {
            // https://leetcode.com/problems/lru-cache/
            Utility.Print("146. LRU Cache");

            LRUCache lRUCache = new LRUCache(2);
            lRUCache.Put(1, 1); // cache is {1=1}
            lRUCache.Put(2, 2); // cache is {1=1, 2=2}
            Console.WriteLine(lRUCache.Get(1));    // return 1
            lRUCache.Put(3, 3); // LRU key was 2, evicts key 2, cache is {1=1, 3=3}
            Console.WriteLine(lRUCache.Get(2));    // returns -1 (not found)
            lRUCache.Put(4, 4); // LRU key was 1, evicts key 1, cache is {4=4, 3=3}
            Console.WriteLine(lRUCache.Get(1));    // return -1 (not found)
            Console.WriteLine(lRUCache.Get(3));    // return 3
            Console.WriteLine(lRUCache.Get(4));    // return 4
        }


        public static void CompressedStringIterator()
        {
            // https://leetcode.com/problems/design-compressed-string-iterator/
            Utility.Print("604. Design Compressed String Iterator");
            string[] compressed = { "L1e2t1C1o1d1e1", "L10e2t1C1o1d1e11", "Q2s4n8V18" };
            string[][] inputType = { new string[] { "StringIterator", "next", "next", "next", "next", "next", "next", "hasNext", "next", "hasNext", "next", "hasNext" },
                                        new string[] { "StringIterator", "next", "next", "next", "next", "next", "next", "hasNext", "next", "hasNext" },
                                        new string[] { "StringIterator", "next", "next", "next", "next", "hasNext", "next", "next", "next", "hasNext", "next", "hasNext", "next", "next", "next", "next", "next", "next", "next", "next", "next", "hasNext", "next", "next", "next", "next", "hasNext", "next", "next", "next", "next", "next", "next", "hasNext", "next", "next", "next", "next", "next", "next", "next", "next", "hasNext", "next", "next", "hasNext", "next", "next", "next", "next", "hasNext", "next", "hasNext", "hasNext", "next", "next", "next", "next", "hasNext", "next", "next", "next", "next", "next", "hasNext", "next", "next", "next", "hasNext", "hasNext", "hasNext", "next", "next", "next", "hasNext", "next", "next", "next", "hasNext", "next", "next", "hasNext", "next", "next", "next", "next", "next", "next", "next", "hasNext", "next", "next", "next", "next", "next", "hasNext", "next" } };
            
            for (int i = 0; i < compressed.Length; i++)
            {
                Console.WriteLine($" Compressed string: \'{compressed[i]}\'");
                
                // Create Iterator Obj
                var iterator = new StringIterator(compressed[i]);

                // to check output
                for (int j = 1; j < inputType[i].Length; j++)
                    if (inputType[i][j] == "next")
                        Console.Write($" {iterator.Next()}");
                    else
                        Console.Write($" {iterator.HasNext()}");
                Console.WriteLine("\n");
            }
        }

        public static void MeetingRoomsII()
        {
            // https://leetcode.com/problems/meeting-rooms-ii/
            Utility.Print("253. Meeting Rooms II");
            int[][][] inputArr = { new int[][]{ new int[] { 0, 30 }, new int[] { 5, 10 }, new int[] { 15, 20 } },
                                new int[][]{ new int[]{ 13, 15 }, new int[] { 1, 13 }, new int[] { 6,9} } };
            foreach (var input in inputArr)
            {
                input.Print("Meetings schedule");
                Console.WriteLine($" Min no of meeting rooms required to accomodate above listed meetings : {DailyProblem.MeetingRoomsII(input)}");
            }
        }

        
        public static void NumberOfIslands()
        {
            // https://leetcode.com/problems/number-of-islands/
            Utility.Print("200. Number of Islands");
            char[][][] gridArr = { new char[][] { new char[] { '1', '1', '1', '1', '0' },
                                                  new char[] { '1', '1', '0', '1', '0' },
                                                  new char[] { '1', '1', '0', '0', '0' },
                                                  new char[] { '0', '0', '0', '0', '0' } },
                                  new char[][] { new char[] { '1', '1', '1', '1', '1', '0', '1', '1', '1', '1', '1', '1', '1', '1', '1', '0', '1', '0', '1', '1' }, new char[] { '0', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '0', '1', '1', '1', '1', '1', '0' }, new char[] { '1', '0', '1', '1', '1', '0', '0', '1', '1', '0', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1' }, new char[] { '1', '1', '1', '1', '0', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1' }, new char[] { '1', '0', '0', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1' }, new char[] { '1', '0', '1', '1', '1', '1', '1', '1', '0', '1', '1', '1', '0', '1', '1', '1', '0', '1', '1', '1' }, new char[] { '0', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '0', '1', '1', '0', '1', '1', '1', '1' }, new char[] { '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '0', '1', '1', '1', '1', '0', '1', '1' }, new char[] { '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '0', '1', '1', '1', '1', '1', '1', '1', '1', '1' }, new char[] { '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1' }, new char[] { '0', '1', '1', '1', '1', '1', '1', '1', '0', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1' }, new char[] { '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1' }, new char[] { '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1' }, new char[] { '1', '1', '1', '1', '1', '0', '1', '1', '1', '1', '1', '1', '1', '0', '1', '1', '1', '1', '1', '1' }, new char[] { '1', '0', '1', '1', '1', '1', '1', '0', '1', '1', '1', '0', '1', '1', '1', '1', '0', '1', '1', '1' }, new char[] { '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '0', '1', '1', '1', '1', '1', '1', '0' }, new char[] { '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '0', '1', '1', '1', '1', '0', '0' }, new char[] { '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1' }, new char[] { '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1' }, new char[] { '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1' } } };
            foreach (var grid in gridArr)
            {
                grid.Print("GRID");
                Console.WriteLine($" No Of Distint Island present in above Grid are : {DailyProblem.NumberOfIslands(grid)}");
            }
        }

        public static void TrapRainWater()
        {
            // https://leetcode.com/problems/trapping-rain-water/
            Utility.Print("42. Trapping Rain Water");
            int[][] heightArr = { new int[] { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 }, new int[] { 4, 2, 0, 3, 2, 5 } };
            foreach (var height in heightArr)
            {
                height.Print("Elevation Map");
                Console.WriteLine($" Maximum Vol of water that can be trapped in above Map is: {DailyProblem.TrapRainWater(height)}");
            }
        }

        public static void FindTheCelebrity()
        {
            // https://leetcode.com/problems/find-the-celebrity/
            Utility.Print("277. Find the Celebrity");
            int[][] know2d = { new int[] { 1, 1, 0 }, new int[] { 0, 1, 0 }, new int[] { 1, 1, 1 } };
            know2d.Print("Relations Input");
            int noOfPeople = 3;

            var obj = new Celebrity(know2d);
            //var result = obj.FindCelebritySlower(noOfPeople);
            var result = obj.FindCelebrityEfficient(noOfPeople);
            //var result = obj.FindCelebrityOptimized(noOfPeople);
            Console.WriteLine($" Celebrity in above relation-array is: {result}");
        }

        public static void IsPalindrome()
        {
            // https://leetcode.com/problems/valid-palindrome/
            Utility.Print("125. Valid Palindrome");
            string[] input = { "0P", "A man, a plan, a canal: Panama" };
            foreach (var str in input)
                Console.WriteLine($" \'{str}\' \tis Palindrome: {DailyProblem.IsPalindrome(str.ToLower())}");
        }


        public static void WordSearch()
        {
            // https://leetcode.com/problems/word-search/
            Utility.Print("79. Word Search");
            char[][] board = { new char[] { 'A', 'B', 'C', 'E' },
                               new char[] { 'S', 'F', 'C', 'S' },
                               new char[] { 'A', 'D', 'E', 'E' } };
            string searchForWord = "ABCCED";
            board.Print("GRID");
            Console.WriteLine($" Search for Given word: \'{searchForWord}\' in above GRID returned: {DailyProblem.WordProblem(board, searchForWord)}");
        }


        public static void WordSearchII()
        {
            // https://leetcode.com/problems/word-search-ii/
            Utility.Print("212. Word Search II");
            char[][] board = { new char[] { 'o', 'a', 'a', 'n' },
                               new char[] { 'i', 't', 'a', 'e' },
                               new char[] { 'n', 'g', 'k', 'r' },
                               new char[] { 'i', 'f', 'l', 'v' } };
            string[] words = { "oath", "pea", "eat","eating", "rain" };
            board.Print("GRID");
            words.Print("Words being searched");
            var result = DailyProblem.WordProblemII(board, words);
            Console.Write($" Search for above list of words in above GRID found:");
            foreach (var matchedWord in result)
                Console.Write($" \'{matchedWord}\'");
            
            Console.WriteLine();
        }


        public static void ImplementIndexOf()
        {
            // https://leetcode.com/problems/implement-strstr/
            Utility.Print("28. Implement strStr()");
            string[] haystack = { "hello", "", "aaab","mississippi" };
            string[] needle = { "ll", "", "aaaa", "issip" };
            for (int i = 0; i < haystack.Length; i++)
                Console.WriteLine($" \'{needle[i]}\' was found in \t\'{haystack[i]}\' at index: {DailyProblem.ImplementIndexOfRabinKarp(haystack[i], needle[i])}");
        }

        public static void UniquePaths()
        {
            // https://leetcode.com/problems/unique-paths/
            Utility.Print("62. Unique Paths");
            int row = 7, col = 3;
            var result = DailyProblem.UniquePaths_DP(row, col);
            //var result = DailyProblem.UniquePaths_Recursion(row, col);
            Console.WriteLine($" No Of Unqiue Paths for GRID of RowxCol: {row}x{col} from Top-Left to Bottom-Right: {result}");
        }

        public static void UniquePathsII()
        {
            // https://leetcode.com/problems/unique-paths-ii/
            Utility.Print("63. Unique Paths II");
            int[][][] inputArr = { new int[][] { new int[] { 0, 0, 0 }, new int[] { 0, 1, 0 }, new int[] { 0, 0, 0 } },
                                    new int[][] { new int[] { 1, 0 }, new int[] { 0, 0 } },
                                    new int[][] { new int[] { 0, 0 }, new int[] { 0, 1 } } };
            foreach (var input in inputArr)
            {
                input.Print("GRID");
                var result = DailyProblem.UniquePathsWithObstacles(input);
                Console.WriteLine($" No Of Unqiue Paths for above GRID (with obstacles) starting from Top-Left to Bottom-Right: {result}\n");
            }
        }

        public static void RotateImage()
        {
            // https://leetcode.com/problems/rotate-image/
            Utility.Print("48. Rotate Image");
            int[][][] matrixArr = { new int[][] { new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 }, new int[] { 7, 8, 9 } },
                                    new int[][] { new int[] { 5, 1, 9, 11 }, new int[] { 2, 4, 8, 10 }, new int[] { 13, 3, 6, 7 }, new int[] { 15, 14, 12, 16 } } };
            foreach (var matrix in matrixArr)
            {
                matrix.Print("Input Image");
                //DailyProblem.RotateFourRectangleApproach(matrix);
                DailyProblem.RotateImageTransposeAndReverse(matrix);
                //DailyProblem.RotateImage(matrix);
                matrix.Print("90 degree rotated image");
            }
        }

        public static void DesignSearchAutocompleteSystem()
        {
            // https://leetcode.com/problems/design-search-autocomplete-system/
            Utility.Print("642. Design Search Autocomplete System");
            string[] sentences = new string[] { "i love you", "island", "ironman", "i love leetcode" };
            int[] frequency = new int[] { 5, 3, 2, 2 };
            for (int i = 0; i < sentences.Length; i++)
                Console.WriteLine($" {sentences[i]} => \tRepeated {frequency[i]} times");
            char[] input = { 'i', ' ', 'a', '#', 'i', ' ', 'a', '#', 'i', ' ', 'a', '#' };
            input.Print("Input Character Sequence");
            Console.WriteLine($"--------------------------------------------------------------------------------------");

            // Create "AutoCompleteSystem" for initiallay provided Sentences & their corrosponding frequencies
            AutoCompleteSystem system = new AutoCompleteSystem(sentences, frequency);
            string userInput = "";
            foreach (var ch in input)
            {
                userInput += ch;
                Console.WriteLine($"Searching with Prefix \'{userInput}\' matching sentences are: \t[Max limit restricted to '3']");

                // autocomplete-system search-engine searching for possible matches with each newly added characters
                // & returns list of all sentences matching the Prefix entered so far by user
                foreach (var matches in system.Input(ch))
                    Console.WriteLine($" Top Matches are \t=> {matches}");

                userInput = ch == '#' ? "" : userInput;
                if (ch == '#')
                    Console.WriteLine($"--------------------------------------------------------------------------------------");
            }
        }

        public static void FirstMissingPositive()
        {
            // https://leetcode.com/problems/first-missing-positive/
            Utility.Print("41. First Missing Positive");
            int[][] numsArr = { new int[] { 7, 8, 9, 11, 12 }, new int[] { }, new int[] { 0 }, new int[] { 1 }, new int[] { 1, 1000 }, new int[] { 2 }, new int[] { 2, 1 } };
            foreach (var nums in numsArr)
            {
                nums.Print("Input");
                var firstMissingPositiveNo = DailyProblem.FirstMissingPositive(nums);
                Console.WriteLine($" First Missing Positive number in above input array is: {firstMissingPositiveNo}\n");
            }
        }

        public static void PartitionLabels()
        {
            // https://leetcode.com/problems/partition-labels/
            Utility.Print("763. Partition Labels");
            string S = "ababcbacadefegdehijhklij";
            Console.WriteLine($" Input string: \t {S}");
            var parts = DailyProblem.PartitionLabels(S);
            foreach(var lengthOfPart in parts)
                Console.Write($" {lengthOfPart}");
        }

        public static void IntegerToRoman()
        {
            // https://leetcode.com/problems/integer-to-roman/
            // https://leetcode.com/problems/roman-to-integer/
            Utility.Print("12. Integer to Roman && 13. Roman to Integer");
            int[] nums = { 3, 4, 9, 58, 1994 };
            foreach (var num in nums)
            {
                var roman = DailyProblem.IntegerToRoman(num);
                var no = DailyProblem.RomanToInteger(roman);
                Console.WriteLine($" Interger to Roman: \t {no} \t{roman}");
            }
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

        public static void LongestSubStringOfNonRepeatingCharacters()
        {
            // GFG https://www.geeksforgeeks.org/length-of-the-longest-substring-without-repeating-characters/
            Utility.Print("Length of the longest substring without repeating characters");
            string[] strArr = { "BBBBA", "ABDEFGABEF", "GEEKSFORGEEKS" };
            foreach (var str in strArr)
                StringAlgorithms.LongestSubStringNonRepeatingChar(str);       // O(n)
            //StringAlgorithms.LongestSubStringNonRepeatingCharacters(str);   // O(n^2)
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

        public static void FindTwoRepeatingElements()
        {
            Utility.Print("Problem-19  Find the two repeating elements in a given array: Given an array with size," +
                " all elements of the array are in range 1 to n and also all elements occur only once except two numbers which occur twice." +
                " Find those two repeating numbers. For example: if the array is 4,2,4,5,2,3,1 with size = 7 and n = 5." +
                " This input has n + 2 = 7 elements with all elements occurring once except 2 and 4 which occur twice. So the output should be 4 2.(pp. 569)");
            int[] input = { 4, 2, 4, 5, 2, 3, 1 };
            int len = input.Length;
            int range = 5;    // [1...5] all elements are in this range
            input.Print("Input Array");

            SearchAlgorithms.TwoRepeatingElements(input, range, len);
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

        public static void FindPeakOfIncreasingSequence()
        {
            Utility.Print("Problem-37 Increasing sequence (p. 578)");
            int[] input = { 1, 2, 4, 5, 7, 9, 3, 2, 1 };
            input.Print("Input : ");
            SearchAlgorithms.IncreasingSequence(input, 0, input.Length - 1);
        }

        public static void BinarySearchInRotatedSortedArray()
        {
            Utility.Print("Problem-40  Given a sorted array of n integers that has been rotated an unknown number of times," +
                " give a O(logn) algorithm that finds an element in the array.(pp. 579 - 580)");
            int[] input = { 15, 16, 19, 20, 25, 1, 3, 4, 5, 7, 10, 14 };
            int searchFor = 5;
            input.Print("Input : ");
            var index = SearchAlgorithms.BinarySearchInRotatedArray(input, searchFor);
            var indexFast = SearchAlgorithms.BinarySearchInRotatedArraySinglePass(input, 0, input.Length - 1, searchFor);
            if (index != -1)
                Console.WriteLine($" Element {searchFor} found at index : {index}");
            else
                Console.WriteLine($" Element {searchFor} Not Found in array");
        }

        public static void FindFirstAndLastOccurenceOfNumInSortedArrayWithDuplicates()
        {
            Utility.Print("Problem-46  Given a sorted array A of n elements, possibly with duplicates," +
                " find the index of the first occurrence of a number in O(logn) time.(p. 582)");
            int[] input = { 1, 1, 1, 1, 2, 2, 3, 3, 3, 3, 3, 4, 5, 5, 5, 5, 6, 7, 7, 8 };
            input.Print("Input : ");
            int searchFor = 3;
            var index = SearchAlgorithms.FirstOccurenceInSortedArray(input, searchFor);
            Console.WriteLine($" First index where {searchFor} found is : {index}");

            Utility.Print("Problem-47  Given a sorted array A of n elements, possibly with duplicates. Find the index of the last occurrence of a number in O(logn) time.(p. 583)");
            input.Print("Input : ");
            index = SearchAlgorithms.LastOccurenceInSortedArray(input, searchFor);
            Console.WriteLine($" Last index where {searchFor} found is : {index}");
        }

        public static void SeperateEvenNOdds()
        {
            Utility.Print("Problem-67  Separate even and odd numbers: Given an array A[], write a function that segregates even and odd numbers.(p. 589)");
            int[] input = { 12, 34, 45, 9, 8, 90, 3 };
            input.Print("Input Array : ");
            SearchAlgorithms.SeperateEvenOdd(input);
            input.Print("Output Array : ");
        }

        // GFG https://www.geeksforgeeks.org/given-an-array-arr-find-the-maximum-j-i-such-that-arrj-arri/
        public static void MaxIndexDiffInArray()
        {
            Utility.Print("Problem-77  Given an array A[], find the maximum j – i such that A[j] > A[i]." +
                " For example, Input: {34, 8, 10, 3, 2, 80, 30, 33, 1} and Output: 6 (j = 7, i = 1).(p. 595)");
            int[] input = { 34, 8, 10, 3, 2, 80, 30, 33, 1 };
            input.Print("Input Array : ");
            SearchAlgorithms.MaxIndexDiff(input);
        }

        // GFG https://www.geeksforgeeks.org/count-frequencies-elements-array-o1-extra-space-time/
        // Other Approach similar to MaxRecurrence https://youtu.be/UW1InjlrXFU
        public static void CountFrequenciesOfNElements()
        {
            Utility.Print("Problem-79  Given an array of n elements, how do you print the frequencies of elements without using extra space. Assume all elements are positive, editable and less than n.(p. 596)");
            int[][] inputs = { new int[] { 10, 10, 9, 4, 7, 6, 5, 2, 3, 2, 1 },
                                new int[] { 4, 4, 4, 4, 4, 6 },
                                new int[] { 1, 3, 5, 7, 9, 1, 3, 5, 7, 9, 1}
                            };
            foreach (var input in inputs)
            {
                input.Print("Input Array : ");
                SearchAlgorithms.CountFrequencyNegationMethod(input);
                //SearchAlgorithms.CountFrequencyAddArrayLengthMethod(input);
            }
        }

        public static void FindKSmallestElements()
        {
            Utility.Print("Problem-10  Find the k-smallest elements in an array S of n elements using partitioning method.(p. 604)");
            int[] input = { 10, 10, 9, 4, 7, 6, 5, 2, 3, 2, 1 };
            input.Print("Input array :");
            var k = 4;
            // based on Quick Sort
            SelectionAlgorithms.KSmallestElements(input, 0, input.Length - 1, k);

            // Another way to solve in O(klogk) + O((n-k)logk) = O(nlogk) by creating 'balanced binary search tree' for K elements
            // & than inserting elements one by one if it's greater than largest element in tree (after removing current largest)
            // in the end printing all elements of tree via InOrder traversal

            // print k elements from start as array sorted in ascending order
            Console.WriteLine($" Printing k : {k} smallest elements");
            for (int i = 0; i < k; i++)
                Console.Write($" {input[i]}");
        }

        // GFG https://www.geeksforgeeks.org/happy-number/
        public static void HappyNumbers()
        {
            Utility.Print("Find if given number is happy number or not.");
            /* Number is happy is or not?
             * Ex: 7 square of Digits (7*7) = 49 (2 digits) || Square of (4 = 16) + (9 = 81) = 97 (2 digits) || Square of (9=81)+(7=49) = 130 ||
             * Square of (1=1)+(3=9)+(0=0) = 10 || Square of (1=1)+(0=0) = 1
             * So number who's square of digit comes out to be 1 after some 'n' operation is said to be 'Happy Number'
             */
            int[] inputs = { 7, 13, 19, 23, 31, 79, 97, 103, 109, 139, 144, 256, 1091 };
            foreach (var num in inputs)
            {
                // Hashset to hold sequence of no's while finding is given no is happy or not
                var notHappySet = new HashSet<int>();

                Console.WriteLine($" {num} is Happy : {isHappyFloydWay(num)}");

                // Recursive algo + Auxillary Space
                bool isHappyNumber(int number)
                {
                    if (number == 1) return true;
                    if (notHappySet.Contains(number)) return false;         // if present in set means we have visited the number before & now in loop
                    notHappySet.Add(number);
                    return isHappyNumber(GetSquaredDigitSum(number));
                }

                // Iterative algo + Auxillary Space
                bool isHappyNum(int number)
                {
                    while (true)
                    {
                        var squareOfDigits = GetSquaredDigitSum(number);
                        if (squareOfDigits == 1) return true;
                        if (notHappySet.Contains(squareOfDigits)) break;    // if present in set means we have visited the number before & now in loop
                        notHappySet.Add(number = squareOfDigits);
                    }
                    return false;
                }

                // Iterative algo + O(1) space
                bool isHappyFloydWay(int number)
                {
                    int slow = number, fast = number;
                    do
                    {
                        slow = GetSquaredDigitSum(slow);
                        fast = GetSquaredDigitSum(GetSquaredDigitSum(fast));
                    }
                    while (slow != fast);
                    return slow == 1;
                }
            }
            int GetSquaredDigitSum(int n) => (n == 0) ? 0 : (n % 10) * (n % 10) + GetSquaredDigitSum(n / 10);
        }

        // GFG https://www.geeksforgeeks.org/kth-smallestlargest-element-unsorted-array-set-3-worst-case-linear-time/
        public static void KSmallestWorstCaseMedianOfMedians()
        {
            Utility.Print("K’th Smallest/Largest Element in Unsorted Array | Set 3 (Worst Case Linear Time)");
            // for O(NLogK) algorithms use Heap/QuickSort
            // For Expected O(n) use Random Pivot in QuickSort
            // For worse case but still O(n) use Med of Med below
            int[] input = { 7, 10, 4, 3, 20, 15 };
            input.Print("Input array :");
            var k = 4;  // 4th smallest
            // based on Quick Sort
            var kthIndex = SelectionAlgorithms.KSmallestUsingMedOfMed(input, 0, input.Length - 1, k);
            Console.WriteLine($" {k}th Smallest in above array in : {input[kthIndex]}");
        }

        public static void MedianOfTwoSortedArraysSameSize()
        {
            int[] a1 = { 1, 12, 16, 26, 38 , 45};
            int[] a2 = { 2, 13, 17, 30, 45 , 65};
            a1.Print(" Arr1 :");
            a2.Print(" Arr2 :");
            var len = a1.Length;


            Utility.Print("Median Of Two Sorted Arrays of Same Size, using Count || Time O(n)");
            var median = SelectionAlgorithms.MedianSortedArrayEqualSizeUsingCount(a1, a2, len);
            if (median >= 0) Console.WriteLine($" Median value of above two sorted array is : {median}");

            Utility.Print("Median Of Two Sorted Arrays of Same Size, using Median || Recursive || Time O(Logn)");
            median = SelectionAlgorithms.MedianSortedArrayEqualSizeByComparingMedians(a1, 0, len - 1, a2, 0, len - 1);
            if (median >= 0) Console.WriteLine($" Median value of above two sorted array is : {median}");

            Utility.Print("Median Of Two Sorted Arrays of Same Size, using Median || Iterative || Time O(Logn)");
            median = SelectionAlgorithms.MedianSortedArrayEqualSizeByComparingMedians_Iterative(a1, 0, len - 1, a2, 0, len - 1);
            if (median >= 0) Console.WriteLine($" Median value of above two sorted array is : {median}");
        }

        public static void MedianOfTwoSortedArraysDifferentSize()
        {
            Utility.Print("Median Of Two Sorted Arrays of different size, Efficient Approach|| Time O(Log(Min(n,m))");
            int[] a1 = { 1, 3, 8, 9, 15 };          //{ 1, 12, 16, 26, 38 };
            int[] a2 = { 7, 11, 18, 19, 21, 25 };   //{ 2, 13, 17, 30, 45, 65 };
            a1.Print(" Arr1 :");
            a2.Print(" Arr2 :");
            var median = SelectionAlgorithms.MedianOfSortedArray(a1, a2);
            Console.WriteLine($" Median value of above two sorted array is : {median}");
        }

        public static void MaximumValueContinousSubsequence()
        {
            Utility.Print("Problem-25  Maximum Value Contiguous Subsequence: (p. 748)");
            int[][] inputs = { new int[] { -2, 11, -4, 13, -5, 2 }, new int[] { 1, -3, 4, -2, -1, 6 } };
            foreach (var input in inputs)
            {
                input.Print("Input values :");
                var maxValue = DivideAndConquerAlgorithms.MaxValueContinousSubsequence(input, 0, input.Length - 1);
                Console.WriteLine($" Maximum Value Contiguous Subsequence in above array is : {maxValue}");
            }
        }

        public static void CalculateKtoPowerN()
        {
            Utility.Print("Problem-29  To calculate kn, give algorithm and discuss its complexity.(p. 754)");
            long k = 9, n = 24;
            Console.WriteLine($" k {k} to power {n} : {DivideAndConquerAlgorithms.FindPower(k, n)}");
        }

        // GFG https://www.geeksforgeeks.org/closest-pair-of-points-using-divide-and-conquer-algorithm/
        public static void ClosetPairOfPoints()
        {
            Utility.Print("Closest-Pair of Points: Given a set of n points, S = {p1,p2,p3,…,pn}, " +
                "where pi = (xi,yi). Find the pair of points having the smallest distance among all pairs (assume that all points are in one dimension).(p. 749)");
            int[,] input = { { 2, 3 }, { 12, 30 }, { 40, 50 }, { 5, 1 }, { 12, 10 }, { 3, 4 } };
            Point[] pointArr = new Point[input.GetLength(0)];

            for (int i = 0; i < input.GetLength(0); i++)            // store pair of X,Y cordinates as Array of Points
                pointArr[i] = new Point(input[i, 0], input[i, 1]);

            var minDistance = DivideAndConquerAlgorithms.ClosetPair(pointArr);
            Console.WriteLine($" Minimum distance in 'pair of points' in above provided Points in 2D-space : \t {minDistance}");
        }

        // GFG https://www.geeksforgeeks.org/the-skyline-problem-using-divide-and-conquer-algorithm/
        public static void SkyLineProblem()
        {
            Utility.Print("The Skyline Problem using Divide and Conquer algorithm. " +
                "Given n rectangular buildings in a 2 - dimensional city, computes the skyline of these buildings, eliminating hidden lines.");
            //int[,] input = { { 1, 14, 7 }, { 3, 9, 10 }, { 5, 17, 12 }, { 14, 11, 18 }, { 15, 6, 27 }, { 20, 19, 22 }, { 23, 15, 30 }, { 26, 14, 29 } };
            int[,] input = { { 1, 11, 5 }, { 2, 6, 7 }, { 3, 13, 9 }, { 12, 7, 16 }, { 14, 3, 25 }, { 19, 18, 22 }, { 23, 13, 29 }, { 24, 4, 28 } };
            Building[] buildArr = new Building[input.GetLength(0)];

            for (int i = 0; i < input.GetLength(0); i++)
                buildArr[i] = new Building(input[i, 0], input[i, 1], input[i, 2]);
            DivideAndConquerAlgorithms.PrintBuildings(buildArr);

            var skyLine = DivideAndConquerAlgorithms.GetSkyLine(buildArr, 0, buildArr.Length - 1);              // O(nlogn)
            DivideAndConquerAlgorithms.PrintSkyLine(skyLine);

            var skyLineSlow = DivideAndConquerAlgorithms.BruteForceSkyLine(buildArr, 0, buildArr.Length - 1);   // O(n^2)
        }



        public static void FibonacciAndFactorial()
        {
            Utility.Print("Find out Fibonacci and Factorial of 'N'");
            int[] numbers = { 0, 1, 5, 10 };
            // For memo 
            var max = numbers.Max();
            // create an array to update intermediate value as & when encoutnered while calculating min steps for a given 'num'
            long[] memoTable = new long[max + 1];
            foreach (var num in numbers)
            {
                Console.Write($"  Fibonnaci of {num} is : {DynamicProgramming.FibonnaciIterative(num)} \t||");
                Console.WriteLine($"  Factorial of {num} is : {DynamicProgramming.Factorial(num, memoTable)}");
            }
        }

        // Tushar Roy https://youtu.be/NnD96abizww
        public static void LongestCommonSubsequenceLength()
        {
            Utility.Print("19.8 Longest Common Subsequence (p. 768)");
            // X = “ABCBDAB” and Y = “BDCABA”, the LCS(X, Y) = {“BCBA”, “BDAB”, “BCAB”} = 4
            string str1 = "ABCDAF";// ABCBDAB";
            string str2 = "ACBCF";// BDCABA";
            string lcsString = "";
            var LCS = DynamicProgramming.LongestCommonSubsequence(str1, str2, ref lcsString);
            Console.WriteLine($" Input1 : \t{str1}\n Input2 : \t{str2}\n Longest Common Subsequence : {lcsString} of length {LCS}");
        }

        public static void ConvertGivenRecurrenceToCode()
        {
            Utility.Print("Problem-1,2,3  Convert the following recurrence to code.(pp. 772 - 773)");
            /* T(0) =T(1) = 2
             * for n > 1
             * T(n) sum of [ 2 * T(i) * T(i-1) ] for all i starting from '1 to n-1'
             */
            int[] nums = { 1, 2, 3, 4, 5 };
            int[] tab = new int[nums.Max() + 1];
            foreach (var num in nums)
                Console.WriteLine($" Answer for number {num} is : {DynamicProgramming.RecurrenceToCodeUsingDPEfficient(num,tab)}");
        }

        public static void MaxValueContinousSubsequence()
        {
            Utility.Print("Problem-6  Maximum Value Contiguous Subsequence (p. 774)");
            int[][] inputs = { new int[] { -2, 11, -4, 13, -5, 2 }, new int[] { 1, -3, 4, -2, -1, 6 } };
            foreach (var input in inputs)
            {
                input.Print("Input values :");
                var maxValue = DynamicProgramming.MaxValueContinousSubsequence(input);
                Console.WriteLine($" Maximum Value Contiguous Subsequence in above array is : {maxValue}");
            }
        }

        public static void FindCatalanNumber()
        {
            Utility.Print("Problem-14  Catalan Numbers: How many binary search trees are there with n vertices?(p. 782)");
            int[] nodeArr = { 1, 2, 3, 4, 5 };
            int[] memo = new int[nodeArr.Max() + 1];            // Cache/table being populated and used in DP solution
            foreach (var noOfNodes in nodeArr)
            {
                var result = DynamicProgramming.CatalanNumber(noOfNodes, memo);
                Console.WriteLine($" No Of Possilbe 'Binary Search Trees' with '{noOfNodes}' Nodes i.e, CatalanNumber : \t{result}");
            }
        }

        public static void FindOrderToPerformMatrixMultiplications()
        {
            Utility.Print("Problem-15  Matrix Chain/Product Multiplications/Parenthesizations(p. 784)");
            int[][] arrChainOfMatrices = { new int[] { 40, 20, 30, 10, 30},
                                            new int[] {10, 20, 30, 40, 30},
                                            new int[] {10, 20, 30},
                                            new int[] { 1, 2, 3, 4, 3} };

            foreach (var chainOfMatrices in arrChainOfMatrices)
            {
                PrintMatrix(chainOfMatrices);
                //var result = DynamicProgramming.MatrixChainOrderBruteForce(chainOfMatrices, 1, chainOfMatrices.Length - 1); // Brute Force
                var result = DynamicProgramming.MatrixChainOrder(chainOfMatrices);
                Console.WriteLine($"\n Minimum no of Multiplication required to perform Matrix Multiplication on above Matrices : {result} ");
            }

            // local function to print Matrixces and their dimensions
            void PrintMatrix(int[] arr)
            {
                var len = arr.Length;
                Console.Write($"\n Input Matrices \t");
                for (int i = 1; i < len; i++)
                    Console.Write($" {arr[i - 1]}x{arr[i]} >>");      // end of last matrix becomes the start of next matrix

                Console.WriteLine();
            }
        }

        public static void ZeroOneKnapSackProblem()
        {
            Utility.Print("Problem-18  0-1 Knapsack Problem (p. 787)");
            int[] profit = { 60, 100, 120 };
            int[] wt = { 10, 20, 30 };
            int[] bagCapacityArr = { 30, 50, 60 };              // Max weight knapsack can handle
            int noOfItems = profit.Length;                      // no of items to choose from
            profit.Print("Profit Array");
            wt.Print("Weight Array");

            foreach (var bagCapacity in bagCapacityArr)
            {
                var maxProfit = DynamicProgramming.KnapSack(bagCapacity, profit, wt, noOfItems);
                Console.WriteLine($" Max Profit/Value we can get by filling KnapSack of capacity '{bagCapacity}' is : '{maxProfit}'\n");
            }
        }

        public static void CoinChangingMinimumNoOfCoins()
        {
            Utility.Print("Problem-19 Making Change (p. 789)");
            int[] coins = { 1, 5, 6, 8 };
            int[] changeReqFor = { 2, 6, 9, 11 };
            int noOfCoins = coins.Length;
            coins.Print("Coins Array");

            foreach (var changeAmt in changeReqFor)
            {
                var minNoOfCoins = DynamicProgramming.CoinChangeMinimumNoOfCoins(changeAmt, coins, noOfCoins);
                Console.WriteLine($" Min No of coins to get '{changeAmt}' in change is : '{minNoOfCoins}'\n");
            }
        }

        public static void FindLongestIncreasingSubsequence()
        {
            Utility.Print("Problem-20  Longest Increasing Subsequence (p. 790)");
            int[] input = { 5, 6, 2, 3, 4, 1, 9, 9, 8, 9, 5 };
            input.Print("Input");
            var maxLen = DynamicProgramming.LongestIncreasingSubsequence(input);
            Console.WriteLine($" 'Longest Increasing Subsequence' in above array is of Length : \t{maxLen}");
        }

        public static void BuildingBridgesOverRiver()
        {
            Utility.Print("Problem-24  Building Bridges in India (p. 795)");
            int[][,] inputArr = { new int[,] { { 4, 1 }, { 1, 3 }, { 3, 4 }, { 2, 2 } }, new int[,] { { 6, 2 }, { 4, 3 }, { 2, 6 }, { 1, 5 } } };
            foreach (var input in inputArr)
            {
                var len = input.GetLength(0);                    // no of cities

                // create list of cities from raw input data
                List<CityPair> cities = new List<CityPair>();
                for (int i = 0; i < len; i++)
                {
                    cities.Add(new CityPair(input[i, 0], input[i, 1]));
                    Console.WriteLine($" {input[i, 0]}||{input[i, 1]}");
                }

                var maxBridgesPossible = DynamicProgramming.MaxBridges(cities, len);
                Console.WriteLine($" Max Bridges that can be build over above pair of cities is : \t{maxBridgesPossible} \n");
            }

        }

        public static void SubsetSumProblem()
        {
            Utility.Print("Problem-25  Subset Sum: Sequence of N positive no's A1 . . . An, algorithm which checks if there exists subset of A whose sum of no's is T?(p. 796)");
            int[] input = { 2, 3, 7, 8, 10 };
            int[] sumArr = { 2, 4, 7, 21, 11 };
            input.Print("Set of No's: ");

            foreach (var findSubsetForSum in sumArr)
                Console.WriteLine($" Subset in above array whose sum equals to '{findSubsetForSum}', \tExists = {DynamicProgramming.SubsetSum(input, input.Length, findSubsetForSum)}\n");
        }

        public static void ParitionArrayInEqualHalf()
        {
            Utility.Print("Problem-28  Partition partition problem is to determine whether a given set can be partitioned into two subsets such that the sum of elements in both subsets is the same(p. 800)");
            int[] input = { 1, 5, 11, 5 };
            input.Print("Input Array");
            Console.WriteLine($" Above array can be paritioned into two subsets with equal total-sum : {DynamicProgramming.FindPartition(input, input.Length)}");

        }

        public static void FindOptimalBinarySearchTree()
        {
            Utility.Print("Problem - 30 Optimal Binary Search Tree (p. 804)");
            int[] keys = { 10, 20, 30, 40 };
            int[] frequency = { 4, 2, 6, 3 };
            keys.Print("Keys");
            frequency.Print("Frequence");

            var costOfSearch = DynamicProgramming.OptimalBinarySearchTree(keys, frequency, keys.Length);
            Console.WriteLine($"\n Min Cost of Search in Optimal BST with above 'keys & frequencey' is : \t{costOfSearch}");
        }

        public static void MinOperationToConvertStringAtoB()
        {
            Utility.Print("Minimum Edit Distance");
            Console.WriteLine("Problem - 31 Problem Statement: Given two strings A of length m and B of length n," +
                " transform A into B with a minimum number of operations of the following types: (p. 806)" +
                "\n >> delete a character from A,\n >> insert a character into A,\n >> change some character in A into a new character.\n");

            string[] aArr = { "abcdef" ,"harsh", ""};
            string[] bArr = { "azced", "", "har" };

            foreach (var a in aArr)
                foreach (var b in bArr)
                {
                    var minCost = DynamicProgramming.MinimumEditDistance(a, a.Length, b, b.Length);
                    Console.WriteLine($" Min Cost of converting A:'{a}' into B:'{b}' is: {minCost}\n");
                }
        }

        public static void AllPairsShortestPathFloydWarshall()
        {
            Utility.Print("Problem-32  All Pairs Shortest Path Problem: Floyd’s Algorithm:(p. 808)");
            // Directed Graph represented in form of Adjacancy Matrix of size V x V
            var infinity = int.MaxValue;        // infinity represents no edge
            int[,] graph = {    { 0, 3, infinity, 7 },
                                { 8, 0, 2, infinity },
                                { 5, infinity, 0, 1 },
                                { 2, infinity, infinity, 0 } };
            graph.Print(true);
            graph = DynamicProgramming.FloydWarshall(graph, graph.GetLength(0));

            Console.WriteLine($" After Applying Floyd Warshall, Min Distance is Shows below:");
            graph.Print(true);
        }

        public static void OptimalStrategyForACoinGame()
        {
            Utility.Print("Problem-33  Optimal Strategy for a Game(p. 810)");
            int[][] inputs = { new int[] { 8, 15, 3, 7 }, new int[] { 2, 2, 2, 2 }, new int[] { 20, 30, 2, 2, 2, 10 } };
            
            foreach(var input in inputs)
            {
                var len = input.Length;
                int[,] cache = new int[len, len];
                input.Print("Given Coins");

                var maxWinValue = DynamicProgramming.OptimalStrategy(input, len);                       // Tabulation 
                //var maxWinValue = DynamicProgramming.OptimalStrategyMemo(input, 0, len - 1, cache);     // Memoization
                //var maxWinValue = DynamicProgramming.OptimalStrategyGameRecursive(input, 0, len - 1);   // Recursive Brute Force 
                Console.WriteLine($" Max value Player1 (us) can get with above set of values : {maxWinValue}\n");
            }
        }

        public static void LongestPalindromicSubsequence()
        {
            Utility.Print("Problem-35  Longest Palindrome Subsequence (p. 813)");
            string[] inputArr = { "AGDDDA", "AGCTCBMAACTGGAM", "GEEKSFORGEEKS" };
            foreach (var input in inputArr)
            {
                var len = input.Length;

                //var maxLps = DynamicProgramming.LongestPalindromicSubsequenceRecursive(input, 0, len - 1);    // Recursive
                //Console.WriteLine($" Length of Longest Palindromic Subsequence LPS in '{input}' is of length: '{maxLps}'");

                var maxLps = DynamicProgramming.LongestPalindromicSubsequence(input, len);                    // DP Tabulation
                Console.WriteLine($" Length of Longest Palindromic Subsequence LPS in '{input}' is of length: '{maxLps}'");
            }
        }

        public static void LongestPalindromicSubString()
        {
            Utility.Print("Problem-36  Longest Palindromic Substring (p. 814)");
            string[] inputArr = { "AAAABBAA", "AGDDDA", "AGCTCBMAACTGGAM", "GEEKSFORGEEKS", "ABAXAABAXABYBAXABYB" };
            foreach (var input in inputArr)
            {
                var len = input.Length;
                var maxLps = DynamicProgramming.LongestPalindromicSubString(input, len);                    // DP Tabulation
                Console.WriteLine($" Length of Longest Palindromic SubString LPS in '{input}' is of length: '{maxLps}'");
            }
        }

        public static void NoOfTimesStringOccursAsSubsequenceInAnotherString()
        {
            // GFG https://www.geeksforgeeks.org/find-number-times-string-occurs-given-string/
            Utility.Print("Problem - 37 Find number of times a string occurs as a subsequence in given string (p. 816)");
            string text = "GeeksforGeeks";
            string pattern = "Gks";
            var times = DynamicProgramming.TimesStringAOccursAsSubsequenceInStringB(text, text.Length, pattern, pattern.Length);
            Console.WriteLine($" No of times '{pattern}' appears as subsequence in '{text}' is: {times}");
        }

        public static void MaxSizeSquareSUbMatrix()
        {
            Utility.Print("Problem-40  Maximum size square sub-matrix with all 1’s:(p. 819)");
            int[,] input = { { 0, 1, 1, 0, 1 },
                             { 1, 1, 0, 1, 0 },
                             { 0, 1, 1, 1, 0 },
                             { 1, 1, 1, 1, 0 },
                             { 1, 1, 1, 1, 1 },
                             { 0, 0, 0, 0, 0 } };
            input.Print(true);
            var maxSize = DynamicProgramming.MaxSizeSquareSubMatrix(input, input.GetLength(0), input.GetLength(1));
            Console.WriteLine($" Maximum size square sub-Matrix with all 1's in above matrix can be of size {maxSize} ");
        }

        
        public static void MaximumSumRectangle()
        {
            // GFG https://www.geeksforgeeks.org/maximum-sum-rectangle-in-a-2d-matrix-dp-27/
            Utility.Print("Problem-42  Maximum sum rectangle Sub-matrix:" +
                " Given an n × n matrix M of positive and negative integers," +
                " give an algorithm to find the sub-matrix with the largest possible sum.(p. 823)");
            int[][,] inputArr = { new int[,] {{ 2, 1, -3, -4, 5 },
                                              { 0, 6, 3, 4, 1 },
                                              { 2, -2, -1, 4, -5 },
                                              { -3, 3, 1, 0, 3 } },
                                  new int[,] {{ 1, 2, -1, -4, -20},
                                              { -8, -3, 4, 2, 1},
                                              { 3, 8, 10, 1, 3},
                                              { -4, -1, 1, 7, -6} } };
            foreach (var input in inputArr)
            {
                input.Print();
                var maxSum = DynamicProgramming.MaximumSumRectangleSubMatrix(input, input.GetLength(0), input.GetLength(1));
                Console.WriteLine($" Maximum Sum Rectangular SubMatrix in above Matrix has SUM : {maxSum}\n");
            }
        }

        public static void FindingOptimalJumpsToReachLast()
        {
            // Problem undestanding https://youtu.be/vBdo7wtwlXs
            Utility.Print("Problem-45  Finding Optimal Number of Jumps To Reach Last Element:(p. 824)");
            int[][] inputArr = { new int[] { 1, 3, 5, 8, 9, 2, 6, 7, 6, 8, 9 }, new int[] { 1, 1 , 1, 1, 1, 1, 1, 1, 1, 1, 1 } };
            foreach (var input in inputArr)
            {
                input.Print("Jump Array");
                Console.WriteLine($" Min no of jumps required to reach end in above array is: {DynamicProgramming.OptimalJumpsToReachLastOn(input, input.Length)}\n");
            }
        }

        public static void CircusTowerRoutine()
        {
            Utility.Print("Problem-48  A circus is designing a tower routine(p. 827)");
            int[,] input = { { 65, 100 }, { 70, 150 }, { 56, 90 }, { 75, 190 }, { 60, 95 }, { 68, 110 } };
            List<Person> persons = new List<Person>();
            for (int i = 0; i < input.GetLength(0); i++)
            {
                persons.Add(new Person(input[i, 0], input[i, 1]));
                Console.Write($" Ht: {input[i, 0]} Wt: {input[i, 1]} ||");
            }
            var maxPersonPossible = DynamicProgramming.CircusTowerRoutine(persons, persons.Count);
            Console.WriteLine($"\n Max person possible with above list of persons is: {maxPersonPossible}");
        }

        // Tushar Roy https://youtu.be/WepWFGxiwRs
        // GFG https://www.geeksforgeeks.org/word-break-problem-dp-32/
        public static void WordBreakProblem()
        {
            /* Problem Statemenet: Given an input string and a dictionary of words,
             * find out if the input string can be segmented into a space-separated sequence of dictionary words
             */
            Utility.Print("WordBreakProblem");
            HashSet<string>[] dictonaryArr = { new HashSet<string>() { "i", "a", "am", "ace" },
                                                new HashSet<string>() { "c", "od", "e" },
                                                new HashSet<string>() {"leet","code" } };
            string[] inputArr = { "code", "iamace", "leetcode" };
            foreach (var dictionary in dictonaryArr)
                foreach (var input in inputArr)
                {
                    var memo = new Dictionary<string, bool>();
                    dictionary.Print();
                    //var result = DynamicProgramming.WordBreakProblemRecursive(input, dictionary);
                    var result = DynamicProgramming.WordBreakProblemMemo(input, dictionary, memo);
                    //var result = DynamicProgramming.WordBreakProblemTabulation(input, dictionary);
                    Console.WriteLine($"\n For above Dictonary given sentence \'{input}\' can be split into words which exists: \t{result}");
                }
        }
    }
}
