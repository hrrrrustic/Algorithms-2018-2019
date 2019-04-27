using System;
using System.IO;

namespace AlgorithmsAndStructuresByPCMS.StringAlgorithms
{
   public class zFunction
    {
        public static void Solve(string[] args)
        {
            string[] inputData = File.ReadAllLines("search2.in");
            string pattern = inputData[0];
            string text = inputData[1];
            int[] zFunctionValue = SearchByzFunction(text, pattern, '#');
            OutPut(zFunctionValue, pattern.Length);
        }

        private static int[] SearchByzFunction(string text, string pattern, char specialSymbol)
        {
            text = pattern + specialSymbol + text;
            int[] zFunctionValue = new int[text.Length];
            int leftPositionOfBlock = 0, rightPositionOfBlock = 0;

            for (int i = 1; i < text.Length; i++)
            {
                zFunctionValue[i] = Math.Max(0, Math.Min((rightPositionOfBlock - i), zFunctionValue[i - leftPositionOfBlock]));
                while (i + zFunctionValue[i] < text.Length && text[zFunctionValue[i]] == text[i + zFunctionValue[i]])
                    zFunctionValue[i]++;

                if (i + zFunctionValue[i] > rightPositionOfBlock)
                {
                    leftPositionOfBlock = i;
                    rightPositionOfBlock = i + zFunctionValue[i];
                }
            }
            return zFunctionValue;
        }

        private static void OutPut(int[] zFunctionValue, int patternLength)
        {
            int countOfIngoing = 0;
            for (int i = patternLength; i < zFunctionValue.Length; i++)
            {
                if (zFunctionValue[i] == patternLength)
                    countOfIngoing++;
            }
            Console.WriteLine(countOfIngoing);
            for (int i = patternLength; i < zFunctionValue.Length; i++)
            {
                if (zFunctionValue[i] == patternLength)
                    Console.Write((i - patternLength) + " ");
            }
        }
    }
}
