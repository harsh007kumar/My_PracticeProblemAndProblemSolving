using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewProblemNSolutions
{
    public class TrieNode
    {
        public Dictionary<char, TrieNode> children = null;
        public bool isWord;
        public int times = 0;
        public int index = -1;
        public string word;
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
            temp.word = new string(word);
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
        public bool FindPrefix(TrieNode cur, string s, int idx, StringBuilder sb)
        {
            if (cur.isWord) return true;
            else if (idx == s.Length) return false;

            if (cur.children.TryGetValue(s[idx], out TrieNode subTree))
            {
                sb.Append(s[idx]);
                return FindPrefix(subTree, s, idx + 1, sb);
            }
            else return false;
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
        // return child node if passed input trinode has given char ch in dictionary
        public static TrieNode ValidPath(TrieNode r, char ch)
        {
            if (r.children.TryGetValue(ch, out TrieNode val))
                return val;
            else
                return null;
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

    public class TrieForPreFixSuffixSearch
    {
        public TrieNode root, temp;
        public TrieForPreFixSuffixSearch() => root = new TrieNode();

        // Insert
        public void Add(string word, int idx)
        {
            TrieNode temp = root;
            foreach (var ch in word)
            {
                if (!temp.children.ContainsKey(ch)) temp.children.Add(ch, new TrieNode());
                temp = temp.children[ch];
            }
            temp.isWord = true;
            temp.index = idx;
        }

        public List<int> SearchPrefix(string prefix)
        {
            List<int> indiciesOfMatches = new List<int>();
            temp = root;
            foreach (var ch in prefix)              // Try reaching to the last node in prefix
                if (temp.children.ContainsKey(ch))
                    temp = temp.children[ch];
                else
                    return indiciesOfMatches;
            
            Populate(temp);                         // Now add indicies of all words that match above prefix
            return indiciesOfMatches;
            
            // Local func
            void Populate(TrieNode temp)
            {
                if (temp.isWord)                    // if current node is a word add it to 'ans'
                    indiciesOfMatches.Add(temp.index);

                foreach (var possibleMatch in temp.children.Values) // recursively search in sub-trees of curr Node
                    Populate(possibleMatch);
            }

        }
    }

    public class TrieForSearchSuggestionSystem : Trie
    {

        public TrieForSearchSuggestionSystem() : base() { }

        // Updates 'matchesFound' List with all Sentences/Words reachable from current 'TrieNode' till count is less <= 3
        public void SearchSuggestion(TrieNode currNode, Stack<char> prefixSoFar, List<string> matchesFound)
        {
            if (currNode.isWord)
                matchesFound.Add(new string(prefixSoFar.Reverse().ToArray()));
            // traverse in lexographically sorted order
            foreach (var possibleWord in currNode.children.OrderBy(x => x.Key))
            {
                if (matchesFound.Count >= 3) return;
                prefixSoFar.Push(possibleWord.Key);
                SearchSuggestion(possibleWord.Value, prefixSoFar, matchesFound);
                prefixSoFar.Pop();
            }
        }
    }

    public class TrieForMapSum
    {
        public TrieNode root;
        public TrieForMapSum() => root = new TrieNode();

        // Insert
        public void Add(string word)
        {
            TrieNode temp = root;
            foreach (var ch in word)
            {
                if (!temp.children.ContainsKey(ch)) temp.children.Add(ch, new TrieNode());
                temp = temp.children[ch];
            }
            temp.isWord = true;
            temp.word = word;
        }
        // Get matching Prefix Sum
        public int GetMatchingPrefixSum(string prefix, Dictionary<string, int> keyValueMap)
        {
            int totalSum = 0;
            TrieNode temp = root;
            foreach (var ch in prefix)
            {
                if (!temp.children.ContainsKey(ch)) return totalSum;
                temp = temp.children[ch];
            }
            GetAllSubNodeSum(temp);
            return totalSum;

            // local helper func
            void GetAllSubNodeSum(TrieNode currNode)
            {
                if (currNode.isWord) totalSum += keyValueMap[currNode.word];
                foreach (var node in currNode.children)
                    GetAllSubNodeSum(currNode.children[node.Key]);
            }
        }
    }




}
