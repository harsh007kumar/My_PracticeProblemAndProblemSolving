using System;
using System.Text;

namespace Currency_Conversion_From_NumbersToWords
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            StringBuilder input = new StringBuilder("123456789315654678");
            Console.WriteLine(ConvertCurrencyToWords(input.ToString()));
            Console.WriteLine($"{input}");
        }
        static string ConvertCurrencyToWords(string currency)
        {
            //1,000 1thousand (1grp of 3 zero's)
            //1,000,000 1 million (2 grp of 3 zero's)
            //1,000,000,000 1 billion (3 grp of 3 zero's)
            //1,000,000,000,000 1 trillion (4 grp of 3 zero's)
            //1,000,000,000,000,000 1 Quadrillion (5 grp of 3 zero's)

            string output = string.Empty, temp = string.Empty;;
            int ln = currency.Length;
            while (ln> 0)
            {
                if (ln > 15)
                {
                    temp = " "+currency.Substring(0, ln - 15) + " Quadrillion";
                    output += temp;
                    currency = currency.Substring(ln - 15);
                }
                else if (ln > 12)
                {
                    temp = " "+currency.Substring(0, ln - 12) + " trillion";
                    output += temp;
                    currency = currency.Substring(ln - 12);
                }
                else if (ln > 9)
                {
                    temp = " "+currency.Substring(0, ln - 9) + " billion";
                    output += temp;
                    currency = currency.Substring(ln - 9);
                }
                else if (ln > 6)
                {
                    temp = " "+currency.Substring(0, ln - 6) + " million";
                    output += temp;
                    currency = currency.Substring(ln - 6);
                }
                else if (ln > 3)
                {
                    temp = " "+currency.Substring(0, ln - 3) + " thousand";
                    output += temp;
                    currency = currency.Substring(ln - 3);
                }
                else if (ln > 2)
                {
                    temp = " "+currency.Substring(0, ln-2) + " hundred";
                    if (Convert.ToInt16(temp.Substring(1, 1)) != 0)
                        output += temp;
                    currency = currency.Substring(ln - 2);
                }
                else if(ln>1)
                {
                    int no = Convert.ToInt32(currency.Substring(0, 1));

                    if (no != 1 && no != 0)
                    {
                        temp = " " + (Twos)no;
                        output += temp;
                        currency = currency.Substring(ln - 1);
                    }
                    else if (no == 1)
                    {
                        no = Convert.ToInt32(currency.Substring(0, 2));
                        temp = " " + (Word)no;
                        output += temp;
                        currency = string.Empty;
                    }
                    else
                        currency = currency.Substring(ln - 1);
                }
                else if (ln > 0)
                {
                    temp = " " + (Ones)Convert.ToInt32(currency.Substring(0));
                    if (temp.Substring(1, 4) != "zero")
                        output += temp;
                    currency = string.Empty;
                }
                ln = currency.Length;
            }
            return output;
        }
        enum Twos
        {
            ninty = 9,
            eighty = 8,
            seventy = 7,
            sixty = 6,
            fifty = 5,
            forthy = 4,
            thirty = 3,
            twenty = 2,
            XXX = 1,
            zero = 0
        }
        enum Ones
        {
            nine = 9,
            eight = 8,
            seven = 7,
            six = 6,
            five = 5,
            four = 4,
            three = 3,
            two = 2,
            one = 1,
            zero = 0
        }
        enum Word
        {
            ten = 10,
            eleven = 11,
            twelve = 12,
            thirteen = 13,
            fourteen = 14,
            fifteen = 15,
            sixteen = 16,
            seventeen = 17,
            eighteen = 18,
            ninteen = 19
        }
    }
}
