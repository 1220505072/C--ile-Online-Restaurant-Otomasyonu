using evet;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class MenuVeriGuncelle : Form
    {
        public MenuVeriGuncelle()
        {
            InitializeComponent();
        }

        public static class SessionManager
        {
            public static int CurrentUserID { get; set; }
            public static string CurrentUserName { get; set; }
            public static string CurrentUserSurname { get; set; }
        }

        private void MenuVeriGuncelle_Load(object sender, EventArgs e)
        {
            LoadMenuItems();  // Menü öğelerini yükle
            dgvMenuItems.CellValueChanged += dgvMenuItems_CellValueChanged;  // CellValueChanged olayını bağla
        }


        private void LoadMenuItems()
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-K4MOT0FU\SQLEXPRESS;Initial Catalog=Proje1;Integrated Security=True"))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT OgeID, Ad, Açıklama, Fiyat, Kategori, EklenmeTarihi FROM MenuOgeleri";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    dgvMenuItems.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}", "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
          
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            // DataGridView'deki her bir satır için güncellemeleri veritabanına yansıtıyoruz
            foreach (DataGridViewRow row in dgvMenuItems.Rows)
            {
                if (row.IsNewRow) continue; // Yeni satırları atla

                int ogeID = Convert.ToInt32(row.Cells["OgeID"].Value);
                string ad = row.Cells["Ad"].Value.ToString();
                string aciklama = row.Cells["Açıklama"].Value.ToString();
                decimal fiyat = Convert.ToDecimal(row.Cells["Fiyat"].Value);
                string kategori = row.Cells["Kategori"].Value.ToString();

                UpdateMenuItem(ogeID, ad, aciklama, fiyat, kategori);
            }
        }
        private void dgvMenuItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Eğer yeni satır ise ya da hücre indexi geçersizse, işlem yapma
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // Değiştirilen hücredeki veriyi alalım
            int ogeID = Convert.ToInt32(dgvMenuItems.Rows[e.RowIndex].Cells["OgeID"].Value);
            string ad = dgvMenuItems.Rows[e.RowIndex].Cells["Ad"].Value.ToString();
            string aciklama = dgvMenuItems.Rows[e.RowIndex].Cells["Açıklama"].Value.ToString();
            decimal fiyat = Convert.ToDecimal(dgvMenuItems.Rows[e.RowIndex].Cells["Fiyat"].Value);
            string kategori = dgvMenuItems.Rows[e.RowIndex].Cells["Kategori"].Value.ToString();

            // Veritabanında güncelleme işlemini gerçekleştirelim
            UpdateMenuItem(ogeID, ad, aciklama, fiyat, kategori);
        }

        private void UpdateMenuItem(int ogeID, string ad, string aciklama, decimal fiyat, string kategori)
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-K4MOT0FU\SQLEXPRESS;Initial Catalog=Proje1;Integrated Security=True"))
            {
                try
                {
                    conn.Open();

                    string query = "UPDATE MenuOgeleri SET Ad = @Ad, Açıklama = @Aciklama, Fiyat = @Fiyat, Kategori = @Kategori WHERE OgeID = @OgeID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@OgeID", ogeID);
                        cmd.Parameters.AddWithValue("@Ad", ad);
                        cmd.Parameters.AddWithValue("@Aciklama", aciklama);
                        cmd.Parameters.AddWithValue("@Fiyat", fiyat);
                        cmd.Parameters.AddWithValue("@Kategori", kategori);

                        // Komutu çalıştırarak veriyi güncelliyoruz
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Menü öğesi başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Menü öğelerini tekrar yükleyelim
                    LoadMenuItems();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}", "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            YoneticiAnaSayfaForm y = new YoneticiAnaSayfaForm(SessionManager.CurrentUserName, SessionManager.CurrentUserSurname);
            y.Show();

            // Mevcut formu gizle (örneğin, menü ekleme formunu gizleme)
            this.Hide();
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
