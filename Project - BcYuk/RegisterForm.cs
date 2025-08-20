using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Project___BcYuk
{
    public partial class RegisterForm : Form
    {
        // Menyimpan status apakah form digunakan untuk edit data (true) atau pendaftaran baru (false)
        private bool isEditMode = false; // default: mode daftar

        // Menyimpan ID user yang sedang diedit, default -1 (tidak ada)
        private int editingUserId = -1;


        public static RegisterForm Instance;
        public static RegisterForm instance;
        private static int currentUserId;

        public static RegisterForm GetInstance(int userId)
        {
            if (Instance == null || Instance.IsDisposed || currentUserId != userId)
            {
                Instance = new RegisterForm(userId);
                currentUserId = userId;
            }
            return Instance;
        }

       
        public static RegisterForm GetInstance()
        {
            if (instance == null || instance.IsDisposed)
                instance = new RegisterForm(); // constructor tanpa parameter
            return instance;
        }

        // Constructor untuk mode pendaftaran baru
        public RegisterForm()
        {
            InitializeComponent();
            isEditMode = false; // tetap mode daftar
            lblDaftar.Text = "Daftar Sekarang";
            lblBertanya.Visible = true;
            linkRegister.Visible = true;
        }

        // Constructor untuk mode edit data, menerima ID user yang akan diedit
        public RegisterForm(int userId)
        {
            InitializeComponent();
            isEditMode = true;
            editingUserId = userId;
        }




        private void RegisterForm_Load(object sender, EventArgs e)
        {
            LoadClassOptions();

            if (isEditMode)
            {
                var userCollection = MongoDBService.GetCollection("UserAuth");

                // Ambil data user berdasarkan ID dari Support class
                var filter = Builders<BsonDocument>.Filter.Eq("ID", Support.userID);
                var user = userCollection.Find(filter).FirstOrDefault();

                // Jika user ditemukan, isi field dengan data yang ada
                if (user != null)
                {
                    // Isi field dengan data user yang ada
                    txtNamaLengkap.Text = user["FullName"].AsString;
                    txtNomorTelepon.Text = user["PhoneNumber"].AsString;
                    txtPassword.Text = user["Password"].AsString;
                    txtKonfirmasiPassword.Text = user["Password"].AsString;
                    cmbKelas.SelectedValue = user["ClassId"].AsInt32;

                    btnDaftar.Text = "Edit Biodata";
                }
            }

        }
        private void LoadClassOptions()
        {
            // Ambil data kelas dari MongoDB
            var classCollection = MongoDBService.GetCollection("Class");
            // Ambil semua kelas
            var classes = classCollection.Find(new BsonDocument()).ToList();

            // Ambil ID dan nama kelas
            var classList = classes.Select(c => new
            {
                ID = c["ID"].AsInt32,
                ClassName = c["ClassName"].AsString
            }).ToList();

            cmbKelas.DisplayMember = "ClassName";
            cmbKelas.ValueMember = "ID";
            cmbKelas.DataSource = classList;

            // Tidak memilih kelas secara default
            cmbKelas.SelectedIndex = -1;  // agar tidak ada item yang otomatis dipilih
            cmbKelas.Text = "Pilih kelas..."; // placeholder yang kamu mau tampilkan
        }


        private bool checkAll()
        {
            Regex phone = new Regex(@"^\d{10,15}$");

            var userCollection = MongoDBService.GetCollection("UserAuth");

            // ✅ Kalau sedang edit, pastikan nama tidak digunakan user lain
            var existingUserFilter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("FullName", txtNamaLengkap.Text),
                Builders<BsonDocument>.Filter.Ne("ID", Support.userID)
            );

            var existingUser = userCollection.Find(existingUserFilter).FirstOrDefault();
            if (existingUser != null)
            {
                MessageBox.Show("Username sudah digunakan oleh user lain");
                return false;
            }

            // Password check
            if (txtPassword.Text.Length < 8)
            {
                MessageBox.Show("Password harus memiliki panjang minimal 8 karakter");
                return false;
            }
            else if (txtKonfirmasiPassword.Text != txtPassword.Text)
            {
                MessageBox.Show("Password tidak valid");
                return false;
            }

            return true;
        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkMasuk_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void cmbKelas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
           
        }

        private void btnDaftar_Click(object sender, EventArgs e)
        {
           
        }

        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private async void btnDaftar_Click_1(object sender, EventArgs e)
        {
            
          
            if (checkAll())
            {

                var userCollection = MongoDBService.GetCollection("UserAuth");

                // Jika dalam mode edit, update data user
                //if (isEditMode && editingUserId != -1)
                //{
                //    btnDaftar.Text = "Menyimpan...";
                //    btnDaftar.Enabled = false;


                //    int selectedClassId = (int)cmbKelas.SelectedValue;

                //    // Update data user di background thread
                //    await Task.Run(() =>
                //    {
                //        var filter = Builders<BsonDocument>.Filter.Eq("ID", Support.userID);
                //        var update = Builders<BsonDocument>.Update
                //            .Set("FullName", txtNamaLengkap.Text)
                //            .Set("PhoneNumber", txtNomorTelepon.Text)
                //            .Set("Password", txtPassword.Text)
                //            .Set("ClassId", selectedClassId);

                //        userCollection.UpdateOne(filter, update);
                //    });

                //    // Update support biar user langsung login
                //    Support.userFullName = txtNamaLengkap.Text;
                //    Support.userPhoneNumber = txtNomorTelepon.Text;
                //    Support.ClassId = cmbKelas.SelectedValue.ToString();

                //    btnDaftar.Text = "Edit Biodata";
                //    btnDaftar.Enabled = true;
                //    MessageBox.Show("✅ Biodata berhasil diperbarui!");
                //    this.Close();
                //}
                //else
                //{
                //    // Jika dalam mode pendaftaran baru, simpan data user
                //    int selectedClassId = (int)cmbKelas.SelectedValue;

                //    // Ambil user terakhir untuk menentukan ID baru
                //    var lastUser = userCollection.Find(new BsonDocument())
                //                                 .Sort(Builders<BsonDocument>.Sort.Descending("ID"))
                //                                 .FirstOrDefault();

                //    int newId = lastUser == null ? 1 : lastUser["ID"].AsInt32 + 1;

                //    // Buat dokumen baru untuk user baru
                //    var newUser = new BsonDocument
                //    {
                //        { "ID", newId },
                //        { "FullName", txtNamaLengkap.Text },
                //        { "PhoneNumber", txtNomorTelepon.Text },
                //        { "Password", txtPassword.Text },
                //        { "Role", "Customer" },
                //        { "CreatedAt", DateTime.UtcNow },
                //        { "ClassId", selectedClassId }
                //    };

                //    // Simpan user baru ke MongoDB
                //    userCollection.InsertOne(newUser);

                //    // Update support biar user langsung login
                //    Support.userID = newId;
                //    Support.userFullName = txtNamaLengkap.Text;
                //    Support.userPassword = txtPassword.Text;
                //    Support.userPhoneNumber = txtNomorTelepon.Text;
                //    Support.ClassId = selectedClassId.ToString();

                //    MessageBox.Show("✅ Register berhasil!");
                //    CustomerMainForm form = new CustomerMainForm();
                //    form.Show();
                //    this.Hide();
                //}

                if (isEditMode && editingUserId != -1)
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("ID", Support.userID);
                    var user = userCollection.Find(filter).FirstOrDefault();

                    if (user != null)
                    {
                        // Ambil nilai-nilai lama
                        string oldName = user.GetValue("FullName", "").AsString;
                        string oldPhone = user.GetValue("PhoneNumber", "").AsString;
                        string oldPassword = user.GetValue("Password", "").AsString;
                        int oldClassId = user.GetValue("ClassId", 0).ToInt32();

                        // Ambil nilai baru
                        string newName = txtNamaLengkap.Text;
                        string newPhone = txtNomorTelepon.Text;
                        string newPassword = txtPassword.Text;
                        int newClassId = (int)cmbKelas.SelectedValue;

                        // Cek apakah ada perubahan
                        bool isChanged = oldName != newName || oldPhone != newPhone || oldPassword != newPassword || oldClassId != newClassId;

                        if (!isChanged)
                        {
                            MessageBox.Show("❗Tidak ada perubahan yang disimpan.");
                            return;
                        }

                        // Jika ada perubahan, lanjut update
                        btnDaftar.Text = "Menyimpan...";
                        btnDaftar.Enabled = false;

                        await Task.Run(() =>
                        {
                            var update = Builders<BsonDocument>.Update
                                .Set("FullName", newName)
                                .Set("PhoneNumber", newPhone)
                                .Set("Password", newPassword)
                                .Set("ClassId", newClassId);

                            userCollection.UpdateOne(filter, update);
                        });

                        // Update ke Support class
                        Support.userFullName = newName;
                        Support.userPhoneNumber = newPhone;
                        Support.ClassId = newClassId.ToString();

                        btnDaftar.Text = "Edit Biodata";
                        btnDaftar.Enabled = true;
                        MessageBox.Show("✅ Biodata berhasil diperbarui!");
                        this.Close();
                    }
                }



            }
        }

        private void linkRegister_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginFormcs loginFormcs = new LoginFormcs();
            loginFormcs.Show();
            this.Hide();
        }

        private void lblDaftar_Click(object sender, EventArgs e)
        {

        }
    }
}
