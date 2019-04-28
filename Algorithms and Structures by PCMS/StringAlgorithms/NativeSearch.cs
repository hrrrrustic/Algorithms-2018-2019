using System;
using System.Collections.Generic;
using System.IO;

namespace AlgorithmsAndStructuresByPCMS.StringAlgorithms
{
    public class NativeAlgorithm
    {
        public static void Solve(string[] args)
        {
            string[] inputData = File.ReadAllLines("search1.in");
            string text = inputData[1];
            string pattern = inputData[0];
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
                for (int j = 0; j < pattern.Length; j++)
                {
                    if (text[i + j] != pattern[j])
                        compareHelper = false;
                }
                if (compareHelper)
                {
                    startPositions.Add(i + 1);
                }
            }

            return startPositions;
        }
    }
}
