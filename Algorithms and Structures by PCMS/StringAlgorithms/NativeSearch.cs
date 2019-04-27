using System;
using System.Collections.Generic;
using System.IO;

//TODO:8 Неймспейсы
namespace StringAlgorithms
{
    public class NativeAlgorithm
    {
        public static void Solve(string[] args)
        {
            string[] inputData = File.ReadAllLines("search1.in");
            string text = inputData[1];
            string pattern = inputData[0];
            //TODO:5 Костыль, вроде как. У тебя же и так метод нормально обработает эту ситуацию, не?
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
                //TODO:7 А зачем отдельно выделать этот if, если это просто первый шаг for'а?
                if (text[i] == pattern[0])
                {
                    int k = i;
                    for (int j = 1; j < pattern.Length; j++)
                    {
                        //TODO:6 !(a == b) можно упростить
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
