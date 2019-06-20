using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AlgorithmsAndStructuresByPCMS.GraphAlgorithms
{
    public class BellmanFord
    {
        private class Graph
        {
            public long VertexCount { get; }
            public  long EdgeCount { get; }
            public  long[] Distances { get; }
            public List<Tuple<long, long, long>> EdgeList { get; }

            public Graph(long vertexCount, List<Tuple<long, long, long>> edgeList, long edgeCount)
            {
                VertexCount = vertexCount;
                EdgeList = edgeList;
                EdgeCount = edgeCount;
                Distances = new long[vertexCount];
                for (int i = 0; i < vertexCount; i++)
                {
                    Distances[i] = long.MaxValue;
                }
            }
        }
        public static void Solve(string[] args)
        {
            long[][] data = File
                .ReadAllLines("path.in")
                .Select(k => k.Trim().Split(' ').Select(long.Parse).ToArray())
                .ToArray();

            long start = data[0][2] - 1;

            Graph currentGraph = InitGraph(data.Skip(1).ToArray(), data[0][1], data[0][0]);

            currentGraph.Distances[start] = 0;

            BellmanFordAlgo(currentGraph);
            BellmanFordSearchVertexOnNegativeCycle(currentGraph);
            PrintAnswer(currentGraph);
        }

        private static Graph InitGraph(long[][] inputData, long edgeCount, long vertexCount)
        {
            List<Tuple<long, long, long>> edgeList = new List<Tuple<long, long, long>>();
            for (int i = 0; i < edgeCount; i++)
            {
                long from = inputData[i + 1][0] - 1;
                long to = inputData[i + 1][1] - 1;
                long weight = inputData[i + 1][2];
                edgeList.Add(Tuple.Create(from, to, weight));
            }

            return new Graph(vertexCount, edgeList, edgeCount);
        }

        private static void BellmanFordAlgo(Graph graph)
        {
            for (int i = 0; i < graph.VertexCount - 1; i++)
            {
                for (int j = 0; j < graph.EdgeCount; j++)
                {
                    long fromVertex = graph.EdgeList[j].Item1;
                    long toVertex = graph.EdgeList[j].Item2;
                    long edgeWeight = graph.EdgeList[j].Item3;
                    if (graph.Distances[fromVertex] != long.MaxValue && graph.Distances[fromVertex] + edgeWeight < graph.Distances[toVertex])
                        graph.Distances[toVertex] = graph.Distances[fromVertex] + edgeWeight;
                }
            }
        }

        private static void BellmanFordSearchVertexOnNegativeCycle(Graph graph)
        {
            for (int i = 0; i < graph.VertexCount - 1; i++)
            {
                for (int j = 0; j < graph.EdgeCount; j++)
                {

                    long fromVertex = graph.EdgeList[j].Item1;
                    long toVertex = graph.EdgeList[j].Item2;
                    long edgeWeight = graph.EdgeList[j].Item3;
                    if (graph.Distances[fromVertex] != long.MaxValue && graph.Distances[fromVertex] + edgeWeight < graph.Distances[toVertex])
                        graph.Distances[toVertex] = (long)(-5 * 1e18) + 228;
                }
            }
        }

        private static void PrintAnswer(Graph graph)
        {
            for (int i = 0; i < graph.VertexCount; i++)
            {
                if (graph.Distances[i] <= -5 * 1e18 + 228)
                    Console.WriteLine("-");
                else if (graph.Distances[i] == long.MaxValue)
                    Console.WriteLine("*");
                else
                    Console.WriteLine(graph.Distances[i]);
            }
        }
    }
}
