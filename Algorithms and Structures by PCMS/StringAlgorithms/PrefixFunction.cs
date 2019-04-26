using System;
using System.IO;

namespace StringAlgorithms
{
    public class ZFunctionForPrefixFunction
    {
        public static void Solve(string[] args)
        {
            string text = File.ReadAllText("prefix.in").Trim();
            int[] zFunction = ZFunction(text);
            Console.WriteLine(string.Join(" ", ZFunctionToPrefixFunction(zFunction)));
        }
        private static int[] ZFunction(string text)
        {
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
        private static int[] ZFunctionToPrefixFunction(int[] zFunction)
        {
            int[] prefixFunction = new int[zFunction.Length];
            for (int i = 1; i < zFunction.Length; i++)
            {
                for (int j = zFunction[i] - 1; j > -1; j--)
                    if (prefixFunction[i + j] > 0)
                        break;
                    else
                        prefixFunction[i + j] = j + 1;
            }
            return prefixFunction;
        }
    }
}
