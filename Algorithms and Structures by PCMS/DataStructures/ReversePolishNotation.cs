using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ReversePolishNotation
{
    class Stack
    {
        static void Solve(string[] args)
        {
            string[] input;
            bool result = false;
            int j = 0;
            using (var file = new StreamReader("postfix.in"))
            {
                input = file.ReadLine().Split(' ');
            }
            int[] stack = new int[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                int numbers;
                result = int.TryParse(input[i], out numbers);
                if (result)
                {
                    stack[j] = numbers;
                }
                else
                {
                    switch (input[i])
                    {
                        case "+":
                            stack[j - 2] = stack[j - 2] + stack[j - 1];
                            j -= 2;
                            break;
                        case "-":
                            stack[j - 2] = stack[j - 2] - stack[j - 1];
                            j -= 2;
                            break;
                        case "*":
                            stack[j - 2] = stack[j - 2] * stack[j - 1];
                            j -= 2;
                            break;
                    }
                }
                j++;
            }
            using (var outfile = new StreamWriter("postfix.out"))
            {
                outfile.WriteLine(stack[0]);
            }
        }
    }
}