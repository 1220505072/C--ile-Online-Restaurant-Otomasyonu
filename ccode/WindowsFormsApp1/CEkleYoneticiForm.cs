using evet;
using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class CEkleYoneticiForm : Form
    {
        public CEkleYoneticiForm()
        {
            InitializeComponent();
            // ComboBox'a seçenekler ekleniyor
            comboBox1.Items.Add("Calisan");
            comboBox1.Items.Add("Yonetici");
            comboBox1.SelectedIndex = 0; // Varsayılan olarak ilk seçenek seçili
        }
        public static class SessionManager
        {
            public static int CurrentUserID { get; set; }
            public static string CurrentUserName { get; set; }
            public static string CurrentUserSurname { get; set; }
        }
        // Veritabanı bağlantı dizesi
        string connectionString = @"Data Source=LAPTOP-K4MOT0FU\SQLEXPRESS;Initial Catalog=Proje1;Integrated Security=True";

        // Kayıt işlemi
        private bool KayitOl(string ad, string soyad, string eposta, string parola, string telefon, string adres, string rol)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); // Veritabanı bağlantısını aç

                    // E-posta adresinin veritabanında olup olmadığını kontrol et
                    string checkEmailQuery = "SELECT COUNT(*) FROM Kullanicilar WHERE Eposta = @eposta";
                    SqlCommand checkEmailCmd = new SqlCommand(checkEmailQuery, connection);
                    checkEmailCmd.Parameters.AddWithValue("@eposta", eposta);

                    int emailCount = (int)checkEmailCmd.ExecuteScalar();
                    if (emailCount > 0)
                    {
                        MessageBox.Show("Bu e-posta adresi zaten kayıtlı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false; // E-posta zaten kayıtlı, yeni kayıt yapılmasın
                    }

                    // Kullanıcıyı veritabanına ekleme
                    string query = "INSERT INTO Kullanicilar (Ad, Soyad, Eposta, Parola, TelefonNo, Adres, Rol) " +
                                   "VALUES (@ad, @soyad, @eposta, @parola, @telefon, @adres, @rol)";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@ad", ad);
                    cmd.Parameters.AddWithValue("@soyad", soyad);
                    cmd.Parameters.AddWithValue("@eposta", eposta);
                    cmd.Parameters.AddWithValue("@parola", parola);
                    cmd.Parameters.AddWithValue("@telefon", telefon);
                    cmd.Parameters.AddWithValue("@adres", adres);
                    cmd.Parameters.AddWithValue("@rol", rol);  // Rol parametresi

                    int rowsAffected = cmd.ExecuteNonQuery(); // Satır etkilenmesini kontrol et
                    if (rowsAffected > 0)
                    {
                        return true; // Kayıt başarılı
                    }
                    else
                    {
                        MessageBox.Show("Kayıt işlemi başarısız. Lütfen tekrar deneyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                catch (SqlException ex)
                {
                    // SQL hataları için detaylı hata mesajı
                    MessageBox.Show($"Veritabanı hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                catch (Exception ex)
                {
                    // Diğer hatalar için genel bir hata mesajı
                    MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            YoneticiAnaSayfaForm y = new YoneticiAnaSayfaForm(SessionManager.CurrentUserName, SessionManager.CurrentUserSurname);
            y.Show();

            // Mevcut formu gizle (örneğin, menü ekleme formunu gizleme)
            this.Hide();
        }

        // Parolayı MD5 ile hash'leme işlemi
        private string HashParola(string parola)
        {
            using (MD5 md5 = MD5.Create())
            {
                // Parolayı byte dizisine dönüştür
                byte[] bytes = Encoding.UTF8.GetBytes(parola);

                // Hash'le
                byte[] hashBytes = md5.ComputeHash(bytes);

                // Hash'li sonucu string'e dönüştür
                StringBuilder hashString = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashString.Append(b.ToString("x2"));
                }

                return hashString.ToString(); // Hashlenmiş şifreyi döndürüyoruz
            }
        }

        // E-posta doğrulama metodu
        private bool ValidateEmail(string email)
        {
            if (!email.Contains("@"))
            {
                MessageBox.Show("Geçerli bir e-posta adresi giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        // Şifre doğrulama metodu
        private bool ValidatePassword(string password)
        {
            if (password.Length < 8 || !Regex.IsMatch(password, "[A-Z]") || !Regex.IsMatch(password, "[a-z]") || !Regex.IsMatch(password, "[0-9]"))
            {
                MessageBox.Show("Şifre en az 8 karakter uzunluğunda, bir büyük harf, bir küçük harf ve bir sayı içermelidir.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        // Telefon doğrulama metodu
        private bool ValidatePhone(string phone)
        {
            if (!Regex.IsMatch(phone, "^0[0-9]{10}$"))
            {
                MessageBox.Show("Telefon numarası 0 ile başlamalı ve toplam 11 rakamdan oluşmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btnKayitOl_Click(object sender, EventArgs e)
        {
            string ad = txtAd.Text.Trim();
            string soyad = txtSoyad.Text.Trim();
            string eposta = txtEposta.Text.Trim().ToLower(); // E-posta küçük harf ile kaydedilir
            string parola = txtParola.Text.Trim();
            string telefon = txtTelefon.Text.Trim();
            string adres = txtAdres.Text.Trim();

            // ComboBox'tan seçilen rolü al
            string rol = comboBox1.SelectedItem.ToString();

            // Doğrulama işlemleri
            if (!ValidateEmail(eposta) || !ValidatePassword(parola) || !ValidatePhone(telefon))
            {
                return; // Doğrulama başarısızsa işlemi durdur
            }

            // Parolayı hash'leme işlemi
            string hashedParola = HashParola(parola);

            if (KayitOl(ad, soyad, eposta, hashedParola, telefon, adres, rol))
            {
                MessageBox.Show("Kayıt başarılı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide(); // Kayıt formunu gizle
                LoginForm loginForm = new LoginForm(); // Login formunu oluştur
                loginForm.Show(); // Login formunu göster
            }
            else
            {
                MessageBox.Show("Kayıt işlemi başarısız. Lütfen tekrar deneyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            YoneticiAnaSayfaForm y = new YoneticiAnaSayfaForm(ad, soyad);
            y.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {


            {
                YoneticiAnaSayfaForm y = new YoneticiAnaSayfaForm(SessionManager.CurrentUserName, SessionManager.CurrentUserSurname);
                y.Show();

                // Mevcut formu gizle (örneğin, menü ekleme formunu gizleme)
                this.Hide();

            }
        }
    }
}
