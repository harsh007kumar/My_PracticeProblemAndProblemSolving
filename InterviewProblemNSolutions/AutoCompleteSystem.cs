using System.Collections.Generic;
using System.Text;

namespace InterviewProblemNSolutions
{
    /*  Design Search AutoComplete System - Problem Statement

        Design a search autocomplete system for a search engine. Users may input a sentence (at least one word and end with a special character '#'). For each character they type except '#', you need to return the top 3 historical hot sentences that have prefix the same as the part of sentence already typed. Here are the specific rules:

        The hot degree for a sentence is defined as the number of times a user typed the exactly same sentence before.
        The returned top 3 hot sentences should be sorted by hot degree (The first is the hottest one). If several sentences have the same degree of hot, you need to use ASCII-code order (smaller one appears first).
        If less than 3 hot sentences exist, then just return as many as you can.
        When the input is a special character, it means the sentence ends, and in this case, you need to return an empty list.
        Your job is to implement the following functions:

        The constructor function:

        AutocompleteSystem(String[] sentences, int[] times): This is the constructor. The input is historical data. Sentences is a string array consists of previously typed sentences. Times is the corresponding times a sentence has been typed. Your system should record these historical data.

        Now, the user wants to input a new sentence. The following function will provide the next character the user types:

        List<String> input(char c): The input c is the next character typed by the user. The character will only be lower-case letters ('a' to 'z'), blank space (' ') or a special character ('#'). Also, the previously typed sentence should be recorded in your system. The output will be the top 3 historical hot sentences that have prefix the same as the part of sentence already typed.

 
        Example:
        Operation: AutocompleteSystem(["i love you", "island","ironman", "i love leetcode"], [5,3,2,2])
        The system have already tracked down the following sentences and their corresponding times:
        "i love you" : 5 times
        "island" : 3 times
        "ironman" : 2 times
        "i love leetcode" : 2 times
        Now, the user begins another search:

        Operation: input('i')
        Output: ["i love you", "island","i love leetcode"]
        Explanation:
        There are four sentences that have prefix "i". Among them, "ironman" and "i love leetcode" have same hot degree. Since ' ' has ASCII code 32 and 'r' has ASCII code 114, "i love leetcode" should be in front of "ironman". Also we only need to output top 3 hot sentences, so "ironman" will be ignored.

        Operation: input(' ')
        Output: ["i love you","i love leetcode"]
        Explanation:
        There are only two sentences that have prefix "i ".

        Operation: input('a')
        Output: []
        Explanation:
        There are no sentences that have prefix "i a".

        Operation: input('#')
        Output: []
        Explanation:
        The user finished the input, the sentence "i a" should be saved as a historical sentence in system. And the following input will be counted as a new search.

 
        Note:

        The input sentence will always start with a letter and end with '#', and only one blank space will exist between two words.
        The number of complete sentences that to be searched won't exceed 100. The length of each sentence including those in the historical data won't exceed 100.
        Please use double-quote instead of single-quote when you write test cases even for a character input.
        Please remember to RESET your class variables declared in class AutocompleteSystem, as static/class variables are persisted across multiple test cases. Please see here for more details.
     */

    public class AutoCompleteSystem
    {
        // to append new characters in current sentence till we encounter '#'
        StringBuilder preFixSoFar = null;
        TrieForAutoComplete Trie = null;
        List<KeyValuePair<string, int>> matchesFound = null;
        TrieNode currNode = null;

        // Constructor
        // Time O(K*L), We need to iterate over L sentences each of average length k, to create the trie for the given set of sentences
        public AutoCompleteSystem(string[] sentences, int[] times)
        {
            preFixSoFar = new StringBuilder();
            Trie = new TrieForAutoComplete();
            currNode = Trie.root;               // Holds Position which TrieNode currently we are on

            // Populate Trie, Time O(k*n), K = avg length of sentences & n = no of sentences
            for (int i = 0; i < times.Length; i++)
                Trie.Add(sentences[i], times[i]);
        }

        // Public Func() using which user provides input char by char, '#' marks end of the sentence.
        // autocomplete-system search-engine searching for possible matches with each newly added characters
        // & returns list of all sentences matching the Prefix entered so far by user
        // Time is O(NLogN), which is dominated mostly by the sorting required to sort sentences (based on frequency & ASCII)
        // for all sentces who matched given preFixSoFar, N no of TrieNodes accessible from 'currNode'
        public List<string> Input(char c)
        {
            if (c == '#') // end of current sentence
            {
                // add sentence to the system, if already present increment count frequency by 1
                if (!currNode.isWord)
                {
                    currNode.isWord = true;
                    currNode.times = 1;
                }
                else
                    currNode.times++;
                
                // Reset the variable for next 'User Input'
                currNode = Trie.root;
                preFixSoFar = new StringBuilder();
                return new List<string>();
            }
            else
            {
                // No match avaliable after adding latest character to 'preFixSoFar' last TrieNode
                if (!currNode.children.ContainsKey(c))
                {
                    currNode.children.Add(c, new TrieNode());
                    currNode = currNode.children[c];
                    return new List<string>();
                }

                currNode = currNode.children[c];
                // Matching sentences will be populated to KeyValue list as key and their times/frequency as value
                matchesFound = new List<KeyValuePair<string, int>>();

                // append newest character
                preFixSoFar.Append(c);

                // Find matches
                Trie.SearchMatch(currNode, preFixSoFar.ToString(), matchesFound);   // Time O(n), n = no of TrieNodes

                // Sort Matches as per given criteria (Max Frequency if thats same
                // compare sentences char by char from start via giving more priority to ones with lower ASCII values)
                matchesFound.Sort(new AutoCompleteFrequency());                     // Time O(nLogn) for sorting

                List<string> result = new List<string>();
                // Select Top 3
                for (int i = 0; i < matchesFound.Count && i < 3; i++)               // O(3)
                    result.Add(matchesFound[i].Key);

                return result;
            }
        }
    }
    // Custom Comparator which sorts as per frequency i.e. Value if its same sort based upon string value
    // Time O(NLogN) || Space O(1)
    public class AutoCompleteFrequency : IComparer<KeyValuePair<string,int>>
    {
        public int Compare(KeyValuePair<string, int> kvp1, KeyValuePair<string, int> kvp2)
        {
            if (kvp1.Value != kvp2.Value)           // if Frequency is differenct return one with higher one
                return (kvp1.Value > kvp2.Value) ? -1 : 1;
            return kvp1.Key.CompareTo(kvp2.Key);    // if frequency was same return comparison b/w sentences
        }
    }
}
