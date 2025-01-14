using WindowsFormsApp1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// Kullanıcı oturumu için CurrentUserID, Ad ve Soyad bilgilerini tutan SessionManager sınıfı

namespace evet
{

    public partial class PaymentForm : Form
    {
        public static class SessionManager
        {
            public static int CurrentUserID { get; set; }
            public static string CurrentUserAd { get; set; }
            public static string CurrentUserSoyad { get; set; }
        }

        public PaymentForm()
        {
            InitializeComponent();
        }
        // Sipariş veritabanına kaydedildiğinde kullanılacak metot
        private void SaveOrderToDatabase()
        {
            // Kullanıcı oturumu açılmamışsa hata mesajı ver
            if (SessionManager.CurrentUserID == 0)
            {
                MessageBox.Show("Lütfen önce giriş yapın!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Sepet boşsa, sipariş kaydedilemiyor
            if (ShoppingCart.Items.Count == 0)
            {
                MessageBox.Show("Sipariş listesi boş!");
                return;
            }

            // Veritabanı bağlantısı
            using (SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-K4MOT0FU\SQLEXPRESS;Initial Catalog=Proje1;Integrated Security=True"))
            {
                try
                {
                    conn.Open();
                    foreach (var item in ShoppingCart.Items)
                    {
                        // Sipariş verisini ekleme sorgusu
                        string query = @"INSERT INTO Siparisler1 
                                         (OgeID, Ad, Fiyat, Miktar, KullaniciID, SiparisTarihi) 
                                         VALUES (@OgeID, @Ad, @Fiyat, @Miktar, @KullaniciID, @SiparisTarihi)";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@OgeID", item.OgeID);
                            cmd.Parameters.AddWithValue("@Ad", item.Ad);
                            cmd.Parameters.AddWithValue("@Fiyat", item.Fiyat);
                            cmd.Parameters.AddWithValue("@Miktar", item.Miktar);
                            cmd.Parameters.AddWithValue("@KullaniciID", SessionManager.CurrentUserID); // Kullanıcı ID'si
                            cmd.Parameters.AddWithValue("@SiparisTarihi", DateTime.Now);

                            cmd.ExecuteNonQuery(); // Veritabanına kayıt
                        }
                    }
                    MessageBox.Show("Sipariş başarıyla kaydedildi!");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Veritabanı hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Ödeme butonuna tıklama işlemi
        private void btnPay_Click(object sender, EventArgs e)
        {
            // Kart bilgilerini al
            string kartNumarasi = txtCardNumber.Text;
            string sonKullanmaTarihi = txtExpiryDate.Text;
            string cvv = txtCVV.Text;

            // Eğer kart bilgileri eksikse uyarı ver
            if (string.IsNullOrWhiteSpace(kartNumarasi) || string.IsNullOrWhiteSpace(sonKullanmaTarihi) || string.IsNullOrWhiteSpace(cvv))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Eksik Alan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ödeme işlemi simülasyonu
            MessageBox.Show("Ödeme başarıyla alındı. Siparişiniz oluşturuldu!");

            // Siparişi veritabanına kaydet
            SaveOrderToDatabase();

            // Kullanıcı adı ve soyadını SessionManager'dan al
            string ad = SessionManager.CurrentUserAd;
            string soyad = SessionManager.CurrentUserSoyad;

            // Ana sayfa formunu oluştur ve ad soyad bilgilerini geç
            MusteriAnaSayfaForm musteriForm = new MusteriAnaSayfaForm(ad, soyad);

            // Yeni formu göster
            musteriForm.Show();

            // Şu anki formu kapat
            this.Hide(); // Bu, şu anki formu gizler ve açtığınız formu göstermeye devam eder.
        }

    }
}