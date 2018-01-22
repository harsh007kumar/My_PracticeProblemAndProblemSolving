using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Game_of_Chance
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = GetInput();

            int[] Output = StudentCount(input);
            PrintResult(Output);
            Console.ReadKey();
        }

        static int[] StudentCount(string[] intput1)
        {
            int NoOfSample, NoOfDays, NoOfRewardsPoint, NoOfEmployees, Winners;
            int.TryParse(intput1[0], out NoOfSample);
            int[] output = new int[NoOfSample];
            for (int i = 1; i <= NoOfSample; i++)
            {
                var tuple = ReadSample(intput1[i]);
                NoOfDays = tuple.Item1;
                NoOfRewardsPoint = tuple.Item2 / 100;
                NoOfEmployees = tuple.Item3;
                bool Can = true;
                Winners = 0;
                while (NoOfDays > 0)
                {
                    int remaninigPoints = NoOfRewardsPoint - NoOfDays;
                    if (remaninigPoints >= 0 && NoOfEmployees > 0)
                    {
                        Can &= true;
                        NoOfRewardsPoint = remaninigPoints;
                        Winners++;
                    }
                    else
                    {
                        if (NoOfDays > 0)
                            Can &= true;
                        else
                            Can &= false;
                    }
                    NoOfEmployees--;
                    NoOfDays--;
                }
                output[i - 1] = Winners;
            }

            return output;
        }
        static Tuple<int, int, int> ReadSample(string s)
        {
            return Tuple.Create(Convert.ToInt32(s.Substring(0, 1)), Convert.ToInt32(s.Substring(2, 3)), Convert.ToInt32(s.Substring(6, 1)));
        }
        static string[] GetInput()
        {
            // Fetching input
            Dictionary<int, string> UserInput = ReadFromFile();
            string[] str = new string[UserInput.Count];
            foreach (KeyValuePair<int, string> var in UserInput)
            {
                string value;
                if (var.Key > 1)
                    value = var.Value.Substring(1, 7);
                else
                    value = var.Value;
                str[var.Key - 1] = value;
            }
            return str;
        }

        static void PrintResult(int[] output)
        {
            for (int i = 0; i < output.Length; i++)
            {
                Console.WriteLine(output[i]);
            }
        }
        static Dictionary<int, string> ReadFromFile()
        {
            string line;
            int lineCounter = 0;

            Dictionary<int, string> userInput = new Dictionary<int, string>();
            StreamReader inputStream = new StreamReader(@"..\..\Input_Game_of_Chance.txt");
            while ((line = inputStream.ReadLine()) != null)
            {
                lineCounter++;
                userInput.Add(lineCounter, line);
                Console.WriteLine("Line {0} : {1}", lineCounter, line);
            }
            return userInput;
        }
    }
}
