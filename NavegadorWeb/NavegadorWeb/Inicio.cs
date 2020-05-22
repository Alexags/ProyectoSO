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
using System.Security.Policy;

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
        static WebBrowser newWebBrowser;
        Web_Browser elementos;
        Thread newHilo;
        static int cont = 0;
        static WebBrowser webBrowser1;
        static List<Thread> hilos = new List<Thread>();
        static List<TabPage> tabs = new List<TabPage>();
        List<String> historiallist = new List<String>();
        public event EventHandler FileDownload;
        public static bool cerrojo = true;
        static bool ventana = false;
        
        static Dictionary<string, string> map = new Dictionary<string, string>();
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
            webBrowser1 = new WebBrowser();
            webBrowser1.Location = new Point(5, 60);
            webBrowser1.Width = this.Width - 30;
            webBrowser1.Height = this.Height - 105;
            tabPage1.Controls.Add(webBrowser1);
            t1 = new Thread(new ThreadStart(cargaPaginaPrincipal));
            t1.Name = "Thread" + cont;
            cont++;
            t1.IsBackground = false;
            t1.Start();
            tabPage1.Text = "google.com";
            tabPage1.Name = "tab0";
            
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
            ventana = false;
            recurso(textBox1, webBrowser1);

            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(textBox1.Text);


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


        private void newVentana_Click(object sender, EventArgs e)
        {
            string title = "google.com";
            myTabPage = new TabPage(title);
            myTabPage.Name = "tab" + cont;
            this.tabControl1.TabPages.Insert(cont, myTabPage);
            TextBox tex = new TextBox();
            newWebBrowser = new WebBrowser();
            Button b = new Button();
            this.tabControl1.SelectedTab = myTabPage;

            b.SetBounds(0, 0, 33, 33);
            b.Image = Properties.Resources.flcha_I;
            b.Click += delegate
            {
                if (newWebBrowser.CanGoBack)
                    newWebBrowser.GoBack();
            };

            Button g = new Button();
            g.SetBounds(39, 0, 33, 33);
            g.Image = Properties.Resources.flecha_D;

            g.Click += delegate
            {
                if (newWebBrowser.CanGoForward)
                    newWebBrowser.GoForward();
            };

            Button f = new Button();
            f.SetBounds(78, 0, 33, 33);
            f.Image = Properties.Resources.curve;

            f.Click += delegate
            {

                newWebBrowser.Refresh();
            };

            Button o = new Button();
            o.SetBounds(117, 0, 33, 33);
            o.Image = Properties.Resources.icon;
            o.Click += delegate
            {
                newWebBrowser.Stop();
            };

            Button y = new Button();
            y.SetBounds(156, 0, 33, 33);
            y.Image = Properties.Resources.internet__2_;
            y.Click += delegate
            {
                newWebBrowser.GoHome();
            };
            Button n = new Button();
            n.SetBounds(770, 0, 33, 33);
            n.Image = Properties.Resources.seo_social_web_network_internet_340_icon_icons_com_61497;


            Button newVenta = new Button();
            newVenta.SetBounds(420, 0, 33, 33);


            tex.Location = new Point(240, 0);
            //tex.Width = 30;
            //tex.Height = 30;
            tex.SetBounds(193, 0, 570, 150);
            newWebBrowser.Location = new Point(5, 43);

            newWebBrowser.Width = this.Width - 30;
            newWebBrowser.Height = this.Height - 105;
            myTabPage.Width = this.Width - 17;
            myTabPage.Height = this.Height - 41;
            tabControl1.Width = this.Width - 17;
            tabControl1.Height = this.Height - 41;
            //newWebBrowser.Navigate("http://www.google.com");
            
            newWebBrowser.DocumentCompleted += delegate
            {
                tex.Text = newWebBrowser.Url.ToString();
            };
            n.Click += delegate
            {
                ventana = true;
                recurso(tex, newWebBrowser);
            };

            Button l = new Button();
            l.SetBounds(805, 0, 33, 33);
            l.Text = "+";
            l.Click += delegate
            {
                newVentana_Click(sender, e);
            };

            myTabPage.Controls.Add(b);
            myTabPage.Controls.Add(g);
            myTabPage.Controls.Add(f);
            myTabPage.Controls.Add(o);
            myTabPage.Controls.Add(y);
            myTabPage.Controls.Add(n);
            myTabPage.Controls.Add(l);
            myTabPage.Controls.Add(tex);
            //myTabPage.Name = "tab" + cont;
            myTabPage.Controls.Add(newWebBrowser);
            tabs.Add(myTabPage);
            newHilo = new Thread(new ThreadStart(cargaPaginitaa));
            newHilo.Name = "tr" + cont;
            cont++;
            newHilo.IsBackground = false;
            newHilo.Start();
            hilos.Add(newHilo);
            /*newHilo = new Thread(new ThreadStart(cargaPaginitaa));
            newHilo.IsBackground = false;
            newHilo.Start();

            historial.Items.Add("Limpiar historial");*/

        }
        public void cargaPaginitaa()
        {
            /*myTabPage.Controls.Add(n);
            myTabPage.Controls.Add(newWebBrowser);*/
            newWebBrowser.Navigate("http://www.google.com");
            //newWebBrowser = new WebBrowser();
            //elementos = new Web_Browser(this.tabControl1, this, newWebBrowser);

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
                if (historial.SelectedItem.ToString() == "Limpiar historial")
                {
                    historial.Items.Clear();

                }
                else
                {
                    webBrowser1.Navigate(historial.SelectedItem.ToString());
                }

            }
        }
        

        private void WebBrowser1_ProgressChanged(Object sender, WebBrowserProgressChangedEventArgs e) { 

            

           /* System.Text.StringBuilder messageBoxCS = new System.Text.StringBuilder();
            messageBoxCS.AppendFormat("{0} = {1}", "CurrentProgress", e.CurrentProgress);
            messageBoxCS.AppendLine();
            messageBoxCS.AppendFormat("{0} = {1}", "MaximumProgress", e.MaximumProgress);
            messageBoxCS.AppendLine();
            MessageBox.Show(messageBoxCS.ToString(), "ProgressChanged Event");*/
        }

        //
        public void descargaArchivos()
        {
            /*https://eloutput.com/app/uploads-eloutput.com/2020/05/atories-descargar-instagram.jpg*/
            
            string remoteUri = urlDescargar.Text;
            string fileName = obtenerNombre(remoteUri), myStringWebResource = null;

            WebClient myWebClient = new WebClient();

            myStringWebResource = remoteUri /*+ fileName*/;
            Console.WriteLine("Downloading File \"{0}\" from \"{1}\" .......\n\n", fileName, myStringWebResource);
            // Download the Web resource and save it into the current filesystem folder.
            myWebClient.DownloadFile(myStringWebResource, fileName);
            //DownloadFile
            Console.WriteLine("Successfully Downloaded File \"{0}\" from \"{1}\"", fileName, myStringWebResource);
            Console.WriteLine("\nDownloaded file saved in the following file system folder:\n\t" + Application.StartupPath);

            Console.WriteLine("nombre archivo"+fileName);

        }

        public string obtenerNombre(string hrefLink)
        {
            string[] parts = hrefLink.Split('/');
            string fileName = "";

            if (parts.Length > 0)
                fileName = parts[parts.Length - 1];
            else
                fileName = hrefLink;
            return fileName;

        }
        private static void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            System.Windows.Forms.HtmlDocument document;
            if (ventana)
            {
                 document = newWebBrowser.Document;
            }
            else
            {
                document = webBrowser1.Document;
            }
            

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

        private static void recurso(TextBox tex, WebBrowser browser)
        {
            if (cerrojo)
            {
                cerrojo = false;
                if (!map.ContainsKey(tex.Text))
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(tex.Text);

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
                        browser.DocumentText = data;
                        browser.Navigating +=
                            new WebBrowserNavigatingEventHandler(webBrowser1_Navigating);
                        Console.WriteLine("entaksdjcnkajsdro");
                        map.Add(tex.Text, data);
                        response.Close();
                        readStream.Close();
                    }
                }else
                {
                    browser.DocumentText = map[tex.Text];
                    browser.Navigating +=
                        new WebBrowserNavigatingEventHandler(webBrowser1_Navigating);
                }
                cerrojo = true;
            }            
        }
        private void Prueba_Click(object sender, EventArgs e)
        {
            descargaArchivos();
        }

        private void urlDescargar_TextChanged(object sender, EventArgs e)
        {

        }

        private void descargaFiles(object sender, EventArgs e)
        {   
            Console.WriteLine("descarga evento.");
           // MessageBox.Show("You are in the WebBrowser.FileDownload event.");
        }

        private void Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
          

        }

        void client_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
    
        }

        private void limpiarCaché_Click(object sender, EventArgs e)
        {
            map.Clear();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }


    }

    
}
