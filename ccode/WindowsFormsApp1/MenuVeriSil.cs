using evet;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class MenuVeriSil : Form
    {
        public MenuVeriSil()
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

        private void MenuVeriSil_Load(object sender, EventArgs e)
        {
            // Veritabanından menü öğelerini çekip DataGridView'e ekleyelim
            LoadMenuItems();
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

                    // DataGridView'e verileri ekliyoruz
                    dgvMenuItems.DataSource = dataTable;

                    // DataGridView'e Sil butonu ekleyelim
                    DataGridViewButtonColumn btnSil = new DataGridViewButtonColumn();
                    btnSil.HeaderText = "Sil";
                    btnSil.Name = "Sil";  // Butonun adı
                    btnSil.Text = "Sil";
                    btnSil.UseColumnTextForButtonValue = true;
                    dgvMenuItems.Columns.Add(btnSil);

                    // Sil butonuna tıklanabilir olayını ekleyelim
                    dgvMenuItems.CellContentClick += dgvMenuItems_CellContentClick;
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


        private void dgvMenuItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Sil butonuna tıklanıp tıklanmadığını kontrol edelim
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvMenuItems.Columns["Sil"].Index)
            {
                // Seçilen satırdaki öğe ID'sini alalım
                int ogeID = Convert.ToInt32(dgvMenuItems.Rows[e.RowIndex].Cells["OgeID"].Value);

                // Silme işlemi
                DeleteMenuItem(ogeID);
            }
        }


        private void DeleteMenuItem(int ogeID)
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-K4MOT0FU\SQLEXPRESS;Initial Catalog=Proje1;Integrated Security=True"))
            {
                try
                {
                    conn.Open();

                    // Silme işlemi için SQL sorgusunu yazıyoruz
                    string query = "DELETE FROM MenuOgeleri WHERE OgeID = @OgeID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Parametreyi ekliyoruz
                        cmd.Parameters.AddWithValue("@OgeID", ogeID);

                        // Komutu çalıştırarak veriyi siliyoruz
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Menü öğesi başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Menü öğelerini tekrar yükleyelim
                    LoadMenuItems();
                }
                catch (Exception ex)
                {
                    // Hata durumunda kullanıcıya bilgi veriyoruz
                    MessageBox.Show($"Hata: {ex.Message}", "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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
