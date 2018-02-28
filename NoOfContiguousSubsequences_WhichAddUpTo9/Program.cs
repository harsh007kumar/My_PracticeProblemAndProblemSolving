using System;

namespace NoOfContiguousSubsequences_WhichAddUpTo9
{
    // https://youtu.be/tbMEj5VWShg
    // Find the number of contiguous subsequences which recursively add up to 9 | GeeksforGeeks

    // Ex: I/p=> 4189 O/p=> 18,189,9 i.e, 3
    // Ex: I/p=> 999 O/p=> 9,99,999,9,99,9 i.e, 6

    class MainClass
    {
        public static void Main(string[] args)
        {
            string input = "999"; // 4189 = 3, 999 = 6
            int result = ContiguousSubsequencesWithSumAs9(input);
            Console.WriteLine(result);

            Console.ReadLine();
        }

        private static int ContiguousSubsequencesWithSumAs9(string input)
        {
            int ln = input.Length, i, j, x, count = 0;
            for (i = 0; i < ln;i++)
            {
                for (j = 1; j <= ln - i;j++)
                {
                    x = 0;
                    int.TryParse(input.Substring(i,j),out x);
                    if (x % 9 == 0)
                        count++;
                }
            }
            return count;
        }
    }
}
