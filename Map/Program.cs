using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Map
{
    class Program
    {
        static void Main(string[] args)
        {
            const int hardDecision = 1000000;
            List<Tuple<string, string>>[] space = new List<Tuple<string, string>>[hardDecision];
            List<string> answers = new List<string>();
            string[] input = File.ReadAllLines("map.in");
            for (int i = 0; i < input.Length; i++)
            {
                string[] inputSplitted = input[i].Split(' ');
                int position = Math.Abs((int)((GetHash(inputSplitted[1])) % hardDecision));
                switch (inputSplitted[0])
                {
                    case "put":
                        bool item = true;
                        if (space[position] == null)
                        {
                            space[position] = new List<Tuple<string, string>>();
                        }
                        for (int j = 0; j < space[position].Count; j++)
                        {
                            if (space[position][j].Item1 == inputSplitted[1])
                            {
                                space[position][j] = Tuple.Create(space[position][j].Item1, inputSplitted[2]);
                                item = false;
                                break;
                            }
                        }
                        if (item)
                        {
                            space[position].Add(Tuple.Create(inputSplitted[1], inputSplitted[2]));
                        }
                        break;
                    case "get":
                        bool item1 = true;
                        if (space[position] != null)
                        {
                            for (int j = 0; j < space[position].Count; j++)
                            {
                                if (space[position][j].Item1 == inputSplitted[1])
                                {
                                    answers.Add(space[position][j].Item2);
                                    item1 = false;
                                    break;
                                }
                            }
                        }
                        if (item1)
                        {
                            answers.Add("none");
                        }
                        break;
                    case "delete":
                        if (space[position] != null)
                        {
                            for (int j = 0; j < space[position].Count; j++)
                            {
                                if (space[position][j].Item1 == inputSplitted[1])
                                {
                                    space[position].RemoveAt(j);
                                    break;
                                }
                            }
                        }
                        break;
                }

            }
            using (var outFile = new StreamWriter("map.out"))
            {
                outFile.WriteLine(string.Join("\r\n", answers));
            }
        }
        static ulong GetHash(string needToHash)
        {
            ulong p = 1;
            ulong dwa = (ulong)1e9 + 7;
            ulong hash = 0;
            for (int i = 0; i < needToHash.Length; i++)
            {
                hash += ((ulong)(needToHash[i] - 'A' + 1) * p);
                p = p * 31;
            }
            return hash % dwa;
        }
    }
}   