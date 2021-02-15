using System;
using System.Collections.Generic;

namespace InterviewProblemNSolutions
{
    // https://leetcode.com/problems/check-if-array-pairs-are-divisible-by-k/
    public class TwoSum
    {
        readonly Dictionary<int, int> ht;
        /** Initialize your data structure here. */
        public TwoSum() => ht = new Dictionary<int, int>();

        /** Add the number to an internal data structure.. */
        // Time O(1)
        public void Add(int num)
        {
            Console.WriteLine($" Adding \'{num}\' to TwoSum datastructure");
            if (!ht.ContainsKey(num)) ht.Add(num, 1);
            else ht[num]++;
        }

        /** Find if there exists any pair of numbers which sum is equal to the value. */
        // Time O(n), n = no of nums in dictionary
        public bool Find(int value)
        {
            Console.Write($" Looking for pair which sums up to: \'{value}\' ");
            // case 1: value = 4 and we have two 2's in hashtable
            if (value % 2 == 0 && ht.ContainsKey(value / 2) && ht[value / 2] > 1)
                return true;
            // case 2: value = 0 and we have two 0's in hashtable
            if (value == 0 && ht.ContainsKey(0) && ht[0] > 1)
                return true;
            // case 3: all other scenario's
            // just make sure we are not returning false positive when value being searched is 0 and num is also 0 and its present just once,
            // if 0 is present twice or more its covered in case 2
            foreach (var num in ht.Keys)
                if (ht.ContainsKey(value - num) && value - num != num)
                    return true;  // (value-b) i.e. a exists

            return false;
        }
    }
}
