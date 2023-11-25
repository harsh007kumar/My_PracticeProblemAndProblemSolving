using System;
using System.Collections.Generic;

namespace InterviewProblemNSolutions
{
    // Time O(n/p), n = no of all possible keys & p = size of unqiue idx avaliable in array map
    // Space O(p,k), p = predefined size of array of list & k = no of keys inserted in Dictionary
    public class HashMap
    {

        List<Pair<int, int>>[] map;       // array of list of type pair
        static int prime = 7919;
        /** Initialize your data structure here. */
        public HashMap() => map = new List<Pair<int, int>>[prime];

        /** value will always be non-negative. */
        public void Put(int key, int value)
        {
            Console.WriteLine($" Adding/Updating Key \'{key}\' with Value: \'{value}\' to the Dictionary");

            int idx = key % prime;
            if (map[idx] == null) map[idx] = new List<Pair<int, int>>();

            // search if given key is already present
            for (int i = 0; i < map[idx].Count; i++)
                if (map[idx][i].key == key)
                {
                    map[idx][i].val = value;    // update the existing value with new
                    return;
                }
            // if key not present add fresh Pair of key,val in list present at 'idx' index in array
            map[idx].Add(new Pair<int, int>(key, value));
        }

        /** Returns the value to which the specified key is mapped, or -1 if this map contains no mapping for the key */
        public int Get(int key)
        {
            Console.Write($" Trying to fetch value associated with Key \'{key}\' from the Dictionary: ");

            int idx = key % prime;
            if (map[idx] == null) return -1;

            // search if given key is present
            for (int i = 0; i < map[idx].Count; i++)
                if (map[idx][i].key == key)
                    return map[idx][i].val;
            return -1;
        }

        /** Removes the mapping of the specified value key if this map contains a mapping for the key */
        public void Remove(int key)
        {
            Console.WriteLine($" Trying to remove Key \'{key}\' from the Dictionary, if present");

            int idx = key % prime;
            if (map[idx] == null) return;

            // search if given key is present
            for (int i = 0; i < map[idx].Count; i++)
                if (map[idx][i].key == key)
                {
                    map[idx].RemoveAt(i);
                    return;
                }
        }
    }

    // Generic Class of Pair of 2 values
    public class Pair<u, v>
    {
        public u key;
        public v val;
        public Pair(u key, v val)   // Constructor
        {
            this.key = key;
            this.val = val;
        }
        public override string ToString() => $" {key},{val}";
    }




    // BELOW IS TABLE BASED APPROACH NOT MEMORY EFFICIENT
    public class MyHashMapTableStorage
    {

        int[] map;
        /** Initialize your data structure here. */
        public MyHashMapTableStorage() => map = new int[1000001];

        /** value will always be non-negative. */
        public void Put(int key, int value) => map[key] = value + 1;

        /** Returns the value to which the specified key is mapped, or -1 if this map contains no mapping for the key */
        public int Get(int key) => map[key] - 1;

        /** Removes the mapping of the specified value key if this map contains a mapping for the key */
        public void Remove(int key) => map[key] = 0;
    }


}
