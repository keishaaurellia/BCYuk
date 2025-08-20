using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Project___BcYuk
{
    public partial class CheckoutForm : Form
    {
        MyCartForm cartForm;
        public static CheckoutForm Instance;


        public static CheckoutForm GetInstance(MyCartForm cartForm)
        {
            if (Instance == null || Instance.IsDisposed || Instance.Visible == false)
            {
                Instance = new CheckoutForm(cartForm);
            }
            return Instance;
        }




        public CheckoutForm(MyCartForm cartForm)
        {
            InitializeComponent();
            Instance = this;
            this.cartForm = cartForm;
        }

       
        private void CheckoutForm_Load(object sender, EventArgs e)
        {
            LoadPickupSchedule();
            LoadCartForCheckout();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void UpdateTotalPrice()
        {
            decimal totalPrice = UserControl1.cartItems.Sum(item => item.price * item.quantity);
            checkoutlblTotalPrice.Text = "Rp " + totalPrice.ToString("N0", new CultureInfo("id-ID"));
        }

        public void UpdateSubtotalPrice()
        {
            decimal totalPrice = UserControl1.cartItems.Sum(item => item.price * item.quantity);
            checkoutlblTotalPrice.Text = "Rp " + totalPrice.ToString("N0", new CultureInfo("id-ID"));
        }


        private void LoadPickupSchedule()
        {
            var today = DateTime.UtcNow.Date; // ✅ gunakan UTC
            var nowMinutes = (int)DateTime.UtcNow.TimeOfDay.TotalMinutes; // ✅ UTC juga

            var filterBuilder = Builders<BsonDocument>.Filter;
            var filter = filterBuilder.Eq("PickupDate", today) &
                         filterBuilder.Gte("PickupEndTimeMinutes", nowMinutes); // ✅ waktu dalam menit

            var sort = Builders<BsonDocument>.Sort.Ascending("PickupTimeMinutes");

            var result = MongoDBService.GetByFilter("PickupSchedule", filter, sort);

            var jadwal = result
                .Select(p => new
                {
                    ID = p.GetValue("ID", BsonNull.Value).AsInt32,
                    Display = DateTime.Parse(p["PickupDate"].ToString()).ToString("dddd (dd/MM/yy)") +
                              ", " + p.GetValue("Description", "").AsString
                })
                .ToList();

            if (jadwal.Count == 0)
            {
                cmbJadwalPickup.Items.Clear();
                cmbJadwalPickup.Text = "Jadwal tidak tersedia";
            }
            else
            {
                cmbJadwalPickup.DisplayMember = "Display";
                cmbJadwalPickup.ValueMember = "ID";
                cmbJadwalPickup.DataSource = jadwal;
            }
        }

        private void LoadCartForCheckout()
        {
            flowLayoutPanel2.Controls.Clear(); // flowLayoutPanel1 = panel di form Checkout

            foreach (var item in UserControl1.cartItems)
            {
                var cartItem = new UserControlCart(item)
                {
                    productName = item.name,
                    productCategory = item.category,
                    productQty = item.quantity.ToString(),
                    productlblTotalQtyCheckout = item.quantity.ToString(),
                    productlblPrice = "Rp " + item.price.ToString("N0", new CultureInfo("id-ID")),
                    productSubtotal = "Rp " + (item.price * item.quantity).ToString("N0", new CultureInfo("id-ID"))
                };

                cartItem.SetCheckoutMode(true); // 🔥 Tampilkan versi checkout (tanpa delete & qty)
                                                // 🔥 Daftarkan listener ke event QuantityChanged
                cartItem.QuantityChanged += () => UpdateTotalPrice();
                flowLayoutPanel2.Controls.Add(cartItem);
            }
            UpdateTotalPrice();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            MyCartForm.GetInstance().Show();
            MyCartForm.GetInstance().RefreshCart();
            this.Close();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnCheckout_Click_1(object sender, EventArgs e)
        {

            //MessageBox.Show("User ID saat checkout: " + Support.userID);

            if (UserControl1.cartItems.Count == 0)
            {
                MessageBox.Show("Keranjang kosong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ CEK STOK DI SINI
            foreach (var item in UserControl1.cartItems)
            {
                var productDoc = MongoDBService.GetByFilter("Product",
                    Builders<BsonDocument>.Filter.Eq("ID", item.productID)).FirstOrDefault();

                if (productDoc == null)
                {
                    MessageBox.Show($"❌ Produk {item.name} tidak ditemukan!");
                    return;
                }

                int currentStock = productDoc.GetValue("Stock", 0).ToInt32();

                if (item.quantity > currentStock)
                {
                    MessageBox.Show($"❌ Stok produk '{item.name}' hanya tersedia {currentStock}. " +
                        $"Silakan kurangi jumlah di keranjang.");
                    return;
                }
            }

            // 1. Buat dokumen order
            var newOrder = new BsonDocument
                    {
                        { "CustomerID", Support.userID },
                        { "OrderDate", DateTime.Now },
                        { "TotalPrice", UserControl1.cartItems.Sum(x => x.price * x.quantity) },
                        { "PickupScheduleId", (int)cmbJadwalPickup.SelectedValue },
                       
                    };

            MongoDBService.Insert("Order", newOrder);

            // Ambil ID order yang baru dimasukkan
            var lastOrder = MongoDBService
                .GetAll("Order")
                .OrderByDescending(x => x["OrderDate"].ToUniversalTime())
                .FirstOrDefault();

            if (lastOrder == null)
            {
                MessageBox.Show("Gagal menyimpan pesanan!");
                return;
            }

            BsonValue orderId;
            if (lastOrder.Contains("ID"))
                orderId = lastOrder["ID"];
            else
                orderId = lastOrder["_id"];


            // 2. Simpan Order Detail
            var orderDetails = new List<BsonDocument>();
            // ✅ Group cart items
            var groupedCartItems = UserControl1.cartItems
             .GroupBy(p => new { p.productID, note = (string.IsNullOrWhiteSpace(p.note) ? "-" : p.note.Trim()) })

             .Select(g =>
             {
                 var first = g.First();
                 var totalQty = g.Sum(x => x.quantity);
                 var totalPrice = g.Sum(x => x.price * x.quantity);

                 return new BsonDocument
        {
                    { "OrderID", orderId },
                    { "ProductID", g.Key.productID },
                    { "Quantity", g.Sum(x => x.quantity) },
                    { "SubTotal", g.Sum(x => x.price * x.quantity) },
                    { "Note", g.Key.note }
        };

             }).ToList();


            // ✅ Insert grouped details
            MongoDBService.InsertMany("OrderDetail", groupedCartItems);


            // 4. Insert Payment
            var newPayment = new BsonDocument
            {
                { "OrderID", orderId },
                { "PaymentMethodID", 2 },
                { "PaymentDate", DateTime.Now },
                
            };
            MongoDBService.Insert("Payment", newPayment);


            // 5. Tampilkan QRIS
            QRISForm qrisForm = new QRISForm();
            qrisForm.ShowDialog();


            if (qrisForm.UserConfirmed)
            {
                // 6. Update status payment & insert receipt
                var receipt = new BsonDocument
                        {
                            { "OrderID", orderId },
                            { "TotalAmount", lastOrder["TotalPrice"] },
                            { "ReceiptDate", DateTime.Now },
                            { "PaymentMethodId", 2 },
                            { "StatusId", 1}
                       };
                MongoDBService.Insert("Receipt", receipt);
                // ✅ Kurangi stok di sini, karena status 5 boleh

                UpdateStockByStatusIfAllowed(orderId, 1);


                // 8. Clear keranjang
                var productDTOs = GetGroupedCartItems(); // 📦 Ambil dulu isi keranjang yang sudah digroup

                // 🔽 Tambahkan ini sebelum clear keranjang
                foreach (var item in productDTOs)
                {
                    var productDoc = MongoDBService.GetByFilter("Product",
                        Builders<BsonDocument>.Filter.Eq("ID", item.productID)).FirstOrDefault();

                    if (productDoc != null)
                    {
                        double hpp = productDoc.GetValue("HPP", 0).ToDouble();
                        double sellingPrice = (double)item.price;
                        double margin = sellingPrice - hpp;

                        var report = new BsonDocument
                    {
                        { "ReportDate", DateTime.Now.Date },
                        { "ProductID", item.productID },
                        { "Hpp", hpp },
                        { "SellingPrice", sellingPrice },
                        { "Margin", margin },
                        { "OpeningStock", productDoc.GetValue("Stock", 0).ToInt32() + item.quantity },
                        { "Sold", item.quantity },
                        { "VendorHpp", productDoc.GetValue("VendorHpp", 0).ToDouble() },
                        { "CategoryID", productDoc.GetValue("CategoryID", 0).ToInt32() },
                        { "PaymentMethodID", 2 },
                        { "ReportTime", DateTime.Now }
                    };

                        MongoDBService.Insert("SalesReport", report);
                    }
                }

                ClearAllCartState(); // 🧹 Baru clear keranjang setelah itu

                ReceiptForm receiptForm = new ReceiptForm();
                receiptForm.SetReceiptInfo(orderId.ToString(), (decimal)lastOrder["TotalPrice"].ToDouble(), "QRIS", 1, productDTOs);
                this.Hide(); // 👉 Sembunyikan CheckoutForm
                receiptForm.ShowDialog(); // 👉 Tampilkan Receipt
                this.Close(); // 👉 Tutup CheckoutForm setelah receipt ditutup



                this.Close();
            }
            else
            {

                // ❌ User menutup QRIS, anggap pesanan dibatalkan
                var receiptFilter = Builders<BsonDocument>.Filter.Eq("OrderID", orderId);
                var receiptDoc = MongoDBService.GetByFilter("Receipt", receiptFilter).FirstOrDefault();

                if (receiptDoc != null)
                {
                    var updateReceipt = Builders<BsonDocument>.Update
                        .Set("StatusId", 4);
                    MongoDBService.Update("Receipt", receiptFilter, updateReceipt);

                }
                else
                {
                    var cancelReceipt = new BsonDocument
            {
                { "OrderID", orderId },
                { "TotalAmount", lastOrder["TotalPrice"] },
                { "ReceiptDate", DateTime.Now },
                { "PaymentMethodId", 2 }, // QRIS
                {"StatusId", 4 }
                
            };

                    MongoDBService.Insert("Receipt", cancelReceipt);
                }


                MessageBox.Show("Pembayaran dibatalkan.");
                var productDTOs = GetGroupedCartItems(); // Ambil isi cart dulu
                ClearAllCartState(); // Baru clear cart

                // Show Receipt walaupun dibatalkan
                ReceiptForm receiptForm = new ReceiptForm();
                receiptForm.SetReceiptInfo(orderId.ToString(),
                    (decimal)lastOrder["TotalPrice"].ToDouble(),
                    "QRIS",
                    4, // statusId = 4 => dibatalkan
                    productDTOs);

                this.Hide(); // 👉 Sembunyikan CheckoutForm
                receiptForm.ShowDialog(); // 👉 Tampilkan Receipt
                this.Close(); // 👉 Tutup CheckoutForm setelah receipt ditutup

            }
        }

        public static List<ProductDTO> GetGroupedCartItems()
        {
            return UserControl1.cartItems
                .GroupBy(p => new { p.productID, Note = string.IsNullOrWhiteSpace(p.note) ? "-" : p.note.Trim() })

                .Select(g =>
                {
                    var first = g.First();
                    return new ProductDTO
                    {
                        productID = first.productID,
                        name = first.name,
                        price = first.price,
                        quantity = g.Sum(x => x.quantity),
                        image = first.image,
                        note = first.note
                    };
                }).ToList();
        }

        public void FullClearCart()
        {
            UserControl1.cartItems.Clear();
            UserControl1.cartProductIds.Clear(); // Kalau kamu pakai ID tracking

        }


        private void ClearCartFromDatabase(int userId)
        {
            if (userId <= 0)
            {
                MessageBox.Show("❌ User ID tidak valid.");
                return;
            }

            var cartCollection = MongoDBService.GetCollection("Cart");
            var filter = Builders<BsonDocument>.Filter.Eq("UserID", userId);
            var result = cartCollection.DeleteMany(filter);

            //MessageBox.Show($"🗑️ Dihapus dari MongoDB: {result.DeletedCount} item untuk user {userId}");
        }


        private void ClearAllCartState()
        {
            //MessageBox.Show("🧹 Memulai clear keranjang...");

            ClearCartFromDatabase(Support.userID);

            UserControl1.cartItems.Clear();
            UserControl1.cartProductIds.Clear();

            cartForm.flowLayoutPanel2.Controls.Clear();
            cartForm.RefreshCart();

            //MessageBox.Show("✅ Keranjang berhasil dikosongkan.");
        }

        private void UpdateStockByStatusIfAllowed(BsonValue orderId, int statusId)
        {
            var allowedStatuses = new[] { 2, 3, 5, 7 };

            if (!allowedStatuses.Contains(statusId))
                return;

            // Cegah stok dikurang 2x (misalnya cek SalesReport)
            var alreadyUpdated = MongoDBService.GetByFilter("SalesReport",
                Builders<BsonDocument>.Filter.Eq("OrderID", orderId)).Any();

            if (alreadyUpdated)
                return;

            // Lanjut kurangi stok
            var orderDetails = MongoDBService.GetByFilter("OrderDetail",
                Builders<BsonDocument>.Filter.Eq("OrderID", orderId));

            foreach (var item in orderDetails)
            {
                int productId = item["ProductID"].AsInt32;
                int quantity = item["Quantity"].AsInt32;

                var filter = Builders<BsonDocument>.Filter.Eq("ID", productId);
                var update = Builders<BsonDocument>.Update.Inc("Stock", -quantity);
                MongoDBService.Update("Product", filter, update);
            }
        }




        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
