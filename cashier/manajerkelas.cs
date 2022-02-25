using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tugas1
{
    public partial class manajerkelas : Form
    {
        public manajerkelas()
        {
            InitializeComponent();

            btnKelas.Enabled = false;

            int thn = 1970;
            do
            {
                tahun.Items.Add(thn);
                thn++;
            } while (thn <= DateTime.Now.Year);

            var select = "SELECT transKelas.id as Id, siswa.nama as Nama, kelas.nama as Kelas, transKelas.tglTrans as Tanggal, transKelas.totBiaya as Biaya, transKelas.jmlDp as DP, transKelas.biayaKrg as Biaya_Kurang, tglLunas as Pelunasan FROM transKelas, siswa, kelas WHERE siswa.id = transKelas.idSiswa AND transKelas.idKelas = kelas.id";

            string strConnection = Properties.Settings.Default.tugas1dbConnectionString;
            SqlConnection con = new SqlConnection(strConnection);
            var dataAdapter = new SqlDataAdapter(select, con);

            //var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            transaksiKelas.DataSource = ds.Tables[0];
            transaksiKelas.ReadOnly = true;

            var select1 = "SELECT COUNT(*), SUM(totBiaya) FROM transKelas";

            con.Open();
            var command = new SqlCommand(select1, con);

            SqlDataReader read = command.ExecuteReader(CommandBehavior.SingleRow);

            if (read.HasRows)
            {
                while (read.Read())
                {
                    jml.Text = read[0].ToString();
                    jmlUang.Text = read[1].ToString();
                }
            }
            read.Close();

        }

        private void btnSiswa_Click(object sender, EventArgs e)
        {
            this.Hide();
            manajer mnj = new manajer();
            mnj.ShowDialog();
        }

        private void btnSnack_Click(object sender, EventArgs e)
        {
            this.Hide();
            manajersnack snc = new manajersnack();
            snc.ShowDialog();
        }

        private void btnBulan_Click(object sender, EventArgs e)
        {
            var select = "SELECT transKelas.id as Id, siswa.nama as Nama, kelas.nama as Kelas, transKelas.tglTrans as Tanggal, transKelas.totBiaya as Biaya, transKelas.jmlDp as DP, transKelas.biayaKrg as Biaya_Kurang, tglLunas as Pelunasan FROM transKelas, siswa, kelas WHERE siswa.id = transKelas.idSiswa AND transKelas.idKelas = kelas.id AND MONTH(tglTrans) = '" + bln.Text + "'";

            string strConnection = Properties.Settings.Default.tugas1dbConnectionString;
            SqlConnection con = new SqlConnection(strConnection);
            con.Open();
            var dataAdapter = new SqlDataAdapter(select, con);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            transaksiKelas.DataSource = ds.Tables[0];
            transaksiKelas.ReadOnly = true;

            var select1 = "SELECT  COUNT(*) as jumlah, SUM(totBiaya) FROM transKelas WHERE MONTH(tglTrans) = '" + bln.Text + "'";
            SqlCommand com = new SqlCommand(select1, con);
            SqlDataReader read = com.ExecuteReader(CommandBehavior.SingleRow);

            if (read.HasRows)
            {
                while (read.Read())
                {
                    jml.Text = read[0].ToString();
                    jmlUang.Text = read[1].ToString();
                }
            }
            read.Close();
        }

        private void btnTahun_Click(object sender, EventArgs e)
        {
            var select = "SELECT transKelas.id as Id, siswa.nama as Nama, kelas.nama as Kelas, transKelas.tglTrans as Tanggal, transKelas.totBiaya as Biaya, transKelas.jmlDp as DP, transKelas.biayaKrg as Biaya_Kurang, tglLunas as Pelunasan FROM transKelas, siswa, kelas WHERE siswa.id = transKelas.idSiswa AND transKelas.idKelas = kelas.id AND YEAR(tglTrans) = '" + tahun.Text + "'";

            string strConnection = Properties.Settings.Default.tugas1dbConnectionString;
            SqlConnection con = new SqlConnection(strConnection);
            con.Open();
            var dataAdapter = new SqlDataAdapter(select, con);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            transaksiKelas.DataSource = ds.Tables[0];
            transaksiKelas.ReadOnly = true;

            var select1 = "SELECT  COUNT(*) as jumlah, SUM(totBiaya) FROM transKelas WHERE YEAR(tglTrans) = '" + tahun.Text + "'";
            SqlCommand com = new SqlCommand(select1, con);
            SqlDataReader read = com.ExecuteReader(CommandBehavior.SingleRow);

            if (read.HasRows)
            {
                while (read.Read())
                {
                    jml.Text = read[0].ToString();
                    jmlUang.Text = read[1].ToString();
                }
            }
            read.Close();
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormLogin lhn = new FormLogin();
            lhn.ShowDialog();
        }

        private void kelaster_Click(object sender, EventArgs e)
        {
            this.Hide();
            manajerdetail mnj = new manajerdetail();
            mnj.ShowDialog();
        }
    }
}
