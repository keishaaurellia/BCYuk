using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Project___BcYuk
{
    public partial class AddCartFormcs : Form
    {
        //DataClasses1DataContext db = new DataClasses1DataContext();
        public event Action CartUpdated;
        public Product SelectedProduct { get; set; }

        public AddCartFormcs(Product product)
        {


            InitializeComponent();
            SelectedProduct = product;

            lblProductName.Text = product.name;
            lblStock.Text = product.stock.ToString();
            lblProductPrice.Text = "Rp " + product.price.ToString("N0", new CultureInfo("id-ID"));

            lblStock.Enabled = false;

            // Panggil LoadProductImage di sini
            LoadProductImage(product.image);

            // 🔥 Tambahan di sini
            var existing = UserControl1.cartItems.FirstOrDefault(p => p.productID == SelectedProduct.productID);
            if (existing != null)
            {
                numQty.Value = existing.quantity; // Set nilai awal sesuai isi keranjang
            }
            else
            {
                numQty.Value = 1; // Default kalau belum ada
            }
        }

        private void LoadProductImage(string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                if (imagePath.StartsWith("http")) // 🔗 Kalau URL
                {
                    try
                    {
                        picturebxProduct.LoadAsync(imagePath); // ✅ Langsung dari internet
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("❌ Gagal load gambar dari URL: " + ex.Message);
                    }
                }
                else // 📂 Kalau file lokal
                {
                    string imageFolderPath = @"C:\Users\muhra\Downloads\Poto produk BC yuk\";
                    string fullPath = Path.Combine(imageFolderPath, imagePath);

                    if (File.Exists(fullPath))
                    {
                        try
                        {
                            picturebxProduct.Image = Image.FromFile(fullPath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("❌ Gagal load gambar lokal: " + ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("❌ Gambar lokal tidak ditemukan: " + fullPath);
                    }
                }
            }
        }


        private void AddCartFormcs_Load(object sender, EventArgs e)
        {
            
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
      

        private void btnAdd_Click_2(object sender, EventArgs e)
        {

        }

        private void customePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnAddtoMyCart_Click_2(object sender, EventArgs e)
        {

            int qty = (int)numQty.Value;

            if (qty <= 0)
            {
                MessageBox.Show("Kuantitas minimal 1!");
                return;
            }

            if (qty > SelectedProduct.stock)
            {
                MessageBox.Show("Kuantitas tidak bisa lebih tinggi dari stock");
                return;
            }

            if (Support.userID <= 0)
            {
                MessageBox.Show("Error: User belum login!");
                return;
            }

            // ✅ Normalisasi note dulu (meskipun kamu gak pake textbox note sekarang)
            SelectedProduct.note = "-";

            // Cek di memory (list cart sementara)
            var existing = UserControl1.cartItems.FirstOrDefault(p => p.productID == SelectedProduct.productID);

            if (existing != null)
            {
                existing.quantity += qty;

                // ✅ Update langsung quantity-nya ke nilai baru
                var filter = Builders<BsonDocument>.Filter.And(
                    Builders<BsonDocument>.Filter.Eq("UserID", Support.userID),
                    Builders<BsonDocument>.Filter.Eq("ProductID", SelectedProduct.productID)
                );

                var update = Builders<BsonDocument>.Update.Set("Quantity", qty);
                MongoDBService.Update("Cart", filter, update);

                // 🔁 Sinkronkan ke memory (list cartItems di UserControl1)
                existing.quantity = qty;

            }
            else
            {
                SelectedProduct.quantity = qty;

                // ✅ Tambah ke memory list
                UserControl1.cartItems.Add(SelectedProduct);
                UserControl1.cartProductIds.Add(SelectedProduct.productID);

                // ✅ Tambah ke MongoDB
                var newCartDoc = new BsonDocument
        {
            { "UserID", Support.userID },
            { "ProductID", SelectedProduct.productID },
            { "Quantity", qty },
            { "Note", SelectedProduct.note }
        };

                MongoDBService.Insert("Cart", newCartDoc);
            }

            CartUpdated?.Invoke(); // Trigger refresh UI
            MessageBox.Show("✅ Produk berhasil ditambahkan ke keranjang!", "Berhasil");
            this.Hide();
        
        }

        private void lblStock_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
