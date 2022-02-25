using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tugas1
{
    public partial class admindef : Form
    {
        public admindef()
        {
            InitializeComponent();
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormLogin lgn = new FormLogin();
            lgn.ShowDialog();

        }

        private void btnKasir_Click(object sender, EventArgs e)
        {
            this.Hide();
            adminkasir ksr = new adminkasir();
            ksr.ShowDialog();
        }

        private void btnSiswa_Click(object sender, EventArgs e)
        {
            this.Hide();
            adminsiswa sis = new adminsiswa();
            sis.ShowDialog();

        }

        private void btnKelas_Click(object sender, EventArgs e)
        {
            this.Hide();
            adminkelas kls = new adminkelas();
            kls.ShowDialog();
        }

        private void btnPengajar_Click(object sender, EventArgs e)
        {
            this.Hide();
            adminpengajar png = new adminpengajar();
            png.ShowDialog();
        }

        private void btnManajer_Click(object sender, EventArgs e)
        {
            this.Hide();
            adminmanajer mnj = new adminmanajer();
            mnj.ShowDialog();
        }
    }
}
