using System;
using System.Collections.Generic;

namespace InterviewProblemNSolutions
{
    public class WordFilter
    {
        TrieForPreFixSuffixSearch trieOriginal, trieReverse;
        public WordFilter(string[] words)
        {
            trieOriginal = new TrieForPreFixSuffixSearch();
            trieReverse = new TrieForPreFixSuffixSearch();
            for (int i = 0; i < words.Length; i++)
            {
                trieOriginal.Add(words[i], i);          // add word along with its index in input arr 'words'
                
                var rev = words[i].ToCharArray();
                Array.Reverse(rev);
                trieReverse.Add(new string(rev), i);    // add reverse-word along with its index in input arr 'words'
            }
        }
        // Returns largest index of word which matches 'prefix' & 'suffix'
        public int Find(string prefix, string suffix)
        {
            // Get all indices match the prefix from 'orignal word trie'
            List<int> preFixList = trieOriginal.SearchPrefix(prefix);
            
            var rev = suffix.ToCharArray();
            Array.Reverse(rev);
            // Get all indices match the reverse-suffic from 'reverse word trie'
            List<int> suffixList = trieReverse.SearchPrefix(new string(rev));

            int ans = -1;
            HashSet<int> suffixSet = new HashSet<int>(suffixList);  // Convert suffix result to HashSet
            foreach (var idx in preFixList)                         // for all indices in preFixList which are also present in suffix get the max idx possible
                if (suffixSet.Contains(idx))
                    ans = Math.Max(ans, idx);
            
            return ans;
        }
    }

    /**
     * Your WordFilter object will be instantiated and called as such:
     * WordFilter obj = new WordFilter(words);
     * int param_1 = obj.F(prefix,suffix);
     */
}
