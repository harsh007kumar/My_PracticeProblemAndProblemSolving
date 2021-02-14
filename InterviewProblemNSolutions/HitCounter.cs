using System.Collections.Generic;

namespace InterviewProblemNSolutions
{
    public class HitCounter
    {
        readonly Queue<int> q;

        /** Initialize your data structure here. */
        public HitCounter() => q = new Queue<int>();

        /** Record a hit.
            @param timestamp - The current timestamp (in seconds granularity). */
        public void Hit(int timestamp) => q.Enqueue(timestamp);

        /** Return the number of hits in the past 5 minutes.
            @param timestamp - The current timestamp (in seconds granularity). */
        public int GetHits(int timestamp)
        {
            while (q.Count > 0 && q.Peek() <= timestamp - 300)
                q.Dequeue();
            return q.Count;
        }
    }
}
