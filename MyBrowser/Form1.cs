using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace MyBrowser
{
    public partial class MyBrowser : Form
    {
        int now = 0;
        int count = 0;
        string nowhttp="";
        string txt = "mylist.txt";
        string downloadHttp = "https://docs.google.com/document/export?format=txt&id=1UJ_-1lWb_KrJvXKvoqRAgJwEVFuInxFqVA5HJ-5yqAw&token=AC4w5Vh6Br1q3Os8qb59jo0kDufFGfwd7A%3A1444204710231";
        System.IO.StreamReader file;

        public MyBrowser()
        {
            InitializeComponent();
            webBrowser1.ScriptErrorsSuppressed = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
                    file = new System.IO.StreamReader(txt);
                    //timer1.Enabled = true;
                    label1.Text = "Done";
                }
            }
        }

        private void getDowload_Click(object sender, EventArgs e)
        {
            WebClient wc = new WebClient();
            wc.DownloadFile(downloadHttp,txt);
            Start();
        }
        private void Start()
        {
            file = new System.IO.StreamReader("mylist.txt");
            timer1.Enabled = true;  
        }
    }
}
