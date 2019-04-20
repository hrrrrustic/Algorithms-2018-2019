using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Stack
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] StackSimulator;
            int Counter;
            List<int> Answers = new List<int>();
            using (var file = new StreamReader("stack.in"))
            {
                Counter = int.Parse(file.ReadLine());
                StackSimulator = new int[Counter];
                for (int i = 0; i < Counter; i++)
                {
                    string[] Input = file.ReadLine().Split(' ');
                    if (Input[0] == "+")
                    {
                        StackSimulator[i] = int.Parse(Input[1]);
                    }
                    else
                    {
                        Answers.Add(StackSimulator[i - 1]);
                        i -= 2;
                        Counter -= 2;
                    }
                }
            }
            using (var outfile = new StreamWriter("stack.out"))
            {
                outfile.WriteLine(string.Join("\r\n", Answers));
            }
        }
    }
}