using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Project___BcYuk
{
    public partial class HistoryTransactionForm : Form
    {

        public static HistoryTransactionForm Instance;
        public static HistoryTransactionForm GetInstance()
        {
            if (Instance == null || Instance.IsDisposed)
                Instance = new HistoryTransactionForm(); // constructor tanpa parameter
            return Instance;
        }

        public HistoryTransactionForm()
        {
            InitializeComponent();
        }

        private void HistoryTransactionForm_Load(object sender, EventArgs e)
        {
            lblUser.Text = Support.userFullName;

            LoadUserHistory();

        }


        private void LoadUserHistory()
        {
            flowLayoutPanelHistory.Controls.Clear();
            flowLayoutPanelOnGoing.Controls.Clear();

            var filter = Builders<BsonDocument>.Filter.Eq("CustomerID", Support.userID);
            var orders = MongoDBService.GetByFilter("Order", filter);

            //foreach (var order in orders)
            //{
            //    var orderId = order["_id"].ToString();
            //    var orderDate = order.GetValue("OrderDate", BsonNull.Value).ToUniversalTime();
            //    var totalPrice = (decimal)order.GetValue("TotalPrice", 0).ToDouble();

            //    // ✅ Ambil Status dari collection Receipt
            //    var receiptFilter = Builders<BsonDocument>.Filter.Eq("OrderID", order["_id"]);
            //    var receiptDoc = MongoDBService.GetByFilter("Receipt", receiptFilter).FirstOrDefault();
            //    int statusId = receiptDoc?.GetValue("StatusId", 0).ToInt32() ?? 0;

            //    var uc = new UserControlHistory(orderId);
            //    uc.SetOrderInfo(orderDate, totalPrice, statusId);

            //    if (statusId == 3 || statusId == 4 || statusId == 7)
            //        flowLayoutPanelHistory.Controls.Add(uc); // ✅ masuk ke Riwayat
            //    else
            //        flowLayoutPanelOnGoing.Controls.Add(uc); // ✅ masuk ke Sedang Berjalan
            //}

            foreach (var order in orders)
            {
                var orderId = order["_id"].ToString();
                var orderDate = order.GetValue("OrderDate", BsonNull.Value).ToUniversalTime();
                var totalPrice = (decimal)order.GetValue("TotalPrice", 0).ToDouble();

                // 🔍 Tambahin debug disini
                Debug.WriteLine($"📦 OrderID: {order["_id"]}");

                var receiptFilter = Builders<BsonDocument>.Filter.Eq("OrderID", order["_id"]);
                var receiptDoc = MongoDBService.GetByFilter("Receipt", receiptFilter).FirstOrDefault();

                if (receiptDoc == null)
                {
                    Debug.WriteLine("❌ Receipt not found for order.");
                    continue; // skip biar gak error di bawah
                }
                else
                {
                    Debug.WriteLine($"✅ StatusId: {receiptDoc.GetValue("StatusId", 0)}");
                }

                int statusId = receiptDoc.GetValue("StatusId", 0).ToInt32();

                var uc = new UserControlHistory(orderId);
                uc.SetOrderInfo(orderDate, totalPrice, statusId);

                if (statusId == 3 || statusId == 4 || statusId == 8)
                {
                    Debug.WriteLine($"🧾 Tambah ke Riwayat: {orderId} (Status: {statusId})");
                    flowLayoutPanelHistory.Controls.Add(uc);
                }
                else
                {
                    Debug.WriteLine($"📡 Tambah ke OnGoing: {orderId} (Status: {statusId})");
                    flowLayoutPanelOnGoing.Controls.Add(uc);
                }
            }

        }


        private void guna2Button3_Click(object sender, EventArgs e)
        {
            MyCartForm.GetInstance().Show();
            MyCartForm.GetInstance().RefreshCart();
            this.Close();

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            CustomerMainForm.GetInstance().Show();
            this.Close(); // atau this.Hide();

        }

        private void guna2Button4_Click_1(object sender, EventArgs e)
        {
            AccountForm.GetInstance().Show();
            this.Close(); // atau this.Hide();
        }
    }
}
