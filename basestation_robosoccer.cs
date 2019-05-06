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
using System.Threading;

namespace KRSBI_Beroda
{
    public partial class Form1 : Form
    {
        //====================================================================================================
        TcpClient clientSocket = new TcpClient();            // Main Connection
        NetworkStream serverStream = default(NetworkStream); // Main Connection
        //====================================================================================================
        TcpClient clientSocket1 = new TcpClient();           //For Robo 1
        NetworkStream serverStream1 = default(NetworkStream);// Robo 1
        //====================================================================================================
        TcpClient clientSocket2 = new TcpClient();           //For Robo 2
        NetworkStream serverStream2 = default(NetworkStream);// Robo 2
        //====================================================================================================
        TcpClient clientSocket3 = new TcpClient();           //For Robo 2
        NetworkStream serverStream3 = default(NetworkStream);// Robo 3
        //====================================================================================================
        string readdata = null, readdata1 = null, readdata2 = null, readdata3 = null;
        public Form1()
        {
            InitializeComponent();
        }

        //================================================================="Connect Refbox"==================================================
        private void btn_conip_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (tb_iprefbox.Text == "" || tb_port.Text == "")

                {
                    MessageBox.Show("IP or Port (Main) Is Empty !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    TcpClient clientSocket = new TcpClient();  // Main Connection
                    clientSocket.Connect(tb_iprefbox.Text, Int32.Parse(tb_port.Text));
                    Thread ctThread = new Thread(getMessage);
                    ctThread.Start();
                    btn_conip.Enabled = false;
                    btn_disconip.Enabled = true;
                }
            }
            catch (SocketException)
            {
                MessageBox.Show("Port Salah", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void getMessage()
        {
            string returndata;
            while (true)
            {
                serverStream = clientSocket.GetStream();
                var buffsize = clientSocket.ReceiveBufferSize;
                byte[] inStream = new byte[buffsize];

                serverStream.Read(inStream, 0, buffsize);
                returndata = System.Text.Encoding.ASCII.GetString(inStream);
                readdata = returndata;
                msg();

            }
        }

        private void msg()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(msg));
            }
            else
            {
                tb_command.Text = readdata;
            }
        }

        private void btn_disconip_Click(object sender, EventArgs e)
        {
            clientSocket.Close();
            btn_conip.Enabled = true;
            btn_disconip.Enabled = false;
            tb_command.Text = "";
            MessageBox.Show("Refbox Disconnect");
        }

        //================================================================="Connect Robot 1"==================================================

