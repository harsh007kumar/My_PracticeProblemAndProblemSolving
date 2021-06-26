
namespace InterviewProblemNSolutions
{
    public class CountBSTNode
    {
        public int val, Count;
        public CountBSTNode lt, rt;
        public CountBSTNode(int val, int count = 0)
        {
            lt = rt = null;
            this.val = val;
            this.Count = count;
        }
    }

    public class CountSmallerBST
    {
        public CountBSTNode root;
        public CountSmallerBST() => root = null;

        public int Add(ref CountBSTNode parent, int val, int target)
        {
            if (parent == null)
            {
                parent = new CountBSTNode(val, 0);
                return target;
            }
            else if (parent.val < val)
                return parent.Count + Add(ref parent.rt, val, target + 1);
            else // if(parent.val >= val)
            {
                parent.Count++;
                return Add(ref parent.lt, val, target);
            }
        }
    }
}
