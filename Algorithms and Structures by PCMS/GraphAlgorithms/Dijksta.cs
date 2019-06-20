using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsAndStructuresByPCMS.GraphAlgorithms
{
    public class KvpComparerForDijkstra : IComparer<KeyValuePair<int, int>>
    {
        public int Compare(KeyValuePair<int, int> leftKvp, KeyValuePair<int, int> rightKvp)
        {
            if (leftKvp.Value < rightKvp.Value)
                return -1;
            if (leftKvp.Value > rightKvp.Value)
                return 1;
            if (leftKvp.Value == rightKvp.Value && leftKvp.Key == rightKvp.Key)
                return 0;
            if (leftKvp.Key > rightKvp.Key && leftKvp.Value == rightKvp.Value)
                return 1;
            return -1;
        }
    }

    public class Dijkstra
    {
        private class Graph
        {
            public readonly List<KeyValuePair<int,int>>[] AdjList;
            private int VertexCount { get; }
            public int[] Distance { get; }
            public Graph(List<KeyValuePair<int,int>>[] adjList, int maxValue)
            {
                VertexCount = adjList.Length;
                AdjList = adjList;
                Distance = Enumerable.Repeat(maxValue, VertexCount).ToArray();
            }
        }
        public static void Solve()
        {
            int[] inputData = Console
                .ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            Graph graph = InitGraph(inputData[0], inputData[1]);
            Console.WriteLine(string.Join(" ", DijkstraAlgo(graph)));
        }
        private static int[] DijkstraAlgo(Graph graph)
        {
            SortedDictionary<KeyValuePair<int, int>, int> justPriorityQueue = new SortedDictionary<KeyValuePair<int, int>, int>(new KvpComparerForDijkstra());
            graph.Distance[0] = 0;
            justPriorityQueue.Add(new KeyValuePair<int, int>(0, graph.Distance[0]), 0);
            while (justPriorityQueue.Count != 0)
            {
                var currentMinEdge = justPriorityQueue.First();
                justPriorityQueue.Remove(justPriorityQueue.First().Key);
                if (graph.AdjList[currentMinEdge.Key.Key] != null)
                {
                    foreach (var item in graph.AdjList[currentMinEdge.Key.Key])
                    {
                        int destinationVertex = item.Key;
                        int edgeWeight = item.Value;
                        if (graph.Distance[currentMinEdge.Key.Key] + edgeWeight < graph.Distance[destinationVertex])
                        {
                            justPriorityQueue.Remove(new KeyValuePair<int, int>(destinationVertex, graph.Distance[destinationVertex]));
                            graph.Distance[destinationVertex] = graph.Distance[currentMinEdge.Key.Key] + edgeWeight;
                            justPriorityQueue.Add(new KeyValuePair<int, int>(destinationVertex, graph.Distance[destinationVertex]), 0);
                        }
                    }
                }
            }
            return graph.Distance;
        }
        private static Graph InitGraph(int vertexCount, int edgeCount)
        {
            List<KeyValuePair<int, int>>[] adjList = new List<KeyValuePair<int, int>>[vertexCount];
            for (int i = 0; i < edgeCount; i++)
            {
                int[] data = Console
                    .ReadLine()
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray();

                if (adjList[data[0] - 1] == null)
                    adjList[data[0] - 1] = new List<KeyValuePair<int, int>>();

                adjList[data[0] - 1].Add(new KeyValuePair<int, int>(data[1] - 1, data[2]));
                if (adjList[data[1] - 1] == null)
                    adjList[data[1] - 1] = new List<KeyValuePair<int, int>>();

                adjList[data[1] - 1].Add(new KeyValuePair<int, int>(data[0] - 1, data[2]));
            }
            return new Graph(adjList, 1000000);
        }
    }
}
