using System;
using System.Collections.Generic;
using System.IO;

namespace AlgorithmsAndStructuresByPCMS.Hash
{
    public class HashTableForMap
    {
        public static void Solve()
        {
            const int hardDecision = 1000000;

            var hashTable = new List<Tuple<string, string>>[hardDecision];
            var answers = new List<string>();
            string[] inputData = File.ReadAllLines("map.in");

            foreach (string line in inputData)
            {
                string[] args = line.Split(' ');

                string command = args[0];
                string key = args[1];

                int position = Math.Abs((int)((GetHash(key)) % hardDecision));
                
                switch (command)
                {
                    case "put":
                        string value = args[2];
                        PutCommand(hashTable, position, key, value);
                        break;
                    case "get":
                        GetCommand(hashTable, position, key, answers);
                        break;
                    case "delete":
                        DeleteCommand(hashTable, position, key);
                        break;
                }
            }
            File.WriteAllText("map.out", string.Join("\r\n", answers));
        }

        private static void PutCommand(List<Tuple<string, string>>[] hashTable, int position, string key, string value)
        {
            if (hashTable[position] == null)
            {
                hashTable[position] = new List<Tuple<string, string>>();
            }

            for (int j = 0; j < hashTable[position].Count; j++)
            {
                if (hashTable[position][j].Item1 == key)
                {
                    hashTable[position][j] = Tuple.Create(hashTable[position][j].Item1, value);
                    return;
                }
            }

            // If don't contain key
            hashTable[position].Add(Tuple.Create(key, value));
        }

        private static void GetCommand(List<Tuple<string, string>>[] hashTable, int position, string key, List<string> answers)
        {
            if (hashTable[position] != null)
            {
                for (int j = 0; j < hashTable[position].Count; j++)
                {
                    if (hashTable[position][j].Item1 == key)
                    {
                        answers.Add(hashTable[position][j].Item2);
                        return;
                    }
                }
            }

            // If don't contain key
            answers.Add("none");
        }

        private static void DeleteCommand(List<Tuple<string, string>>[] hashTable, int position, string key)
        {
            if (hashTable[position] != null)
            {
                for (int j = 0; j < hashTable[position].Count; j++)
                {
                    if (hashTable[position][j].Item1 == key)
                    {
                        hashTable[position].RemoveAt(j);
                        return;
                    }
                }
            }
        }

        private static ulong GetHash(string valueToHash)
        {
            const ulong collisionDecreaser = (ulong)1e9 + 7; // Скорее всего не нужно

            ulong primeNumber = 1;
            ulong hashResult = 0;

            foreach (char element in valueToHash)
            {
                hashResult += (ulong)(element - 'A' + 1) * primeNumber;
                primeNumber *= 31;
            }
            return hashResult % collisionDecreaser;
        }
    }
}