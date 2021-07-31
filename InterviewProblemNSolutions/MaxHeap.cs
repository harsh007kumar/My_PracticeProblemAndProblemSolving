using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblemNSolutions
{
    public class MaxHeap
    {
        public int[] arr;
        public int Count = 0;
        public MaxHeap(int size)
        {
            arr = new int[size];
        }
        public int LeftChild(int x) => 2 * x + 1;
        public int RtChild(int x) => LeftChild(x) + 1;
        public int Parent(int x) => (x - 1) / 2;
        public int GetMax() => arr[0];
        public void UpdateTop(int val)
        {
            arr[0] = val;
            MaxHeapify();
        }
        public void Insert(int val)
        {
            int i = Count++;
            arr[i] = val;
            while (i != 0 && arr[Parent(i)] < arr[i])
            {
                int parent = Parent(i);

                // Swap child node with parent
                int temp = arr[parent];
                arr[parent] = arr[i];
                arr[i] = temp;

                i = parent;
            }
        }
        public void MaxHeapify(int i = 0)
        {
            while (i < Count)
            {
                //For empty or Heap with single element we need not perform any operation
                if (Count < 2) return;

                int largest = i;
                int left = LeftChild(i), rt = RtChild(i);

                if (left < Count && arr[left] > arr[i])
                    largest = left;
                if (rt < Count && arr[rt] > arr[largest])
                    largest = rt;
                if (largest != i)
                {
                    // Swap root node with larger child
                    int temp = arr[largest];
                    arr[largest] = arr[i];
                    arr[i] = temp;

                    // Heapify child nodes to re-order subtree and maintain MinHeap integrity
                    i = largest;
                }
                else break;
            }
        }
        public int ExtractMax()
        {
            if (Count <= 0) throw new InvalidOperationException("Extract-Max cannot be performed on Empty Heap");
            var ans = arr[0];
            arr[0] = arr[--Count];
            MaxHeapify();
            return ans;
        }
    }


    public class MaxHeapMinRefuelStops
    {
        public int[][] arr;
        public int Count = 0;
        public MaxHeapMinRefuelStops(int size) => arr = new int[size][]; /// Constructor

        public int LeftChild(int x) => 2 * x + 1;
        public int RtChild(int x) => LeftChild(x) + 1;
        public int Parent(int x) => (x - 1) / 2;

        public void Add(int[] val)
        {
            int i = Count++;
            arr[i] = val;
            while (i != 0 && arr[Parent(i)][1] < arr[i][1])
            {
                int parent = Parent(i);

                // Swap child node with parent
                var temp = arr[parent];
                arr[parent] = arr[i];
                arr[i] = temp;

                i = parent;
            }
        }
        public void MaxHeapify(int i = 0)
        {
            while (i < Count)
            {
                //For empty or Heap with single element we need not perform any operation
                if (Count < 2) return;

                int largest = i;
                int left = LeftChild(i), rt = RtChild(i);

                if (left < Count && arr[left][1] > arr[i][1])
                    largest = left;
                if (rt < Count && arr[rt][1] > arr[largest][1])
                    largest = rt;
                if (largest != i)
                {
                    // Swap root node with larger child
                    var temp = arr[largest];
                    arr[largest] = arr[i];
                    arr[i] = temp;

                    // Heapify child nodes to re-order subtree and maintain MinHeap integrity
                    i = largest;
                }
                else break;
            }
        }
        public int[] ExtractMax()
        {
            if (Count <= 0) throw new InvalidOperationException("Extract-Max cannot be performed on Empty Heap");
            var ans = arr[0];
            arr[0] = arr[--Count];
            MaxHeapify();
            return ans;
        }
    }


}
