using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GraphAlgorithms
{
    class Floyd
    {
        static void Solve(string[] args)
        {
            int[][] data = File.ReadAllLines("pathsg.in").Select(k => k.Trim().Split(' ').Select(e => int.Parse(e)).ToArray()).ToArray();
            int vertexCount = data[0][0];
            int edgeCount = data[0][1];
            int[][] dist = new int[vertexCount][];
            for (int i = 0; i < vertexCount; i++)
            {
                dist[i] = new int[vertexCount];
                for (int j = 0; j < vertexCount; j++)
                {
                    if(i == j)
                        dist[i][j] = 0;
                    else
                        dist[i][j] = 1000000000;
                }
            }
            for (int i = 0; i < edgeCount; i++)
            {
                int from = data[i + 1][0] - 1;
                int to = data[i + 1][1] - 1;
                int weight = data[i + 1][2];
                dist[from][to] = weight;
            }
            for (int i = 0; i < vertexCount; i++)
            {
                for (int j = 0; j < vertexCount; j++)
                {
                    for (int k = 0; k < vertexCount; k++)
                    {
                        dist[j][k] = Math.Min(dist[j][k], dist[j][i] + dist[i][k]);
                    }
                }
            }
            for (int i = 0; i < vertexCount; i++)
            {
                for (int j = 0; j < vertexCount; j++)
                {
                    Console.Write(dist[i][j] + " ");
                }
                Console.Write("\r\n");
            }
        }
    }
}
