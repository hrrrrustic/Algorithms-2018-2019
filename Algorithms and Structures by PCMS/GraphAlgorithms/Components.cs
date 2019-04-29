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
        public Graph(int vertexCount, int edgeCount)
        {
            EdgeCount = edgeCount;
            VertexCount = vertexCount;
            VertexList = new int[vertexCount];
            Visited = new bool[vertexCount];
            AdjList = new List<int>[vertexCount];
        }
    }

    public class BreadthFirstSearchforComponents
    {
        static int componentNumber = 1;
        public static void Solve()
        {
            int[][] inputData = File.ReadAllLines("components.in").Select(k => k.Trim().Split(' ').Select(e => int.Parse(e)).ToArray()).ToArray();
            Graph graph = new Graph(inputData[0][0], inputData[0][1]);
            InitGraph(graph, inputData.Skip(1).ToArray().ToArray());
            FindComponents(graph);
            string answer = $"{ componentNumber - 1}" + "\r\n" + string.Join(" ", graph.VertexList);
            File.WriteAllText("components.out", answer);
        }
        private static void FindComponents(Graph graph)
        {
            for (int i = 0; i < graph.VertexCount; i++)
            {
                if (!graph.Visited[i])
                    BFS(graph, i, graph.VertexCount);
            }
        }
        private static void InitGraph(Graph graph, int[][] edgeList)
        {
            for (int i = 0; i < graph.EdgeCount; i++)
            {
                int j = edgeList[i][0] - 1;
                int k = edgeList[i][1] - 1;
                if (graph.AdjList[j] == null)
                    graph.AdjList[j] = new List<int>();
                graph.AdjList[j].Add(k);
                if (graph.AdjList[k] == null)
                    graph.AdjList[k] = new List<int>();
                graph.AdjList[k].Add(j);
            }
        }
        private static void BFS(Graph graph, int startVertex, int vertexCount)
        {
            Queue<int> bfsQueue = new Queue<int>();
            bfsQueue.Enqueue(startVertex);
            graph.Visited[startVertex] = true;
            graph.VertexList[startVertex] = componentNumber;
            while (bfsQueue.Count != 0)
            {
                int currentVertex = bfsQueue.Dequeue();
                if (graph.AdjList[currentVertex] != null)
                {
                    for (int i = 0; i < graph.AdjList[currentVertex].Count; i++)
                    {
                        if (!graph.Visited[graph.AdjList[currentVertex][i]])
                        {
                            graph.Visited[graph.AdjList[currentVertex][i]] = true;
                            graph.VertexList[graph.AdjList[currentVertex][i]] = componentNumber;
                            bfsQueue.Enqueue(graph.AdjList[currentVertex][i]);
                        }
                    }
                }
            }
            componentNumber++;
        }
    }
}   