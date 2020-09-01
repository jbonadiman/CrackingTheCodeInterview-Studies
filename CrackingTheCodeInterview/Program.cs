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

            int[] array = new int[linkedList.Count];
            linkedList.CopyTo(array, 3);

            Console.WriteLine(string.Join(" <> ", array));

        }
    }
}
