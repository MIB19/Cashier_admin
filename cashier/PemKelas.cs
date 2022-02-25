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
    public partial class PemKelas : Form
    {
        public PemKelas(kasirkelas data)
        {
            InitializeComponent();

                lbl_nm.Text = data._nm;
                lblttl.Text = data._ttl;
                lblnama.Text = data._nama;
                lbljumlah.Text = data._total;
            
        }

        

        private void cetak_data_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(this.bunifuCards1.Width, this.bunifuCards1.Height);

            bunifuCards1.DrawToBitmap(bm, new Rectangle(0, 0, this.bunifuCards1.Width, this.bunifuCards1.Height));

            e.Graphics.DrawImage(bm, 0, 0);
        }

        

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Hide();
            kasirkelas ksr = new kasirkelas();
            ksr.ShowDialog();
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            cetak_data.Print();
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }
    }
}
