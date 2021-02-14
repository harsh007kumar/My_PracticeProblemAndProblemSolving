using System;

namespace InterviewProblemNSolutions
{
    public class MinHeap
    {
        public int[] arr;
        public int Count = 0;
        public MinHeap(int size) => arr = new int[size];
        public int LeftChild(int x) => 2 * x + 1;
        public int RtChild(int x) => LeftChild(x) + 1;
        public int Parent(int x) => (x - 1) / 2;
        public void Insert(int val)
        {
            int i = Count++;
            arr[i] = val;
            while (i != 0 && arr[Parent(i)] > arr[i])
            {
                int parent = Parent(i);

                // Swap child node with parent
                int temp = arr[parent];
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

                if (left < Count && arr[left] < arr[i])
                    smaller = left;
                if (rt < Count && arr[rt] < arr[smaller])
                    smaller = rt;
                if (smaller != i)
                {
                    // Swap root node with smaller child
                    int temp = arr[smaller];
                    arr[smaller] = arr[i];
                    arr[i] = temp;

                    // Heapify child nodes to re-order subtree and maintain MinHeap integrity
                    i = smaller;
                }
                else break;
            }
        }
        public int ExtractMin()
        {
            if (Count <= 0) throw new InvalidOperationException("Extract-Min cannot be performed on Empty Heap");
            var ans = arr[0];
            arr[0] = arr[--Count];
            Heapify();
            return ans;
        }
    }
}
