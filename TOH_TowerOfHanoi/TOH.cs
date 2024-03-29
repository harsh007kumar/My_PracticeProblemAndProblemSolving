using System;
using System.Collections.Generic;

namespace TOH_TowerOfHanoi
{
    public class TOH
    {
        static void Main(string[] args)
        {
            Stack<int>[] Towers = new Stack<int>[3]{new Stack<int>(), new Stack<int>(), new Stack<int>()};
            var towerA = Towers[0];
            var towerB = Towers[1];
            var towerC = Towers[2];
            const int smallestDiskSize = 1, noOfDisk = 10, noOfDiskToBeMovedFromTop = 5;
            PopulateATower(towerA, smallestDiskSize, noOfDisk);
            PrintTowers(Towers);
            
            //Solution to TOH, move N disk from tower A to C
            MoveNDisk(noOfDiskToBeMovedFromTop, towerA, towerB, towerC);

            PrintTowers(Towers);
            Console.ReadKey();
        }
        public static void MoveNDisk(int noOfDiskToBeMovedFromTop, Stack<int> fromTower, Stack<int> viaTower, Stack<int> toTower)
        {
            var A = fromTower;
            var B = viaTower;
            var C = toTower;
            Console.Write($"\nMoving {noOfDiskToBeMovedFromTop} Disk ");
            if (noOfDiskToBeMovedFromTop > 0)
            {
                //Step1: Move N-1 disk from 'A' to 'B' using 'C'
                MoveNDisk(noOfDiskToBeMovedFromTop - 1, A, C, B);

                //Step2: Move Nth/Last disk 'A' directly to 'C'
                C.Push(A.Pop());

                //Step3: Move N-1 disk from 'B' to 'C' using 'A'
                MoveNDisk(noOfDiskToBeMovedFromTop - 1, B, A, C);
            }
        }

        public static void PrintTowers(Stack<int>[] towers)
        {
            Console.WriteLine("\nPrint contents of all towers");
            int i = 1;
            foreach (var tower in towers)
            {
                Console.Write("\nTower {0} : ",i++);
                    foreach (var disk in tower)
                        Console.Write($" {disk}");
            }
        }
        public static void PopulateATower(Stack<int> s, int smallestDiskSize, int noOfDisk)
        {
            for (int no = smallestDiskSize + noOfDisk - 1; no > smallestDiskSize - 1; no--)
                s.Push(no);
        }
    }
}
