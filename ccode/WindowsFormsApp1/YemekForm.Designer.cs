namespace evet
{
    partial class YemekForm
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
        /// Required method for Designer support - do not modify the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanelProducts = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnGoToCart = new System.Windows.Forms.Button();
            this.numericUpDownMinPrice = new System.Windows.Forms.NumericUpDown();
            this.BtnGoBack = new System.Windows.Forms.Button();
            this.numericUpDownMaxPrice = new System.Windows.Forms.NumericUpDown();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.BtnApplyPriceFilter = new System.Windows.Forms.Button();
            this.comboBoxSortPrice = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.flowLayoutPanelProducts.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxPrice)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanelProducts
            // 
            this.flowLayoutPanelProducts.AutoScroll = true;
            this.flowLayoutPanelProducts.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.flowLayoutPanelProducts.Controls.Add(this.groupBox1);
            this.flowLayoutPanelProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelProducts.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelProducts.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelProducts.Name = "flowLayoutPanelProducts";
            this.flowLayoutPanelProducts.Size = new System.Drawing.Size(800, 450);
            this.flowLayoutPanelProducts.TabIndex = 0;
            this.flowLayoutPanelProducts.WrapContents = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.BtnGoToCart);
            this.groupBox1.Controls.Add(this.numericUpDownMinPrice);
            this.groupBox1.Controls.Add(this.BtnGoBack);
            this.groupBox1.Controls.Add(this.numericUpDownMaxPrice);
            this.groupBox1.Controls.Add(this.txtSearch);
            this.groupBox1.Controls.Add(this.BtnApplyPriceFilter);
            this.groupBox1.Controls.Add(this.comboBoxSortPrice);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(785, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fitreleme Grubu";
            // 
            // BtnGoToCart
            // 
            this.BtnGoToCart.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BtnGoToCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnGoToCart.Location = new System.Drawing.Point(635, 57);
            this.BtnGoToCart.Name = "BtnGoToCart";
            this.BtnGoToCart.Size = new System.Drawing.Size(144, 30);
            this.BtnGoToCart.TabIndex = 0;
            this.BtnGoToCart.Text = "Sepeti Görüntüle";
            this.BtnGoToCart.UseVisualStyleBackColor = false;
            this.BtnGoToCart.Click += new System.EventHandler(this.BtnGoToCart_Click);
            // 
            // numericUpDownMinPrice
            // 
            this.numericUpDownMinPrice.Location = new System.Drawing.Point(6, 62);
            this.numericUpDownMinPrice.Name = "numericUpDownMinPrice";
            this.numericUpDownMinPrice.Size = new System.Drawing.Size(100, 22);
            this.numericUpDownMinPrice.TabIndex = 3;
            // 
            // BtnGoBack
            // 
            this.BtnGoBack.Location = new System.Drawing.Point(649, 14);
            this.BtnGoBack.Name = "BtnGoBack";
            this.BtnGoBack.Size = new System.Drawing.Size(130, 30);
            this.BtnGoBack.TabIndex = 1;
            this.BtnGoBack.Text = "Geri dön";
            this.BtnGoBack.UseVisualStyleBackColor = true;
            this.BtnGoBack.Click += new System.EventHandler(this.BtnGoBack_Click_1);
            // 
            // numericUpDownMaxPrice
            // 
            this.numericUpDownMaxPrice.Location = new System.Drawing.Point(127, 62);
            this.numericUpDownMaxPrice.Name = "numericUpDownMaxPrice";
            this.numericUpDownMaxPrice.Size = new System.Drawing.Size(100, 22);
            this.numericUpDownMaxPrice.TabIndex = 4;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(479, 60);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(150, 22);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged_1);
            // 
            // BtnApplyPriceFilter
            // 
            this.BtnApplyPriceFilter.Location = new System.Drawing.Point(242, 44);
            this.BtnApplyPriceFilter.Name = "BtnApplyPriceFilter";
            this.BtnApplyPriceFilter.Size = new System.Drawing.Size(75, 43);
            this.BtnApplyPriceFilter.TabIndex = 0;
            this.BtnApplyPriceFilter.Text = "filtrele";
            this.BtnApplyPriceFilter.UseVisualStyleBackColor = true;
            this.BtnApplyPriceFilter.Click += new System.EventHandler(this.BtnApplyPriceFilter_Click);
            // 
            // comboBoxSortPrice
            // 
            this.comboBoxSortPrice.FormattingEnabled = true;
            this.comboBoxSortPrice.Location = new System.Drawing.Point(331, 60);
            this.comboBoxSortPrice.Name = "comboBoxSortPrice";
            this.comboBoxSortPrice.Size = new System.Drawing.Size(121, 24);
            this.comboBoxSortPrice.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "MİN FİYAT";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(124, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "MAX FİYAT";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(346, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "SIRALAMA";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(495, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "ARAMA ÇUBUĞU";
            // 
            // YemekForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.flowLayoutPanelProducts);
            this.Name = "YemekForm";
            this.Text = "YemekForm";
            this.flowLayoutPanelProducts.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxPrice)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelProducts;
        private System.Windows.Forms.Button BtnGoToCart;
        private System.Windows.Forms.Button BtnGoBack;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox comboBoxSortPrice;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxPrice;
        private System.Windows.Forms.NumericUpDown numericUpDownMinPrice;
        private System.Windows.Forms.Button BtnApplyPriceFilter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
