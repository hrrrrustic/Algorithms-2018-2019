using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GraphAlgorithms
{
    class PrimaForCompleteGraph
    {
        static void Solve(string[] args)
        {
            double answer = 0;
            //int[][] data = File.ReadAllLines("spantree.in").Select(k => k.Split(' ').Select(e => int.Parse(e)).ToArray()).ToArray();
            int v = int.Parse(Console.ReadLine());
            bool[] used = new bool[v];
            double[] bestDist = new double[v];
            int[][] data = new int[v][];
            double[][] adjMatrix = new double[v][];
            for (int i = 0; i < v; i++)
            {
                if (adjMatrix[i] == null)
                    adjMatrix[i] = new double[v];
                data[i] = Console.ReadLine().Split(' ').Select(k => int.Parse(k)).ToArray();
                for (int j = 0; j < i; j++)
                {
                    if (adjMatrix[j] == null)
                        adjMatrix[j] = new double[v];
                    double val = Math.Sqrt((data[j][0] - data[i][0])*(data[j][0] - data[i][0]) + (data[j][1] - data[i][1])*(data[j][1] - data[i][1]));
                    adjMatrix[i][j] = val;
                    adjMatrix[j][i] = val;
                }
                bestDist[i] = 100000;
            }
            //int vertexCount = data[0][0];
            bestDist[0] = 0;
            for (int i = 0; i < v; i++)
            {
                int q = -1;
                for (int j = 0; j < v; j++)
                {
                    if (!used[j] && (q == -1 || bestDist[j] < bestDist[q]))
                        q = j;
                }
                answer += bestDist[q];
                used[q] = true;
                if (i != v - 1)
                    for (int j = 0; j < v; j++)
                    {
                        if (adjMatrix[q][j] < bestDist[j])
                            bestDist[j] = adjMatrix[q][j];
                    }
            }
            Console.WriteLine(answer);
            //File.WriteAllText("spantree.out", answer.ToString());
        }
    }
}