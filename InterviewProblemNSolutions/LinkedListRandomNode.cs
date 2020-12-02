using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblemNSolutions
{
    public class LinkedListRandomNode
    {
        static Random rand = null;
        ListNode _head = null, curr = null, result = null;
        int count;

        /** @param head The linked list's head.
            Note that the head is guaranteed to be not null, so it contains at least one node. */
        public LinkedListRandomNode(ListNode head) => _head = head;
        static LinkedListRandomNode() => rand = new Random();


        /** Returns a random node's value. */
        public int GetRandom()          // Time O(n), n = no of nodes in LinkedList
        {
            result = curr = _head;
            count = 1;
            while (curr!=null)
            {
                if (rand.Next(count) == 0) result = curr;
                // To Sample 'K' elements it can be modified as
                // if (j = rand.Next(count) < k) result[j] = curr;
                curr = curr.next;
                count++;
            }
            return result.val;
        }
        /*
        public int GetRandom()
        {
            int count = rand.Next(23);
            while (--count > 0)
                slow = slow != null ? slow.next : _head;
            return slow != null ? slow.val : _head.val;
        }
        */
    }
}
