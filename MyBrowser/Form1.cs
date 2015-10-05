using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyBrowser
{
    public partial class MyBrowser : Form
    {
        int now = 0;
        int count = 0;
        string nowhttp="";
        string txt = "myweb.txt";
        System.IO.StreamReader file =new System.IO.StreamReader("myweb.txt");
        public MyBrowser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.ScriptErrorsSuppressed = true;
            
            timer1.Enabled = true;            
        }
        private int NumberBrowser()
        {
            return 10;
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
                    label1.Text = "次數 " + count; //顯示出更新了多少次
                    webBrowser1.Navigate(nowhttp);
                }
                else
                {
                    timer1.Enabled = false;
                    file.Close();
                    file = new System.IO.StreamReader(txt);
                    timer1.Enabled = true;
                }
            }
         
            
            
        }
    }
}
