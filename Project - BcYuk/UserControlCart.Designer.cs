namespace Project___BcYuk
{
    partial class UserControlCart
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlCart));
            this.picturebxImgProduct = new Guna.UI2.WinForms.Guna2PictureBox();
            this.cartLblNameProduct = new System.Windows.Forms.Label();
            this.cartLblCategoryProduct = new System.Windows.Forms.Label();
            this.cartlblSubtotal = new System.Windows.Forms.Label();
            this.btnRemove = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.txtCartCatatan = new Guna.UI2.WinForms.Guna2TextBox();
            this.numQty = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.lblProductPrice = new System.Windows.Forms.Label();
            this.cartlbltotalbeliCheckout = new System.Windows.Forms.Label();
            this.lblJdlJumlahBeli = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picturebxImgProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRemove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQty)).BeginInit();
            this.SuspendLayout();
            // 
            // picturebxImgProduct
            // 
            this.picturebxImgProduct.BorderRadius = 10;
            this.picturebxImgProduct.Image = ((System.Drawing.Image)(resources.GetObject("picturebxImgProduct.Image")));
            this.picturebxImgProduct.ImageRotate = 0F;
            this.picturebxImgProduct.Location = new System.Drawing.Point(29, 39);
            this.picturebxImgProduct.Name = "picturebxImgProduct";
            this.picturebxImgProduct.Size = new System.Drawing.Size(279, 252);
            this.picturebxImgProduct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picturebxImgProduct.TabIndex = 0;
            this.picturebxImgProduct.TabStop = false;
            // 
            // cartLblNameProduct
            // 
            this.cartLblNameProduct.AutoSize = true;
            this.cartLblNameProduct.Font = new System.Drawing.Font("Poppins Medium", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cartLblNameProduct.Location = new System.Drawing.Point(325, 89);
            this.cartLblNameProduct.Name = "cartLblNameProduct";
            this.cartLblNameProduct.Size = new System.Drawing.Size(208, 56);
            this.cartLblNameProduct.TabIndex = 1;
            this.cartLblNameProduct.Text = "Nutri Boost";
            // 
            // cartLblCategoryProduct
            // 
            this.cartLblCategoryProduct.AutoSize = true;
            this.cartLblCategoryProduct.Font = new System.Drawing.Font("Poppins Medium", 14F, System.Drawing.FontStyle.Bold);
            this.cartLblCategoryProduct.ForeColor = System.Drawing.Color.DimGray;
            this.cartLblCategoryProduct.Location = new System.Drawing.Point(326, 39);
            this.cartLblCategoryProduct.Name = "cartLblCategoryProduct";
            this.cartLblCategoryProduct.Size = new System.Drawing.Size(163, 50);
            this.cartLblCategoryProduct.TabIndex = 2;
            this.cartLblCategoryProduct.Text = "Minuman";
            // 
            // cartlblSubtotal
            // 
            this.cartlblSubtotal.AutoSize = true;
            this.cartlblSubtotal.Font = new System.Drawing.Font("Poppins Medium", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cartlblSubtotal.Location = new System.Drawing.Point(1497, 138);
            this.cartlblSubtotal.Name = "cartlblSubtotal";
            this.cartlblSubtotal.Size = new System.Drawing.Size(173, 56);
            this.cartlblSubtotal.TabIndex = 3;
            this.cartlblSubtotal.Text = "Rp 5.000";
            // 
            // btnRemove
            // 
            this.btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.Image")));
            this.btnRemove.ImageRotate = 0F;
            this.btnRemove.Location = new System.Drawing.Point(1716, 138);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnRemove.Size = new System.Drawing.Size(81, 56);
            this.btnRemove.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnRemove.TabIndex = 6;
            this.btnRemove.TabStop = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // txtCartCatatan
            // 
            this.txtCartCatatan.BorderRadius = 10;
            this.txtCartCatatan.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCartCatatan.DefaultText = "";
            this.txtCartCatatan.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtCartCatatan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtCartCatatan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCartCatatan.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCartCatatan.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCartCatatan.Font = new System.Drawing.Font("Poppins", 11F);
            this.txtCartCatatan.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCartCatatan.Location = new System.Drawing.Point(335, 238);
            this.txtCartCatatan.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCartCatatan.Name = "txtCartCatatan";
            this.txtCartCatatan.PlaceholderText = "Kasih catatan";
            this.txtCartCatatan.SelectedText = "";
            this.txtCartCatatan.Size = new System.Drawing.Size(1462, 53);
            this.txtCartCatatan.TabIndex = 8;
            this.txtCartCatatan.TextChanged += new System.EventHandler(this.txtCartCatatan_TextChanged);
            // 
            // numQty
            // 
            this.numQty.BackColor = System.Drawing.Color.Transparent;
            this.numQty.BorderRadius = 10;
            this.numQty.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.numQty.Font = new System.Drawing.Font("Poppins", 12F);
            this.numQty.Location = new System.Drawing.Point(1204, 134);
            this.numQty.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numQty.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQty.Name = "numQty";
            this.numQty.Size = new System.Drawing.Size(241, 60);
            this.numQty.TabIndex = 23;
            this.numQty.UpDownButtonFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(127)))), ((int)(((byte)(0)))));
            this.numQty.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQty.ValueChanged += new System.EventHandler(this.numQty_ValueChanged);
            // 
            // lblProductPrice
            // 
            this.lblProductPrice.AutoSize = true;
            this.lblProductPrice.Font = new System.Drawing.Font("Poppins Medium", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductPrice.Location = new System.Drawing.Point(325, 154);
            this.lblProductPrice.Name = "lblProductPrice";
            this.lblProductPrice.Size = new System.Drawing.Size(171, 56);
            this.lblProductPrice.TabIndex = 9;
            this.lblProductPrice.Text = "Rp 7.000";
            // 
            // cartlbltotalbeliCheckout
            // 
            this.cartlbltotalbeliCheckout.AutoSize = true;
            this.cartlbltotalbeliCheckout.Font = new System.Drawing.Font("Poppins Medium", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cartlbltotalbeliCheckout.Location = new System.Drawing.Point(712, 154);
            this.cartlbltotalbeliCheckout.Name = "cartlbltotalbeliCheckout";
            this.cartlbltotalbeliCheckout.Size = new System.Drawing.Size(36, 56);
            this.cartlbltotalbeliCheckout.TabIndex = 24;
            this.cartlbltotalbeliCheckout.Text = "1";
            // 
            // lblJdlJumlahBeli
            // 
            this.lblJdlJumlahBeli.AutoSize = true;
            this.lblJdlJumlahBeli.Font = new System.Drawing.Font("Poppins Medium", 14F, System.Drawing.FontStyle.Bold);
            this.lblJdlJumlahBeli.ForeColor = System.Drawing.Color.DimGray;
            this.lblJdlJumlahBeli.Location = new System.Drawing.Point(502, 158);
            this.lblJdlJumlahBeli.Name = "lblJdlJumlahBeli";
            this.lblJdlJumlahBeli.Size = new System.Drawing.Size(204, 50);
            this.lblJdlJumlahBeli.TabIndex = 25;
            this.lblJdlJumlahBeli.Text = "Jumlah Beli:";
            // 
            // UserControlCart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblJdlJumlahBeli);
            this.Controls.Add(this.cartlbltotalbeliCheckout);
            this.Controls.Add(this.numQty);
            this.Controls.Add(this.lblProductPrice);
            this.Controls.Add(this.txtCartCatatan);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.cartlblSubtotal);
            this.Controls.Add(this.cartLblCategoryProduct);
            this.Controls.Add(this.cartLblNameProduct);
            this.Controls.Add(this.picturebxImgProduct);
            this.Name = "UserControlCart";
            this.Size = new System.Drawing.Size(1842, 323);
            this.Load += new System.EventHandler(this.UserControlCart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picturebxImgProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRemove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQty)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2PictureBox picturebxImgProduct;
        private System.Windows.Forms.Label cartLblNameProduct;
        private System.Windows.Forms.Label cartLblCategoryProduct;
        private System.Windows.Forms.Label cartlblSubtotal;
        private Guna.UI2.WinForms.Guna2CirclePictureBox btnRemove;
        private Guna.UI2.WinForms.Guna2TextBox txtCartCatatan;
        private Guna.UI2.WinForms.Guna2NumericUpDown numQty;
        private System.Windows.Forms.Label lblProductPrice;
        private System.Windows.Forms.Label cartlbltotalbeliCheckout;
        private System.Windows.Forms.Label lblJdlJumlahBeli;
    }
}
