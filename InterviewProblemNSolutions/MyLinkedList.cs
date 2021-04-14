
namespace InterviewProblemNSolutions
{
    // Singly LinkedList
    public class MyLinkedList
    {
        ListNode head, last, curr;
        int count = 0;
        /** Initialize your data structure here. */
        public MyLinkedList() => head = last = null;

        /** Get the value of the index-th node in the linked list. If the index is invalid, return -1. */
        // Time O(n)
        public int Get(int index)
        {
            if (index >= count || index < 0) return -1;
            curr = head;
            while (index-- > 0)
                curr = curr.next;
            return curr.val;
        }

        /** Add a node of value val before the first element of the linked list. After the insertion, the new node will be the first node of the linked list. */
        // Time O(1)
        public void AddAtHead(int val)
        {
            var newNode = new ListNode(val);
            if (count++ == 0)
                head = last = newNode;
            else
            {
                newNode.next = head;
                head = newNode;
            }
        }

        /** Append a node of value val to the last element of the linked list. */
        // Time O(1)
        public void AddAtTail(int val)
        {
            var newNode = new ListNode(val);
            if (count++ == 0)
                head = last = newNode;
            else
            {
                last.next = newNode;
                last = newNode;
            }
        }

        /** Add a node of value val before the index-th node in the linked list. If index equals to the length of linked list, the node will be appended to the end of linked list. If index is greater than the length, the node will not be inserted. */
        // Time O(n)
        public void AddAtIndex(int index, int val)
        {
            if (index > count || index < 0) return;
            else if (index == 0)       // add at head
                AddAtHead(val);
            else if (index == count)   // add at tail
                AddAtTail(val);
            else // add somewhere in b/w
            {
                count++;
                curr = head;
                while (--index > 0)    // move till 1 node prior to index
                    curr = curr.next;
                var newNode = new ListNode(val);
                newNode.next = curr.next;
                curr.next = newNode;
            }
        }

        /** Delete the index-th node in the linked list, if the index is valid. */
        // Time O(n)
        public void DeleteAtIndex(int index)
        {
            if (index >= count || index < 0) return;
            if (count-- == 0)
                head = last = null;
            else // we have more than 1 nodes in our LinkedList
            {
                if (index == 0)    // update head
                    head = head.next;
                else            // delete some other node
                {
                    curr = head;
                    while (--index > 0)    // move till 1 node prior to index
                        curr = curr.next;
                    // if node which we are going to delete is last node
                    if (last == curr.next)
                        last = curr;
                    // remove the index th node
                    curr.next = curr.next.next;
                }
            }
        }

    }
}
