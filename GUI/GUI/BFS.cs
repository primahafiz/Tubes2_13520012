using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    class BFS
    {
        static Queue<string> q;
        static List<string> ans;
        static List<Tuple<string, string, int>> edge;
        public string[] BFSMain(string dirPath, string searchFile, bool isAll)
        {
            // queue for BFS
            q = new Queue<string>();
            // list path of searchFile
            ans = new List<string>();
            // list of edge <Node1,Node2,Color>
            // Color : 0 => black, 1 => red, 2 => blue
            edge = new List<Tuple<string, string, int>>();

            q.Enqueue(dirPath);
            int idx = 0;

            while (q.Count != 0)
            {
                string head = q.Dequeue();
                // check if a path is file
                if (isFile(head))
                {
                    // check if file=searchFile
                    if (Path.GetFileName(head) == searchFile)
                    {
                        ans.Add(head);
                        if (!isAll)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    // get all directory inside
                    string[] pathsDir = Directory.GetDirectories(head);
                    // get all file inside
                    string[] pathsFile = Directory.GetFiles(head);

                    // add all path file and folder to queue and list of edge 
                    foreach (string path in pathsFile)
                    {
                        q.Enqueue(path);
                        edge.Add(Tuple.Create(head, path, 0));
                    }
                    foreach (string path in pathsDir)
                    {
                        q.Enqueue(path);
                        edge.Add(Tuple.Create(head, path, 0));
                    }
                }
                // mark edge that has been visited with red color
                for (; idx < edge.Count; idx++)
                {
                    if (edge[idx].Item1 != head) break;
                    edge[idx] = Tuple.Create(edge[idx].Item1, edge[idx].Item2, 1);
                }
            }
            // mark edge with blue color if edge is a path to the searchFile
            foreach (string pathAns in ans)
            {
                for (int i = 0; i < edge.Count; i++)
                {
                    // check if pathAns is inside edge[i].Item1 and edge[i].Item2
                    if (isPrefixSubs(edge[i].Item1, pathAns) && isPrefixSubs(edge[i].Item2, pathAns))
                    {
                        edge[i] = Tuple.Create(edge[i].Item1, edge[i].Item2, 2);
                    }
                }
            }
            printList(edge);
            Folder_Crawling.SearchingGraph.buildGraph(edge);
            return ans.ToArray();
        }

        public static bool isFile(string path)
        {
            return File.Exists(path);
        }
        public static bool isPrefixSubs(string s1, string s2)
        {
            string[] split1 = s1.Split('\\');
            string[] split2 = s2.Split('\\');
            if (split1.Length > split2.Length)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < split1.Length; i++)
                {
                    if (split1[i] != split2[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        public static void printArr(string[] arr)
        {
            Console.WriteLine("List ans :");
            foreach (string element in arr)
            {
                Console.WriteLine(element);
            }
        }
        public static void printList(List<Tuple<string, string, int>> edge)
        {
            Console.WriteLine("List edge :");
            foreach (Tuple<string, string, int> element in edge)
            {
                Console.Write(Path.GetFileName(element.Item1) + " " + Path.GetFileName(element.Item2) + " " + element.Item3 + "\n");
            }
        }
    }
}
