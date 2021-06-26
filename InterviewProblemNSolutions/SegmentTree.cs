using System;

namespace InterviewProblemNSolutions
{
    public class SegmentNode
    {
        public int lt, rt, sum;
        public SegmentNode leftChild, rightChild;
        public SegmentNode(int leftIdx, int rtIdx)                          // Constructor
        {
            lt = leftIdx;
            rt = rtIdx;
            leftChild = rightChild = null;
            sum = 0;
        }
    }

    // Sum of given range
    public class SegmentTree
    {
        public SegmentNode root = null;
        private int[] arr;

        public SegmentTree(int[] arr)
        {
            this.arr = arr;
            root = BuildTree(0, arr.Length - 1);
        }

        public SegmentNode BuildTree(int start, int last)                   // O(n)
        {
            if (start == last)
                return new SegmentNode(start, last) { sum = arr[start] };   // leaf node

            int mid = start + (last - start) / 2;
            SegmentNode node = new SegmentNode(start, last)
            {
                leftChild = BuildTree(start, mid),
                rightChild = BuildTree(mid + 1, last),
            };

            // update sum of current node
            node.sum = node.leftChild.sum + node.rightChild.sum;
            
            return node;
        }

        public void UpdateTree(SegmentNode cur, int idx, int val)           // O(logn)
        {
            if (cur.lt == cur.rt)
                cur.sum = val;
            else
            {
                var mid = (cur.lt + cur.rt) / 2;

                if (idx <= mid)         // idx we are trying to update lies on left
                    UpdateTree(cur.leftChild, idx, val);
                else // if (idx > mid) // idx we are trying to update lies on right
                    UpdateTree(cur.rightChild, idx, val);
                
                // update sum at current node to reflect changes
                cur.sum = cur.leftChild.sum + cur.rightChild.sum;
            }
        }

        public int SumRange(SegmentNode cur, int start, int last)           // O(logn)
        {
            if (cur.lt == start && cur.rt == last)
                return cur.sum;
            else
            {
                var mid = (cur.lt + cur.rt) / 2;

                if (last <= mid)
                    return SumRange(cur.leftChild, start, last);
                else if (start > mid)
                    return SumRange(cur.rightChild, start, last);
                else
                    return SumRange(cur.leftChild, start, mid) + SumRange(cur.rightChild, mid + 1, last);
            }
        }
    }

    // Smaller element count on right for given array
    public class SegmentTree_SmallerCount
    {
        public SegmentNode root = null;
        public int Start, End;

        public SegmentTree_SmallerCount(int[] arr)
        {
            Start = int.MaxValue;
            End = int.MinValue;
            for (int i = 0; i < arr.Length; i++)                            // O(n)
            {
                Start = Math.Min(Start, arr[i]);
                End = Math.Max(End, arr[i]);
            }
            // build segment tree for all possible ranges b/w smallest n largest number in input array
            // while setting sum value at each node as Zero '0'
            root = BuildTree(Start, End, 0);
        }

        public SegmentNode BuildTree(int start, int last, int val)          // O(n)
        {
            if (start == last)
                return new SegmentNode(start, last) { sum = val };          // leaf node

            int mid = start + (last - start) / 2;
            SegmentNode node = new SegmentNode(start, last)
            {
                leftChild = BuildTree(start, mid, val),
                rightChild = BuildTree(mid + 1, last, val)
            };

            //// update sum of current node
            //node.sum = node.leftChild.sum + node.rightChild.sum;

            return node;
        }

        public void UpdateTree(SegmentNode cur, int idx, int val)           // O(logn)
        {
            if (cur.lt == cur.rt)
                cur.sum += val;
            else
            {
                var mid = cur.lt + (cur.rt - cur.lt) / 2;

                if (idx <= mid)         // idx we are trying to update lies on left
                    UpdateTree(cur.leftChild, idx, val);
                else // if (idx > mid) // idx we are trying to update lies on right
                    UpdateTree(cur.rightChild, idx, val);

                // update sum at current node to reflect changes
                cur.sum = cur.leftChild.sum + cur.rightChild.sum;
            }
        }

        public int SumRange(SegmentNode cur, int start, int last)           // O(logn)
        {
            if (cur == null) return 0;
            if (cur.lt == start && cur.rt == last)
                return cur.sum;
            else
            {
                var mid = cur.lt + (cur.rt - cur.lt) / 2;

                if (last <= mid)
                    return SumRange(cur.leftChild, start, last);
                else if (start > mid)
                    return SumRange(cur.rightChild, start, last);
                else
                    return SumRange(cur.leftChild, start, mid) + SumRange(cur.rightChild, mid + 1, last);
            }
        }
    }


}
