using System;
using System.Collections.Generic;
using System.IO;

namespace AlgorithmsAndStructuresByPCMS.DataStructures
{
    public class ReversePolishNotation
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
            int current = 0;
            
            int[] stack = new int[inputNotation.Length];
            for (int i = 0; i < inputNotation.Length; i++)
            {
                if (int.TryParse(inputNotation[i], out int number))
                {
                    stack[current] = number;
                }
                else
                {
                    stack[current - 2] = func[inputNotation[i]](stack[current - 2], stack[current - 1]);
                    current -= 2;
                }
                current++;
            }

            File.WriteAllText("postfix.out", stack[0].ToString());
        }
    }
}