using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblemNSolutions
{
    public static class DynamicProgramming
    {
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
    }
}
