using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AlgorithmsAndStructuresByPCMS.GraphAlgorithms
{
    public class KvpComparerForSpanTree : IComparer<KeyValuePair<int, int>>
    {
        public int Compare(KeyValuePair<int, int> kvp1, KeyValuePair<int, int> kvp2)
        {
            if (kvp1.Value < kvp2.Value)
                return -1;
            if (kvp1.Value > kvp2.Value)
                return 1;
            if (kvp1.Value == kvp2.Value && kvp1.Key == kvp2.Key)
                return 0;
            if (kvp1.Key > kvp2.Key && kvp1.Value == kvp2.Value)
                return 1;
            return -1;
        }
    }

    public class Prima
    {
        private class Graph
        {
            public List<KeyValuePair<int, int>>[] AdjList { get; }
            public int VertexCount { get; }
            public bool[] Visited { get; }

            public Graph(List<KeyValuePair<int,int>>[] adjList, int vertexCount, int edgeCount)
            {
                AdjList = adjList;
                VertexCount = vertexCount;
                Visited = new bool[vertexCount];
            }
        }

        public static void Solve(string[] args)
        {
            int[][] data = File
                .ReadAllLines("spantree2.in")
                .Select(k => k.Split(' ').Select(int.Parse).ToArray())
                .ToArray();
            Graph currentGraph = InitGraph(data.Skip(1).ToArray(), data[0][1], data[0][0]);

            Console.WriteLine(PrimaAlgo(currentGraph));
        }

        private static Graph InitGraph(int[][] data, int edgeCount, int vertexCount)
        {
            List<KeyValuePair<int, int>>[] adjList = new List<KeyValuePair<int, int>>[vertexCount];
            for (int i = 0; i < edgeCount; i++)
            {
                int firstVertex = data[i][0] - 1;
                int secondVertex = data[i][1] - 1;
                int edgeWeight = data[i][2];
                if (adjList[firstVertex] == null)
                    adjList[firstVertex] = new List<KeyValuePair<int, int>>();

                adjList[firstVertex].Add(new KeyValuePair<int, int>(secondVertex, edgeWeight));

                if (adjList[secondVertex] == null)
                    adjList[secondVertex] = new List<KeyValuePair<int, int>>();

                adjList[secondVertex].Add(new KeyValuePair<int, int>(firstVertex, edgeWeight));
            }

            return new Graph(adjList, vertexCount, edgeCount);
        }

        private static long PrimaAlgo(Graph graph)
        {
            long spanTreeWeight = 0;
            const int maxWeight = 100000;
            int[] bestDistance = Enumerable.Repeat(maxWeight + 1, graph.VertexCount).ToArray();

            SortedDictionary<KeyValuePair<int, int>, int> priorityQueue =
                new SortedDictionary<KeyValuePair<int, int>, int>(new KvpComparerForSpanTree());

            bestDistance[0] = 0;
            priorityQueue.Add(new KeyValuePair<int, int>(0, 0), 0);

            for (int i = 0; i < graph.VertexCount; i++)
            {
                var bestEdge = priorityQueue.First();
                int destinationVertex = bestEdge.Key.Key;
                int bestEdgeWeight = bestEdge.Key.Value;
                spanTreeWeight += bestEdgeWeight;
                graph.Visited[destinationVertex] = true;
                priorityQueue.Remove(new KeyValuePair<int, int>(destinationVertex, bestEdgeWeight));

                for (int j = 0; j < graph.AdjList[destinationVertex].Count; j++)
                {
                    int distance = graph.AdjList[destinationVertex][j].Value;
                    int destination = graph.AdjList[destinationVertex][j].Key;
                    if (distance < bestDistance[destination] && !graph.Visited[destination])
                    {
                        priorityQueue.Remove(new KeyValuePair<int, int>(destination, bestDistance[destination]));
                        priorityQueue.Add(new KeyValuePair<int, int>(destination, distance), 0);
                        bestDistance[destination] = distance;
                    }
                }
            }

            return spanTreeWeight;
        }
    }
}
