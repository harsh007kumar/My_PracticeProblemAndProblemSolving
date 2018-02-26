using System;
using System.Text;

namespace Lexicographical_ConcatenationOfAllSubstrings
{
    // Lexicographical concatenation of all substrings of a string | GeeksforGeeks
    // https://www.youtube.com/watch?v=iUkj6mKrKSw

    class MainClass
    {
        public static void Main(string[] args)
        {
            string input = "abc";
            Console.WriteLine(Lexicographical_Concatenation(input));

            Console.ReadLine();
        }

        private static string Lexicographical_Concatenation(string input)
        {
            StringBuilder sb = new StringBuilder();
            int len = input.Length,i,j;
            for (i = 0; i < len;i++)
            {
                for (j = 1; j <= len-i; j++)
                {
                    Console.WriteLine(input.Substring(i,j));
                    sb.Append(input.Substring(i, j));
                }
            }

            return sb.ToString();
        }
    }
}
