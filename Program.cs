using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Folder_Crawling
{
    static class Program
    {
        public static void Main()
        {
            BFS mainBFS = new BFS();
            string[] ans= mainBFS.BFSMain("D:\\Prima\\ITB\\IF sem 4\\RPL", "notulen asistensi.docx", false);
            BFS.printArr(ans);
        }
    }
}
