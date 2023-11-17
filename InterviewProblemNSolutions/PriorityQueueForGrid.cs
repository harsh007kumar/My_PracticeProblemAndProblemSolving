using System.Collections.Generic;

namespace InterviewProblemNSolutions
{
    // implementing Minimum Priorty Queue, Bcoz C# still does not have Heap/PQ ADT
    public class PriorityQueueForGrid
    {
        List<PairForGrid> ls;
        public int count = 0;
        public PriorityQueueForGrid() => ls = new List<PairForGrid>();
        public int lt(int x) => 1 + 2 * x;
        public int rt(int x) => 2 + 2 * x;
        public int Par(int x) => (x - 1) / 2;

        public void Add(PairForGrid p)
        {
            //Console.WriteLine($"Adding {p}");
            ls.Add(p);
            var lastIdx = count++;
            while (lastIdx > 0)
            {
                var parent = Par(lastIdx);
                //Console.WriteLine($" parent idx {parent} | parentt {ls[parent]} | currIdx {lastIdx} | curr {ls[lastIdx]} | size {count}");
                if (ls[parent].wt > ls[lastIdx].wt)
                {
                    //Console.WriteLine($" before {ls[parent]} | {ls[lastIdx]}");
                    var temp = ls[lastIdx];
                    ls[lastIdx] = ls[parent];
                    ls[parent] = temp;

                    //Console.WriteLine($" after {ls[parent]} | {ls[lastIdx]}");
                    lastIdx = parent;
                }
                else break;
            }

        }
        public PairForGrid GetMin()
        {
            //Console.WriteLine($"Returning Min {ls[0]}");
            var min = ls[0];
            ls[0] = ls[--count];
            ls.RemoveAt(count); // since using list need to remove last element
            Heapify();
            //Console.WriteLine($" Current Heap Length {count} & min {ls[0]}");
            return min;
        }
        void Heapify(int idx = 0)
        {
            while (idx < count)
            {
                var left = lt(idx);
                var right = rt(idx);
                var smaller = idx;
                if (left < count && ls[left].wt < ls[idx].wt)
                    smaller = left;
                if (right < count && ls[right].wt < ls[smaller].wt)
                    smaller = right;

                if (smaller != idx)
                {
                    var temp = ls[idx];
                    ls[idx] = ls[smaller];
                    ls[smaller] = temp;
                    idx = smaller;
                }
                else break;
            }
        }
    }

    public class PairForGrid
    {
        public int wt;
        public int x;
        public int y;
        public PairForGrid(int w, int x, int y)
        {
            wt = w;
            this.x = x;
            this.y = y;
        }
        public override string ToString() => $"{x},{y} =>{wt}|";
    }
}
