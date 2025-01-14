namespace evet
{
    partial class SepetForm
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
            this.flowLayoutPanelCartItems = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPay = new System.Windows.Forms.Button();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.flowLayoutPanelCartItems.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanelCartItems
            // 
            this.flowLayoutPanelCartItems.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.flowLayoutPanelCartItems.Controls.Add(this.panel1);
            this.flowLayoutPanelCartItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelCartItems.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelCartItems.Name = "flowLayoutPanelCartItems";
            this.flowLayoutPanelCartItems.Size = new System.Drawing.Size(800, 450);
            this.flowLayoutPanelCartItems.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnPay);
            this.panel1.Controls.Add(this.lblTotalAmount);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 0;
            // 
            // btnPay
            // 
            this.btnPay.Location = new System.Drawing.Point(42, 62);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(100, 23);
            this.btnPay.TabIndex = 1;
            this.btnPay.Text = "Ödeme Yap";
            this.btnPay.UseVisualStyleBackColor = true;
            this.btnPay.Click += new System.EventHandler(this.btnPay_Click);
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Location = new System.Drawing.Point(49, 19);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(44, 16);
            this.lblTotalAmount.TabIndex = 0;
            this.lblTotalAmount.Text = "label1";
            // 
            // SepetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.flowLayoutPanelCartItems);
            this.Name = "SepetForm";
            this.Text = "SepetForm";
            this.flowLayoutPanelCartItems.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelCartItems;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnPay;
        private System.Windows.Forms.Label lblTotalAmount;
    }
}