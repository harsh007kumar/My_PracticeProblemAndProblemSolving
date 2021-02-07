using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblemNSolutions
{
    public class TrieNode
    {
        public Dictionary<char, TrieNode> children = null;
        public bool isWord;
        public int times = 0;
        public TrieNode() => children = new Dictionary<char, TrieNode>();
    }
    public class Trie
    {
        public TrieNode root;
        public Trie() => root = new TrieNode();

        // Insert
        public void Add(char[] word)
        {
            TrieNode temp = root;
            foreach(var ch in word)
            {
                if (!temp.children.ContainsKey(ch)) temp.children.Add(ch, new TrieNode());
                temp = temp.children[ch];
            }
            temp.isWord = true;
        }
        // Search Prefix
        public bool SearchPrefix(List<char> words)
        {
            TrieNode temp = root;
            foreach(var ch in words)
            {
                if (!temp.children.ContainsKey(ch)) return false;
                temp = temp.children[ch];
            }
            return true;
        }
        // Search Word
        public bool SearchWord(List<char> words)
        {
            TrieNode temp = root;
            foreach (var ch in words)
            {
                if (!temp.children.ContainsKey(ch)) return false;
                temp = temp.children[ch];
            }
            return temp.isWord;
        }
    }

    public class TrieForAutoComplete : Trie
    {
        public TrieForAutoComplete() : base() { }

        // Insert for 'Search AutoComplete System'
        public void Add(string sentence, int count)
        {
            TrieNode temp = root;
            for (int i = 0; i < sentence.Length; i++)
            {
                if (!temp.children.ContainsKey(sentence[i])) temp.children.Add(sentence[i], new TrieNode());
                temp = temp.children[sentence[i]];
            }
            if (!temp.isWord)
            {
                temp.isWord = true;     // Mark the end of the word
                temp.times = count;     // Update the times word has appeared previously
            }
            else
                temp.times++;           // same word was already present than update the count
        }

        // Updates 'matchesFound' List with all Sentences/Words reachable from current 'TrieNode'
        public void SearchMatch(TrieNode currNode, string prefixSoFar, List<KeyValuePair<string, int>> matchesFound)
        {
            if (currNode.isWord)    // currNode.children.Count == 0
                matchesFound.Add(new KeyValuePair<string, int>(prefixSoFar, currNode.times));
            foreach (var possibleWord in currNode.children)
                SearchMatch(possibleWord.Value, prefixSoFar + possibleWord.Key, matchesFound);
        }
    }

    public class WordDictionary
    {
        private TrieNode root;
        /** Initialize your data structure here. */
        public WordDictionary() => root = new TrieNode();
        
        // Adds word to the data structure, it can be matched later.
        public void AddWord(string word)
        {
            Console.WriteLine($" adding \'{word}\' to Trie");
            TrieNode temp = root;
            foreach (var ch in word)
            {
                if (!temp.children.ContainsKey(ch)) temp.children.Add(ch, new TrieNode());
                temp = temp.children[ch];
            }
            temp.isWord = true; // mark end node as true
        }

        /// <summary>
        /// Returns true if there is any string in the data structure that matches word or false otherwise.
        /// word may contain dots '.' where dots can be matched with any letter.
        /// </summary>
        /// <param name="word"></param>
        /// <param name="temp"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public bool Search(string word, TrieNode temp = null, int i = -1)
        {
            Console.WriteLine($" Searching for \'{word.Substring(i + 1, word.Length - (i + 1))}\' in Trie");
            if (temp == null) temp = root;
            while (++i < word.Length)
            {
                var ch = word[i];
                if (ch != '.')
                {
                    if (!temp.children.ContainsKey(ch)) return false;
                    temp = temp.children[ch];
                }
                else
                {
                    foreach (var possibleChild in temp.children.Values)
                        if (Search(word, possibleChild, i)) return true;
                    return false;
                }
            }
            return temp.isWord;
        }
    }
}
