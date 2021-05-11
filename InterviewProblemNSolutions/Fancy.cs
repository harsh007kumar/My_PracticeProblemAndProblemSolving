using System;
using System.Collections.Generic;

namespace InterviewProblemNSolutions
{
    public class Fancy
    {
        List<long> ls;
        List<Pair<int, List<int>>> opAtIdx;
        List<int> opTillNow;
        int lastUpdatedIdx = -1, mod = 1000000007;
        public Fancy()
        {
            ls = new List<long>();                      // to store values
            opAtIdx = new List<Pair<int, List<int>>>(); // to store operation to be performed to all values till current index
            opTillNow = new List<int>();                // to store operation which have been performed till now
        }

        public void Append(int val)
        {
            ls.Add(val);            // add value
            lastUpdatedIdx++;       // increament the idx
        }

        // we using -ve to indicate value need to be added
        public void AddAll(int inc) => UpdateOperationsList(-inc);

        // we using +ve to indicate value need to be multiplyed
        public void MultAll(int m) => UpdateOperationsList(m);

        private void UpdateOperationsList(int operation)
        {
            if (opAtIdx.Count == 0 || opAtIdx[opAtIdx.Count - 1].key < lastUpdatedIdx)
                opAtIdx.Add(new Pair<int, List<int>>(lastUpdatedIdx, new List<int>()));// add new list of set of operation for curr index
            
            opAtIdx[opAtIdx.Count - 1].val.Add(operation);                          // add operation to last added index
        }

        public int GetIndex(int idx)
        {
            if (idx >= ls.Count) return -1;
            long computed = ls[idx];
            int operationsAfterIdx = BinarySearchOperationIndex(idx);

            if (operationsAfterIdx != -1)
                while (operationsAfterIdx < opAtIdx.Count)
                {
                    // perform all operation added at current index
                    foreach (var op in opAtIdx[operationsAfterIdx].val)
                    {
                        if (op < 0)
                            computed += -op;        // since we changes value to -ve for additions
                        else
                            computed *= op;         // since we kept multipication values +ve

                        computed %= mod;
                    }
                    operationsAfterIdx++;   // move to next index to add its operations as well
                }
            Console.WriteLine($" Computed Value at '{idx}' is: '{computed}'");
            return (int)computed;
        }

        private int BinarySearchOperationIndex(int target)
        {
            int start = 0, last = opAtIdx.Count - 1, closetLargerIdx = opAtIdx.Count;
            while (start <= last)   // aim to find the idx which is equal to target or closed larger idx to target
            {
                int mid = start + (last - start) / 2;
                if (opAtIdx[mid].key >= target)
                {
                    last = mid - 1;
                    closetLargerIdx = mid;
                }
                else
                    start = mid + 1;
            }
            return closetLargerIdx;
        }
    }

    /**
     * Your Fancy object will be instantiated and called as such:
     * Fancy obj = new Fancy();
     * obj.Append(val);
     * obj.AddAll(inc);
     * obj.MultAll(m);
     * int param_4 = obj.GetIndex(idx);
     */

    public class Fancy_Faster
    {
        List<int> ls;
        List<long> sum;
        List<long> mul;
        int mod = 1000000007;
        public Fancy_Faster()
        {
            ls = new List<int>();           // to store values
            sum = new List<long>() { 0 };   // to store cumulative additions
            mul = new List<long>() { 1 };   // to store cumulative multiplications
        }

        public void Append(int val)
        {
            ls.Add(val);                    // add value
            // update last values for curr index
            sum.Add(sum[sum.Count - 1]);
            mul.Add(mul[mul.Count - 1]);
        }

        public void AddAll(int incBy) => sum[sum.Count - 1] = (sum[sum.Count - 1] + incBy) % mod;

        public void MultAll(int m)
        {
            sum[sum.Count - 1] = (sum[sum.Count - 1] * m) % mod;
            mul[mul.Count - 1] = (mul[mul.Count - 1] * m) % mod;
        }

        public int GetIndex(int idx)
        {
            if (idx >= ls.Count) return -1;

            //var multiplicationFactor = mul[mul.Count - 1] / mul[idx];
            //var cumulativeSum = sum[sum.Count - 1] - (sum[idx] * multiplicationFactor) % mod;
            //var computed = ((ls[idx] * multiplicationFactor) % mod + cumulativeSum) % mod;

            // ** -- https://leetcode.com/problems/fancy-sequence/discuss/900010/C-O(1)-Solution -- ** //
            //Given Fermat Little Theorem 
            //   1 ≡ a^(m-1) (mod m) 
            //=> a^-1 ≡ a^(m-2) (mod m)
            //Let a = mul[idx], m = mod97
            //So mul.Last()/mul[idx] 
            //=> mul.Last()*mul[idx]^-1 
            //=> mul.Last()*mul[idx]^(mod-2) 
            //=> mul.Last() * PowMod(mul[idx], mod97 - 2, mod97)
            long multiplicationFactor = mul[mul.Count - 1] * PowMod(mul[idx], mod - 2, mod) % mod;
            long cumulativeSum = sum[sum.Count - 1] + mod - sum[idx] * multiplicationFactor % mod;
            long computed = (ls[idx] * multiplicationFactor + cumulativeSum) % mod;

            Console.WriteLine($" Computed Value at '{idx}' is: '{computed}'");
            return (int)computed;
        }

        private long PowMod(long x, long y, long mod)
        {
            long res = 1;
            while (y > 0)
            {
                if ((y & 1) == 1)
                    res = res * x % mod;
                x = x * x % mod;
                y >>= 1;
            }

            return res;
        }
    }
}
