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
    public partial class kasiratk : Form
    {
        public string hsl;
        public string _nama;
        public string _snack;
        public string _tanggal;
        public string _jumlah;
        public string _total;
        public kasiratk()
        {
            InitializeComponent();
        }

        private void kasiratk_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'tugas1dbDataSet.siswa' table. You can move, or remove it, as needed.
            this.siswaTableAdapter.Fill(this.tugas1dbDataSet.siswa);

        }

        private void pil_val(object sender, CancelEventArgs e)
        {
            if (pilAtk.Text == "Buku")
            {
                totBiaya.Text = "4000";
                hsl = "4000";
            }
            else if (pilAtk.Text == "Pensil")
            {
                totBiaya.Text = "2000";
                hsl = "2000";
            }else if (pilAtk.Text == "Pulpen")
            {
                totBiaya.Text = "2500";
                hsl = "2500";
            }else if (pilAtk.Text == "Penggaris")
            {
                totBiaya.Text = "4000";
                hsl = "4000";
            }else if (pilAtk.Text == "Penghapus")
            {
                totBiaya.Text = "1500";
                hsl = "1500";
            }else if (pilAtk.Text == "Stipo")
            {
                totBiaya.Text = "8000";
                hsl = "8000";
            }else if (pilAtk.Text == "Folio")
            {
                totBiaya.Text = "1500";
                hsl = "1500";
            }
        }

        private void jum_val(object sender, CancelEventArgs e)
        {
            int integer = Convert.ToInt32(hsl);
            int jml = Convert.ToInt32(txJumlah.Text);

            int harga = integer * jml;

            totBiaya.Text = harga.ToString();
        }

        private void jum_key(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btntambah_Click(object sender, EventArgs e)
        {
            _nama = nama.Text;
            _tanggal = tglTrans.Text;
            _snack = pilAtk.Text;
            _jumlah = txJumlah.Text;
            _total = totBiaya.Text;

            this.Hide();
            pemAtk pem = new pemAtk(this);
            pem.ShowDialog();
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            Kasir asr = new Kasir();
            asr.ShowDialog();
        }
    }
}
