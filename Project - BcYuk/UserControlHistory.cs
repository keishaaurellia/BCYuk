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
    public partial class UserControlHistory : UserControl
    {
        private string _orderId;
        public UserControlHistory(string orderId)
        {
            InitializeComponent();

            _orderId = orderId;
        }

        private void UserControlHistory_Load(object sender, EventArgs e)
        {

        }

        public void SetOrderInfo(DateTime? orderDate, decimal totalPrice, int statusId)
        { 
            lblHistoryOrderNumber.Text = "Order Number : " + (_orderId.ToString());
            lblHistoryTanggal.Text = orderDate.HasValue
                ? orderDate.Value.ToString("dd MMMM yyyy")
                : "-"; // Atau tampilkan "Tanggal tidak tersedia"

            lblHistoryTotalBelanja.Text = "Rp " + totalPrice.ToString("N0");
            SetStatusVisual(statusId);
        }


        public void SetStatusVisual(int statusId)
        {
            switch (statusId)
            {
                case 1:
                    lblHistoryStatuslblHistoryStatus.Text = "Menunggu Konfirmasi";
                    lblHistoryStatuslblHistoryStatus.ForeColor = Color.Orange;
                    break;
                case 2:
                    lblHistoryStatuslblHistoryStatus.Text = "Pesanan Disiapkan";
                    lblHistoryStatuslblHistoryStatus.ForeColor = Color.Goldenrod;
                    break;
                case 3:
                    lblHistoryStatuslblHistoryStatus.Text = "Pesanan Selesai";
                    lblHistoryStatuslblHistoryStatus.ForeColor = Color.Green;
                    break;
                case 4:
                    lblHistoryStatuslblHistoryStatus.Text = "Pesanan Dibatalkan Pengguna";
                    lblHistoryStatuslblHistoryStatus.ForeColor = Color.DarkGray;
                    break;
                case 5:
                    lblHistoryStatuslblHistoryStatus.Text = "Pembayaran Berhasil";
                    lblHistoryStatuslblHistoryStatus.ForeColor = Color.SeaGreen;
                    break;
                case 6:
                    lblHistoryStatuslblHistoryStatus.Text = "Pembayaran Gagal";
                    lblHistoryStatuslblHistoryStatus.ForeColor = Color.DarkRed;
                    break;
                case 7:
                    lblHistoryStatuslblHistoryStatus.Text = "Sisa waktu Ambil Pesanan 10 Menit";
                    lblHistoryStatuslblHistoryStatus.ForeColor = Color.DarkRed;
                    break;
                case 8:
                    lblHistoryStatuslblHistoryStatus.Text = "Pesanan Dibatalkan Aplikasi";
                    lblHistoryStatuslblHistoryStatus.ForeColor = Color.DarkRed;
                    break;
                default:
                    lblHistoryStatuslblHistoryStatus.Text = "Status tidak diketahui";
                    lblHistoryStatuslblHistoryStatus.ForeColor = Color.Black;
                    break;
            }
        }




        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnReceipt_Click(object sender, EventArgs e)
        {


            try
            {
                var objectId = ObjectId.Parse(_orderId);
                var orderFilter = Builders<BsonDocument>.Filter.Eq("_id", objectId);
                var order = MongoDBService.GetByFilter("Order", orderFilter).FirstOrDefault();

                if (order != null)
                {
                    // Ambil payment/receipt
                    var receiptFilter = Builders<BsonDocument>.Filter.Eq("OrderID", order["_id"]);
                    var receiptDoc = MongoDBService.GetByFilter("Receipt", receiptFilter).FirstOrDefault();

                    // Ambil PaymentMethodId dari receipt

                    int methodId = receiptDoc?.GetValue("PaymentMethodId", 0).ToInt32() ?? 0;


                    // Query ke collection PaymentMethod
                    var methodDoc = MongoDBService.GetByFilter("PaymentMethod",
                        Builders<BsonDocument>.Filter.Eq("ID", methodId)).FirstOrDefault();

                    // Ambil nama metode pembayaran, fallback ke "-"
                    string paymentMethod = methodDoc?.GetValue("MethodName", "-").AsString ?? "-";

                    int statusId = receiptDoc?.GetValue("StatusId", 0).ToInt32() ?? 0;
                   
                    decimal totalPrice = (decimal)order["TotalPrice"].ToDouble();

                    // Ambil detail produk
                    var detailFilter = Builders<BsonDocument>.Filter.Eq("OrderID", ObjectId.Parse(_orderId));
                    var orderDetails = MongoDBService.GetByFilter("OrderDetail", detailFilter);
                    var items = orderDetails
                        .Select(od => new {
                            ProductID = od["ProductID"].AsInt32,
                            Quantity = od["Quantity"].ToInt32(),
                            SubTotal = (decimal)od["SubTotal"].ToDouble(),
                            Note = od.GetValue("Note", "").AsString.Trim() // Trim biar konsisten
                        })
                        .GroupBy(x => new { x.ProductID, Note = string.IsNullOrWhiteSpace(x.Note) ? "-" : x.Note })
                        .Select(g =>
                        {
                            var quantity = g.Sum(x => x.Quantity);
                            var subtotal = g.Sum(x => x.SubTotal);
                            var first = g.First();
                            var product = MongoDBService.GetByFilter("Product",
                                Builders<BsonDocument>.Filter.Eq("ID", g.Key.ProductID)).FirstOrDefault();

                            return new ProductDTO
                            {
                                productID = g.Key.ProductID,
                                name = product["ProductName"].AsString,
                                price = subtotal / quantity,
                                quantity = quantity,
                                image = product.GetValue("ImageURL", "").AsString,
                                note = g.Key.Note
                            };
                        }).ToList();

                    // Tampilkan Receipt
                    ReceiptForm receiptForm = new ReceiptForm();
                    receiptForm.SetReceiptInfo(_orderId, totalPrice, paymentMethod, statusId, items);
                    receiptForm.ShowDialog(); // 👉 Tampilkan Receipt   
                    
                }
                else
                {
                    MessageBox.Show("❗Data order tidak ditemukan.");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("❗Format OrderID tidak valid.");
            }


            



        }

        private void customePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblHistoryStatuslblHistoryStatus_Click(object sender, EventArgs e)
        {

        }
    }
}
