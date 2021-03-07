using System;
using System.Collections.Generic;

namespace InterviewProblemNSolutions
{
    public class MyHashSet
    {

        static int prime = 7919;
        List<int>[] set;
        /** Initialize your data structure here. */
        public MyHashSet() => set = new List<int>[prime];

        public void Add(int key)
        {
            Console.WriteLine($" Adding Key \'{key}\' to the HashSet");

            int idx = key % prime;
            if (set[idx] == null) set[idx] = new List<int>() { key };
            else
            {
                for (int i = 0; i < set[idx].Count; i++)
                    if (set[idx][i] == key)    // key already present than exit 
                        return;

                set[idx].Add(key);          // else add the key
            }
        }

        public void Remove(int key)
        {
            Console.WriteLine($" Removing Key \'{key}\' if present from the HashSet");

            int idx = key % prime;
            if (set[idx] == null) return;

            // search if key present than remove & exit 
            for (int i = 0; i < set[idx].Count; i++)
                if (set[idx][i] == key)
                {
                    set[idx].RemoveAt(i);
                    return;
                }
        }

        /** Returns true if this set contains the specified element */
        public bool Contains(int key)
        {
            Console.Write($" Checking if Key \'{key}\' is present in the HashSet: ");

            int idx = key % prime;
            if (set[idx] == null) return false;

            // search if key is present
            for (int i = 0; i < set[idx].Count; i++)
                if (set[idx][i] == key)
                    return true;

            return false;
        }
    }

    /**
     * Your MyHashSet object will be instantiated and called as such:
     * MyHashSet obj = new MyHashSet();
     * obj.Add(key);
     * obj.Remove(key);
     * bool param_3 = obj.Contains(key);
     */
}
