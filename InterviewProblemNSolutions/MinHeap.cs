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
        public int GetMin() => Count > 0 ? arr[0] : Int32.MinValue;
    }

    public class MinHeapPair
    {
        public int[][] arr;
        public int Count = 0;
        public MinHeapPair(int size) => arr = new int[size][];
        public int Left(int x) => 2 * x + 1;
        public int Right(int x) => 2 * x + 2;
        public int Parent(int x) => (x - 1) / 2;
        public void Add(int n, int freq)  // bottom-up
        {
            Console.WriteLine($" Key {n}- Freq {freq}, Heap Size {Count}");
            int i = Count++;
            arr[i] = new int[2] { n, freq };
            while (i != 0 && arr[Parent(i)][1] > arr[i][1])
            {
                int p = Parent(i);
                // swap
                var temp = arr[p];
                arr[p] = arr[i];
                arr[i] = temp;
                i = p;
            }
        }
        public void Heapify(int i = 0)    // top-down
        {
            while (i < Count)
            {
                if (Count < 2) return;
                int lt = Left(i), rt = Right(i);
                int smaller = i;
                if (lt < Count && arr[lt][1] < arr[i][1])
                    smaller = lt;
                if (rt < Count && arr[rt][1] < arr[smaller][1])
                    smaller = rt;
                if (smaller != i)
                {
                    var temp = arr[smaller];
                    arr[smaller] = arr[i];
                    arr[i] = temp;
                    // Heapify child
                    i = smaller;
                }
                else return;
            }

        }
        public int[] ExtractMin()
        {
            var ans = arr[0];
            arr[0] = arr[--Count];
            Heapify();
            return ans;
        }
    }
}
