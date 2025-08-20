namespace Project___BcYuk
{
    partial class UserControlProductReceipt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlProductReceipt));
            this.receiptLblProductName = new System.Windows.Forms.Label();
            this.receiptLblProductQuantity = new System.Windows.Forms.Label();
            this.receiptProductLblPrice = new System.Windows.Forms.Label();
            this.receiptProductLblNote = new System.Windows.Forms.Label();
            this.receiptpicturebxImgProduct = new Guna.UI2.WinForms.Guna2PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.receiptpicturebxImgProduct)).BeginInit();
            this.SuspendLayout();
            // 
            // receiptLblProductName
            // 
            this.receiptLblProductName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.receiptLblProductName.AutoSize = true;
            this.receiptLblProductName.Font = new System.Drawing.Font("Poppins Medium", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.receiptLblProductName.Location = new System.Drawing.Point(253, 22);
            this.receiptLblProductName.Name = "receiptLblProductName";
            this.receiptLblProductName.Size = new System.Drawing.Size(208, 56);
            this.receiptLblProductName.TabIndex = 42;
            this.receiptLblProductName.Text = "Nutri Boost";
            // 
            // receiptLblProductQuantity
            // 
            this.receiptLblProductQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.receiptLblProductQuantity.AutoSize = true;
            this.receiptLblProductQuantity.Font = new System.Drawing.Font("Poppins Medium", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.receiptLblProductQuantity.Location = new System.Drawing.Point(1937, 22);
            this.receiptLblProductQuantity.Name = "receiptLblProductQuantity";
            this.receiptLblProductQuantity.Size = new System.Drawing.Size(36, 56);
            this.receiptLblProductQuantity.TabIndex = 46;
            this.receiptLblProductQuantity.Text = "1";
            // 
            // receiptProductLblPrice
            // 
            this.receiptProductLblPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.receiptProductLblPrice.AutoSize = true;
            this.receiptProductLblPrice.Font = new System.Drawing.Font("Poppins Medium", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.receiptProductLblPrice.Location = new System.Drawing.Point(2050, 22);
            this.receiptProductLblPrice.Name = "receiptProductLblPrice";
            this.receiptProductLblPrice.Size = new System.Drawing.Size(171, 56);
            this.receiptProductLblPrice.TabIndex = 51;
            this.receiptProductLblPrice.Text = "Rp 7.000";
            // 
            // receiptProductLblNote
            // 
            this.receiptProductLblNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.receiptProductLblNote.AutoSize = true;
            this.receiptProductLblNote.Font = new System.Drawing.Font("Poppins SemiBold", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.receiptProductLblNote.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.receiptProductLblNote.Location = new System.Drawing.Point(254, 87);
            this.receiptProductLblNote.Name = "receiptProductLblNote";
            this.receiptProductLblNote.Size = new System.Drawing.Size(166, 53);
            this.receiptProductLblNote.TabIndex = 52;
            this.receiptProductLblNote.Text = "Catatan :";
            // 
            // receiptpicturebxImgProduct
            // 
            this.receiptpicturebxImgProduct.BorderRadius = 10;
            this.receiptpicturebxImgProduct.Image = ((System.Drawing.Image)(resources.GetObject("receiptpicturebxImgProduct.Image")));
            this.receiptpicturebxImgProduct.ImageRotate = 0F;
            this.receiptpicturebxImgProduct.Location = new System.Drawing.Point(27, 6);
            this.receiptpicturebxImgProduct.Name = "receiptpicturebxImgProduct";
            this.receiptpicturebxImgProduct.Size = new System.Drawing.Size(194, 152);
            this.receiptpicturebxImgProduct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.receiptpicturebxImgProduct.TabIndex = 53;
            this.receiptpicturebxImgProduct.TabStop = false;
            // 
            // UserControlProductReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.receiptpicturebxImgProduct);
            this.Controls.Add(this.receiptProductLblNote);
            this.Controls.Add(this.receiptLblProductName);
            this.Controls.Add(this.receiptLblProductQuantity);
            this.Controls.Add(this.receiptProductLblPrice);
            this.Name = "UserControlProductReceipt";
            this.Size = new System.Drawing.Size(2289, 170);
            this.Load += new System.EventHandler(this.UserControlProductHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.receiptpicturebxImgProduct)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label receiptLblProductName;
        private System.Windows.Forms.Label receiptLblProductQuantity;
        private System.Windows.Forms.Label receiptProductLblPrice;
        private System.Windows.Forms.Label receiptProductLblNote;
        private Guna.UI2.WinForms.Guna2PictureBox receiptpicturebxImgProduct;
    }
}
