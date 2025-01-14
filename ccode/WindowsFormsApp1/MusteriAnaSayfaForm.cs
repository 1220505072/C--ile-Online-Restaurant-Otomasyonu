using System.Windows.Forms;
using System;
using evet;
using static evet.PaymentForm;
using System.Data.SqlClient;
using System.Data;

namespace WindowsFormsApp1
{
    public partial class MusteriAnaSayfaForm : Form
    {
        private string ad;
        private string soyad;
        private Timer musteriSiparisTimer;


        // Kullanıcı adı ve soyadı parametre olarak alınan kurucu
        public MusteriAnaSayfaForm(string ad, string soyad)
        {
            InitializeComponent();
            this.ad = ad;
            this.soyad = soyad;
        }

        private void MusteriAnaSayfaForm_Load(object sender, EventArgs e)
        {
            // Kullanıcı ID'sini alarak adı ve soyadı veritabanından çek
            if (SessionManager.CurrentUserID != 0)
            {
                (ad, soyad) = GetKullaniciAdSoyad(SessionManager.CurrentUserID);
            }

            // Kullanıcının ad ve soyadını bir label üzerinde gösterin
            lblKullaniciAdi.Text = $"Hoş geldiniz, {ad} {soyad}!";

            // Müşteri siparişlerini yükle
            LoadCustomerOrders();

            // Timer kurulum
            musteriSiparisTimer = new Timer();
            musteriSiparisTimer.Interval = 5000; // 5 saniye aralıkla çalışacak
            musteriSiparisTimer.Tick += MusteriSiparisTimer_Tick;
            musteriSiparisTimer.Start();
        }
        private void MusteriSiparisTimer_Tick(object sender, EventArgs e)
        {
            LoadCustomerOrders(); // Sipariş listesini yenile
        }
        // Kullanıcı bilgilerini veritabanından çekme metodu
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
        private void anaSayfa_Click(object sender, EventArgs e)
        {
            // LoginForm'u göster
            MusteriAnaSayfaForm MusteriForm = new MusteriAnaSayfaForm(ad, soyad);
            MusteriForm.Show();
            this.Hide(); // Form1'i gizle
        }

        private void menu_Click(object sender, EventArgs e)
        {
            // YemekForm'un bir örneğini oluşturun
            MenuForm yemekForm = new MenuForm();

            // YemekForm'u gösterin
            yemekForm.Show();

            // Mevcut formu gizleyin
            this.Hide();
        }
        private void adres_Click(object sender, EventArgs e)
        {

        }

        private void LoadCustomerOrders()
        {
            string connectionString = @"Data Source=LAPTOP-K4MOT0FU\SQLEXPRESS;Initial Catalog=Proje1;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // SiparisID ve Durum'u sorguluyoruz
                    string query = "SELECT SiparisID, Durum FROM Siparisler1 WHERE KullaniciID = @KullaniciID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@KullaniciID", SessionManager.CurrentUserID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    // ListView kontrolünü temizle
                    listViewOrders.Items.Clear();

                    // Tek bir sütun ekleyin
                    if (listViewOrders.Columns.Count == 0)
                    {
                        listViewOrders.Columns.Add("Sipariş ID ve Durum", 300); // 300 px genişlikte tek sütun
                    }

                    // Verileri yan yana ekle
                    while (reader.Read())
                    {
                        string siparisID = reader["SiparisID"]?.ToString() ?? "Bilinmiyor";
                        string durum = reader["Durum"]?.ToString() ?? "Durum Yok";

                        // ID ve Durum'u yan yana yazdırın
                        string combinedText = $"Sipariş ID: {siparisID}, Durum: {durum}";

                        // ListViewItem oluşturun ve yan yana gösterin
                        ListViewItem item = new ListViewItem(combinedText);
                        listViewOrders.Items.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    // Hata durumunda kullanıcıya bilgi ver
                    MessageBox.Show($"Hata: {ex.Message}", "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void btnSil_Click(object sender, EventArgs e)
        {
            // Kullanıcıya onay soralım
            DialogResult result = MessageBox.Show("Tüm siparişleri silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // ListView'deki tüm öğeleri temizle
                listViewOrders.Items.Clear();
            }
        }

    }
}

