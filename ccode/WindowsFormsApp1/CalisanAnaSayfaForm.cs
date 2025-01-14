using System.Windows.Forms;
using System;
using evet;
using static evet.PaymentForm;
using System.Data.SqlClient;
using System.Data;

namespace WindowsFormsApp1
{
    public partial class CalisanAnaSayfaForm : Form
    {
        private string ad;
        private string soyad;
        private BindingSource bindingSource = new BindingSource();


        // Kullanıcı adı ve soyadı parametre olarak alınan kurucu
        public CalisanAnaSayfaForm(string ad, string soyad)
        {
           InitializeComponent();
            this.ad = ad;
            this.soyad = soyad;
            dataGridViewOrders.DataSource = bindingSource; // BindingSource'u bağla
        }

        private void CalisanAnaSayfaForm_Load(object sender, EventArgs e)
        {
            // Kullanıcı ID'sini alarak adı ve soyadı veritabanından çek
            if (SessionManager.CurrentUserID != 0)
            {
                (ad, soyad) = GetKullaniciAdSoyad(SessionManager.CurrentUserID);
            }

            // Kullanıcının ad ve soyadını bir label üzerinde gösterin
            lblKullaniciAdi.Text = $"Hoş geldiniz Sayın Çalışan, {ad} {soyad}!";

            // Siparişleri yükle
            LoadOrders();
        }

        private (string ad, string soyad) GetKullaniciAdSoyad(int kullaniciID)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=LAPTOP-K4MOT0FU\SQLEXPRESS;Initial Catalog=Proje1;Integrated Security=True"))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Ad, Soyad FROM Kullanicilar WHERE KullaniciID = @KullaniciID";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string ad = reader["Ad"].ToString();
                        string soyad = reader["Soyad"].ToString();
                        return (ad, soyad);
                    }
                    else
                    {
                        return (null, null);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}", "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return (null, null);
                }
            }
        }

        private void LoadOrders()
        {
            using (SqlConnection conn = new SqlConnection("Data Source=LAPTOP-K4MOT0FU\\SQLEXPRESS;Initial Catalog=Proje1;Integrated Security=True"))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT SiparisID, Ad, Fiyat, Miktar, KullaniciID, SiparisTarihi, Durum FROM Siparisler1";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    bindingSource.DataSource = dt;
                    dataGridViewOrders.DataSource = bindingSource;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}");
                }
            }
        }

        private void UpdateOrderStatus(int siparisID, string yeniDurum)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=LAPTOP-K4MOT0FU\\SQLEXPRESS;Initial Catalog=Proje1;Integrated Security=True"))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Siparisler1 SET Durum = @Durum WHERE SiparisID = @SiparisID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Durum", yeniDurum);
                        cmd.Parameters.AddWithValue("@SiparisID", siparisID);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            LoadOrders();
                            MessageBox.Show("Sipariş durumu başarıyla güncellendi.");
                        }
                        else
                        {
                            MessageBox.Show("Sipariş durumu güncellenemedi. Lütfen sipariş ID'sini kontrol edin.");
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}");
                }
            }
        }


        private bool CanChangeToIptal(string currentStatus)
        {
            // Eğer sipariş "Yolda" veya "Teslim Edildi" ise "İptal Edildi" durumuna geçemez.
            return currentStatus != "Yolda" && currentStatus != "Teslim Edildi";
        }

        private void btnOnayla_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.CurrentRow != null)
            {
                int siparisID = Convert.ToInt32(dataGridViewOrders.CurrentRow.Cells["SiparisID"].Value);
                UpdateOrderStatus(siparisID, "Sipariş Alındı");
            }
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.CurrentRow != null)
            {
                int siparisID = Convert.ToInt32(dataGridViewOrders.CurrentRow.Cells["SiparisID"].Value);
                string currentStatus = dataGridViewOrders.CurrentRow.Cells["Durum"].Value.ToString();

                if (CanChangeToIptal(currentStatus))
                {
                    UpdateOrderStatus(siparisID, "İptal Edildi");
                }
                else
                {
                    MessageBox.Show("Bu sipariş 'Yolda' veya 'Teslim Edildi' durumunda olduğu için iptal edilemez.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnYolaCikti_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.CurrentRow != null)
            {
                int siparisID = Convert.ToInt32(dataGridViewOrders.CurrentRow.Cells["SiparisID"].Value);
                UpdateOrderStatus(siparisID, "Yolda");
            }
        }


        private void btnTeslimEdildi_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.CurrentRow != null)
            {
                int siparisID = Convert.ToInt32(dataGridViewOrders.CurrentRow.Cells["SiparisID"].Value);
                UpdateOrderStatus(siparisID, "Teslim Edildi");
            }
        }



        private void btnMoveNext_Click_1(object sender, EventArgs e)
        {
            bindingSource.MoveNext();
        }

        private void btnMovePrevious_Click_1(object sender, EventArgs e)
        {
            bindingSource.MovePrevious();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bindingSource.MoveFirst();
        }

        private void btnMoveLast_Click_1(object sender, EventArgs e)
        {
            bindingSource.MoveLast();
        }

        private void btnOzet_Click(object sender, EventArgs e)
        {
            CalisanGunlukOzet ozet= new CalisanGunlukOzet();
            ozet.Show();
            this.Close();
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
