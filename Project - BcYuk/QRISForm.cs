using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project___BcYuk
{
    public partial class QRISForm : Form
    {
        public bool UserConfirmed { get; private set; } = false;
        int totalSeconds = 180; // 3 menit
        public static QRISForm Instance;

        public static QRISForm GetInstance()
        {
            if (Instance == null || Instance.IsDisposed)
            {
                Instance = new QRISForm();
            }
            return Instance;
        }

        public QRISForm()
        {
            InitializeComponent();
            Instance = this;
        }



        private void QRISForm_Load(object sender, EventArgs e)
        {
            

            qrTimeoutTimer.Interval = 1000;
            UpdateTimerLabel();
            qrTimeoutTimer.Start();
        }

        private void btnSayaSudahBayar_Click(object sender, EventArgs e)
        {
            qrTimeoutTimer.Stop();
            UserConfirmed = true; // ✅ ini penting!
            MessageBox.Show("Silakan tunjukkan bukti pembayaran ke petugas.", "Menunggu Konfirmasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void qrTimeoutTimer_Tick(object sender, EventArgs e)
        {
            totalSeconds--;

            if (totalSeconds <= 0)
            {
                qrTimeoutTimer.Stop();
                MessageBox.Show("Waktu pembayaran habis. QRIS ditutup otomatis.", "Timeout", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
               
            }
            else
            {
                UpdateTimerLabel();
            }
        }

        private void UpdateTimerLabel()
        {
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;
            lblCountdown.Text = $"Waktu tersisa: {minutes} menit {seconds:D2} detik";

            if (totalSeconds <= 30)
                lblCountdown.ForeColor = Color.Red;
            else
                lblCountdown.ForeColor = Color.Black;
        }

        private void QRISForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!UserConfirmed)
            {
                var result = MessageBox.Show(
                    "❌ Kamu belum klik 'Sudah Bayar'. Jika kamu keluar sekarang, pesanan akan dibatalkan. Yakin ingin keluar?",
                    "Konfirmasi Keluar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    e.Cancel = true; // ← ini baru bisa!
                }
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void btnSayaSudahBayar_Click_1(object sender, EventArgs e)
        {
           
        }
    }
}
