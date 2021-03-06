using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    class DFS
    {
        static List<string> ans;
        static List<Tuple<string, string, int>> edge;
        static int flag;

        static Bitmap bitmap;

        public Tuple<string[], Bitmap> DFSMain(string dirPath, string searchFile, bool isAll, PictureBox pictureBox)
        {
            // list path of searchFile
            // ans = new List<string>();
            // list of edge <Node1,Node2,Color>
            // Color : 0 => black, 1 => red, 2 => blue
            // edge = new List<Tuple<string, string, int>>();

            ans = new List<string>();
            edge = new List<Tuple<string, string, int>>();
            flag = 1;
            
            DFSRecursive(dirPath, searchFile, isAll, pictureBox);

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

            // printList(edge);
            bitmap = Folder_Crawling.SearchingGraph.buildGraph(edge, true);
            pictureBox.Image = bitmap;
            return Tuple.Create(ans.ToArray(), bitmap);
        }

        public void DFSRecursive(string dirPath, string searchFile, bool isAll, PictureBox pictureBox)
        {
            // get all directory inside
            string[] pathsDir = Directory.GetDirectories(dirPath);
            // get all file inside
            string[] pathsFile = Directory.GetFiles(dirPath);

            foreach (string path in pathsDir)
            {
                edge.Add(Tuple.Create(dirPath, path, 0));
            }
            foreach (string path in pathsFile)
            {
                edge.Add(Tuple.Create(dirPath, path, 0));
            }
            
            foreach (string path in pathsDir)
            {
                if (flag == 1)
                {
                    edge.Remove(Tuple.Create(dirPath, path, 0));
                    edge.Add(Tuple.Create(dirPath, path, 1));
                    DFSRecursive(path, searchFile, isAll, pictureBox);
                    bitmap = Folder_Crawling.SearchingGraph.buildGraph(edge, false);
                    pictureBox.Image = bitmap;
                }
            }
            foreach (string path in pathsFile)
            {
                if (flag == 1)
                {
                    edge.Remove(Tuple.Create(dirPath, path, 0));
                    edge.Add(Tuple.Create(dirPath, path, 1));
                    if (isFile(path))
                    {
                        // check if file=searchFile
                        if (Path.GetFileName(path) == searchFile)
                        {
                            ans.Add(path);
                            if (!isAll)
                            {
                                flag = 0;
                                break;
                            }
                        }
                    } 
                    bitmap = Folder_Crawling.SearchingGraph.buildGraph(edge, false);
                    pictureBox.Image = bitmap;
                }
            }
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
