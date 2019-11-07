using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MarkovChain
{
    class MarkovChain
    {
        Dictionary<string, Node> wordDictionary = new Dictionary<string, Node>();
        string[] words;

        private void Markov()
        {
            string text = File.ReadAllText(@"C:\Users\agouz\Desktop\beemoviescript.txt");   //"test text. The fitness gram pacer test is a multi-stage aerobic capacity test that progressively gets more difficult as it continues. the 20 meter pacer test will begin shortly; line up at the start. Ready, begin.";
            text = text.ToLower();
            words = text.Split(" ");

            wordDictionary.Add(words[0], new Node(new Dictionary<string, int>()));
            for(int i = 1; i < words.Length; i++)
            {
                //checking if the word is not in word dictionary
                if (!wordDictionary.ContainsKey(words[i]))
                {
                    wordDictionary.Add(words[i], new Node(new Dictionary<string, int>()));
                }

                if(wordDictionary[words[i - 1]].MarkovPossibilities.ContainsKey(words[i]))
                {
                    wordDictionary[words[i - 1]].MarkovPossibilities[words[i]]++;
                }
                else
                {
                    wordDictionary[words[i - 1]].MarkovPossibilities.Add(words[i], 1);
                }
            }
        }

        private string generateWord(string currWord)
        {
            Random random = new Random();
            if(currWord == words[words.Length - 1])
            {
                currWord = words[random.Next(0, words.Length)]; 
            }



            Node currWordNode;
            if(wordDictionary.ContainsKey(currWord))
            {
                currWordNode = wordDictionary[currWord];
            }
            else
            {
                throw new Exception("either wordDictionary is empty or the current word doesn't exist in it.");
            }

            List<string> possibleWords = new List<string>();

            foreach (var word in currWordNode.MarkovPossibilities.Keys)
            {
                for (int i = 0; i < currWordNode.MarkovPossibilities[word]; i++)
                {
                    possibleWords.Add(word);
                }
            }
            int randomIndex = random.Next(0, possibleWords.Count);

            return possibleWords[randomIndex];
        }

        public void generateText(int amount)
        {

            //Markov();
            //string currWord = "test";
            //Console.Write(currWord + ' ');
            //for(int i = 0; i < amount; i++)
            //{
            //    currWord = nextWord(currWord);
            //    Console.Write(currWord + ' ');
            //}
            Markov();


            string currWord = "the";
            Console.Write(currWord + ' ');
            for(int i = 0; i < amount; i++)
            {
                currWord = generateWord(currWord);
                Console.Write(currWord + ' ');
            }
        }
    }

    public class Node
    {
        public Dictionary<string, int> MarkovPossibilities;

        public Node(Dictionary<string, int> markovProbs)
        {
            MarkovPossibilities = markovProbs;
        }
    }
}
