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
using Bulutfon.OAuth;

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
            // With OAUTH 2
            string client_id = "CLIENT_ID";
            string client_secret = "CLIENT_SECRET";
            var loggedIn = LoginForm.Login(
                client_id,
                client_secret,
                this);
            if (loggedIn)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                dataGridView1.DataSource = BulutfonApi.GetDids(Authentication.Token);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // With Master Token
            button1.Enabled = false;
            button2.Enabled = false;
            Token token = new Token("MASTER TOKEN", "");
            dataGridView1.DataSource = BulutfonApi.GetDids(token);
        }
    }
}
