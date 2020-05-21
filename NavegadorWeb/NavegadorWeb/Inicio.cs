using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Threading;

namespace NavegadorWeb
{
    public partial class Inicio : Form
    {
        Thread t1;
        WebClient Client;
        String[] cache;
        static Mutex mutex = new Mutex();
        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        TabPage myTabPage;
        
        Thread newHilo;

        List<String> historiallist = new List<String>();
       // Thread hilo;
        public Inicio()
        {
            InitializeComponent();
        }
      

        public void cargaPaginaPrincipal()
        {
            webBrowser1.Navigate("http://www.google.com");
           
        }
        private void Inicio_Load(object sender, EventArgs e)
        {
            
             t1 = new Thread(new ThreadStart(cargaPaginaPrincipal));
             t1.IsBackground = false;
             t1.Start();

            tabPage1.Text = "google.com";
            historial.Items.Add("Limpiar historial");
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoBack)
                webBrowser1.GoBack();
            tabPage1.Text = webBrowser1.Url.ToString();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void recurso()
        {
            Console.Write("entra al recurso");
            mutex.WaitOne();

        }
        private void tabPage2_Click(object sender, EventArgs e)
        {
           // newWebBrowser.Navigate(tex.Text);
        }

        void clickbuscar(object sender, EventArgs e)
        {
            //newWebBrowser.Navigate(tex.Text);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoForward)
                webBrowser1.GoForward();
            tabPage1.Text = webBrowser1.Url.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
            tabPage1.Text = webBrowser1.Url.ToString();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            webBrowser1.Stop();
            tabPage1.Text = webBrowser1.Url.ToString();
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://docs.oracle.com/javase/7/docs/api/java/io/StringWriter.html");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            
            if (response.StatusCode == HttpStatusCode.OK)
            {
                
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (String.IsNullOrWhiteSpace(response.CharacterSet))
                    readStream = new StreamReader(receiveStream);
                else
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

                string data = readStream.ReadToEnd();
                Console.Write(data);
                webBrowser1.DocumentText =data;
                webBrowser1.Navigating +=
                    new WebBrowserNavigatingEventHandler(webBrowser1_Navigating);
                response.Close();
                readStream.Close();
            }
            /*webBrowser1.Navigate(textBox1.Text);
            historial.Items.Add(textBox1.Text);
            tabPage1.Text = textBox1.Text;*/
        }

        
        private void button4_Click(object sender, EventArgs e)
        {
            webBrowser1.GoHome();
            tabPage1.Text = webBrowser1.Url.ToString();
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
            //string file = System.IO.Path.GetFileName(url);
            WebClient cln = new WebClient();
            cln.DownloadFile("https://i1.wp.com/todoimagenescristianas.com/wp-content/uploads/2013/05/imagenes-cristianas-descargar-gratis_9.jpg", @"c:\imagenes-cristianas-descargar-gratis_9.jpg");
            //cln.DownloadFile(url, file);
        }

        private void newVentana_Click(object sender, EventArgs e)
        {
            newHilo = new Thread(new ThreadStart(cargaPaginitaa));
            newHilo.IsBackground = false;
            newHilo.Start();

            historial.Items.Add("Limpiar historial");

        }
        public void cargaPaginitaa()
        {
            Web_Browser newWeb_Browser = new Web_Browser(this.tabControl1, this);

        }

        private void cagarVenCompleto(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            textBox1.Text = webBrowser1.Url.ToString();
            
            historial.Name = "Historial";
        }

        private void historial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (historial.SelectedItem != null)
            {
                if(historial.SelectedItem.ToString() == "Limpiar historial")
                {
                    historial.Items.Clear();
                    
                }
                else
                {
                    webBrowser1.Navigate(historial.SelectedItem.ToString());
                }
                
            }
        }
        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            Console.Write("Entruuuu");
            System.Windows.Forms.HtmlDocument document =
                this.webBrowser1.Document;

            if (document != null && document.All["userName"] != null &&
                String.IsNullOrEmpty(
                document.All["userName"].GetAttribute("value")))
            {
                e.Cancel = true;
                System.Windows.Forms.MessageBox.Show(
                    "You must enter your name before you can navigate to " +
                    e.Url.ToString());
            }
        }
    }

    
}
