   using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApp1;
using static evet.PaymentForm;

namespace evet
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        // Veritabanı bağlantı dizesi
        string connectionString = @"Data Source=LAPTOP-K4MOT0FU\SQLEXPRESS;Initial Catalog=Proje1;Integrated Security=True";

        // Giriş butonuna tıklama işlemi
        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            string eposta = txtEposta.Text.Trim().ToLower(); // E-posta küçük harf ile alınır
            string parola = txtParola.Text.Trim(); // Parola alınır

            // E-posta veya parola boş olmamalı
            if (string.IsNullOrEmpty(eposta))
            {
                MessageBox.Show("E-posta alanı boş olamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(parola))
            {
                MessageBox.Show("Parola alanı boş olamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Giriş işlemini başlat
            var (girisBasarili, ad, soyad) = GirisYap(eposta, parola);

            if (girisBasarili)
            {
                string rol = GetKullaniciRol(eposta);

                if (rol == "Musteri")
                {
                    // Ad ve soyad bilgileriyle müşterinin ana sayfasını aç
                    MusteriAnaSayfaForm musteriForm = new MusteriAnaSayfaForm(ad, soyad);
                    musteriForm.Show();
                }
                else if (rol == "Calisan")
                {
                    CalisanAnaSayfaForm calisanForm = new CalisanAnaSayfaForm(ad, soyad);
                    calisanForm.Show();
                }
                else if (rol == "Yonetici")
                {
                    YoneticiAnaSayfaForm yoneticiForm = new YoneticiAnaSayfaForm(ad, soyad);
                    yoneticiForm.Show();
                }

                this.Hide(); // Login formunu gizle
            }
        }

        // Kullanıcının giriş bilgilerini doğrulayan metot
        // Kullanıcının giriş bilgilerini doğrulayan metot
        // Kullanıcının giriş bilgilerini doğrulayan metot
        private (bool, string, string) GirisYap(string eposta, string parola)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); // Bağlantıyı aç

                    // Kullanıcı bilgilerini çekmek için sorgu
                    string query = "SELECT KullaniciID, Parola, Ad, Soyad FROM Kullanicilar WHERE Eposta = @eposta";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@eposta", eposta);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read()) // Veritabanında kullanıcı bulundu mu?
                    {
                        int kullaniciID = Convert.ToInt32(reader["KullaniciID"]);
                        string storedPassword = reader["Parola"].ToString();
                        string ad = reader["Ad"].ToString();
                        string soyad = reader["Soyad"].ToString();

                        // Kullanıcının girdiği parola hash'lenip doğrulanıyor
                        string hashedParola = HashParola(parola);

                        if (storedPassword == hashedParola) // Parola doğru mu?
                        {
                            // Kullanıcı bilgilerini SessionManager'a kaydediyoruz
                            SessionManager.CurrentUserID = kullaniciID;
                            SessionManager.CurrentUserAd = ad;  // Adı sakla
                            SessionManager.CurrentUserSoyad = soyad;  // Soyadı sakla
                            return (true, ad, soyad); // Giriş başarılı
                        }
                    }

                    MessageBox.Show("E-posta veya parola hatalı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return (false, null, null); // Giriş başarısız
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}", "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return (false, null, null); // Hata meydana geldi
                }
            }
        }



        // Kullanıcının rolünü veritabanından çekmek için metot
        private string GetKullaniciRol(string eposta)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT Rol FROM Kullanicilar WHERE Eposta = @eposta";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@eposta", eposta);

                    var rol = cmd.ExecuteScalar(); // Kullanıcının rolünü al

                    if (rol != null)
                    {
                        return rol.ToString(); // Rolü döndür
                    }
                    else
                    {
                        return null; // Rol bulunamadı
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}", "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null; // Hata meydana geldi
                }
            }
        }

        // Parolayı MD5 ile hash'leme işlemi
        private string HashParola(string parola)
        {
            using (MD5 md5 = MD5.Create()) // MD5 hash algoritması ile parola şifrelemesi yapılır
            {
                byte[] bytes = Encoding.UTF8.GetBytes(parola); // Parolayı byte dizisine dönüştür
                byte[] hashBytes = md5.ComputeHash(bytes); // Hash'le

                StringBuilder hashString = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashString.Append(b.ToString("x2")); // Hash'li değeri hexadecimal formatında ekle
                }

                return hashString.ToString(); // Hash'li şifreyi döndür
            }
        }

        // Eğer kullanıcı "Kayıt Ol" formuna gitmek isterse
        private void btnKayitOl_Click(object sender, EventArgs e)
        {
            this.Hide(); // Login formunu gizle
            RegisterForm registerForm = new RegisterForm(); // Kayıt formunu oluştur
            registerForm.Show(); // Kayıt formunu göster
        }

        private void lnkSifremiUnuttum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PasswordChangeForm resetForm = new PasswordChangeForm(); // Şifre sıfırlama formu oluştur
            resetForm.Show(); // Formu göster
            this.Hide(); // LoginForm'u gizle
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Hide(); // Form1'i gizle
        }
    }
}