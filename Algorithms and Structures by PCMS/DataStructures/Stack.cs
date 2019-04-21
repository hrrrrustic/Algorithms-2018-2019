using System.Collections.Generic;
using System.IO;

namespace AlgorithmsAndStructuresByPCMS.DataStructures
{
    public class Stack
    {
        public static void Solve()
        {
            string[] inputData = File.ReadAllLines("stack.in");
            int commandCount = int.Parse(inputData[0]);
            int[] stack = new int[commandCount];
            List<int> answers = new List<int>();
            int stackTop = 0;
            for (int i = 0; i < commandCount; i++)
            {
                string[] currentRequest = inputData[i + 1].Split(' ');
                string command = currentRequest[0];
                if (command == "+")
                {
                    int value = int.Parse(currentRequest[1]);
                    stack[stackTop] = value;
                    stackTop++;
                }
                else
                {
                    answers.Add(stack[stackTop - 1]);
                    stackTop--;
                }
            }
            File.WriteAllText("stack.out", string.Join("\r\n", answers));
        }
    }
}