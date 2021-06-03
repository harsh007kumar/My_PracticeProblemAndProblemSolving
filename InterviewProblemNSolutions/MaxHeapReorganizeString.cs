namespace InterviewProblemNSolutions
{
    public class MaxHeapReorganize
    {
        public int[][] arr;
        public int Count;
        int Parent(int idx) => (idx - 1) / 2;
        int LtChild(int idx) => (idx * 2) + 1;
        int RtChild(int idx) => (idx * 2) + 2;

        public MaxHeapReorganize()
        {
            arr = new int[26][];
            Count = 0;
        }
        public void Add(int freq, int character)
        {
            var idx = Count++;
            arr[idx] = new int[] { freq, character };
            // Percolate up operation
            while (idx > 0)
            {
                int parent = Parent(idx);
                // parent freq is smaller than curr char than swap
                if (arr[parent][0] < arr[idx][0])
                {
                    var temp = arr[parent];
                    arr[parent] = arr[idx];
                    arr[idx] = temp;
                    idx = parent;
                }
                else break;
            }
        }
        public void Heapify(int idx = 0)
        {
            // removing character with 0 freq
            if (idx < Count && arr[idx][0] <= 0)
            {
                var last = arr[Count - 1];
                arr[Count - 1] = arr[idx];
                arr[idx] = last;
                --Count;
                Heapify(idx);
                return;
            }

            int lt, rt, max;
            while (idx < Count)
            {
                max = idx;
                lt = LtChild(idx);
                rt = RtChild(idx);
                if (lt < Count && arr[lt][0] > arr[max][0])
                    max = lt;
                if (rt < Count && arr[rt][0] > arr[max][0])
                    max = rt;
                if (max != idx)
                {
                    var temp = arr[max];
                    arr[max] = arr[idx];
                    arr[idx] = temp;
                    idx = max;
                }
                else break;
            }
        }
    }
}
