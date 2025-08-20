using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
namespace Project___BcYuk
{
    public partial class CustomerMainForm : Form
    {
        // Menyimpan daftar ID produk yang ditambahkan ke keranjang belanja
        public static List<int> cartProductIds = new List<int>();


        public static CustomerMainForm Instance;

        public static CustomerMainForm GetInstance()
        {
            if (Instance == null || Instance.IsDisposed)
                Instance = new CustomerMainForm();
            return Instance;
        }


        public CustomerMainForm()
        {
            InitializeComponent();
        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CustomerMainForm_Load(object sender, EventArgs e)
        {

            lblUsername.Text = Support.userFullName;

            // ⬇️ Load data cart duluan ke memory
            LoadCartItems();

            // ⬇️ Baru load tampilan produk
            LoadAllProducts();

            // ⬇️ Load kategori terakhir
            var categories = MongoDBService.GetAll("Category")
                .Select(x => x["CategoryName"].AsString)
                .Distinct()
                .ToList();
            categories.Insert(0, "Semua");
            cmbCategory.DataSource = categories;

        }

        //Menampilkan semua produk yang ada dari database ke flowLayoutPanel1.
        private void LoadAllProducts()
        {
            // Bersihkan isi panel terlebih dahulu
            flowLayoutPanel1.Controls.Clear();

            var products = MongoDBService.GetAll("Product")
                .GroupBy(p => p["ID"].AsInt32) // Hindari duplikat
                .Select(g => g.First())
                .ToList();

            foreach (var product in products)
            {
                UserControl1 listProduct = new UserControl1(product);  // Buat kontrol untuk tiap produk
                flowLayoutPanel1.Controls.Add(listProduct);
            }

        }

        public void LoadProductData()
        {

            flowLayoutPanel1.Controls.Clear();

            var products = MongoDBService.GetAll("Product");

            foreach (var product in products)
            {
                UserControl1 productControl = new UserControl1(product);
                flowLayoutPanel1.Controls.Add(productControl);
            }
        }

        public void RefreshProductButtons()
        {
          

            foreach (Control control in flowLayoutPanel1.Controls)
            {
                if (control is UserControl1 userControl)
                {
                    userControl.UpdateButtonState();
                }
            }

        }

        private void LoadCartItems()
        {
            var cartCollection = MongoDBService.GetCollection("Cart");
            var filter = Builders<BsonDocument>.Filter.Eq("UserID", Support.userID);
            var cartItems = cartCollection.Find(filter).ToList();

            UserControl1.cartItems.Clear();
            UserControl1.cartProductIds.Clear();

            foreach (var cart in cartItems)
            {
                UserControl1.cartProductIds.Add(cart["ProductID"].AsInt32);

                var product = new Product
                {
                    productID = cart["ProductID"].AsInt32,
                    quantity = cart["Quantity"].AsInt32
                };

                UserControl1.cartItems.Add(product);
            }
        }


        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void cartForm_Click(object sender, EventArgs e)
        {
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            MyCartForm.GetInstance().Show();
            MyCartForm.GetInstance().RefreshCart();
            this.Close();

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            HistoryTransactionForm.GetInstance().Show();
            this.Close(); // atau this.Hide();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
           
            var filter = Builders<BsonDocument>.Filter.Regex("ProductName", new BsonRegularExpression(txtSearch.Text, "i"));
            var query = MongoDBService.GetByFilter("Product", filter)
                .GroupBy(p => p["ID"].AsInt32)
                .Select(g => g.First())
                .ToList();


            flowLayoutPanel1.Controls.Clear();

            foreach (var product in query)
            {
              
                UserControl1 listProduct = new UserControl1(product);


                flowLayoutPanel1.Controls.Add(listProduct);
            }
        }




        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

            string selectedCategory = cmbCategory.SelectedItem.ToString();

            if (selectedCategory == "Semua")
            {
                LoadAllProducts();
                return;
            }

            var categoryDoc = MongoDBService.GetByFilter("Category",
                Builders<BsonDocument>.Filter.Eq("CategoryName", selectedCategory)).FirstOrDefault();

              if (categoryDoc != null)
            {
                int categoryId = categoryDoc["ID"].AsInt32;

                var filteredProducts = MongoDBService.GetByFilter("Product",
                    Builders<BsonDocument>.Filter.Eq("CategoryID", categoryId))
                    .GroupBy(p => p["ID"].AsInt32)
                    .Select(g => g.First())
                    .ToList();

                flowLayoutPanel1.Controls.Clear();

                foreach (var product in filteredProducts)
                {
                    UserControl1 listProduct = new UserControl1(product);
                    flowLayoutPanel1.Controls.Add(listProduct);
                }
            }
        }


        private void lblUsername_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button4_Click_1(object sender, EventArgs e)
        {
            AccountForm.GetInstance().Show();
            this.Close(); // atau this.Hide();

        }
    }
}
