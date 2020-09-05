using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblemNSolutions
{
    public static class DynamicProgramming
    {
        static int[] minSteps = new int[100];

        // Ques. https://youtu.be/f2xi3c1S95M

        /// <summary>
        /// Recursive way to solve the given problem
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public static int GetMinSteps_Recursive(int no)
        {
            Console.Write($" {no}");
            if (no == 1) return 0;                      // 0 steps required to minimize 1 to 1
            else if (no == 2) return 1;                 // no '2' divide by 2 once give us 1, hence 1 step
            else if (no == 3) return 1;                 // no '3' divide by 3 once give us 1, hence 1 step
            else if (no % 3 == 0) return 1 + GetMinSteps_Recursive(no / 3);   // if 'no' is divisible by 3 || return 1 step + recursive call : no/3
            else if (no % 2 == 0) return 1 + GetMinSteps_Recursive(no / 2);   // if 'no' is divisible by 2 || return 1 step + recursive call : no/2
            return 1 + GetMinSteps_Recursive(no - 1);     // none of above feasible simply Minimize no - 1 || return 1 step + recursive call : no - 1
        }

        // Memoization (top-down)approach
        public static int GetMinSteps_DP_Memo(int no, ref int[] memo)
        {
            if (no == 1 || memo[no] != 0) return memo[no];  // if no = 1 return 0 or if we found value for no in our memo array no calculation needed return the value

            if (no % 3 == 0)
                memo[no] = 1 + GetMinSteps_DP_Memo(no / 3, ref memo);
            else if (no % 2 == 0)
                memo[no] = 1 + GetMinSteps_DP_Memo(no / 2, ref memo);
            else
                memo[no] = 1 + GetMinSteps_DP_Memo(no - 1, ref memo);
            return memo[no];
        }

        // Tabulation (bottom-up) approach
        public static int[] GetMinSteps_DP_Tab(int no)
        {
            var tab = new int[no];
            for (int i = 0; i < no; i++)                    // Initiliaze array to intMax
                tab[i] = int.MaxValue;
            tab[1] = 0;                                     // update step required for num 1 as 0 (base condition)

            // we start updating value from 1..2..3..4..5....no in tab array so we can utilize it later
            for (int i = 2; i < no; i++)
            {
                if (i % 3 == 0)
                    tab[i] = 1 + tab[i / 3];
                else if (i % 2 == 0)
                    tab[i] = 1 + tab[i / 2];
                else
                    tab[i] = 1 + tab[i - 1];
            }
            return tab;
        }

        public static int FibonnaciRecursive(int num)
        {
            if (num == 0) return 0;
            if (num == 1) return 1;
            return FibonnaciRecursive(num - 1) + FibonnaciRecursive(num - 2);
        }

        public static int Fibonnaci(int num)
        {
            if (num < 1) return 0;
            int fib0 = 0, fib1 = 1;
            int result = 1;
            while(num>1)
            {
                result = fib0 + fib1;
                fib0 = fib1;
                fib1 = result;
                num--;
            }
            return result;
        }

        public static long FactorialRecursive(long num)
        {
            if (num < 2) return 1;
            return num * FactorialRecursive(num - 1);
        }

        public static long Factorial(long num)
        {
            if (num < 2) return 1;              // base case for num is 0 or 1
            long lastFact = 2, currNum = 2, result = 2;

            while (num > currNum++)             // while currNum is not equal to input num keep on calculating factorial
            {
                result = currNum * lastFact;
                lastFact = result;
            }
            return result;
        }
    }
}
