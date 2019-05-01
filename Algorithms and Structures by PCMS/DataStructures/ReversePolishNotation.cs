using System;
using System.Collections.Generic;
using System.IO;

namespace AlgorithmsAndStructuresByPCMS.DataStructures
{
    public class StackForPolishNotation
    {
        public static void Solve(string[] args)
        {
            Dictionary<string, Func<int, int, int>> func = new Dictionary<string, Func<int, int, int>>()
            {
                { "+", ((x, y) => x + y) },
                { "-",((x, y) => x - y) },
                { "*", ((x, y) => x * y) }
            };
            string[] inputNotation = File.ReadAllText("postfix.in").Split(' ');
            int currPosition = 0;
            int[] stack = new int[inputNotation.Length];
            for (int i = 0; i < inputNotation.Length; i++)
            {
                if (int.TryParse(inputNotation[i], out int number))
                {
                    stack[currPosition] = number;
                }
                else
                {
                    stack[currPosition - 2] = func[inputNotation[i]](stack[currPosition - 2], stack[currPosition - 1]);
                    currPosition -= 2;
                }
                currPosition++;
            }
            File.WriteAllText("postfix.out", stack[0].ToString());
        }
    }
}