﻿using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace GUI
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
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

		}

		private void button2_Click(object sender, EventArgs e)
		{
			String path = textBox1.Text;
			String file = textBox2.Text;
			String method;
			Boolean occurences = checkBox1.Checked;
			Boolean isChecked = radioButton1.Checked;
			String picture = Directory.GetCurrentDirectory();
			string[] ans;

			while (Path.GetFileName(picture) != "tubes2-stima")
			{
				picture = Directory.GetParent(picture).FullName;
			}

			picture = picture + @"\pictures\";

			if (isChecked)
				method = radioButton1.Text;
			else
				method = radioButton2.Text;

			ans = BFS.BFSMain(path, file, occurences);

			pictureBox1.ImageLocation = picture + "graph.jpg";
			//BFS.printArr(ans);
			/*
			while (true)
            {
				pictureBox1.ImageLocation = picture + "gempa.jpg";
				Application.DoEvents();
				Task.Delay(1000).Wait();
				Application.DoEvents();


				pictureBox1.ImageLocation = picture + "kocheng.jpg";
				Application.DoEvents();
				Task.Delay(1000).Wait();
				Application.DoEvents();

			}
			*/

			MessageBox.Show(path + "\n" + file + "\n" + occurences + "\n" + method + "\n" + picture + "\n" + ans[0]);






		}

		private void label3_Click(object sender, EventArgs e)
		{

		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{

		}
	}
}