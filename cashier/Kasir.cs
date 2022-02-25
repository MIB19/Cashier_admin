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
    public partial class Kasir : Form
    {
        public Kasir()
        {
            InitializeComponent();
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormLogin login = new FormLogin();
            login.ShowDialog();
        }

        private void btnKelas_Click(object sender, EventArgs e)
        {
            this.Hide();
            kasirkelas kls = new kasirkelas();
            kls.ShowDialog();
        }

        private void btnSnack_Click(object sender, EventArgs e)
        {
            this.Hide();
            kasirsnack snc = new kasirsnack();
            snc.ShowDialog();
        }

        private void btnAtk_Click(object sender, EventArgs e)
        {
            this.Hide();
            kasiratk atk = new kasiratk();
            atk.ShowDialog();
        }
    }
}
