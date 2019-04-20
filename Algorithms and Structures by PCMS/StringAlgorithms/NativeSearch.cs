using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace NativeSearch
{
    class NativeAlgorithm
    {
        static void Solve(string[] args)
        {
            string[] data = File.ReadAllLines("search1.in");
            string pattern = data[0];
            if(pattern.Length > data[1].Length)
            {
                Console.WriteLine(0);
                return;
            }
            int answer = 0;
            List<int> startpos = new List<int>();
            for (int i = 0; i < data[1].Length - pattern.Length + 1; i++)
            {
                bool help = true;
                if (data[1][i] == pattern[0])
                {
                    int k = i;
                    for (int j = 1; j < pattern.Length; j++)
                    {
                        if (!(data[1][k + 1] == pattern[j]))
                            help = false;
                        k++;
                    }
                    if (help)
                    {
                        answer++;
                        startpos.Add(i + 1);
                    }
                }
            }
            Console.WriteLine(answer);
            Console.WriteLine(string.Join(" ", startpos));
        }
    }
}
