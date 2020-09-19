using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewProblemNSolutions
{
    public class PetrolPump
    { 
        public int Petrol, Distance;
        public PetrolPump(int petrol, int distance)     // Constructor
        { Petrol = petrol; Distance = distance; }
    }

    public class DailyProblem
    {
        // Time O(n) || Space O(1)
        // Simpler approach would be to use a Queue to keep track of pump added in order while current fuel level >= 0,
        // else start Dequeuing Pumps from the start untill fuel level is >= 0 again
        public static int PetrolPump(List<PetrolPump> pumps, int len)
        {
            if (len < 1) return -1;
            int startAt = 0, endAt = 1;
            int currFuel = (pumps[startAt].Petrol - pumps[startAt].Distance);

            // breaking condition is start iterated thru each possible pump in array and endAt has't came around to be same as start
            while (startAt < len)
            {
                while (currFuel >= 0)
                {
                    currFuel += (pumps[endAt].Petrol - pumps[endAt].Distance);           // add next pump
                    endAt = (endAt + 1) % len;                  // update end index
                    if (endAt == startAt) return startAt;       // found the 'starting pump index'
                }
                while (currFuel < 0 && startAt < len)
                {
                    currFuel -= (pumps[startAt].Petrol - pumps[startAt].Distance);      // remove start pump
                    startAt++;                                  // update start index
                }
            }
            return -1;      // start reached end of array still did not found appropriate starting index to complete the loop
        }


        /// <summary>
        /// Time O(n*m), where n = length of first number and m = length of second number || Space O(1)
        /// Returns multiplication of two numbers very long numbers(can't fit in memory, numbers can be of 500+ digits long)
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="len1"></param>
        /// <param name="num2"></param>
        /// <param name="len2"></param>
        /// <returns></returns>
        public static string MultiplyLargeNumbersRepresentedAsString(string num1,int len1,string num2, int len2)
        {
            if (len1 < len2)
                MultiplyLargeNumbersRepresentedAsString(num2, len2, num1, len1);
            /* Keep longer no as num1
             * List of string to stored multiplication with each digit of num2 (second num)
             * for each digit in num2
             *      multipy each digit and keep carry which would be added to (mulitplication of next digits)
             *      at end if we have carry add it to the front of last multiplication result.
             * once we have multiplication with each digit, perform sum of all string stored in list, keep carry
             */

            string[] listOfMultiplications = new string[len2];

            // reversing the num2 so its easy to pick the digits one by one from start and multiply by num1
            num2 = ReverseString(num2);

            // fill 'listOfMultiplications' || Time O(len2*len1)
            for (int i = 0; i < len2; i++)
            {
                // append appropriate zeros to end of the string
                AppendZeros(ref listOfMultiplications[i], i);

                // multiply each digit and keep the carry which would be added to mulpilication of next digit
                int carry = 0;
                for (int j = len1 - 1; j >= 0; j--)             // starting from least significant digit
                {
                    int singlesMultiply = (int)Char.GetNumericValue(num2[i]) * (int)Char.GetNumericValue(num1[j]);
                    var carryAdded = singlesMultiply + carry;
                    listOfMultiplications[i] = carryAdded % 10 + listOfMultiplications[i];
                    carry = carryAdded / 10;
                }
                // append any left over carry to the result
                if(carry>0) listOfMultiplications[i] = carry + listOfMultiplications[i];
            }

            // Now we have the result of multiplications with individual digits at each index,
            // sum all these rows(list of strings to get final result)
            return SumUpNumbersStoredAsListOfStrings(listOfMultiplications, len2);
        }

        public static string SumUpNumbersStoredAsListOfStrings(string[] numList, int len)
        {
            for(int i=1;i<len;i++)
            {
                // if there is difference in len append extra 0 to start of the numbers to make them of same length
                var diffLen = numList[i].Length - numList[i - 1].Length;
                if (diffLen > 0)
                    AppendZeros(ref numList[i - 1], diffLen);
                else if (diffLen < 0)
                    AppendZeros(ref numList[i], Math.Abs(diffLen));

                int carry = 0;
                string sum = "";
                for (int j = numList[i].Length - 1; j >= 0; j--)
                {
                    int digitsSum = (int)Char.GetNumericValue(numList[i][j]) + (int)Char.GetNumericValue(numList[i-1][j]);
                    var carryAdded = digitsSum + carry;
                    sum = carryAdded % 10 + sum;
                    carry = carryAdded / 10;
                }
                // append any left over carry to the result
                if (carry > 0) sum = carry + sum;
                numList[i] = sum;
            }
            return numList[len-1];          // last string now has sum of all the numbers above it in the list
        }
        
        public static string ReverseString(string str)
        {
            var charArr = str.ToCharArray();
            Array.Reverse(charArr);
            return new string(charArr);
        }

        public static void AppendZeros(ref string s1, int count)
        {
            for (int i = 0; i < count; i++)
                s1 = "0" + s1;
        }

    }
}
