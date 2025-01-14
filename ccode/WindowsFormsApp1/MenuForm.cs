using evet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace evet
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void btnYemekler_Click(object sender, EventArgs e)
        {
            YemekForm yemek = new YemekForm("yemek");
            yemek.Show();
            this.Hide();

        }

        private void btnIcecekler_Click(object sender, EventArgs e)
        {
            YemekForm yemek = new YemekForm("icecek");
            yemek.Show();
            this.Hide();
        }

        private void btnTatlilar_Click(object sender, EventArgs e)
        {
            YemekForm yemek = new YemekForm("tatli");
            yemek.Show();
            this.Hide();
        }
    }
}
