using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GraphAlgorithms
{
    class Matrix
    {
        static void Solve(string[] args)
        {
            
            string[] data = File.ReadAllLines("input.txt");
            int[] count = new int[int.Parse(data[0].Split(' ').First())];
            for (int i = 0; i < data.Length - 1; i++)
            {
                int[] splittedData = data[i + 1].Split(' ').Select(k => int.Parse(k) - 1).ToArray();
                count[splittedData[0]]++;
                count[splittedData[1]]++;
            }
            File.WriteAllText("output.txt", string.Join(" ", count));
        }
    }
}
