namespace InterviewProblemNSolutions
{
    public class SelectionAlgorithms
    {
        // Time Complexity Worst Case O(n^2) || on Avg O(nlogk), where n = no of elements & k = k smallest elements || Space O(1)
        public static int KSmallestElements(int[] input, int start, int last, int k)
        {
            if (input == null || input.Length == 0) return -1;
            if (start == last) return start;
            else // if (start < last)
            {
                var pivot = Partition(input, start, last);
                if (pivot == k - 1) return pivot;
                else if (k > pivot) return KSmallestElements(input, pivot + 1, last, k);
                else return KSmallestElements(input, start, pivot - 1, k);
            }
        }

        protected static int Partition(int[] input, int start, int last)
        {
            var pivot = last;
            var pivotElement = input[pivot];
            var currentIndex = start;
            while (currentIndex < last)
            {
                if (input[currentIndex] < pivotElement)
                    Utility.Swap(ref input[currentIndex], ref input[start++]);
                currentIndex++;
            }
            Utility.Swap(ref input[pivot], ref input[start]);
            return start;
        }
    }
}
