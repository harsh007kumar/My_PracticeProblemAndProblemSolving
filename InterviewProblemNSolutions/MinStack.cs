using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblemNSolutions
{
    public class MinStack
    {
        Stack<int> st, minSt;
        /** initialize your data structure here. */
        public MinStack()   // Constructor
        {
            minSt = new Stack<int>();
            st = new Stack<int>();
        }

        // Push element x onto stack.
        public void Push(int x)
        {
            st.Push(x);

            if (minSt.Count == 0) minSt.Push(x);
            else minSt.Push(Math.Min(x, minSt.Peek()));
        }
        // Removes the element on top of the stack.
        public void Pop()
        {
            st.Pop();
            minSt.Pop();
        }
        // Get the top element.
        public int Top() => st.Peek();
        // Retrieve the minimum element in the stack.
        public int GetMin() => minSt.Peek();
    }
}
