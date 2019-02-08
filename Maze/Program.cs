using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Maze
{
    class Program
    {
        static int[][] length;
        static bool isFound = false;
        static void Main(string[] args)
        {
            List<char> commands = new List<char>();
            string[] mazeInfo;
            int finishColPos = 0;
            int finishRowPos = 0;
            char[][] data;
            using (var file = new StreamReader("input.txt"))
            {
                mazeInfo = file.ReadLine().Split(' ');
                data = file.ReadToEnd().Split('\n').Select(k => k.ToArray()).ToArray();
            }
            int rowCount = int.Parse(mazeInfo[0]);
            int colCount = int.Parse(mazeInfo[1]);
            length = new int[rowCount][];
            for (int i = 0; i < rowCount; i++)
            {
                length[i] = new int[colCount];
                for (int j = 0; j < colCount; j++)
                {
                    length[i][j] = -1;
                }
            }
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    if (data[i][j] == 'S')
                    {
                        BFS(ref data, i, j, rowCount, colCount, ref finishColPos, ref finishRowPos);
                        break;
                    }
                }
            }
            if (isFound)
                FindShortestWay(finishRowPos, finishColPos, colCount, rowCount, ref commands);
            else commands.Clear();
            using (var outFile = new StreamWriter("output.txt"))
            {
                /*for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        outFile.Write("{0,4}", data[i][j]);
                    }
                    outFile.Write("\r\n");
                }
                outFile.WriteLine();
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        outFile.Write("{0,4}",length[i][j]);
                    }
                    outFile.Write("\r\n");
                }
                */
                commands.Reverse();
                outFile.WriteLine(commands.Count == 0 ? -1 : commands.Count);
                outFile.Write(string.Join("", commands));
            }
        }
        static void BFS(ref char[][] map, int startRowPos, int startColPos, int rowCount, int colCount, ref int finishColPos, ref int finishRowPos)
        {
            Queue<int> dfsqueue = new Queue<int>();
            dfsqueue.Enqueue(startRowPos);
            dfsqueue.Enqueue(startColPos);
            length[startRowPos][startColPos] = 0;
            while (dfsqueue.Count != 0)
            {
                int currRowPos = dfsqueue.Dequeue();
                int currColPos = dfsqueue.Dequeue();
                map[currRowPos][currColPos] = '!';
                for (int i = currRowPos - 1; i < currRowPos + 2; i++)
                {
                    for (int j = currColPos - 1; j < currColPos + 2; j++)
                    {
                        if (Math.Sqrt((currColPos - j)* (currColPos - j) + (currRowPos - i)* (currRowPos - i)) != 1 || j > colCount - 1|| j < 0 || i > rowCount - 1 || i < 0)
                            continue;
                        if (map[i][j] == '.')
                        {
                            dfsqueue.Enqueue(i);
                            dfsqueue.Enqueue(j);
                            if (length[i][j] == -1)
                                length[i][j] = length[currRowPos][currColPos] + 1;
                        }
                        if (map[i][j] == 'T')
                        {
                            length[i][j] = 10000000;
                            finishRowPos = i;
                            finishColPos = j;
                            isFound = true;
                            return;
                        }
                    }
                }
            }
        }
        static void FindShortestWay(int rowPos, int colPos, int colCount, int rowCount, ref List<char> commands)
        {
            int minNeighb = 100000000;
            int minCol = 0;
            int minRow = 0;
            while (minNeighb != 0)
            {
                for (int i = rowPos - 1; i < rowPos + 2; i++)
                {
                    for (int j = colPos - 1; j < colPos + 2; j++)
                    {
                        if (Math.Sqrt((colPos - j) * (colPos - j) + (rowPos - i) * (rowPos - i)) != 1 || j > colCount - 1 || j < 0 || i > rowCount - 1 || i < 0 || length[i][j] == -1)
                            continue;
                        if (length[i][j] < minNeighb)
                        {

                            minNeighb = length[i][j];
                            minRow = i;
                            minCol = j;
                        }
                    }
                }
                if (minRow > rowPos)
                    commands.Add('U');
                else if (minRow < rowPos)
                    commands.Add('D');
                else if (minCol > colPos)
                    commands.Add('L');
                else commands.Add('R');
                rowPos = minRow;
                colPos = minCol;
            }
        }
    }
}
