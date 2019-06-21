using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AlgorithmsAndStructuresByPCMS.GraphAlgorithms
{
    public class FordBellman
    {
        private class Graph
        {
            public List<Tuple<int,int,long>> EdgeList { get; }
            public int EdgeCount { get; }
            public int VertexCount { get; }

            public Graph(List<Tuple<int, int, long>> edgeList, int vertexCount)
            {
                EdgeList = edgeList;
                VertexCount = vertexCount;
                EdgeCount = edgeList.Count;
            }
        }
        public static void Solve(string[] args)
        {
            long[][] data = File.
                ReadAllLines("negcycle.in").
                Select(k => k.Trim().Split(' ').Select(long.Parse).ToArray())
                .ToArray();

            Graph currentGraph = InitGraph(data.Skip(1).ToArray(), (int)data[0][0]);

            List<int> negativeCycle = SearchNegativeCycleByBellmanFordAlgo(currentGraph);

            if (negativeCycle == null)
            {
                Console.WriteLine("NO");
            }
            else
            {
                Console.WriteLine("YES\r\n" + negativeCycle.Count + "\r\n" + string.Join(" ", negativeCycle));
            }
        }

        private static Graph InitGraph(long[][] data, int vertexCount)
        {
            List<Tuple<int, int, long>> edgeList = new List<Tuple<int, int, long>>();
            for (int i = 0; i < vertexCount; i++)
            {
                for (int j = 0; j < vertexCount; j++)
                {
                    int fromVertex = i;
                    int toVertex = j;
                    long edgeWeight = data[i][j];
                   
                    if (edgeWeight != 1000000000)
                        edgeList.Add(Tuple.Create(fromVertex, toVertex, edgeWeight));
                }
            }
            return new Graph(edgeList, vertexCount);
        }

        private static List<int> SearchNegativeCycleByBellmanFordAlgo(Graph graph)
        {
            long[] distance = Enumerable.Repeat((long)1000000000, graph.VertexCount).ToArray();
            int[] parents = Enumerable.Repeat(-1, graph.VertexCount).ToArray();

            distance[0] = 0;
            int cycleStart = -1;
            for (int i = 0; i < graph.VertexCount - 1; i++)
            {
                cycleStart = -1;
                for (int j = 0; j < graph.EdgeCount; j++)
                {
                    int fromVertex = graph.EdgeList[j].Item1;
                    int toVertex = graph.EdgeList[j].Item2;
                    long edgeWeight = graph.EdgeList[j].Item3;
                    if (distance[fromVertex] + edgeWeight < distance[toVertex])
                    {
                        distance[toVertex] = distance[fromVertex] + edgeWeight;
                        cycleStart = toVertex;
                        parents[toVertex] = fromVertex;
                    }
                }
            }
            if (cycleStart == -1)
            {
                return null;
            }

            return GetCycle(graph, cycleStart, parents);
        }

        private static List<int> GetCycle(Graph graph, int cycleStart, int[] parents)
        {
            List<int> cycle = new List<int>();
            for (int i = 0; i < graph.VertexCount; i++)
            {
                cycleStart = parents[cycleStart];
            }
            int current = cycleStart;
            while (true)
            {
                cycle.Add(current + 1);
                if (current == cycleStart && cycle.Count != 1)
                    break;
                current = parents[current];
            }
            cycle.Reverse();
            return cycle;
        }
    }
}

