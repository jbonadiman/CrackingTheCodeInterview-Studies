using Structures;
using System;
using System.Collections.Generic;

namespace CrackingTheCodeInterview
{
    class Program
    {
        static void Main(string[] args)
        {
            var linkedList = new SimpleLinkedList<int>
            {
                5,
                10,
                20,
                40
            };

            Console.WriteLine(linkedList);
            linkedList.Insert(0, 50);
            Console.WriteLine(linkedList);
            linkedList.Insert(linkedList.Count, 400);
            Console.WriteLine(linkedList);

            linkedList.Insert(2, 100);
            Console.WriteLine(linkedList);

        }
    }
}
