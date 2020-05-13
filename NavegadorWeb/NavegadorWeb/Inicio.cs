﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;



namespace NavegadorWeb
{
    public partial class Inicio : Form
    {
        WebClient Client;
        public Inicio()
        {
            InitializeComponent();
            

        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://www.google.com");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoBack)
                webBrowser1.GoBack();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            string title = "TabPage " + (tabControl1.TabCount + 1).ToString();
            TabPage myTabPage = new TabPage(title);
            this.tabControl1.TabPages.Insert(1,myTabPage);
            Button b = new Button();
            b.Text = "bosco";
            
            
            myTabPage.Controls.Add(b);
           

        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoForward)
                webBrowser1.GoForward();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            webBrowser1.Stop();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(textBox1.Text); 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            webBrowser1.GoHome();
        }

        private void Inicio_Resize(object sender, EventArgs e)
        {
            webBrowser1.Width = this.Width - 30;
            webBrowser1.Height = this.Height - 105;
            tabPage1.Width = this.Width - 17;
            tabPage1.Height = this.Height - 41;
            tabControl1.Width = this.Width - 17;
            tabControl1.Height = this.Height - 41;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            downloadFile();
        }

        private void downloadFile()
        {
            Console.Write("Entra");
            //string file = System.IO.Path.GetFileName(url);
            WebClient cln = new WebClient();
            cln.DownloadFile("https://i1.wp.com/todoimagenescristianas.com/wp-content/uploads/2013/05/imagenes-cristianas-descargar-gratis_9.jpg", @"c:\imagenes-cristianas-descargar-gratis_9.jpg");
            //cln.DownloadFile(url, file);
        }
    }
}
