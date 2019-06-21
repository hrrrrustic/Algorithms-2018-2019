using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AlgorithmsAndStructuresByPCMS.GraphAlgorithms
{

    public class DijkstraForWayToVertex
    {
        private class Graph
        {
            public int VertexCount { get; }
            public List<KeyValuePair<int, long>>[] AdjMatrix { get; }
            public bool[] Visited { get; }

            public Graph(int vertexCount, List<KeyValuePair<int, long>>[] adjMatrix)
            {
                VertexCount = vertexCount;
                AdjMatrix = adjMatrix;
                Visited = new bool[vertexCount];
            }
        }
        public static void Solve(string[] args)
        {
            long[][] data = File
                .ReadAllLines("pathmgep.in")
                .Select(k => k.Trim().Split(' ').Select(long.Parse).ToArray())
                .ToArray();

            int startVertex = (int)(data[0][1]) - 1;
            int finishVertex = (int)(data[0][2]) - 1;

            Graph currentGraph = InitGraph(data.Skip(1).ToArray(), (int)data[0][0]);

            long distance = DijkstraAlgo(currentGraph, startVertex, finishVertex);

            if (distance == long.MaxValue)
            {
                Console.WriteLine(-1);
            }
            else
            {
                Console.WriteLine(distance);
            }
        }

        private static Graph InitGraph(long[][] data, int vertexCount)
        {
            List<KeyValuePair<int, long>>[] adjMatrix = new List<KeyValuePair<int, long>>[vertexCount];
            for (int i = 0; i < vertexCount; i++)
            {
                for (int j = 0; j < vertexCount; j++)
                {
                    if (i == j)
                        continue;
                    if (data[i + 1][j] != -1)
                    {
                        if (adjMatrix[i] == null)
                            adjMatrix[i] = new List<KeyValuePair<int, long>>();
                        adjMatrix[i].Add(new KeyValuePair<int, long>(j, data[i + 1][j]));
                    }
                }
            }
            return new Graph(vertexCount, adjMatrix);
        }

        private static long DijkstraAlgo(Graph graph, int startVertex, int finishVertex)
        {
            long[] distancesFromStartVertex = Enumerable.Repeat(long.MaxValue, graph.VertexCount).ToArray();
            distancesFromStartVertex[startVertex] = 0;

            for (int i = 0; i < graph.VertexCount; i++)
            {
                long k = -1;
                for (int j = 0; j < graph.VertexCount; j++)
                {
                    if (!graph.Visited[j] && (k == -1 || distancesFromStartVertex[j] < distancesFromStartVertex[k]))
                        k = j;
                }
                graph.Visited[k] = true;

                if (graph.AdjMatrix[k] != null)
                    foreach (var item in graph.AdjMatrix[k])
                    {
                        if (distancesFromStartVertex[k] + item.Value < distancesFromStartVertex[item.Key])
                            distancesFromStartVertex[item.Key] = distancesFromStartVertex[k] + item.Value;
                    }
            }

            return distancesFromStartVertex[finishVertex];
        }
    }
}
