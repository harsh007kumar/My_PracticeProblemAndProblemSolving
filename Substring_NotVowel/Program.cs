using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Substring_NotVowel
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("ritikisagoodboy");
            int length = sb.Length;
            int x = 4;
            int y = x;
            List<string>[] srr = new List<string>[sb.Length-x];

            //Char[] Ch = { 'a', 'e', 'i', 'o', 'u' };

            List<int> arr = new List<int>(); ;
            //int final = 0;
            int NonVowel;
            for (int j = 0; j < length-y; j++)
            {
                arr.Clear();
                //arr = new List<int>();
                NonVowel = 0;
                int count = 0;
                srr[j] = new List<string>();
                for (int i = 0; i <= (length - x); i++)
                {
                    string st = sb.ToString().Substring(i, x);
                    srr[j].Add(st);

                    //final = st.Length;
                    //Console.Write(st+" : ");

                    #region Check for Vowel
                    ////Checking each charater in the string is Vowel or not
                    //foreach (char chr in st.ToCharArray())
                    //{
                    //    if (!Ch.Contains(chr))
                    //    { NonVowel++; }
                    //}
                    foreach (char chr in st.ToCharArray())
                    {
                        if (!(chr == 'a' || chr == 'e' || chr == 'i' || chr == 'o' || chr == 'u'))
                            NonVowel++;
                    }
                    
                    #endregion

                    arr.Add(NonVowel);
                    if (NonVowel >= y)
                        count++;
                    //Console.WriteLine(NonVowel);
                    NonVowel = 0;

                }
                //final=srr[j][0].Length;
                if (count == arr.Count)
                {
                    Console.WriteLine(srr[j][0].Length);//MAIN OUTPUT
                    return;
                }
                //final = 0;
                //.WriteLine("i {0}",j);
                if (x < length-1)
                    x++;

            }

            Console.WriteLine("-1");
            Console.ReadLine();
        }
        static bool CheckConsonant(string st)
        {
            foreach (char chr in st.ToCharArray())
            {
                if (!(chr == 'a' || chr == 'e' || chr == 'i' || chr == 'o' || chr == 'u'))
                    return true;
            }
            return false;
        }
    }
}
