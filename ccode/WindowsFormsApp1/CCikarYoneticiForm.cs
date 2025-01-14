using evet;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class CCikarYoneticiForm : Form
    {
        public CCikarYoneticiForm()
        {
            InitializeComponent();
        }
        public static class SessionManager
        {
            public static int CurrentUserID { get; set; }
            public static string CurrentUserName { get; set; }
            public static string CurrentUserSurname { get; set; }
        }
        // Veritabanı bağlantı dizesi
        string connectionString = @"Data Source=LAPTOP-K4MOT0FU\SQLEXPRESS;Initial Catalog=Proje1;Integrated Security=True";

        // Kullanıcıyı sadece e-posta ile silme işlemi
        private bool KullaniciSil(string eposta)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); // Veritabanı bağlantısını aç

                    // Kullanıcıyı silme sorgusu
                    string query = "DELETE FROM Kullanicilar WHERE LOWER(Eposta) = LOWER(@eposta)";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@eposta", eposta); // E-posta parametresi

                    int rowsAffected = cmd.ExecuteNonQuery(); // Satır etkilenmesini kontrol et
                    if (rowsAffected > 0)
                    {
                        return true; // Silme başarılı
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı bulunamadı veya yanlış e-posta girdiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnSil_Click(object sender, EventArgs e)
        {
            string eposta = txtEposta.Text.Trim().ToLower(); // E-posta küçük harf ile kaydedilir

            // E-posta formatını kontrol et
            if (string.IsNullOrEmpty(eposta) || !eposta.Contains("@"))
            {
                MessageBox.Show("Geçersiz e-posta adresi!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (KullaniciSil(eposta))
            {
                MessageBox.Show("Kullanıcı silindi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Silme işlemi başarısız. Lütfen tekrar deneyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            YoneticiAnaSayfaForm y = new YoneticiAnaSayfaForm(SessionManager.CurrentUserName, SessionManager.CurrentUserSurname);
            y.Show();

            // Mevcut formu gizle (örneğin, menü ekleme formunu gizleme)
            this.Hide();
         
        }
    }
}
