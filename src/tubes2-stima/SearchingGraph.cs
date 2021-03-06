using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.Core.Geometry.Curves;

namespace Folder_Crawling
{
    class SearchingGraph
    {
        public static Bitmap buildGraph(List<Tuple<string, string, int>> edges, bool save)
        {
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
            //create the graph content
            foreach (Tuple<string, string, int> edge in edges)
            {
                if (edge.Item3 == 0)
                {
                    graph.AddEdge(edge.Item1, edge.Item2).Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
                    if (graph.FindNode(edge.Item1).Attr.Color != Microsoft.Msagl.Drawing.Color.Blue && graph.FindNode(edge.Item1).Attr.Color != Microsoft.Msagl.Drawing.Color.Red)
                    {
                        graph.FindNode(edge.Item1).Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
                    }
                    if (graph.FindNode(edge.Item2).Attr.Color != Microsoft.Msagl.Drawing.Color.Blue && graph.FindNode(edge.Item2).Attr.Color != Microsoft.Msagl.Drawing.Color.Red)
                    {
                        graph.FindNode(edge.Item2).Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
                    }
                }
                else if (edge.Item3 == 1)
                {
                    graph.AddEdge(edge.Item1, edge.Item2).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                    if (graph.FindNode(edge.Item1).Attr.Color == Microsoft.Msagl.Drawing.Color.Black)
                    {
                        graph.FindNode(edge.Item1).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                    }
                    if (graph.FindNode(edge.Item2).Attr.Color == Microsoft.Msagl.Drawing.Color.Black)
                    {
                        graph.FindNode(edge.Item2).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                    }
                }
                else
                {
                    graph.AddEdge(edge.Item1, edge.Item2).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                    graph.FindNode(edge.Item1).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                    graph.FindNode(edge.Item2).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;

                }
                graph.FindNode(edge.Item1).LabelText = Path.GetFileName(edge.Item1);
                graph.FindNode(edge.Item2).LabelText = Path.GetFileName(edge.Item2);

            }
            Microsoft.Msagl.GraphViewerGdi.GraphRenderer renderer = new Microsoft.Msagl.GraphViewerGdi.GraphRenderer(graph);
            renderer.CalculateLayout();

            Bitmap bitmap;
            if (save) {
                bitmap = new Bitmap((int)graph.Width, (int)(graph.Height), PixelFormat.Format32bppPArgb);
                renderer.Render(bitmap);

                String picture = Directory.GetCurrentDirectory();

                while (true)
                {
                    string[] pathsDir = Directory.GetDirectories(picture);
                    foreach (String pathDir in pathsDir)
                    {
                        if (Path.GetFileName(pathDir) == "pictures")
                        {
                            picture = pathDir;
                            break;
                        }
                    }
                    if (Path.GetFileName(picture) == "pictures")
                    {
                        break;
                    }
                    picture = Directory.GetParent(picture).FullName;
                }

                File.WriteAllText(picture + "\\graph.jpg", "");
                try
                {
                    File.Delete(picture + "\\graph.jpg");
                }
                catch
                {
            
                }
                try
                {
                    bitmap.Save(picture + "\\graph.jpg");
                }
                catch
                {

                }
            }
            int width = 500;
            int height = 360;
            bitmap = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
            renderer.Render(bitmap);

            return bitmap;
        }
    }
}
