using System;
using System.Collections.Generic;

namespace InterviewProblemNSolutions
{
    public class MaxFreqStack
    {
        Dictionary<int, int> numFreq;
        Dictionary<int, Stack<int>> grpFreq;
        int maxFreq = 0, freq;
        // Constructor
        public MaxFreqStack()
        {
            numFreq = new Dictionary<int, int>();
            grpFreq = new Dictionary<int, Stack<int>>();
        }

        // Time O(1)
        public void Push(int x)
        {
            Console.WriteLine($" Pushing {x} onto MaxFreqStack Data-Structure");
            freq = 1;
            // New Num pushed to Stack add it to numFreq else increament the existing frequency
            if (!numFreq.ContainsKey(x)) numFreq.Add(x, 1);
            else freq = ++numFreq[x];

            // update maxFreq
            if (maxFreq < freq)
                maxFreq = freq;

            // if new frequency grp is not present create one than push current element to that freq grp stack
            if (!grpFreq.ContainsKey(freq)) grpFreq.Add(freq, new Stack<int>());
            grpFreq[freq].Push(x);
        }
        // Time O(1)
        public int Pop()
        {
            int recentNumWithMaxFreq = grpFreq[maxFreq].Pop();
            
            // if it last occurrence of num remove it from numFreq HashTable
            if (--numFreq[recentNumWithMaxFreq] == 0)
                numFreq.Remove(recentNumWithMaxFreq);

            Console.WriteLine($" Most Recent Num with max frequency to be popped is: \'{recentNumWithMaxFreq}\' with Freq: \'{maxFreq}\'");

            // if there are no more elements with maxFreq left, decreament the maxFreq & free up space in grpFreq HashTable
            if (grpFreq[maxFreq].Count == 0)
            {
                grpFreq.Remove(maxFreq);
                --maxFreq;
            }
            
            return recentNumWithMaxFreq;
        }
    }
}
