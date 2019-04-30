using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AlgorithmsAndStructuresByPCMS.GraphAlgorithms
{
    public class Graph
    {
        public bool[] Visited;
        public int[] VertexList;
        public List<int>[] AdjList;
        public int EdgeCount { get; set; }
        public int VertexCount { get; set; }
        public int ComponentNumber { get; set; }
        public Graph(List<int>[] adjList)
        {
            EdgeCount = adjList.Sum(k => k?.Count ?? 0);
            VertexCount = adjList.Length;
            VertexList = new int[VertexCount];
            Visited = new bool[VertexCount];
            AdjList = adjList;
            ComponentNumber = 1;
        }

        public void FindComponents()
        {
            for (int i = 0; i < VertexCount; i++)
            {
                if (!Visited[i])
                {
                    BFS(i);
                }
            }
        }

        private void BFS(int startVertex)
        {
            Queue<int> bfsQueue = new Queue<int>();
            bfsQueue.Enqueue(startVertex);
            Visited[startVertex] = true;
            VertexList[startVertex] = ComponentNumber;
            while (bfsQueue.Count != 0)
            {
                int currentVertex = bfsQueue.Dequeue();
                if (AdjList[currentVertex] != null)
                {
                    for (int i = 0; i < AdjList[currentVertex].Count; i++)
                    {
                        if (!Visited[AdjList[currentVertex][i]])
                        {
                            Visited[AdjList[currentVertex][i]] = true;
                            VertexList[AdjList[currentVertex][i]] = ComponentNumber;
                            bfsQueue.Enqueue(AdjList[currentVertex][i]);
                        }
                    }
                }
            }
            ComponentNumber++;
        }
    }

    public class BreadthFirstSearchforComponents
    {
        public static void Solve()
        {
            int[][] inputData = File.ReadAllLines("components.in").Select(k => k.Trim().Split(' ').Select(e => int.Parse(e)).ToArray()).ToArray();
            Graph graph = InitGraph(inputData[0][0], inputData[0][1], inputData.Skip(1).ToArray());
            graph.FindComponents();
            string answer = $"{ graph.ComponentNumber - 1}" + "\r\n" + string.Join(" ", graph.VertexList);
            File.WriteAllText("components.out", answer);
        }
        
        private static Graph InitGraph(int vertexCount, int edgeCount, int[][] edgeList)
        {
            List<int>[] adjList = new List<int>[vertexCount];

            for (int i = 0; i < edgeCount; i++)
            {
                int j = edgeList[i][0] - 1;
                int k = edgeList[i][1] - 1;
                if (adjList[j] == null)
                    adjList[j] = new List<int>();
                adjList[j].Add(k);
                if (adjList[k] == null)
                    adjList[k] = new List<int>();
                adjList[k].Add(j);
            }

            return new Graph(adjList);

        }
    }
}   