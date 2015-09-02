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
            string client_id = "CLIENT_ID";
            string client_secret = "CLIENT_SECRET";
            var loggedIn = LoginForm.Login(
                client_id, 
                client_secret, 
                this);
            if (loggedIn)
            {
                button1.Enabled = false;
                dataGridView1.DataSource = BulutfonApi.GetDids(Authentication.Token);
            }
        }
    }
}
