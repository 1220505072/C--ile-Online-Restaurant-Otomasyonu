using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace evet
{
    public partial class SepetForm : Form
    {
        private decimal toplamTutar;

        public SepetForm()
        {
            InitializeComponent();
            DisplayCartItems();
        }

        // Sepeti FlowLayoutPanel'de listele
        private void DisplayCartItems()
        {
            toplamTutar = 0;

            foreach (var item in ShoppingCart.Items)
            {
                Panel cartItemPanel = new Panel
                {
                    Width = 300,
                    Height = 100,
                    BorderStyle = BorderStyle.FixedSingle
                };

                Label lblName = new Label
                {
                    Text = item.Ad,
                    Location = new System.Drawing.Point(10, 10)
                };
                cartItemPanel.Controls.Add(lblName);

                Label lblPrice = new Label
                {
                    Text = "Fiyat: " + item.Fiyat.ToString("C2"),
                    Location = new System.Drawing.Point(10, 40)
                };
                cartItemPanel.Controls.Add(lblPrice);

                Label lblQuantity = new Label
                {
                    Text = "Adet: " + item.Miktar.ToString(),
                    Location = new System.Drawing.Point(10, 70)
                };
                cartItemPanel.Controls.Add(lblQuantity);

                toplamTutar += item.Fiyat * item.Miktar;

                flowLayoutPanelCartItems.Controls.Add(cartItemPanel);
            }

            lblTotalAmount.Text = "Toplam Tutar: " + toplamTutar.ToString("C2");
        }

        // Ödeme işlemi başlatma
        private void btnPay_Click(object sender, EventArgs e)
        {

            // Burada ödeme işlemi yapılacak
            MessageBox.Show("Ödeme Başlatıldı. Teşekkürler!");
            // Sepet sıfırlanabilir veya veritabanına kaydedilebilir
            ShoppingCart cartForm = new ShoppingCart();
            cartForm.ClearCart();
        }
    }
}
