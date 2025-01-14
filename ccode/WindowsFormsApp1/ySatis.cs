using evet;
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

namespace WindowsFormsApp1
{
    public partial class ySatis : Form
    {
        // BindingSource kontrolünü manuel olarak tanımlıyoruz.
        private BindingSource bindingSource1 = new BindingSource();
        public ySatis()
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
        private void LoadTopSellingProducts()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // En çok satılan ürünleri listeleyecek SQL sorgusu
                    string query = @"
            SELECT TOP 5
                Siparisler1.Ad AS UrunAd, 
                SUM(Siparisler1.Miktar) AS ToplamSatilanMiktar
            FROM 
                Siparisler1
            WHERE 
                Siparisler1.Durum = 'Teslim Edildi'
            GROUP BY 
                Siparisler1.Ad
            ORDER BY 
                ToplamSatilanMiktar DESC";

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


  
        private void ySatis_Load(object sender, EventArgs e)
        {
            LoadTopSellingProducts();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            // LoginForm'u yeni bir pencere olarak açıyoruz.
            LoginForm loginForm = new LoginForm();
            loginForm.Show();  // Login formunu aç
            this.Hide();  // Bu formu gizle (isteğe bağlı)
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
