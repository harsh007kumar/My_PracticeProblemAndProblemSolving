using System.Collections.Generic;

namespace InterviewProblemNSolutions
{

    // This is the interface that allows for creating nested lists.
    // You should not implement it, or speculate about its implementation
    public interface NestedInteger
    {
        // @return true if this NestedInteger holds a single integer, rather than a nested list.
        bool IsInteger();

        // @return the single integer that this NestedInteger holds, if it holds a single integer
        // Return null if this NestedInteger holds a nested list
        int GetInteger();

        // @return the nested list that this NestedInteger holds, if it holds a nested list
        // Return null if this NestedInteger holds a single integer
        IList<NestedInteger> GetList();
    }

    // https://leetcode.com/problems/flatten-nested-list-iterator/
    // 341. Flatten Nested List Iterator

    public class NestedIterator
    {

        Stack<Pair<IList<NestedInteger>, int>> st;
        public NestedIterator(IList<NestedInteger> nestedList)
        {
            st = new Stack<Pair<IList<NestedInteger>, int>>();
            st.Push(new Pair<IList<NestedInteger>, int>(nestedList, 0));
        }

        public bool HasNext()
        {
            while (st.Count > 0)
            {
                var curr = st.Peek();
                // keep looking until we find atleast one integer
                if (curr.key[curr.val].IsInteger())
                    return true;
                // if current list current index is also 'nestedList' push new list in Stack with starting idx '0'
                else
                {
                    var newList = curr.key[curr.val++].GetList();
                    st.Push(new Pair<IList<NestedInteger>, int>(newList, 0));
                    Clean();
                }
            }
            return false;
        }
        
        public int Next()
        {
            var curr = st.Peek();
            int ans = curr.key[curr.val++].GetInteger();
            Clean();
            return ans;
        }

        void Clean()
        {
            while (st.Count > 0)
            {
                var top = st.Peek();
                // if we have reached end of current list, pop list from stack
                if (top.key == null || top.val >= top.key.Count)
                    st.Pop();
                else break;
            }
        }
    }
}
