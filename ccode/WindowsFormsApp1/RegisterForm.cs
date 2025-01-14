using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace evet
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
 
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
                    cmd.Parameters.AddWithValue("@rol", rol);

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
        }

        // Parolayı MD5 ile hash'leme işlemi
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

        // E-posta doğrulama
        private bool ValidateEmail(string email)
        {
            if (!email.Contains("@"))
            {
                MessageBox.Show("E-posta adresi '@' içermelidir.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        // Şifre doğrulama
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

        // Telefon numarası doğrulama
        private bool ValidatePhone(string phone)
        {
            if (!Regex.IsMatch(phone, "^0[0-9]{10}$"))
            {
                MessageBox.Show("Telefon numarası 0 ile başlamalı ve toplam 11 rakamdan oluşmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // LoginForm'u göster
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide(); // Form1'i gizle
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Kullanıcıdan alınan veriler
            string ad = txtAd.Text.Trim();
            string soyad = txtSoyad.Text.Trim();
            string eposta = txtEposta.Text.Trim().ToLower();
            string parola = txtParola.Text.Trim();
            string telefon = txtTelefon.Text.Trim();
            string adres = txtAdres.Text.Trim();

            // Boş alan kontrolü
            if (string.IsNullOrEmpty(ad))
            {
                MessageBox.Show("Ad alanı boş bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAd.Focus();
                return;
            }

            if (string.IsNullOrEmpty(soyad))
            {
                MessageBox.Show("Soyad alanı boş bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSoyad.Focus();
                return;
            }

            if (string.IsNullOrEmpty(eposta))
            {
                MessageBox.Show("E-posta alanı boş bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEposta.Focus();
                return;
            }

            if (string.IsNullOrEmpty(parola))
            {
                MessageBox.Show("Parola alanı boş bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtParola.Focus();
                return;
            }

            if (string.IsNullOrEmpty(telefon))
            {
                MessageBox.Show("Telefon alanı boş bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTelefon.Focus();
                return;
            }

            if (string.IsNullOrEmpty(adres))
            {
                MessageBox.Show("Adres alanı boş bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAdres.Focus();
                return;
            }

            // Alan doğrulamaları
            if (!ValidateEmail(eposta))
            {
                MessageBox.Show("E-posta adresi geçerli değil. '@' işareti içermelidir.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEposta.Focus();
                return;
            }

            if (!ValidatePassword(parola))
            {
                MessageBox.Show("Şifre en az 8 karakter uzunluğunda olmalı, bir büyük harf, bir küçük harf ve bir sayı içermelidir.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtParola.Focus();
                return;
            }

            if (!ValidatePhone(telefon))
            {
                MessageBox.Show("Telefon numarası 0 ile başlamalı ve toplam 11 rakamdan oluşmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTelefon.Focus();
                return;
            }

            // Parolayı hash'leme işlemi
            string hashedParola = HashParola(parola);

            // Veritabanına kayıt işlemi
            if (KayitOl(ad, soyad, eposta, hashedParola, telefon, adres, "Musteri"))
            {
                MessageBox.Show("Kayıt başarılı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
            }
            else
            {
                MessageBox.Show("Kayıt işlemi başarısız. Lütfen tekrar deneyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
