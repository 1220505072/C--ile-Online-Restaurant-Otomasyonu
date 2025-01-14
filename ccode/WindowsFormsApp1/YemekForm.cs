using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace evet
{
    public partial class YemekForm : Form
    {
        public class Produkt
        {
            public int OgeID { get; set; }
            public string Ad { get; set; }
            public decimal Fiyat { get; set; }
            public string Aciklama { get; set; }  // Açıklama sütunu
            public string ResimYolu { get; set; } // Resim yolu
            public DateTime? EklenmeTarihi { get; set; }  // Eklenme tarihi
        }

        private string kategori;
        private List<Produkt> urunler; // Ürünlerin listesi
        private List<Produkt> filteredUrunler; // Filtrelenmiş ürünler

        // Veritabanı bağlantı dizesi
        private string connectionString = @"Data Source=LAPTOP-K4MOT0FU\SQLEXPRESS;Initial Catalog=Proje1;Integrated Security=True";

        public YemekForm(string kategori)
        {
            InitializeComponent();
            this.kategori = kategori;
            urunler = new List<Produkt>();
            filteredUrunler = new List<Produkt>();
            LoadProducts();
            InitializeControls();

            // Set the FlowLayoutPanel settings for 3 items per row
            flowLayoutPanelProducts.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanelProducts.WrapContents = true;
            flowLayoutPanelProducts.AutoScroll = true;
            flowLayoutPanelProducts.Padding = new Padding(10); // Add padding around the panel
        }

        // Ürünleri veritabanından çekme
        private void LoadProducts()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT OgeID, Ad, Fiyat, Açıklama, ResimYolu, EklenmeTarihi FROM MenuOgeleri WHERE Kategori = @Kategori";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@Kategori", kategori);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    urunler.Add(new Produkt
                    {
                        OgeID = Convert.ToInt32(row["OgeID"]),
                        Ad = row["Ad"].ToString(),
                        Fiyat = Convert.ToDecimal(row["Fiyat"]),
                        Aciklama = row["Açıklama"].ToString(),
                        ResimYolu = row["ResimYolu"].ToString(),
                        EklenmeTarihi = row["EklenmeTarihi"] != DBNull.Value ? Convert.ToDateTime(row["EklenmeTarihi"]) : (DateTime?)null
                    });
                }
            }

            filteredUrunler = urunler.ToList(); // Initially show all products
            DisplayProducts(filteredUrunler);
        }

        // Ürünleri FlowLayoutPanel'de listele
        private void DisplayProducts(List<Produkt> productsToDisplay)
        {
            flowLayoutPanelProducts.Controls.Clear();

            foreach (var urun in productsToDisplay)
            {
                Panel productPanel = new Panel
                {
                    Width = 250, // Adjust width to fit 3 items in a row
                    Height = 350, // Increased height to accommodate button and other controls
                    BorderStyle = BorderStyle.FixedSingle
                };

                // Ürün adı
                Label lblName = new Label
                {
                    Text = urun.Ad,
                    Location = new Point(10, 10),
                    AutoSize = true
                };
                productPanel.Controls.Add(lblName);

                // Ürün fiyatı
                Label lblPrice = new Label
                {
                    Text = "Fiyat: " + urun.Fiyat.ToString("C2"),
                    Location = new Point(10, 40),
                    AutoSize = true
                };
                productPanel.Controls.Add(lblPrice);

                // Ürün açıklaması
                Label lblDescription = new Label
                {
                    Text = urun.Aciklama ?? "Açıklama yok.",
                    Location = new Point(10, 70),
                    AutoSize = true
                };
                productPanel.Controls.Add(lblDescription);

                // Ürün resmi
                if (!string.IsNullOrEmpty(urun.ResimYolu) && System.IO.File.Exists(urun.ResimYolu))
                {
                    PictureBox picBox = new PictureBox
                    {
                        Image = Image.FromFile(urun.ResimYolu),
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Location = new Point(10, 100),
                        Size = new Size(80, 80) // Adjust size for image
                    };
                    productPanel.Controls.Add(picBox);
                }

                // Ürün miktarı seçme
                NumericUpDown numQuantity = new NumericUpDown
                {
                    Location = new Point(10, 190),
                    Minimum = 1,
                    Maximum = 100,
                    Size = new Size(80, 20)
                };
                productPanel.Controls.Add(numQuantity);

                // Sepete ekle butonu
                Button btnAddToCart = new Button
                {
                    Text = "Sepete Ekle",
                    Location = new Point(100, 190),
                    Size = new Size(130, 30)
                };
                btnAddToCart.Click += (sender, e) => AddToCart(urun, (int)numQuantity.Value);
                productPanel.Controls.Add(btnAddToCart);

                flowLayoutPanelProducts.Controls.Add(productPanel);
                flowLayoutPanelProducts.Controls.Add(groupBox1);
              


            }
        }
        // Fiyat aralığına göre filtreleme ve sıralama
        private void FilterAndSortProducts()
        {
            decimal minPrice = numericUpDownMinPrice.Value;
            decimal maxPrice = numericUpDownMaxPrice.Value;
            string sortOption = comboBoxSortPrice.SelectedIndex.ToString();  // 0: Artan, 1: Azalan

            // SQL sorgusu oluşturuluyor
            string query = "SELECT OgeID, Ad, Fiyat, Açıklama, ResimYolu, EklenmeTarihi " +
                           "FROM MenuOgeleri " +
                           "WHERE Kategori = @Kategori " +
                           "AND Fiyat BETWEEN @MinPrice AND @MaxPrice ";

            // Fiyat sıralama ekleniyor
            if (sortOption == "0")
            {
                query += "ORDER BY Fiyat ASC";
            }
            else if (sortOption == "1")
            {
                query += "ORDER BY Fiyat DESC";
            }

            // Veritabanından ürünleri alıyoruz
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@Kategori", kategori);
                adapter.SelectCommand.Parameters.AddWithValue("@MinPrice", minPrice);
                adapter.SelectCommand.Parameters.AddWithValue("@MaxPrice", maxPrice);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                filteredUrunler.Clear(); // Listeyi temizle
                foreach (DataRow row in dt.Rows)
                {
                    filteredUrunler.Add(new Produkt
                    {
                        OgeID = Convert.ToInt32(row["OgeID"]),
                        Ad = row["Ad"].ToString(),
                        Fiyat = Convert.ToDecimal(row["Fiyat"]),
                        Aciklama = row["Açıklama"].ToString(),
                        ResimYolu = row["ResimYolu"].ToString(),
                        EklenmeTarihi = row["EklenmeTarihi"] != DBNull.Value ? Convert.ToDateTime(row["EklenmeTarihi"]) : (DateTime?)null
                    });
                }
            }

            // Filtrelenmiş ürünleri ekrana yazdır
            DisplayProducts(filteredUrunler);
        }

        private void AddToCart(Produkt urun, int miktar)
        {
            ShoppingCart.Items.Add(new CartItem
            {
                OgeID = urun.OgeID,
                Ad = urun.Ad,
                Fiyat = urun.Fiyat,
                Miktar = miktar,
                ResimYolu = urun.ResimYolu
            });

            MessageBox.Show($"{urun.Ad} sepete eklendi!");
        }

        // Arama fonksiyonu
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.ToLower();
            var filteredProducts = urunler.Where(p => p.Ad.ToLower().Contains(searchTerm)).ToList();
            DisplayProducts(filteredProducts);
        }

        // Fiyat aralığına göre filtreleme
        private void NumericUpDownMinPrice_ValueChanged(object sender, EventArgs e)
        {
            FilterByPrice();
        }

        private void NumericUpDownMaxPrice_ValueChanged(object sender, EventArgs e)
        {
            FilterByPrice();
        }

        private void FilterByPrice()
        {
            decimal minPrice = numericUpDownMinPrice.Value;
            decimal maxPrice = numericUpDownMaxPrice.Value;

            // Fiyatları seçilen aralığa göre filtrele
            var filteredProducts = urunler.Where(p => p.Fiyat >= minPrice && p.Fiyat <= maxPrice).ToList();
            DisplayProducts(filteredProducts);
        }


        private void InitializeControls()
        {
            // Arama TextBox
            txtSearch.TextChanged += TxtSearch_TextChanged;

            // Fiyat aralığı NumericUpDown
            numericUpDownMinPrice.ValueChanged += NumericUpDownMinPrice_ValueChanged;
            numericUpDownMaxPrice.ValueChanged += NumericUpDownMaxPrice_ValueChanged;

            // Fiyat sıralama ComboBox
            comboBoxSortPrice.Items.Add("Artan Fiyat");   // Artan fiyat
            comboBoxSortPrice.Items.Add("Azalan Fiyat");  // Azalan fiyat
            comboBoxSortPrice.SelectedIndex = 0;  // Varsayılan olarak Artan Fiyat seçili
            comboBoxSortPrice.SelectedIndexChanged += ComboBoxSortPrice_SelectedIndexChanged;
        }


        private void BtnGoBack_Click_1(object sender, EventArgs e)
        {
            MenuForm menuForm = new MenuForm();
            menuForm.Show();
            this.Hide();
        }

        private void BtnGoToCart_Click(object sender, EventArgs e)
        {
            // ShoppingCart formunu aç
            ShoppingCart cartForm = new ShoppingCart();
            cartForm.Show();
            this.Hide();
        }
        private void ComboBoxSortPrice_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ComboBox'tan seçilen sıralama türü
            string sortOption = comboBoxSortPrice.SelectedItem.ToString();

            // Veritabanı sorgusunu oluştur
            string query = "SELECT OgeID, Ad, Fiyat, Açıklama, ResimYolu, EklenmeTarihi FROM MenuOgeleri WHERE Kategori = @Kategori";

            // Artan fiyat sıralaması
            if (sortOption == "Artan Fiyat")
            {
                query += " ORDER BY Fiyat ASC"; // Artan sıralama
            }
            // Azalan fiyat sıralaması
            else if (sortOption == "Azalan Fiyat")
            {
                query += " ORDER BY Fiyat DESC"; // Azalan sıralama
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@Kategori", kategori);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                filteredUrunler.Clear(); // Önceki filtreyi temizle
                foreach (DataRow row in dt.Rows)
                {
                    filteredUrunler.Add(new Produkt
                    {
                        OgeID = Convert.ToInt32(row["OgeID"]),
                        Ad = row["Ad"].ToString(),
                        Fiyat = Convert.ToDecimal(row["Fiyat"]),
                        Aciklama = row["Açıklama"].ToString(),
                        ResimYolu = row["ResimYolu"].ToString(),
                        EklenmeTarihi = row["EklenmeTarihi"] != DBNull.Value ? Convert.ToDateTime(row["EklenmeTarihi"]) : (DateTime?)null
                    });
                }
            }

            // Filtrelenmiş ürünleri ekrana yazdır
            DisplayProducts(filteredUrunler);
        }


        private void BtnApplyPriceFilter_Click(object sender, EventArgs e)
        {
            // Use the FilterAndSortProducts method to filter and sort
            FilterAndSortProducts();
        }
       
        

        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.ToLower();
            string query = "SELECT OgeID, Ad, Fiyat, Açıklama, ResimYolu, EklenmeTarihi FROM MenuOgeleri WHERE Kategori = @Kategori AND Ad LIKE @SearchTerm";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@Kategori", kategori);

                // Parametreyi LIKE operatörü ile doğru formatta kullanmak için "%" ekliyoruz.
                adapter.SelectCommand.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                filteredUrunler.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    filteredUrunler.Add(new Produkt
                    {
                        OgeID = Convert.ToInt32(row["OgeID"]),
                        Ad = row["Ad"].ToString(),
                        Fiyat = Convert.ToDecimal(row["Fiyat"]),
                        Aciklama = row["Açıklama"].ToString(),
                        ResimYolu = row["ResimYolu"].ToString(),
                        EklenmeTarihi = row["EklenmeTarihi"] != DBNull.Value ? Convert.ToDateTime(row["EklenmeTarihi"]) : (DateTime?)null
                    });
                }
            }

            // Filtrelenmiş ürünleri ekrana yazdır
            DisplayProducts(filteredUrunler);
        }
    }
}

