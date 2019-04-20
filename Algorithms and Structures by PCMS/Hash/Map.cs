using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Hash
{
    class HashTableforMap
    {
        static void Solve(string[] args)
        {
            const int hardDecision = 1000000;
            List<Tuple<string, string>>[] hashTable = new List<Tuple<string, string>>[hardDecision];
            List<string> answers = new List<string>();
            string[] inputData = File.ReadAllLines("map.in");
            for (int i = 0; i < inputData.Length; i++)
            {
                string[] inputSplitted = inputData[i].Split(' ');
                string key = inputSplitted[1];
                string value = inputSplitted[2];
                string command = inputSplitted[0];
                int position = Math.Abs((int)((GetHash(key)) % hardDecision));
                bool contains = false;
                switch (command)
                {
                    case "put":
                        if (hashTable[position] == null)
                        {
                            hashTable[position] = new List<Tuple<string, string>>();
                        }
                        for (int j = 0; j < hashTable[position].Count; j++)
                        {
                            if (hashTable[position][j].Item1 == key)
                            {
                                hashTable[position][j] = Tuple.Create(hashTable[position][j].Item1, value);
                                contains = true;
                                break;
                            }
                        }
                        if (!contains)
                        {
                            hashTable[position].Add(Tuple.Create(key, value));
                        }
                        break;
                    case "get":
                        if (hashTable[position] != null)
                        {
                            for (int j = 0; j < hashTable[position].Count; j++)
                            {
                                if (hashTable[position][j].Item1 == key)
                                {
                                    answers.Add(hashTable[position][j].Item2);
                                    contains = true;
                                    break;
                                }
                            }
                        }
                        if (!contains)
                        {
                            answers.Add("none");
                        }
                        break;
                    case "delete":
                        if (hashTable[position] != null)
                        {
                            for (int j = 0; j < hashTable[position].Count; j++)
                            {
                                if (hashTable[position][j].Item1 == key)
                                {
                                    hashTable[position].RemoveAt(j);
                                    break;
                                }
                            }
                        }
                        break;
                }
            }
            File.WriteAllText("map.out", string.Join("\r\n", answers));
        }
        static ulong GetHash(string valueToHash)
        {
            ulong PrimeNumber = 1;
            const ulong collisionDecreaser = (ulong)1e9 + 7; // Скорее всего не нужно
            ulong hashResult = 0;
            for (int i = 0; i < valueToHash.Length; i++)
            {
                hashResult += ((ulong)(valueToHash[i] - 'A' + 1) * PrimeNumber);
                PrimeNumber = PrimeNumber * 31;
            }
            return hashResult % collisionDecreaser;
        }
    }
}   