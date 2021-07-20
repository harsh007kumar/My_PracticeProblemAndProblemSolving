using System;
using System.Linq;

namespace InterviewProblemNSolutions
{
    public class ShuffleArray
    {
        int[] original, shuffled;
        int randomIdx, len, temp;
        Random ob;

        public ShuffleArray(int[] nums)
        {
            original = nums;
            shuffled = nums.ToArray();
            ob = new Random();
            len = nums.Length;
        }

        /** Resets the array to its original configuration and return it. */
        public int[] Reset()
        {
            shuffled = original.ToArray();              // O(n)
            return shuffled;
        }

        /** Returns a random shuffling of the array. */
        public int[] Shuffle()
        {
            // start from last index and keasdep randomly selecting any index from 0...i
            // to swap current idx num with
            // Fisher-Yates Algorithm
            for (int i = len - 1; i > 0; i--)           // O(n)
            {
                randomIdx = ob.Next(i + 1);
                // swap
                temp = shuffled[i];
                shuffled[i] = shuffled[randomIdx];
                shuffled[randomIdx] = temp;
            }
            return shuffled;

            /* 1st Attempt
            // pick two random index n swap their values
            idx1 = ob.Next(len);
            idx2 = ob.Next(len);
            while(idx1==idx2)
                idx2 = ob.Next(len);
            // swap
            temp = shuffled[idx1];
            shuffled[idx1] = shuffled[idx2];
            shuffled[idx2] = temp;
            return shuffled;
            */
        }
    }
    /**
     * Your Solution object will be instantiated and called as such:
     * ShuffleArray obj = new ShuffleArray(nums);
     * int[] param_1 = obj.Reset();
     * int[] param_2 = obj.Shuffle();
     */
}
