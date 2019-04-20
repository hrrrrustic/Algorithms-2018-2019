using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Hash
{
    class HashTableForSet
    {
        private const int hardDecision = 1000001;
        static void Solve(string[] args)
        {
            List<string> answers = new List<string>();                      
            List<int>[] hashTable = new List<int>[hardDecision];              
            string[] inputData = File.ReadAllLines("set.in");                
            for (int i = 0; i < inputData.Length; i++)
            {                                                         
                string[] splittedData = inputData[i].Split(' ');
                string command = splittedData[0];
                int value = int.Parse(splittedData[1]);
                int position = GetHash(value);
                switch (command)
                {
                    case "insert":                                                  
                        if (hashTable[position] == null)
                        {
                            hashTable[position] = new List<int>();
                        }
                        if (!hashTable[position].Contains(value))
                        {
                            hashTable[position].Add(value);
                        }
                        break;

                    case "delete":
                        hashTable[position]?.Remove(value);
                        break;
                    case "exists":
                        answers.Add((hashTable[position]?.Contains(value) ?? false).ToString().ToLower());
                        break;
                }
            }
            File.WriteAllText("set.out", string.Join("\r\n", answers));
        }

        private static int GetHash(int valueToHash)
        {
            return Math.Abs(valueToHash % hardDecision);
        }
    }
}