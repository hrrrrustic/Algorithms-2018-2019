using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AlgorithmsAndStructuresByPCMS.GraphAlgorithms
{
    public class FordFalkerson
    {
        private class Graph
        {
            public List<Tuple<int,int,int>>[] AdjList { get; }
            public int VertexCount { get; }
            public int Sink { get; }
            public const int MaxEdgeCapacity = (int) 1e5;
            public bool[] Visited { get; set; }

            public Graph(List<Tuple<int, int, int>>[] adjList, int vertexCount)
            {
                AdjList = adjList;
                VertexCount = vertexCount;
                Sink = vertexCount - 1;
                Visited = Enumerable.Repeat(false, vertexCount).ToArray();
            }
        }
        public static void Solve(string[] args)
        {
            int[][] data = File
                .ReadAllLines("maxflow.in")
                .Select(k => k.Trim().Split(' ').Select(int.Parse).ToArray())
                .ToArray();
            Graph currentGraph = InitGraph(data.Skip(1).ToArray(), data[0][1], data[0][0]);
            int maxFlow = 0;
            while(true)
            {

                int prevMaxFlowValue = maxFlow;
                maxFlow += FordFalkersonAlgoByDFS(currentGraph, 0, Graph.MaxEdgeCapacity);

                if (prevMaxFlowValue == maxFlow)
                    break;
                currentGraph.Visited = Enumerable.Repeat(false, currentGraph.VertexCount).ToArray();
            }

            Console.WriteLine(maxFlow);
        }

        private static int FordFalkersonAlgoByDFS(Graph graph, int source, int currentCapacity)
        {
            if (source == graph.Sink)
                return currentCapacity;
            graph.Visited[source] = true;
            for (int i = 0; i < graph.AdjList[source].Count; i++)
            {
                if (!graph.Visited[graph.AdjList[source][i].Item1] && graph.AdjList[source][i].Item3 < graph.AdjList[source][i].Item2)
                {
                    int minValue = currentCapacity > graph.AdjList[source][i].Item2 - graph.AdjList[source][i].Item3 ? 
                        graph.AdjList[source][i].Item2 - graph.AdjList[source][i].Item3 : currentCapacity;

                    int delta = FordFalkersonAlgoByDFS(graph, graph.AdjList[source][i].Item1, minValue);
                    if (delta > 0)
                    {
                        graph.AdjList[source][i] = Tuple
                            .Create
                                (
                                graph.AdjList[source][i].Item1, graph.AdjList[source][i].Item2,
                                graph.AdjList[source][i].Item3 + delta
                                );
                        int index;
                        for (index = 0; index < graph.AdjList[graph.AdjList[source][i].Item1].Count; index++)
                        {
                            if (graph.AdjList[graph.AdjList[source][i].Item1][index].Item1 == source)
                                break;
                        }
                        graph.AdjList[graph.AdjList[source][i].Item1][index] = Tuple
                            .Create
                                (
                                graph.AdjList[graph.AdjList[source][i].Item1][index].Item1,
                                graph.AdjList[graph.AdjList[source][i].Item1][index].Item2,
                                graph.AdjList[graph.AdjList[source][i].Item1][index].Item3 - delta
                                );

                        return delta;
                    }
                }
            }
            return 0;
        }

        private static Graph InitGraph(int[][] data, int edgeCount, int vertexCount)
        {
            List<Tuple<int, int, int>>[] adjList = new List<Tuple<int, int, int>>[vertexCount];
            for (int i = 0; i < edgeCount; i++)
            {
                int fromVertex = data[i][0] - 1;
                int toVertex = data[i][1] - 1;
                if (adjList[fromVertex] == null)
                    adjList[fromVertex] = new List<Tuple<int, int, int>>(999);
                adjList[fromVertex].Add(Tuple.Create(toVertex, data[i][2], 0));
                if (adjList[toVertex] == null)
                    adjList[toVertex] = new List<Tuple<int, int, int>>(999);
                adjList[toVertex].Add(Tuple.Create(fromVertex, data[i][2], data[i][2]));
            }
            return  new Graph(adjList, vertexCount);
        }
    }
}
