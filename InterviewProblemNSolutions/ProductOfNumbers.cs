using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblemNSolutions
{
    // 1352. Product of the Last K Numbers || https://leetcode.com/problems/product-of-the-last-k-numbers/
    public class ProductOfNumbers
    {
        List<int> pre;        // stores product of all nums from 0 index till current

        // Constructor
        public ProductOfNumbers() => pre = new List<int>();

        // Time O(1)
        public void Add(int num)
        {
            Console.WriteLine($" Adding {num}");
            if (num == 0)
                pre.Clear();
            else
            {
                if (pre.Count > 0)
                    pre.Add(num * pre[pre.Count - 1]);
                else
                    pre.Add(num);
            }
        }
        // Time O(1)
        public int GetProduct(int k)
        {
            Console.Write($" Fetching Product of Last \'{k}\' numbers: ");
            if (k > pre.Count) return 0;
            int kthIndexProduct = k < pre.Count ? pre[pre.Count - k - 1] : 1;
            return pre[pre.Count - 1] / kthIndexProduct;
        }
    }
}
