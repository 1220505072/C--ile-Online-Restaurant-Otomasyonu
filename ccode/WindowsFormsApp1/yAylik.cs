using evet;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class yAylik : Form
    {
        // BindingSource kontrolünü manuel olarak tanımlıyoruz.
        private BindingSource bindingSource1 = new BindingSource();

        public yAylik()
        {
            InitializeComponent();
        }
        public static class SessionManager
        {
            public static int CurrentUserID { get; set; }
            public static string CurrentUserName { get; set; }
            public static string CurrentUserSurname { get; set; }
        }

        private string connectionString = @"Data Source=LAPTOP-K4MOT0FU\SQLEXPRESS;Initial Catalog=Proje1;Integrated Security=True";

        private void LoadOrderStatistics()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Aynı haftadaki tüm teslim edilen siparişlerin toplamını alacak SQL sorgusu
                    string query = "SELECT " +
               "CONCAT(YEAR(SiparisTarihi), '-', RIGHT('0' + CAST(MONTH(SiparisTarihi) AS VARCHAR(2)), 2)) AS SiparisYiliAy, " +  // Yıl-Ay formatında tarih
               "SUM(Miktar * Fiyat) AS TutarToplam " +
               "FROM Siparisler1 " +
               "WHERE Durum = 'Teslim Edildi' " +
               "GROUP BY YEAR(SiparisTarihi), MONTH(SiparisTarihi)";  // Yıl ve ay numarasına göre gruplama
                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Veri okuyucu ile sorguyu çalıştır
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Veri seti (DataTable) oluştur
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    // Veriyi binding source'a bağla
                    bindingSource1.DataSource = dt;

                    // DataGridView'ı bağla
                    dataGridView1.DataSource = bindingSource1;
                }
                catch (Exception ex)
                {
                    // Hata durumunda kullanıcıya bilgi ver
                    MessageBox.Show($"Hata: {ex.Message}", "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            YoneticiAnaSayfaForm y = new YoneticiAnaSayfaForm(SessionManager.CurrentUserName, SessionManager.CurrentUserSurname);
            y.Show();

            // Mevcut formu gizle (örneğin, menü ekleme formunu gizleme)
            this.Hide();
        }

        // Navigasyon butonları (örneğin, ileri, geri, ilk ve son kayda gitme işlemleri)
        private void btnFirst_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveFirst(); // İlk kayda git
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            bindingSource1.MovePrevious(); // Bir önceki kayda git
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveNext(); // Bir sonraki kayda git
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveLast(); // Son kayda git
        }

        private void yAylik_Load(object sender, EventArgs e)
        {
            LoadOrderStatistics();
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
