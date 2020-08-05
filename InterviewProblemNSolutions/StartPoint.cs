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
            MinimumStepsToMinimizeToOne();      // Learn Dynamic Programming (Memoization & Tabulation)
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
            Utility.Print("MinimumStepsToMinimize_NoTo1 || Dynamic Programming (Memoization & Tabulation)");
            int[] numArray = { 6, 5, 4, 10, 15, 25 };
            foreach (var num in numArray)
                Console.WriteLine($"\n Minmum Steps Requied to minimize '{num}' to '1' :\t{DynamicProgramming.GetMinSteps_Recursive(num)}");
        }
    }
}
