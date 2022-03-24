using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace GUI
{
	public partial class Form1 : Form
	{
		bool click = false;
		public Form1()
		{
			InitializeComponent();
			linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel1_Click);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			CommonOpenFileDialog dialog = new CommonOpenFileDialog();
			dialog.IsFolderPicker = true;
			if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
			{
				textBox1.Text = dialog.FileName;
			}
		}

		private void textBox2_TextChanged(object sender, EventArgs e)
		{

		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void Form1_Load(object sender, EventArgs e)
		{
			panel1.AutoScroll = true;
		}

		private async void button2_Click(object sender, EventArgs e)
		{
			String path = textBox1.Text;
			String file = textBox2.Text;
			String method;
			Boolean occurences = checkBox1.Checked;
			Boolean isChecked1 = radioButton1.Checked;
			Boolean isChecked2 = radioButton2.Checked;
			String picture = Directory.GetCurrentDirectory();
			Tuple<string[], Bitmap> res;

			button3.Visible = false;
			label4.Text = "File path :";
			label5.Text = "";
			label6.Text = "";

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

			if (isChecked1)
				method = radioButton1.Text;
			else if (isChecked2)
				method = radioButton2.Text;
			else 
				method = "";

			linkLabel1.Links.Clear();
			if (path == null || file == null) {
				MessageBox.Show("Fill the required form !!", "Error");
			} else {
				if (method != "BFS" && method != "DFS") {
					MessageBox.Show("Fill the required form !!", "Error");
				} else {
					click = true;
					DateTime startTime = DateTime.Now;
					if (method == "BFS") {
						linkLabel1.Text = "";
						BFS bfs = new BFS();
						res = bfs.BFSMain(path, file, occurences, pictureBox1);
					} else {
						linkLabel1.Text = "";
						DFS dfs = new DFS();
						res = dfs.DFSMain(path, file, occurences, pictureBox1);
					}
					DateTime stopTime = DateTime.Now;
					
					int i = 1;
					int count = 0;
					int start;
					foreach (String str in res.Item1) {
						linkLabel1.Text += i.ToString() + ". " + res.Item1[i-1] + "\n";
						count += 2 + i.ToString().Length;
						start = count;
						linkLabel1.Links.Add(start, res.Item1[i-1].Length, toAccesableLink(res.Item1[i-1]));
						count += res.Item1[i-1].Length + 1;
						i++;
					}
					label4.Text = "File path : (" + res.Item1.Length.ToString() + ")";
					label5.Text = "click picture to download";
					double time = stopTime.Subtract(startTime).TotalMilliseconds;
					if (time < 1000)
                    {
						label6.Text = "Estimated time : " + stopTime.Subtract(startTime).TotalMilliseconds + " ms";
                    }
					else
                    {
						label6.Text = "Estimated time : " + stopTime.Subtract(startTime).TotalSeconds + " s";

					}

					button3.Visible = true;
				}
			}
		}

		public String toAccesableLink(String str)
		{
			String res = "";
			int idx = 0;
			for (int i = str.Length-1; i >= 0; i--)
			{
				if (str[i] == '\\')
				{
					idx = i;
					break;
				}
			}
			for (int i = 0; i < idx; i++)
			{
				res += str[i];
			}
			return res;
		}
		
		private void label3_Click(object sender, EventArgs e)
		{

		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
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
			Bitmap bitmap = new Bitmap(picture + "\\graph.jpg");
			if (click == false) {
				MessageBox.Show("Fill the required form !!", "Error");
			} else {
				CommonOpenFileDialog dialog = new CommonOpenFileDialog();
				dialog.IsFolderPicker = true;
				if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
				{
					File.WriteAllText(dialog.FileName+"\\graph.jpg", "");
					try
					{
						File.Delete(dialog.FileName + "\\graph.jpg");
					}
					catch
					{
				
					}
					try
					{
						bitmap.Save(dialog.FileName + "\\graph.jpg");
					}
					catch
					{

					}
				}
			}
		}
		private void linkLabel1_Click(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			// Determine which link was clicked within the LinkLabel.
			this.linkLabel1.Links[linkLabel1.Links.IndexOf(e.Link)].Visited = true;

			// Display the appropriate link based on the value of the 
			// LinkData property of the Link object.
			string target = e.Link.LinkData as string;
 
			Process.Start("explorer.exe", target);
		}

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

		Image ZoomPicture(Image img, Size size)
        {
			Bitmap bm = new Bitmap(img, Convert.ToInt32(img.Width * size.Width), Convert.ToInt32(img.Height * size.Height));
			Graphics gpu = Graphics.FromImage(bm);
			gpu.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			return bm;
        }

		public Bitmap ConvertToBitmap(string fileName)
		{
			Bitmap bitmap;
			using (Stream bmpStream = System.IO.File.Open(fileName, System.IO.FileMode.Open))
			{
				Image image = Image.FromStream(bmpStream);

				bitmap = new Bitmap(image);

			}
			return bitmap;
		}

		private void label5_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

		private void button3_Click(object sender, EventArgs e)
		{
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

			picture = picture + @"\graph.jpg";
			Console.WriteLine(picture);

			Image img = Image.FromFile(picture);
			pictureBox1.Image = null;
			pictureBox1.Image = ZoomPicture(img, new Size(1, 1));
			pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
		}
    }
}
