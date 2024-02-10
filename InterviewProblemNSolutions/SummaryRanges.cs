using System.Collections.Generic;

namespace InterviewProblemNSolutions
{
    public class SummaryRanges
    {
        readonly List<int[]> range;

        // default Constructor
        public SummaryRanges() => range = new();

        // O(logn + n)
        public void AddNum(int value)
        {
            /* ALGO
            1. Keep sorted list so that each new value appropriate index can be found in logn by binarysearch
            2. if new value doesn't already lies b/w and existing interval
            3. add a new interval at the idx returned from binarySearch
            4. if added new interval check if it can be merged with left and right intervals individually
             */
            // find bigger value to right
            int lt = 0, rt = range.Count - 1, mid;
            bool withinExistingRange = false;

            while (lt <= rt)       // O(logn)
            {
                mid = (lt + rt) / 2;
                // mid start in bigger than move to left
                if (value < range[mid][0])
                    rt = mid - 1;
                // mid end in smaller than move to right
                else if (range[mid][1] < value)
                    lt = mid + 1;
                else
                {
                    // new value lies in mid..range no need to do anything
                    withinExistingRange = true;
                    break;
                }
            }
            if (!withinExistingRange)   // if value does lies in any range add it to the list at last know value value of lt pointer
            {
                range.Insert(lt, new int[] { value, value });    // O(n)

                // check if right start and cur value are continuous
                if (lt + 1 < range.Count && range[lt + 1][0] == value + 1) // O(n)
                {
                    range[lt][1] = range[lt + 1][1];    // merge intervals & increase the end of lt
                    range.RemoveAt(lt + 1);   // remove cur
                }

                // check if left end and cur value are continuous
                if (lt - 1 >= 0 && range[lt - 1][1] + 1 == value)          // O(n)
                {
                    range[lt - 1][1] = range[lt][1];    // merge intervals & increase the end of lt-1
                    range.RemoveAt(lt);   // remove cur
                }
            }
        }
        // O(n) | to convert list to array
        public int[][] GetIntervals() => range.ToArray();
    }

    /**
     * Your SummaryRanges object will be instantiated and called as such:
     * SummaryRanges obj = new SummaryRanges();
     * obj.AddNum(value);
     * int[][] param_2 = obj.GetIntervals();
     */
}
