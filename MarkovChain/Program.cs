using System;
using System.IO;

namespace MarkovChain
{
    class Program
    {
        static void Main(string[] args)
        {
            MarkovChain myMarkov = new MarkovChain();
            myMarkov.generateText(100);

            

            Console.ReadKey();
        }
    }
}
