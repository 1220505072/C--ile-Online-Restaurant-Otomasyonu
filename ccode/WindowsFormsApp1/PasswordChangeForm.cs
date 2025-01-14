using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace evet
{
    public partial class PasswordChangeForm : Form
    {
        public PasswordChangeForm()
        {
            InitializeComponent();
        }
       

        // Veritabanı bağlantı dizesi
        string connectionString = @"Data Source=LAPTOP-K4MOT0FU\SQLEXPRESS;Initial Catalog=Proje1;Integrated Security=True";

        // "Şifreyi Güncelle" butonuna tıklama işlemi
        private void btnSifreGuncelle_Click(object sender, EventArgs e)
        {
            string eposta = txtEposta.Text.Trim(); // E-posta adresini TextBox'tan alıyoruz
            string eskiSifre = txtEskiSifre.Text.Trim();
            string yeniSifre = txtYeniSifre.Text.Trim();
            string yeniSifreTekrar = txtYeniSifreTekrar.Text.Trim();

            // Alanların boş olmaması kontrolü
            if (string.IsNullOrEmpty(eposta) || string.IsNullOrEmpty(eskiSifre) || string.IsNullOrEmpty(yeniSifre) || string.IsNullOrEmpty(yeniSifreTekrar))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Yeni şifrelerin eşleşme kontrolü
            if (yeniSifre != yeniSifreTekrar)
            {
                MessageBox.Show("Yeni şifreler uyuşmuyor.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Yeni şifre doğrulama
            if (!ValidatePassword(yeniSifre))
            {
                return; // Şifre kriterleri sağlanmadıysa işlem durdurulur
            }

            // Şifre güncelleme işlemi
            if (SifreGuncelle(eposta, eskiSifre, yeniSifre))
            {
                MessageBox.Show("Şifre başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Formu kapat
            }
            else
            {
                MessageBox.Show("Eski şifre hatalı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Şifre güncelleme metodu
        private bool SifreGuncelle(string eposta, string eskiSifre, string yeniSifre)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Eski şifreyi kontrol et
                    string selectQuery = "SELECT Parola FROM Kullanicilar WHERE Eposta = @eposta";
                    SqlCommand selectCmd = new SqlCommand(selectQuery, connection);
                    selectCmd.Parameters.AddWithValue("@eposta", eposta);

                    var storedPassword = selectCmd.ExecuteScalar();

                    if (storedPassword == null)
                        return false; // Kullanıcı bulunamadı

                    // Eski şifre doğrulama
                    string hashedEskiSifre = HashParola(eskiSifre);
                    if (storedPassword.ToString() != hashedEskiSifre)
                        return false; // Eski şifre hatalı

                    // Yeni şifreyi hash'le
                    string hashedYeniSifre = HashParola(yeniSifre);

                    // Yeni şifreyi güncelle
                    string updateQuery = "UPDATE Kullanicilar SET Parola = @yeniParola WHERE Eposta = @eposta";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, connection);
                    updateCmd.Parameters.AddWithValue("@yeniParola", hashedYeniSifre);
                    updateCmd.Parameters.AddWithValue("@eposta", eposta);

                    updateCmd.ExecuteNonQuery();
                    return true; // Güncelleme başarılı
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}", "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false; // Hata meydana geldi
                }
            }
        }

        // Şifre hashleme metodu
        private string HashParola(string parola)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(parola);
                byte[] hashBytes = md5.ComputeHash(bytes);

                StringBuilder hashString = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashString.Append(b.ToString("x2"));
                }

                return hashString.ToString();
            }
        }

        // Şifre doğrulama metodu
        private bool ValidatePassword(string password)
        {
            if (password.Length < 8 ||
                !Regex.IsMatch(password, "[A-Z]") ||
                !Regex.IsMatch(password, "[a-z]") ||
                !Regex.IsMatch(password, "[0-9]"))
            {
                MessageBox.Show("Şifre en az 8 karakter uzunluğunda olmalı, bir büyük harf, bir küçük harf ve bir sayı içermelidir.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
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
