using System;
using System.Collections.Generic;

namespace InterviewProblemNSolutions
{
    [Obsolete("Trie Approach is way complex and does passes all LC testcases, instead check Dictionary based algo => DailyProblem.Spellchecker(string[] wordlist, string[] queries)")]
    static class Spellchecker
    {
        public static string[] VowelSpellchecker(string[] wordlist, string[] queries)
        {
            int l = queries.Length;
            TrieVowelSpellchecker t = new TrieVowelSpellchecker();
            for (int i = 0; i < wordlist.Length; i++)
                t.Add(wordlist[i], t.root);

            string[] ans = new string[l];
            for (int i = 0; i < l; i++)
                ans[i] = t.Search(queries[i], queries[i], t.root);

            return ans;
        }

        class Node
        {
            public Dictionary<char, Node> child;
            public List<string> words;
            public HashSet<string> set;
            public List<char> vowelOrder;
            // Constructor
            public Node()
            {
                child = new Dictionary<char, Node>();
                words = new List<string>();
                set = new HashSet<string>();
                vowelOrder = new List<char>();
            }
        }
        class TrieVowelSpellchecker
        {
            public Node root;
            
            // Constructor
            public TrieVowelSpellchecker() => root = new Node();

            // Time = Space = O(l), l = length of word being added to Trie
            public void Add(string word, Node curr)        // add words
            {
                foreach (var ch in word.ToLower())
                {
                    if (!curr.child.ContainsKey(ch))
                        curr.child.Add(ch, new Node());

                    // if current char is vowel add it to list of vowel also present as child of current node
                    if (IsVowel(ch) && !CharAlreadyPresent(curr.vowelOrder, ch))
                        curr.vowelOrder.Add(ch);

                    curr = curr.child[ch];
                }
                curr.words.Add(word);
                curr.set.Add(word);
            }

            public string Search(string query, string originalQuery, Node curr)
            {
                for (int i = 0; i < query.Length; i++)
                {
                    char ch = query[i];

                    // Excat char matches, try searching if their is a hit with this sub-trie
                    if (curr.child.ContainsKey(ch))
                    {
                        var result = Search(query.Substring(i + 1), originalQuery, curr.child[ch]);
                        if (result != "")           // return 1st hit
                            return result;
                    }

                    ch = Char.ToLower(ch);
                    // Excat char but case-mismatch, try searching if their is a hit with this sub-trie
                    if (curr.child.ContainsKey(ch))
                    {
                        var result = Search(query.Substring(i + 1), originalQuery, curr.child[ch]);
                        if (result != "")           // return 1st hit
                            return result;
                    }

                    // if still no Hit found than check if not a vowel, moving frwd not possible
                    if (!IsVowel(ch))
                        return "";
                    else
                    {
                        // current mistmatch char is vowel, try finding if any other vowel can be used
                        foreach (var vowel in curr.vowelOrder)
                            if (curr.child.ContainsKey(vowel))
                            {
                                var result = Search(query.Substring(i + 1), originalQuery, curr.child[vowel]);
                                if (result != "")   // return 1st hit
                                    return result;
                            }
                        // if no vowel returns valid ans than return blank string
                        return "";
                    }
                }
                // excat word found
                if (curr.set.Contains(originalQuery))
                    return originalQuery;
                // word with either capitlization or vowel mismatch found
                if (curr.words.Count > 0)
                    return curr.words[0];
                // no match found
                return "";
            }

            // Time O(1)
            bool IsVowel(char ch) => ch == 'a' || ch == 'e' || ch == 'i' || ch == 'o' || ch == 'u';
            
            // Time O(5) ~O(1)
            bool CharAlreadyPresent(List<char> listOfVowels, char ch)
            {
                foreach (char character in listOfVowels)
                    if (character == ch)    // 'ch' already exists
                        return true;
                return false;
            }
        }

        

        
    }
}
