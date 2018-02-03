using System;

namespace Create_LinkedList
{
    class Node
    {
        public int Data { get; set; }
        public Node next;
        public Node prv;

        public Node(int no)
        {
            Data = no;
            next = null;
            prv = null;
        }
    }
    class DoublyLinkedList
    {
        public Node Head;
        public DoublyLinkedList() { Head = null; }

        public void AddAtStart(int data)
        {
            Node addOne = new Node(data);   // Create New Node
            if(Head != null)
                Head.prv = addOne;          // Point the previously First Node->Previous to newly added one
            addOne.next = Head;             // Point the newly added Node->Next to previously First Node
            Head = addOne;                  // Pointing the Head of LinkedList to newly added node
        }

        public void AddAtEnd(int data)
        {
            Node addOne = new Node(data);   // Create New Node
            if (Head == null)
                Head = addOne;              // Point Head to first element in List
            else
            {
                Node Temp = Head;
                while (Temp.next != null)   // Iterate to the Last Node in List
                    Temp = Temp.next;
                Temp.next = addOne;         // Point Last Node->Next to new element
                addOne.prv = Temp;          // Point newly added Node->Previous to previously last element
            }
        }
    }
    class MainClass
    {
        public static void Main(string[] args)
        {
            DoublyLinkedList myList = new DoublyLinkedList();
            myList.AddAtStart(5);   // 5
            myList.AddAtStart(10);  // 10 5
            myList.AddAtEnd(15);    // 10 5 15
            myList.AddAtEnd(20);    // 10 5 15 20
            myList.AddAtStart(25);  // 25 10 5 15 20
            myList.AddAtEnd(30);    // 25 10 5 15 20 30

            PrintFromStart(myList);
            PrintFromEnd(myList);
            PrintFromStart(myList);
            PrintFromEnd(myList);
            Console.ReadLine();
        }

        // To Print Elements From Start
        static void PrintFromStart(DoublyLinkedList myList)
        {
            Node current = null;
            if(myList !=null)
                current = myList.Head;

            while(current!=null)
            {
                Console.Write($" {current.Data}");
                current= current.next;
            }
            Console.WriteLine();
        }

        // To Print Elements From Last
        static void PrintFromEnd(DoublyLinkedList myList)
        {
            Node current = null;
            if (myList != null)
                current = myList.Head;
            
            while (current.next != null)
                current = current.next;
            while (current!= null)
            {
                Console.Write($" {current.Data}");
                current = current.prv;
            }
            Console.WriteLine();
        }
    }
}
