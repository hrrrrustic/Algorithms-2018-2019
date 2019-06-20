using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AlgorithmsAndStructuresByPCMS.GraphAlgorithms
{
    public class MatrixForCheckParallel
    {
        public static void Solve()
        {
            string[] inputData = File.ReadAllLines("input.txt");
            int vertexCount = int.Parse(inputData[0].Split(' ').First());
            int edgeCount = int.Parse(inputData[0].Split(' ').Last());

            File.WriteAllText("output.txt", 
                IsParallelAndDirected(inputData.Skip(1).ToArray(), vertexCount, edgeCount) ? "YES" : "NO");
        }
        private static bool IsParallelAndDirected(string[] edgeList, int vertexCount, int edgeCount)
        {
            int[,] adjMatrix = new int[vertexCount, vertexCount];
            for (int i = 0; i < edgeCount; i++)
            {
                int j = int.Parse(edgeList[i].Split(' ').First()) - 1;
                int k = int.Parse(edgeList[i].Split(' ').Last()) - 1;
                adjMatrix[j, k]++;
                if (adjMatrix[j, k] + adjMatrix[k, j] > 1)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
