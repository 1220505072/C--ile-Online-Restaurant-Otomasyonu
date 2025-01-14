namespace evet
{
    partial class MenuForm
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
            this.btnYemekler = new System.Windows.Forms.Button();
            this.btnIcecekler = new System.Windows.Forms.Button();
            this.btnTatlilar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnYemekler
            // 
            this.btnYemekler.Location = new System.Drawing.Point(12, 57);
            this.btnYemekler.Name = "btnYemekler";
            this.btnYemekler.Size = new System.Drawing.Size(240, 246);
            this.btnYemekler.TabIndex = 0;
            this.btnYemekler.Text = "Yemekler";
            this.btnYemekler.UseVisualStyleBackColor = true;
            this.btnYemekler.Click += new System.EventHandler(this.btnYemekler_Click);
            // 
            // btnIcecekler
            // 
            this.btnIcecekler.Location = new System.Drawing.Point(273, 57);
            this.btnIcecekler.Name = "btnIcecekler";
            this.btnIcecekler.Size = new System.Drawing.Size(242, 246);
            this.btnIcecekler.TabIndex = 1;
            this.btnIcecekler.Text = "İçecekler";
            this.btnIcecekler.UseVisualStyleBackColor = true;
            this.btnIcecekler.Click += new System.EventHandler(this.btnIcecekler_Click);
            // 
            // btnTatlilar
            // 
            this.btnTatlilar.Location = new System.Drawing.Point(548, 57);
            this.btnTatlilar.Name = "btnTatlilar";
            this.btnTatlilar.Size = new System.Drawing.Size(240, 246);
            this.btnTatlilar.TabIndex = 2;
            this.btnTatlilar.Text = "Tatlılar";
            this.btnTatlilar.UseVisualStyleBackColor = true;
            this.btnTatlilar.Click += new System.EventHandler(this.btnTatlilar_Click);
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnTatlilar);
            this.Controls.Add(this.btnIcecekler);
            this.Controls.Add(this.btnYemekler);
            this.Name = "MenuForm";
            this.Text = "MenuForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnYemekler;
        private System.Windows.Forms.Button btnIcecekler;
        private System.Windows.Forms.Button btnTatlilar;
    }
}