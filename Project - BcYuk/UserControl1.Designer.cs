namespace Project___BcYuk
{
    partial class UserControl1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControl1));
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.customePanel = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.btnAdd = new Guna.UI2.WinForms.Guna2Button();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.pictureProduct = new Guna.UI2.WinForms.Guna2PictureBox();
            this.lblName = new System.Windows.Forms.Label();
            this.tableLayoutPanel3.SuspendLayout();
            this.customePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureProduct)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.customePanel, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(-1, -1);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(521, 720);
            this.tableLayoutPanel3.TabIndex = 26;
            this.tableLayoutPanel3.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel3_Paint);
            // 
            // customePanel
            // 
            this.customePanel.BackColor = System.Drawing.Color.Transparent;
            this.customePanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.customePanel.BorderRadius = 10;
            this.customePanel.BorderThickness = 1;
            this.customePanel.Controls.Add(this.btnAdd);
            this.customePanel.Controls.Add(this.lblCategory);
            this.customePanel.Controls.Add(this.lblPrice);
            this.customePanel.Controls.Add(this.pictureProduct);
            this.customePanel.Controls.Add(this.lblName);
            this.customePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customePanel.Location = new System.Drawing.Point(28, 31);
            this.customePanel.Margin = new System.Windows.Forms.Padding(28, 31, 28, 31);
            this.customePanel.Name = "customePanel";
            this.customePanel.Size = new System.Drawing.Size(465, 658);
            this.customePanel.TabIndex = 32;
            this.customePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.customePanel_Paint);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BorderRadius = 10;
            this.btnAdd.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAdd.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAdd.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAdd.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAdd.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(127)))), ((int)(((byte)(0)))));
            this.btnAdd.Font = new System.Drawing.Font("Poppins Medium", 14F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(32, 576);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(404, 64);
            this.btnAdd.TabIndex = 22;
            this.btnAdd.Text = "Add to cart";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click_1);
            // 
            // lblCategory
            // 
            this.lblCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Poppins Medium", 14F, System.Drawing.FontStyle.Bold);
            this.lblCategory.ForeColor = System.Drawing.Color.DimGray;
            this.lblCategory.Location = new System.Drawing.Point(32, 413);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(163, 50);
            this.lblCategory.TabIndex = 23;
            this.lblCategory.Text = "Makanan";
            this.lblCategory.Click += new System.EventHandler(this.lblCategory_Click);
            // 
            // lblPrice
            // 
            this.lblPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Poppins", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrice.Location = new System.Drawing.Point(32, 516);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(144, 50);
            this.lblPrice.TabIndex = 21;
            this.lblPrice.Text = "Rp 3.000";
            // 
            // pictureProduct
            // 
            this.pictureProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureProduct.BorderRadius = 10;
            this.pictureProduct.Image = ((System.Drawing.Image)(resources.GetObject("pictureProduct.Image")));
            this.pictureProduct.ImageRotate = 0F;
            this.pictureProduct.Location = new System.Drawing.Point(32, 18);
            this.pictureProduct.Name = "pictureProduct";
            this.pictureProduct.Size = new System.Drawing.Size(404, 382);
            this.pictureProduct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureProduct.TabIndex = 20;
            this.pictureProduct.TabStop = false;
            this.pictureProduct.Click += new System.EventHandler(this.pictureProduct_Click);
            // 
            // lblName
            // 
            this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.Font = new System.Drawing.Font("Poppins", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(32, 465);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(404, 42);
            this.lblName.TabIndex = 19;
            this.lblName.Text = "Choki - Choki";
            this.lblName.Click += new System.EventHandler(this.lblName_Click);
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel3);
            this.Margin = new System.Windows.Forms.Padding(15, 3, 3, 3);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(520, 719);
            this.Load += new System.EventHandler(this.UserControl1_Load);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.customePanel.ResumeLayout(false);
            this.customePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureProduct)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel customePanel;
        private Guna.UI2.WinForms.Guna2Button btnAdd;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblPrice;
        private Guna.UI2.WinForms.Guna2PictureBox pictureProduct;
        private System.Windows.Forms.Label lblName;
    }
}
