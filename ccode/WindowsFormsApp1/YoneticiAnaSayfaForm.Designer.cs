namespace WindowsFormsApp1
{
    partial class YoneticiAnaSayfaForm
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
            this.CalisanEkle = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAylik = new System.Windows.Forms.Button();
            this.btnHaftalik = new System.Windows.Forms.Button();
            this.btnGunluk = new System.Windows.Forms.Button();
            this.CalisanCikar = new System.Windows.Forms.Button();
            this.btnMenuGuncelle = new System.Windows.Forms.Button();
            this.btnMenuCikar = new System.Windows.Forms.Button();
            this.btnMenuEkle = new System.Windows.Forms.Button();
            this.satis = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblKullaniciAdi
            // 
            this.lblKullaniciAdi.AutoSize = true;
            this.lblKullaniciAdi.Location = new System.Drawing.Point(165, 24);
            this.lblKullaniciAdi.Name = "lblKullaniciAdi";
            this.lblKullaniciAdi.Size = new System.Drawing.Size(44, 16);
            this.lblKullaniciAdi.TabIndex = 0;
            this.lblKullaniciAdi.Text = "label1";
            // 
            // CalisanEkle
            // 
            this.CalisanEkle.Location = new System.Drawing.Point(558, 21);
            this.CalisanEkle.Name = "CalisanEkle";
            this.CalisanEkle.Size = new System.Drawing.Size(139, 67);
            this.CalisanEkle.TabIndex = 1;
            this.CalisanEkle.Text = "Çalışan ekle";
            this.CalisanEkle.UseVisualStyleBackColor = true;
            this.CalisanEkle.Click += new System.EventHandler(this.CalisanEkle_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Controls.Add(this.lblKullaniciAdi);
            this.groupBox1.Location = new System.Drawing.Point(12, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(756, 63);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "HOŞ GELDİNİZ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.satis);
            this.groupBox2.Controls.Add(this.btnAylik);
            this.groupBox2.Controls.Add(this.btnHaftalik);
            this.groupBox2.Controls.Add(this.btnGunluk);
            this.groupBox2.Controls.Add(this.CalisanCikar);
            this.groupBox2.Controls.Add(this.btnMenuGuncelle);
            this.groupBox2.Controls.Add(this.btnMenuCikar);
            this.groupBox2.Controls.Add(this.btnMenuEkle);
            this.groupBox2.Controls.Add(this.CalisanEkle);
            this.groupBox2.Location = new System.Drawing.Point(12, 110);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(756, 315);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "YÖNETİCİ İŞLEMLERİ";
            // 
            // btnAylik
            // 
            this.btnAylik.Location = new System.Drawing.Point(313, 189);
            this.btnAylik.Name = "btnAylik";
            this.btnAylik.Size = new System.Drawing.Size(119, 84);
            this.btnAylik.TabIndex = 8;
            this.btnAylik.Text = "Aylık Özet";
            this.btnAylik.UseVisualStyleBackColor = true;
            this.btnAylik.Click += new System.EventHandler(this.btnAylik_Click);
            // 
            // btnHaftalik
            // 
            this.btnHaftalik.Location = new System.Drawing.Point(168, 189);
            this.btnHaftalik.Name = "btnHaftalik";
            this.btnHaftalik.Size = new System.Drawing.Size(121, 84);
            this.btnHaftalik.TabIndex = 7;
            this.btnHaftalik.Text = "Haftalık Özet";
            this.btnHaftalik.UseVisualStyleBackColor = true;
            this.btnHaftalik.Click += new System.EventHandler(this.btnHaftalik_Click);
            // 
            // btnGunluk
            // 
            this.btnGunluk.Location = new System.Drawing.Point(6, 189);
            this.btnGunluk.Name = "btnGunluk";
            this.btnGunluk.Size = new System.Drawing.Size(139, 84);
            this.btnGunluk.TabIndex = 6;
            this.btnGunluk.Text = "Günlük Özet";
            this.btnGunluk.UseVisualStyleBackColor = true;
            this.btnGunluk.Click += new System.EventHandler(this.btnGunluk_Click);
            // 
            // CalisanCikar
            // 
            this.CalisanCikar.Location = new System.Drawing.Point(558, 112);
            this.CalisanCikar.Name = "CalisanCikar";
            this.CalisanCikar.Size = new System.Drawing.Size(139, 58);
            this.CalisanCikar.TabIndex = 5;
            this.CalisanCikar.Text = "Çalışan Çıkar";
            this.CalisanCikar.UseVisualStyleBackColor = true;
            this.CalisanCikar.Click += new System.EventHandler(this.CalisanCikar_Click);
            // 
            // btnMenuGuncelle
            // 
            this.btnMenuGuncelle.Location = new System.Drawing.Point(379, 43);
            this.btnMenuGuncelle.Name = "btnMenuGuncelle";
            this.btnMenuGuncelle.Size = new System.Drawing.Size(133, 67);
            this.btnMenuGuncelle.TabIndex = 4;
            this.btnMenuGuncelle.Text = "Menu Güncelle";
            this.btnMenuGuncelle.UseVisualStyleBackColor = true;
            this.btnMenuGuncelle.Click += new System.EventHandler(this.btnMenuGuncelle_Click);
            // 
            // btnMenuCikar
            // 
            this.btnMenuCikar.Location = new System.Drawing.Point(208, 43);
            this.btnMenuCikar.Name = "btnMenuCikar";
            this.btnMenuCikar.Size = new System.Drawing.Size(137, 67);
            this.btnMenuCikar.TabIndex = 3;
            this.btnMenuCikar.Text = "Menü Çıkar";
            this.btnMenuCikar.UseVisualStyleBackColor = true;
            this.btnMenuCikar.Click += new System.EventHandler(this.btnMenuCikar_Click);
            // 
            // btnMenuEkle
            // 
            this.btnMenuEkle.Location = new System.Drawing.Point(25, 43);
            this.btnMenuEkle.Name = "btnMenuEkle";
            this.btnMenuEkle.Size = new System.Drawing.Size(149, 67);
            this.btnMenuEkle.TabIndex = 2;
            this.btnMenuEkle.Text = "Menü ekle";
            this.btnMenuEkle.UseVisualStyleBackColor = true;
            this.btnMenuEkle.Click += new System.EventHandler(this.btnMenuEkle_Click);
            // 
            // satis
            // 
            this.satis.Location = new System.Drawing.Point(558, 189);
            this.satis.Name = "satis";
            this.satis.Size = new System.Drawing.Size(139, 84);
            this.satis.TabIndex = 9;
            this.satis.Text = "Satış Sıralamaları";
            this.satis.UseVisualStyleBackColor = true;
            this.satis.Click += new System.EventHandler(this.satis_Click);
            // 
            // YoneticiAnaSayfaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "YoneticiAnaSayfaForm";
            this.Text = "YoneticiAnaSayfaForm";
            this.Load += new System.EventHandler(this.YoneticiAnaSayfaForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblKullaniciAdi;
        private System.Windows.Forms.Button CalisanEkle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnMenuGuncelle;
        private System.Windows.Forms.Button btnMenuCikar;
        private System.Windows.Forms.Button btnMenuEkle;
        private System.Windows.Forms.Button CalisanCikar;
        private System.Windows.Forms.Button btnAylik;
        private System.Windows.Forms.Button btnHaftalik;
        private System.Windows.Forms.Button btnGunluk;
        private System.Windows.Forms.Button satis;
    }
}