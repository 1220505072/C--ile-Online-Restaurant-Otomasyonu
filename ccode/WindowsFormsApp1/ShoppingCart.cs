using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace evet
{
    public partial class ShoppingCart : Form
    {
        // Sepet öğeleri
        public static List<CartItem> Items { get; set; } = new List<CartItem>();  // static yaparak her yerden erişilebilir

        public ShoppingCart()
        {
            InitializeComponent();
            DisplayCartItems();
        }

        // Sepetteki ürünleri göstermek için bir metod
        private void DisplayCartItems()
        {
            // Toplam tutar değişkeni
            decimal toplamTutar = 0;

            // FlowLayoutPanel gibi bir kontrol ekleyebilirsiniz
            foreach (var item in Items)
            {
                Panel itemPanel = new Panel
                {
                    Width = 400,
                    Height = 100, // Panel boyutunu daha uygun bir şekilde ayarladım
                    BorderStyle = BorderStyle.FixedSingle
                };

                // Sepet öğesini ekranda göstermek için etiketler oluşturun
                Label lblItem = new Label
                {
                    Text = $"{item.Ad} - {item.Miktar} x {item.Fiyat:C2}",
                    AutoSize = true,
                    Location = new Point(70, 10) // Resimden sonra gelmesi için konumu ayarladım
                };
                itemPanel.Controls.Add(lblItem);

                // Resim yolu varsa, resmi ekleyelim
                if (!string.IsNullOrEmpty(item.ResimYolu) && System.IO.File.Exists(item.ResimYolu))
                {
                    PictureBox picBox = new PictureBox
                    {
                        Image = Image.FromFile(item.ResimYolu),
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Location = new Point(10, 10),
                        Size = new Size(50, 50)
                    };
                    itemPanel.Controls.Add(picBox);
                }

                // Silme butonunu ekleyelim
                Button btnRemove = new Button
                {
                    Text = "Sil",
                    Size = new Size(50, 30),
                    Location = new Point(330, 10) // Buton konumunu ayarlıyoruz
                };

                // Silme butonuna tıklama olayını ekleyelim
                btnRemove.Click += (sender, e) =>
                {
                    // Silme işlemi için sepetten öğeyi çıkarma
                    Items.Remove(item);
                    // FlowLayoutPanel içeriğinden paneli kaldırma
                    flowLayoutPanelCart.Controls.Remove(itemPanel);
                    // Toplam tutarı yeniden hesapla
                    UpdateTotalAmount();
                };

                itemPanel.Controls.Add(btnRemove);

                // Ürün miktarını değiştirebilmek için NumericUpDown ekleyelim
                NumericUpDown numQuantity = new NumericUpDown
                {
                    Value = item.Miktar,
                    Minimum = 1,
                    Maximum = 100, // İstenilen limit
                    Size = new Size(60, 30),
                    Location = new Point(70, 40)
                };

                // Miktar değiştiğinde toplam tutarı güncelleme
                numQuantity.ValueChanged += (sender, e) =>
                {
                    // İlgili ürünü bul ve miktarını güncelle
                    item.Miktar = (int)numQuantity.Value;
                    // Toplam tutarı yeniden hesapla
                    UpdateTotalAmount();

                    // Ürün adedini etiket üzerinde de güncelle
                    lblItem.Text = $"{item.Ad} - {item.Miktar} x {item.Fiyat:C2}";
                };

                itemPanel.Controls.Add(numQuantity);

                // Toplam tutarı hesapla
                toplamTutar += item.Fiyat * item.Miktar;

                // FlowLayoutPanel'e ekle
                flowLayoutPanelCart.Controls.Add(itemPanel);
            }

            // Toplam tutarı etiket olarak göstermek
            lblTotalAmount.Text = "Toplam Tutar: " + toplamTutar.ToString("C2");
        }

        // Toplam tutarı güncelleyen metod
        private void UpdateTotalAmount()
        {
            decimal toplamTutar = 0;
            foreach (var item in Items)
            {
                toplamTutar += item.Fiyat * item.Miktar;
            }
            lblTotalAmount.Text = "Toplam Tutar: " + toplamTutar.ToString("C2");
        }

        // Sepeti temizleme işlemi
        public void ClearCart()
        {
            Items.Clear();
            flowLayoutPanelCart.Controls.Clear(); // FlowLayoutPanel içeriğini temizle
            lblTotalAmount.Text = "Toplam Tutar: 0,00 TL";  // Toplam tutarı sıfırla
        }

        // Ödeme yap butonu tıklanıldığında
     
        private void btnSepet_Click_1(object sender, EventArgs e)
        {
            if (ShoppingCart.Items.Count == 0)
            {
                MessageBox.Show("Sepetinizde ürün bulunmamaktadır. Lütfen ürün ekleyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                this.Hide(); // Mevcut SepetForm'u gizle
                ShoppingCart shoppingForm = new ShoppingCart();
                shoppingForm.Show();
                return;


            }
            else
            {
                PaymentForm paymentForm = new PaymentForm();
                paymentForm.ShowDialog();
            }
            // Ödeme sonrası sepeti temizle
            ClearCart();
            this.Hide();
        }
    }

    // Sepet öğesi için basit bir sınıf
    public class CartItem
    {
        public int OgeID { get; set; }
        public string Ad { get; set; }
        public decimal Fiyat { get; set; }
        public int Miktar { get; set; }
        public string ResimYolu { get; set; }  // Resim yolu ekledik
    }
}
