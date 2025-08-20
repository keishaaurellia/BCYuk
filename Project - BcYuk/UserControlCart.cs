using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Project___BcYuk
{
    public partial class UserControlCart : UserControl
    {

        

        public event Action QuantityChanged;
        private Product cartItem;


        public Product SelectedProduct { get; set; }

        public UserControlCart(Product product)
        {
            InitializeComponent();

            this.cartItem = product;
            // Set data ke UI seperti gambar, nama, harga, dll
            txtCartCatatan.Text = product.note; // isi awal
            txtCartCatatan.TextChanged += txtCartCatatan_TextChanged;

            SelectedProduct = product; // ✅ Pastikan produk disimpan dengan benar
            productName = product.name;
            productCategory = product.category;
            productQty = product.quantity.ToString();
            productlblTotalQtyCheckout = product.quantity.ToString();
            productlblPrice = "Rp " + (product.price.ToString("N0", new CultureInfo("id-ID")));
            //productlblQty = (product.quantity.ToString()) + " x";
            productSubtotal = "Rp" + ((product.Price * product.quantity) ?? 0).ToString("N0", new CultureInfo("id-ID"));

            // 🔥 Pastikan path gambar valid sebelum di-load
            string imagePath = product.image;

            if (!string.IsNullOrEmpty(imagePath))
            {
                if (imagePath.StartsWith("http")) // 🔗 Dari internet
                {
                    try
                    {
                        picturebxImgProduct.LoadAsync(imagePath); // ✅ Load dari URL langsung
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("❌ Gagal load gambar dari internet: " + ex.Message);
                    }
                }
                else // 📂 Dari lokal
                {
                    string imageFolderPath = @"C:\Users\muhra\Downloads\Poto produk BC yuk\";
                    string fullPath = Path.Combine(imageFolderPath, imagePath);

                    SetProductImage(fullPath);
                }
            }
        }
        private void UserControlCart_Load(object sender, EventArgs e)
        {

        }


        public void SetCheckoutMode(bool isCheckout)
        {
            if (isCheckout)
            {
                // Sembunyikan elemen seperti Quantity dan tombol delete
                numQty.Visible = false;
                btnRemove.Visible = false;
                lblJdlJumlahBeli.Visible = true;
                cartlbltotalbeliCheckout.Visible = true;

                // Tampilkan catatan (jika perlu)
                txtCartCatatan.Visible = true;

                // 🔽 Ubah ukuran kontrol supaya lebih ramping di form checkout
                this.Width = 850; // misalnya sebelumnya 180, kecilin jadi 120
                
                txtCartCatatan.Width = 600;

                cartlblSubtotal.Location = new Point(710, lblProductPrice.Location.Y);

            }
            else
            {
                lblJdlJumlahBeli.Visible = false;
                cartlbltotalbeliCheckout.Visible = false;
                numQty.Visible = true;
                btnRemove.Visible = true;
                txtCartCatatan.Visible = false;

            }
        }

        public void SetReceiptMode(bool isReceipt)
        {
            if (isReceipt)
            {
                // Sembunyikan elemen-elemen yang tidak diperlukan di receipt
                cartLblCategoryProduct.Visible = false;
                lblJdlJumlahBeli.Visible = false;
                cartlblSubtotal.Visible = false;
                picturebxImgProduct.Visible = false;
                numQty.Visible = false;
                btnRemove.Visible = false;

                // Tampilkan elemen yang diperlukan
                cartLblNameProduct.Visible = true;
                lblProductPrice.Visible = true;

                // Tambahkan "x" ke jumlah qty hanya saat mode receipt
                if (!cartlbltotalbeliCheckout.Text.EndsWith("x"))
                {
                    cartlbltotalbeliCheckout.Text += "x";
                }
            }
            else
            {
                // Kembalikan UI seperti semula
                picturebxImgProduct.Visible = true;
                numQty.Visible = true;
                btnRemove.Visible = true;

                cartlblSubtotal.Visible = false;
                lblProductPrice.Visible = false;

                // Hapus "x" kalau sebelumnya ditambahin
                cartlbltotalbeliCheckout.Text = cartlbltotalbeliCheckout.Text.Replace("x", "").Trim();
            }
        }



        public string productName
        {
            get => cartLblNameProduct.Text;
            set => cartLblNameProduct.Text = value;
        }

        public string productCategory
        {
            get => cartLblCategoryProduct.Text;
            set => cartLblCategoryProduct.Text = value;
        }

        public string productQty
        {
            get => numQty.Text;
            set => numQty.Text = value;
        }

        public string productlblPrice
        {
            get => lblProductPrice.Text;
            set => lblProductPrice.Text = value;
        }

        public string productSubtotal
        {
            get => cartlblSubtotal.Text;
            set => cartlblSubtotal.Text = value;
        }

        public string productlblTotalQtyCheckout
        {
            get => cartlbltotalbeliCheckout.Text;
            set => cartlbltotalbeliCheckout.Text = value; 
        }


        public void SetProductImage(string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
            {
                try
                {
                    picturebxImgProduct.Image = Image.FromFile(imagePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Error loading image: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("❌ File gambar tidak ditemukan: " + imagePath);
            }
        }


        private void RemoveProductFormCart()
        {
            if (SelectedProduct == null)
            {
                MessageBox.Show("Error: No product selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirmResult = MessageBox.Show("Are you sure to remove this product?", "Confirm Remove",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                // 1. Hapus dari cartItems memory
                var productToRemove = UserControl1.cartItems.FirstOrDefault(p => p.productID == SelectedProduct.productID);
                if (productToRemove != null)
                {
                    UserControl1.cartItems.Remove(productToRemove);
                }


                // ✅ Hapus dari MongoDB
                var filter = Builders<BsonDocument>.Filter.And(
                    Builders<BsonDocument>.Filter.Eq("UserID", Support.userID),
                    Builders<BsonDocument>.Filter.Eq("ProductID", SelectedProduct.productID)
                );
                MongoDBService.Delete("Cart", filter);


                //// 2. Hapus dari DB
                //using (var db = new DataClasses1DataContext())
                //{
                //    var cartDb = db.Carts.FirstOrDefault(c => c.UserID == Support.userID && c.ProductID == SelectedProduct.productID);
                //    if (cartDb != null)
                //    {
                //        db.Carts.DeleteOnSubmit(cartDb);
                //        db.SubmitChanges();
                //    }
                //}

                // 3. Hapus dari UI (langsung remove UserControl dari parent)
                this.Parent.Controls.Remove(this);

                // 4. Hapus dari list ID (biar tombol "Add Cart" aktif lagi)
                UserControl1.cartProductIds.Remove(SelectedProduct.productID);

                //// 5. Update tombol-tombol cart di halaman utama
                //var productControls = Application.OpenForms
                //    .OfType<CustomerMainForm>()?
                //    .FirstOrDefault()?
                //    .Controls
                //    .OfType<UserControl1>();

                //if (productControls != null)
                //{
                //    foreach (var control in productControls)
                //    {
                //        control.UpdateButtonState();
                //    }
                //}

                // Update tombol di CustomerMainForm
                var customerForm = Application.OpenForms.OfType<CustomerMainForm>().FirstOrDefault();
                if (customerForm != null)
                {
                    customerForm.RefreshProductButtons();
                }

                // Update total di cart
                var cartForm = Application.OpenForms.OfType<MyCartForm>().FirstOrDefault();
                cartForm?.UpdateTotalPrice();

                // 6. Update total harga di MyCartForm
                //MyCartForm myCartForm = Application.OpenForms.OfType<MyCartForm>().FirstOrDefault();
                //if (myCartForm != null)
                //{
                //    myCartForm.UpdateTotalPrice();
                //}

                //MyCartForm cartForm = Application.OpenForms.OfType<MyCartForm>().FirstOrDefault();

                //if (cartForm == null)
                //{
                //    cartForm = new MyCartForm();
                //    cartForm.Show();
                //}
                //else
                //{
                //    cartForm.BringToFront(); // Bawa ke depan kalau udah kebuka
                //}

              

                MessageBox.Show("Product removed from cart!");
            }
        }


        private void btnRemove_Click(object sender, EventArgs e)
        {
            RemoveProductFormCart();
        }

        private void txtCartCatatan_TextChanged(object sender, EventArgs e)
        {
            if (cartItem != null)
            {
                cartItem.note = txtCartCatatan.Text.Trim();
            }

        }

        private void numQty_ValueChanged(object sender, EventArgs e)
        {

            if (SelectedProduct == null)
                return;

            int newQty = (int)numQty.Value;

            if (newQty > SelectedProduct.stock)
            {
                MessageBox.Show("Quantity cannot be higher than stock!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numQty.Value = SelectedProduct.quantity;
                return;
            }

            var cartItem = UserControl1.cartItems.FirstOrDefault(p => p.productID == SelectedProduct.productID);
            if (cartItem != null)
            {
                cartItem.quantity = newQty;
                SelectedProduct.quantity = newQty;

                decimal newSubtotal = cartItem.price * newQty;
                cartlblSubtotal.Text = "Rp " + newSubtotal.ToString("N0", new CultureInfo("id-ID"));

                // ✅ Update di MongoDB
                var filter = Builders<BsonDocument>.Filter.And(
                    Builders<BsonDocument>.Filter.Eq("UserID", Support.userID),
                    Builders<BsonDocument>.Filter.Eq("ProductID", cartItem.productID)
                );

                var update = Builders<BsonDocument>.Update.Set("Quantity", newQty);
                MongoDBService.Update("Cart", filter, update);

                // 🔥 Ini bagian penting! Notify MyCartForm
                QuantityChanged?.Invoke();
            }

          
        }


    }
    }

