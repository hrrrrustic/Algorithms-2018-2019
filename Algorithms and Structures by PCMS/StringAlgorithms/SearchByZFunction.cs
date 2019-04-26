using System;
using System.IO;

namespace StringAlgorithms
{
   public class ZFunction
    {
        public static void Solve(string[] args)
        {
            string[] inputData = File.ReadAllLines("search2.in");
            string pattern = inputData[0];
            string text = inputData[1];
            int[] zFunction = SearchByZFunction(text, pattern, '#');
            OutPut(zFunction, pattern.Length);
        }
        private static int[] SearchByZFunction(string text, string pattern, char specialSymbol)
        {
            text = pattern + specialSymbol + text;
            int[] zFunction = new int[text.Length];
            int leftPositionOfBlock = 0, rightPositionOfBlock = 0;
            for (int i = 1; i < text.Length; i++)
            {
                zFunction[i] = Math.Max(0, Math.Min((rightPositionOfBlock - i), zFunction[i - leftPositionOfBlock]));
                while (i + zFunction[i] < text.Length && text[zFunction[i]] == text[i + zFunction[i]])
                    zFunction[i]++;
                if (i + zFunction[i] > rightPositionOfBlock)
                {
                    leftPositionOfBlock = i;
                    rightPositionOfBlock = i + zFunction[i];
                }
            }
            return zFunction;
        }
        private static void OutPut(int[] zFunction, int patternLength)
        {
            int countOfIngoing = 0;
            for (int i = patternLength; i < zFunction.Length; i++)
            {
                if (zFunction[i] == patternLength)
                    countOfIngoing++;
            }
            Console.WriteLine(countOfIngoing);
            for (int i = patternLength; i < zFunction.Length; i++)
            {
                if (zFunction[i] == patternLength)
                    Console.Write((i - patternLength) + " ");
            }
        }
    }
}
