using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Set
{
    class Program
    {
        private const int hardDecision = 1000001;
        static void Main(string[] args)
        {
            List<string> Answers = new List<string>();                      
            List<int>[] space = new List<int>[hardDecision];              
            string[] input = File.ReadAllLines("set.in");                
            for (int i = 0; i < input.Length; i++)
            {                                                         
                string[] inputSplitted = input[i].Split(' ');             
                int value = int.Parse(inputSplitted[1]);
                int position = GetHash(value);
                switch (inputSplitted[0])
                {
                    case "insert":                                                  
                        if (space[position] == null)
                        {
                            space[position] = new List<int>();
                        }
                        if (space[position].Contains(value) == false)
                        {
                            space[position].Add(value);
                        }
                        break;

                    case "delete":
                        space[position]?.Remove(value);
                        break;
                    case "exists":
                        Answers.Add((space[position]?.Contains(value) ?? false).ToString().ToLower());
                        break;
                }
            }
            using (var outFile = new StreamWriter("set.out"))
            {
                outFile.WriteLine(string.Join("\r\n", Answers));
            }
        }

        static int GetHash(int a)
        {
            int hash;
            hash = a % hardDecision;
            return Math.Abs(hash);
        }
    }
}