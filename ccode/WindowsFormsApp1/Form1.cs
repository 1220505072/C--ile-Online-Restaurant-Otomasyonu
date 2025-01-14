using System;
using System.Windows.Forms;


namespace evet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Giriş Yap butonuna tıklama işlemi
        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            // LoginForm'u göster
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide(); // Form1'i gizle
        }

        // Kayıt Ol butonuna tıklama işlemi
        private void btnKayitOl_Click(object sender, EventArgs e)
        {
            // RegisterForm'u göster
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Hide(); // Form1'i gizle
        }
    }
}
