using System;
using System.Linq;
using System.IO;

namespace AlgorithmsAndStructuresByPCMS.GraphAlgorithms
{
    public class Floyd
    {
        private class Graph
        {
            public int VertexCount { get; }
            public int[][] DistanseMatrix { get; }

            public Graph(int[][] distanseMatrix, int vertexCount)
            {
                VertexCount = vertexCount;
                DistanseMatrix = distanseMatrix;
            }
        }

        public static void Solve(string[] args)
        {
            int[][] inputData = File
                .ReadAllLines("pathsg.in")
                .Select(k => k.Trim().Split(' ')
                    .Select(int.Parse)
                    .ToArray())
                .ToArray();
            int vertexCount = inputData[0][0];
            int edgeCount = inputData[0][1];

            Graph currentGraph = InitGraph(inputData.Skip(1).ToArray(), InitDistanseMatrix(vertexCount), edgeCount,
                vertexCount);

            FloydAlgo(currentGraph);
            PrintAnswer(currentGraph);
        }

        private static Graph InitGraph(int[][] edgeList, int[][] dist, int edgeCount, int vertexCount)
        {
            for (int i = 0; i < edgeCount; i++)
            {
                int from = edgeList[i][0] - 1;
                int to = edgeList[i][1] - 1;
                int weight = edgeList[i][2];
                dist[from][to] = weight;
            }
            return new Graph(dist, vertexCount);
        }

        private static void PrintAnswer(Graph graph)
        {
            for (int i = 0; i < graph.VertexCount; i++)
            {
                for (int j = 0; j < graph.VertexCount; j++)
                {
                    Console.Write(graph.DistanseMatrix[i][j] + " ");
                }

                Console.Write("\r\n");
            }
        }

        private static int[][] InitDistanseMatrix(int vertexCount)
        {
            int[][] dist = new int[vertexCount][];
            for (int i = 0; i < vertexCount; i++)
            {
                dist[i] = new int[vertexCount];
                for (int j = 0; j < vertexCount; j++)
                {
                    if (i == j)
                        dist[i][j] = 0;
                    else
                        dist[i][j] = 1000000000;
                }
            }

            return dist;
        }

        private static void FloydAlgo(Graph graph)
        {
            for (int i = 0; i < graph.VertexCount; i++)
            {
                for (int j = 0; j < graph.VertexCount; j++)
                {
                    for (int k = 0; k < graph.VertexCount; k++)
                    {
                        graph.DistanseMatrix[j][k] = Math.Min(graph.DistanseMatrix[j][k], graph.DistanseMatrix[j][i] + graph.DistanseMatrix[i][k]);
                    }
                }
            }
        }
    }
}
