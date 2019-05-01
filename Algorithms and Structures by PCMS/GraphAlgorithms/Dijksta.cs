using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsAndStructuresByPCMS.GraphAlgorithms
{
    public class KVPComparerForDijkstra : IComparer<KeyValuePair<int, int>>
    {
        public int Compare(KeyValuePair<int, int> LeftKVP, KeyValuePair<int, int> RightKVP)
        {
            if (LeftKVP.Value < RightKVP.Value)
                return -1;
            else if (LeftKVP.Value > RightKVP.Value)
                return 1;
            else if (LeftKVP.Value == RightKVP.Value && LeftKVP.Key == RightKVP.Key)
                return 0;
            else if (LeftKVP.Key > RightKVP.Key && LeftKVP.Value == RightKVP.Value)
                return 1;
            else return -1;
        }
    }

    public class Dijkstra
    {
        public class Graph
        {
            public List<KeyValuePair<int,int>>[] AdjList;
            public int EdgeCount { get; set; }
            public int VertexCount { get; set; }
            public int[] Distance { get; set; }
            public Graph(List<KeyValuePair<int,int>>[] adjList, int maxValue)
            {
                EdgeCount = adjList.Sum(k => k?.Count ?? 0);
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
                .Select(k => int.Parse(k))
                .ToArray();

            int vertexCount = inputData[0];
            int edgeCount = inputData[1];
            Graph graph = InitGraph(vertexCount, edgeCount);
            Console.WriteLine(string.Join(" ", DijkstraAlgo(graph)));
        }
        private static int[] DijkstraAlgo(Graph graph)
        {
            SortedDictionary<KeyValuePair<int, int>, int> justPriorityQueue = new SortedDictionary<KeyValuePair<int, int>, int>(new KVPComparerForDijkstra());
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
                int[] data = Console.ReadLine().Split(' ').Select(k => int.Parse(k)).ToArray();
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
