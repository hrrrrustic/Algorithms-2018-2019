using System.IO;

namespace AlgorithmsAndStructuresByPCMS.DataStructures
{
    public class StackForPolishNotation
    {
        public static void Solve(string[] args)
        {
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
                    switch (inputNotation[i])
                    {
                        case "+":
                            stack[currPosition - 2] = stack[currPosition - 2] + stack[currPosition - 1];
                            break;
                        case "-":
                            stack[currPosition - 2] = stack[currPosition - 2] - stack[currPosition - 1];
                            break;
                        case "*":
                            stack[currPosition - 2] = stack[currPosition - 2] * stack[currPosition - 1];
                            break;
                    }
                    currPosition -= 2;
                }
                currPosition++;
            }
            File.WriteAllText("postfix.out", stack[0].ToString());
        }
    }
}