        private void btn_conrobo1_Click(object sender, EventArgs e)
        {

             try
            {
                if (tb_iprobo1.Text == "" || tb_portrobo1.Text == "")
                {
                    MessageBox.Show("IP or Port (1) Is Empty !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {

                    clientSocket1.Connect(tb_iprobo1.Text, Int32.Parse(tb_portrobo1.Text));
                    Thread ctThread = new Thread(getMessage1);
                    ctThread.Start();
                    robotstatus1.Text = "CONNECTED";
                }
            }
            catch (SocketException)
            {
                MessageBox.Show("Port Salah", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btn_send1_Click(object sender, EventArgs e)
        {
            byte[] outStream1 = Encoding.ASCII.GetBytes(tb_send1.Text);
            serverStream1.Write(outStream1, 0, outStream1.Length);
            serverStream1.Flush();
        }

        private void getMessage1()
        {
            string returndata1;
            while (true)
            {
                serverStream1 = clientSocket1.GetStream();
                var buffsize = clientSocket1.ReceiveBufferSize;
                byte[] inStream1 = new byte[buffsize];

                serverStream1.Read(inStream1, 0, buffsize);
                returndata1 = System.Text.Encoding.ASCII.GetString(inStream1);
                readdata1 = returndata1;
                msg1();
            }
        }

        private void msg1()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(msg1));
            }
            else
            {

                 byte[] outStream1 = Encoding.ASCII.GetBytes(tb_command.Text);
                 serverStream1.Write(outStream1, 0, outStream1.Length);
                 serverStream1.Flush();
                 
            }
        }

        private void btn_disconrobo1_Click(object sender, EventArgs e)
        {
            //serverStream1.Close();
            
            byte[] outStream1 = Encoding.ASCII.GetBytes("KILL");
            serverStream1.Write(outStream1, 0, outStream1.Length);
            serverStream1.Flush();
            clientSocket1.Close();
            MessageBox.Show("Robo1 Disconnect");
        }
        sss
        //==========================================================="Connect Robot 2"===========================================================

        private void btn_conrobo2_Click(object sender, EventArgs e)
        {
            try
            {
                if (tb_iprobo2.Text == "" || tb_portrobo2.Text == "")
                {
                    MessageBox.Show("IP or Port (2) Is Empty !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    clientSocket2.Connect(tb_iprobo2.Text, Int32.Parse(tb_portrobo2.Text));
                    Thread ctThread = new Thread(getMessage2);
                    ctThread.Start();
                    robotstatus2.Text = "CONNECTED";

                }
            }

            catch (SocketException)
            {
                MessageBox.Show("Port Salah", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btn_send2_Click(object sender, EventArgs e)
        {
            
            byte[] outStream2 = Encoding.ASCII.GetBytes(tb_send2.Text);
            serverStream2.Write(outStream2, 0, outStream2.Length);
            serverStream2.Flush();
        }

   
        private void getMessage2()
        {
            string returndata2;
            while (true)
            {
                serverStream2 = clientSocket2.GetStream();
                var buffsize = clientSocket2.ReceiveBufferSize;
                byte[] inStream2 = new byte[buffsize];

                serverStream2.Read(inStream2, 0, buffsize);
                returndata2 = System.Text.Encoding.ASCII.GetString(inStream2);
                readdata2 = returndata2;
                msg2();
            }
        }

        private void msg2()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(msg2));
            }
            else
            {
               
                byte[] outStream2 = Encoding.ASCII.GetBytes(tb_command.Text);
                serverStream2.Write(outStream2, 0, outStream2.Length);
                serverStream2.Flush();
                
            }
        }

        private void btn_disconrobo2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Robo2 Disconnect");
            clientSocket2.Close();
        }
        //======================================================================"Connect Robot 3"===================================================
        private void btn_conrobo3_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (tb_iprobo3.Text == "" || tb_portrobo3.Text == "")
                {
                    MessageBox.Show("IP or Port (3) Is Empty !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    clientSocket3.Connect(tb_iprobo3.Text, Int32.Parse(tb_portrobo3.Text));
                    Thread ctThread = new Thread(getMessage3);
                    ctThread.Start();
                    robotstatus3.Text = "CONNECTED";

                }
            }

            catch (SocketException)
            {
                MessageBox.Show("Port Salah", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      

        private void btn_send3_Click(object sender, EventArgs e)
        {
            
            byte[] outStream3 = Encoding.ASCII.GetBytes(tb_send3.Text);
            serverStream3.Write(outStream3, 0, outStream3.Length);
            serverStream3.Flush();
        }

      

        private void getMessage3()
        {
            string returndata3;
            while (true)
            {
                serverStream3 = clientSocket3.GetStream();
                var buffsize = clientSocket3.ReceiveBufferSize;
                byte[] inStream3 = new byte[buffsize];

                serverStream3.Read(inStream3, 0, buffsize);
                returndata3 = System.Text.Encoding.ASCII.GetString(inStream3);
                readdata3 = returndata3;
                msg3();
            }
        }

        private void msg3()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(msg3));
            }
            else
            {
                
                byte[] outStream3 = Encoding.ASCII.GetBytes(tb_command.Text);
                serverStream3.Write(outStream3, 0, outStream3.Length);
                serverStream3.Flush();
                
            }
        }

        private void btn_disconrobo3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Robo3 Disconnect");
            clientSocket3.Close();
        }

    }
}
