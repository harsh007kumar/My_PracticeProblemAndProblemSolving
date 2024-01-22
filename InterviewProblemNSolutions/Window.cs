using System.Collections.Generic;

namespace InterviewProblemNSolutions
{
    public class Window
    {
        readonly List<long> list = new();
        public long sum = 0;
        private readonly int minWinSize = 0;
        public Window(int minWinSize) => this.minWinSize = minWinSize;
        public void Insert(int val)
        {
            int index = BinarySearch(val);
            if (index >= 0)
                list.Insert(index, val);
            else
            {
                list.Add(val);
                index = list.Count - 1;
            }
            if (index < minWinSize)
            {
                sum += list[index];
                if (minWinSize < list.Count)
                    sum -= list[minWinSize];
            }
        }
        public void Remove(int val)
        {
            int index = BinarySearch(val);
            if (index >= 0)
            {
                if (index < minWinSize)
                {
                    sum -= list[index];
                    if (minWinSize < list.Count)
                        sum += list[minWinSize];
                }
                list.RemoveAt(index);
            }
        }
        public long MinSum() => sum;
        public int BinarySearch(int val)
        {

            int l = 0, r = list.Count - 1;
            if (list.Count == 0 || val > list[r]) return -1;

            int mid = (l + r) / 2;
            while (l < r)
            {
                if (list[mid] < val)
                    l = mid + 1;
                else
                    r = mid;
                mid = (l + r) / 2;
            }
            return r;
        }
    }
}