using System;
using System.Collections.Generic;

namespace Sock_Merchant_Problem
{
    class Program
    {
        // 1st attempt solving with loops (complexity = n square) NOT IDEAL SOLUTION
        static int sockMerchant_1(int n, int[] ar)
        {
            int count = 0;
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("== RUN no {0} ===================",i+1);
                if (ar[i] == -1)
                    continue;
                Console.WriteLine("Element {0} found at position {1}",ar[i],i);
                bool pairFound = false;
                for (int j = i + 1; j < n; j++)
                {
                    if (ar[j] == ar[i] && ar[j] != -1)
                    {
                        Console.WriteLine("It's pair found at position {0}", j);
                        count++;
                        ar[j] = -1;
                        ar[i] = -1;
                        Console.WriteLine("PAIR Count is " + count);
                        pairFound = true;
                        break;
                    }
                }
                if(!pairFound)
                    Console.WriteLine("It's pair Not found");
                Console.WriteLine("===============================");
            }
            return count;
        }
        // 2nd attempt solving with Dictonary (complexity = 2n = n)
        static int sockMerchant_2(int n, int[] ar)
        {
            int count = 0;
            Dictionary<int, int> sockPair = new Dictionary<int, int>();
            for (int i = 0; i < n; i++)
            {
                if (sockPair.TryGetValue(ar[i], out count))
                    sockPair[ar[i]]=count+1;    //Updating count of existing key
                else
                    sockPair.Add(ar[i], 1);     //Adding new key
            }
            count = 0;
            foreach(KeyValuePair<int,int> item in sockPair)
                count += item.Value / 2;
            return count;
        }
        static void Main(string[] args)
        {
            int n = 9;
            string inputStr = "10 20 20 10 10 30 50 10 20";
            int[] ar = Array.ConvertAll(inputStr.Split(' '), arTemp => Convert.ToInt32(arTemp));
            int result = sockMerchant_2(n, ar);
            Console.WriteLine("Total no of pair of socks : " + result);
            Console.ReadKey();
        }
    }
}
