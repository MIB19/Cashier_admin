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
    public partial class manajersnack : Form
    {
        public manajersnack()
        {
            InitializeComponent();

            btnSnack.Enabled = false;

            int thn = 1970;
            do
            {
                tahun.Items.Add(thn);
                thn++;
            } while (thn <= DateTime.Now.Year);

            var select = "SELECT transSnack.id as Id, siswa.nama as Nama, snack.nama as Snack, transSnack.harga as Harga, transSnack.tglTrans FROM transSnack, siswa, snack WHERE siswa.id = transSnack.idSiswa AND transSnack.idSnack = snack.id";

            string strConnection = Properties.Settings.Default.tugas1dbConnectionString;
            SqlConnection con = new SqlConnection(strConnection);
            var dataAdapter = new SqlDataAdapter(select, con);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            transaksiSnack.DataSource = ds.Tables[0];
            transaksiSnack.ReadOnly = true;

            var select1 = "SELECT COUNT(*) as jumlah FROM transSnack";

            con.Open();
            var command = new SqlCommand(select1, con);

            SqlDataReader read = command.ExecuteReader(CommandBehavior.SingleRow);

            if (read.HasRows)
            {
                while (read.Read())
                {
                    jml.Text = read[0].ToString();
                }
            }
            read.Close();

            var sum = "SELECT SUM(harga) FROM transSnack";
            var command1 = new SqlCommand(sum, con);
            SqlDataReader read1 = command1.ExecuteReader(CommandBehavior.SingleRow);

            if (read1.HasRows)
            {
                while (read1.Read())
                {
                    jmlUang.Text = read1[0].ToString();
                }
            }
            read1.Close();

        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormLogin log = new FormLogin();
            log.ShowDialog();
        }

        private void btnSnack_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormLogin lgn = new FormLogin();
            lgn.ShowDialog();
        }

        private void btnKelas_Click(object sender, EventArgs e)
        {
            this.Hide();
            manajerkelas kls = new manajerkelas();
            kls.ShowDialog();
        }

        private void btnSiswa_Click(object sender, EventArgs e)
        {
            this.Hide();
            manajer mnj = new manajer();
            mnj.ShowDialog();
        }

        private void btnBulan_Click(object sender, EventArgs e)
        {
            var select = "SELECT transSnack.id, siswa.nama, snack.nama as nama_snack, transSnack.harga, transSnack.tglTrans FROM transSnack, siswa, snack WHERE siswa.id = transSnack.idSiswa AND transSnack.idSnack = snack.id AND MONTH(tglTrans) = '" + bln.Text + "'";

            string strConnection = Properties.Settings.Default.tugas1dbConnectionString;
            SqlConnection con = new SqlConnection(strConnection);
            con.Open();
            var dataAdapter = new SqlDataAdapter(select, con);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            transaksiSnack.DataSource = ds.Tables[0];
            transaksiSnack.ReadOnly = true;

            var select1 = "SELECT  COUNT(*) as jumlah, SUM(harga) FROM transSnack WHERE MONTH(tglTrans) = '" + bln.Text + "'";
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
            var select = "SELECT transSnack.id, siswa.nama, snack.nama as nama_snack, transSnack.harga, transSnack.tglTrans FROM transSnack, siswa, snack WHERE siswa.id = transSnack.idSiswa AND transSnack.idSnack = snack.id AND YEAR(tglTrans) = '" + tahun.Text + "'";

            string strConnection = Properties.Settings.Default.tugas1dbConnectionString;
            SqlConnection con = new SqlConnection(strConnection);
            con.Open();
            var dataAdapter = new SqlDataAdapter(select, con);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            transaksiSnack.DataSource = ds.Tables[0];
            transaksiSnack.ReadOnly = true;

            var select1 = "SELECT  COUNT(*) as jumlah, SUM(harga) FROM transSnack WHERE MONTH(tglTrans) = '" + tahun.Text + "'";
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

        
    }
}
