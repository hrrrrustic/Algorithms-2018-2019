using System;
using System.Linq;

namespace AlgorithmsAndStructuresByPCMS.GraphAlgorithms
{
    public class PrimaForCompleteGraph
    {
        private class Graph
        {
            public int VertexCount { get; }
            public double[][] AdjMatrix { get; }
            public bool[] Visited { get; }

            public Graph(int vertexCount, double[][] adjMatrix)
            {
                AdjMatrix = adjMatrix;
                VertexCount = vertexCount;
                Visited = new bool[vertexCount];
            }
        }
        public static void Solve(string[] args)
        {
            int rowCount = int.Parse(Console.ReadLine());

            Graph currentGraph = InitGraph(rowCount);
            
            Console.WriteLine(PrimaAlgo(currentGraph));
        }

        private static Graph InitGraph(int vertexCount)
        {
            double[][] adjMatrix = new double[vertexCount][];
            int[][] vertexCoordinates = new int[vertexCount][];
            for (int i = 0; i < vertexCount; i++)
            {
                if (adjMatrix[i] == null)
                    adjMatrix[i] = new double[vertexCount];

                vertexCoordinates[i] = Console
                    .ReadLine()
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray();

                for (int j = 0; j < i; j++)
                {
                    if (adjMatrix[j] == null)
                        adjMatrix[j] = new double[vertexCount];

                    double edgeWeight = Math.Sqrt
                            (
                             (vertexCoordinates[j][0] - vertexCoordinates[i][0]) * (vertexCoordinates[j][0] - vertexCoordinates[i][0]) + 
                             (vertexCoordinates[j][1] - vertexCoordinates[i][1]) * (vertexCoordinates[j][1] - vertexCoordinates[i][1])
                            );

                    adjMatrix[i][j] = edgeWeight;
                    adjMatrix[j][i] = edgeWeight;
                }
            }
            return new Graph(vertexCount, adjMatrix);
        }

        private static double PrimaAlgo(Graph graph)
        {
            const double maxEdgeWeight = 100000;
            double[] bestDistance = Enumerable.Repeat(maxEdgeWeight, graph.VertexCount).ToArray();

            double answer = 0;
            bestDistance[0] = 0;

            for (int i = 0; i < graph.VertexCount; i++)
            {
                int k = -1;
                for (int j = 0; j < graph.VertexCount; j++)
                {
                    if (!graph.Visited[j] && (k == -1 || bestDistance[j] < bestDistance[k]))
                        k = j;
                }
                answer += bestDistance[k];
                graph.Visited[k] = true;
                if (i != graph.VertexCount - 1)
                    for (int j = 0; j < graph.VertexCount; j++)
                    {
                        if (graph.AdjMatrix[k][j] < bestDistance[j])
                            bestDistance[j] = graph.AdjMatrix[k][j];
                    }
            }

            return answer;
        }
    }
}