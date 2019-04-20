using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GraphAlgorithms
{
    class MatrixForEdjeToMatrix
    {
        static void Solve(string[] args)
        {
            string[] data = File.ReadAllLines("input.txt");
            int vertexCount = int.Parse(data[0].Split(' ').First());
            int edgeCount = int.Parse(data[0].Split(' ').Last());
            int[,] matrix = new int[vertexCount, vertexCount];
            for (int i = 1; i < edgeCount + 1; i++)
            {
                int j = int.Parse(data[i].Split(' ').First()) - 1;
                int k = int.Parse(data[i].Split(' ').Last()) - 1;
                matrix[j, k] = 1;
            }
            using (var outFile = new StreamWriter("output.txt"))
            {
                for (int i = 0; i < vertexCount; i++)
                {
                    for (int j = 0; j < vertexCount; j++)
                    {
                        outFile.Write(matrix[i, j] + " ");
                    }
                    outFile.Write("\r\n");
                }
            }
        }
    }
}
