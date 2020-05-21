using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;


namespace NavegadorWeb
{
    class Web_Browser
    {
        WebBrowser newWebBrowser;
        TabPage myTabPage;
        string title;
        TextBox text;
        Button b;
        Button g;
        Button f;
        Button o;
        Button y;
        Button n;
        Button newVenta;
        Button l;

        
        public Web_Browser(TabControl tabControl, Form form)
        {
            this.newWebBrowser = new WebBrowser();
            this.title = "google.com";
            this.myTabPage = new TabPage(this.title);
            tabControl.TabPages.Insert(1, this.myTabPage);
            this.text = new TextBox();
            
            tabControl.SelectedTab = this.myTabPage;

            this.b = new Button();
            this.b.SetBounds(0, 0, 33, 33);
            this.b.Image = Properties.Resources.flcha_I;
            this.b.Click += delegate
            {
                if (this.newWebBrowser.CanGoBack)
                    this.newWebBrowser.GoBack();
            };

            this.g = new Button();
            this.g.SetBounds(39, 0, 33, 33);
            this.g.Image = Properties.Resources.flecha_D;
            this.g.Click += delegate
            {
                if (this.newWebBrowser.CanGoForward)
                    this.newWebBrowser.GoForward();
            };

            this.f = new Button();
            this.f.SetBounds(78, 0, 33, 33);
            this.f.Image = Properties.Resources.curve;
            this.f.Click += delegate
            {

                this.newWebBrowser.Refresh();
            };

            this.o = new Button();
            this.o.SetBounds(117, 0, 33, 33);
            this.o.Image = Properties.Resources.icon;
            this.o.Click += delegate
            {
                this.newWebBrowser.Stop();
            };

            this.y = new Button();
            this.y.SetBounds(156, 0, 33, 33);
            this.y.Image = Properties.Resources.internet__2_;
            this.y.Click += delegate
            {
                this.newWebBrowser.GoHome();
            };

            this.n = new Button();
            this.n.SetBounds(770, 0, 33, 33);
            this.n.Image = Properties.Resources.seo_social_web_network_internet_340_icon_icons_com_61497;

            this.newVenta = new Button();
            this.newVenta.SetBounds(420, 0, 33, 33);

            this.text.Location = new Point(240, 0);
            this.text.SetBounds(193, 0, 570, 150);
            newWebBrowser.Location = new Point(5, 43);

            this.newWebBrowser.Width = form.Width - 30;
            this.newWebBrowser.Height = form.Height - 105;
            this.myTabPage.Width = form.Width - 17;
            this.myTabPage.Height = form.Height - 41;
            tabControl.Width = form.Width - 17;
            tabControl.Height = form.Height - 41;
            this.newWebBrowser.Navigate("http://www.google.com");

            this.newWebBrowser.DocumentCompleted += delegate
            {
                this.text.Text = newWebBrowser.Url.ToString();
            };
            n.Click += delegate
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://docs.oracle.com/javase/7/docs/api/java/io/StringWriter.html");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                /*if (response.StatusCode == HttpStatusCode.OK)
                {

                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    if (String.IsNullOrWhiteSpace(response.CharacterSet))
                        readStream = new StreamReader(receiveStream);
                    else
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

                    string data = readStream.ReadToEnd();
                    Console.Write(data);
                    this.newWebBrowser.DocumentText = data;
                    this.newWebBrowser.Navigating +=
                        new WebBrowserNavigatingEventHandler(webBrowser1_Navigating);
                    response.Close();
                    readStream.Close();
                }*/


            };

            this.l = new Button();
            this.l.SetBounds(805, 0, 33, 33);
            this.l.Text = "+";
            this.l.Click += delegate
            {
                //llama la funcion de agregar pagina
            };

            this.myTabPage.Controls.Add(this.b);
            this.myTabPage.Controls.Add(this.g);
            this.myTabPage.Controls.Add(this.f);
            this.myTabPage.Controls.Add(this.o);
            this.myTabPage.Controls.Add(this.y);
            this.myTabPage.Controls.Add(this.n);
            this.myTabPage.Controls.Add(this.l);
            this.myTabPage.Controls.Add(this.text);
            this.myTabPage.Controls.Add(this.newWebBrowser);

            

        }
    }
}


