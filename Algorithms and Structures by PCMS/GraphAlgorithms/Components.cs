using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AlgorithmsAndStructuresByPCMS.GraphAlgorithms
{
    public class BreadthFirstSearchforComponents
    {
        public class Graph
        {
            public List<int>[] AdjList;
            public int EdgeCount { get; set; }
            public int VertexCount { get; set; }
            public int ComponentNumber { get; set; }
            public Graph(List<int>[] adjList)
            {
                EdgeCount = adjList.Sum(k => k?.Count ?? 0);
                VertexCount = adjList.Length;
                AdjList = adjList;
                ComponentNumber = 1;
            }

            public int[] FindComponents()
            {
                int[] vertexList = new int[VertexCount];
                bool[] visited = new bool[VertexCount];
                for (int i = 0; i < VertexCount; i++)
                {
                    if (!visited[i])
                    {
                        BFS(i, visited, vertexList);
                    }
                }
                return vertexList;
            }

            private void BFS(int startVertex, bool[] visited, int[] vertexList)
            {
                Queue<int> bfsQueue = new Queue<int>();
                bfsQueue.Enqueue(startVertex);
                visited[startVertex] = true;
                vertexList[startVertex] = ComponentNumber;
                while (bfsQueue.Count != 0)
                {
                    int currentVertex = bfsQueue.Dequeue();
                    if (AdjList[currentVertex] != null)
                    {
                        for (int i = 0; i < AdjList[currentVertex].Count; i++)
                        {
                            if (!visited[AdjList[currentVertex][i]])
                            {
                                visited[AdjList[currentVertex][i]] = true;
                                vertexList[AdjList[currentVertex][i]] = ComponentNumber;
                                bfsQueue.Enqueue(AdjList[currentVertex][i]);
                            }
                        }
                    }
                }
                ComponentNumber++;
            }
        }
        public static void Solve()
        {
            int[][] inputData = File
                .ReadAllLines("components.in")
                .Select(k => k.Trim().Split(' ').Select(int.Parse).ToArray())
                .ToArray();

            Graph graph = InitGraph(inputData[0][0], inputData[0][1], inputData.Skip(1).ToArray());
            int[] components = graph.FindComponents();
            string answer = $"{ graph.ComponentNumber - 1}" + "\r\n" + string.Join(" ", components);
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