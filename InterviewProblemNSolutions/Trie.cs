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
}
