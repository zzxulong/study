using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformTest
{
    public partial class Form1 : Form
    {
        public class dataView
        {
            public string name { get; set; }
        }

        public BindingList<dataView> data1=new BindingList<dataView>();
        public Form1()
        {
            data1.Add(new dataView() { name = "1123123" });
            data1.Add(new dataView() { name = "adsfasdf" });
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            var cookies=new System.Net.CookieContainer(){ Capacity=100};
            cookies.Add(new System.Net.Cookie("asdf", "adsfsdf","/","baidu.com"));



            this.contextMenuStrip1.Show(label1, this.label1.Location);
        }

		private void Form1_Load(object sender,EventArgs e)
		{
            this.dataGridView1.DataSource = data1;

		}

        private void Button1_Click (object sender, EventArgs e)
        {
            //var item=this.dataGridView1.Rows[1].SetValues("3333");
            this.data1.FirstOrDefault().name = "33333";
            this.data1.ResetItem(0);
        }
    }
}
