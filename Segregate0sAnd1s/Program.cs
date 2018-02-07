using System;

namespace Segregate0sAnd1s
{
    class MainClass
    {
        // You are given an array of 0s and 1s in random order. Segregate 0s on left side and 1s on right side of the array. Traverse array only once.

        // Input array   =  [0, 1, 0, 1, 0, 0, 1, 1, 1, 0] 
        // Output array =  [0, 0, 0, 0, 0, 1, 1, 1, 1, 1] 

        public static void Main(string[] args)
        {
            int[] arr = { 0, 1, 0, 0, 1, 0, 0, 1, 1, 1, 0 };

            Method2_Counting0s(arr);
            Method1_SwappingAdjacent0sAnd1s(arr);

            Console.ReadLine();
        }


        private static void Method1_SwappingAdjacent0sAnd1s(int[] arr)
        {
            int length = arr.Length;
            for (int x = 1; x < length; x++)
            {
                if (arr[x] < arr[x - 1])
                {
                    int temp = arr[x - 1];
                    arr[x - 1] = arr[x];
                    arr[x] = temp;
                    if (x > 2)
                        x -= 2;
                }
            }
            Print(arr);
        }

        private static void Method2_Counting0s(int[] arr)
        {
            int length = arr.Length, count = 0;
            for (int x = 0; x < length; x++)
            {
                if (arr[x] == 0)
                    count++;    // Just count no of Zero's in array
            }
            while(length>0)
            {
                if (count > 0)
                {
                    Console.Write("{0} ", 0);
                    count--;
                }
                else
                    Console.Write("{0} ", 1);
                length--;
            }
            Console.WriteLine();    
        }
        static void Print(int[] arr)
        {
            foreach (int i in arr)
                Console.Write("{0} ", i);
            Console.WriteLine();
        }
    }
}
