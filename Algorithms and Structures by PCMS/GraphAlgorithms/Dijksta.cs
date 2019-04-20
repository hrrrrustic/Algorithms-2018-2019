using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Dijkstra
{
    public class KVPComparer : IComparer<KeyValuePair<int, int>> // Господи за что?
    {
        public int Compare(KeyValuePair<int, int> kvp1, KeyValuePair<int, int> kvp2)
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

    class Dijkstra
    {
        static void Solve(string[] args)
        {
            //int[][] data = File.ReadAllLines("pathgep.in").Select(k => k.Split(' ').Select(e => int.Parse(e)).ToArray()).ToArray();
            int[] info = Console.ReadLine().Split(' ').Select(k => int.Parse(k)).ToArray();
            //int vertexCount = data[0][0];
            int vertexCount = info[0];
            //int edgeCount = data[0][1];
            int edgeCount = info[1];
            List<KeyValuePair<int, int>>[] adjMatrix = new List<KeyValuePair<int, int>>[vertexCount];
            for (int i = 0; i < edgeCount; i++)
            {
                int[] data = Console.ReadLine().Split(' ').Select(k => int.Parse(k)).ToArray();
                //if (adjMatrix[data[i + 1][0] - 1] == null)
                //adjMatrix[data[i + 1][0] - 1] = new List<KeyValuePair<int, int>>();
                if (adjMatrix[data[0] - 1] == null)
                    adjMatrix[data[0] - 1] = new List<KeyValuePair<int, int>>();
                //adjMatrix[data[i + 1][0] - 1].Add(new KeyValuePair<int, int>(data[i + 1][1] - 1, data[i + 1][2]));
                adjMatrix[data[0] - 1].Add(new KeyValuePair<int, int>(data[1] - 1, data[2]));
                //if (adjMatrix[data[i + 1][1] - 1] == null)
                //adjMatrix[data[i + 1][1] - 1] = new List<KeyValuePair<int, int>>();
                if (adjMatrix[data[1] - 1] == null)
                    adjMatrix[data[1] - 1] = new List<KeyValuePair<int, int>>();
                adjMatrix[data[1] - 1].Add(new KeyValuePair<int, int>(data[0] - 1, data[2]));
            }
            int[] dist = new int[vertexCount];
            for (int i = 0; i < vertexCount; i++)
            {
                dist[i] = 10000000;
            }
            dist[0] = 0;
            bool[] used = new bool[vertexCount];
            SortedDictionary<KeyValuePair<int, int>, int> neeeeeeeeeeeeeeet = new SortedDictionary<KeyValuePair<int, int>, int>(new KVPComparer());
            dist[0] = 0;
            neeeeeeeeeeeeeeet.Add(new KeyValuePair<int, int>(0, dist[0] ), 0);
            while (neeeeeeeeeeeeeeet.Count != 0)
            {
                var cur = neeeeeeeeeeeeeeet.First();
                neeeeeeeeeeeeeeet.Remove(neeeeeeeeeeeeeeet.First().Key);
                if (adjMatrix[cur.Key.Key] != null)
                    foreach (var item in adjMatrix[cur.Key.Key])
                    {
                        int to = item.Key;
                        int lenght = item.Value;
                        if (dist[cur.Key.Key] + lenght < dist[to])
                        {
                            neeeeeeeeeeeeeeet.Remove(new KeyValuePair<int, int> (to, dist[to]));
                            dist[to] = dist[cur.Key.Key] + lenght;
                            neeeeeeeeeeeeeeet.Add(new KeyValuePair<int, int>(to, dist[to]), 0);
                        }
                    }
            }
            /*for (int i = 0; i < vertexCount; i++)
            {
                int q = -1;
                for (int j = 0; j < vertexCount; j++)
                {
                    if (!used[j] && (q == -1 || dist[j] < dist[q]))
                        q = j;
                }
                used[q] = true;
                foreach (var item in adjMatrix[q])
                {
                    if (dist[q] + item.Value < dist[item.Key])
                        dist[item.Key] = dist[q] + item.Value;
                }
            }
            */  
            //File.WriteAllText("pathgep.out", string.Join(" ", dist));
            Console.WriteLine(string.Join(" ", dist));
        }
    }
}
