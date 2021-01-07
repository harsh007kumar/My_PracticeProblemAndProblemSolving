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
            //DutchFlagProblem();
            //EventAndDelegate();
            //CircularPetrolPumpProblem();
            //AddBinary();
            //MultiplyLargeNumbersRepresentedAsString();
            //IsomorphicStrings();
            //SlidingWindowMaximum();
            //SortArrayByParityII();
            //ShortestDistanceFromAllBuildings();
            //AlienDictionary();
            //ProductOfArrayExceptSelf();
            //ContinuousSubarraySum();
            //TwoSum();
            //ThreeSum();
            //FourSum();
            FourSumII();
            //MostCommonWord();
            //ValidSudoku();
            SudokuSolver();
            //ComplimentBase10();
            //SpiralMatrix();
            SpiralMatrixII();
            //WinnerOfTicTacToe();
            //ReorderDataInLogFiles();
            //MajorityElement();
            //MajorityElementII();
            //LRUCache();
            //CompressedStringIterator();
            //MeetingRoomsII();
            //NumberOfIslands();
            //TrapRainWater();
            //FindTheCelebrity();
            //IsPalindrome();
            //WordSearch();
            //WordSearchII();
            //ImplementIndexOf();
            //UniquePaths();
            //UniquePathsII();
            //RotateImage();
            //DesignSearchAutocompleteSystem();
            //FirstMissingPositive();
            //PartitionLabels();
            //IntegerToRoman();
            //CompareVersionNumbers();
            //CourseSchedule();
            CourseScheduleII();
            //SymmetricTree();
            BasicCalculator();
            //BasicCalculatorII();
            //ReverseWordsInAString();
            //ReverseWordsInAStringII();
            //MinimumCostToConnectSticks();
            //MinCostToMergeStones();
            //StringToInteger();
            //ReverseNum();
            //SearchA2DMatrixII();
            //CloneGraph();
            //LetterCombinations();
            //SerializeAndDeserializeBST();
            //MinimumHeightTrees();
            ArrayFormationThroughConcatenation();
            //MinCostToMoveChips();
            //FindMinimumInRotatedSortedArrayII();
            //FindTheSmallestDivisorGivenAThreshold();
            //ConstructTreeFromPreAndInOrderTraversal();
            //DayOfTheWeek();
            //CanPlaceFlowers();
            //TwoSumLessThanK();
            //TwoSumInBST();
            //TwoSumInBSTs();
            //BinaryTreeTilt();
            //MaxAncestorDiff();
            //BinaryTreeLongestConsecutiveSequenceII();
            //SetMatrixZeroes();
            //MergeSortedArray();
            //FlipAndInvertImage();
            //ValidSquare();
            //MySqrt();
            //PermutationsII();
            //MaxPossibleSumOfProductOfTheIndexesMultipliedByElement();
            //RegularExpressionMatching();
            //PoorPigs();
            //RemoveInterval();
            //GroupAnagrams();
            //LongestMountain();
            //MergeIntervals();
            //InsertIntervals();
            //MaximumProductSubarray();
            //MirroReflection();
            //GenerateParenthesis();
            //BestTimeToBuyAndSellStockII();
            //BestTimeToBuyAndSellStockIII();
            //DecodeString();
            //KthLargestElementInArray();
            //AssignCookies();
            //CousinsInBinaryTree();
            //MinDepthOfBinaryTree();
            NumbersAtMostNGivenDigitSet();
            InsertDeleteGetRandomO1();  // RandomizedSet
            UniqueMorseCodeWords();
            WordLadder();
            HouseRobber();
            HouseRobberII();
            HouseRobberIII();
            SmallestIntegerDivisibleByK();
            LongestSubstringWithAtLeastKRepeatingCharacters();
            JumpGame();
            JumpGameII();
            JumpGameIII();
            JumpGameIV();
            LinkedListRandomNode();
            IncreasingOrderSearchTree();
            KthFactorOfN();
            SingleNumberII();
            LargestNumber();
            NextPermutation();
            MinStack();
            PairsOfSongsWithTotalDurationsDivisibleBy60();
            BullsAndCows();
            RemoveDuplicatesFromSortedArrayII();
            FlipColumnsForMaximumNumberOfEqualRows();
            SubstringWithConcatenationOfAllWords();
            LowestCommonAncestorDeepestLeaves();
            BurstBalloons();
            PalindromePartitioning();
            SquaresSortedArray();
            BinaryTreeRightSideView();
            MinimumCostToHireKWorkers();
            IncreasingTripletSubsequence();
            PermutationInString();
            CherryPickupII();
            MinOperationsToReduceToZero();
            DecodedStringAtIndex();
            SmallestRangeII();
            RotateList();
            AllPossibleFullBinaryTrees();
            ConstructStringFromBinaryTree();
            FindDuplicateSubtrees();
            NextGreaterElementI();
            NextGreaterElementII();
            NextGreaterElementIII();
            InsertIntoBST();
            FindDiagonalOrder();
            NodesAtKDistanceFromGivenNodeInBinaryTree();
            LeafSimilarTrees();
            RedundantConnection();
            CPU_OptimizationProblem();
            AmazonShoppingProblem();
            DecodeWays();
            ReachANumber();
            PseudoPalindromicPaths();
            GameOfLife();
            ReOrderLinkedList();
            RemoveDuplicatesFromSortedListI();
            RemoveDuplicatesFromSortedListII();
            LinkedListiNBinaryTree();
            MergeInBetweenLinkedLists();
            FindACorrespondingNodeOfABinaryTreeInACloneOfThatTree();
            PartitionList();
            BeautifulArrangement();
            MakeTheStringGreat();
            RemoveOutermostParentheses();
            DailyTemperatures();
            SortCharactersByFrequency();
            SortArrayByIncreasingFrequency();
            DetectCyclesIn2DGrid();
            SmallestStringStartingFromLeaf();





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
                high = arr.Length - 1;
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
            // https://leetcode.com/problems/gas-station/
            Utility.Print("Find the first circular tour that visits all petrol pumps");
            Utility.Print("134. Gas Station");
            int[][,] petrolsPumpsArr = {    new int[,] { { 4, 6 }, { 6, 5 }, { 7, 3 }, { 4, 5 } },
                                            new int[,] { { 6, 4 }, { 3, 6 }, { 7, 3 } },
                                            new int[,] { { 3, 3 }, { 3, 4 }, { 4, 4 } },
                                            new int[,] { { 5, 4 } },
                                            new int[,] { { 5, 8 } } };  // multiple array inputs
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
                var results = DailyProblem.FourSum(numsArr[i], target[i]);
                foreach (var pair in results)
                    Console.WriteLine($" Nums in above array which evaluate to \'{target[i]}\': \t{pair[0]} + {pair[1]} + {pair[2]} + {pair[3]} = {target[i]}");
                Console.WriteLine();
            }
        }

        public static void FourSumII()
        {
            // https://leetcode.com/problems/4sum-ii/
            Utility.Print("454. 4Sum II");
            int[] A = { 1, 2 }, B = { -2, -1 }, C = { -1, 2 }, D = { 0, 2 };
            A.Print("A");
            B.Print("B");
            C.Print("C");
            D.Print("D");
            Console.WriteLine($" No Of Tuples such that A[i]+B[j]+C[k]+D[l]==0 are {DailyProblem.FourSumCount(A, B, C, D)}\n");
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
            char[][] expected = { new char[]{'5','3','4','6','7','8','9','1','2'},
                                new char[] {'6','7','2','1','9','5','3','4','8'},
                                new char[] {'1','9','8','3','4','2','5','6','7' },
                                new char[] {'8','5','9','7','6','1','4','2','3' },
                                new char[] {'4','2','6','8','5','3','7','9','1' },
                                new char[] {'7','1','3','9','2','4','8','5','6' },
                                new char[] {'9','6','1','5','3','7','2','8','4' },
                                new char[] {'2','8','7','4','1','9','6','3','5' },
                                new char[] {'3','4','5','2','8','6','1','7','9' } };
            
            DailyProblem.SudokuSolver(board);
            if (ValidateResult(board, expected))
                Console.WriteLine($" Succesfully SOLVED above SUDOKU puzzle by filling the empty cells as below\n");
            board.Print("SOLVED SUDOKU BOARD");
            
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
            for (int i = 0; i < logs.Length; i++)
                ValidateResult(DailyProblem.ReorderDataInLogFilesByDividingDigitsAndWordLogs(logs[i]), expectedOutput[i]);

            void ValidateResult(string[] arr1, string[] arr2)
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
            string[] words = { "oath", "pea", "eat", "eating", "rain" };
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
            string[] haystack = { "hello", "", "aaab", "mississippi" };
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
            foreach (var lengthOfPart in parts)
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

        public static void CompareVersionNumbers()
        {
            // https://leetcode.com/problems/compare-version-numbers/
            Utility.Print("165. Compare Version Numbers");
            string[][] versionsArr = { new string[] { "1.01", "1.001" }, new string[] { "1.01", "1.0.0" }, new string[] { "0.1", "1.1" }, new string[] { "1.0.1", "1" }, new string[] { "7.5.2.4", "7.5.3" } };
            foreach (var versionPair in versionsArr)
                Console.WriteLine($" Version1: {versionPair[0]} || \tVersion2: {versionPair[1]} || \tVersion Comparison result: {DailyProblem.CompareVersionNumbers(versionPair[0], versionPair[1])}");
        }

        public static void CourseSchedule()
        {
            // https://leetcode.com/problems/course-schedule/
            Utility.Print("207.Course Schedule");
            int[][] input = new int[][] { new int[] { 1, 0 }, new int[] { 0, 1 } };
            input.Print();
            Console.WriteLine($" For Above Courses you have to take, is it possible to finish them: {DailyProblem.CourseSchedule(2, input)}");
        }

        public static void CourseScheduleII()
        {
            // https://leetcode.com/problems/course-schedule-ii/submissions/
            Utility.Print("210. Course Schedule II");
            int[][][] input = { new int[][] { new int[] { 1, 0 }, new int[] { 0, 1 } },
                                new int[][] { new int[] { 1, 0 }, new int[] { 2, 0 }, new int[] { 3, 1 }, new int[] { 3, 2 } },
                                new int[][] { new int[] { 1, 0 } } };
            int[] numCourses = { 2, 4, 3 };
            for (int i = 0; i < numCourses.Length; i++)
            {
                input[i].Print();
                var orderingOfCourses = DailyProblem.CourseScheduleII(numCourses[i], input[i]);
                Console.WriteLine($" For Completing above '{numCourses[i]}' Courses you have to start Course in below Order:");
                foreach (var courseId in orderingOfCourses)
                    Console.Write($" {courseId} <<");
                Console.WriteLine(Utility.lineDelimeter);
            }
        }

        public static void SymmetricTree()
        {
            // https://leetcode.com/problems/symmetric-tree/
            Utility.Print("101. Symmetric Tree");
            TreeNode symmetric = new TreeNode(1)
            {
                left = new TreeNode(2)
                {
                    left = new TreeNode(3),
                    right = new TreeNode(4)
                },
                right = new TreeNode(2)
                {

                    left = new TreeNode(4),
                    right = new TreeNode(3)
                }
            };
            TreeNode notSymmetric = new TreeNode(1)
            {
                left = new TreeNode(2)
                {
                    right = new TreeNode(3)
                },
                right = new TreeNode(2)
                {
                    right = new TreeNode(3)
                }
            };
            Console.WriteLine($" Above Tree isSymmetric: {DailyProblem.IsSymmetricRecursive(symmetric.left, symmetric.right)}");    // Recursive Solution
            Console.WriteLine($" Above Tree isSymmetric: {DailyProblem.IsSymmetricIterativeBFSApproach(notSymmetric)}");            // Iterative Solution
        }


        public static void BasicCalculator()
        {
            // https://leetcode.com/problems/basic-calculator/
            Utility.Print("224. Basic Calculator");
            string[] inputArr = { "1 + 1", " 2-1 + 2 ", "(1+(4+5+2)-3)+(6+8)" };
            foreach (var input in inputArr)
            {
                int startingIndex = 0;
                Console.WriteLine($" Input: \'{input}\' evaluates to : \'{DailyProblem.BasicCalculator(input, ref startingIndex)}\'");
            }
        }
        public static void BasicCalculatorII()
        {
            // https://leetcode.com/problems/basic-calculator-ii/
            Utility.Print("227. Basic Calculator II");
            string[] inputs = { "3+2*2", " 3/2 ", " 3+5 / 2 ", "42", "0-2147483647", "14/3*2", "1-1+1" };
            foreach (var input in inputs)
            {
                Console.WriteLine($" Input: \'{input}\'");
                //Console.WriteLine($" \tCalculates To: {DailyProblem.BasicCalculatorII(input)}");
                Console.WriteLine($" \tCalculates To: {DailyProblem.BasicCalculatorIIFaster(input)}");
            }
        }


        public static void ReverseWordsInAString()
        {
            // https://leetcode.com/problems/reverse-words-in-a-string/
            Utility.Print("151. Reverse Words in a String");
            string[] inputArr = { "the sky is blue", "  hello world  ", "a good   example", "  Bob    Loves  Alice   ", "Alice does not even like bob",
                "58jQ7q9wVX       R    IpHr  PhhTE a8PVETA6a    8gRZzYIcm0 5e      O0       G0KMWhibvb   3T4xjF     ZVvCk0     RfDxBA4D86   EXPD7DHbt  3e43ty   ttSQf  vrQ3CL       ecN       PvX8HYN4uy       VoTmn9 B8mWctu PDzb8     lJgpP9Ed      u  I   Ks5ehQz1YC   A3WQ    yTx0b      9i      ZxfhD13 V0  0    d       XE  SGQVF  xo      4IW9nR    6b1zL5D6     AiGw Fel       94Ydz   4Z8hUM      Kp7lqan  w  2K   0w8hqDv Eu578UQu VYeUzIfj9       OcVb7MkfT       BWqda0Uv1E     oXK   0m       yX   eSuYZAKvZ      lPCcdP4IqB      OkgWkrgwy       b      Dwocr0S    79RG       dzBvAbNP4      AeYHAp3       tu      Sg4Rl SmaUYK   BiYtjAtEG ONXS0  8YUQZMt RMo2NtE4     6gczG       D7b       jkn   TuDrLd       DNX2WfO   A    FkJNYO GKoxywV8 c    7yH       xmB     x7Lzu73obb  aLe     5TVccMVw       Id0Ro     kLMi80  86sf     4Y3DnBmV       7BcA   ALOOVkYS    S4c       X    JQ1xU6H   zhE4r7     WwsyuPFeqW      7a39  jLH8bUbCe     LBDyvk1Z     BbjlJ   teOn3T4l   zBZUayGq       V0B   odjTY9X3      mTT8G7 RmVAiMT    GGLtvo ye0JDhVNZ     Pu   soF0VFZi5m      2CjbEQO s3CuQ      F  Z4ondG     xkrnqHz cfx      DaTy      PN   MZL      TPq   m0      YCIZg1iYs  FEg  in    NlpKKN4   NgNPwU  xZ6       w5uLUUS  DEGx  Lb4msfrjve     zUYrkt       at     sqXNnWy1U   4SYDWTTvBq  BimhW      5sAm  aU    oJGZQI      pexbXa   dkBmO6TlS       v2439r Yg    4cb59qz9Z  JgWdfo61Q  jstB     HRmHLLgKGl    TvMhGWw       O1dWcLGaDn       nl   EENjJdX     nGxG94eFbq     Mc8H     5oo6MFhyeL       fyc51V6h56      CyVmBnQO       AA6vJ3f  rvDr   GKxqZt4 74PR    iibqCCeJLv      HWipSriC       3ttGuPxF     QF8bTr   WJORgWM       f1Ol3t      8EYkQ  4WGYrc       l2b8  JgjWGxrY      9Dwhbqqm      3SSBywBTc4     9u     YP       P0jLTyGI6      Bp       8ljYOHh     rQz2KKHp7    cYGjLDgbV     CezVSntkSJ     vLMh      k2x  XeSj    pPV3r    DmHNSKy   e       oo  zRyUV     H     Unt0Lplxx      c   Hp    6MA8    eee3lF MD0R5G       b99     I   aLHBPt3We Rv5 41w2qG7    pMv       zQrvIxgKjx  AO28K5BL      4fp     8V       j       jw 74MD       PVyENWdeG     0s      JV     y7C8Ll4   NAtVtTVY5w      siyxBv       wYVzhr zpLg     twb9YWns  XJ gkMQ1       Fe7OT fCF4FBMW     AqSz5    WstY1  Wjwbd      7NPJludT       7jvzV1    CZspKEX    F1QudO    0nEmsbQ     YC   UW8pOdb6k       xfxohy fVHYN       2  R82LIGj TYEz1P4     wfiu2B      ztkrzC       XqUSvoOgK   nU6cB28v8P       C4OFzyAL9  yH3qVYNj   EasJ   DWw      5b    HmtIeUQf     2f     FHWdX6   6XXHL0mDcD   H3W       Szd58e   aIodDIegG      rO    IqQ       K4      tYg y       m3IV1       E7ntocq   x8cq     n     Sgv2Kq   s0P74      yD1s NT       MN20      HX      uS  bxuEvB5p 0CY9oxYs pByJJ0A      yAa8pu5      KAgy N9HW58 h4tcvO5UrD    Xk8Dj       a FGJ       FeMOMslP6       9c2Q      bS    hz0jsT96     OUomibItLQ C2E4yXiKD       iQkPx     NRaNb8i     dsd3J7zm  WDGPJoD uI      4rvAKo    m6ZJT    BH   9GFKSS    5uepU g5 IQm3",
                "  hello world  " };

            foreach (var input in inputArr)
                Console.WriteLine($" Input: \t\'{input}\'\n Output: \t\'{DailyProblem.ReverseWordsInAString(input)}\'\n");
        }

        public static void ReverseWordsInAStringII()
        {
            // https://leetcode.com/problems/reverse-words-in-a-string-ii/
            Utility.Print("186. Reverse Words in a String II");
            char[] input = { 't', 'h', 'e', ' ', 's', 'k', 'y', ' ', 'i', 's', ' ', 'b', 'l', 'u', 'e' };
            input.Print("Input String");
            DailyProblem.ReverseWordsInAStringII(input);
            input.Print("Reverse String");
        }

        public static void MinimumCostToConnectSticks()
        {
            Utility.Print("1167. Minimum Cost to Connect Sticks");
            int[][] inputArr = {new int[] { 2, 4, 3},
                                new int[] { 2, 2, 3, 3},
                                new int[] { 1, 2, 5, 10, 35, 89},
                                new int[] { 3354, 4316, 3259, 4904, 4598, 474, 3166, 6322, 8080, 9009},
                                new int[] { 20, 4, 8, 2},
                                new int[] { 8471,4944,1488,3935,6725,6025,3848,4138,2541,4045,3702,1717,8453,7933,8805,1469,652,4438,5906,3792,1162,1383,9588,7076,2155,1817,8559,2642,5602,2094,3478,2549,3457,1341,334,6526,7922,9518,9044,856,4319,7161,2488,9683,3006,2331,1927,3439,7533,6843,7809,6903,9055,7076,6421,7770,2938,9031,6869,3810,3676,4854,8241,4435,3091,3718,7372,9977,7448,830,2318,6537,6213,9045,6957,5183,7542,4759,5887,6728,9619,3728,8792,7519,2664,2895,5394,8311,7366,9808,4163,3142,3087,1572,6419,6384,1671,1419,7843,3384,867,7002,8211,1952,7542,511,6231,9926,7472,3864,1411,8562,7024,7436,1667,4936,6472,6523,898,461,365,3305,2498,7604,5336,4664,1687,6546,4653,1295,414,8724,466,5891,5386,8721,3863,9588,8591,1212,4508,2704,8993,2829,2789,890,352,4664,203,503,2337,3861,548,2791,886,5343,8410,7126,5298,8655,9555,8130,2036,5951,728,4058,5263,9579,6520,6406,9888,7866,7347,8828,3516,6499,8808,7087,7391,1067,2066,3741,7721,5194,4663,9544,3668,1742,6030,3705,4208,405,4034,7517,6528,8995,6185,5924,6678,5639,6725,6776,5132,5686,1607,8327,975,7171,4126,4169,2562,2742,6605,2625,711,1074,6651,8225,1125,5438,5457,5367,6726,206,4226,4327,3412,7587,9246,8288,6590,1591,6479,2366,7492,802,8793,6708,6271,8898,535,5871,4186,3361,5884,1418,5082,3838,4034,8259,2061,3663,9708,3881,5084,3380,7426,571,6877,2243,1121,1993,2513,2243,9822,8188,2748,7752,1769,8237,1961,1321,919,3927,4146,9866,5043,5184,6588,2745,2231,2758,6598,2707,3382,5931,3097,8110,4292,10,4526,2141,6677,9621,3991,7614,6628,9649,5186,5881,9872,2881,7957,6343,3640,5236,1527,5809,3139,7441,2745,4620,7909,1218,6705,1413,659,565,6291,6103,5616,8789,3948,8052,4290,9948,7014,9123,30,2343,3187,5352,9831,3350,8600,815,7414,4618,9483,6807,308,6513,333,6149,6701,3056,6567,6413,1832,7088,4651,442,7831,4996,508,5593,353,4872,6926,9188,5247,9555,611,4833,2016,2101,7313,8219,6014,4716,2718,6315,3774,3759,9367,3604,1956,5093,7644,9500,8480,4121,2402,2318,2410,2978,289,3606,8790,513,7206,9513,3498,7004,8149,309,678,3381,2298,3082,5530,2303,519,3491,2533,6962,7135,2411,1359,4913,7388,3621,1569,6243,7106,627,8953,8944,8032,4516,2708,9242,3673,6631,4062,4846,6906,1003,4333,7825,5399,459,6082,4998,7221,7746,3077,4986,4873,1369,4978,2412,9864,9223,4573,1807,1194,17,8650,2828,3464,6594,5661,8057,187,5823,3003,1198,7893,5772,8993,173,4828,1168,7915,3951,6534,9457,4653,8323,5411,635,7205,7964,1169,3100,9415,4948,6724,1666,9388,7440,245,384,8352,4548,9314,8117,7542,277,1949,7164,4294,971,7166,7433,5803,1259,8501,5286,9117,950,4967,7961,3216,1729,4529,1814,9012,4998,4657,93,9362,6953,8302,895,1180,3090,7847,739,1711,7928,2367,7622,5411,3507,1151,9174,5997,5007,8657,7499,1664,5962,6027,6950,5129,4083,354,5461,483,2986,2044,6242,4440,9614,7111,5380,7942,2512,4510,2072,994,6549,3301,7702,5901,3229,2078,4619,7487,2249,4318,9442,7111,3657,2900,9682,1917,9158,5729,5836,7595,2947,4707,8043,8318,2490,2559,8583,3512,5601,7885,1337,9684,4165,2321,9448,8146,6732,6544,1607,6137,3524,6078,3792,1168,9907,6861,7985,8178,8625,373,6860,8157,4975,4763,7203,4498,45,4839,7824,1248,439,8490,8378,2648,8895,4471,7005,2672,8136,4084,4378,3796,2663,251,5542,2780,5825,5227,1563,7685,3029,7958,9283,8303,7686,2793,4265,1162,1948,7090,6573,274,3502,2789,9785,8466,5269,9868,5864,5931,7810,6273,7864,9395,607,1707,3097,5663,6141,3179,7502,1347,4106,9167,8682,9823,9416,7527,3686,2923,1261,2664,8845,2111,2374,5282,5524,378,4681,9044,9328,7420,612,7235,3274,8047,8801,3979,3876,7775,6045,4881,4492,3869,815,2046,9328,5534,9793,9359,3492,9090,2174,4626,3361,4367,1285,154,7136,3658,1890,5448,4068,2884,9568,9473,8790,7524,3285,1214,168,1698,6046,5414,6672,7700,3270,8973,8357,405,9720,3409,1928,4633,1292,9824,4572,197,7579,6671,515,4860,2643,6974,8175,913,1370,4477,441,9048,4956,6903,5630,2075,6530,316,8022,8071,4993,5422,4612,7970,508,1657,5822,8027,7357,3942,5969,534,1898,6895,5564,8633,7926,2619,4174,4520,614,2580,7790,7373,2330,3043,6228,7461,7892,6158,2570,9067,2403,7558,5417,4809,7257,6509,2428,4629,9748,4099,565,8768,1435,7649,8082,1091,3228,2792,5586,7350,9468,5608,9794,2913,585,8120,2432,8418,6703,1469,1437,6997,5466,7733,3814,5433,6736,2443,9759,1805,6096,9537,7309,4417,7090,4189,3447,4930,1712,1074,742,9422,8348,1491,6845,4773,1671,8544,6882,8924,2590,4546,577,4604,1014,5693,7917,6954,5203,3157,3057,720,5873,8702,7834,6534,5861,6014,2867,7989,8454,5863,888,3182,5504,5595,7829,7762,8028,5299,9524,4534,18,818,7341,5425,7884,4456,8909,2700,1991,7865,6093,2222,5921,5112,8791,8863,7662,4154,3964,9288,6374,1821,8607,5388,9900,6119,4025,1192,1212,3236,4956,3960,5889,7556,3299,3793,9702,3902,5081,3358,1576,6488,2784,211,1789,8557,4243,3818,672,1759,9480,7655,1263,7543,8909,5311,1810,9911,6120,7115,1066,7563,1315,8055,5767,6856,3862,4220,8439,2595,3177,4053,4576,7462,267,3064,2352,5260,3086,1155,6888,4399,7043,6020,6550,4407,8547,3916,2062,112,8597,5107,7556,9458,7391,835,505,3944,3843,4879,2142,14,5099,9833,4488,6323,918,9778,9286,3597,2604,3274,4360,4788,7972,6634,8201,682,9294,1739,3065,7875,5157,1191,7795,3829,7504,8277,3999,4620,8060,2851,5137,6682,3206,6454,9783,4918,3081,8402,9253,3662,2958,6358,9327,8727,1668,4942,5325,572,888,5570,2702,5927,4256,8874,6808,9807,8625,8003,549,6314,8258,4898,1010,8238,421,9984,6420,1810,4036,8762,6202,2206,7644,1845,9090,7783,1656,3629,745,6247,9489,3377,8146,171,1041,813,5327,4790,4080,3615,4363,5,4930,6545,763,6102,4704,587,2469,5244,7125,7521,6871,8780,8077,8708,3717,8946,1554,6788,8216,6822,4401,8762,7758,216,9010,3736,2119,5473,742,8432,8613,4147,3801,1156,7313,6980,3979,8238,2157,7994,332,327,7067,1407,4131,6832,7410,8224,1238,2864,3969,5901,8754,9367,7140,1677,3165,4281,9810,5630,9027,2439,6474,8042,4314,9956,9241,3769,7231,476,1687,3193,4451,9510,7730,978,2337,2907,4311,2343,7109,1241,4671,2987,4277,9264,1132,8991,9296,8027,8639,257,3054,2165,5095,8631,5938,5229,165,8661,5874,6144,7345,8314,9062,6735,2498,5462,1387,8087,3534,4725,5788,3865,6186,7461,768,8859,6987,5091,8840,2087,3868,8301,7303,198,6214,9475,4146,2975,3606,6212,9490,6361,3194,2264,7764,4496,9154,7472,3472,3313,2862,3678,6165,3560,4081,8855,679,9602,6248,9421,7955,3252,1088,1407,3779,9831,4358,305,2721,6427,5035,4668,9675,9074,2762,7331,4722,2054,432,3366,1389,1251,6885,3456,5606,2115,4399,6443,8064,597,4802,9893,7767,4749,8483,8997,8353,894,5791,4222,6027,613,3272,514,5949,3487,1613,4675,3048,3172,6534,2972,3476,2892,9038,1044,2691,3025,2002,9092,1881,9140,3719,2598,9503,5939,7647,1008,3964,7617,7699,1255,4188,7167,1130,5827,630,4449,2133,5213,1183,7001,2150,4613,5832,9078,3027,8770,2902,4568,9322,4012,3541,2613,8044,4533,319,7265,5008,3695,1756,72,7645,7957,3526,2420,7910,1145,8832,2579,573,8318,5965,5771,3767,9554,9045,4013,6757,1897,602,6849,9093,8291,9699,1994,7268,6755,7777,369,5469,4037,2610,8669,6980,9443,3944,3633,2344,1870,204,6264,5260,2238,3281,9163,6959,5849,5874,8368,1745,871,2988,3753,5864,6755,8642,6531,8239,4964,734,5044,5653,4054,496,7408,2090,7214,5707,9591,9901,5695,9286,9333,1702,1663,1711,4024,7215,4066,3097,3403,5341,5747,5866,3456,1497,871,5211,177,804,7229,6958,6653,4288,1184,6847,7018,9315,6780,9105,6885,9533,4088,590,3466,1460,4528,3248,4370,3791,1548,470,6497,3919,8807,9159,5714,978,8279,2052,6729,5517,7448,5076,5224,2086,9063,1665,1516,1645,5511,1420,4894,2508,3104,7334,4681,5821,6004,7939,181,302,3765,3307,6,526,6427,1287,6742,443,8447,9854,8397,112,6055,2136,3531,5194,6148,908,2784,9476,1149,4520,630,7694,490,2019,1106,6555,3555,4447,407,3694,3277,4040,7861,3796,8658,1180,9808,6911,489,5951,8588,7431,8511,5231,2657,1736,1139,5608,7895,7822,2737,8405,8861,9253,538,5573,8524,5068,1193,3237,448,5312,6943,2988,820,5822,918,9740,8268,6250,6486,2417,8719,8785,9939,7020,1998,5726,761,6724,6759,1876,7671,8155,275,999,9438,8123,9016,3902,2499,3191,4617,4711,6657,956,9005,9161,3463,7637,9687,8753,3662,2886,2746,9425,6087,5063,5861,1157,497,8957,4124,9520,146,1698,3000,5500,3045,5739,2541,6834,6519,197,6475,3045,4468,8486,1978,6690,8987,4705,6495,1850,223,8181,3290,8819,5106,4474,2430,9153,7321,2175,8512,5404,7075,6774,638,7285,6484,9400,8811,7094,2552,1467,1642,3877,1994,1792,828,8951,7146,3591,6752,4749,4484,321,355,5181,284,2394,3796,9634,3860,5381,1928,9038,7217,8071,8634,4006,9230,4423,3327,3117,7978,2181,4789,5548,3989,5495,7065,8309,556,5696,6631,1429,440,3466,9520,1365,7786,1192,9101,4586,5548,4783,8280,940,7769,3528,5850,3926,7450,6188,2769,1488,2250,7258,1194,8229,1795,8443,3330,3578,4198,5782,484,9310,9377,3062,9492,8828,2774,269,560,5662,8206,7634,4818,6078,2889,4024,5236,8486,8587,5563,3013,8713,7812,1688,8662,5162,4305,1035,2773,8091,6057,7879,2595,6452,2967,8147,1470,3154,256,9013,9303,3014,1478,4284,6968,3250,2226,6720,6955,1217,3437,6873,2404,5839,9103,4341,3257,5060,6648,4883,2209,117,9453,7970,839,8399,4084,7156,4404,1615,7446,5756,7058,3277,2046,4720,576,6070,9924,3064,9949,1025,3425,9209,5256,8029,6778,1666,4734,235,715,5591,7935,2136,5703,1296,5765,6782,7144,1730,3507,2789,217,1533,5388,4555,4988,1329,3011,6210,1528,6774,559,9560,8612,3523,8434,5601,3267,7853,6443,3677,1229,8881,9691,1432,6655,6192,3670,9049,3385,2869,1748,9890,3969,3392,5523,1142,4524,6014,7362,6087,8292,7762,4367,4785,291,9580,2922,4418,4840,3963,1182,1374,2888,5295,4633,1250,9222,6702,9287,9427,7119,1400,3469,4183,7797,6714,7525,4921,155,6765,6775,8088,6158,6031,5180,5665,876,8273,4742,4617,5762,4614,4925,62,9101,5228,4793,2488,1666,5672,8561,3847,135,7838,4656,6708,6887,7775,3815,7296,3897,1661,7733,7784,5985,6873,6398,9533,7991,8090,8371,4217,8841,9919,6193,820,7034,2621,8055,5318,6402,2435,1559,7961,3755,7315,4682,8293,4063,5,7478,4047,7149,6242,3653,6754,543,1796,1155,8985,6072,3267,4313,3290,4127,5406,7046,3123,2877,4471,8699,7816,4445,66,6838,971,456,9681,3528,9938,6490,1219,6754,605,6502,3234,9499,4746,73,1830,85,6905,8875,1865,6859,3889,1965,9931,7646,6871,5842,3627,3114,4871,2681,3105,9011,7576,8820,7282,2298,9665,3916,8762,2370,7534,1655,8470,9630,9110,1125,4313,1833,8727,4273,2360,100,1248,3256,6260,5856,6463,2777,3294,7515,7092,4271,1663,6368,8470,6857,4559,8913,6692,5632,3535,1781,3320,3843,6788,1662,3558,9880,3863,8896,4180,5441,9368,4387,9263,5445,6453,4739,9362,59,2411,8163,2035,4109,2048,7452,3560,2810,6059,5846,9846,1811,8448,5283,2878,1681,7476,6180,7869,1247,9332,8440,7982,8779,1341,2250,3794,658,2780,4227,4507,4592,1695,5603,5566,1926,1231,1372,4715,4140,8969,257,1834,666,3073,5854,7856,3326,6039,736,5872,8343,2915,4392,4704,2527,900,1018,9853,985,7791,1968,9508,5894,8154,2574,2387,7095,6310,2100,5670,1496,8660,2363,1269,6107,1020,7703,3529,7359,6653,3328,7294,8074,2846,3959,2407,8635,8182,4404,8156,3997,8424,8494,3052,9177,4240,8692,1380,8482,2182,9450,5257,1377,2430,4,5626,9972,2140,1643,5721,2975,3065,2514,8718,6970,531,1969,4189,3122,3858,1845,4779,4922,9626,4694,3904,4729,2446,4843,1270,594,3808,8359,5008,3013,6306,1493,4598,913,2263,4086,2349,9073,2643,7915,4974,4486,7471,6718,7813,3516,2369,849,5200,9383,2037,744,4180,1691,3326,7518,3706,521,8624,7969,2109,5015,831,5297,9353,2603,1204,7391,1294,4821,2695,4282,345,6157,6182,4588,3250,4844,3772,5886,2228,8679,3238,5969,7762,6781,4212,5407,4061,2333,4396,7424,5917,6593,2245,3181,7804,2363,2456,2499,6689,8306,2188,4194,4943,76,675,882,7665,3838,9586,2129,4693,2600,1075,4004,9426,1549,8005,1272,9104,4430,424,2957,9135,5091,2449,7967,6277,698,3909,2888,6218,3602,4985,2303,7810,9360,9783,1925,3437,5651,369,2065,8783,917,9431,8476,7246,6006,1847,9030,5422,4012,2843,6627,6173,560,735,8242,8705,5144,2635,6881,6655,9269,3959,997,1917,9690,6041,9331,5063,1289,3353,5876,179,8775,8372,305,273,4579,3486,580,7104,8944,2951,4656,9382,8502,7459,3550,7428,2827,5910,8227,4583,7882,7873,4130,9534,9315,2860,8348,3791,1391,4554,6814,3240,1563,3963,3143,7041,1975,49,2881,9193,6518,6833,8761,37,3558,1276,2023,7398,3433,3125,3498,7693,9554,7111,1131,9914,2753,4344,8209,1854,5275,589,8509,8390,8758,171,1067,7506,3208,8814,8627,4745,5838,4827,8702,7192,4927,9205,6283,1976,7563,8199,713,9999,2814,2815,6456,9555,2429,6087,8277,6843,817,3775,8594,7248,5342,9247,6966,5752,9871,1730,9590,7089,4654,6373,6154,3942,4354,6614,3041,1234,1846,2306,2215,6551,1402,133,4639,3912,6523,6872,5641,2761,2067,1577,341,1239,816,6240,8377,6596,618,3194,9363,778,7410,5375,9198,297,7277,5023,7103,7770,6606,750,6896,7479,95,7233,2387,7203,7631,808,6134,9287,3935,6208,880,3536,204,2592,5245,6981,5293,9980,6632,3382,1075,5171,3861,1713,2194,8625,7859,1980,651,2351,6753,1969,2469,8717,360,1902,994,6234,1605,5254,2323,3864,8483,7611,6996,6202,9733,9230,272,4672,9390,3994,1204,9803,9150,1061,9196,8807,9194,8533,4724,7584,7040,3412,7057,9978,3370,1963,9534,8377,8125,9865,2557,7613,4010,148,6086,6179,321,559,62,1702,2523,9218,8025,3048,6640,2054,5766,5709,5895,6169,4154,431,4842,7902,7395,7384,381,1363,999,9307,6122,9286,3151,5316,169,9135,382,1723,6604,8687,4263,1349,6849,8765,6727,6255,354,657,694,8183,9144,2215,2257,5406,2183,883,3982,699,4599,239,4305,7782,3424,6523,2611,4536,1846,8955,2749,5960,779,2535,4018,4432,1360,3486,2554,9979,4772,8322,527,690,5538,7971,8471,6423,637,2976,2569,4316,2592,8842,9549,6541,5663,5779,9804,7304,4147,9654,1696,5850,2829,7727,8945,3735,3046,3887,7348,2380,5341,7673,720,1710,3016,9758,2443,2493,8266,1549,2294,748,668,3513,8905,8120,9825,6928,5005,6238,9699,4139,3718,4082,5947,7889,1693,9469,1910,8609,5444,4069,1463,6821,6425,5613,7749,254,2819,7076,5399,3944,7516,649,2552,9972,8043,2896,3440,3055,9619,3452,9010,176,7029,2271,8428,6051,6320,1918,1008,9619,8468,7754,6971,6244,6050,5478,8717,9444,7692,5506,1325,179,8283,3608,7026,1986,2704,3136,7049,564,5608,4018,5423,2043,7018,3324,1554,704,6630,4285,1606,7321,1185,2531,7108,2560,2717,2132,9503,4934,8755,6715,2645,7145,5920,9961,5404,7506,650,4921,4638,7886,6909,7304,3398,304,6846,4464,9730,68,7185,748,5173,2967,4961,921,2451,4522,8533,8232,8506,2964,8569,9182,3745,4589,3554,5488,3434,3877,1671,6189,9944,9665,1774,2288,3783,2206,4702,7917,6003,7110,747,7575,2003,3764,8598,7581,7968,7171,6282,7260,2542,5404,8679,6051,3538,2673,5065,1631,9117,6833,5547,4300,5409,9614,1132,5068,4541,7779,5553,3312,2903,4112,5760,4230,4895,9660,2354,8675,6175,1606,8154,2294,456,8585,7689,2098,8934,227,6763,5097,9274,5183,4396,2188,8511,1339,7288,9250,1120,5815,3444,758,6648,2915,3351,8898,1401,5449,3238,8663,5155,7876,7354,5624,4121,8187,4808,9912,4944,8703,7237,4032,3448,2380,6677,3545,441,2175,7436,5727,3909,7665,5071,9269,8006,4400,9884,9454,1640,7186,8519,9990,7041,12,7283,768,8950,9204,2869,86,1807,1320,511,5629,9922,4246,6441,8930,9108,9840,3297,8563,3895,6859,2643,1492,9199,4263,631,1268,4263,2404,558,373,7745,1561,547,2051,9479,7841,4359,3764,8609,5106,756,6328,1040,9647,5796,800,2904,2810,9820,423,6762,1263,2183,1890,3237,6408,853,6829,9640,7424,6554,3627,2682,4722,1156,3528,5223,3465,9520,8382,5380,810,2724,3488,7234,5534,4673,751,2832,5493,7325,7714,9167,638,3175,3798,9554,7160,1845,3642,3789,5905,2756,1958,7918,5893,814,3356,1095,1441,2227,2348,9933,2201,5594,5871,2439,8096,3911,1723,9950,9830,8075,4011,4299,3116,4914,1581,5099,7260,784,5509,3515,2520,4115,7032,1360,7307,5753,4237,8722,7230,4162,4060,8567,7674,3338,6471,2092,497,5252,8026,3579,7301,9844,3061,9299,7679,2867,259,4698,580,9978,8436,7548,8541,5767,1427,8713,2392,8603,1443,8685,5343,969,6622,6454,6229,4909,2217,8696,4605,873,955,8110,5496,443,9101,2327,7574,5879,3300,173,239,2402,7789,4519,2714,5463,5893,8398,891,3074,6612,4908,1970,8333,5401,1415,6383,2117,9641,9549,6633,6866,8820,3037,6650,5279,5896,749,1796,6631,7210,8977,721,9679,153,5175,5248,3139,5217,3378,85,7545,7936,9658,4998,9115,5043,2603,1126,5905,7572,5654,1331,714,1455,5472,4192,9183,3045,9157,6627,9659,6232,8577,3699,689,3921,2410,5303,3227,137,6029,6412,224,7405,8346,3071,4401,6892,2557,8173,8913,1614,7000,3964,7567,1581,4636,5207,817,7908,9550,4168,148,3311,3172,5535,9383,945,6506,9011,383,1032,6560,4970,9331,710,8329,8457,7496,7885,8372,7674,5643,116,3827,3695,9748,2927,5304,7599,8331,1520,9545,931,9347,274,5807,951,2002,9166,1131,9545,8919,3448,6494,3705,8847,2530,5729,1930,4407,3586,3938,3028,1382,9190,56,8478,1973,6174,5792,2676,8639,1742,1436,3276,2727,7783,157,9239,2661,758,7468,6123,6661,9143,7251,1514,9867,9177,3154,2754,7310,1854,8604,9363,3921,9483,1786,6851,5320,2891,3656,6349,9835,4918,7153,6992,5761,5972,8910,3969,1541,573,7810,9495,4472,3200,6338,653,2860,3986,2771,5429,6362,8003,4669,8121,1263,5559,5223,7386,8708,511,7020,197,7917,9846,4240,1184,9474,946,8696,2642,3761,7018,4616,6627,514,7161,8154,1387,8634,7332,554,9625,9529,9956,4495,9642,5716,5953,4902,1449,1829,4082,3800,1472,8413,7632,2694,8943,6699,3484,9148,9355,1487,1582,7999,1990,6089,831,717,2812,185,9453,7405,5201,868,7093,190,8758,3747,9966,8190,9315,9955,929,7572,4808,4162,7505,1516,6960,8153,7888,2000,5372,6588,7512,2168,6029,9197,5183,7805,1288,123,233,3076,1574,4204,3795,4393,8120,5014,7881,4678,1683,1040,3088,6770,4639,2806,6355,1361,1260,5596,2394,253,4037,5655,2920,1381,6164,3608,4610,2645,5230,7016,1359,6268,475,5447,6771,3642,2298,7799,1115,2696,5322,8359,9398,6659,8435,9379,137,8897,7816,1703,1427,6302,5722,8601,1283,1783,5557,2365,9179,1792,8789,521,6141,5368,3569,3777,3592,6850,9074,4649,5730,5067,3760,230,6534,5048,6283,8081,1050,9665,7782,1148,5494,8631,8682,8569,8099,5604,5165,6905,3811,3777,3916,536,1128,3142,167,8635,1320,6679,5378,5551,5493,2687,1892,6334,3341,7984,3992,6433,4256,1807,8782,8955,3275,3659,1224,2221,8392,8031,4282,4432,7415,765,8594,1905,4823,9295,3558,1807,4171,5738,4883,3214,4467,4898,9403,8930,1664,5446,527,8685,8992,4557,6685,1608,7591,4725,3438,4724,5254,1699,5654,2669,9717,6288,3919,7842,9048,1366,5582,8887,6882,4024,2177,7301,6190,6014,9457,7521,3119,4491,8869,4389,9826,463,2063,6701,9877,8806,9667,2129,89,2657,5714,5874,1717,5030,9161,6922,5298,4974,2318,4028,5947,685,8510,9978,1172,6657,1877,6514,4236,3403,1012,3275,5795,7597,746,7908,8376,6961,7698,2536,4525,8886,3232,8270,1981,9027,3520,6504,5768,8709,9948,7644,4067,5731,9103,8486,569,8526,7565,9125,1744,5107,1459,1550,6243,511,5125,9469,917,3240,766,4526,3289,785,8432,7782,6644,9489,802,2590,4710,3454,1361,6154,8770,9354,7469,5333,4012,9693,2142,4782,9433,6228,5278,7812,1073,4884,4335,8705,4164,2731,6403,4812,4239,1578,5072,1229,4303,2073,8951,650,2336,4554,6348,1859,1775,9104,2838,170,7374,8261,2771,7541,9842,8829,3492,911,9707,4579,9505,5200,268,7027,5382,6993,5177,9765,267,4156,5787,7918,7980,6922,6794,6938,8958,5179,48,6757,5154,4674,8168,8647,1724,8585,8944,1613,9875,7286,8201,8970,8742,5881,4765,409,7099,512,2493,4024,4192,648,8566,7439,3010,3073,8760,3118,6934,4935,8430,9195,7486,8332,4594,9293,2426,1484,8543,6126,9525,7893,7455,7655,3374,3920,1575,8890,5876,8297,4054,8601,6792,6231,3957,9350,1475,2649,5260,6875,4619,8040,5635,101,3414,5258,2692,2249,2944,1535,6658,2731,2879,7314,8653,8425,4562,1392,4033,1555,5422,8576,7818,3145,3496,9193,769,1607,5300,8373,284,4752,5361,9607,1113,6097,3896,7921,8163,5807,9983,3665,264,981,4605,7901,1731,7659,9152,4433,3434,7880,1278,1970,9708,5457,7393,3126,7470,2100,6284,30,3628,4297,4264,3365,1554,4720,2922,4012,2663,9869,7552,5478,9968,5720,3136,9844,1575,9161,7755,9696,7040,7022,1202,1401,9597,3028,1808,668,574,8355,813,6837,1836,7498,2968,5595,2733,5321,8578,7003,7850,7904,5998,8117,4389,6675,2960,6054,9644,4915,4667,1164,9233,277,625,7264,5697,9677,6347,7772,7765,9705,5414,8793,7638,6191,2259,7098,496,8989,145,214,2825,9950,1767,337,805,8708,3560,7867,4038,6024,4262,3106,1837,9175,255,6217,6225,5409,491,6170,814,2624,6823,927,5249,1215,533,9187,2449,9743,8241,3622,7933,6092,4837,48,2547,8100,8268,5072,1069,2269,5956,4343,8408,5623,846,279,9157,2146,6546,4034,8536,9504,1028,9236,3881,8974,4354,263,5972,3093,5774,5065,5563,1595,4211,9132,8111,7441,9106,9412,2570,5519,801,4212,7760,3962,3772,287,8409,4729,8966,2153,5027,22,8226,8202,9790,3843,3753,2208,730,7306,6346,3783,8126,7364,4767,1550,1335,3137,2244,8931,550,7402,9446,586,1411,877,9416,2734,2790,8757,5634,9072,4333,9415,9240,3665,8109,7951,7351,9532,7524,6893,475,9083,3757,3570,5994,540,7578,4532,6500,995,7023,8250,7012,5961,4162,5925,4059,499,747,9294,204,2166,6443,7684,2448,1359,5083,1398,2969,4963,9792,9703,2400,1177,721,1751,8046,7792,9062,1886,9042,2604,7375,2838,2773,7005,7768,4926,5131,8351,6576,4510,1627,2045,9982,599,2308,4328,4680,5903,5631,3750,6115,9784,4397,2956,5214,5289,9951,914,5396,928,1330,5041,759,1506,6150,6745,9020,155,2846,2168,9320,4717,6465,8619,8236,4934,9089,4348,1570,5114,4728,9102,4250,5922,3644,4380,8869,6038,1074,3373,1333,1243,9564,6828,9775,6058,7178,2276,6972,561,7083,5118,4637,6871,6154,689,164,9695,3212,9483,3251,4163,2097,111,6677,951,4669,2816,4152,8582,3415,4426,5769,6582,5830,7473,8923,2378,1396,1835,5562,9290,2169,4690,6109,7989,1085,339,9470,7622,5455,2865,757,19,3579,2646,1459,7735,4785,7998,5574,6734,4050,1805,8000,2942,91,2746,625,535,5179,9213,5259,9203,6135,3358,1793,9749,5853,7057,1613,8252,6502,3682,9057,8507,2867,204,9539,2580,9898,3903,864,6434,8451,2509,5360,8682,8622,5886,5143,6740,158,6544,5065,3579,4277,6010,9290,521,2338,2521,2832,5162,4708,120,7382,5598,826,9700,1302,5917,3233,7830,5499,2036,1833,3621,2662,1774,9014,3305,2622,4264,5669,3731,6819,7773,4986,3709,2615,5231,3328,431,7433,1285,7277,7168,2703,9604,2241,8278,2921,8319,5596,8475,9797,5251,3061,6316,6369,2851,6898,9452,3310,8517,7866,1701,7816,19,4637,2378,6917,9533,8268,4257,1968,663,3528,4770,9005,1700,6509,5559,2559,1455,1473,5655,2194,8305,9296,2253,5886,1619,2445,9531,6815,7875,7336,3497,4496,328,3204,3474,7419,8989,5263,7124,3914,8530,3303,3594,7952,9868,1162,4167,6891,7740,2600,3767,2019,5566,8788,1868,9540,9657,5188,2142,2570,3481,8057,210,5433,6537,1671,6160,3855,3856,4183,5021,8265,5926,3758,602,26,2288,7611,9118,6969,3571,3578,3039,7602,7468,4693,4463,7982,5381,4914,6179,852,6993,7606,7935,5229,9195,8912,9630,4853,2345,4059,8323,5383,2837,7713,5586,3366,7071,7385,4511,6699,6176,6068,6376,2994,6483,4566,2788,6874,2275,1928,9368,3885,7875,4338,8543,8017,3057,4637,688,4599,7822,3792,1615,1348,3710,5971,9358,6463,6269,6535,1962,2260,5973,8073,4657,620,4186,2274,4633,2428,5665,6415,556,410,1,2340,4106,4335,3272,7533,2385,5720,7117,7155,8542,9260,808,1920,8215,565,4372,7149,229,5745,2029,9146,6351,5124,6206,9515,1354,6444,6425,9630,315,8548,6464,1607,6043,8325,8377,3674,1407,2967,4075,1198,363,5624,1884,3372,4955,4444,5934,5432,3209,4693,6062,3404,3228,2048,4623,3981,8046,8380,2731,87,3215,2930,7611,8343,6269,5106,7660,109,4292,3098,9543,6055,1075,5013,179,9883,1642,9360,1813,6417,1271,6802,2080,4267,416,2872,56,7597,8720,3702,7860,8513,3364,4111,5186,249,2184,6810,5801,5668,2511,2893,1661,7026,1672,9364,4814,7729,9221,9305,6623,5217,7781,2552,9290,4230,2997,9273,1033,3836,7073,3793,8635,250,458,1365,4817,8557,2802,2165,2861,7277,4832,4615,3896,5865,5409,2705,6222,2766,2593,5103,1064,1045,9283,3090,2894,9708,1887,3088,1090,6055,7061,2166,5299,1193,8089,3959,9822,9722,9525,3959,6526,4253,9522,2328,7352,3704,4408,8335,2205,8621,8022,6134,2344,4108,6334,3239,7921,4496,9838,7539,3658,7553,7526,4228,1806,4576,8939,4230,5327,9492,4852,7280,1417,7548,2418,1411,6749,491,6326,2356,5476,7256,1813,6084,2153,9568,7191,6858,8425,5528,7782,1757,9006,7137,620,990,3041,2615,4995,4656,3426,920,493,7387,8266,2829,2748,4494,1935,1687,721,6781,4998,7592,5026,540,5716,6557,6106,7033,4347,6357,4881,4892,6036,5101,3225,8973,3379,1197,4446,7647,5759,5767,5156,5419,7222,4720,3158,1629,4750,7264,6631,1938,6717,4702,8282,7180,941,711,5950,9856,7288,1415,3900,3549,1320,4217,2159,2679,1641,7503,5496,4672,7621,5863,3381,8598,9396,6616,6313,8760,9670,1813,841,9339,2065,6082,1851,1665,5402,9276,8902,7609,9440,7053,4357,3514,4159,1664,7207,4954,1519,9142,5170,1688,6216,9058,4684,7035,63,2708,953,5585,2178,4334,245,9083,5300,8532,3826,8181,3735,426,9084,282,4494,1396,9108,3988,3075,9443,3177,7230,9369,8155,1276,2154,8374,3508,599,6987,453,1053,2277,7117,8107,5243,5590,9507,9888,7648,3251,5003,7406,3271,3797,5194,3296,6474,3795,7957,6014,8368,7346,4344,6642,9272,1032,9735,1633,715,3464,5518,8984,4378,689,1245,738,6397,5924,515,7562,5205,7029,9611,488,9308,5649,4641,3699,8835,1012,4719,2146,782,9307,718,6665,1563,7092,8649,5779,4052,3080,7290,3554,1965,8421,9871,4472,3132,3017,4125,7546,6124,8179,2144,8263,9723,6298,6122,3739,7087,1987,8795,6459,4758,4923,3063,8098,9925,8019,3798,4671,5202,4187,2384,3553,1698,1250,9644,4132,1228,1645,5753,6375,4637,54,3341,5045,5773,305,7078,6582,4036,1846,4034,9912,8416,4431,7726,1617,7763,2386,5510,520,4036,1269,2813,6731,7670,4893,6541,6492,5888,8641,252,3304,1421,8409,9212,8423,4287,9884,1962,2760,7737,5620,19,5687,3613,6907,4506,5669,8753,6941,8859,357,5258,9160,2627,4016,1385,5359,1478,1652,5354,5733,3133,3879,1498,2803,8079,3245,9581,2529,3912,7230,6757,4282,1010,1774,7059,4995,9298,5229,3074,5593,2293,3634,5731,7435,1602,4163,9181,7493,185,6967,2794,9857,9208,2702,3353,694,4577,1484,8818,6021,2554,1959,2804,3514,2893,1063,7777,2342,1697,5536,726,9602,8642,297,1689,8340,5153,1380,2587,7442,1709,7188,3332,8719,3208,4582,8575,5027,4103,3221,3852,3573,3529,6691,676,8268,8821,4468,6907,5025,5363,5498,7928,5332,5105,3252,3960,5677,4994,8302,2855,9736,2405,884,9994,7061,4806,6597,8971,9831,7866,357,4712,2315,7039,9717,9771,8239,2990,5717,3226,7770,6684,1874,5007,4484,540,3938,7783,9139,4017,5344,3875,937,7320,3696,6377,7906,3973,6666,9120,8016,2934,5200,155,1321,1630,7685,609,5099,3607,315,3231,611,2325,4336,6703,1328,802,9054,2837,8722,6872,3757,7656,9960,3205,6489,6076,4163,7821,9511,4277,219,6289,4360,4046,5976,7940,1632,1419,61,4370,4288,5570,1443,9098,8311,779,4152,5417,4252,1223,6863,5932,9341,4114,1762,289,9268,2253,2110,1984,2734,4629,517,9929,7761,3337,6885,1361,7246,2548,1746,9538,8901,1720,8374,5026,6918,3663,8768,8733,434,3251,812,3910,786,4192,211,1473,5563,5918,7010,738,4913,5877,2189,8223,2992,3890,8069,947,6208,5403,7904,5862,308,2021,8570,7918,1022,4678,1719,6061,4691,7374,6348,7709,3528,3483,696,1032,1423,2530,7349,2050,6340,9736,4159,2667,9699,1269,925,8051,3417,4215,6817,4372,6698,7983,4126,5133,8728,1269,65,34,3241,9131,6401,8982,4261,5149,9217,6415,241,1305,4780,5188,2983,6201,1347,8797,4269,1625,9122,667,4731,3634,5730,5189,2686,7781,3241,8388,6775,9671,7134,1754,6289,399,7797,2588,9048,443,2722,2615,798,9770,3975,3365,779,740,571,4985,8963,5079,6017,3343,3668,5345,3363,5358,7277,8417,8303,5959,7194,9182,7697,2186,4308,3301,6037,3387,6665,4175,3297,461,5902,4074,5120,5532,6047,6601,6554,853,6898,6997,1269,2896,906,4109,2546,5585,564,8479,3538,2261,8360,3333,7003,5932,2577,1402,3862,4791,5556,8692,6987,6401,9936,7588,7568,4119,9138,3065,6284,9528,1796,2197,3881,6580,8405,9219,1782,5964,427,6123,1769,3536,6269,6278,1022,2008,9255,9920,2439,3773,7178,9499,6917,7872,691,1306,6446,8980,4299,9662,1225,2129,8801,1003,5360,8364,2798,8951,154,6307,7578,8900,2294,5640,9229,9163,8606,5783,7202,2048,2196,561,1779,9018,7330,1281,8229,9098,6418,4768,5573,3286,1255,7495,744,7978,4284,1609,4650,27,2379,3870,3949,9085,7208,9107,1079,5940,8067,4122,7983,4473,1912,4681,426,1944,5512,2944,7407,4263,8456,7008,8555,9198,7589,8442,2459,4322,5791,6600,2411,7021,2315,3537,3293,7383,2958,2515,9802,7785,9100,7414,9938,2795,865,8822,6779,3170,7374,3033,6148,1930,1529,8697,9729,2730,9218,711,502,9145,1679,8686,9145,1358,5498,8788,974,4731,1639,8957,8339,5354,8492,5873,8783,9500,759,6227,3634,6951,7984,9775,90,718,4626,3882,9117,705,7480,9865,8733,5066,7270,4068,6132,8291,1354,9670,2063,504,5619,811,8866,4313,8255,935,1345,9350,9178,6613,1069,7369,4224,8112,9823,9491,9976,9448,5372,6377,1457,8482,327,9269,5308,4996,9862,9060,6874,8811,2626,6205,8931,7104,7047,4181,717,1870,7423,9179,3517,7040,3161,9606,3669,1808,3849,8850,5804,3263,2211,8455,6029,1453,3316,6642,4794,4737,5357,2019,3031,8578,5274,7759,4597,8832,548,9588,6349,7930,2025,2415,3932,783,7603,564,173,7886,1161,5682,282,7041,3930,9471,950,2699,1043,5289,1537,2325,4168,1655,1229,2589,8488,7971,1323,5692,2504,2324,6948,3347,5268,8268,3022,1332,1419,607,9063,4962,1095,211,5786,4723,716,510,5302,8151,986,3193,8906,9298,5312,8346,4910,3193,5427,8371,1150,9047,7766,4936,3937,8022,8548,3022,2226,8542,6370,4578,4485,475,6870,9496,9930,8806,1596,731,3538,5151,4995,8945,2859,6218,2174,8829,4775,4874,8996,4681,5909,9630,1312,9865,7647,3560,2233,2061,2702,5204,7937,1775,7731,4747,9435,8300,4247,7400,9137,2710,4383,273,8306,7065,8599,4944,2984,1274,2795,5992,4014,6375,3105,7821,4709,7171,153,7548,2267,7089,9785,6527,8219,9086,33,5453,2737,9049,5034,5145,3584,7147,7361,8642,4626,7809,9023,8846,6433,8633,6728,1003,6272,4165,7118,9270,10,5890,472,8041,2751,8334,886,9826,990,5371,5897,9786,9158,4822,959,4007,7818,9637,3238,3657,7986,8152,7493,3995,9742,2868,4963,8503,5441,2153,6512,7358,6899,564,9659,1259,4692,4059,5328,8681,5241,1304,3168,5276,29,4269,6895,1093,5498,8093,8070,2721,3182,5586,4946,5194,9115,8498,8678,7107,5722,3679,6484,6404,6831,5325,7914,7312,2532,5179,9020,4819,7293,8239,3622,3311,3641,8774,6016,5584,9433,1111,3998,9696,1973,9695,3730,2055,8113,9614,8229,2776,3140,9582,7934,4047,1784,5711,5755,1420,5105,4956,484,7946,6463,5043,3585,3174,5115,3519,5597,3450,9231,5772,1701,8108,8142,9365,337,9584,2742,5931,8864,5375,6576,3719,5366,3877,3208,9076,4922,3798,5482,307,5058,732,9680,3010,1831,9309,8684,9052,8857,1775,5,4398,5749,2310,8836,7172,2741,8124,2320,302,2464,5793,9807,6456,6387,5332,4800,3849,6817,9354,6668,4977,744,6607,3294,6815,3603,2340,885,5990,7882,7992,5327,1749,8214,9045,1986,3903,312,6754,3121,2659,9489,7887,8440,6656,2651,6237,272,5258,3437,6579,1721,7383,31,1856,4118,1700,2034,3543,912,2043,2287,9310,4892,7746,1419,3479,1920,2913,4478,1818} };
            foreach (var input in inputArr)
            {
                input.Print("Ropes");
                Console.WriteLine($" Minimum Cost to Connect above Sticks: \t\'{DailyProblem.MinimumCostToConnectStick(input)}\'");
            }
        }

        public static void MinCostToMergeStones()
        {
            // https://leetcode.com/problems/minimum-cost-to-merge-stones/
            Utility.Print("1000. Minimum Cost to Merge Stones");
            int[][] stones = { new int[] { 3, 2, 4, 1 }, new int[] { 3, 2, 4, 1 }, new int[] { 1 }, new int[] { 6, 4, 4, 6 }, new int[] { 3, 5, 1, 2, 6 }, new int[] { 1, 4, 3, 3, 2 }, new int[] { 1, 4, 3, 3, 2 }, new int[] { 1, 4, 3, 3, 2 } };
            int[] k = { 2, 3, 2, 2, 3, 3, 4, 5 };
            for (int i = 0; i < stones.Length; i++)
            {
                stones[i].Print("Stones Array");
                Console.WriteLine($" with Merging K={k[i]} at a time, \tMinimum cost to merge all piles of stones into one pile: {DailyProblem.MinimumCostToMergeStones(stones[i], k[i])}\n");
            }
        }

        public static void StringToInteger()
        {
            // https://leetcode.com/problems/string-to-integer-atoi/
            Utility.Print("8. String to Integer (atoi)");
            string[] inputArr = { "42", "-42", "4193 with words", "words and 987", "-91283472332", "91283472332", "+1", "2147483648", "2147483647", "-2147483648", "-2147483649" };
            foreach (var str in inputArr)
                Console.WriteLine($" String to Integer conversion for \t\'{str}\' \tyielded \t\'{DailyProblem.StringToInteger(str)}\'");
        }

        public static void ReverseNum()
        {
            // https://leetcode.com/problems/reverse-integer/
            Utility.Print("7. Reverse Integer");
            int[] numsArr = { 123, -123, 0, -321, int.MaxValue, int.MinValue, 1534236469, -2147483412 };
            foreach (var num in numsArr)
                Console.WriteLine($" Reverse of Num:\t{num} \t{DailyProblem.ReverseInt(num)}");
        }

        public static void SearchA2DMatrixII()
        {
            // https://leetcode.com/problems/search-a-2d-matrix-ii/
            Utility.Print("240. Search a 2D Matrix II");
            int[][,] matrixArr = {  new int[,]{ { 1, 4, 7, 11, 15 },
                                                { 2, 5, 8, 12, 19 },
                                                { 3, 6, 9, 16, 22 },
                                                { 10, 13, 14, 17, 24 },
                                                { 18, 19, 23, 26, 30 } },
                                    new int[,]{ { 1 , 1 } }
                                 };
            int[] targetArr = { 5, 20, 19, 2 };
            foreach (var matrix in matrixArr)
            {
                matrix.Print();
                foreach (var target in targetArr)
                    Console.WriteLine($" Search for \'{target}\' in above 2D matrix resulted in: \t{DailyProblem.SearchNumberIn2DMatrix(matrix, target)}");
                //Console.WriteLine($" Search for \'{target}\' in above 2D matrix resulted in: \t{DailyProblem.SearchA2DMatrixIIBinarySearch(matrix, target)}");
            }
        }

        public static void CloneGraph()
        {
            // https://leetcode.com/problems/clone-graph/
            Utility.Print("133. Clone Graph");
            ListNode[] nodesArr = new ListNode[4];
            for (int i = 0; i < nodesArr.Length; i++)
                nodesArr[i] = new ListNode(i + 1);

            // Node 1
            nodesArr[0].neighbors.Add(nodesArr[1]);
            nodesArr[0].neighbors.Add(nodesArr[3]);
            // Node 2
            nodesArr[1].neighbors.Add(nodesArr[0]);
            nodesArr[1].neighbors.Add(nodesArr[2]);
            // Node 3
            nodesArr[2].neighbors.Add(nodesArr[1]);
            nodesArr[2].neighbors.Add(nodesArr[3]);
            // Node 4
            nodesArr[3].neighbors.Add(nodesArr[0]);
            nodesArr[3].neighbors.Add(nodesArr[2]);

            var clonedGraph = DailyProblem.CloneGraph(nodesArr[0]);
        }

        public static void LetterCombinations()
        {
            // https://leetcode.com/problems/letter-combinations-of-a-phone-number/
            Utility.Print("17. Letter Combinations of a Phone Number");
            string[] inputArr = { "23", "", "2", "789" };
            foreach (var digits in inputArr)
            {
                var letterCombos = DailyProblem.LetterCombinationsOfPhoneNo(digits);
                Console.Write($" For given input digits: \'{digits}\' possible letter combination is are: ");
                foreach (var combo in letterCombos)
                    Console.Write($" {combo} ||");
                Console.WriteLine();
            }
        }

        public static void SerializeAndDeserializeBST()
        {
            // https://leetcode.com/problems/serialize-and-deserialize-bst/
            Utility.Print("449. Serialize and Deserialize BST");
            TreeNode root = new TreeNode(2) { left = new TreeNode(1), right = new TreeNode(3) };
            var serialzedDeserialzedRoot = DailyProblem.DeSerialize(DailyProblem.Serialize(root));
        }


        public static void MinimumHeightTrees()
        {
            // https://leetcode.com/problems/minimum-height-trees/
            Utility.Print("310. Minimum Height Trees");
            int[][][] edges = new int[][][] {
                                                new int[][] { new int[] { 1, 0 }, new int[] { 1, 2 }, new int[] { 1, 3 } },
                                                new int[][] { },
                                                new int[][] { new int[] { 0, 1} },
                                                new int[][] { new int[] { 3, 0 }, new int[] { 3, 1 }, new int[] { 3, 2 }, new int[] { 3, 4 }, new int[] { 5, 4 } },
                                                new int[][] { new int[] { 0, 1 }, new int[] { 0, 2 }, new int[] { 0, 3 }, new int[] { 3, 4 }, new int[] { 4, 5 } } };
            int[] vertexCount = { 4, 1, 2, 6, 6 };
            for (int i = 0; i < edges.Length; i++)
            {
                Console.WriteLine($" No of Vertices: {vertexCount[i]}");
                edges[i].Print("edges");
                foreach (var minVertex in DailyProblem.FindMinHeightTrees(edges[i], vertexCount[i]))
                    Console.WriteLine($" Minimum height trees (MHTs) start from Vertex: \t{minVertex}");
                Console.WriteLine(Utility.lineDelimeter);
            }
        }


        public static void ArrayFormationThroughConcatenation()
        {
            // https://leetcode.com/problems/check-array-formation-through-concatenation/
            Utility.Print("1640. Check Array Formation Through Concatenation");
            int[][] arr = { new int[] { 91, 4, 64, 78 }, new int[] { 1, 2, 3 }, new int[] { 15, 88 }, new int[] { 49, 18, 16 } };
            int[][][] pieces = { new int[][] { new int[] { 78 }, new int[] { 4, 64 }, new int[] { 91 } },
                                 new int[][] { new int[] { 2 } , new int[] { 1, 3 } },
                                 new int[][] { new int[] { 88 }, new int[] { 15 } },
                                 new int[][] { new int[] { 16, 18, 49 } } };
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i].Print("Arr");
                pieces[i].Print("Pieces");
                Console.WriteLine($" 'Arr' can be formed using 'Pieces' : {DailyProblem.ArrayFormationThroughConcatenation(arr[i], pieces[i])}\n\n");
            }
        }


        public static void MinCostToMoveChips()
        {
            // https://leetcode.com/problems/minimum-cost-to-move-chips-to-the-same-position/
            Utility.Print("1217. Minimum Cost to Move Chips to The Same Position");
            int[][] positionsArr = { new int[] { }, new int[] { 1, 2, 3 }, new int[] { 2, 2, 2, 3, 3 }, new int[] { 1, 1000000 } };
            foreach (var positions in positionsArr)
            {
                positions.Print("CHIPS POSITION");
                Console.WriteLine($" Minimum cost to move all the above chips at one location is: \t{DailyProblem.MinCostToMoveChips(positions)}");
            }
        }


        public static void FindMinimumInRotatedSortedArrayII()
        {
            // https://leetcode.com/problems/find-minimum-in-rotated-sorted-array-ii/
            Utility.Print("154. Find Minimum in Rotated Sorted Array II");
            int[][] inputArr = { new int[] { 1, 3, 5 }, new int[] { 2, 2, 2, 0, 1 }, new int[] { 3, 3, 3, 1 }, new int[] { 10, 1, 10, 10, 10 }, new int[] { 2, 2, 2, 0, 1 } };
            foreach (var input in inputArr)
            {
                input.Print("Input");
                Console.WriteLine($" Min element: \t{DailyProblem.MinInRotatedSortedArrayWithDuplicates(input)}");
            }
        }


        public static void FindTheSmallestDivisorGivenAThreshold()
        {
            // https://leetcode.com/problems/find-the-smallest-divisor-given-a-threshold/
            Utility.Print("1283. Find the Smallest Divisor Given a Threshold");
            int[][] nums = { new int[] { 1, 2, 5, 9 }, new int[] { 2, 3, 5, 7, 11 }, new int[] { 19 }, new int[] { 134472, 307981, 242985, 654527, 955840, 632108, 900623, 995266, 71687, 306036, 945300, 951599, 542263, 977635, 347623, 856289, 681188, 601083, 470509, 454993, 152343, 409659, 121954, 341572, 633728, 48156, 212838, 385665, 321423, 38135, 37059, 972113, 537032, 193415, 734084, 563311, 109963, 729639, 16828, 959255, 551051, 47289, 924481, 828064, 549189, 747075, 386591, 10841, 630268, 734691, 963174, 258084, 166115, 629547, 662751, 969570, 783137, 331024, 319628, 441024, 436095, 322214, 870665, 445763, 125435, 625422, 398538, 846541, 2767, 718091, 395597, 290490, 1864, 145771, 638824, 179978, 173447, 357817, 337197, 266159, 583993, 165217, 342895, 271023, 318003, 7634, 222230, 436757, 460750, 542548, 574199, 817228, 651464, 672996, 67700, 362803, 19883, 570574, 575006, 680092, 28748, 307955, 931296, 341184, 992413, 791765, 604730, 155146, 418775, 608374, 109409, 138709, 509788, 38335, 497370, 540407, 755923, 278168, 893004, 55810, 222614, 282112, 605758, 529279, 718275, 632851, 888711, 497778, 913296, 726288, 987648, 500613, 516526, 406455, 784126, 46579, 537308, 558812, 89840, 208386, 248815, 838528, 921368, 762521, 11967, 79616, 307149, 432412, 798434, 200445, 814259, 201349, 936125, 919867, 929989, 485058, 619979, 633318, 730022, 353482, 382983, 738574, 772800, 487985, 659100, 445751, 570792, 945534, 705044, 10414, 353202, 52463, 193486, 825463, 192685, 969540, 408152, 215082, 542662, 761437, 216823, 762929, 821457, 567562, 662945, 362536, 294434, 110478, 912681, 68679, 998119, 549239, 201738, 986914, 406166, 666781, 394718, 334482, 890905, 339751, 181524, 299217, 416690, 930527, 827758, 378730, 73229, 312722, 370365, 939016, 808925, 440368, 479266, 996297, 950225, 183128, 478589, 548788, 331373, 264191, 672510, 445999, 594301, 819354, 620736, 41902, 233523, 560606, 903405, 563466, 418884, 83750, 996775, 963151, 643403, 987821, 672083, 194779, 55970, 555370, 984174, 788128, 874860, 284248, 810887, 110646, 578012, 971076, 474725, 86569, 104468, 946560, 683195, 156997, 671716, 562390, 510984, 815384, 925945, 743175, 685825, 716987, 803732, 870556, 772537, 466975, 946504, 220371, 67540, 242117, 771063, 492412, 611448, 101746, 30166, 79470, 834708, 268302, 174470, 149896, 762374, 875815, 48905, 620046, 176431, 452436, 751720, 456730, 979932, 561757, 680074, 79729, 169893, 466702, 574842, 688335, 435694, 384587, 301692, 120436, 518912, 411347, 475150, 2640, 742324, 798254, 822284, 152198, 621904, 776432, 796775, 239528, 819968, 881274, 633928, 989640, 236660, 326129, 682822, 659925, 483160, 55369, 457226, 45249, 644523, 869022, 167620, 71356, 201558, 676785, 703608, 550696, 702033, 865541, 539243, 870517, 395227, 84436, 438100, 796766, 972696, 519608, 382813, 571887, 688386, 223306, 301816, 821780, 777411, 765277, 272419, 800853, 4208, 906420, 947538, 410771, 163078, 105943, 817680, 115936, 708274, 508540, 382246, 21739, 86255, 676263, 109717, 550933, 663110, 193232, 919345, 655967, 758328, 740471, 407347, 72235, 148172, 505626, 322341, 735162, 650249, 357766, 742146, 33507, 738905, 247245, 932060, 133977, 562316, 757980, 721170, 544685, 872710, 628670, 457160, 358512, 28558, 725066, 364701, 309362, 64622, 294956, 920591, 314166, 815528, 631975, 571492, 751384, 111077, 467767, 781689, 273204, 865387, 97458, 832026, 219464, 904123, 541374, 148304, 548357, 895786, 532669, 453370, 959041, 229659, 376901, 250207, 689368, 605213, 620423, 214642, 419509, 584149, 123078, 725735, 750389, 304505, 981694, 934980, 136203, 737831, 725390, 861238, 154688, 749184, 925874, 387351, 253201, 685922, 487567, 643113, 98999, 71470, 901722, 775960, 480272, 538910, 555533, 369369, 488342, 629226, 190032, 106148, 336773, 26678, 475164, 842651, 19449, 977964, 724875, 506251, 486835, 761189, 484653, 180528, 654045, 989521, 265748, 236394, 652147, 85399, 53933, 465278, 569142, 396418, 135358, 85179, 902235, 650124, 364135, 960721, 979319, 860603, 586588, 898695, 537825, 895012, 315813, 316977, 382459, 95467, 463669, 540337, 273546, 625782, 207710, 985258, 998269, 945134, 469699, 933318, 112153, 295153, 489388, 874322, 224074, 946016, 825566, 748680, 655467, 636987, 281191, 769617, 50027, 528654, 992458, 132632, 92107, 630027, 226864, 575166, 858583, 203845, 726829, 633208, 296100, 450329, 453442, 447093, 473478, 892533, 585383, 747946, 318870, 162037, 944215, 473316, 808876, 190875, 394382, 617394, 741139, 241818, 98858, 830531, 572716, 628852, 56405, 990448, 282760, 534150, 733344, 561869, 369168, 333701, 436531, 389817, 329149, 385697, 326463, 209557, 124993, 369847, 982921, 597854, 925787, 778277, 459074, 133485, 502499, 208513, 223305, 302335, 268028, 670031, 584615, 331555, 494069, 280036, 324408, 68831, 919795, 570589, 548050, 414759, 382616, 84621, 482585, 121889, 428920, 896957, 395333, 55131, 539032, 562015, 924779, 581403, 880956, 637636, 872078, 846731, 218998, 731569, 644724, 574310, 508269, 301586, 476942, 776610, 854594, 982573, 81711, 606400, 742198, 897808, 862771, 380362, 40492, 162574, 728333, 281863, 730233, 532922, 657864, 419865, 211877, 843702, 39772, 404211, 928444, 731115, 473103, 693958, 196635, 834781, 717411, 489758, 502486, 29865, 603137, 343160, 792651, 798091, 295741, 193946, 225600, 606646, 993419, 241033, 990778, 763511, 922148, 308352, 683374, 266189, 757221, 662474, 30760, 747468, 652753, 290334, 115189, 503762, 301420, 177102, 580334, 145709, 999285, 923699, 210094, 896214, 109471, 143009, 691693, 297398, 229522, 449123, 104560, 836820, 971261, 111949, 338851, 141552, 573104, 647434, 752900, 650652, 168921, 620378, 87446, 442390, 730746, 932995, 997330, 29661, 888742, 191241, 828209, 777024, 614095, 502524, 37724, 691232, 559946, 567849, 132455, 309676, 548090, 459443, 918506, 401508, 994157, 776244, 13417, 521004, 122222, 985859, 319619, 738857, 564459, 405414, 775287, 694044, 244736, 406078, 818311, 348432, 937552, 600193, 645157, 802930, 771498, 590580, 501416, 248129, 365576, 194606, 37648, 318760, 13829, 320128, 126363, 849193, 215572, 230844, 253811, 990163, 473607, 689942, 835644, 532764, 871698, 262053, 843228, 731711, 111421, 743445, 841872, 816418, 426255, 354822, 944584, 11824, 774713, 104566, 762015, 282919, 701515, 135076, 198847, 722680, 560387, 198328, 824607, 535277, 206250, 286872, 407716, 198271, 865760, 469408, 804176, 41806, 874432, 799782, 142319, 676811, 499216, 701724, 998397, 263197, 853587, 692086, 342638, 158061, 694699, 251590, 726172, 51129, 493640, 237122, 20157, 616242, 57227, 412186, 429027, 130896, 138074, 303301, 527404, 580456, 203970, 512651, 411869, 28154, 999952, 896455, 889742, 165930, 684721, 994802, 860946, 252372, 38841, 17483, 507403, 295610, 912363, 172949, 839215, 796236, 152975, 240597, 896623, 343417, 691520, 389632, 92690, 189531, 535092, 959883, 771366, 501918, 20453, 706260, 819764, 964564, 985651, 380794, 321432, 294437, 791942, 622572, 719699, 435357, 222029, 457114, 686311, 609426, 364004, 772975, 88884, 63157, 644748, 908860, 560433, 281933, 20996, 934242, 754877, 688979, 159822, 150193, 381164, 651041, 402777, 222858, 769834, 931856, 9365, 145548, 968321, 284084, 84078, 605524, 260859, 107018, 829292, 311838, 368321, 245985, 933097, 435793, 657263, 642995, 253214, 71633, 168489, 15967, 645506, 161991, 816576, 924379, 48762, 848025, 576629, 852660, 23936, 183779, 241918, 954927, 662009, 412152, 319837, 268575, 582917, 763437, 209788, 859907, 303874, 925949, 357733, 629414, 918645, 514552, 559518, 582019, 588629, 210041, 591849, 859076, 468522, 25117, 812748, 184826, 927208, 51400, 709139, 595904, 41404, 882126, 117341, 257366, 378775, 295124, 178634, 94768, 32627, 934854, 907828, 708518, 299911, 236366, 36304, 202981, 243118, 301959, 380714, 582472, 64461, 155403, 520759, 175262, 477980, 416682, 92338, 106653, 118208, 685583, 257168, 480285, 26008, 689729, 558302, 934027, 717797, 381600, 738554, 625538, 725551, 140518, 333753, 286580, 729336, 592888, 289055, 958138, 528630, 853015, 370725 } };
            int[] threshold = { 6, 11, 5, 2029 };
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i].Print("Nums");
                Console.WriteLine($" Smallest Divisor for Threshold \'{threshold[i]}\' for above arr is: \t{DailyProblem.SmallestDivisor(nums[i], threshold[i])}");
            }
        }


        public static void ConstructTreeFromPreAndInOrderTraversal()
        {
            // https://leetcode.com/problems/construct-binary-tree-from-preorder-and-inorder-traversal/
            Utility.Print("105. Construct Binary Tree from Preorder and Inorder Traversal");
            int[] preorder = { 3, 9, 20, 15, 7 };
            int[] inorder = { 9, 3, 15, 20, 7 };
            preorder.Print("PreOrder");
            inorder.Print("InOrder");
            var root = DailyProblem.BuildTree(preorder, inorder);
        }


        public static void DayOfTheWeek()
        {
            // https://leetcode.com/problems/day-of-the-week/
            Utility.Print("1185. Day of the Week");
            int[] day = { 28, 7, 31, 18, 15, 31, DateTime.Now.Day }, month = { 12, 11, 8, 7, 8, 8, DateTime.Now.Month }, year = { 1971, 2020, 2019, 1999, 1993, 2100, DateTime.Now.Year };
            for (int i = 0; i < day.Length; i++)
                Console.WriteLine($" Day{day[i]}\tMonth{month[i]}\tYear{year[i]}\tis \t\'{DailyProblem.DayOfTheWeek(day[i], month[i], year[i])}\'");
        }


        public static void CanPlaceFlowers()
        {
            // https://leetcode.com/problems/can-place-flowers/
            Utility.Print("605. Can Place Flowers");
            int[][] flowerbed = { new int[] { 1, 0, 0, 0, 1 }, new int[] { 1, 0, 0, 0, 1 }, new int[] { 1 }, new int[] { 0, 0, 0, 0, 0 } };
            int[] n = { 1, 2, 0, 4 };
            for (int i = 0; i < flowerbed.Length; i++)
            {
                flowerbed[i].Print("FLowerBed");
                Console.WriteLine($" Placing \'{n[i]}\' flowers in above flower bed is possible: {DailyProblem.CanPlaceFlowers(flowerbed[i], n[i])}");
            }
        }


        public static void TwoSumLessThanK()
        {
            // https://leetcode.com/problems/two-sum-less-than-k/
            Utility.Print("1099. Two Sum Less Than K");
            int[][] A = { new int[] { 34, 23, 1, 24, 75, 33, 54, 8 }, new int[] { 10, 20, 30 } };
            int[] K = { 60, 15 };
            for (int i = 0; i < A.Length; i++)
            {
                A[i].Print("Input");
                Console.WriteLine($" For K= {K[i]} Two Sum Less Than K such that there exists i < j with A[i] + A[j] = S and S < K is: {DailyProblem.TwoSumLessThanK(A[i], K[i])}");
            }
        }


        public static void TwoSumInBST()
        {
            // https://leetcode.com/problems/two-sum-iv-input-is-a-bst/
            Utility.Print("653. Two Sum IV - Input is a BST");
            TreeNode root = new TreeNode(5)
            {
                left = new TreeNode(3)
                {
                    left = new TreeNode(2),
                    right = new TreeNode(4)
                },
                right = new TreeNode(6)
                {
                    right = new TreeNode(7)
                }
            };
            root.InOrder();
            int target = 9;

            // 2 Pass approach 
            // Console.WriteLine($" In above BST, Two Node with sum equal to Target {target} exist: {DailyProblem.FindTargetInBST(root, target)} ");
            // 1 Pass approach
            Console.WriteLine($" In above BST, Two Node with sum equal to Target {target} exist: {DailyProblem.FindTargetInBSTFaster(root, target)} ");
        }


        public static void TwoSumInBSTs()
        {
            // https://leetcode.com/problems/two-sum-bsts/
            Utility.Print("1214. Two Sum BSTs");
            TreeNode root1 = new TreeNode(2)
            {
                left = new TreeNode(1),
                right = new TreeNode(4)
            };
            TreeNode root2 = new TreeNode(1)
            {
                left = new TreeNode(0),
                right = new TreeNode(3)
                {
                    right = new TreeNode(7)
                }
            };
            root1.InOrder();
            root2.InOrder();
            int target = 5;
            Console.WriteLine($" In above Two BST, Two Node with sum equal to Target \'{target}\' exist: {DailyProblem.FindTargetInTwoBSTs(root1, root2, target)} ");
        }


        public static void BinaryTreeTilt()
        {
            // https://leetcode.com/problems/binary-tree-tilt/
            Utility.Print("563. Binary Tree Tilt");
            TreeNode root = new TreeNode(1)
            {
                left = new TreeNode(2),
                right = new TreeNode(3)
            };
            root.InOrder();
            int oldTreeSum = 0;
            Console.WriteLine($" Binary Tree Tilt of above tree is: {DailyProblem.FindBinaryTreeTilt(root, ref oldTreeSum)}");
        }


        public static void MaxAncestorDiff()
        {
            // https://leetcode.com/problems/maximum-difference-between-node-and-ancestor/
            Utility.Print("1026. Maximum Difference Between Node and Ancestor");
            TreeNode root = new TreeNode(8)
            {
                left = new TreeNode(3)
                {
                    left = new TreeNode(1),
                    right = new TreeNode(6)
                    {
                        left = new TreeNode(4),
                        right = new TreeNode(7)
                    }
                },
                right = new TreeNode(10)
                {
                    right = new TreeNode(14)
                    {
                        right = new TreeNode(13)
                    }
                }
            };
            TreeNode root2 = new TreeNode(1)
            {
                right = new TreeNode(2)
                {
                    right = new TreeNode(0)
                    {
                        left = new TreeNode(3)
                    }
                }
            };

            root.InOrder();
            int min = int.MaxValue;
            Console.WriteLine($" Maximum difference b/w Node and Ansector is: {DailyProblem.MaxAncestorDiff(root, ref min)}");
        }


        public static void BinaryTreeLongestConsecutiveSequenceII()
        {
            // https://leetcode.com/problems/binary-tree-longest-consecutive-sequence-ii/
            Utility.Print("549. Binary Tree Longest Consecutive Sequence II");
            TreeNode root = new TreeNode(1)
            {
                left = new TreeNode(2),
                right = new TreeNode(3) { left = new TreeNode(4) }
            };
            TreeNode root2 = new TreeNode(3) { left = new TreeNode(1) { right = new TreeNode(2) } };
            root.InOrder();
            int maxSortedLen = 0;
            DailyProblem.BinaryTreeLongestConsecutiveSequence(root, ref maxSortedLen);
            Console.WriteLine($" Longest Consecutive Sequence in above BinaryTree (Ascending/Descending) is of length: {maxSortedLen}");
        }


        public static void SetMatrixZeroes()
        {
            // https://leetcode.com/problems/set-matrix-zeroes/
            Utility.Print("73. Set Matrix Zeroes");
            int[][] matrix = { new int[] { 0, 1, 2, 0 }, new int[] { 3, 4, 5, 2 }, new int[] { 1, 3, 1, 5 } };
            matrix.Print("INPUT MATRIX");
            DailyProblem.SetMatrixZeroes(matrix);

            matrix.Print("OUTPUT MATRIX");

            int[][] output = { new int[] { 0, 0, 0, 0 }, new int[] { 0, 4, 5, 0 }, new int[] { 0, 3, 1, 0 } };
            output.Print("EXPECTED MATRIX");
        }


        public static void MergeSortedArray()
        {
            // https://leetcode.com/problems/merge-sorted-array/
            Utility.Print("88. Merge Sorted Array");
            int[] nums1 = { 1, 2, 3, 0, 0, 0 }, nums2 = { 2, 5, 6 };
            int m = 3, n = 3;
            DailyProblem.MergeSortedArray(nums1, m, nums2, n);
        }


        public static void FlipAndInvertImage()
        {
            // https://leetcode.com/problems/flipping-an-image/
            Utility.Print("832. Flipping an Image");
            int[][] A = { new int[] { 1, 1, 0 }, new int[] { 1, 0, 1 }, new int[] { 0, 0, 0 } };
            A.Print(" Input Image Matrix");
            DailyProblem.FlipAndInvertImage(A);
            A.Print(" Image Matrix after horizontal flip, then invert");
        }

        public static void ValidSquare()
        {
            // https://leetcode.com/problems/valid-square/
            Utility.Print("593. Valid Square");
            int[][][] pointsArr = { new int[][] {   new int[] { 1, 1 },
                                                    new int[] { 1, 0 },
                                                    new int[] { 0, 1 },
                                                    new int[] { 0, 0 } },
                                    new int[][] {   new int[] { 1, 0 },
                                                    new int[] { -1, 0 },
                                                    new int[] { 0, 1 },
                                                    new int[] { 0, -1 } },
                                    new int[][] {   new int[] { 0, 0 },
                                                    new int[] { 5, 0 },
                                                    new int[] { 5, 4 },
                                                    new int[] { 0, 4 } },
                                    new int[][] {   new int[] { 0, 0 },
                                                    new int[] { 1, 1 },
                                                    new int[] { 0, 0 },
                                                    new int[] { 0, 0 } },
                                    new int[][] {   new int[] { 0, 0 },
                                                    new int[] { 0, 0 },
                                                    new int[] { 0, 0 },
                                                    new int[] { 0, 0 } }
                                  };
            foreach (var pointsSet in pointsArr)
            {
                pointsSet.Print("Coordinates");
                var result = DailyProblem.ValidSquare(pointsSet[0], pointsSet[1], pointsSet[2], pointsSet[3]);
                Console.WriteLine($" Above given Coodinates form a Valid-Square: {result}\n");
            }
        }


        public static void MySqrt()
        {
            // https://leetcode.com/problems/sqrtx/
            Utility.Print("69. Sqrt(x)");
            int[] xArr = { 4, 8, 21, 0, 1, 2, 3, 2147395599, 2147483647, 2147395600 };
            foreach (var x in xArr)
                Console.WriteLine($" Sqrt of {x} is {DailyProblem.MySqrtBinarySearch(x)}");
        }


        public static void PermutationsII()
        {
            // https://leetcode.com/problems/permutations-ii/
            Utility.Print("47. Permutations II");
            int[][] numsArr = { new int[] { 1, 1, 2 }, new int[] { 1, 2, 3 } };
            foreach (var nums in numsArr)
            {
                nums.Print("Nums");
                foreach (var uniquePermutation in DailyProblem.PermuteUnique(nums))
                {
                    foreach (var num in uniquePermutation)
                        Console.Write($" {num} ||");
                    Console.WriteLine();
                }
            }
        }


        public static void MaxPossibleSumOfProductOfTheIndexesMultipliedByElement()
        {
            // 
            Utility.Print("Max Possible Sum Of => 'Product Of The Indexes Multiplied By Elements'");
            int[][] arr = { new int[] { -1, 3, 2, -5 }, new int[] { -5, 4, -9, 3, 5 }, new int[] { -9, 1, 2, 3, 4, 5, 6, -5, -4, 7, 5, 6, -8, -4, -6, -1 }, new int[] { 3, 6, -5, 4, -9, 3, 5 }, new int[] { 3, -4, 5, 1 } };
            foreach (var nums in arr)
            {
                nums.Print("Input Array");
                var MaxIndexProductSum = DailyProblem.MaxPossibleSumOfProductOfTheIndexesMultipliedByElement(nums);
                Console.WriteLine($" Max Sum Possible in above array is => '{MaxIndexProductSum}'");
            }
        }


        public static void RegularExpressionMatching()
        {
            // https://leetcode.com/problems/regular-expression-matching/
            Utility.Print("10. Regular Expression Matching");
            string[] strArr = { "aa", "aa", "ab", "aab", "mississippi", "mississippi", "ab", "aaa", "aaa", "a", "ab", "a", "abcdede" };
            string[] patternArr = { "a", "a*", ".*", "c*a*b", "mis*is*p*.", "mis*is*ip*.", ".*c", "a*a", "ab*a*c*a", "ab*", ".*..", ".*..a*", "ab.*de" };
            for (int i = 0; i < strArr.Length; i++)
            {
                //bool isMatch = DailyProblem.RegularExpressionMatchingRecursive(strArr[i], patternArr[i]);
                bool isMatch = DailyProblem.RegularExpressionMatchingMemo(strArr[i], patternArr[i], new int[strArr[i].Length + 1, patternArr[i].Length + 1]);
                //bool isMatch = DailyProblem.RegularExpressionMatchingDP(strArr[i], patternArr[i]);
                Console.WriteLine($" RegularExp \'{patternArr[i]}\' match with entire string \'{strArr[i]}\' results in: {isMatch}");
            }
        }


        public static void PoorPigs()
        {
            // https://leetcode.com/problems/poor-pigs/solution/
            Utility.Print("458. Poor Pigs");
            int buckets = 1000, minutesToDie = 15, minutesToTest = 60;
            Console.WriteLine($" To Test =>\t{buckets} buckets\n Within =>\t{minutesToTest} minutes\n Poision kills Pig in =>\t{minutesToDie} minutes" +
                $"\n WE NEED => \t{DailyProblem.PoorPigs(buckets, minutesToDie, minutesToTest)} Pigs");
        }


        public static void RemoveInterval()
        {
            // https://leetcode.com/problems/remove-interval/
            Utility.Print("1272. Remove Interval");
            int[][][] intervals = { new int[][] { new int[] { 0, 2 }, new int[] { 3, 4 }, new int[] { 5, 7 } },
                                    new int[][] { new int[] { 0, 5 } },
                                    new int[][] { new int[] { -5, -4 }, new int[] { -3, -2 }, new int[] { 1, 2 }, new int[] { 3, 5 }, new int[] {8,9} } };
            int[][] tobeRemoved = { new int[] { 1, 6 },
                                    new int[] { 2, 3 },
                                    new int[] { -1, 4 } };
            for (int i = 0; i < intervals.Length; i++)
            {
                intervals[i].Print("INTERVALS");
                tobeRemoved[i].Print("TO_BE_REMOVED");
                Console.Write($" After removing all overlapping:");
                foreach (var sortedInterval in DailyProblem.RemoveInterval(intervals[i], tobeRemoved[i]))
                    Console.Write($" [{sortedInterval[0]} {sortedInterval[1]}] ||");
                Console.WriteLine();
            }
        }


        public static void GroupAnagrams()
        {
            // https://leetcode.com/problems/group-anagrams/
            Utility.Print("49. Group Anagrams");
            string[][] strsArr = { new string[] { "eat", "tea", "tan", "ate", "nat", "bat" }, new string[] { "" }, new string[] { "a" },
                                    new string[] { "ac", "d" }, new string[] { "cab", "tin", "pew", "duh", "may", "ill", "buy", "bar", "max", "doc" },
                                    new string[] { "ddddddddddg", "dgggggggggg", "essequibo","obsequies" },
                                    new string[] { "run", "had", "lot", "kim", "fat", "net", "fin", "rca", "chi", "lei", "lox", "iva", "liz", "hug", "hot", "irk", "lap", "tan", "tux", "yuk", "hep", "map", "ran", "ell", "kit", "put", "non", "aol", "add", "lad", "she", "job", "mes", "pen", "vic", "fag", "bud", "ken", "nod", "jam", "coy", "hui", "sue", "nap", "ton", "coy", "rut", "fit", "cut", "eta", "our", "oho", "zip" },
                                    new string[] { "fay", "val", "ask", "own", "lei", "fog", "hub", "doe", "del", "sgt", "ref", "lao", "coy", "cop", "fro", "war", "try", "ltd", "peg", "tom", "fdr", "fro", "got", "did", "mac", "luz", "bun", "mom", "wot", "soy", "low", "dam", "ran", "rex", "fop", "wow", "men", "boy" },
                                    new string[] { "tic", "oks", "gay", "cot", "too", "let", "try", "fer", "cut", "bid", "pei", "jot", "val", "fad", "tip", "rot", "yam", "vat", "oho", "sic", "eat", "ina", "hew", "jew", "jun", "cab", "awl", "tax", "lye", "sgt", "job", "zen", "fur", "row", "lax", "bib", "joe", "wen", "boa", "lou", "ewe", "ate", "pan", "bet", "use" },
                                    new string[] { "lbj", "tom", "mci", "vat", "hep", "eon", "sal", "doe", "lot", "ham", "mop", "bin", "elf", "sis", "mob", "maw", "elk", "hub", "die", "ran", "tut", "moe", "bat", "feb", "bic", "jim", "hie", "pat", "pyx", "fad", "beg", "non", "meg", "vat", "irk", "zap", "tub", "gal", "ban", "wan", "shy", "chi", "ali" },
                                    new string[] { "ron","huh","gay","tow","moe","tie","who","ion","rep","bob","gte","lee","jay","may","wyo","bay","woe","lip","tit","apt","doe","hot","dis","fop","low","bop","apt","dun","ben","paw","ere","bad","ill","fla","mop","tut","sol","peg","pop","les"} };//,
                                    //new string[] { "nozzle","punjabi","waterlogged","imprison","crux","numismatists","sultans","rambles","deprecating","aware","outfield","marlborough","guardrooms","roast","wattage","shortcuts","confidential","reprint","foxtrot","dispossession","floodgate","unfriendliest","semimonthlies","dwellers","walkways","wastrels","dippers","engrossing","undertakings","unforeseen","oscilloscopes","pioneers","geller","neglects","cultivates","mantegna","elicit","couriered","shielded","shrew","heartening","lucks","teammates","jewishness","documentaries","subliming","sultan","redo","recopy","flippancy","rothko","conductor","e","carolingian","outmanoeuvres","gewgaw","saki","sarah","snooping","hakka","highness","mewling","spender","blockhead","detonated","cognac","congaing","prissy","loathes","bluebell","involuntary","aping","sadly","jiving","buffalo","chided","instalment","boon","ashikaga","enigmas","recommenced","snell","parsley","buns","abracadabra","forewomen","persecuted","carsick","janitorial","neonate","expeditiously","porterhouse","bussed","charm","tinseled","pencils","inherits","crew","estimate","blacktop","mythologists","essequibo","dusky","fends","pithily","positively","participants","brew","tows","pentathlon","misdiagnoses","paraphrase","telephoning","engining","anglo","duisburg","shorthorns","physical","enquiries","grudging","floodlit","safflower","asphalts","representing","airbrush","bedevilling","fulminations","peacefuller","hurl","unequalled","wiser","vinson","paglia","doggones","optimist","rulering","katmandu","flutists","sterling","oregonians","boosts","slaver","straightedges","stendhal","defaulters","stylize","chucking","adulterate","partaking","omelettes","monochrome","bitched","foxhound","tapir","vocalizing","manifolding","northerner","ineptly","dunce","matchbook","locutions","docudrama","sinkers","paralegal","sip","maliced","lechers","zippy","tillman","penknives","olympias","designates","mossiest","leanne","lavishing","understate","underwriting","showered","belittle","propounded","gristly","toxicity","trike","baudelaire","sheers","annmarie","poultices","therapeutics","inputs","bailed","minutest","pynchon","jinx","jackets","subsections","harmonizes","caesareans","freshened","haring","disruption","buckle","per","pined","solemnity","recombined","chamber","tangling","pitiful","authoritarians","oort","ingratiate","refreshed","bavarian","generically","rescheduled","typewritten","level","magnetism","socialists","oligocene","resentful","lambast","counteroffer","firefight","phil","attenuates","teary","demarcated","moralities","electrified","pettiness","unpacking","hungary","heavies","tenancies","tirade","solaria","scarcity","prettiest","carrillo","yodel","cantilever","ridiculously","tagalog","schismatics","ossification","hezbollah","downscaling","calking","tapped","girl","alba","lavishness","stepparents","integrator","overact","father","fobbing","pb","require","toes","sweats","prisoners","mbabane","hatch","motleyer","worlds","decentralize","ingrained","shekels","directorship","negotiating","hiawatha","busying","reciprocate","spinsterhood","supervened","scrimmage","decolonized","buildups","sedative","swats","despotic","driblets","redoubting","stoic","xeroxes","satellited","exteriors","deregulates","lawful","flunk","broached","energetics","moodily","popinjays","shoshone","misleads","abduct","nonevent","flees","harry","cleverness","manipulative","shoplifts","tom","junk","poniard","transmute","stricter","trochees","snack","relations","edger","culminate","implication","carjacked","kissers","federate","offsetting","sutures","wakened","axis","boxcars","grinds","scenting","cordoba","lumberyards","incendiary","antiphonal","decipherable","gilliam","redecorates","plum","nitpickers","linefeed","awakes","embittering","spewing","annul","filial","scarlet","connors","sanctum","scotsman","isobar","activity","overthrowing","unseasoned","tarantulae","outtake","diego","mars","stunted","hunted","sublimation","barbadian","barbarisms","epidemic","assesses","imposture","freaks","detroit","bloodiest","avails","prenatal","connecticut","guardsmen","betwixt","windsock","neutralized","psychoanalysis","rubberized","overproduces","narcissism","tallow","cringes","resinous","paintbrushes","duality","paints","deactivated","expertly","speedsters","coward","bass","psychiatrist","curies","betrays","bubble","mellow","showy","retarding","radishes","coy","unreservedly","larks","apportioned","flaccid","relabelling","alphabeted","anointment","helms","gillian","trophying","breakage","underbrush","directest","wingtips","pretence","preshrink","remarries","addle","brouhaha","mobbing","g","dings","gains","stockade","ouch","particulates","listens","habituation","kill","crouped","hyperbolae","hutching","stoney","rightness","davids","questioned","ethiopians","courtliness","delays","navahos","devils","keeling","accelerators","investigator","spindling","illegality","extremer","revlon","purity","bradly","jut","machs","liquidated","informant","smartly","disfigure","parliaments","croup","teletypes","impression","trainee","implications","embed","podiatrists","jewelled","brokenhearted","spaceman","unsteadier","kitchen","twirling","conurbations","pygmies","lourdes","watertight","reassessing","dempsey","matriarch","alas","abscissae","decanter","commentated","sandy","idler","soybean","cutoff","dictate","credibility","indeterminable","release","blank","curitiba","pericardia","probably","indisposition","hesitantly","duff","ratty","derivative","decals","explication","cockier","monoxides","hyperventilate","genially","polluter","divan","may","convalesces","morpheme","pupa","prospered","tagging","nerdier","detached","spearing","hilbert","russeted","amanuensis","periwinkles","jujube","guarantors","premises","descanting","baned","deviance","sidearms","lamentable","barristers","climes","succulence","mauve","oceanographers","migraine","bisexual","peruvians","fatheads","parsimony","pilaf","portly","conniving","insidiously","inventing","constabulary","cling","stunting","accessioned","deadliness","overthrow","expectorant","agamemnon","blindfold","striker","shrugging","jibes","appropriateness","annihilates","hairs","proselytes","goads","rankling","predominated","hart","enemies","culpability","drank","martinets","prospering","dominick","complemented","invention","foolscap","tolerances","lorelei","profits","awarer","ungodlier","victoriously","mistrusts","princes","drudge","moderator","transversed","disco","japed","loomed","incense","ponds","gumbel","disarranges","coaxes","technology","hyde","debentures","confidantes","hankered","savant","styes","croupy","unapproachable","cisterns","unto","duds","conglomerating","clio","negroid","looked","methodism","hilario","balloon","thesauruses","integuments","thermometer","slacks","wigwagging","gaping","incensed","misquotes","chocking","patrols","upcoming","insists","livings","thoth","uselessness","vibrated","potluck","starboard","uniquer","boone","scintillates","darker","massey","arbitrariness","miniaturized","rousseau","chiffon","consortia","coral","finesses","half","biked","unlikeliest","hilarious","acrid","twinkles","galileo","outsmarted","ostentation","cradle","frats","misidentifies","uncleaner","bacardi","smoothest","antwan","warren","jingling","stocks","daumier","paranoid","pantaloons","dishing","receive","underpays","kane","variation","beset","disobliged","dreadlocks","psychics","twofers","lieutenants","pebbling","interposes","shingles","profanes","machining","dysfunctions","wolfram","brut","nebraskan","truculently","copeland","devonian","fuller","silvia","philosophers","cali","adores","disquiet","savvies","minatory","blooms","radiotelephones","paradoxically","competitions","gandhi","weddell","occludes","retracing","kari","dead","lagoons","menfolk","abundant","enacts","conferences","procreation","steadier","cols","rabble","unquestioned","stupefying","whodunit","dizzier","paula","riposte","elixirs","discontented","zimbabweans","assemblage","unsalted","genders","caldwell","pulleys","pureness","kingston","vests","hearken","abuses","scull","hussar","solace","gondwanaland","surfacing","vivienne","subculture","reciprocal","expediencies","projectiles","segregationist","prickle","pooped","telecommutes","axes","scenery","peppery","parenthesizing","checked","trademarked","unreasonable","curtly","dynamically","vulcanize","airtight","blotch","edmund","stoicism","scrambles","whirled","chasing","millstones","helplessly","permalloy","remanding","duplicate","broadsided","readjustment","buggers","quaked","grapples","democrats","landfalls","apprehensively","turmoiling","railing","lothario","modishly","faggoted","deflecting","interment","dodo","recreants","baywatch","frescoes","temblor","brigade","handgun","bradstreet","caspar","godsend","cochleae","queered","unevenness","hairnet","millimeters","flawless","plumbing","disciplinarian","orbiting","foothill","serviettes","peseta","windmills","myrdal","provides","slowdowns","clouting","gainsays","dishpans","mediates","weaker","shoestrings","gerunds","potsdam","chips","disqualifications","focus","quarry","dwarfs","laurels","coverall","reconsidered","exploded","distending","bronzes","apollonian","sweeper","couperin","gourmets","irreconcilable","goldbricking","emotes","demilitarizes","lambkin","grouper","anyways","hugs","quizzed","misstatement","spectrums","frigates","plenipotentiaries","parasites","tacitly","savvying","treks","dissociating","departing","resins","psychiatric","tablespoonfuls","aught","makeup","copping","interwove","selling","fantasize","flamingos","smolders","stript","laverne","extremely","chattering","imminent","vaulting","slackly","pasteurizes","goody","pearls","conceptualization","fins","brogues","muskogee","naziism","stromboli","sunflower","tosca","luridness","booing","zaniness","creel","bootblacks","attendants","swordplay","impinging","premiere","sedimentation","traffickers","carrel","observatories","telltales","cuckolded","ampler","alternately","shovel","tasmania","whooping","methodologies","pickling","overseer","sunnier","sanchez","supervening","viscus","cramped","santayana","utopias","intimated","pianists","computerizing","interpolating","reggie","horseshoe","preeminent","qantas","standish","flagpoles","thievery","admiring","palefaces","overflows","gaea","monique","sheepskin","bestiaries","beethoven","fleming","convalescing","moldier","snobby","jewry","hoodwinking","hope","henri","listlessly","doggoning","anointed","notable","talented","uric","towards","flue","arbitrated","ingredients","academy","clutches","novelle","parallelling","confabbed","synthesized","frontally","underexpose","ulcerates","injuring","stimulant","catalytic","ogle","throatily","ions","chores","spyglasses","metabolic","statesmanlike","tingles","ossifies","forge","coiffing","transepts","autopsy","colorfast","winery","procyon","sequesters","amended","putted","huff","fliers","harpooning","protecting","shipboard","dwindled","collations","stonewalls","criticism","thigh","quarried","knelling","knitted","redeemable","berm","seventy","misguides","schlemiel","pawn","ineligibility","lathe","bosses","temperance","haywire","everglade","confections","gruelings","mindful","paracelsus","quarreled","furtively","airdropped","clodhopper","transmuting","whilst","moldavia","exploiting","chicories","unrolling","shorthand","antigens","satirically","earner","primmer","jolly","perch","nonplussing","circulars","hanks","fingerprinted","syllogism","adulate","nominally","telecasted","quelled","accustoming","backslide","culminates","spiraled","compactor","gatorade","cornballs","investor","cupboards","deign","induced","ewe","snoopers","supposed","glitters","overlie","ambassadorial","chancel","accumulations","strictest","thalami","shops","moos","applicators","corncob","dona","apostrophes","kibitzes","rinses","kemerovo","confide","clemenceau","centenarians","memorialized","windburn","nominate","obscene","equivocations","arts","karloff","projected","scorned","limping","lava","sanitaria","clementine","brandies","unionize","compacted","griming","trilogies","babysit","congas","glittery","pimientos","phototypesetter","multivitamin","carbohydrates","lode","photographs","iniquity","micrometer","freemasonry","burros","marchers","percentiles","werewolf","weightlifting","valedictories","gacrux","senselessly","stoppage","monolithic","handy","overspent","nymphomaniac","seasick","misogynistic","coltrane","coeval","inversion","darkliest","landfills","barbers","suppurate","cavern","submerge","illumination","hesitates","lashes","covenants","thomism","aneurism","disappointed","gnarls","sprint","abash","frightens","undoings","pa","helicopters","reexamines","vassal","blessing","devaluation","purports","urinals","adjudged","garaging","pacific","infomercials","whitewashed","fawned","baptisms","concede","cornflakes","fostered","clewed","tiller","dalmatians","signification","boneless","chunkiness","omar","paramedicals","professor","unionizing","scripted","anchors","tabloid","alton","redrafted","reflexive","luddite","lamb","bidirectional","seaports","christendom","gets","chaperoning","tchaikovsky","wasters","dioxin","nuke","apologized","queasily","fujiwara","prearranges","abdul","upraising","sparklers","signposting","comparison","sb","cherokees","ungentlemanly","typing","waisted","sputter","biographers","waltz","stanches","upbringings","smithereens","tutor","young","eloy","sourdoughs","clingier","hoisting","blazon","homosexuality","lorries","kippering","abacus","specks","congressional","auditing","lash","eternal","carve","facade","defrauds","neighbored","musses","dismount","lope","lawbreaker","deed","japes","repeal","factorization","impetuosity","sitters","disorganizing","fussing","vale","epitomized","executrixes","deprivations","woodcarvings","miscalls","skateboard","pedicured","cloakroom","vassaled","innumerable","knelt","cellulose","beams","uniform","metatarsals","meteorologist","column","burnishes","dentists","quids","toasts","tableland","archivist","gladiolas","replica","lording","viewed","polisher","trooping","indistinctly","resisters","flycatchers","toughed","regor","insolvent","ninnies","truckled","birthplaces","telescopic","abelson","puritanism","leanings","disturbingly","transmission","mortify","upshot","newlywed","adam","ballplayer","lockwood","quirking","blocs","theatre","palliatives","smudges","marvelled","ramble","offside","indissoluble","droplets","fencing","hubbard","estimation","incorrect","malarial","confucian","games","sacraments","trivets","gammas","nastiest","merrymakers","sealskin","overkilled","bosser","strafe","exclusives","bouldered","antiwar","guitarists","jerry","earthly","oscilloscope","edmonton","merger","laminated","surmountable","casually","backspaced","charcoals","overheating","caramel","oldened","asterisked","tun","peafowl","purplest","skippering","prep","congregating","glaringly","crummiest","noreen","bromide","nomenclatures","kristin","purportedly","vamoosing","busybody","crucify","capote","milliners","veils","windsurf","reconnecting","layering","ossified","noble","tiptoeing","smiles","swain","perihelion","bagels","obfuscation","spreadsheets","buddy","flints","planting","hogwarts","abusers","welfare","mouses","lament","auras","unrelieved","cougars","cattails","chubby","handstands","woolly","concealment","mediterraneans","judas","electrocardiographs","skulks","puttered","crimean","liven","odds","warehousing","lifeguard","deepness","clowns","blossomed","constriction","honest","noisemakers","whist","overcame","sulphured","vertebras","commiseration","jolted","adjourns","bungles","sonnies","housekeeper","buddha","bolsters","warlords","banjarmasin","militated","anywheres","lula","weirdos","raymond","sections","taoisms","pay","latest","bights","carousel","sups","lavatory","conciser","lon","beefburger","clinically","snakes","backslash","developmental","squibb","smote","mastectomy","genius","sallying","niagara","guild","altai","ascetics","marts","misbehaved","desired","pagodas","platypus","freemen","lovemaking","transfers","brewing","absorbents","unions","lite","wilder","popovers","yamaha","faultier","supplements","forsythia","rummy","propagation","motorbikes","velez","unequivocally","lend","silliness","idiosyncratic","disseminated","carter","washed","dizzying","bedsore","pawned","lr","nubile","galloped","subservience","marlin","chance","schooners","faction","clutters","transmits","weathercocks","illustrations","quell","senegalese","touchiest","psychs","joshes","shallots","garrottes","coifs","glaswegian","hydrated","smirch","strutting","arnold","coughing","tangier","olympics","overexposing","benefactor","reputably","snootier","smuggles","bogus","priories","chandra","diplomatics","muskrat","forbad","monasticism","outshone","farewell","thomas","epaulets","nectarines","affording","buckles","concordance","lebesgue","pawed","lackey","sweden","confirmatory","humble","wizards","controlling","scoffing","worthy","homely","lexical","batteries","chorusing","inboard","cotton","lustrous","devalued","herbart","travestied","veneered","maxillas","omelet","ptarmigans","alnilam","submerging","bucks","niceties","yong","gender","toileting","biding","caffeine","lubricant","dashikis","balm","filings","series","paraguayan","fatefully","craggier","oversexed","milkweed","passels","concretely","rapiers","channeled","multifaceted","tenth","conflagration","pivoted","horribles","tugs","fireman","hull","semifinalist","odorous","carats","uncomfortably","clappers","chaffinches","demagnetize","limits","ups","wimp","reserved","busyness","illuminate","autocratic","zips","sculley","vainest","conciliators","vacillations","daughter","beau","stashes","mini","shallow","divided","invert","caesurae","vibrantly","germination","winching","curmudgeon","hurts","battens","lovable","redistricted","neighing","nonexistence","foghorns","manifesting","retrofitting","fictitious","gracefully","shetlands","transistors","rectitude","shea","acquisitions","predetermines","huts","blintze","cortland","murdered","leninism","ninetieths","fractal","inveighs","compelled","ringer","mistiest","snorting","transportation","dictionaries","khyber","misspelling","bifocal","playmates","disputant","neuters","epidemics","vijayawada","ambles","splashdowns","cants","bandannas","millennia","glows","fowled","cupped","laramie","occupied","gelatine","scorches","sidelights","beagling","campused","doer","gunfighting","tsitsihar","marin","fireside","yellow","seagram","strap","arrivals","sixty","lipscomb","wares","awfullest","servants","dreamer","crockery","mahavira","brad","outpourings","dusty","shrubs","briefest","segments","cartilages","excommunicated","generator","placket","berate","emulations","suspicious","fortnights","sped","griding","panting","trimaran","suspend","retract","adversity","regimental","hammett","wallflower","tyrannize","cinemascope","ambassador","humerus","litter","trying","sinkable","descents","coveralls","region","landslid","bowed","zigzagging","upholstery","parch","scratchy","altitude","angling","scaldings","plussed","babel","cirrus","haberdasher","gayle","chillest","catboat","battling","bellini","extincts","appendices","unmade","footstool","deaths","secretively","erasmus","agra","soothsayers","stillest","despoils","affiliated","mumble","aleut","gyp","purchased","kory","cesspool","interacted","demarcates","fanciness","defines","absorbed","fireplugs","pluckiest","misstates","grenades","untidiness","ventricles","overexpose","dryad","tumbler","chengdu","thistledown","dork","unauthorized","holmes","downgrading","syphilises","trammelled","sending","afflict","ornithologist","serried","much","emoluments","wilfully","strayed","canada","dachshund","compost","glamorized","gerrymandering","senate","allots","arsonists","coloratura","borderlines","cartooned","evert","continents","profiteers","merritt","cyclical","quickens","funkier","dourer","salmonellae","seemingly","cheeky","showering","proses","imperturbably","gush","intolerable","wozniak","vegetables","neglectful","aesculapius","whimsicality","unfolds","conceited","junkyards","immanent","norbert","pollsters","ruse","gymnosperms","toothbrush","accommodate","multitudinous","blessedest","squeezers","portal","newspaperwoman","polytheistic","affectioning","rhiannon","holing","johanna","gregariousness","fishtail","tainting","wasteful","aeneas","flycatcher","salem","ventilator","sat","hiroshima","breed","housewarming","migrate","smocking","plethora","fathead","mussy","communion","foxhole","renters","telecommunications","obtusest","puncture","preparing","encyclopaedias","footholds","violets","megalomaniac","dakotas","accentuating","joanna","gentleman","dardanelles","aeration","gerrymander","liker","chatters","questioningly","guesswork","lunar","upgrading","mara","consultancy","sanguines","sunks","polyethylene","injected","aneurysms","caterwaul","eventfully","aquariums","yowl","valeria","suicides","emptiness","pajamas","uphill","myles","playfully","palestine","agape","chichi","vatican","exponentiation","shipper","planes","reprints","dieters","involving","dimwitted","cranach","homestretch","fitfully","fluoridation","esthetics","coworkers","cannibalize","lipids","heartsick","flatware","rage","hazier","overburdening","psychosis","wage","debasements","personalized","itaipu","valletta","rhythm","cyclic","hillocks","badlands","proffer","cretin","sexuality","hefts","rehabilitate","disfranchisement","skews","rundowns","unrolls","dividing","boomerang","gigglier","respects","layette","heads","devised","franny","therapists","ballsy","inkier","passer","capone","housebreak","rachel","portered","palpate","bugles","marionettes","werewolves","primitive","powerboat","polio","wylie","industrial","convenes","sweep","wear","peaks","trudging","manuring","yttrium","hometown","graving","latency","warrant","tinsmiths","preheats","withered","priestley","dipper","overtakes","thurber","softwood","renounced","turfed","immensely","pilgrim","neophyte","mendicant","trampolining","detachable","corralling","haydn","victimizing","pockmarks","goya","triumphs","sleet","whiplashes","inconsistent","slaked","handled","nissan","housecleaned","gentling","alexandra","dallying","kiddos","rayleigh","oxen","boarders","demonstrator","mollusk","micawber","earthshaking","burials","consecration","algeria","sinister","bloodcurdling","garrotted","byte","dungeon","ferrets","trunk","finery","dodos","staid","champagnes","gelt","hedonists","barrier","trikes","lock","scaly","hibernation","lopsidedly","snake","ellison","runabout","rhapsodizing","divinity","bugged","entomological","kazoo","powdered","smeared","shoestring","knocker","galbraith","clomp","rudders","infused","sale","silicone","underfoot","chirruped","beatific","preached","commingles","enchanters","nonflammable","vacillating","autistic","uganda","intruding","expatiating","coddles","attendant","races","paying","confuting","guyed","expanded","tolstoy","contestants","judd","comediennes","pansy","upstage","schoolmistresses","lesion","rollback","rapt","sicked","locomotion","flint","interactions","vaginae","intonation","comedic","totaled","sickles","immensities","leastwise","brothers","rinking","worksheet","impending","mussed","reinterpretation","backtrack","thrummed","devastate","plaiding","accusatory","rafts","stew","infringe","impulses","shuffleboard","decaffeinates","marquez","milksop","artisan","attlee","decide","italic","catalyst","squeamish","transmuted","coffer","sadness","forays","slipperier","bella","dandy","singletons","inflating","pittance","shearer","spilling","acid","mortgager","proverbs","wily","headwinds","murmured","humidified","cramping","herrick","phoneticians","boas","bucksaw","play","falters","overbites","dill","renovators","mush","clockworks","diaspora","favorites","stomachaches","flinch","boston","scrounges","hubert","utters","million","weekended","touchdown","reactor","postcards","brown","snide","banjoist","recourse","deuced","scurrying","chihuahuas","abutments","proliferate","carillonned","saucier","kaposi","amanda","fillet","demographics","eldon","skyed","abut","bernadette","downplays","alluvia","trappers","insofar","breathlessness","insistently","imitative","cede","embargoed","boisterousness","edicts","braise","impacted","ukrainian","lankiest","bounden","trilling","pinnate","unconvincing","kent","indulging","stagnant","piston","cookery","commits","caucasoid","fronds","bootless","clustered","giggled","lugubriousness","intents","elms","boorishly","stalemate","blaine","proportionality","sampson","ethnologists","francisco","headmistress","propel","knapsack","misapplied","ensnare","subtotal","intersected","maud","shooing","pouts","messed","schmidt","liberated","continuations","impossible","unsatisfied","flanneling","unscrupulous","comity","scopes","incised","venial","takes","auction","bashfully","bremen","televangelists","eyesight","ineptness","hecate","sweller","carrot","presidency","hook","nerveless","mastication","abstractnesses","precognition","indent","sombre","fieriness","quiescent","indispositions","shiftiness","caveatted","gulps","best","spread","chews","prevaricating","shoon","manicures","privier","yawns","surnames","solacing","tattering","contrail","downsizing","unnerve","avenger","misanthropists","retiring","roughest","canard","dowdiness","corinne","hilton","summon","whacky","luncheonettes","morals","hunches","ibices","waded","meany","valving","articulations","typefaces","born","wretches","reflexes","tickled","showier","reddest","scallop","rethought","registries","behaves","abnegates","sharping","helm","rapscallions","manifestation","elected","mulligan","unmanageable","exude","centerfolds","unbind","enshrine","skeptically","pained","yeah","frostbiting","nonessential","bakers","exorcist","tonalities","timmy","provisional","bugatti","whisper","nonfiction","snippet","quest","jabberer","mittens","metatarsal","quotient","sponsorship","ferociously","brig","meditation","scotch","mess","saith","appropriately","smithson","emacs","bookending","misses","warmonger","subvert","pretties","ficklest","inaugurated","teetotal","shipmate","irony","canaan","jeopardized","lanes","sloan","connexion","ideals","uprising","sited","lamebrains","patchwork","vocalizes","maura","taller","patronage","barbarians","midshipmen","granddaughter","grapefruits","sideways","mistreating","pensive","foreseen","manageability","lammer","affiliation","aguilar","aunts","ghats","tricycles","privileged","athlete","ls","colonnades","constrictors","stickied","ritualism","catharses","scrappiest","almoravid","tiaras","habituate","merino","witches","handbag","steamroll","conics","marion","forte","rectangles","australoid","gaborone","invidiously","fugues","nervelessly","p","plunderers","newspaperwomen","embolism","splendider","lathes","pleistocene","acceleration","caterers","hauled","fluent","severus","dubbed","rollicked","cal","hastily","scourged","homogenization","leakages","teaches","vocabulary","interpolate","bacterium","ambiguous","virtual","flory","apostrophe","bellamy","lushest","misconstrued","obsessives","romanticizes","feeblest","encodes","anthropologists","clinked","hammerstein","duckbill","expurgations","coroner","unclasping","inconsistently","default","skullcaps","inverses","toadstool","raga","cristina","malady","nonrenewable","zonal","suva","trothed","clarifies","moravia","waggles","flyspeck","blanker","overstays","admired","speckles","fumigates","languished","dieseled","president","tuition","escapees","meyerbeer","futilely","pills","afghans","homeboys","blankness","hackles","footman","spies","horsed","arctics","sunder","mcluhan","secures","withdrawn","bellicose","quibbles","chastens","episcopate","shenanigans","salinity","mending","handsome","tweed","ladle","condorcet","metric","tattletale","rashly","nurtures","contemplation","tucker","moroni","nickolas","mcclellan","phonics","copyright","overwhelms","swahilis","chessmen","amusingly","conjoint","abstrusest","canonicals","ono","callouses","jiggled","typecast","memoranda","tuxes","stalwart","ennobling","nuked","gaggle","followings","beeper","hacksawing","acclimatize","orient","declaims","rising","enamor","doxologies","waistcoats","gustatory","kitchening","overpower","subordination","bone","ricking","yenisei","harvesters","junior","elector","simulcasted","clarinettists","modulators","happily","critic","damage","ineffectual","arbitrates","portrayed","animists","monasteries","abductor","dissolve","befuddled","emergence","laments","naturally","ankle","glastonbury","woodpile","zoroastrian","near","orneriest","temperated","icon","stiffed","penis","reapplied","getting","specializes","bushy","complaisant","slocum","unreadier","stiffest","fives","clumsiest","engineers","plumped","waistline","judgeship","cryptographers","bordello","babar","monthly","possiblest","libeler","cartwheeling","stiffer","monickers","digraphs","salinger","carping","wrigglers","assyrians","sprawls","visits","saunaed","preambling","flairs","obtuse","unbounded","septuagenarians","syllabi","roulette","conjure","clones","aristocratic","orleans","discountenancing","primordials","expropriations","patellas","prurient","walloon","electroencephalographs","contribution","dwarfed","caryatides","billy","presumed","gnarly","chinchilla","shinning","gripped","sigurd","ruff","circumcising","shriveled","cysts","bernstein","storming","calibrations","suffuse","stodginess","brownian","colorblind","aberration","cornered","droppers","heartbreaks","becker","suffusion","ambassadorships","souses","arizonian","oarlocks","accountants","elephants","surer","citadels","betroths","gentian","individualism","venezuelan","tour","rancorously","cabral","constants","parochialism","fustian","coxswains","ironing","rusted","babbling","inapplicable","sedans","leukocyte","opaqueness","bluffs","tuneless","capsized","brochures","severance","hyperventilated","len","shortstop","adoringly","damming","credits","accentuates","lurch","illegitimacy","martens","bleeders","rankle","mortarboard","unify","remark","thermometers","provoked","bittersweet","dame","clarinets","workers","misinterpreted","surveillance","thickset","assaulted","intervening","swede","rustbelt","suns","gasoline","clifton","energized","indiscreetly","soundless","notepad","uneaten","cicatrixes","rhythms","inimitably","subjectively","drifter","snowdrop","moonstone","olga","sabled","eightieths","yonder","inaccurately","madams","flautist","surrealist","persimmons","davenport","retinues","markets","cursing","femora","sharpers","fishermen","cores","clarifying","lakes","sake","sync","impinged","smartness","retarded","grubby","okayed","rustles","croupiers","civilizing","suffocatings","blackmails","recessives","substantives","profitable","delicatessens","familiars","terence","frumpier","maximums","shiners","regals","stieglitz","newsagents","confirmation","inebriation","alva","snowplowing","crudity","tams","gobs","sourpusses","pursuant","busts","angrily","betaking","subsumed","uniformity","mote","censuring","sir","sifting","yachts","sexists","orientation","forewarned","brigs","gyro","knickknacks","retrains","klondiked","nonpluses","contemplative","sears","trammed","serializes","brigands","stratified","burial","sumter","malplaquet","upholsters","parenthesized","dinkies","classy","jugulars","contradicts","sunup","terracing","bulletined","postponements","teenier","afterburners","greenback","dependants","hardy","unbelievers","surrealistic","chaises","snapple","dowry","writhes","expurgates","magically","toolkit","boondoggled","timorously","fuselages","corine","deploying","neckerchiefs","oz","sequoya","waives","dives","reprocessing","clearly","discontinuations","mezzanines","antibiotic","likeness","squabbles","navigable","temples","described","libretto","tanking","perilous","drowsiness","pilferers","beakers","ingram","prevaricate","voiced","procrastinated","vanilla","salween","han","marquises","bewails","cocks","ledger","panty","dupont","typewriter","blackfoot","possession","conglomerated","collects","escutcheon","titting","condiments","smouldered","marquees","interjected","element","outlet","briefing","liquidator","shrieks","nanking","salting","reopens","glasgow","arabians","transgressing","reasoning","bleated","bearable","criticize","whereat","bureaus","dismounts","roxie","tussled","yeomen","equine","cyclist","hugely","felted","messy","desist","shenandoah","sushi","storeroom","hindquarter","opposed","benevolently","archives","luaus","diskette","chiefer","hangover","sparta","faxed","hedge","eddington","pins","bushiest","loco","rooking","lifer","trespassed","swears","husked","darrell","completed","shirring","interactively","places","injured","stolid","secondly","explores","committees","airwaves","littler","sprawling","hydroplaned","uglied","depended","mortared","gloaming","shinbones","outlaw","proselytize","moralling","worn","extricates","exclamatory","layperson","frolicsome","conjugates","arrest","guttural","paycheck","phyllis","alphonse","drumming","multiply","wed","actuating","sidled","sapient","fingers","wantonness","habitually","emulsion","handwork","indeterminate","prig","pareto","wartime","nuthatches","squalider","selflessness","chip","uvulars","debonair","replenishing","naughtiest","renounce","anemic","schoolgirl","dilation","slipping","tape","ginning","crucifixions","indecisiveness","formidably","pokes","resigned","chitchatted","holograms","coppices","dialects","vicissitudes","penologist","shirtwaist","heeds","hybridizes","calliope","likable","adequately","wight","heehawed","exhibits","undressing","seditious","collapses","perishes","peevish","foregoings","harsher","effectuate","halls","petitioned","afrikaners","bright","holidayed","subtitled","comfortable","whoa","aspens","mops","islams","valedictorians","jellies","jewel","gouging","leftmost","breaches","minimal","overcharged","porting","smith","predominates","motorbike","enigmatically","equable","trickery","streetcar","flautists","countrysides","disregarding","pakistanis","grapevine","chicana","maneuverable","diction","falsified","peppering","murmurs","abated","radiance","maharajas","pediments","entrants","friedman","discombobulated","misprints","reimbursements","andropov","fizzle","stress","sifters","establishments","chippers","boomed","describable","contused","moscow","stonewall","meander","kaleidoscopes","subcontract","physiognomies","bayeux","telegraphic","emotional","govs","crotches","jayne","morison","funguses","schlepped","rhubarbs","bump","banged","shrieked","petal","phooey","piercings","defending","lapidaries","muffle","halcyon","transmitting","smallest","tottered","bossily","ruling","sequencers","lowest","overtaxing","misalignment","plutonium","paroling","windsurfs","annals","nasser","gomorrah","dispute","intensity","unwillingly","witt","stockiest","addends","jacklyn","kind","giauque","reborn","discontinued","headdress","favorite","vivaces","keith","descriptions","secondarily","whinnier","kismet","underrates","restated","guessed","inestimable","irritable","managua","deft","blockbusters","ascribes","melodramatics","cormorants","deliquescent","unpins","twits","confers","bargained","tide","floundering","censorious","gouges","magistrates","reynolds","sequels","as","incinerator","swathes","candelabras","spelling","scratchier","dejecting","implementation","reaffirms","blanches","western","ebony","reason","appending","polluters","jove","gambits","corpuses","grittier","rhetoricians","distils","explicate","jansen","jolt","curing","medicine","tender","supplanted","andres","scrofula","glopped","indulgently","pilchards","scuffs","harmonize","hoagies","xmases","witness","caller","detail","mayo","misidentify","bath","disports","filets","cheapest","scorpions","prevent","promptness","looping","authenticating","cab","reamers","protestation","outgrown","devoting","naphthalene","quarrels","rambler","garment","councilor","chronic","acceptances","resurrection","southpaw","raceway","diffidence","uneventful","mecca","cheri","scarceness","nebraska","elfin","bookies","dietary","auditory","eggo","articulateness","steadying","boulders","impressing","misdo","greta","comae","horace","pedalled","tram","sahib","fetishists","dumbness","belonged","rotunda","moons","falconers","rail","dispels","redeemer","petitioners","forewent","onshore","precincts","detoxes","quintupling","befouling","knit","balls","eddy","poetess","divines","candor","tomcats","weapons","unsnaps","viaducts","chatted","susie","reconvened","condemns","thickeners","collect","rock","apothecary","clue","mulberries","territories","pups","earthing","pledged","coffers","wells","extractors","mistiness","sterility","microfilming","anesthesiologists","miss","volleys","nabs","foamier","wrested","minefields","empathizes","lakshmi","victualling","huffiest","shirrs","centralizes","inapt","besieging","babbitt","psychoanalysts","gybe","enlisting","discomfits","helicoptered","sukkot","skidded","avoirdupois","horsewhipping","khulna","plateaus","rwandan","emerson","copycatted","herald","bemuses","hesitate","gleefully","siestas","tonsure","generals","flunky","eyes","indivisibly","hydrangea","berne","gag","redesigned","insinuating","reenters","remote","douche","budget","afterlives","shootout","shortbread","eatables","omens","abelard","waxen","presaging","platforms","beachheads","redeveloped","praia","peppy","indonesians","plated","abouts","sunbathed","mobiles","playacted","files","verlaine","deceptively","townsmen","acetic","resorting","ritually","rhinestone","neologisms","opposition","cauterizing","sprinkling","theoreticians","khoikhoi","peahens","compensated","shrive","roughage","drastically","objector","manliness","lanyard","espied","saturate","canards","launderer","hightailed","hauler","frosty","blithely","astrophysicist","squirrel","derailing","huffily","suture","mahatma","egocentric","reverting","doctors","anubis","flatly","minuter","staterooms","delineate","rive","offbeat","principals","micra","invoices","menominee","cartographer","ujungpandang","notoriously","reapplying","totalled","whiskers","crassness","incoherently","grassiest","sponging","protects","numeration","preppy","ceased","powering","recreates","musicians","header","bounteous","negligently","berenice","trustfully","methinks","preface","disproportionately","comeuppances","firefighting","synced","disturbing","tuscan","robles","factored","aircraft","nazca","shrank","panhandler","oversleeps","huckleberry","countersank","hopper","paneled","kayaking","billowy","inhalers","sapped","rarefy","thunderclouds","rockets","imperiously","dregs","hockshops","stemming","nymphomaniacs","tightening","refreshing","ryder","persecutor","win","darted","benchmark","abuzz","incises","brunei","scuttled","astronauts","pizazz","covetously","maltreating","barbarism","manila","zuni","humanizes","microcosms","fellow","exonerate","geckoes","foregone","cuttings","butterflying","underflow","saintliness","slather","toddle","isobars","spelunkers","rigmaroles","offings","interning","representative","luann","pursuit","tennis","marijuana","reconvening","shapelessly","strum","kilts","brutalized","gloom","rajas","courses","poising","disclaimers","baluchistan","brief","recognizable","gore","outermost","ascertainable","pitfalls","posers","vegetable","inglorious","mottos","hurtle","heartbreak","chichier","bolshevist","cockscomb","delineates","spouted","asides","chrystal","presentiment","pekings","teargas","classes","foamed","distinguished","sherlock","purling","apace","goalies","specifiable","enfranchised","cycle","crunchiest","ericson","membership","sheraton","reaffirmed","lassie","readily","gunfire","archaism","bulged","watercraft","internal","dishonors","caviled","volcanic","blahs","instances","yammers","colombians","mussier","allegheny","contraband","heinrich","dogged","impossibility","syntactic","infuriate","max","specifically","brent","eugenics","ankhs","icelander","woodshed","emblazoning","thru","overhearing","ultrasuede","hillock","twitching","dishonoring","gridlocking","grouches","sequestering","pressurized","freeloads","ursula","carpet","formulation","aliens","warbling","frazzling","furnishes","abridged","theocracies","karamazov","abridgements","adipose","wideness","lacing","adversaries","prohibitions","nosy","inopportune","radiotherapists","extortionist","relabels","bullish","diagraming","bracing","scrounged","seniority","congregationalists","incompatibles","cavalcade","allergist","stippled","salaciousness","crazily","snuggles","deserve","squashiest","waterfowls","carjacking","trampolined","sister","swankier","kazakhstan","blaspheming","kidnappers","disorganizes","elongated","glopping","windbag","buckeye","rebellious","leaf","biographer","verily","downpours","baxter","qaddafi","suffocated","drowns","doses","rebated","amplifying","paprika","played","midwifed","nightfall","rocketing","solemnizing","pacifiers","alaska","snicker","small","hotshots","jumbos","leagued","subpoena","sivan","overbalanced","rasalhague","washbasin","imponderables","packers","consecrate","majorettes","clarendon","fain","pettifog","bessie","reword","genoa","storey","ezra","shutting","legitimately","obsequies","leggy","diodes","mechanizes","buggies","disavowal","jaunties","johnnie","ladyship","epiphany","probabilities","bucketing","tomboys","slackens","womanizer","venn","toddling","hustling","tattle","bikes","mouthe","daryl","succumb","geffen","honeymoons","denmark","caucasians","introverts","verdure","plausible","conjuror","insetting","aftermath","withers","miller","interdepartmental","decalogue","liquefies","recreated","holographic","median","pidgin","matchless","skinnier","milligram","psychic","titted","consecutive","reject","inhales","colossal","tintinnabulation","virgil","fiduciary","friday","resumptions","rambling","ilene","bordeaux","swaying","giacometti","succeeded","hobbies","photocopying","reinvests","chiselers","respire","dodoes","vaporous","gloried","unproductive","salami","argots","allege","mutinous","mirfak","keepers","hocked","primness","tubers","improvidence","soil","discontinuing","landslides","mollycoddling","smokier","corolla","ripper","splenetic","convening","twangs","snootiest","rousing","stablest","ordinarier","windiest","quizes","tallahassee","hutchinson","becomingly","attentively","sourcing","guppy","demands","lupins","ladders","liechtenstein","emancipated","toughness","maladjustment","bringing","clopping","acrylic","undervalues","bogy","juggles","proprietor","assertions","snobbishness","overcautious","bernie","pylons","enmity","westerns","hobbyhorse","edgy","probing","footsore","cagney","rumpling","engorges","holdover","lyman","ambiguously","boding","adulterous","intriguing","capstan","punitive","stiletto","monotony","weathercocking","torturing","sixtieths","inkling","nutritionist","akron","habits","eyries","ukraine","curlicues","sate","rosendo","seclude","commended","truthfulness","fender","shekel","unrelated","crouch"} };
            foreach (var strs in strsArr)
            {
                strs.Print("Input");
                foreach (var grp in DailyProblem.GroupAnagrams(strs))
                {
                    Console.Write("[");
                    foreach (var word in grp)
                        Console.Write($" \'{word}\'");
                    Console.WriteLine("]");
                }
                Console.WriteLine();
            }
        }


        public static void LongestMountain()
        {
            // https://leetcode.com/problems/longest-mountain-in-array/
            Utility.Print("845. Longest Mountain in Array");
            int[][] inputArr = { new int[] { 2, 1, 4, 7, 3, 2, 5 }, new int[] { 2, 2, 2 }, new int[] { 1, 2, 0, 2, 0, 2 }, new int[] { 8, 3, 7, 3, 4, 10, 1, 1, 2, 8 } };
            foreach(var input in inputArr)
            {
                input.Print("Input Array");
                Console.WriteLine($" Longest Mountain in above has length: \t{DailyProblem.LongestMountain(input)}");
            }
        }


        public static void MergeIntervals()
        {
            // https://leetcode.com/problems/merge-intervals/
            Utility.Print("56. Merge Intervals");
            int[][][] intervalsArr = { new int[][] { new int[] { 1, 3 }, new int[] { 2, 6 }, new int[] { 8, 10 }, new int[] { 15, 18 } },
                                        new int[][] { new int[] { 1, 4 }, new int[] { 2, 3 } }, 
                                        new int[][] { new int[] { 2, 3 }, new int[] { 2, 2 }, new int[] { 3, 3 }, new int[] { 1, 3 }, new int[] { 5, 7 }, new int[] { 2, 2 }, new int[] { 4, 6 } } };
            foreach (var intervals in intervalsArr)
            {
                intervals.Print("Input Intervals");
                var merged = DailyProblem.MergeIntervals(intervals);
                merged.Print("Merge Intervals");
            }
        }


        public static void InsertIntervals()
        {
            // https://leetcode.com/problems/insert-interval/
            Utility.Print("57. Insert Interval");
            int[][][] intervalsArr = { new int[][] { new int[] { 1, 3 }, new int[] { 6, 9 } },
                                        new int[][] { new int[] { 1, 5 } },
                                        new int[][] { new int[] { 1, 5 } },
                                        new int[][] { new int[] { 1, 5 } },
                                        new int[][] { new int[] { 1, 5 } },
                                        new int[][] { new int[] { 1, 5 } },
                                        new int[][] { new int[] { 1, 5 }, new int[] { 6, 8 } },
                                        new int[][] { new int[] { 3, 5 }, new int[] { 12, 15 } },
                                        new int[][] { new int[] { 1, 5 }, new int[] { 9, 12 } },
                                        new int[][] { new int[] { 1, 2 }, new int[] { 3, 5 }, new int[] { 6, 7 }, new int[] { 8, 10 }, new int[] { 12, 16 } },
                                        new int[][] { new int[] { 1, 4 }, new int[] { 9, 12 }, new int[] { 19, 22 } } };
            int[][] newInterval = { new int[] { 2, 5 }, new int[] { 2, 3 }, new int[] { 2, 7 }, new int[] { 0, 3 }, new int[] { 6, 8 }, new int[] { 0, 6 }, new int[] { 0, 9 }, new int[] { 6, 6 }, new int[] { 0, 4 }, new int[] { 4, 8 }, new int[] { 7, 13 } };
            for (int i = 0; i < intervalsArr.Length; i++)
            {
                intervalsArr[i].Print("Input INTERVALS");
                newInterval[i].Print("New INTERVAL");
                var merged = DailyProblem.InsertIntervals(intervalsArr[i], newInterval[i]);
                merged.Print("Merged INTERVALS");
                Console.WriteLine(Utility.lineDelimeter);
            }
        }


        public static void MaximumProductSubarray()
        {
            // https://leetcode.com/problems/maximum-product-subarray/
            Utility.Print("152. Maximum Product Subarray");
            int[][] numsArr = { new int[] { 2, 3, -2, 4 }, new int[] { -2, 0, -1 } };
            foreach(var nums in numsArr)
            {
                nums.Print("Nums");
                Console.WriteLine($" Max Contiguous Product in above is: {DailyProblem.MaximumProductSubarray(nums)}");
                Console.WriteLine($" Max Contiguous Product in above is: {DailyProblem.MaximumProductSubarrayBruteForce(nums)}");
            }
        }


        public static void MirroReflection()
        {
            // https://leetcode.com/problems/mirror-reflection/
            Utility.Print("858. Mirror Reflection");
            int p = 2, q = 1;
            /* Problem Statement:
             * There is a special square room with mirrors on each of the four walls.
             * Except for the southwest corner, there are receptors on each of the remaining corners, numbered 0, 1, and 2.
             * 
             * The square room has walls of length p, and a laser ray from the southwest corner first meets the east wall at a distance q from the 0th receptor.
             * 
             * Return the number of the receptor that the ray meets first.  (It is guaranteed that the ray will meet a receptor eventually.)
             * 
             * Example 1:
             * Input: p = 2, q = 1
             * Output: 2
             * Explanation: The ray meets receptor 2 the first time it gets reflected back to the left wall.
             */
            Console.WriteLine($" For a 'Square Mirror room' of side len P: {p} and Ray coming from southwest-corner hitting east side wall at distance Q: {q} from bottom" +
                $"\n Will Hit Receptor No: {DailyProblem.MirroReflection(p, q)}");
        }


        public static void GenerateParenthesis()
        {
            // https://leetcode.com/problems/generate-parentheses/
            Utility.Print("22. Generate Parentheses");
            int[] nArr = { 1, 2, 3, 4 };
            foreach (int num in nArr)
            {
                Console.WriteLine($" For N: {num} brackets valid parenthesis are:");
                List<string> result = new List<string>(100);
                DailyProblem.GenerateParenthesisBackTrack(num, "", 0, 0, result);
                foreach (var validParan in result)
                    Console.Write($" '{validParan}'");
                Console.WriteLine();
            }
        }


        public static void BestTimeToBuyAndSellStockII()
        {
            // https://leetcode.com/problems/best-time-to-buy-and-sell-stock-ii/
            Utility.Print("122. Best Time to Buy and Sell Stock II");
            int[][] pricesArr = { new int[] { 7, 1, 5, 3, 6, 4 }, new int[] { 1, 2, 3, 4, 5 }, new int[] { 7, 6, 4, 3, 1 } };
            foreach (var prices in pricesArr)
            {
                prices.Print("PRICES");
                Console.WriteLine($" After completing multiple Buy-Sell on above, MaxProfit: {DailyProblem.BestTimeToBuyAndSellStockII(prices)}");
            }
        }


        public static void BestTimeToBuyAndSellStockIII()
        {
            // TECH DOSE https://youtu.be/37s1_xBiqH0
            // https://leetcode.com/problems/best-time-to-buy-and-sell-stock-iii/
            Utility.Print("123. Best Time to Buy and Sell Stock III");
            int[][] pricesArr = { new int[] { 3, 3, 5, 0, 0, 3, 1, 4 }, new int[] { 1, 2, 3, 4, 5 }, new int[] { 7, 6, 4, 3, 1 }, new int[] { 1 }, new int[] { 1, 2, 4, 2, 5, 7, 2, 4, 9, 0 } };
            foreach (var prices in pricesArr)
            {
                prices.Print("PRICES");
                Console.WriteLine($" After completing at max 2 Buy-Sell on above, MaxProfit: {DailyProblem.BestTimeToBuyAndSellStockIII_DivideAndConquer(prices)}");
            }
        }


        public static void DecodeString()
        {
            // https://leetcode.com/problems/decode-string/
            Utility.Print("394. Decode String");
            string[] strArr = { "3[a]2[bc]", "3[a2[c]]", "2[abc]3[cd]ef", "abc3[cd]xyz", "100[leetcode]" };
            foreach (var s in strArr)
                Console.WriteLine($" \'{s}\' \t<= Decocde into => \t\'{DailyProblem.DecodeString(s)}\'");
        }


        public static void KthLargestElementInArray()
        {
            // https://leetcode.com/problems/kth-largest-element-in-an-array/
            Utility.Print("215. Kth Largest Element in an Array");
            int[][] inputArr = { new int[] { 3, 2, 1, 5, 6, 4 }, new int[] { 3, 2, 3, 1, 2, 4, 5, 5, 6 }, new int[] { 7, 6, 5, 4, 3, 2, 1 }, new int[] { 5, 2, 4, 1, 3, 6, 0 } };
            int[] k = { 2, 7, 5, 4 };
            for (int i = 0; i < k.Length; i++)
            {
                inputArr[i].Print("Nums");
                Console.WriteLine($" \'{k[i]}\' largest element in above array is : {DailyProblem.FindKthLargest(inputArr[i], k[i])}");
            }
        }


        public static void AssignCookies()
        {
            // https://leetcode.com/problems/assign-cookies/
            Utility.Print("455. Assign Cookies");
            int[][] childsArr = { new int[] { 1, 2, 3 }, new int[] { 1, 2 } };
            int[][] cookiesArr = { new int[] { 1, 1 }, new int[] { 1, 2, 3 } };
            for(int i=0;i<childsArr.Length;i++)
            {
                childsArr[i].Print("CHILDS");
                cookiesArr[i].Print("COOKIES");
                Console.WriteLine($" No Of Content CHilds are {DailyProblem.FindContentChildren(childsArr[i], cookiesArr[i])}");
            }
        }


        public static void CousinsInBinaryTree()
        {
            // https://leetcode.com/problems/cousins-in-binary-tree/
            Utility.Print("993. Cousins in Binary Tree");
            /* In a binary tree, the root node is at depth 0, and children of each depth k node are at depth k+1.
             * Two nodes of a binary tree are cousins if they have the same depth, but have different parents.
             * We are given the root of a binary tree with unique values, and the values x and y of two different nodes in the tree.
             * Return true if and only if the nodes corresponding to the values x and y are cousins.
             */
            TreeNode root = new TreeNode(1)         // Cousins Not-True Example with 2&3 or 3&4
            {
                left = new TreeNode(2) { left = new TreeNode(4)},
                right = new TreeNode(3)
            };
            TreeNode root1 = new TreeNode(1)        // Cousins True Example with 4&5
            {
                left = new TreeNode(2) { right = new TreeNode(4) },
                right = new TreeNode(3) { right = new TreeNode(5) }
            };
            root1.InOrder();
            Console.WriteLine($" In above tree, NodeX: {4} NodeY: {3} are Cousins(same depth) & not sibiling (diff parent): {DailyProblem.IsCousins(root1, 4, 5)}");
        }


        public static void MinDepthOfBinaryTree()
        {
            // https://leetcode.com/problems/minimum-depth-of-binary-tree/
            Utility.Print("111. Minimum Depth of Binary Tree");
            TreeNode root = new TreeNode(1)         // Cousins Not-True Example with 2&3 or 3&4
            {
                left = new TreeNode(2) { left = new TreeNode(4) },
                right = new TreeNode(3)
            };
            root.InOrder();
            Console.WriteLine($" Minimum Depth of above BinaryTree is: {DailyProblem.MinDepthOfBinaryTree(root)}");
        }


        public static void NumbersAtMostNGivenDigitSet()
        {
            // https://leetcode.com/problems/numbers-at-most-n-given-digit-set/
            Utility.Print("902. Numbers At Most N Given Digit Set");
            string[][] digits = { new string[] { "1", "3", "5", "7" }, new string[] { "7" }, new string[] { "1", "4", "9" } };
            int[] n = { 100, 8, 1000000000 };
            for (int i = 0; i < n.Length; i++)
            {
                digits[i].Print("Digits");
                Console.WriteLine($" No of positive intergers that can be generated that are less than or equal to a given integer n '{n[i]}'" +
                    $" are {DailyProblem.AtMostNGivenDigitSetDP(digits[i], n[i])} no's");
            }
        }


        public static void InsertDeleteGetRandomO1()
        {
            // https://leetcode.com/problems/insert-delete-getrandom-o1/ RandomizedSet
            Utility.Print("380. Insert Delete GetRandom O(1)");
            var randomizedSet = new InsertDeleteGetRandomO1();
            Console.WriteLine(randomizedSet.Insert(1));
            Console.WriteLine(randomizedSet.Remove(2));
            Console.WriteLine(randomizedSet.Insert(2));
            Console.WriteLine(randomizedSet.GetRandom());
            Console.WriteLine(randomizedSet.Remove(1));
            Console.WriteLine(randomizedSet.Insert(2));
            Console.WriteLine(randomizedSet.GetRandom());
        }


        public static void UniqueMorseCodeWords()
        {
            // https://leetcode.com/problems/unique-morse-code-words/
            Utility.Print("804. Unique Morse Code Words");
            string[] words = { "gin", "zen", "gig", "msg" };
            words.Print("Input Words to be transformed");
            Console.WriteLine($" Number of unique MorseCode transformation for the above string of words is: {DailyProblem.UniqueMorseRepresentations(words)}");
        }


        public static void WordLadder()
        {
            // https://leetcode.com/problems/word-ladder/
            Utility.Print("127. Word Ladder");
            string[] beginWord = { "hit", "hot", "talk", "hog" }, endWord = { "cog", "dog", "tail", "cog" };
            List<string>[] wordList = { new List<string>() { "hot", "dot", "dog", "lot", "log", "cog" },
                                        new List<string>() { "hot", "dog" },
                                        new List<string>() { "talk", "tons", "fall", "tail", "gale", "hall", "negs" },
                                        new List<string>() { "cog" } };
            for (int i = 0; i < wordList.Length; i++)
            {
                wordList[i].ToArray().Print("Word-List");
                Console.WriteLine($" Length of shortest transformation sequence from beginWord \'{beginWord[i]}\' to endWord \'{endWord[i]}\'" +
                    $" is: \'{DailyProblem.WordLadder(beginWord[i], endWord[i], wordList[i])}\'");
            }
        }

        public static void HouseRobber()
        {
            // https://leetcode.com/problems/house-robber/
            Utility.Print("198. House Robber");
            int[][] numsArr = { new int[] { 1, 2, 3, 1 }, new int[] { 2, 7, 9, 3, 1 } };
            foreach(var nums in numsArr)
            {
                nums.Print("Nums");
                Console.WriteLine($" maximum amount of money you can rob = {DailyProblem.HouseRobberI(nums)}");
            }
        }

        public static void HouseRobberII()
        {
            // https://leetcode.com/problems/house-robber-ii/
            Utility.Print("213. House Robber II");
            int[][] numsArr = { new int[] { 1, 2, 3, 1 }, new int[] { 2, 3, 2 }, new int[] { 0 } };
            foreach (var nums in numsArr)
            {
                nums.Print("Nums");
                Console.WriteLine($" maximum amount of money you can rob = {DailyProblem.HouseRobberII(nums)}");
            }
        }

        public static void HouseRobberIII()
        {
            // https://leetcode.com/problems/house-robber-iii/
            Utility.Print("337. House Robber III");
            TreeNode[] rootArr = { new TreeNode(3) { left = new TreeNode(2) { right = new TreeNode(3) },
                                                   right = new TreeNode(3) { right = new TreeNode(1) } },
                                 new TreeNode(4) { left = new TreeNode(1) { left = new TreeNode(2) { left = new TreeNode(3) } } } };
            foreach (var root in rootArr)
            {
                root.InOrder();
                Console.WriteLine($" Maximum amount of money the thief can rob = {DailyProblem.HouseRobberIII(root)}");
            }
        }


        public static void SmallestIntegerDivisibleByK()
        {
            // https://leetcode.com/problems/smallest-integer-divisible-by-k/
            Utility.Print("1015. Smallest Integer Divisible by K");
            int[] kArr = { 1, 2, 3, 5 };
            foreach (var k in kArr)
                Console.WriteLine($" Smallest N divisble by K={k} consisting of digit '1' only is of length=\t{DailyProblem.SmallestRepunitDivByK(k)}");
        }


        public static void LongestSubstringWithAtLeastKRepeatingCharacters()
        {
            // https://leetcode.com/problems/longest-substring-with-at-least-k-repeating-characters/solution/
            Utility.Print("395. Longest Substring with At Least K Repeating Characters");
            string[] s = { "aaabb", "ababbc", "aabcbacad" };
            int[] k = { 3, 2, 2 };
            for (int i = 0; i < s.Length; i++)
                Console.WriteLine($" Input: {s[i]} & K: {k[i]}" +
                    $"\n length of the longest substring of s such that the frequency of each character in this substring is greater than or equal to k: {DailyProblem.LongestSubstringWithAtLeastKRepeatingCharacters(s[i], k[i])}");
        }
        

        public static void JumpGame()
        {
            // https://leetcode.com/problems/jump-game/
            Utility.Print("55. Jump Game");
            int[][] numsArr = { new int[] { 2, 3, 1, 1, 4 }, new int[] { 3, 2, 1, 0, 4 }, new int[] { 0 } };
            foreach(var nums in numsArr)
            {
                nums.Print("Nums");
                Console.WriteLine($" Starting from index 0 in above array, are we able to reach last index: {DailyProblem.JumpGame(nums, new int[nums.Length])}");
            }
        }

        public static void JumpGameII()
        {
            // https://leetcode.com/problems/jump-game-ii/
            Utility.Print("45. Jump Game II");
            int[][] numsArr = { new int[] { 2, 3, 1, 1, 4 }, new int[] { 2, 3, 0, 1, 4 }, new int[] { 2, 1 } };
            foreach(var nums in numsArr)
            {
                nums.Print("Input");
                Console.WriteLine($" Minimum number of jumps to reach from 0th to last index \'{DailyProblem.JumpGameII_Greedy(nums)}\'");
            }
        }

        public static void JumpGameIII()
        {
            // https://leetcode.com/problems/jump-game-iii/
            Utility.Print("1306. Jump Game III");
            int[][] nums = { new int[] { 4, 2, 3, 0, 3, 1, 2 }, new int[] { 4, 2, 3, 0, 3, 1, 2 }, new int[] { 3, 0, 2, 1, 2 }, new int[] { 0 } };
            int[] start = { 5, 0, 2, 0 };
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i].Print("Nums");
                Console.WriteLine($" Starting from index {start[i]} in above array, are we able to reach an index with value '0': {DailyProblem.JumpGameIII(nums[i], new int[nums[i].Length], start[i])}");
            }
        }


        public static void JumpGameIV()
        {
            // https://leetcode.com/problems/jump-game-iv/
            Utility.Print("1345. Jump Game IV");
            int[][] inputArr = { new int[] { 100, -23, -23, 404, 100, 23, 23, 23, 3, 404 },
                                    new int[] { 7 },
                                    new int[] { 7, 6, 9, 6, 9, 6, 9, 7 },
                                    new int[] { 6, 1, 9 },
                                    new int[] { 11, 22, 7, 7, 7, 7, 7, 7, 7, 22, 13 } };
            foreach (var arr in inputArr)
            {
                arr.Print("Jump Array");
                Console.WriteLine($" You need min \'{DailyProblem.JumpGameIV(arr)}\' jumps to reach last index from first");
            }
        }


        public static void LinkedListRandomNode()
        {
            // https://leetcode.com/problems/linked-list-random-node/
            Utility.Print("382. Linked List Random Node");
            ListNode head = new ListNode(10) { next = new ListNode(20) { next = new ListNode(30) } };
            LinkedListRandomNode obj = new LinkedListRandomNode(head);
            head.Print();
            for (int i = 0; i < 12; i++)
                Console.WriteLine($" Random Node from above list: {obj.GetRandom()}");
        }


        public static void IncreasingOrderSearchTree()
        {
            // https://leetcode.com/problems/increasing-order-search-tree/
            Utility.Print("897. Increasing Order Search Tree");
            TreeNode root = new TreeNode(2) 
            {
                left = new TreeNode(1),
                right = new TreeNode(4)
                { left = new TreeNode(3) }
            };
            root.InOrder();
            root = DailyProblem.IncreasingOrderSearchTree(root);
            root.InOrder();
        }


        public static void KthFactorOfN()
        {
            // https://leetcode.com/problems/the-kth-factor-of-n/
            Utility.Print("1492. The kth Factor of n");
            int[] n = { 12, 7, 4, 1, 1000 }, k = { 3, 2, 4, 1, 3 };
            for (int i = 0; i < n.Length; i++)
                Console.WriteLine($" For N: \'{n[i]}\' the \'{k[i]}\' Kth factor is => {DailyProblem.KthFactorOfN(n[i], k[i])}");
        }

        public static void SingleNumberII()
        {
            // https://leetcode.com/problems/single-number-ii/
            Utility.Print("137. Single Number II");
            int[][] numsArr = { new int[] { 2, 2, 3, 2 }, new int[] { 0, 1, 0, 1, 0, 1, 99 }, new int[] { 43, 16, 45, 89, 45, -2147483648, 45, 2147483646, -2147483647, -2147483648, 43, 2147483647, -2147483646, -2147483648, 89, -2147483646, 89, -2147483646, -2147483647, 2147483646, -2147483647, 16, 16, 2147483646, 43 } };
            foreach(var nums in numsArr)
            {
                nums.Print("Input");
                Console.WriteLine($" Integer which is repeated just once in above array is: => \'{DailyProblem.SingleNumber(nums)}\'");
                Console.WriteLine();
            }
        }


        public static void LargestNumber()
        {
            // https://leetcode.com/problems/largest-number/
            Utility.Print("179. Largest Number");
            int[][] numsArr = { new int[] { 10, 2 }, new int[] { 3, 30, 34, 5, 9 }, new int[] { 1 }, new int[] { 0, 0 } };
            foreach(var nums in numsArr)
            {
                nums.Print("Input");
                Console.WriteLine($" Largest No formed by re-arrangeing no's from above array is: \'{DailyProblem.LargestNumber(nums)}\'");
                Console.WriteLine();
            }
        }


        public static void NextPermutation()
        {
            // https://leetcode.com/problems/next-permutation/
            Utility.Print("31. Next Permutation");
            int[][] numsArr = { new int[] { 1, 2, 3, }, new int[] { 3, 2, 1 }, new int[] { 1, 3, 2 }, new int[] { 2, 5, 1 }, new int[] { 3, 6, 4, 2 } };
            foreach(var nums in numsArr)
            {
                nums.Print("Input Number");
                DailyProblem.NextPermutation(nums);
                nums.Print("Next Permutation");
                Console.WriteLine();
            }
        }


        public static void SpiralMatrixII()
        {
            // https://leetcode.com/problems/spiral-matrix-ii/
            Utility.Print("59. Spiral Matrix II");
            int[] nArr = { 3, 1, 5 };
            foreach (var n in nArr)
                (DailyProblem.SpiralMatrixII(n)).Print("SPIRAL MATRIX");
        }


        public static void MinStack()
        {
            // https://leetcode.com/problems/min-stack/
            // https://leetcode.com/problems/max-stack/
            Utility.Print("155. Min Stack");
            MinStack minStack = new MinStack();
            minStack.Push(-2);
            minStack.Push(0);
            minStack.Push(-3);
            var v = minStack.GetMin(); // return -3
            minStack.Pop();
            var v2 = minStack.Top();    // return 0
            var v3 = minStack.GetMin(); // return -2

            MinStack minStack2 = new MinStack();
            minStack2.Push(0);
            minStack2.Push(1);
            minStack2.Push(0);
            var w = minStack2.GetMin(); // return -3
            minStack2.Pop();
            var w3 = minStack2.GetMin(); // return -2
        }


        public static void PairsOfSongsWithTotalDurationsDivisibleBy60()
        {
            // https://leetcode.com/problems/pairs-of-songs-with-total-durations-divisible-by-60/
            Utility.Print("1010. Pairs of Songs With Total Durations Divisible by 60");
            int[][] timeArr = { new int[] { 30, 20, 150, 100, 40 }, new int[] { 60, 60, 60 } };
            foreach (var time in timeArr)
            {
                time.Print("Songs");
                Console.WriteLine($" Number of pairs of songs for which their total duration in seconds is divisible by 60: \'{DailyProblem.PairOfSongs(time)}\'\n");
            }
        }


        public static void BullsAndCows()
        {
            // https://leetcode.com/problems/bulls-and-cows/
            Utility.Print("299. Bulls and Cows");
            string[] secret = { "1807", "1123", "1", "1" }, guess = { "7810", "0111", "0", "1" };
            for (int i = 0; i < secret.Length; i++)
                Console.WriteLine($" SECRET {secret[i]}, GUESS {guess[i]}, \tHint in xAyB format is \'{DailyProblem.BullsAndCows(secret[i], guess[i])}\'");
        }


        public static void RemoveDuplicatesFromSortedArrayII()
        {
            // https://leetcode.com/problems/remove-duplicates-from-sorted-array-ii/
            Utility.Print("80. Remove Duplicates from Sorted Array II");
            int[][] numsArr = { new int[] { 1, 1, 1, 2, 2, 3 }, new int[] { 0, 0, 1, 1, 1, 1, 2, 3, 3, } };
            foreach(var nums in numsArr)
            {
                nums.Print("Input Sorted Array");
                var newLen = DailyProblem.RemoveDuplicates(nums);
                Console.Write(" Output 'Sorted Array' (Max 2 duplicate)");
                for (int i = 0; i < newLen; i++)
                    Console.Write($" {nums[i]} ||");
                Console.WriteLine("\n");
            }
        }


        public static void FlipColumnsForMaximumNumberOfEqualRows()
        {
            // https://leetcode.com/problems/flip-columns-for-maximum-number-of-equal-rows/
            Utility.Print("1072. Flip Columns For Maximum Number of Equal Rows");
            int[][] matrix = { new int[] { 0, 1 }, new int[] { 1, 0 } };
            matrix.Print("Input");
            Console.WriteLine($" Maximum number of rows that have all values equal after some number of flips. \'{DailyProblem.MaxEqualRowsAfterFlips(matrix)}\'");
        }


        public static void SubstringWithConcatenationOfAllWords()
        {
            // https://leetcode.com/problems/substring-with-concatenation-of-all-words/
            Utility.Print("30. Substring with Concatenation of All Words");
            string[] s = { "barfoothefoobarman", "wordgoodgoodgoodbestword", "barfoofoobarthefoobarman" };
            string[][] words = { new string[] { "foo", "bar" }, new string[] { "word", "good", "best", "word" }, new string[] { "bar", "foo", "the" } };
            for (int i = 0; i < s.Length; i++)
            {
                words[i].Print("Words");
                Console.Write($" starting indices of substring(s) in s \'{s[i]}\' that is a concatenation of each word in words exactly once, in any order:");
                foreach (var index in DailyProblem.SubstringWithConcatenationOfAllWords(s[i], words[i]))
                    Console.Write($" {index} ||");
                Console.WriteLine("\n");
            }
        }


        public static void LowestCommonAncestorDeepestLeaves()
        {
            // https://leetcode.com/problems/lowest-common-ancestor-of-deepest-leaves/
            // https://leetcode.com/problems/smallest-subtree-with-all-the-deepest-nodes/
            Utility.Print("1123. Lowest Common Ancestor of Deepest Leaves");
            Utility.Print("865. Smallest Subtree with all the Deepest Nodes");
            TreeNode root = new TreeNode(3)
            {
                left = new TreeNode(5)
                {
                    left = new TreeNode(6),
                    right = new TreeNode(2)
                    {
                        left = new TreeNode(7),
                        right = new TreeNode(4)
                    }
                },
                right = new TreeNode(1)
                {
                    left = new TreeNode(0),
                    right = new TreeNode(8)
                }
            };
            root.InOrder();
            Console.WriteLine($" The lowest common ancestor of its deepest leaves is : {DailyProblem.LCADeepestLeaves(root).val} ");
        }


        // Tushar Roy https://youtu.be/IFNibRVgFBo
        public static void BurstBalloons()
        {
            // https://leetcode.com/problems/burst-balloons/
            Utility.Print("312. Burst Balloons");
            int[] nums = { 3, 1, 5, 8 };
            Console.WriteLine($" Max coins collected by bursting the balloons wisely\t\'{DynamicProgramming.BurstBalloons(nums)}\'");
        }


        public static void PalindromePartitioning()
        {
            // https://leetcode.com/problems/palindrome-partitioning/
            Utility.Print("131. Palindrome Partitioning");
            string[] sArr = { "aab", "a" };
            foreach (var s in sArr)
            {
                Console.WriteLine($" For Input String: \'{s}\' below are all possible valid Palindrome Substrings");
                List<IList<string>> finalResult = new List<IList<string>>();
                List<string> localResult = new List<string>();
                //DailyProblem.PalindromePartitioning_Recursive(s, 0, s.Length - 1, finalResult, localResult);
                bool[,] dp = new bool[s.Length, s.Length];
                DailyProblem.PalindromePartitioning_DP(s, 0, s.Length - 1, finalResult, localResult, dp);
                foreach (var eachLen in finalResult)
                {
                    foreach (var palindrome in eachLen)
                        Console.Write($" \'{palindrome}\' ||");
                    Console.WriteLine();
                }
            }
        }


        public static void SquaresSortedArray()
        {
            // https://leetcode.com/problems/squares-of-a-sorted-array/
            Utility.Print("977. Squares of a Sorted Array");
            int[][] numsArr = { new int[] { -4, -1, 0, 3, 10 }, new int[] { -7, -3, 2, 3, 11 } };
            foreach(var nums in numsArr)
            {
                nums.Print("Input");
                int[] squared = DailyProblem.SortedSquares(nums);
                squared.Print("Squared");
            }
        }


        public static void BinaryTreeRightSideView()
        {
            // https://leetcode.com/problems/binary-tree-right-side-view/
            Utility.Print("199. Binary Tree Right Side View");
            TreeNode root = new TreeNode(1)
            {
                left = new TreeNode(2)
                { right = new TreeNode(5) },
                right = new TreeNode(3)
                { right = new TreeNode(4) }
            };
            root.InOrder();
            IList<int> rtView = DailyProblem.BinaryTreeRightSideView(root);
            Console.WriteLine($" Right Side View of Above Tree is:");
            foreach (var rtNode in rtView)
                Console.Write($" {rtNode} ||");
            Console.WriteLine();
        }

        // https://youtu.be/FhEbfSRuNNA
        public static void MinimumCostToHireKWorkers()
        {
            // https://leetcode.com/problems/minimum-cost-to-hire-k-workers/
            Utility.Print("857. Minimum Cost to Hire K Workers");
            int[][] quality = { new int[] { 10, 20, 5 }, new int[] { 3, 1, 10, 10, 1 } };
            int[][] wage = { new int[] { 70, 50, 30 }, new int[] { 4, 8, 2, 2, 7 } };
            int[] k = { 2, 3 };
            for (int i = 0; i < k.Length; i++)
            {
                quality[i].Print("Workers Quality");
                wage[i].Print("Workers Wage");
                Console.WriteLine($" Minimum cost to Hire K\'{k[i]}\' workers is: \'{DailyProblem.MincostToHireWorkers(quality[i], wage[i], k[i])}\'\n");
            }
        }


        public static void IncreasingTripletSubsequence()
        {
            // https://leetcode.com/problems/increasing-triplet-subsequence/
            Utility.Print("334. Increasing Triplet Subsequence");
            int[][] numsArr = { new int[] { 1, 2, 3, 4, 5 }, new int[] { 5, 4, 3, 2, 1 }, new int[] { 2, 1, 5, 0, 4, 6 } };
            foreach(var nums in numsArr)
            {
                nums.Print("Input");
                Console.WriteLine($" There exists a triple of indices (i, j, k) such that i < j < k and nums[i] < nums[j] < nums[k] : {DailyProblem.IncreasingTripletSubsequence(nums)}");
            }
        }



        public static void PermutationInString()
        {
            // https://leetcode.com/problems/permutation-in-string/
            Utility.Print("567. Permutation in String");
            string[] s1 = { "ab", "ab", "hello", "ab", "adc" }, s2 = { "eidbaooo", "eidboaoo", "ooolleoooleh", "eidbaooo", "dcda" };
            for (int i = 0; i < s1.Length; i++)
                Console.WriteLine($" s2 \'{s2[i]}\' contains the permutation of s1 \'{s1[i]}\'" +
                    $"\nIn other words, one of the second string's substring is permutations of the first string:" +
                    $" {StringAlgorithms.PermutationInString(s1[i], s2[i])}\n");
        }



        public static void CherryPickupII()
        {
            // https://leetcode.com/problems/cherry-pickup-ii/
            Utility.Print("1463. Cherry Pickup II");
            int[][][] gridArr = { new int[][] { new int[] { 3, 1, 1 }, new int[] { 2, 5, 1 }, new int[] { 1, 5, 5 }, new int[] { 2, 1, 1 } },
                                new int[][] { new int[] { 1, 0, 0, 0, 0, 0, 1 } ,new int[] { 2, 0, 0, 0, 0, 3, 0 } ,new int[] { 2, 0, 9, 0, 0, 0, 0 } ,new int[] { 0, 3, 0, 5, 4, 0, 0 } ,new int[] { 1, 0, 2, 3, 0, 0, 6 } },
                                new int[][] { new int[] { 1,0,0,3}, new int[] { 0,0,0,3}, new int[] { 0,0,3,3}, new int[] { 9,0,3,3} },
                                new int[][] { new int[] { 1, 1 }, new int[] { 1, 1 } },
                                new int[][] { new int[] { 4, 1, 5, 7, 1 }, new int[] { 6, 0, 4, 6, 4 }, new int[] { 0, 9, 6, 3, 5} } };
            foreach (var grid in gridArr)
            {
                grid.Print("FIELD OF CHERRIES");
                //Console.WriteLine($" Maximum number of cherries collected using both robots: {DynamicProgramming.CherryPickupII(grid, -1, grid[0].Length, 0, grid[0].Length - 1)}\n");
                Console.WriteLine($" Maximum number of cherries collected using both robots: {DynamicProgramming.CherryPickupII_DP(grid, -1, grid[0].Length, 0, grid[0].Length - 1, new Dictionary<string, int>())}\n\n");
            }
        }



        public static void MinOperationsToReduceToZero()
        {
            // https://leetcode.com/problems/minimum-operations-to-reduce-x-to-zero/
            Utility.Print("1658. Minimum Operations to Reduce X to Zero");
            int[][] nums = { new int[] { 1, 1, 4, 2, 3 }, new int[] { 5, 6, 7, 8, 9 }, new int[] { 3, 2, 20, 1, 1, 3 } };
            int[] x = { 2, 4, 10 };
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i].Print("Input");
                bool foundZero = false;
                //int moves = DynamicProgramming.MinOperationsToReduceToZero(nums[i], 0, nums[i].Length - 1, x[i], ref foundZero);
                //int moves = DynamicProgramming.MinOperationsToReduceToZero_DP(nums[i], 0, nums[i].Length - 1, x[i], ref foundZero, new Dictionary<string, int>(nums[i].Length));
                int moves = DynamicProgramming.MinOperations_SlidingWindow(nums[i], x[i]);
                Console.WriteLine($" Minimum number of operations to reduce x \'{x[i]}\' to exactly 0: {moves}");
            }
        }



        public static void DecodedStringAtIndex()
        {
            // 
            Utility.Print("Decoded String at Index");
            string[] S = { "leet2code3", "ha22", "a2345678999999999999999", "n2f7x7bv4l",
                "cpmxv8ewnfk3xxcilcmm68d2ygc88daomywc3imncfjgtwj8nrxjtwhiem5nzqnicxzo248g52y72v3yujqpvqcssrofd99lkovg", "a23",
                "ajx37nyx97niysdrzice4petvcvmcgqn282zicpbx6okybw93vhk782unctdbgmcjmbqn25rorktmu5ig2qn2y4xagtru2nehmsp" };
            int[] K = { 10, 5, 1, 110, 480551547, 6, 976159153 };

            for (int i = 0; i < S.Length; i++)
            {
                //char kth = DailyProblem.DecodedStringAtIndex(S[i], K[i]);
                //char kth = DailyProblem.DecodedStringAtIndex_O1Space_Recursive(S[i], K[i]);
                char kth = DailyProblem.DecodedStringAtIndex_O1Space_Iterative(S[i], K[i]);
                Console.WriteLine($" For encoded string S \'{S[i]}\'\n \'{K[i]}\'th character is:\t\'{kth}\'\n");
            }
        }



        public static void SmallestRangeII()
        {
            // https://leetcode.com/problems/smallest-range-ii/
            Utility.Print("910. Smallest Range II");
            int[][] A = { new int[] { 1 }, new int[] { 0, 10 }, new int[] { 1, 3, 6 }, new int[] { 7, 8, 8 }, new int[] { 4, 8, 2, 7, 2 }, new int[] { 8038, 9111, 5458, 8483, 5052, 9161, 8368, 2094, 8366, 9164, 53, 7222, 9284, 5059, 4375, 2667, 2243, 5329, 3111, 5678, 5958, 815, 6841, 1377, 2752, 8569, 1483, 9191, 4675, 6230, 1169, 9833, 5366, 502, 1591, 5113, 2706, 8515, 3710, 7272, 1596, 5114, 3620, 2911, 8378, 8012, 4586, 9610, 8361, 1646, 2025, 1323, 5176, 1832, 7321, 1900, 1926, 5518, 8801, 679, 3368, 2086, 7486, 575, 9221, 2993, 421, 1202, 1845, 9767, 4533, 1505, 820, 967, 2811, 5603, 574, 6920, 5493, 9490, 9303, 4648, 281, 2947, 4117, 2848, 7395, 930, 1023, 1439, 8045, 5161, 2315, 5705, 7596, 5854, 1835, 6591, 2553, 8628 } };
            int[] K = { 0, 2, 3, 5, 5, 4643 };
            for (int i = 0; i < K.Length; i++)
            {
                A[i].Print("Input");
                //int ans = DailyProblem.SmallestRangeII_Faster(A[i], K[i]);
                int ans = DailyProblem.SmallestRangeII(A[i], K[i]);
                Console.WriteLine($" After choosing either x = -K or x = +K for each number in above array," +
                    $"\n smallest possible difference between the maximum value of B and the minimum value of B is : {ans}\n");
            }

        }


        public static void RotateList()
        {
            // https://leetcode.com/problems/rotate-list/
            Utility.Print("61. Rotate List");
            ListNode ls = new ListNode(1) { next = new ListNode(2) { next = new ListNode(3) { next = new ListNode(4) { next = new ListNode(5) } } } };
            int[] kArr = { 2, 1, 305 };
            
            foreach (var k in kArr)
            {
                ls.Print("Input");
                Console.WriteLine($" List After k\'{k}\' rotations");
                ls = DailyProblem.RotateRight(ls, k);
                ls.Print("Rotated");
                Console.WriteLine();
            }
        }


        public static void AllPossibleFullBinaryTrees()
        {
            // https://leetcode.com/problems/all-possible-full-binary-trees/submissions/
            Utility.Print("894. All Possible Full Binary Trees");
            int[] N = { 0, 1, 5, 8};
            foreach (var n in N)
            {
                Console.WriteLine($"\n Below are no of different FullBinaryTree with N '{n}' total nodes");
                foreach (TreeNode FullBinaryTree in DailyProblem.AllPossibleFBT(n, new Dictionary<int, List<TreeNode>>()))
                    FullBinaryTree.InOrder();
            }
        }


        public static void ConstructStringFromBinaryTree()
        {
            // https://leetcode.com/problems/construct-string-from-binary-tree/
            Utility.Print("606. Construct String from Binary Tree");
            TreeNode root = new TreeNode(1)
            {
                left = new TreeNode(2) { left = new TreeNode(4) },
                right = new TreeNode(3)
            };
            root.InOrder();
            Console.WriteLine($" Construct a string consists of parenthesis and integers from a binary tree" +
                $"\n with the preorder traversal way. => {DailyProblem.Tree2str(root)}");
        }


        public static void FindDuplicateSubtrees()
        {
            // https://leetcode.com/problems/find-duplicate-subtrees/
            Utility.Print("652. Find Duplicate Subtrees");
            TreeNode root = new TreeNode(1)
            {
                left = new TreeNode(2) { left = new TreeNode(4)},
                right = new TreeNode(3)
                {
                    left = new TreeNode(2)
                    { left = new TreeNode(4) },
                    right = new TreeNode(4)
                }
            };
            //var duplicates = DailyProblem.FindDuplicateSubtrees(root, new HashSet<string>(), new Dictionary<string, TreeNode>());
            var duplicates = DailyProblem.FindDuplicateSubtreesFaster(root);
            foreach (TreeNode duplicateSubtree in duplicates)
                duplicateSubtree.InOrder();
        }


        public static void NextGreaterElementI()
        {
            // https://leetcode.com/problems/next-greater-element-i/
            Utility.Print("496. Next Greater Element I");
            int[] nums1 = { 4, 1, 2 };
            int[] nums2 = { 1, 3, 4, 2 };
            nums1.Print("Nums1");
            nums2.Print("Nums2");
            int[] nextHighest = DailyProblem.NextGreaterElementI(nums1, nums2);
            nextHighest.Print("Next Highest for Nums1");
        }


        public static void NextGreaterElementII()
        {
            // https://leetcode.com/problems/next-greater-element-ii/submissions/
            Utility.Print("503. Next Greater Element II");
            int[] nums = { 1, 2, 1 };
            nums.Print("Nums");
            int[] nextHighest = DailyProblem.NextGreaterElementII(nums);
            nextHighest.Print("Next Highest for Nums");
        }


        public static void NextGreaterElementIII()
        {
            // https://leetcode.com/problems/next-greater-element-iii/
            Utility.Print("556. Next Greater Element III");
            int[] nArr = { 12, 21, int.MaxValue, 1332 , 1999999999 };
            foreach (var n in nArr)
                Console.WriteLine($" Smallest integer which has exactly the same digits existing in the integer n \'{n}\'" +
                    $"\n and is greater in value than n is: {DailyProblem.NextGreaterElementIII(n)}\n");
        }


        public static void InsertIntoBST()
        {
            // https://leetcode.com/problems/insert-into-a-binary-search-tree/
            Utility.Print("701. Insert into a Binary Search Tree");
            TreeNode root = new TreeNode(4)
            {
                left = new TreeNode(2)
                {
                    left = new TreeNode(1),
                    right = new TreeNode(3)
                },
                right = new TreeNode(7)
            };
            root.InOrder();
            int val = 5;
            DailyProblem.InsertIntoBST(root, val);
            Console.WriteLine($" BST after inserting \'{val}\'");
            root.InOrder();
        }


        public static void FindDiagonalOrder()
        {
            // https://leetcode.com/problems/diagonal-traverse/
            Utility.Print("498. Diagonal Traverse");
            int[][] matrix = { new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 }, new int[] { 7, 8, 9 } };
            matrix.Print("Input MATRIX");
            int[] diagonalOrder = DailyProblem.FindDiagonalOrder(matrix);
            diagonalOrder.Print("Diagonal Order");
        }


        public static void NodesAtKDistanceFromGivenNodeInBinaryTree()
        {
            // https://leetcode.com/problems/all-nodes-distance-k-in-binary-tree/
            Utility.Print("863. All Nodes Distance K in Binary Tree");
            TreeNode root = new TreeNode(3)
            {
                left = new TreeNode(5)
                {
                    left = new TreeNode(6),
                    right = new TreeNode(2)
                    {
                        left = new TreeNode(7),
                        right = new TreeNode(4)
                    }
                },
                right = new TreeNode(1)
                {
                    left = new TreeNode(0),
                    right = new TreeNode(8)
                }
            };
            root.InOrder("BINARY TREE");
            TreeNode target = root.left;
            int k = 2;
            Console.WriteLine($" Printing all nodes which are kth:{k} distance away Target:{target.val} below:");
            foreach (var kthDistNode in DailyProblem.NodesAtKDistanceFromGivenNodeInBinaryTree(root,target,k))
                Console.Write($" {kthDistNode} ||");
            Console.WriteLine();
        }


        public static void LeafSimilarTrees()
        {
            // https://leetcode.com/problems/leaf-similar-trees/
            Utility.Print("872. Leaf-Similar Trees");
            TreeNode root1 = new TreeNode(3)
            {
                left = new TreeNode(5)
                {
                    left = new TreeNode(6),
                    right = new TreeNode(2)
                    {
                        left = new TreeNode(7),
                        right = new TreeNode(4)
                    }
                },
                right = new TreeNode(1)
                {
                    left = new TreeNode(9),
                    right = new TreeNode(8)
                }
            };
            TreeNode root2 = new TreeNode(3)
            {
                left = new TreeNode(5)
                {
                    left = new TreeNode(6),
                    right = new TreeNode(7)
                },
                right = new TreeNode(1)
                {
                    left = new TreeNode(4),
                    right = new TreeNode(2)
                    {
                        left = new TreeNode(9),
                        right = new TreeNode(8)
                    }
                }
            };
            root1.InOrder("BINARY TREE-1");
            root2.InOrder("BINARY TREE-2");
            Console.WriteLine($" Leaves of above 2 binary trees, from left to right order," +
                $" the values of those leaves form a leaf value sequence: {DailyProblem.LeafSimilar(root1, root2)}");
        }


        public static void RedundantConnection()
        {
            // https://leetcode.com/problems/redundant-connection/
            Utility.Print("684. Redundant Connection");
            int[][][] edgesArr = { new int[][] { new int[] { 1, 2 }, new int[] { 1, 3 }, new int[] { 2, 3 } },
                                   new int[][] { new int[] { 1, 2 }, new int[] { 2, 3 }, new int[] { 3, 4 }, new int[] { 1, 4 }, new int[] { 1, 5 } },
                                   new int[][] { new int[] { 3, 7 }, new int[] { 1, 4 }, new int[] { 2, 8 }, new int[] { 1, 6 }, new int[] { 7, 9 }, new int[] { 6, 10 }, new int[] { 1, 7 }, new int[] { 2, 3 }, new int[] { 8, 9}, new int[] { 5, 9 } },
                                   new int[][] { new int[] { 9, 10 }, new int[] { 5, 8 }, new int[] { 2, 6 }, new int[] { 1, 5 }, new int[] { 3, 8 }, new int[] { 4, 9 }, new int[] { 8, 10 }, new int[] { 4, 10 }, new int[] { 6, 8 }, new int[] { 7, 9 } } };
            foreach (var edges in edgesArr)
            {
                edges.Print("UnDirected Graph Edges");
                int[] redundantEdge = DailyProblem.FindRedundantConnection(edges);
                Console.WriteLine($" Removing Edge: u({redundantEdge[0]})->v({redundantEdge[1]}) will make above Graph Cycle Free\n");
            }
        }


        public static void CPU_OptimizationProblem()
        {
            Utility.Print("CPU Optimization Problem");
            int[][] avgUtil = { new int[] { 30, 5, 4, 8, 19, 89 },
                                new int[] { 5, 10, 88 },
                                new int[] { 59, 81, 28, 83, 91, 24, 43, 10, 6, 75, 92, 57, 12, 3, 20, 63, 99, 32, 51, 96, 63, 29, 25, 5, 55, 95, 25, 45, 52, 88, 54, 82, 30, 30, 32, 80, 92, 44, 64, 4, 7, 48, 53, 19, 86, 66, 48, 64, 49, 84, 55, 44, 21, 2, 11, 49, 46, 43, 88 } };
            int[] instance = { 5, 1, 94 };
            for (int i = 0; i < instance.Length; i++)
                Console.WriteLine($" {DailyProblem.CPU_OptimizationProblem(instance[i], avgUtil[i].ToList())}\n");
        }


        public static void AmazonShoppingProblem()
        {
            Utility.Print("AmazonShoppingProblem:\n Given Count of Products and 2 Array to indicates Connection b/w To-From product,\n Find Min Outer Trio Sum");
            int[] productsFrom = { 2, 3, 5, 1 };
            int[] productTo = { 1, 6, 1, 7 };
            int noOfProducts = 7;
            Console.WriteLine($" Minimum Outer Trio Sum = {DailyProblem.GetMinTrioSum(noOfProducts, productsFrom.ToList(), productTo.ToList())}");
        }


        public static void DecodeWays()
        {
            // https://leetcode.com/problems/decode-ways/
            Utility.Print("91. Decode Ways");
            string[] sArr = { "12", "226", "0", "02", "1", "2264565647168725731756371537107820" };
            foreach (var encodedString in sArr)
                Console.WriteLine($" String \'{encodedString}\' can be decoded in : \'{DailyProblem.NumDecodings(encodedString, new Dictionary<int, int>(encodedString.Length))}\' ways\n");
        }


        public static void ReachANumber()
        {
            // https://leetcode.com/problems/reach-a-number/
            Utility.Print("754. Reach a Number");
            int[] targetArr = { 3, 2, 1, 8 };
            foreach (int target in targetArr) 
                Console.WriteLine($" Min Steps to Reach to Target \'{target}\' is : {DailyProblem.ReachANumber(target)}");
        }


        public static void PseudoPalindromicPaths()
        {
            // https://leetcode.com/problems/pseudo-palindromic-paths-in-a-binary-tree/
            Utility.Print("1457. Pseudo-Palindromic Paths in a Binary Tree");
            TreeNode root = new TreeNode(2)
            {
                left = new TreeNode(3)
                {
                    left = new TreeNode(3),
                    right = new TreeNode(1)
                },
                right = new TreeNode(1)
                {
                    left = new TreeNode(1)
                }
            };
            TreeNode root2 = new TreeNode(2)
            {
                left = new TreeNode(1)
                {
                    left = new TreeNode(1),
                    right = new TreeNode(3)
                    { right = new TreeNode(1) }
                },
                right = new TreeNode(1)
            };
            root.InOrder();
            Console.WriteLine($" Number of pseudo-palindromic paths going from the root node to leaf nodes are : \'{DailyProblem.PseudoPalindromicPaths(root, new HashSet<int>(9))}\'");
        }


        public static void GameOfLife()
        {
            // https://leetcode.com/problems/game-of-life/
            Utility.Print("289. Game of Life");
            int[][] board = {   new int[] { 0, 1, 0 },
                                new int[] { 0, 0, 1 },
                                new int[] { 1, 1, 1 },
                                new int[] { 0, 0, 0 } };
            board.Print("Initial State");
            DailyProblem.GameOfLife(board);
            board.Print("Next State");
        }


        public static void ReOrderLinkedList()
        {
            // https://leetcode.com/problems/reorder-list/
            Utility.Print("143. Reorder List");
            ListNode head = new ListNode(1) { next = new ListNode(2) { next = new ListNode(3) { next = new ListNode(4) } } };
            head.Print("ORIGINAL LINKED LIST");
            DailyProblem.ReOrderList(head);
            head.Print("RE-ORDERED LINKED LIST");
        }


        public static void RemoveDuplicatesFromSortedListI()
        {
            // https://leetcode.com/problems/remove-duplicates-from-sorted-list/
            Utility.Print("83. Remove Duplicates from Sorted List");
            ListNode head = new ListNode(1) { next = new ListNode(2) { next = new ListNode(3) { next = new ListNode(3) { next = new ListNode(4) { next = new ListNode(4) { next = new ListNode(5) } } } } } };
            head.Print("ORIGINAL LINKED LIST");
            DailyProblem.DeleteDuplicatesI(head);
            head.Print("AFTER REMOVING DUPLICATES");
        }


        public static void RemoveDuplicatesFromSortedListII()
        {
            // https://leetcode.com/problems/remove-duplicates-from-sorted-list-ii/
            Utility.Print("82. Remove Duplicates from Sorted List II");
            ListNode head = new ListNode(1) { next = new ListNode(2) { next = new ListNode(3) { next = new ListNode(3) { next = new ListNode(4) { next = new ListNode(4) { next = new ListNode(5) } } } } } };
            head.Print("ORIGINAL LINKED LIST");
            DailyProblem.DeleteDuplicatesII(head);
            head.Print("AFTER REMOVING DUPLICATES");
        }


        public static void LinkedListiNBinaryTree()
        {
            // https://leetcode.com/problems/linked-list-in-binary-tree/
            Utility.Print("1367. Linked List in Binary Tree");
            ListNode head = new ListNode(4) { next = new ListNode(2) { next = new ListNode(8) } };
            TreeNode root = new TreeNode(1)
            {
                left = new TreeNode(4)
                {
                    right = new TreeNode(2)
                    { left = new TreeNode(1) }
                },
                right = new TreeNode(4)
                {
                    left = new TreeNode(2)
                    {
                        left = new TreeNode(6),
                        right = new TreeNode(8)
                        {
                            left = new TreeNode(1),
                            right = new TreeNode(3)
                        }
                    }
                }
            };
            head.Print("LinkedList");
            root.InOrder("Binary-Tree");
            Console.WriteLine($" All the elements in the linked list starting from the head correspond " +
                $"to some downward path connected in the binary tree : \'{DailyProblem.IsSubPath(head, root)}\'");
        }


        public static void MergeInBetweenLinkedLists()
        {
            // https://leetcode.com/problems/merge-in-between-linked-lists/
            Utility.Print("1669. Merge In Between Linked Lists");
            ListNode h1 = new ListNode(0) { next = new ListNode(1) { next = new ListNode(2) } };
            ListNode h2 = new ListNode(1000000) { next = new ListNode(1000001) { next = new ListNode(1000002) } };
            int insertFrom = 1, insertTo = 1;
            h1.Print("List1");
            h2.Print("List2");
            DailyProblem.MergeInBetween(h1, insertFrom, insertTo, h2);
            h1.Print($"List1-After Inserting List2, Starting from {insertFrom}'th index upto {insertTo}'th index ");
        }


        public static void FindACorrespondingNodeOfABinaryTreeInACloneOfThatTree()
        {
            // https://leetcode.com/problems/find-a-corresponding-node-of-a-binary-tree-in-a-clone-of-that-tree/
            Utility.Print("1379. Find a Corresponding Node of a Binary Tree in a Clone of That Tree");
            TreeNode original = new TreeNode(7)
            {
                left = new TreeNode(4),
                right = new TreeNode(3)
                {
                    left = new TreeNode(6),
                    right = new TreeNode(19)
                }
            };
            TreeNode clone = new TreeNode(7)
            {
                left = new TreeNode(4),
                right = new TreeNode(3)
                {
                    left = new TreeNode(6),
                    right = new TreeNode(19)
                }
            };
            original.InOrder("Original");
            clone.InOrder("Clone");
            Console.WriteLine($" {(DailyProblem.GetTargetCopy(original, clone, original.left)).val} is the reference to the same node from the cloned tree as in Target {original.left.val}");
        }


        public static void PartitionList()
        {
            // https://leetcode.com/problems/partition-list/
            Utility.Print("86. Partition List");
            ListNode head = new ListNode(1) { next = new ListNode(4) { next = new ListNode(3) { next = new ListNode(2) { next = new ListNode(5) { next = new ListNode(2) } } } } };
            head.Print("LinkedList");
            DailyProblem.PartitionList(head, 3);
            head.Print($"LinkedList After Paritiion as per val {3}");
        }


        public static void BeautifulArrangement()
        {
            // https://leetcode.com/problems/beautiful-arrangement/
            Utility.Print("526. Beautiful Arrangement");
            int[] nArr = { 2, 1, 3, 15 };
            foreach (int n in nArr)
                Console.WriteLine($" No of the beautiful arrangements that you can construct from n={n} is {DailyProblem.BeautifulArrangement(n)} ");
        }


        public static void MakeTheStringGreat()
        {
            // https://leetcode.com/problems/make-the-string-great/
            Utility.Print("1544. Make The String Great");
            string[] sArr = { "leEeetcode", "abBAcC", "s", "" };
            foreach (var s in sArr)
                Console.WriteLine($" String \'{s}\' after making it good: \'{DailyProblem.MakeTheStringGreat(s)}\'");
        }


        public static void RemoveOutermostParentheses()
        {
            // https://leetcode.com/problems/remove-outermost-parentheses/
            Utility.Print("1021. Remove Outermost Parentheses");
            string[] sArr = { "(()())(())", "(()())(())(()(()))", "()()", "" };
            foreach (var s in sArr)
                Console.WriteLine($" \'{s}\' after removing the outermost parentheses: \'{DailyProblem.RemoveOutermostParentheses(s)}\'");
        }


        public static void DailyTemperatures()
        {
            // https://leetcode.com/problems/daily-temperatures/
            Utility.Print("739. Daily Temperatures");
            int[] T = { 73, 74, 75, 71, 69, 72, 76, 73 };
            T.Print("Daily Temperatures");
            DailyProblem.DailyTemperatures(T).Print("Next HIgher Temperatures");
        }


        public static void SortCharactersByFrequency()
        {
            // https://leetcode.com/problems/sort-characters-by-frequency/
            Utility.Print("451. Sort Characters By Frequency");
            string[] sArr = { "tree", "cccaaa", "Aabb" };
            foreach (var s in sArr)
                Console.WriteLine($" Sorting string \'{s}\' in decreasing order based on the frequency of characters. \'{DailyProblem.FrequencySort(s)}\'");
        }


        public static void SortArrayByIncreasingFrequency()
        {
            // https://leetcode.com/problems/sort-array-by-increasing-frequency/
            Utility.Print("1636. Sort Array by Increasing Frequency");
            int[][] numsArr = { new int[] { 1, 1, 2, 2, 2, 3 }, new int[] { 2, 3, 1, 3, 2 }, new int[] { -1, 1, -6, 4, 5, -6, 1, 4, 1 } };
            foreach (var nums in numsArr)
            {
                nums.Print("Nums");
                DailyProblem.SortArrayByIncreasingFrequency(nums).Print("Frequency-Value-Sorted");
            }
        }


        public static void DetectCyclesIn2DGrid()
        {
            // https://leetcode.com/problems/detect-cycles-in-2d-grid/
            Utility.Print("1559. Detect Cycles in 2D Grid");
            char[][][] gridArr = { new char[][] { new char[] { 'a', 'a', 'a', 'a' }, new char[] { 'a', 'b', 'b', 'a' }, new char[] { 'a', 'b', 'b', 'a' }, new char[] { 'a', 'a', 'a', 'a' } },
                                new char[][] { new char[] {'a','b','b'}, new char[] {'b','z','b'}, new char[] {'b','b','a'} } };
            foreach (var grid in gridArr)
            {
                grid.Print("2D array of characters");
                Console.WriteLine($" The statement 'any cycle of the same value exists in grid in 4-directions' is {DailyProblem.DetectCyclesIn2DGrid(grid)}\n\n");
            }
        }


        public static void SmallestStringStartingFromLeaf()
        {
            // https://leetcode.com/problems/sum-root-to-leaf-numbers/
            Utility.Print("988. Smallest String Starting From Leaf");
            TreeNode root = new TreeNode(0)
            {
                left = new TreeNode(1)
                {
                    left = new TreeNode(3),
                    right = new TreeNode(4)
                },
                right = new TreeNode(2)
                {
                    left = new TreeNode(5),
                    right = new TreeNode(6)
                }
            };
            TreeNode root2 = new TreeNode(0) { right = new TreeNode(1) };
            root.InOrder("Input");
            Console.WriteLine($" lexicographically smallest string that starts at a leaf of this tree and ends at the root \'{DailyProblem.SmallestStringStartingFromLeaf(root)}\'");
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
            StringAlgorithms.MinWindowContainingAllCharacters(input, chArray);
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
            StringAlgorithms.ReplaceSpaceWithGivenChars(input, replaceWith);
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
            while (index >= 0)
            {
                if (i < 0)
                    a[index] = b[j--];
                else if (j < 0)
                    a[index] = a[i--];
                else if (a[i] > b[j])
                    a[index] = a[i--];
                else if (a[i] <= b[j])
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
            // https://leetcode.com/problems/search-in-rotated-sorted-array/
            // https://leetcode.com/problems/search-in-rotated-sorted-array-ii/
            Utility.Print("Problem-40  Given a sorted array of n integers that has been rotated an unknown number of times," +
                " give a O(logn) algorithm that finds an element in the array.(pp. 579 - 580)");
            int[][] input = { new int[] { 15, 16, 19, 20, 25, 1, 3, 4, 5, 7, 10, 14 }, new int[] { 4, 5, 6, 7, 0, 1, 2 }, new int[] { 4, 5, 6, 7, 0, 1, 2 }, new int[] { 1 } };
            int[] searchFor = { 5, 0, 3, 0 };
            for (int i = 0; i < input.Length; i++)
            {
                input[i].Print("Input : ");
                //var index = SearchAlgorithms.BinarySearchInRotatedArray(input[i], searchFor[i]);
                // Recursive
                //var index = SearchAlgorithms.BinarySearchInRotatedArraySinglePass(input[i], 0, input[i].Length - 1, searchFor[i]);
                // Iterative
                var index = SearchAlgorithms.BinarySearchInRotatedArraySinglePassIterative(input[i], 0, input[i].Length - 1, searchFor[i]);
                if (index != -1)
                    Console.WriteLine($" Element \t\'{searchFor[i]}\' found at index : \t{index}");
                else
                    Console.WriteLine($" Element \'{searchFor[i]}\' Not Found in array");
            }
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
            int[] a1 = { 1, 12, 16, 26, 38, 45 };
            int[] a2 = { 2, 13, 17, 30, 45, 65 };
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
            // https://leetcode.com/problems/the-skyline-problem/
            Utility.Print("218. The Skyline Problem");
            Utility.Print("The Skyline Problem using Divide and Conquer algorithm. " +
                "Given n rectangular buildings in a 2 - dimensional city, computes the skyline of these buildings, eliminating hidden lines.");
            //int[,] { { 1, 14, 7 }, { 3, 9, 10 }, { 5, 17, 12 }, { 14, 11, 18 }, { 15, 6, 27 }, { 20, 19, 22 }, { 23, 15, 30 }, { 26, 14, 29 } };
            int[][,] input = { new int[,] { { 1, 14, 7 }, { 3, 9, 10 }, { 5, 17, 12 }, { 14, 11, 18 }, { 15, 6, 27 }, { 20, 19, 22 }, { 23, 15, 30 }, { 26, 14, 29 } },
                                new int[,] { { 1, 11, 5 }, { 2, 6, 7 }, { 3, 13, 9 }, { 12, 7, 16 }, { 14, 3, 25 }, { 19, 18, 22 }, { 23, 13, 29 }, { 24, 4, 28 } },
                                new int[,] { { 1, 1, 2 }, { 1, 2, 2 }, { 1, 3, 2 } },
                                new int[,] { { 2, 7, 4 }, {2, 5, 4}, {2, 6, 4} },
                                new int[,] { { 3, 8, 7}, {3, 7, 8 }, { 3, 6, 9 }, { 3, 5, 10 }, { 3, 4, 11 }, { 3, 3, 12 }, { 3, 2, 13}, { 3, 1, 14} } };
            for (int j = 0; j < input.Length; j++)
            {
                Building[] buildArr = new Building[input[j].GetLength(0)];

                for (int i = 0; i < input[j].GetLength(0); i++)
                    buildArr[i] = new Building(input[j][i, 0], input[j][i, 1], input[j][i, 2]);
                DivideAndConquerAlgorithms.PrintBuildings(buildArr);

                var skyLine = DivideAndConquerAlgorithms.GetSkyLine(buildArr, 0, buildArr.Length - 1);              // O(nlogn)
                DivideAndConquerAlgorithms.PrintSkyLine(skyLine);

                var skyLineSlow = DivideAndConquerAlgorithms.BruteForceSkyLine(buildArr, 0, buildArr.Length - 1);   // O(n^2)
            }
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
                Console.WriteLine($" Answer for number {num} is : {DynamicProgramming.RecurrenceToCodeUsingDPEfficient(num, tab)}");
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
            // https://leetcode.com/problems/partition-equal-subset-sum/
            Utility.Print("416. Partition Equal Subset Sum");
            Utility.Print("Problem-28  Partition problem is to determine whether a given set can be partitioned into two subsets such that the sum of elements in both subsets is the same(p. 800)");
            int[][] inputArr = { new int[] { 1, 5, 11, 5 }, new int[] { 1, 2, 5 }, new int[] { 1, 1 } };
            foreach (var input in inputArr)
            {
                input.Print("Input Array");
                Console.WriteLine($" Above array can be paritioned into two subsets with equal total-sum : {DynamicProgramming.FindPartition(input, input.Length)}");
            }

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

            string[] aArr = { "abcdef", "harsh", "" };
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

            foreach (var input in inputs)
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
            // https://leetcode.com/problems/longest-palindromic-substring/
            Utility.Print("5. Longest Palindromic Substring || Problem-36  Longest Palindromic Substring (p. 814)");
            string[] inputArr = { "babad", "AAAABBAA", "AGDDDA", "AGCTCBMAACTGGAM", "GEEKSFORGEEKS", "ABAXAABAXABYBAXABYB" };
            foreach (var input in inputArr)
                DynamicProgramming.LongestPalindromicSubString(input);                    // DP Tabulation
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
            int[][] inputArr = { new int[] { 1, 3, 5, 8, 9, 2, 6, 7, 6, 8, 9 }, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } };
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
