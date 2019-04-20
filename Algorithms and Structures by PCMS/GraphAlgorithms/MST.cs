using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SpanTree2
{
    public class KVPComparer : IComparer<KeyValuePair<int,int>> // Господи за что?
    {
        public int Compare(KeyValuePair<int,int> kvp1, KeyValuePair<int,int> kvp2)
        {
            if (kvp1.Value < kvp2.Value)
                return -1;
            else if (kvp1.Value > kvp2.Value)
                return 1;
            else if (kvp1.Value == kvp2.Value && kvp1.Key == kvp2.Key)
                return 0;
            else if (kvp1.Key > kvp2.Key && kvp1.Value == kvp2.Value)
                return 1;
            else return -1;
        }
    }

    class MST
    {
        static void Main(string[] args)
        {
            long answer = 0;
            int edgeCount;
            int vertexCount;
            List<KeyValuePair<int, int>>[] adjList;
            using (var file = new StreamReader("spantree2.in"))
            {
                int[] info = file.ReadLine().Split(' ').Select(k => int.Parse(k)).ToArray();
                vertexCount = info[0];
                edgeCount = info[1];
                adjList = new List<KeyValuePair<int, int>>[vertexCount];
                for (int i = 0; i < edgeCount; i++)
                {
                    int[] splittedData = file.ReadLine().Split(' ').Select(e => int.Parse(e)).ToArray();
                    int j = splittedData[0] - 1;
                    int k = splittedData[1] - 1;
                    int dist = splittedData[2];
                    if (adjList[j] == null)
                        adjList[j] = new List<KeyValuePair<int, int>>();
                    KeyValuePair<int, int> value = new KeyValuePair<int, int>(k, dist);
                    adjList[j].Add(value);
                    if (adjList[k] == null)
                        adjList[k] = new List<KeyValuePair<int, int>>();
                    KeyValuePair<int, int> value2 = new KeyValuePair<int, int>(j, dist);
                    adjList[k].Add(value2);
                }
            }
            bool[] visited = new bool[vertexCount];
            int[] bestdist = new int[vertexCount];
            for (int i = 0; i < vertexCount; i++)
            {
                bestdist[i] = 100001;
            }
            SortedDictionary<KeyValuePair<int,int>,int> prim = new SortedDictionary<KeyValuePair<int, int>,int>(new KVPComparer());
            bestdist[0] = 0;
            prim.Add(new KeyValuePair<int, int>(0, 0), 0);
            for (int i = 0; i < vertexCount; i++)
            {
                var f = prim.First();
                int vertex = f.Key.Key;
                int d = f.Key.Value;
                answer += d;
                visited[vertex] = true;
                prim.Remove(new KeyValuePair<int, int>(vertex,d));
                for (int j = 0; j < adjList[vertex].Count; j++)
                {
                    int dist = adjList[vertex][j].Value;
                    int dest = adjList[vertex][j].Key;
                    if (dist < bestdist[dest] && !visited[dest])
                    {
                        try
                        {
                            prim.Remove(new KeyValuePair<int, int>(dest, bestdist[dest]));
                        }
                        catch (Exception) { }
                        prim.Add(new KeyValuePair<int, int>(dest, dist), 0);
                        bestdist[dest] = dist;
                    }
                }
            }
            //File.WriteAllText("spantree3.out", answer.ToString());
            Console.WriteLine(answer);
        }
    }
}