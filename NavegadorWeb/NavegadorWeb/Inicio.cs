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
using System.Web;
using System.Runtime.InteropServices.ComTypes;

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
        WebBrowser newWebBrowser;
        Web_Browser elementos;
        Thread newHilo;
        static int cont = 0;
        static WebBrowser webBrowser1;
        static List<Thread> hilos = new List<Thread>();
        static List<WebBrowser> webs = new List<WebBrowser>();
        public static List<String> historiallist = new List<String>();
        public event EventHandler FileDownload;
        public static bool cerrojo = true;
        static bool solicitando = false;
        static bool ventana = false;
        WebClient myWebClient = new WebClient();
        string url = null;
        Boolean semaforo = false;

        static Dictionary<string, string> map = new Dictionary<string, string>();
        // Thread hilo;
        public Inicio()
        {
            InitializeComponent();
            
        }

        public void cargaPaginaPrincipal()
        {
            webBrowser1.Navigate("http://www.google.com");
            webBrowser1.ScriptErrorsSuppressed = true;

        }
        
        private void Inicio_Load(object sender, EventArgs e)
        {
           
            webBrowser1 = new WebBrowser();
            webBrowser1.Location = new Point(5, 60);
            webBrowser1.Width = this.Width - 30;
            webBrowser1.Height = this.Height - 105;
            tabPage1.Controls.Add(webBrowser1);
            t1 = new Thread(new ThreadStart(cargaPaginaPrincipal));
            t1.IsBackground = false;
            t1.Start();
            tabPage1.Text = "google.com";
            
            historial.Items.Add("Limpiar historial");
            webBrowser1.Navigated += delegate
            {
                Console.WriteLine("Pagina cargada");
            };

            myWebClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(cargando);
            myWebClient.DownloadFileCompleted += new AsyncCompletedEventHandler(descargando);

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
            solicitando = true;
            ventana = false;
            recurso(textBox1);
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
            this.tabControl1.TabPages.Add(myTabPage);
            TextBox tex = new TextBox();
            WebClient newmyWebClient = new WebClient();
            ProgressBar progress = new ProgressBar();
            Label desPor = new Label();
            newWebBrowser = new WebBrowser();
            
            newWebBrowser.Name = "web" + cont;
            cont++;
            webs.Add(newWebBrowser);
            newWebBrowser.ScriptErrorsSuppressed = true;
            Button b = new Button();
            Button borrarTab = new Button();
            Button descar = new Button();
            Button limCache = new Button();
            this.tabControl1.SelectedTab = myTabPage;
            ComboBox hist = new ComboBox();

            desPor.Text = "0%";
            desPor.Width = 21;
            desPor.Height = 13;
            desPor.Location = new Point(182, 45);

            progress.Width = 169;
            progress.Height = 23;
            progress.Location = new Point(7, 37);

            descar.Text = "Descargar";
            descar.Width = 75;
            descar.Height = 23;
            descar.Location = new Point(287, 35);

            limCache.Text = "Limpiar Cache";
            limCache.Width = 96;
            limCache.Height = 23;
            limCache.Location = new Point(368, 35);

            borrarTab.Text = "-";
            borrarTab.Font = new Font("Arial", 14, FontStyle.Bold);
            borrarTab.Width = 32;
            borrarTab.Height = 32;
            borrarTab.Location = new Point(840, 0);
            hist.Width = 185;
            hist.Height = 22;
            hist.Location = new Point(875, 4);
            hist.Text = "Historial";
            hist.BackColor = Color.Black;
            hist.ForeColor = Color.White;
            hist.Items.Add("Limpiar historial");
            newWebBrowser.ScriptErrorsSuppressed = true;

            newmyWebClient.DownloadProgressChanged += (s, es) =>
            {
                progress.Value = es.ProgressPercentage ;
                desPor.Text = progress.Value.ToString() + "%";
                semaforo = true;
            };

            newmyWebClient.DownloadFileCompleted += (s, es) =>
            {
                progress.Value = 0;
                desPor.Text = "0%";
                semaforo = false;
                if (MessageBox.Show("Desea abrir el archivo descargado?", "Archivo descargado", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(url);

                }
            };

            descar.Click += delegate
            {
                if (semaforo == true)
                {

                    MessageBox.Show("No se puede descargar más de un archivo a la vez.");
                }
                else
                {
                    if (RemoteFileExists(tex.Text) == true)
                    {
                        SaveFileDialog dialog = new SaveFileDialog();
                        dialog.Filter = "Todos los archivos|*.*";
                        dialog.FileName = tex.Text.Substring(tex.Text.LastIndexOf("/") + 1);
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            url = dialog.FileName;
                            newmyWebClient.DownloadFileAsync(new Uri(tex.Text), dialog.FileName);
                        }
                    }
                    else
                    {
                        MessageBox.Show("La url es incorrecta.");
                    }

                }
            };
            limCache.Click += delegate
            {
                map.Clear();
            };

            borrarTab.Click += delegate
            {
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                if (tabControl1.TabPages.Count == 0)
                {
                    Application.Exit();
                }
            };

            hist.SelectedIndexChanged += delegate
            {
                if (hist.SelectedItem != null)
                {
                    if (hist.SelectedItem.ToString() == "Limpiar historial")
                    {
                        hist.Items.Clear();
                        historiallist.Clear();
                    }
                    else
                    {
                        tex.Text = hist.SelectedItem.ToString();
                        foreach (WebBrowser web in webs)
                        {
                            if (web.Name.Substring(3).Equals(hist.Parent.Name.Substring(3)))
                            {
                                if (cerrojo)
                                {
                                    cerrojo = false;
                                    if (!map.ContainsKey(tex.Text))
                                    {
                                        if (RemoteFileExists(tex.Text) == true)
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
                                                web.DocumentText = data;
                                                web.Navigating += (s, es) => {
                                                    System.Windows.Forms.HtmlDocument document;
                                                    document = newWebBrowser.Document;

                                                    if (document != null && document.All["userName"] != null &&
                                                        String.IsNullOrEmpty(
                                                        document.All["userName"].GetAttribute("value")))
                                                    {
                                                        es.Cancel = true;
                                                        System.Windows.Forms.MessageBox.Show(
                                                            "You must enter your name before you can navigate to " +
                                                            es.Url.ToString());
                                                    }
                                                };
                                                map.Add(tex.Text, data);

                                                response.Close();
                                                readStream.Close();
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("La url es incorrecta.");
                                        }

                                    }
                                    else
                                    {
                                        web.DocumentText = map[tex.Text];
                                        web.Navigating += (s, es) => {
                                            System.Windows.Forms.HtmlDocument document;
                                            document = newWebBrowser.Document;

                                            if (document != null && document.All["userName"] != null &&
                                                String.IsNullOrEmpty(
                                                document.All["userName"].GetAttribute("value")))
                                            {
                                                es.Cancel = true;
                                                System.Windows.Forms.MessageBox.Show(
                                                    "You must enter your name before you can navigate to " +
                                                    es.Url.ToString());
                                            }
                                        };
                                    }
                                    historiallist.Add(tex.Text);
                                    cerrojo = true;
                                }
                            }
                        }
                    }

                }
            };
            hist.MouseClick += delegate
            {
                hist.Items.Clear();
                foreach (string elemento in historiallist)
                {
                    hist.Items.Add(elemento);
                }
                hist.Items.Add("Limpiar historial");
            };
            b.SetBounds(0, 0, 33, 33);
            b.Image = Properties.Resources.flcha_I;
            b.Click += delegate
            {
                if (newWebBrowser.CanGoBack)
                {
                    foreach (WebBrowser web in webs)
                    {
                        if (web.Name.Substring(3).Equals(hist.Parent.Name.Substring(3)))
                        {
                            web.GoBack();
                        }
                    }
                }
                    
            };

            Button g = new Button();
            g.SetBounds(39, 0, 33, 33);
            g.Image = Properties.Resources.flecha_D;

            g.Click += delegate
            {
                if (newWebBrowser.CanGoForward)
                {
                    foreach (WebBrowser web in webs)
                    {
                        if (web.Name.Substring(3).Equals(hist.Parent.Name.Substring(3)))
                        {
                            newWebBrowser.GoForward();
                        }
                    }
                }
                    
            };

            Button f = new Button();
            f.SetBounds(78, 0, 33, 33);
            f.Image = Properties.Resources.curve;

            f.Click += delegate
            {
                foreach (WebBrowser web in webs)
                {
                    if (web.Name.Substring(3).Equals(hist.Parent.Name.Substring(3)))
                    {
                        newWebBrowser.Refresh();
                    }
                }
                        
            };

            Button o = new Button();
            o.SetBounds(117, 0, 33, 33);
            o.Image = Properties.Resources.icon;
            o.Click += delegate
            {
                foreach (WebBrowser web in webs)
                {
                    if (web.Name.Substring(3).Equals(hist.Parent.Name.Substring(3)))
                    {
                        newWebBrowser.Stop();
                    }
                } 
            };

            Button y = new Button();
            y.SetBounds(156, 0, 33, 33);
            y.Image = Properties.Resources.internet__2_;
            y.Click += delegate
            {
                foreach (WebBrowser web in webs)
                {
                    if (web.Name.Substring(3).Equals(hist.Parent.Name.Substring(3)))
                    {
                        newWebBrowser.GoHome();
                    }
                }    
            };
            Button n = new Button();
            n.SetBounds(770, 0, 33, 33);
            n.Image = Properties.Resources.seo_social_web_network_internet_340_icon_icons_com_61497;
            Button newVenta = new Button();
            newVenta.SetBounds(420, 0, 33, 33);
            tex.Location = new Point(240, 0);
            tex.SetBounds(193, 0, 570, 150);
            newWebBrowser.Location = new Point(5, 60);
            newWebBrowser.Width = this.Width - 30;
            newWebBrowser.Height = this.Height - 105;
            myTabPage.Width = this.Width - 17;
            myTabPage.Height = this.Height - 41;
            tabControl1.Width = this.Width - 17;
            tabControl1.Height = this.Height - 41;
            n.Click += delegate
            {
                foreach(WebBrowser web in webs)
                {
                    if (web.Name.Substring(3).Equals(n.Parent.Name.Substring(3)))
                    {
                        Console.WriteLine(web.Name.Substring(3));
                        if (cerrojo)
                        {
                            cerrojo = false;
                            if (!map.ContainsKey(tex.Text))
                            {
                                if (RemoteFileExists(tex.Text) == true)
                                {
                                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(tex.Text);
                                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                                    if (response.StatusCode == HttpStatusCode.OK)
                                    {
                                        Console.WriteLine("llega");
                                        Stream receiveStream = response.GetResponseStream();
                                        StreamReader readStream = null;
                                        if (String.IsNullOrWhiteSpace(response.CharacterSet))
                                            readStream = new StreamReader(receiveStream);
                                        else
                                            readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                                        string data = readStream.ReadToEnd();
                                        web.DocumentText = data;
                                        web.Navigating += (s, es) => {
                                            System.Windows.Forms.HtmlDocument document;
                                            document = newWebBrowser.Document;

                                            if (document != null && document.All["userName"] != null &&
                                                String.IsNullOrEmpty(
                                                document.All["userName"].GetAttribute("value")))
                                            {
                                                es.Cancel = true;
                                                System.Windows.Forms.MessageBox.Show(
                                                    "You must enter your name before you can navigate to " +
                                                    es.Url.ToString());
                                            }
                                        };
                                        map.Add(tex.Text, data);
                                        response.Close();
                                        readStream.Close();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("La url es incorrecta.");
                                }
                            }
                            else
                            {
                                web.DocumentText = map[tex.Text];
                                web.Navigating += (s, es) => {
                                            System.Windows.Forms.HtmlDocument document;
                                            document = newWebBrowser.Document;

                                            if (document != null && document.All["userName"] != null &&
                                                String.IsNullOrEmpty(
                                                document.All["userName"].GetAttribute("value")))
                                            {
                                                es.Cancel = true;
                                                System.Windows.Forms.MessageBox.Show(
                                                    "You must enter your name before you can navigate to " +
                                                    es.Url.ToString());
                                            }
                                        };
                            }
                            historiallist.Add(tex.Text);
                            cerrojo = true;
                        }
                    }
                }
            };

            Button l = new Button();
            l.SetBounds(805, 0, 33, 33);
            l.Text = "+";
            l.Font = new Font("Arial", 14, FontStyle.Bold);
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
            myTabPage.Controls.Add(hist);
            myTabPage.Controls.Add(borrarTab);
            myTabPage.Controls.Add(limCache);
            myTabPage.Controls.Add(descar);
            myTabPage.Controls.Add(progress);
            myTabPage.Controls.Add(desPor);
            myTabPage.Controls.Add(newWebBrowser);
            newHilo = new Thread(new ThreadStart(cargaPaginitaa));
            newHilo.IsBackground = false;
            newHilo.Start();
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
                    historiallist.Clear();
                }
                else
                {
                    solicitando = true;
                    ventana = false;
                    textBox1.Text = historial.SelectedItem.ToString();
                    recurso(textBox1);
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

            url = textBox1.Text;
            string fileName = obtenerNombre(url), myStringWebResource = null;

            myWebClient = new WebClient();

            myStringWebResource = url /*+ fileName*/;
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
        private  void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            System.Windows.Forms.HtmlDocument document;
            document = webBrowser1.Document;
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

        

        private  void recurso(TextBox tex)
        {
            if (cerrojo)
            {
                cerrojo = false;

                if (!map.ContainsKey(tex.Text))
                {
                    if (RemoteFileExists(tex.Text) == true)
                    {
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(tex.Text);
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            Console.WriteLine("llega");
                            Stream receiveStream = response.GetResponseStream();
                            StreamReader readStream = null;
                            if (String.IsNullOrWhiteSpace(response.CharacterSet))
                                readStream = new StreamReader(receiveStream);
                            else
                                readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                            string data = readStream.ReadToEnd();
                            webBrowser1.DocumentText = data;
                            webBrowser1.Navigating +=
                                new WebBrowserNavigatingEventHandler(webBrowser1_Navigating);
                            map.Add(tex.Text, data);

                            response.Close();
                            readStream.Close();
                        }
                    }
                    else
                    {
                            MessageBox.Show("La url es incorrecta.");
                    }
                    
                }
                else
                {
                    webBrowser1.DocumentText = map[tex.Text];
                    webBrowser1.Navigating +=
                        new WebBrowserNavigatingEventHandler(webBrowser1_Navigating);
                }
                historiallist.Add(tex.Text);
                cerrojo = true;
            }
        }
        private static bool RemoteFileExists(string url)
        {
            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TRUE if the Status code == 200
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                //Any exception will returns false.
                return false;
            }
        }

        private void Prueba_Click(object sender, EventArgs e)
        {
            if (semaforo ==true)
            {

                MessageBox.Show("No se puede descargar más de un archivo a la vez.");
            }
            else
            {
                if (RemoteFileExists(textBox1.Text) == true)
                {
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.Filter = "Todos los archivos|*.*";
                    dialog.FileName = textBox1.Text.Substring(textBox1.Text.LastIndexOf("/") + 1);
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        url = dialog.FileName;
                        myWebClient.DownloadFileAsync(new Uri(textBox1.Text), dialog.FileName);
                    }
                }
                else
                {
                    MessageBox.Show("La url es incorrecta.");
                }
            }
            
            //descargaArchivos();
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

        private void historial_MouseClick(object sender, MouseEventArgs e)
        {
            historial.Items.Clear();
            foreach (string elemento in historiallist)
            {
                historial.Items.Add(elemento);
            }
            historial.Items.Add("Limpiar historial");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
            if(tabControl1.TabPages.Count == 0)
            {
                Application.Exit();
            }
        }

        private void cargando(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage ;
            label1.Text = progressBar1.Value.ToString() + "%";
            semaforo = true;

        }

        private void descargando(object sender, AsyncCompletedEventArgs e)
        {
            progressBar1.Value = 0;
            label1.Text = "0%";
            semaforo = false;
            if (MessageBox.Show("Desea abrir el archivo descargado?","Archivo descargado",MessageBoxButtons.YesNo,MessageBoxIcon.Information)==DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(url);

            }
           
        }

        private void progressBar1_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }

    
}
