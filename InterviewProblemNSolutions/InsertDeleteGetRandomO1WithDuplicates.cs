using System;
using System.Collections.Generic;

namespace InterviewProblemNSolutions
{
    public class InsertDeleteGetRandomO1WithDuplicates
    {
        // total no of elements present in collection
        int count;
        // to hold value and return values for getRandom call
        readonly List<int> nums;
        // to hold the value and its index+freq in list
        readonly Dictionary<int, int[]> numIdxFreq;
        // Random no generator
        readonly Random rand;
        // stores the prefixFreqSum
        int[] freqPrefixSum;

        /* ALGO
        1. All unique elements are added to list
        2. and their index is saved in list along with freq as value in Dictionary/hashtable with num as the key
        3. Insert() is simple just increment freq in dict if already present else add in list and add new entry in dict with idx it was added in list and freq=1
        4. Remove() is also straight if num present in dictionary than just reduce the freq by -1, if freq goes <=0 than replace the last num in list at current idx and update last num record in dict,
            now simple remove the record for the original num in dict and reduce list length by 1
        5. GetRandom() this is intreseting we need to give equal wt hence need to consider freq of all numbers,
            hence create a prefixSum array which contains the actual range/position of each number in original list by counting freq
            ex:     10, 5, 101, 3456
            freq     1, 10, 21, 1000
            prefix   0, 10, 31, 1031
            now whatever nth num random gives b/w 0..1031 we can quickly find appropriate idx in prefix and use that idx to return from num from list
    
            once this array is created we simply use BinarySearch to get the required idx in logn time and return the number present at the idx from the list
            Imp point to note is this prefixFreq array gets invalidated as soon as any no is added or removed and need to be created again
         */
        
        /** Initialize your data structure here. */
        public InsertDeleteGetRandomO1WithDuplicates()        // Constructor
        {
            count = 0;
            nums = new();
            numIdxFreq = new();
            rand = new();
        }

        /** Inserts a value to the set. Returns true if the set did not already contain the specified element. */
        public bool Insert(int val)
        {
            count++;
            freqPrefixSum = null;       // invalidate the freqPrefixSum as frequencies have changed
            if (numIdxFreq.ContainsKey(val))
            {
                numIdxFreq[val][1]++;   // increament the freq by 1
                return false;
            }
            else
            {
                nums.Add(val);
                // add the idx and freq as value against number which is key in dictionary
                numIdxFreq[val] = new int[] { nums.Count - 1, 1 };
                return true;
            }
        }

        /** Removes a value from the set. Returns true if the set contained the specified element. */
        public bool Remove(int val)
        {
            if (numIdxFreq.ContainsKey(val))
            {
                count--;                // decrease total no of elements in our collection
                if (--numIdxFreq[val][1] == 0)     // decreament the freq by 1 makes if goes 0 remove the number from list and dictionary
                {
                    var idx = numIdxFreq[val][0];
                    // move the last element in list to current idx
                    nums[idx] = nums[nums.Count - 1];
                    // update idx of last elements which is just moved in its dictionary reference
                    numIdxFreq[nums[idx]][0] = idx;

                    // remove the val from dictionary
                    numIdxFreq.Remove(val);
                    // shorten the list by 1
                    nums.RemoveAt(nums.Count - 1);
                }
                freqPrefixSum = null;       // invalidate the freqPrefixSum as frequencies have changed
                return true;
            }
            else return false;
        }

        /** Get a random element from the set. */
        public int GetRandom()
        {
            int ithElementsIncludingFreq = rand.Next(count);
            // create the 'freqPrefixSum' if not present
            if (freqPrefixSum == null)
            {
                freqPrefixSum = new int[nums.Count];
                freqPrefixSum[0] = numIdxFreq[nums[0]][1] - 1;
                for (int i = 1; i < nums.Count; i++)
                    freqPrefixSum[i] = freqPrefixSum[i - 1] + numIdxFreq[nums[i]][1];
            }
            int lt = 0, rt = freqPrefixSum.Length - 1, mid;
            while (lt < rt)
            {
                mid = (lt + rt) / 2;
                if (freqPrefixSum[mid] < ithElementsIncludingFreq)
                    lt = mid + 1;
                else if (ithElementsIncludingFreq < freqPrefixSum[mid])
                    rt = mid;
                else if (ithElementsIncludingFreq == freqPrefixSum[mid])
                    return nums[mid];
            }
            return nums[lt];
        }
    }
}
