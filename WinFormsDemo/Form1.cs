using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bulutfon.OAuth.Win;
using Bulutfon.Sdk.Models;
using Bulutfon.Sdk.Models.ResponseObjects;
using Bulutfon.Sdk.Models.Post;
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
                //button1.Enabled = false;
                //dataGridView1.DataSource = BulutfonApi.GetDids(Authentication.Token);
                AutomaticCallCreator auto = new AutomaticCallCreator();
                auto.title = "DotNet Deneme2";
                auto.did = 908508850249;
                auto.receivers = "905322041584";
                auto.announcement_id = 1353;
                ResponseAutomaticCall x =  BulutfonApi.CreateAutomaticCall(Authentication.Token, auto);
                String mes = x.message;
                int b = 5;

                String ada = "asd";
            }
        }
    }
}
