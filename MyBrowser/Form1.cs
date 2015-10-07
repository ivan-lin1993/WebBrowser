using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Net.NetworkInformation;

namespace MyBrowser
{


    public partial class MyBrowser : Form
    {
        int now = 0;
        int count = 0;
        string nowhttp = "";
        string txt = "mylist.txt";
        string downloadHttp = "https://docs.google.com/document/export?format=txt&id=1UJ_-1lWb_KrJvXKvoqRAgJwEVFuInxFqVA5HJ-5yqAw&token=AC4w5Vh6Br1q3Os8qb59jo0kDufFGfwd7A%3A1444204710231";
        System.IO.StreamReader file;
        
        public MyBrowser()
        {

            InitializeComponent();
            webBrowser1.ScriptErrorsSuppressed = true;
            Start();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (!webBrowser1.IsBusy) //在網頁不忙錄時才更新
            {
                if ((nowhttp = file.ReadLine()) != null)
                {
                    label2.Text = nowhttp;
                    //webBrowser1.Refresh();
                    count++;
                    label1.Text = "Now: " + count; //顯示出更新了多少次
                    webBrowser1.Navigate(nowhttp);
                }
                else
                {
                    timer1.Enabled = false;
                    file.Close();
                    //file = new System.IO.StreamReader(txt);
                    //timer1.Enabled = true;
                    label1.Text = "Done";
                    DeletMyList();
                }
            }
        }
        private void DeletMyList()
        {
            File.Delete(txt);
            wait_time.Enabled = true;
            //Application.Exit();
        }
        private void getDowload_Click(object sender, EventArgs e)
        {
        }


        bool ConnectGoogleTW()
        {
            //Google網址
            string googleTW = "www.google.tw";
            //Ping網站
            Ping p = new Ping();
            //網站的回覆
            PingReply reply;

            try
            {
                //取得網站的回覆
                reply = p.Send(googleTW);
                //如果回覆的狀態為Success則return true
                if (reply.Status == IPStatus.Success) { return true; }

            }

            //catch這裡的Exception, 是有可能網站當下的某某狀況造成, 可以直接讓它傳回false.
            //或在重覆try{}裡的動作一次
            catch { return false; }

            //如果reply.Status !=IPStatus.Success, 直接回傳false
            return false;
        }

        private void Start()
        {
            WebClient wc = new WebClient();
            wait_time.Enabled = false;
            if (ConnectGoogleTW()==true)
            {
                wc.DownloadFile(downloadHttp, txt);
                Hide();
                file = new System.IO.StreamReader("mylist.txt");
                timer1.Enabled = true;
                MessageBox.Show("Work");

            }
            else
            {
                wait_time.Enabled = true;
                MessageBox.Show("Failed");
            }
        }

        private void MyBrowser_Load(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void wait_time_Tick(object sender, EventArgs e)
        {
            Start();

        }

    }

}
