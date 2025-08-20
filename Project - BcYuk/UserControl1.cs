using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Project___BcYuk
{
    public partial class UserControl1 : UserControl
    {

        public static List<Product> cartItems = new List<Product>();
        //private Product product;
        public static List<int> cartProductIds = new List<int>();
        private BsonDocument product;
        //DataClasses1DataContext db = new DataClasses1DataContext();


        public UserControl1()
        {
            InitializeComponent();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {


            lblName.AutoSize = false; // Matikan auto-size agar lebarnya tetap
            lblName.AutoEllipsis = true; // Aktifkan titik-titik jika teks kelebihan
            lblName.Size = new Size(250, 30); // Atur ukuran tetap (lebar & tinggi)
            lblName.TextAlign = ContentAlignment.MiddleLeft; // Sejajarkan ke kiri

        }


        public UserControl1(BsonDocument product)
        {
            InitializeComponent();
            this.product = product;

            productName = product["ProductName"].AsString;
            productPrice = "Rp" + product.GetValue("Price", 0).ToDecimal().ToString("N0", new CultureInfo("id-ID"));
            productCategory = GetCategoryName(product["CategoryID"].AsInt32);
            productImage = product.GetValue("ImageURL", "").AsString;
            

            int stock = product.GetValue("Stock", 0).ToInt32();

            // 🔥 Kalau stoknya 0, disable tombol + kasih info
            if (stock <= 0)
            {
                btnAdd.Enabled = false;
                btnAdd.Text = "Stok Habis";
                btnAdd.FillColor = Color.DarkGray; // Kasih warna abu-abu
            }

            UpdateButtonState();
        }

        public string productImage
        {
            get => pictureProduct.ImageLocation ?? string.Empty;

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (value.StartsWith("http")) // Gambar dari URL (internet)
                    {
                        pictureProduct.LoadAsync(value); // ✅ Load dari URL langsung
                    }
                    else // Fallback kalau ada yang masih lokal
                    {
                        string imageFolderPath = @"C:\Users\muhra\Downloads\Poto produk BC yuk\";
                        string imagePath = Path.Combine(imageFolderPath, value);

                        if (File.Exists(imagePath))
                        {
                            try
                            {
                                using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                                {
                                    pictureProduct.Image = Image.FromStream(fs);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error loading image: " + ex.Message);
                            }
                        }
                        else
                        {
                            MessageBox.Show("File tidak ditemukan: " + imagePath);
                        }
                    }
                }
            }
        }



        public string productName
        {
            get => lblName.Text;
            set => lblName.Text = value;
        }

        public string productPrice
        {
            get => lblPrice.Text;
            set => lblPrice.Text = value;
        }

        public string productCategory
        {
            get => lblCategory.Text;
            set => lblCategory.Text = value;
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }




        public void UpdateButtonState()
        {
            int stock = product.GetValue("Stock", 0).ToInt32();
            int productId = product["ID"].AsInt32;

            if (stock <= 0)
            {
                btnAdd.Enabled = false;
                btnAdd.Text = "Stok Habis";
                btnAdd.FillColor = Color.DarkGray;
                return;
            }
            // Cari produk di cartItems
            var item = cartItems.FirstOrDefault(p => p.productID == productId);

            //if (UserControl1.cartProductIds.Contains(product.ID))
            if (item != null)
            {
                btnAdd.Text = $"Qty: {item.quantity}";
                btnAdd.Enabled = true; // Masih bisa diklik kalau mau buka AddCartForm untuk edit qty
            }
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {

            if (product == null)
            {
                MessageBox.Show("❌ Product not found in database!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            Product selectedProduct = new Product
            {
                productID = product["ID"].AsInt32,
                name = product["ProductName"].AsString,
                price = product.GetValue("Price", 0).ToDecimal(),
                category = productCategory,
                stock = product["Stock", 0].AsInt32,
                image = product.GetValue("ImageURL", "").AsString
            };

            AddCartFormcs modalAddCart = new AddCartFormcs(selectedProduct);

            // Tambahkan event handler supaya MyCartForm langsung refresh
            modalAddCart.CartUpdated += () =>
            {
                var cartForm = Application.OpenForms.OfType<MyCartForm>().FirstOrDefault();
                if (cartForm != null)
                {
                    cartForm.LoadCart();         // 🔁 Reload isi keranjang
                    cartForm.UpdateTotalPrice(); // 🔄 Update harga total
                }
                this.UpdateButtonState(); // Di dalam UserControl1

            };

            modalAddCart.ShowDialog();
        }


        private string GetCategoryName(int categoryId)
        {
            var category = MongoDBService.GetByFilter("Category",
                Builders<BsonDocument>.Filter.Eq("ID", categoryId)).FirstOrDefault();

            return category != null ? category["CategoryName"].AsString : "Unknown";
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblCategory_Click(object sender, EventArgs e)
        {

        }

        private void customePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureProduct_Click(object sender, EventArgs e)
        {
           
        }

        private void lblName_Click(object sender, EventArgs e)
        {
            
        }
    }
}