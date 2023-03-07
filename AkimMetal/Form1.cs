using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AkimMetal
{
    public partial class Form1 : Form
    {
        List<Item> items;
        List<UserControl1> users = new List<UserControl1>();
        List<StringBuilder> sbList = new List<StringBuilder>();
        int i = 0;
        public Form1()
        {
            InitializeComponent();

            using (StreamReader sr = new StreamReader("MOCK_DATA.json")) 
            {
                string jsonData = sr.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<Item>>(jsonData);
            }

            for (; i < 3; i++)
            {
                UserControl1 user = new UserControl1();
                user.txtID.Text = items[i].id.ToString();
                user.txtFName.Text = items[i].first_name;
                user.txtLName.Text = items[i].last_name;
                user.txtMail.Text = items[i].email;
                user.txtGender.Text = items[i].gender;
                user.txtIP.Text = items[i].ip_address;
                user.pictureBox1.ImageLocation =@"..\"+items[i].image;

                StringBuilder sb = new StringBuilder();
                sb.Append("https://www.google.com/maps/@");
                sb.Append(items[i].latitude.ToString());
                sb.Append(",");
                sb.Append(items[i].longitude.ToString());
                sb.Append(",");
                sb.Append("15z");

                sbList.Add(sb);

                user.webBrowser.Navigate(sbList[0].ToString());

                users.Add(user);

                flowLayoutPanel1.Controls.Add(users[i]);
            }

            i = 3;
        }


        private void flowLayoutPanel1_Scroll(object sender, ScrollEventArgs e)
        {

            UserControl1 user = new UserControl1();
            user.txtID.Text = items[i].id.ToString();
            user.txtFName.Text = items[i].first_name;
            user.txtLName.Text = items[i].last_name;
            user.txtMail.Text = items[i].email;
            user.txtGender.Text = items[i].gender;
            user.txtIP.Text = items[i].ip_address;
            user.pictureBox1.Load(items[i].image);

            StringBuilder sb = new StringBuilder();
            sb.Append("https://www.google.com/maps/@");
            sb.Append(items[i].latitude.ToString());
            sb.Append(",");
            sb.Append(items[i].longitude.ToString());
            sb.Append(",");
            sb.Append("15z");

            sbList.Add(sb);

            user.webBrowser.Navigate(sbList[0].ToString());

            users.Add(user);

            flowLayoutPanel1.Controls.Add(users[i]);

            i++;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var searchedItems = users.Where(x => x.txtFName.Text.Contains(txtSearch.Text)).ToList();
            flowLayoutPanel1.Controls.Clear();
            users.Clear();

            for(int j= 0; j < searchedItems.Count; j++)
            {
                flowLayoutPanel1.Controls.Add(searchedItems[j]);
            }
            
        }
    }
}
