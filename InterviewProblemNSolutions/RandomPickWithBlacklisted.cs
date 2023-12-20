using System;
using System.Collections.Generic;

namespace InterviewProblemNSolutions
{
    /* https://leetcode.com/problems/random-pick-with-blacklist/
     * Given a blacklist B containing unique integers from [0, N), write a function to return a uniform random integer from [0, N) which is NOT in B.
     * Optimize it such that it minimizes the call to system’s Math.random().
     * Note:
     * 1 <= N <= 1000000000
     * 0 <= B.length < min(100000, N)
     * [0, N) does NOT include N. See interval notation.
     */
    /* FIRST TRY [TLE on 64/67]
    public class RandomPickWithBlacklisted
    {
        Random rand;
        HashSet<int> black;
        int upperBound;

        public RandomPickWithBlacklisted(int N, int[] blacklist)
        {
            rand = new Random();
            black = new HashSet<int>();
            upperBound = N;
            for (int i = 0; i < blacklist.Length; i++)      // add all BlackList to HashSet
                black.Add(blacklist[i]);
        }

        public int Pick()
        {
            int ans = rand.Next(0, upperBound);
            while (black.Contains(ans))                     // if current ans is one of the blackliste ones search again
                ans = rand.Next(0, upperBound);
            return ans;
        }
    }
    */

    /* SECOND ATTEMPT [System.OutOfMemoryException: Insufficient memory to continue the execution on 65/67]
    public class RandomPickWithBlacklisted
    {
        Random rand;
        HashSet<int> black;
        int[] ans;

        public RandomPickWithBlacklisted(int N, int[] blacklist)
        {
            rand = new Random();                            // Random Object to fetch random indices
            black = new HashSet<int>();

            for (int i = 0; i < blacklist.Length; i++)      // add all BlackList to HashSet
                black.Add(blacklist[i]);

            ans = new int[N - blacklist.Length];
            int index = 0;
            for (int i = 0; i < N; i++)                     // add all valid numbers to new array with only valid integers
                if (!black.Contains(i))
                    ans[index++] = i;

        }

        public int Pick() => ans[rand.Next(0, ans.Length)];
    }
    */

    /* THIRD TRY[TLE on 63 / 67]
    public class RandomPickWithBlacklisted
    {
        Random rand;
        int upperBound;
        int[] black;
        public RandomPickWithBlacklisted(int N, int[] blacklist)
        {
            rand = new Random();
            upperBound = N;
            Array.Sort(blacklist);
            black = blacklist;
        }

        public int Pick()
        {
            int ans = rand.Next(0, upperBound);
            while (BSearch(ans))
                ans = rand.Next(0, upperBound);
            return ans;
        }
        // Returns true if given num exists in 'black' array else false
        bool BSearch(int num)
        {
            int start = 0, last = black.Length - 1, mid = 0;
            while (start <= last)
            {
                mid = start + (last - start) / 2;
                if (num == black[mid]) return true;
                else if (num < black[mid]) last = mid - 1;
                else start = mid + 1;
            }
            return false;
        }
    }
    */

    public class RandomPickWithBlacklisted
    {
        int whitelistCnt;
        Random random;
        Dictionary<int, int> dict;

        public RandomPickWithBlacklisted(int n, int[] blacklist)
        {

            random = new Random();

            dict = new Dictionary<int, int>(blacklist.Length);
            for (int i = 0; i < blacklist.Length; i++)
                dict[blacklist[i]] = -1;

            whitelistCnt = n - dict.Count;
            for (int i = 0; i < blacklist.Length; i++)
            {
                if (blacklist[i] < whitelistCnt)            // if black-listed number is smaller than our actual upperBound
                {
                    while (dict.ContainsKey(n - 1))         // decreament 'n' untill we find a valid number which is not present in blacklist
                        n--;
                    dict[blacklist[i]] = n - 1;             // now update the value of blacklist number in dict with a the last valid number
                    n--;                                    // decreament last valid number by 1 for next time
                }
            }
        }

        public int Pick()
        {
            int p = random.Next(whitelistCnt);
            // if random number is one which is blacklist, than return the whitelisted num saved against it from dictionary
            // else simple return the number
            return dict.ContainsKey(p) ? dict[p] : p;
        }
    }
}
