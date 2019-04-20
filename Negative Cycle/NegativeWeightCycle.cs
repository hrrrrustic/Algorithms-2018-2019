using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Negative_Cycle
{
    class Program
    {
        static void Main(string[] args)
        {
            long[][] data = File.ReadAllLines("negcycle.in").Select(k => k.Trim().Split(' ').Select(e => long.Parse(e)).ToArray()).ToArray();
            long vertexCount = data[0][0];
            List<Tuple<long, long, long>> edgeList = new List<Tuple<long, long, long>>();
            for (int i = 0; i < vertexCount; i++)
            {
                for (int j = 0; j < vertexCount; j++)
                {
                    long from = i;
                    long to = j;
                    long weight = data[i + 1][j];
                    if(from == to && weight < 0)
                    {
                        from++;
                        Console.WriteLine("YES\r\n" + 2 + "\r\n" + from + " " + from);
                        //File.WriteAllText("negcycle.out", "YES\r\n" + 2 + "\r\n" + from + " " + from);
                        return;
                    }
                    if (weight != 1000000000)
                        edgeList.Add(Tuple.Create(from, to, weight));
                }
            }
            long[] dist = new long[vertexCount];
            long[] prev = new long[vertexCount];
            for (int i = 0; i < vertexCount; i++)
            {
                dist[i] = 1000000000;
                prev[i] = -1;
            }
            int edgeCount = edgeList.Count;
            dist[0] = 0;
            long cycleStart = -1;
            for (int i = 0; i < vertexCount - 1; i++)
            {
                cycleStart = -1;
                for (int j = 0; j < edgeCount; j++)
                {
                    long from = edgeList[j].Item1;
                    long to = edgeList[j].Item2;
                    long w = edgeList[j].Item3;
                    if (dist[from] + w < dist[to])
                    {
                        dist[to] = dist[from] + w;
                        cycleStart = to;
                        prev[to] = from;
                    }
                }
            }
            List<long> cycle = new List<long>(100);
            if (cycleStart == -1)
            {
                Console.WriteLine("NO");
                return;
            }
            else
            {
                for (int i = 0; i < vertexCount; i++)
                {
                    cycleStart = prev[cycleStart];
                }
                long current = cycleStart;
                while (true)
                {
                    cycle.Add(current + 1);
                    if (current == cycleStart && cycle.Count != 1)
                        break;
                    current = prev[current];
                }
            }
            cycle.Reverse();
            Console.WriteLine("YES\r\n" + cycle.Count + "\r\n" + string.Join(" ", cycle));
            //File.WriteAllText("negcycle.out", "YES\r\n" + cycle.Count + "\r\n" + string.Join(" ", cycle));
        }
    }
}

