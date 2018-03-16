using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherPrediction
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create("ftp://aishwarya-more.square7.ch/a.txt");

                ftpReq.UseBinary = true;
                ftpReq.Method = WebRequestMethods.Ftp.UploadFile;
                ftpReq.Credentials = new NetworkCredential("aishwarya-more", "diadulce");

                byte[] b = File.ReadAllBytes(@"C:\input.txt");
                string value = File.ReadAllText(@"C:\input.txt", System.Text.Encoding.Default);
                ftpReq.ContentLength = b.Length;
                using (Stream s = ftpReq.GetRequestStream())
                {
                    s.Write(b, 0, b.Length);
                }

                FtpWebResponse ftpResp = (FtpWebResponse)ftpReq.GetResponse();

                if (ftpResp != null)
                {
                    if (ftpResp.StatusDescription.StartsWith("226"))
                    {
                        label3.Text = value;
                    }
                    else
                    {
                        label3.Text = "File Not Uploaded";


                    }
                }
            }

            catch (Exception se)
            {
                MessageBox.Show(se.ToString(), "Exception");
            }

        }
    }
}
