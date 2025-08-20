using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace Project___BcYuk
{
    public partial class ReceiptForm : Form
    {
        
        private List<ProductDTO> receiptItems = new List<ProductDTO>();
        private bool receiptDataLoaded = false;

        public static ReceiptForm Instance;

        public static ReceiptForm GetInstance()
        {
            if (Instance == null || Instance.IsDisposed)
                Instance = new ReceiptForm();
            return Instance;
        }



        public ReceiptForm()
        {
            
            InitializeComponent();

            //_orderId = orderId;

        }

        private void tableLayoutPanel13_Paint(object sender, PaintEventArgs e)
        {

        }


        private void ReceiptForm_Load(object sender, EventArgs e)
        {
            if (!receiptDataLoaded)
            {
                MessageBox.Show("❗Data belum disiapkan. Pastikan SetReceiptInfo dipanggil sebelum ShowDialog()");
                return;
            }

            LoadCartForReceipt();


        }

        public void SetReceiptInfo(string orderId, decimal totalPrice, string _, int __, List<ProductDTO> items)
        {
            // Simpan item produk
            receiptItems = new List<ProductDTO>(items);

            // Ambil Order ID sebagai ObjectId
            if (!ObjectId.TryParse(orderId, out ObjectId objectId))
            {
                MessageBox.Show("❗Format Order ID tidak valid.");
                return;
            }

            // Ambil data Order
            var orderFilter = Builders<BsonDocument>.Filter.Eq("_id", objectId);
            var orderDoc = MongoDBService.GetByFilter("Order", orderFilter).FirstOrDefault();

            if (orderDoc != null)
            {
                receiptlblOrderNumber.Text = orderId;
                receiptlblTotalPrice.Text = "Rp " + totalPrice.ToString("N0", new CultureInfo("id-ID"));

                // 👉 Ambil pickup info
                int pickupScheduleId = orderDoc.GetValue("PickupScheduleId", 0).ToInt32();
                var pickupDoc = MongoDBService.GetByFilter("PickupSchedule",
                    Builders<BsonDocument>.Filter.Eq("ID", pickupScheduleId)).FirstOrDefault();

                if (pickupDoc != null)
                {
                    var pickupDate = DateTime.Parse(pickupDoc["PickupDate"].ToString());
                    var pickupTime = pickupDoc.GetValue("PickupTime", "").AsString;

                    lblReceiptTime.Text = pickupDate.ToString("dd MMMM yyyy", new CultureInfo("id-ID")) + " - " + pickupTime;
                }

                // 👉 Ambil receipt info (payment + status)
                var receiptDoc = MongoDBService.GetByFilter("Receipt",
                Builders<BsonDocument>.Filter.Eq("OrderID", objectId)).FirstOrDefault();

                if (receiptDoc != null)
                {
                    int statusId = receiptDoc.GetValue("StatusId", 0).ToInt32();
                    int methodId = receiptDoc.GetValue("PaymentMethodId", 0).ToInt32();

                    MessageBox.Show($"✔️ Status ID dari Receipt: {statusId}");

                    // ❌ Tampilkan warning kalau pembayaran gagal
                    if (statusId == 6)
                    {
                        MessageBox.Show("❌ Pembayaran kamu gagal dikonfirmasi. Silakan coba lagi atau hubungi petugas.");
                    }

                    // 👉 Ambil nama metode pembayaran
                    var methodDoc = MongoDBService.GetByFilter("PaymentMethod",
                        Builders<BsonDocument>.Filter.Eq("ID", methodId)).FirstOrDefault();

                    string methodName = methodDoc?.GetValue("MethodName", "-").AsString ?? "-";
                    receiptlblPaymentMethod.Text = methodName;

                    UpdateStatusButtonHighlight(statusId);
                    UpdateStatusTextSteps(statusId);    
                }


            }

            receiptDataLoaded = true;
        }



        private void LoadCartForReceipt()
        {
            flowLayoutPanelReceipt.Controls.Clear();

            foreach (var item in receiptItems)
            {
                Debug.WriteLine($"🧾 Tampilkan: {item.name} - Qty: {item.quantity} - Price: {item.price}");

                var control = new UserControlProductReceipt(item)
                {
                    Width = flowLayoutPanelReceipt.ClientSize.Width - 25
                };

                flowLayoutPanelReceipt.Controls.Add(control);
            }
        }

        private void UpdateStatusButtonHighlight(int statusId)
        {
            // Reset semua tombol ke default abu-abu
            btnReceiptMenungguKonfirmasi.FillColor = Color.Gainsboro;
            btnReceiptSedangDisiapkan.FillColor = Color.Gainsboro;
            btnReceiptSelesai.FillColor = Color.Gainsboro;

            switch (statusId)
            {
                case 1: // Menunggu Konfirmasi
                case 5: // Pembayaran Berhasil → step awal juga
                    btnReceiptMenungguKonfirmasi.FillColor = Color.Orange;
                    break;

                case 2: // Pesanan disiapkan
                    btnReceiptSedangDisiapkan.FillColor = Color.Orange;
                    break;

                case 3: // Pesanan selesai
                    btnReceiptSelesai.FillColor = Color.Orange;
                    break;

                case 4: // Dibatalkan
                case 6: // Gagal bayar
                    btnReceiptMenungguKonfirmasi.FillColor = Color.Gray;
                    btnReceiptSedangDisiapkan.FillColor = Color.Gray;
                    btnReceiptSelesai.FillColor = Color.Gray;
                    break;

                case 7: // Ambil dalam 10 menit (opsional: semua nyala + warning)
                    btnReceiptMenungguKonfirmasi.FillColor = Color.Orange;
                    btnReceiptSedangDisiapkan.FillColor = Color.Orange;
                    btnReceiptSelesai.FillColor = Color.Orange;
                    MessageBox.Show("⏰ Ambil pesanan kamu dalam 10 menit ya!");
                    break;

                default:
                    MessageBox.Show("❓ Status tidak dikenali.");
                    break;
            }
        }

        private void UpdateStatusTextSteps(int statusId)
        {
            // Reset semua ke default abu-abu
            SetStepText(lblStep1, "Menunggu Konfirmasi", Color.Gray, "⭘");
            SetStepText(lblStep2, "Sedang Disiapkan", Color.Gray, "⭘");
            SetStepText(lblStep3, "Selesai", Color.Gray, "⭘");

            switch (statusId)
            {
                case 1: // Menunggu konfirmasi
                case 5: // Pembayaran berhasil
                    SetStepText(lblStep1, "Menunggu Konfirmasi", Color.Orange, "🔶");
                    break;

                case 2: // Pesanan disiapkan
                    SetStepText(lblStep1, "Menunggu Konfirmasi", Color.Green, "✅");
                    SetStepText(lblStep2, "Sedang Disiapkan", Color.Orange, "🔶");
                    break;

                case 3: // Pesanan selesai
                case 7:
                    //MessageBox.Show("🚨 MASUK status 7");
                    SetStepText(lblStep1, "Menunggu Konfirmasi", Color.Green, "✅");
                    SetStepText(lblStep2, "Sedang Disiapkan", Color.Green, "✅");
                    SetStepText(lblStep3, "Selesai", Color.Orange, "🔶");
                    break;

                case 4: // Dibatalkan
                case 6: // Gagal bayar
                    SetStepText(lblStep1, "Menunggu Konfirmasi", Color.DarkGray, "❌");
                    SetStepText(lblStep2, "Sedang Disiapkan", Color.DarkGray, "❌");
                    SetStepText(lblStep3, "Selesai", Color.DarkGray, "❌");
                    break;  
            }
        }

        private void SetStepText(Label label, string text, Color color, string emoji)
        {
            label.Text = $"{emoji} {text}";
            label.ForeColor = color;
            label.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        }


        public void UpdateTotalPrice()
        {
            decimal total = UserControl1.cartItems.Sum(x => x.price * x.quantity);
            receiptlblTotalPrice.Text = "Rp " + total.ToString("N0", new CultureInfo("id-ID"));
        }



        private void flowLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {
           
         
        

    }

        private void tableLayoutPanel18_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timerResize_Tick(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            CustomerMainForm.GetInstance().Show();
            this.Close();// atau this.Hide();
        }
    }
}
