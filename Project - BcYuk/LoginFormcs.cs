using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Project___BcYuk
{
    public partial class LoginFormcs : Form
    {
        //DataClasses1DataContext db = new DataClasses1DataContext();

        public LoginFormcs()
        {
            InitializeComponent();
        }
        private void btnLogin_Click_4(object sender, EventArgs e)
        {
            // Membuat filter untuk mencocokkan username dan password
            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("FullName", txtUsername.Text),
                Builders<BsonDocument>.Filter.Eq("Password", txtPassword.Text)
            );

            // Mengambil data pengguna dari database MongoDB berdasarkan filter
            var userDoc = MongoDBService.GetByFilter("UserAuth", filter).FirstOrDefault();

            /// Jika data ditemukan (login berhasil)
            if (userDoc != null)
            {
                // Menyimpan informasi pengguna ke dalam variabel global
                Support.userID = userDoc["ID"].AsInt32;
                Support.userFullName = userDoc["FullName"].AsString;
                Support.userPassword = userDoc["Password"].AsString;

                // Menampilkan form utama untuk pengguna yang berhasil login
                CustomerMainForm customerMainForm = new CustomerMainForm();
                customerMainForm.Show();
                this.Hide();
            }
            else
            {
                // Jika data tidak ditemukan (login gagal)
                MessageBox.Show("Data kamu tidak valid");
            }
        }

        private void linkRegister_LinkClicked_3(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterForm.GetInstance().Show();
            this.Close(); // atau this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
           
           
        }

        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
           
        }

        private void btnLogin_Click_2(object sender, EventArgs e)
        {
           
        }

        private void linkRegister_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void btnLogin_Click_3(object sender, EventArgs e)
        {
           

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkRegister_LinkClicked_2(object sender, LinkLabelLinkClickedEventArgs e)
        {

            
        }

    }
}
