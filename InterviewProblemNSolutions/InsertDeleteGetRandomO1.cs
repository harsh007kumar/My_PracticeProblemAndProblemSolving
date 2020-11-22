using System;
using System.Collections.Generic;

namespace InterviewProblemNSolutions
{
    // RandomizedSet
    public class InsertDeleteGetRandomO1
    {
        // to hold value and return values for getRandom call
        List<int> ls = null;
        // to hold the value and its index in list/array
        Dictionary<int, int> dict;
        // Random no generator
        Random rand = new Random();

        /** Initialize your data structure here. */
        public InsertDeleteGetRandomO1()        // Constructor
        {
            ls = new List<int>();
            dict = new Dictionary<int, int>();
        }

        /** Inserts a value to the set. Returns true if the set did not already contain the specified element. */
        public bool Insert(int val)
        {
            if (dict.ContainsKey(val)) return false;
            ls.Add(val);
            dict.Add(val, ls.Count - 1);
            return true;
        }

        /** Removes a value from the set. Returns true if the set contained the specified element. */
        public bool Remove(int val)
        {
            if (!dict.ContainsKey(val)) return false;
            // get the position at which element is present in list/array
            int pos = dict[val];
            dict.Remove(val);

            // 'pos' is the not the last value in list/array
            if (pos != ls.Count - 1)
            {
                // replace this value with value at last index
                ls[pos] = ls[ls.Count - 1];
                // update the index of last value element in dictionary
                dict[ls[pos]] = pos;
            }
            // remove duplicate element from the end
            ls.RemoveAt(ls.Count - 1);

            return true;
        }

        /** Get a random element from the set. */
        public int GetRandom() => ls[rand.Next(ls.Count)];  // MaxValue is excluded
    }
}
