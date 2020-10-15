using System.Collections.Generic;

namespace InterviewProblemNSolutions
{
    public class Node
    {
        public int Data,Key;
        public Node Prv, Next;
        public Node(int data, int key)
        {
            Data = data;
            Key = key;
            Prv = Next = null;
        }
    }

    public class DLL                        // Doubly LinkedList
    {
        public Node Front, End;
        public DLL() => Front = End = null; // Constructor
        public Node InsertRear(int data, int key)
        {
            var newNode = new Node(data,key);
            if (End == null)
                Front = End = newNode;
            else
            {
                End.Next = newNode;
                newNode.Prv = End;
                End = newNode;
            }
            return newNode;
        }
        public void Remove(Node node)
        {
            if (Front == End)
                Front = End = null;
            else
            {
                if (End == node) End = End.Prv;
                if (Front == node) Front = Front.Next;
                if (node.Prv != null) node.Prv.Next = node.Next;
                if (node.Next != null) node.Next.Prv = node.Prv;
            }
        }

        public Node RemoveFront()
        {
            var toBeDeleted = Front;
            if (Front == End)
                Front = End = null;
            else
            {
                Front = Front.Next;
                Front.Prv = null;
            }
            return toBeDeleted;
        }
    }

    public class LRUCache
    {
        private Dictionary<int, Node> Dict; // stores the Key in Dict and its corresponding value at CurrCapacity index of the the array which is stored as value in dictionary
        private DLL LRU;                    // most recently used/added elements are present at the End of the Doubly Linked List
        public int TotalCapacity;
        private int currCapacity = 0;
        public LRUCache(int size)           // constructor
        {
            this.TotalCapacity = size;
            LRU = new DLL();
            Dict = new Dictionary<int, Node>(TotalCapacity);
        }

        // Return the value of the key if the key exists, otherwise return -1
        public int Get(int key)
        {
            if (Dict.ContainsKey(key))      // if present return from Dictionary else return -1
            {
                // update position to most recent in array
                var refInList = Dict[key];
                if (refInList == LRU.End) return LRU.End.Data;

                var node = Dict[key];
                LRU.Remove(node);
                Dict[key] = LRU.InsertRear(node.Data,key);

                return node.Data;               // return value which is now stored at lastIndex in array
            }
            return -1;
        }

        // Update the value of the key if the key exists. Otherwise, add the key-value pair to the cache.
        // If the number of keys exceeds the capacity from this operation, evict the least recently used key.
        public void Put(int key, int newValue)
        {
            if (Dict.ContainsKey(key))
            {
                // update value
                // update key position to most recent in array

                var node = Dict[key];
                LRU.Remove(node);
                Dict[key] = LRU.InsertRear(newValue, key);
            }
            else
            {
                // add to dictionary
                // add at last index in LinkedList

                // No More Space left, Delete first element from and insert new value at last
                if (currCapacity >= TotalCapacity)
                {
                    var firstNode = LRU.RemoveFront();
                    Dict.Remove(firstNode.Key);
                    currCapacity--;
                }

                Dict.Add(key, LRU.InsertRear(newValue, key));
                currCapacity++;
            }
        }
    }
}
