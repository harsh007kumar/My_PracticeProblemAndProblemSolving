
namespace InterviewProblemNSolutions
{
    public class RangeSumNumArray
    {
        SegmentTree st;

        public RangeSumNumArray(int[] nums) => st = new SegmentTree(nums);

        public void Update(int index, int val) => st.UpdateTree(st.root, index, val);

        public int SumRange(int left, int right) => st.SumRange(st.root, left, right);
    }
}
