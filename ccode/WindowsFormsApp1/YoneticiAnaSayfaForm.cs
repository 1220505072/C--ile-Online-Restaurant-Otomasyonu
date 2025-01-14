using System.Windows.Forms;
using System;
using evet;
using static evet.PaymentForm;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class YoneticiAnaSayfaForm : Form
    {
        private string ad;
        private string soyad;

        // Kullanıcı adı ve soyadı parametre olarak alınan kurucu
        public YoneticiAnaSayfaForm(string ad, string soyad)
        {
            InitializeComponent();
            this.ad = ad;
            this.soyad = soyad;
        }

        private void YoneticiAnaSayfaForm_Load(object sender, EventArgs e)
        {
            // Kullanıcı ID'sini alarak adı ve soyadı veritabanından çek
            if (SessionManager.CurrentUserID != 0)
            {
                (ad, soyad) = GetKullaniciAdSoyad(SessionManager.CurrentUserID);
            }

            // Kullanıcının ad ve soyadını bir label üzerinde gösterin
            lblKullaniciAdi.Text = $"Hoş geldiniz, {ad} {soyad}!";
        }

        // Kullanıcı bilgilerini veritabanından çekme metodu
        private (string ad, string soyad) GetKullaniciAdSoyad(int kullaniciID)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=LAPTOP-K4MOT0FU\SQLEXPRESS;Initial Catalog=Proje1;Integrated Security=True"))
            {
                try
                {
                    connection.Open();

                    // Kullanıcı adı ve soyadı sorgusu
                    string query = "SELECT Ad, Soyad FROM Kullanicilar WHERE KullaniciID = @KullaniciID";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read()) // Kullanıcı bulundu mu?
                    {
                        string ad = reader["Ad"].ToString();
                        string soyad = reader["Soyad"].ToString();
                        return (ad, soyad); // Ad ve soyad bilgilerini döndür
                    }
                    else
                    {
                        return (null, null); // Kullanıcı bulunamadı
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}", "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return (null, null); // Hata meydana geldi
                }
            }
        }
        private void CalisanEkle_Click(object sender, EventArgs e)
        {
            CEkleYoneticiForm calisanRegister=new CEkleYoneticiForm();
            calisanRegister.ShowDialog();
            this.Hide();
        }

        private void btnMenuEkle_Click(object sender, EventArgs e)
        {
            MenuVeriEkle menuVeriEkle=new MenuVeriEkle();
            menuVeriEkle.ShowDialog();
            this.Hide();

        }

        private void btnMenuCikar_Click(object sender, EventArgs e)
        {
            MenuVeriSil menuVeriSil=new MenuVeriSil();
            menuVeriSil.ShowDialog();
            this.Hide();
        }

        private void btnMenuGuncelle_Click(object sender, EventArgs e)
        {
            MenuVeriGuncelle menuVeriGuncelle=new MenuVeriGuncelle();
            menuVeriGuncelle.ShowDialog();
            this.Hide();
        }

        private void CalisanCikar_Click(object sender, EventArgs e)
        {
            CCikarYoneticiForm calisanRegister = new CCikarYoneticiForm();
            calisanRegister.ShowDialog();
            this.Hide();
        }

        private void btnGunluk_Click(object sender, EventArgs e)
        {
            yGunluk yGunluk = new yGunluk();    
            yGunluk.Show();
            this.Hide();
        }

        private void btnHaftalik_Click(object sender, EventArgs e)
        {
           yHaftalik yHaftalik = new yHaftalik();
            yHaftalik.Show();
            this.Hide();
        }

        private void btnAylik_Click(object sender, EventArgs e)
        {
            yAylik yAylik = new yAylik();
            yAylik.Show();
            this.Hide();
        }

        private void satis_Click(object sender, EventArgs e)
        {
            ySatis ySatis = new ySatis();
            ySatis.Show();
            this.Hide();
        }
    }
}