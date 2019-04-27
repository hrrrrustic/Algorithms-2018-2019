using System;
using System.IO;

namespace AlgorithmsAndStructuresByPCMS.StringAlgorithms
{
    public class zFunctionForPrefixFunction
    {
        public static void Solve(string[] args)
        {
            string text = File.ReadAllText("prefix.in").Trim();
            int[] zFunctionValue = zFunction(text);
            Console.WriteLine(string.Join(" ", zFunctionToPrefixFunction(zFunctionValue)));
        }

        private static int[] zFunction(string text)
        {
            int[] zFunctionValue = new int[text.Length];
            int leftPositionOfBlock = 0;
            int rightPositionOfBlock = 0;
            
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

        private static int[] zFunctionToPrefixFunction(int[] zFunctionValue)
        {
            int[] prefixFunction = new int[zFunctionValue.Length];
            for (int i = 1; i < zFunctionValue.Length; i++)
            {
                int j = zFunctionValue[i] - 1;
                while(prefixFunction[i + j] <= 0 && j > -1)
                {
                    prefixFunction[i + j] = j + 1;
                    j--;
                }
            }
            return prefixFunction;
        }
    }
}
