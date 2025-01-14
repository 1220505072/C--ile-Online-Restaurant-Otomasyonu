using evet;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static evet.PaymentForm;

namespace WindowsFormsApp1
{
    public partial class MenuVeriEkle : Form
    {
        public MenuVeriEkle()
        {
            InitializeComponent();
        }
        public static class SessionManager
        {
            // Oturumdaki kullanıcının ID'si
            public static int CurrentUserID { get; set; }

            // Oturumdaki kullanıcının adı
            public static string CurrentUserName { get; set; }

            // Oturumdaki kullanıcının soyadı
            public static string CurrentUserSurname { get; set; }
        }

        private void btnMenuEkle_Click(object sender, EventArgs e)
        {
            // TextBox'lardan verileri alıyoruz
            string ad = txtAd.Text; // Ad TextBox'ı
            string aciklama = txtAciklama.Text; // Açıklama TextBox'ı

            // Fiyatı NumericUpDown'dan alıyoruz
            decimal fiyat = numericFiyat.Value; // NumericFiyat NumericUpDown'dan alınan değer

            // ComboBox'tan kategori değerini alıyoruz
            string kategori = cmbKategori.SelectedItem?.ToString(); // ComboBox'tan seçilen kategori

            // Eğer kategori seçilmemişse hata mesajı gösterelim
            if (string.IsNullOrEmpty(kategori))
            {
                MessageBox.Show("Kategori seçmelisiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Resim yolu
            string resimYolu = txtResimYolu.Text; // Resim yolu TextBox'ı
            DateTime eklenmeTarihi = DateTime.Now; // Eklenme tarihi olarak şimdiki zaman

            // Veritabanına bağlanıp ekleme işlemini gerçekleştirelim
            using (SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-K4MOT0FU\SQLEXPRESS;Initial Catalog=Proje1;Integrated Security=True"))
            {
                try
                {
                    conn.Open();

                    // Veritabanına ekleme işlemi için SQL sorgusunu yazıyoruz
                    string query = "INSERT INTO MenuOgeleri (Ad, Açıklama, Fiyat, Kategori, EklenmeTarihi, ResimYolu) " +
                                   "VALUES (@Ad, @Aciklama, @Fiyat, @Kategori, @EklenmeTarihi, @ResimYolu)";

                    // Komut objesi oluşturuyoruz
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Parametreleri ekliyoruz
                        cmd.Parameters.AddWithValue("@Ad", ad);
                        cmd.Parameters.AddWithValue("@Aciklama", aciklama);
                        cmd.Parameters.AddWithValue("@Fiyat", fiyat); // Fiyatı decimal olarak ekliyoruz
                        cmd.Parameters.AddWithValue("@Kategori", kategori); // ComboBox'tan alınan kategori
                        cmd.Parameters.AddWithValue("@EklenmeTarihi", eklenmeTarihi);
                        cmd.Parameters.AddWithValue("@ResimYolu", resimYolu);

                        // Komutu çalıştırarak verileri ekliyoruz
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Menü öğesi başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Hata durumunda kullanıcıya bilgi veriyoruz
                    MessageBox.Show($"Hata: {ex.Message}", "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Menü ekleme işlemi sonrası, oturumdaki kullanıcı bilgilerini kullanarak YoneticiAnaSayfaForm'u aç
            YoneticiAnaSayfaForm y = new YoneticiAnaSayfaForm(SessionManager.CurrentUserName, SessionManager.CurrentUserSurname);
            y.Show();

            // Mevcut formu gizle (örneğin, menü ekleme formunu gizleme)
            this.Hide();
        }

        private void MenuVeriEkle_Load(object sender, EventArgs e)
        {
            // ComboBox'a kategorileri ekleyelim
            cmbKategori.Items.Add("yemek");
            cmbKategori.Items.Add("icecek");
            cmbKategori.Items.Add("tatli");
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            // LoginForm'u yeni bir pencere olarak açıyoruz.
            LoginForm loginForm = new LoginForm();
            loginForm.Show();  // Login formunu aç
            this.Hide();  // Bu formu gizle (isteğe bağlı)
        }
    }
}
