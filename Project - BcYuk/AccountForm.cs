using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Project___BcYuk
{
    public partial class AccountForm : Form
    {
        private IMongoCollection<BsonDocument> productCollection;


        public static AccountForm Instance;

        public static AccountForm GetInstance()
        {
            if (Instance == null || Instance.IsDisposed)
            {
                Instance = new AccountForm();
            }
            return Instance;
        }

        public AccountForm()
        {
            InitializeComponent();
            Instance = this;
            var db = MongoDBConnection.GetDatabase("BcYuk"); // ganti sama nama DB kamu
            productCollection = db.GetCollection<BsonDocument>("Produk"); // ganti nama koleksi
        }

        private void tableLayoutPanel23_Paint(object sender, PaintEventArgs e)
        {

        }
        private void AccountForm_Load(object sender, EventArgs e)
        {
            lblUsername.Text = Support.userFullName;

            var userCollection = MongoDBService.GetCollection("UserAuth");
            var filter = Builders<BsonDocument>.Filter.Eq("ID", Support.userID);
            var user = userCollection.Find(filter).FirstOrDefault();

            if (user != null)
            {
                lblNamaUser.Text = user["FullName"].AsString;
                lblNomorTelepon.Text = user["PhoneNumber"].AsString;

                // Ambil Class Name
                var classCollection = MongoDBService.GetCollection("Class");
                var classFilter = Builders<BsonDocument>.Filter.Eq("ID", user["ClassId"].AsInt32);
                var classDoc = classCollection.Find(classFilter).FirstOrDefault();

                lblJurusanUser.Text = classDoc != null ? classDoc["ClassName"].AsString : "Tidak ditemukan";
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
           
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
          ;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            HistoryTransactionForm.GetInstance().Show();
            this.Close(); // atau this.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            MyCartForm.GetInstance().Show();
            this.Close();
        }

        private void btnEditBiodata_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterForm.GetInstance(Support.userID).ShowDialog();
            this.Show(); // Balik lagi setelah close
            this.AccountForm_Load(null, null); // Refresh

        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
