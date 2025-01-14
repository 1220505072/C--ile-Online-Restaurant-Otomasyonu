namespace WindowsFormsApp1
{
    partial class CalisanAnaSayfaForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblKullaniciAdi = new System.Windows.Forms.Label();
            this.dataGridViewOrders = new System.Windows.Forms.DataGridView();
            this.btnOnayla = new System.Windows.Forms.Button();
            this.btnIptal = new System.Windows.Forms.Button();
            this.btnYolaCikti = new System.Windows.Forms.Button();
            this.btnTeslimEdildi = new System.Windows.Forms.Button();
            this.btnMoveNext = new System.Windows.Forms.Button();
            this.btnMovePrevious = new System.Windows.Forms.Button();
            this.btnMoveFirst = new System.Windows.Forms.Button();
            this.btnMoveLast = new System.Windows.Forms.Button();
            this.btnOzet = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // lblKullaniciAdi
            // 
            this.lblKullaniciAdi.AutoSize = true;
            this.lblKullaniciAdi.Location = new System.Drawing.Point(9, 9);
            this.lblKullaniciAdi.Name = "lblKullaniciAdi";
            this.lblKullaniciAdi.Size = new System.Drawing.Size(44, 16);
            this.lblKullaniciAdi.TabIndex = 0;
            this.lblKullaniciAdi.Text = "label1";
            // 
            // dataGridViewOrders
            // 
            this.dataGridViewOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOrders.Location = new System.Drawing.Point(12, 28);
            this.dataGridViewOrders.Name = "dataGridViewOrders";
            this.dataGridViewOrders.RowHeadersWidth = 51;
            this.dataGridViewOrders.RowTemplate.Height = 24;
            this.dataGridViewOrders.Size = new System.Drawing.Size(1200, 250);
            this.dataGridViewOrders.TabIndex = 1;
            // 
            // btnOnayla
            // 
            this.btnOnayla.Location = new System.Drawing.Point(12, 368);
            this.btnOnayla.Name = "btnOnayla";
            this.btnOnayla.Size = new System.Drawing.Size(122, 44);
            this.btnOnayla.TabIndex = 2;
            this.btnOnayla.Text = "Sipariş Alındı";
            this.btnOnayla.UseVisualStyleBackColor = true;
            this.btnOnayla.Click += new System.EventHandler(this.btnOnayla_Click);
            // 
            // btnIptal
            // 
            this.btnIptal.Location = new System.Drawing.Point(153, 368);
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.Size = new System.Drawing.Size(120, 44);
            this.btnIptal.TabIndex = 3;
            this.btnIptal.Text = "Sipariş İptal Et";
            this.btnIptal.UseVisualStyleBackColor = true;
            this.btnIptal.Click += new System.EventHandler(this.btnIptal_Click);
            // 
            // btnYolaCikti
            // 
            this.btnYolaCikti.Location = new System.Drawing.Point(296, 368);
            this.btnYolaCikti.Name = "btnYolaCikti";
            this.btnYolaCikti.Size = new System.Drawing.Size(102, 44);
            this.btnYolaCikti.TabIndex = 4;
            this.btnYolaCikti.Text = "Sipariş Yolda";
            this.btnYolaCikti.UseVisualStyleBackColor = true;
            this.btnYolaCikti.Click += new System.EventHandler(this.btnYolaCikti_Click);
            // 
            // btnTeslimEdildi
            // 
            this.btnTeslimEdildi.Location = new System.Drawing.Point(422, 368);
            this.btnTeslimEdildi.Name = "btnTeslimEdildi";
            this.btnTeslimEdildi.Size = new System.Drawing.Size(106, 44);
            this.btnTeslimEdildi.TabIndex = 5;
            this.btnTeslimEdildi.Text = "Sipariş Teslim Edildi";
            this.btnTeslimEdildi.UseVisualStyleBackColor = true;
            this.btnTeslimEdildi.Click += new System.EventHandler(this.btnTeslimEdildi_Click);
            // 
            // btnMoveNext
            // 
            this.btnMoveNext.Location = new System.Drawing.Point(12, 297);
            this.btnMoveNext.Name = "btnMoveNext";
            this.btnMoveNext.Size = new System.Drawing.Size(105, 42);
            this.btnMoveNext.TabIndex = 6;
            this.btnMoveNext.Text = "İleri";
            this.btnMoveNext.UseVisualStyleBackColor = true;
            this.btnMoveNext.Click += new System.EventHandler(this.btnMoveNext_Click_1);
            // 
            // btnMovePrevious
            // 
            this.btnMovePrevious.Location = new System.Drawing.Point(135, 297);
            this.btnMovePrevious.Name = "btnMovePrevious";
            this.btnMovePrevious.Size = new System.Drawing.Size(96, 42);
            this.btnMovePrevious.TabIndex = 7;
            this.btnMovePrevious.Text = "Geri ";
            this.btnMovePrevious.UseVisualStyleBackColor = true;
            this.btnMovePrevious.Click += new System.EventHandler(this.btnMovePrevious_Click_1);
            // 
            // btnMoveFirst
            // 
            this.btnMoveFirst.Location = new System.Drawing.Point(252, 297);
            this.btnMoveFirst.Name = "btnMoveFirst";
            this.btnMoveFirst.Size = new System.Drawing.Size(82, 42);
            this.btnMoveFirst.TabIndex = 8;
            this.btnMoveFirst.Text = "İlk Kayıt";
            this.btnMoveFirst.UseVisualStyleBackColor = true;
            this.btnMoveFirst.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnMoveLast
            // 
            this.btnMoveLast.Location = new System.Drawing.Point(355, 297);
            this.btnMoveLast.Name = "btnMoveLast";
            this.btnMoveLast.Size = new System.Drawing.Size(88, 42);
            this.btnMoveLast.TabIndex = 9;
            this.btnMoveLast.Text = "Son Kayıt";
            this.btnMoveLast.UseVisualStyleBackColor = true;
            this.btnMoveLast.Click += new System.EventHandler(this.btnMoveLast_Click_1);
            // 
            // btnOzet
            // 
            this.btnOzet.Location = new System.Drawing.Point(618, 308);
            this.btnOzet.Name = "btnOzet";
            this.btnOzet.Size = new System.Drawing.Size(114, 49);
            this.btnOzet.TabIndex = 10;
            this.btnOzet.Text = "Günlük Özet";
            this.btnOzet.UseVisualStyleBackColor = true;
            this.btnOzet.Click += new System.EventHandler(this.btnOzet_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(604, 382);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(154, 56);
            this.btnLogin.TabIndex = 11;
            this.btnLogin.Text = "ÇIKIŞ";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // CalisanAnaSayfaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnOzet);
            this.Controls.Add(this.btnMoveLast);
            this.Controls.Add(this.btnMoveFirst);
            this.Controls.Add(this.btnMovePrevious);
            this.Controls.Add(this.btnMoveNext);
            this.Controls.Add(this.btnTeslimEdildi);
            this.Controls.Add(this.btnYolaCikti);
            this.Controls.Add(this.btnIptal);
            this.Controls.Add(this.btnOnayla);
            this.Controls.Add(this.dataGridViewOrders);
            this.Controls.Add(this.lblKullaniciAdi);
            this.Name = "CalisanAnaSayfaForm";
            this.Text = "CalisanAnaSayfaForm";
            this.Load += new System.EventHandler(this.CalisanAnaSayfaForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblKullaniciAdi;
        private System.Windows.Forms.DataGridView dataGridViewOrders;
        private System.Windows.Forms.Button btnOnayla;
        private System.Windows.Forms.Button btnIptal;
        private System.Windows.Forms.Button btnYolaCikti;
        private System.Windows.Forms.Button btnTeslimEdildi;
        private System.Windows.Forms.Button btnMoveNext;
        private System.Windows.Forms.Button btnMovePrevious;
        private System.Windows.Forms.Button btnMoveFirst;
        private System.Windows.Forms.Button btnMoveLast;
        private System.Windows.Forms.Button btnOzet;
        private System.Windows.Forms.Button btnLogin;
    }
}