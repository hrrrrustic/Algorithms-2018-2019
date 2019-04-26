using System;
using System.Collections.Generic;
using System.IO;

namespace StringAlgorithms
{
    public class NativeAlgorithm
    {
        public static void Solve(string[] args)
        {
            string[] inputData = File.ReadAllLines("search1.in");
            string text = inputData[1];
            string pattern = inputData[0];
            if(pattern.Length > inputData[1].Length)
            {
                Console.WriteLine(0);
                return;
            }
            List<int> answers = NativeSearch(text, pattern);
            Console.WriteLine(answers.Count);
            Console.WriteLine(string.Join(" ", answers));
        }
        private static List<int> NativeSearch(string text, string pattern)
        {
            List<int> startPositions = new List<int>();
            for (int i = 0; i < text.Length - pattern.Length + 1; i++)
            {
                bool compareHelper = true;
                if (text[i] == pattern[0])
                {
                    int k = i;
                    for (int j = 1; j < pattern.Length; j++)
                    {
                        if (!(text[k + 1] == pattern[j]))
                            compareHelper = false;
                        k++;
                    }
                    if (compareHelper)
                    {
                        startPositions.Add(i + 1);
                    }
                }
            }

            return startPositions;
        }
    }
}
