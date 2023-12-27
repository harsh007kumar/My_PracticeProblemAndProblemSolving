using System.Collections.Generic;

namespace InterviewProblemNSolutions
{
    public class LRUCacheFaster
    {
        readonly Dictionary<int, DLNode> keyDLNodeMap;
        readonly DL list;
        readonly int size = 0;
        public LRUCacheFaster(int capacity)
        {
            keyDLNodeMap = new Dictionary<int, DLNode>(capacity + 1);
            list = new();
            size = capacity;
        }

        public int Get(int key) // O(1)
        {
            if (keyDLNodeMap.TryGetValue(key, out DLNode val))
            {
                if (list.last != val)
                {
                    list.Remove(val);
                    keyDLNodeMap[key] = list.AddAtEnd(key, val.data);
                }
                return val.data;
            }
            return -1;
        }

        public void Put(int key, int value) // O(1)
        {
            // remove existing entry from the list
            if (keyDLNodeMap.TryGetValue(key, out DLNode val))
                list.Remove(val);

            // add key with updated value in list
            keyDLNodeMap[key] = list.AddAtEnd(key, value);

            if (list.Count > size)    // remove LRU data first
                keyDLNodeMap.Remove(list.Remove(list.head));
            /* Altermate way
            // if key already present
            if (keyDLNodeMap.TryGetValue(key, out DLNode val))
            {
                if (list.last != val)
                {
                    // remove existing entry from the list
                    list.Remove(val);
                    // update key with updated value in list and dict
                    keyDLNodeMap[key] = list.AddAtEnd(key, value);
                }
                else
                    val.data = value;
            }
            else // add new key with its value in dict & list
            {
                keyDLNodeMap[key] = list.AddAtEnd(key, value);
                // remove LRU data if exceeding max size
                if (list.Count > size)
                    keyDLNodeMap.Remove(list.Remove(list.head));
            }
            */
        }
    }
    public class DL
    {
        public int Count = 0;
        public DLNode head, last;
        public DL() => head = last = null;
        public DLNode AddAtEnd(int k, int v)  // O(1)
        {
            Count++;
            var cur = new DLNode(k, v);
            if (head == null)
                head = last = cur;
            else
            {
                cur.prv = last;
                last.nxt = cur;
                last = cur;
            }
            return cur;
        }
        public int Remove(DLNode cur) // O(1)
        {
            Count--;
            if (head == last)       // delete only DLNode in doubly-linked-list
                head = last = null;
            else if (head == cur)   // delete from front
            {
                head = head.nxt;
                if (head != null) head.prv = null;
            }
            else if (last == cur)   // delete from last
            {
                last = last.prv;
                if (last != null) last.nxt = null;
            }
            else                    // delete from middle
            {
                cur.prv.nxt = cur.nxt;
                cur.nxt.prv = cur.prv;
            }
            return cur.key;
        }
    }
    public class DLNode
    {
        public int key, data;
        public DLNode prv, nxt;
        public DLNode(int k, int v)
        {
            key = k;
            data = v;
            prv = nxt = null;
        }
    }
}
