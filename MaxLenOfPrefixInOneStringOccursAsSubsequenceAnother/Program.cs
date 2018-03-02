using System;

namespace MaxLenOfPrefixInOneStringOccursAsSubsequenceAnother
{
    // https://youtu.be/PKtKEKsCcKk
    // Maximum length prefix of one string that occurs as subsequence in another | GeeksforGeeks

    // I/P=> s = digger t = biggerdiagram
    // O/P=> 3(dig)

    // I/P=> s = geeksforgeeks t = agbcedfeitk
    // O/P=> 4(geek)
    class MainClass
    {
        public static void Main(string[] args)
        {
            string s = "geeksforgeeks", t = "agbcedfeitk";
            Console.WriteLine($"1st I/P = {s}\n2nd I/P = {t}");
            int max = FindLongestPrefixWhichIsSubsequenceInAnother(s, t);
            Console.WriteLine($" is the world with length {max} which is the longest prefix of 1st string that occurs as subsequence in 2nd");
        }

        private static int FindLongestPrefixWhichIsSubsequenceInAnother(string s, string t)
        {
            int l2 = t.Length, i, count = 0;
            for (i = 0; i < l2; i++)
            {
                if (s[0 + count] == t[i])
                    count++;
            }
            Console.Write("\n'{0}'",s.Substring(0,count));
            return count;
        }
    }
}
