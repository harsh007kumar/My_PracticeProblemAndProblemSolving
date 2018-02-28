using System;

namespace NoOfFactorialHaving_N_ZerosAtEnd
{
    // No Of Factorials having 'N' zeros at end

    class MainClass
    {
        public static void Main(string[] args)
        {
            int NoOfZeros;
            Console.Write("How many Zeros you want at the end\t");
            Int32.TryParse(Console.ReadLine(),out NoOfZeros);
            Console.WriteLine("\nTotal no of Numbers whose Factorial have '{0}' or more Zeros at the End are => {1}", NoOfZeros, NoOfFactorialHaving_N_ZerosAtEnd(NoOfZeros));
            Console.ReadLine();
        }

        private static int NoOfFactorialHaving_N_ZerosAtEnd(int noOfZerosRequired)
        {
            int count = 0, limit = 100, i, fact;
            for (i = 1; i < limit;i++)
            {
                fact = Factorial(i);
                if (ContainsNZeros(fact, noOfZerosRequired) == true)
                {
                    count++;
                    Console.WriteLine("No {0} has Factorial {1}",i,fact);
                }

            }
            return count;
        }

        static int Factorial(int no)
        {
            int factorial = 1;
            while(no>1)
            {
                factorial *= no;
                no -= 1;
            }
            return factorial;
        }

        static bool ContainsNZeros(int no, int noOfZeroRequired)
        {
            bool b = false;
            int noOfZerosAtEndOfThisNo = 0;
            while (no > 0)
            {
                if (no % 10 == 0)
                    noOfZerosAtEndOfThisNo++;
                else
                    break;
                
                if (noOfZerosAtEndOfThisNo == noOfZeroRequired)
                {
                    b = true;
                    break;
                }

                no /= 10;
            }
            return b;
        }
    }
}
