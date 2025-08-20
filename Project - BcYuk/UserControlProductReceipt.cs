using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project___BcYuk
{
    public partial class UserControlProductReceipt : UserControl
    {
        //private ProductDTO _product;
        
        public UserControlProductReceipt(ProductDTO product)
        {
            InitializeComponent();

            // SET SEMUA LABEL DI SINI
            receiptLblProductName.Text = product.name;
            receiptLblProductQuantity.Text = ((decimal)product.quantity).ToString("N0");
            receiptProductLblPrice.Text = "Rp" + ((product.price * product.quantity) ?? 0).ToString("N0", new CultureInfo("id-ID"));
            receiptProductLblNote.Text = "Catatan: " + (product.note ?? "-");


            SetReceiptProductImage(product.image); // kirim langsung URL kalau itu URL



        }

        private void UserControlProductHistory_Load(object sender, EventArgs e)
        {


        }

        public string receiptProductName
        {
            get => receiptLblProductName.Text;
            set => receiptLblProductName.Text = value;
        }
        

        public string receiptProductPrice
        {
            get => receiptProductLblPrice.Text;
            set => receiptProductLblPrice.Text = value;
        }

        public string receiptProductQuantiity
        {
            get => receiptLblProductQuantity.Text;
            set => receiptLblProductQuantity.Text = value;
        }

        public string receiptProductNote
        {
            get => receiptProductLblNote.Text;
            set => receiptProductLblNote.Text = value;
        }

        public void SetReceiptProductImage(string pathOrUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(pathOrUrl))
                {
                    SetDefaultFallbackImage();
                    return;
                }

                // ✅ Deteksi kalau path dari URL
                if (pathOrUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                {
                    receiptpicturebxImgProduct.LoadAsync(pathOrUrl);
                    return;
                }

                // Kalau path lokal (base tanpa ekstensi)
                string[] extensions = { ".jpg", ".jpeg", ".png", ".bmp" };
                string foundPath = null;

                foreach (var ext in extensions)
                {
                    string fullPath = pathOrUrl + ext;
                    if (File.Exists(fullPath))
                    {
                        foundPath = fullPath;
                        break;
                    }
                }

                if (foundPath != null)
                {
                    using (var stream = new FileStream(foundPath, FileMode.Open, FileAccess.Read))
                    {
                        receiptpicturebxImgProduct.Image = Image.FromStream(stream);
                    }
                }
                else
                {
                    SetDefaultFallbackImage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Gagal memuat gambar:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetDefaultFallbackImage();
            }
        }

        private void SetDefaultFallbackImage()
        {
            receiptpicturebxImgProduct.Image = new Bitmap(1, 1);
            receiptpicturebxImgProduct.BackColor = Color.LightGray;
        }




    }
}
