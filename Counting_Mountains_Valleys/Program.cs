using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace Counting_Mountains_Valleys
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfSteps = 8;
            string path = "UDDDUDUU";//UDDDUDUUDUDDUDUU
            int result = countingValleys_2(numberOfSteps, path);
            Console.WriteLine("Total no of valley(s) Gary covered : " + result);
            Console.ReadKey();
        }
        // 1st attempt, Function to count valley's
        private static int countingValleys(int numberOfSteps, string path)
        {
            int current_level = 0, noOfValleys = 0, noOfMountains = 0;
            char[] UpHillDownHill = path.ToCharArray();
            //Stack<char> GarysSteps = new Stack<char>();
            foreach(char step in UpHillDownHill)
            {
                if(current_level==0)
                {
                    if (step == 'U')
                    {
                        noOfMountains++;
                        current_level++;
                    }
                    else
                    {
                        noOfValleys++;
                        current_level--;
                    }
                }
                else
                {
                    if (step == 'U')
                        current_level++;
                    else if(step == 'D')
                        current_level--;
                }
            }
            return noOfValleys;
        }
        // 2nd attempt, Function to count valley's
        private static int countingValleys_2(int numberOfSteps, string path)
        {
            int current_level = 0, noOfValleys = 0, noOfMountains = 0;
            char[] UpHillDownHill = path.ToCharArray();
            foreach (char step in UpHillDownHill)
            {
                if (step == 'U')
                {
                    if (current_level == 0)
                        noOfMountains++;
                    current_level++;
                }
                else
                {
                    if (current_level == 0)
                        noOfValleys++;
                    current_level--;
                }
            }
            return noOfValleys;
        }
    }
}
