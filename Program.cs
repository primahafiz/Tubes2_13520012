using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Folder_Crawling
{
    static class Program
    {
        public static void Main()
        {
            string[] ans;

            BFS mainBFS = new BFS();
            ans= mainBFS.BFSMain("C:\\Users\\Aji\\Documents\\1st project", "index.cpk", false);
            BFS.printArr(ans);

            DFS mainDFS = new DFS();
            ans = mainDFS.DFSMain("C:\\Users\\Aji\\Documents\\1st project", "index.cpk", false);
            DFS.printArr(ans);
        }
    }
}
