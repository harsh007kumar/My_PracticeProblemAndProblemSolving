using System;

namespace LargestNoNotAPerfectSquare
{
    // https://youtu.be/WRh6MmQLnpk
    // Largest number that is not a perfect square | GeeksforGeeks

    // I/P=> {16,20,25,2,3,10} O/P=> 20
    // I/P=> {16,36,64} O/P=> -1

    class MainClass
    {
        public static void Main(string[] args)
        {
            Int32[] arr = { 16, 20, 25, 2, 3, 10 };
            int ans = FindLargestNoNotAnPerfectSquare(arr);
            Console.WriteLine(ans);
            Console.ReadLine();
        }

        private static int FindLargestNoNotAnPerfectSquare(int[] arr)
        {
            int ln = arr.Length, i, max = -1;
            for (i = 0; i < ln;i++)
            {
                int num  = (int)Math.Sqrt(arr[i]);
                if(num*num != arr[i])
                {
                    if (max < arr[i])
                        max = arr[i];
                }
            }
            return max;
        }

    }
}
