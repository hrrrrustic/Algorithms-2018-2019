using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GraphAlgorithms
{
    class MatrixForCheckParallel
    {
        static void Solve(string[] args)
        {
            bool isParallel = false;
            string[] data = File.ReadAllLines("input.txt");
            int vertexCount = int.Parse(data[0].Split(' ').First());
            int edgeCount = int.Parse(data[0].Split(' ').Last());
            int[,] matrix = new int[vertexCount, vertexCount];
            for (int i = 1; i < edgeCount + 1; i++)
            {
                int j = int.Parse(data[i].Split(' ').First()) - 1;
                int k = int.Parse(data[i].Split(' ').Last()) - 1;
                matrix[j, k]++;
                if (matrix[j, k] + matrix[k, j] > 1)
                {
                    isParallel = true;
                    break;
                }

            }
            using (var outFile = new StreamWriter("output.txt"))
            {

                outFile.Write(isParallel == true ? "YES" : "NO");
            }
        }
    }
}
