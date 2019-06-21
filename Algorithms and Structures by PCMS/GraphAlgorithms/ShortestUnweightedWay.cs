using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AlgorithmsAndStructuresByPCMS.GraphAlgorithms
{

    public class BreadthFirstSearch
    {
        private class Graph
        {
            public int VertexCount { get; }
            public List<int>[] AdjList { get; }
            public bool[] Visited { get; }

            public Graph(int vertexCount, int edgeCount, List<int>[] adjList)
            {
                VertexCount = vertexCount;
                AdjList = adjList;
                Visited = new bool[vertexCount];
            }
        }
        public static void Solve(string[] args)
        {
            int[][] inputData = File
                .ReadAllLines("pathbge1.in")
                .Select(k => k.Trim().Split(' ').Select(int.Parse).ToArray())
                .ToArray();

            Graph currentGraph = InitGraph(inputData.Skip(1).ToArray(), inputData[0][0], inputData[0][1]);

            int[] distance = BFSSearchDistance(currentGraph);
            Console.WriteLine(string.Join(" ", distance));
        }

        private static Graph InitGraph(int[][] data, int vertexCount, int edgeCount)
        {
            List<int>[] adjList = new List<int>[vertexCount];
            for (int i = 0; i < edgeCount; i++)
            {
                int fromVertex = data[i][0] - 1;
                int toVertex = data[i][1] - 1;

                if (adjList[fromVertex] == null)
                    adjList[fromVertex] = new List<int>();
                adjList[fromVertex].Add(toVertex);

                if (adjList[toVertex] == null)
                    adjList[toVertex] = new List<int>();
                adjList[toVertex].Add(fromVertex);
            }

            return new Graph(vertexCount, edgeCount, adjList);
        }
        private static int[] BFSSearchDistance(Graph graph)
        {
            const int startVertex = 0;
            int[] distanceFromStart = new int[graph.VertexCount];

            Queue<int> queue = new Queue<int>();

            queue.Enqueue(startVertex);
            graph.Visited[startVertex] = true;
            distanceFromStart[startVertex] = 0;

            while (queue.Count != 0)
            {
                int currentVertex = queue.Dequeue();

                if (graph.AdjList[currentVertex] != null)
                {
                    for (int i = 0; i < graph.AdjList[currentVertex].Count; i++)
                    {
                        if (!graph.Visited[graph.AdjList[currentVertex][i]])
                        {
                            graph.Visited[graph.AdjList[currentVertex][i]] = true;
                            distanceFromStart[graph.AdjList[currentVertex][i]] = distanceFromStart[currentVertex] + 1;
                            queue.Enqueue(graph.AdjList[currentVertex][i]);
                        }
                    }
                }
            }

            return distanceFromStart;
        }
    }
}
