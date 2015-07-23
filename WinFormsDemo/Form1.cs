using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bulutfon.OAuth.Win;
using Bulutfon.Sdk;

namespace WinFormsDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var loggedIn = LoginForm.Login(
                "d68a8d69c16b6ac209980dc5ec7b381933d91c71ca37d83e8e5c64b0ae2f3f9e", 
                "6b9f79ac744ce39a61b1ba236782b7de4d54a96f9f6c43077449cd86c9e9f799", 
                this);
            if (loggedIn) {
                button1.Enabled = false;
                //MessageBox.Show(string.Format("Başarılı!\r\ntoken = '{0}'", Authentication.Token.AccessToken));
                dataGridView1.DataSource = BulutfonApi.GetDids(Authentication.Token);
            }
        }
    }
}
