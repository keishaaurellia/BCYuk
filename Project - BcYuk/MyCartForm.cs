using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Project___BcYuk
{
    public partial class MyCartForm : Form
    {
        //DataClasses1DataContext db = new DataClasses1DataContext();
        // ✅ Tambahkan baris ini
        public static MyCartForm Instance;

        public MyCartForm()
        {
            InitializeComponent();
            Instance = this;
        }


        public static MyCartForm GetInstance()
        {
            if (Instance == null || Instance.IsDisposed)
            {
                Instance = new MyCartForm();
            }
            return Instance;
        }


        private void MyCartForm_Load(object sender, EventArgs e)
        {
            lblUser.Text = Support.userFullName;
            LoadCart(); // 🔥 Panggil hanya jika belum ter-load
            
            //foreach (var product in UserControl1.cartItems)
            //{
            //    decimal price = product.price;
            //    decimal qty = product.quantity;
            //    decimal subtotal = price * qty;

            //    UserControlCart listProduct = new UserControlCart(product)
            //    {
            //        productName = product.name,
            //        productCategory = product.category,
            //        productQty = qty.ToString(),
            //        productlblTotalQtyCheckout = qty.ToString(),
            //        productlblPrice = "Rp " + (product.price.ToString("N0", new CultureInfo("id-ID"))),
            //        productSubtotal = "Rp " + subtotal.ToString("N0", new CultureInfo("id-ID")),
            //    };
             
            //}
           
        }

        public void RefreshCart()
        {
            UserControl1.cartItems.Clear();
            flowLayoutPanel2.Controls.Clear();
            LoadCart();
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {


        }


        public void LoadCart()
        {
            if (Support.userID <= 0)
            {
                MessageBox.Show("Error: Pengguna belum login!", "Login Diperlukan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var cartCollection = MongoDBService.GetCollection("Cart");
            var productCollection = MongoDBService.GetCollection("Product");
            var categoryCollection = MongoDBService.GetCollection("Category");

            var filter = Builders<BsonDocument>.Filter.Eq("UserID", Support.userID);
            var cartItems = cartCollection.Find(filter).ToList();

            flowLayoutPanel2.Controls.Clear();
            UserControl1.cartItems.Clear();

            foreach (var cart in cartItems)
            {
                int productId = cart["ProductID"].AsInt32;
                int qty = cart["Quantity"].AsInt32;

                var productFilter = Builders<BsonDocument>.Filter.Eq("ID", productId);
                var product = productCollection.Find(productFilter).FirstOrDefault();

                if (product == null) continue;

                string categoryName = "";
                if (product.Contains("CategoryID"))
                {
                    int categoryId = product["CategoryID"].AsInt32;
                    var categoryFilter = Builders<BsonDocument>.Filter.Eq("ID", categoryId);
                    var categoryDoc = categoryCollection.Find(categoryFilter).FirstOrDefault();
                    categoryName = categoryDoc?["CategoryName"]?.AsString ?? "";
                }

                var prod = new Product
                {
                    productID = product["ID"].AsInt32,
                    name = product["ProductName"].AsString,
                    category = categoryName,
                    price = product.GetValue("Price", 0).ToDecimal(),
                    stock = product.GetValue("Stock", 0).ToInt32(),
                    image = product.GetValue("ImageURL", "").AsString,
                    quantity = qty
                };

                // ✅ Cek apakah item dengan productID sudah ada
                var existingItem = UserControl1.cartItems.FirstOrDefault(x => x.productID == prod.productID);
                if (existingItem != null)
                {
                    existingItem.quantity += prod.quantity; // tambahkan quantity
                }
                else
                {
                    UserControl1.cartItems.Add(prod);
                }


                var cartItem = new UserControlCart(prod)
                {
                    productName = prod.name,
                    productCategory = prod.category,
                    productQty = qty.ToString(),
                    productlblTotalQtyCheckout = qty.ToString(),
                    productlblPrice = "Rp " + prod.price.ToString("N0", new CultureInfo("id-ID")),
                    productSubtotal = "Rp " + (prod.price * qty).ToString("N0", new CultureInfo("id-ID"))
                };

                cartItem.SetCheckoutMode(false);
                cartItem.QuantityChanged += () => UpdateTotalPrice();

                flowLayoutPanel2.Controls.Add(cartItem);
            }

            UpdateTotalPrice();
        }


        //public void LoadCart()
        //{

        //    Support.dbContext.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, Support.dbContext.Carts);
        //    Support.dbContext.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, Support.dbContext.Products);

        //    flowLayoutPanel2.Controls.Clear();
        //    UserControl1.cartItems.Clear();

        //    if (Support.userID <= 0)
        //    {
        //        MessageBox.Show("Error: User is not logged in!", "Login Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    var db = Support.dbContext;
        //    {
        //        var cartItems = (
        //            from cart in db.Carts
        //            join product in db.Products on cart.ProductID equals product.ID
        //            where cart.UserID == Support.userID
        //            select new
        //            {
        //                cart.ProductID,
        //                product.ProductName,
        //                CategoryName = db.Categories
        //                                .Where(cat => cat.ID == product.CategoryID)
        //                                .Select(cat => cat.CategoryName)
        //                                .FirstOrDefault(),
        //                product.Price,
        //                product.Stock,
        //                product.ImageURL,
        //                cart.Quantity
        //            }).ToList();

        //        if (!cartItems.Any())
        //        {
        //            Debug.WriteLine("⚠️ No items found in cart!");
        //            return;
        //        }

        //        foreach (var item in cartItems)
        //        {
        //            var product = new Product
        //            {
        //                productID = item.ProductID,
        //                name = item.ProductName,
        //                category = item.CategoryName,
        //                price = (item.Price ?? 0),
        //                stock = item.Stock,
        //                image = item.ImageURL,
        //                quantity = item.Quantity
        //            };

        //            UserControl1.cartItems.Add(product);

        //            var cartItem = new UserControlCart(product)
        //            {
        //                productName = item.ProductName,
        //                productCategory = item.CategoryName,
        //                productQty = item.Quantity.ToString(),
        //                productlblTotalQtyCheckout = item.Quantity.ToString(),
        //                productlblPrice = "Rp " + (product.price.ToString("N0", new CultureInfo("id-ID"))),
        //                productSubtotal = "Rp " + ((item.Price ?? 0) * item.Quantity).ToString("N0", new CultureInfo("id-ID")),

        //            };

        //            // 🔥 Tambahin baris ini buat atur tampilannya biar sesuai mode keranjang
        //            cartItem.SetCheckoutMode(false);

        //            // 🔥 Daftarkan listener ke event QuantityChanged
        //            cartItem.QuantityChanged += () => UpdateTotalPrice();

        //            flowLayoutPanel2.Controls.Add(cartItem);
        //        }

        //    }

        //    UpdateTotalPrice(); // Hitung ulang total setelah selesai load
        //}







        public void UpdateTotalPrice()
        {
            decimal totalPrice = UserControl1.cartItems.Sum(item => item.price * item.quantity);
            cartlblTotalPrice.Text = "Rp " + totalPrice.ToString("N0", new CultureInfo("id-ID"));
        }

       



        private void guna2Button3_Click(object sender, EventArgs e)
        {
            MyCartForm.GetInstance().Show();
            MyCartForm.GetInstance().RefreshCart();
            this.Close();

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

            if (UserControl1.cartItems.Count == 0)
            {
                MessageBox.Show("Keranjang kosong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CheckoutForm.GetInstance(this).Show();
            this.Hide(); // ✅ Lebih aman kalau kamu mau satu instance aja
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            CustomerMainForm.GetInstance().Show();
            this.Close(); // atau this.Hide(); 
        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        // Tambahkan di MyCartForm.cs
        public void ClearCartDisplay()
        {
            flowLayoutPanel2.Controls.Clear();
            cartlblTotalPrice.Text = "Rp 0";
        }

        public void FullClearCart()
        {
            UserControl1.cartItems.Clear();
            UserControl1.cartProductIds.Clear();
            flowLayoutPanel2.Controls.Clear();
            cartlblTotalPrice.Text = "Rp 0";
        }






        private void buttonClear_Click(object sender, EventArgs e)
        {
           

        }
        

        private void cartlblTotalPrice_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            HistoryTransactionForm.GetInstance().Show();
            this.Close();// atau this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            CustomerMainForm.GetInstance().Show();
            this.Close(); // atau this.Hide();
        }

        private void guna2Button4_Click_1(object sender, EventArgs e)
        {
            AccountForm.GetInstance().Show();
            this.Close(); // atau this.Hide();

        }

        private void guna2PictureBox1_Click_1(object sender, EventArgs e)
        {
            CustomerMainForm.GetInstance().Show();
            this.Close(); // atau this.Hide();
        }

        private void buttonClear_Click_1(object sender, EventArgs e)
        {
            // ✅ Cek dulu sebelum tampilkan MessageBox konfirmasi
            if (UserControl1.cartItems.Count == 0)
            {
                MessageBox.Show("Keranjang sudah kosong!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
                "Apakah kamu yakin ingin menghapus semua produk dari keranjang?",
                "Konfirmasi Hapus Semua",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm == DialogResult.Yes)
            {
                //using (var db = new DataClasses1DataContext())
                //{
                //    var cartToDelete = db.Carts.Where(c => c.UserID == Support.userID).ToList();

                //    if (cartToDelete.Any())
                //    {
                //        db.Carts.DeleteAllOnSubmit(cartToDelete);
                //        db.SubmitChanges();
                //    }
                //}

                //UserControl1.cartItems.Clear();
                //UserControl1.cartProductIds.Clear();
                //flowLayoutPanel2.Controls.Clear();

                //cartlblTotalPrice.Text = "Rp 0";

                var cartCollection = MongoDBService.GetCollection("Cart");
                var filter = Builders<BsonDocument>.Filter.Eq("UserID", Support.userID);
                cartCollection.DeleteMany(filter);

                FullClearCart();

                MessageBox.Show("Seluruh produk telah dihapus dari keranjang!", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
    }

