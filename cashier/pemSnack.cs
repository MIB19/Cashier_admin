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
    public partial class pemSnack : Form
    {
        public pemSnack(kasirsnack data)
        {
            InitializeComponent();

            nama.Text = data._nama;
            snack.Text = data._snack;
            tanggal.Text = data._tanggal;
            jumlah.Text = data._jumlah;
            total.Text = data._total;
            

        }

        

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Hide();
            kasirsnack snc = new kasirsnack();
            snc.ShowDialog();
        }

        private void print_Click(object sender, EventArgs e)
        {
            cetak.Print();
        }

        private void cetak_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bit = new Bitmap(this.panel1.Width, this.panel1.Height);

            panel1.DrawToBitmap(bit, new Rectangle(0, 0, this.panel1.Width, this.panel1.Height));

            e.Graphics.DrawImage(bit, 0, 0);
        }
    }
}
