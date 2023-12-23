using System.Collections.Generic;

namespace InterviewProblemNSolutions
{
    public class MedianFinder
    {
        PriorityQueue<int, int> maxHeapLt = null;
        PriorityQueue<int, int> minHeapRt = null;
        int Count = 0;
        
        // Constructor
        public MedianFinder()
        {
            maxHeapLt = new PriorityQueue<int, int>(Comparer<int>.Create((x, y) => y.CompareTo(x)));
            minHeapRt = new PriorityQueue<int, int>();
        }

        // O(logN), n = no of elements added to data-structure till now
        public void AddNum(int num)
        {
            Count++;
            // new number always added to left side i.e. maxHeap first
            maxHeapLt.Enqueue(num, num);           // O(logN)

            // left side max should be <= right side min, if not move it to right
            if (minHeapRt.TryPeek(out int n, out int priority) && maxHeapLt.Peek() > n)
            {
                var biggerFromLt = maxHeapLt.Dequeue();
                minHeapRt.Enqueue(biggerFromLt, biggerFromLt);
            }

            // rebalance if required (len/count of 2 half different by > 1)
            if (maxHeapLt.Count > minHeapRt.Count + 1)
            {
                var fromLeft = maxHeapLt.Dequeue();
                minHeapRt.Enqueue(fromLeft, fromLeft);
            }
            else if (maxHeapLt.Count + 1 < minHeapRt.Count)
            {
                var fromRight = minHeapRt.Dequeue();
                maxHeapLt.Enqueue(fromRight, fromRight);
            }
        }
        
        // Time O(1)
        public double FindMedian()
        {
            if (Count % 2 == 0)  // even no of elements
                return (double)(maxHeapLt.Peek() + minHeapRt.Peek()) / 2;
            else            // odd no of elements
                return maxHeapLt.Count > minHeapRt.Count ? maxHeapLt.Peek() : minHeapRt.Peek();
        }
    }
}
