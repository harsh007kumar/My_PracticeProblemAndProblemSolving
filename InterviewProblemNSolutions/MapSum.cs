using System.Collections.Generic;

namespace InterviewProblemNSolutions
{
    public class MapSum
    {
        Dictionary<string, int> dict;
        TrieForMapSum trieOb;

        /** Initialize your data structure here. */
        public MapSum()
        {
            dict = new Dictionary<string, int>();
            trieOb = new TrieForMapSum();
        }

        public void Insert(string key, int val)
        {
            if (!dict.ContainsKey(key))
                trieOb.Add(key);        // add the key to Trie if not already added previously

            dict[key] = val;            // add/update key-val mapping in Dictionary
        }

        public int Sum(string prefix) => trieOb.GetMatchingPrefixSum(prefix, dict);
    }

    /**
     * Your MapSum object will be instantiated and called as such:
     * MapSum obj = new MapSum();
     * obj.Insert(key,val);
     * int param_2 = obj.Sum(prefix);
     */

}
