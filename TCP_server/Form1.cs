using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace TCP_server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private TcpListener serwer = null;
        private TcpClient klient = null;

        private void start_Click(object sender, EventArgs e)
        {
            IPAddress adresIP = null;

            try
            {
                adresIP = IPAddress.Parse(adres.Text);
            }
            catch
            {
                MessageBox.Show("Błędny format adresu IP", "Błąd");
                adres.Text = string.Empty;
                return;
            }

            int port = Convert.ToUInt16(my_port.Value);

            try
            {
                serwer = new TcpListener(adresIP, port);
                serwer.Start();

                klient = serwer.AcceptTcpClient();

                info_o_polaczeniu.Items.Add("Nawiązano półączenie!");
                start.Enabled = false;
                stop.Enabled = true;
                klient.Close();
                serwer.Stop();
            }
            catch(Exception ex)
            {
                info_o_polaczeniu.Items.Add("Bład inicjacji serwera!");
                MessageBox.Show(ex.ToString(),"Błąd");
            }

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            stop.Enabled = false;
        }

        private void stop_Click(object sender, EventArgs e)
        {
            serwer.Stop();
            klient.Close();

            info_o_polaczeniu.Items.Add("Zakończono prace serwera!");
            start.Enabled = true;
            stop.Enabled = false;
        }
    }
}
