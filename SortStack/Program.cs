using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortStack
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> str = new Stack<int>();
            str.Push(1);
            str.Push(4);
            str.Push(2);
            str.Push(28);
            str.Push(6);
            str.Push(7);
            str.Push(3);

            for(int i =0;i<str.Count;i++)
                SortTheStack(str);  
        }

        private static void SortTheStack(Stack<int> str)
        {
            if (str.Count >= 2)
            {
                int top = str.Pop();
                int currentTop = str.Pop();
                if (currentTop > top)
                {

                    str.Push(top);
                    SortTheStack(str);
                    str.Push(currentTop);
                }
                else
                {

                    str.Push(currentTop);
                    SortTheStack(str);
                    str.Push(top);
                }
            }

        }
        
    }
}
