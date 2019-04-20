using System;
using System.Collections.Generic;
using System.IO;

namespace AlgorithmsAndStructuresByPCMS.Hash
{
    public class HashTableForSet
    {
        private const int HardDecision = 1000001;

        public static void Solve()
        {
            var answers = new List<string>();
            var hashTable = new List<int>[HardDecision];
            string[] inputData = File.ReadAllLines("set.in");

            foreach (string line in inputData)
            {
                string[] splittedData = line.Split(' ');
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
            return Math.Abs(valueToHash % HardDecision);
        }
    }
}