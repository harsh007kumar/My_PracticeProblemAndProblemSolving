using System;

namespace InterviewProblemNSolutions
{
    // MinHeap => PriorityQueue
    public class PriorityQueue<T1,T2>
    {
        public Pair<T1,T2>[] arr;
        public int Count = 0;
        public PriorityQueue(int n = 0) => arr = new Pair<T1, T2>[n];   // Constructor
        public int LeftChild(int x) => 2 * x + 1;
        public int RtChild(int x) => LeftChild(x) + 1;
        public int Parent(int x) => (x - 1) / 2;
        public void Insert(T1 key, T2 val)
        {
            int i = Count++;
            arr[i] = new Pair<T1, T2>(key, val);
            while (i != 0 && Convert.ToInt32(arr[Parent(i)].key) > Convert.ToInt32(arr[i].key))
            {
                int parent = Parent(i);

                // Swap child node with parent
                var temp = arr[parent];
                arr[parent] = arr[i];
                arr[i] = temp;

                i = parent;
            }
        }
        public void Heapify(int i = 0)
        {
            while (i < Count)
            {
                //For empty or Heap with single element we need not perform any operation
                if (Count < 2) return;

                int smaller = i;
                int left = LeftChild(i), rt = RtChild(i);

                if (left < Count && Convert.ToInt32(arr[left].key) < Convert.ToInt32(arr[i].key))
                    smaller = left;
                if (rt < Count && Convert.ToInt32(arr[rt].key) < Convert.ToInt32(arr[smaller].key))
                    smaller = rt;
                if (smaller != i)
                {
                    // Swap root node with smaller child
                    var temp = arr[smaller];
                    arr[smaller] = arr[i];
                    arr[i] = temp;

                    // Heapify child nodes to re-order subtree and maintain MinHeap integrity
                    i = smaller;
                }
                else break;
            }
        }
        public Pair<T1,T2> ExtractMin()
        {
            if (Count <= 0) throw new InvalidOperationException("Extract-Min cannot be performed on Empty Heap");
            var ans = arr[0];
            arr[0] = arr[--Count];
            Heapify();
            return ans;
        }
    }
}
