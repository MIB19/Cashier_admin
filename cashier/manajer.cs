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
    public partial class manajer : Form
    {
        public manajer()
        {
            InitializeComponent();

            btnSiswa.Enabled = false;

            var select = "SELECT id as Id, nama as Nama, tglLahir as TglLahir, email as Email, nohp as No_HP, jenisKel as Gender, tglDaftar as Tgl_Daftar, alamat as Alamat FROM siswa";

            string strConnection = Properties.Settings.Default.tugas1dbConnectionString;
            SqlConnection con = new SqlConnection(strConnection);
            con.Open();
            var dataAdapter = new SqlDataAdapter(select, con);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            tabelsiswa.DataSource = ds.Tables[0];
            tabelsiswa.ReadOnly = true;

            var select1 = "SELECT COUNT(*) as jumlah FROM siswa";

            //string strConnection = Properties.Settings.Default.tugas1dbConnectionString;
            //SqlConnection con = new SqlConnection(strConnection);
            //con.Open();
            var command = new SqlCommand(select1, con);

            SqlDataReader read = command.ExecuteReader(CommandBehavior.SingleRow);

            if (read.HasRows)
            {
                while (read.Read())
                {
                    jml.Text = read[0].ToString();
                }
            }

            int thn = 1970;
            do
            {
                tahun.Items.Add(thn);
                thn++;
            } while (thn <= DateTime.Now.Year);

            
        }

        private void manajer_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'tugas1dbDataSet.siswa' table. You can move, or remove it, as needed.
            this.siswaTableAdapter.Fill(this.tugas1dbDataSet.siswa);

        }

        private void btnBulan_Click(object sender, EventArgs e)
        {
            var select = "SELECT id as Id, nama as Nama, tglLahir as TglLahir, email as Email, nohp as No_HP, jenisKel as Gender, tglDaftar as Tgl_Daftar, alamat as Alamat FROM siswa WHERE MONTH(tglDaftar) = '" + bln.Text + "'";

            string strConnection = Properties.Settings.Default.tugas1dbConnectionString;
            SqlConnection con = new SqlConnection(strConnection);
            con.Open();
            var dataAdapter = new SqlDataAdapter(select, con);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            tabelsiswa.DataSource = ds.Tables[0];
            tabelsiswa.ReadOnly = true;

            var select1 = "SELECT  COUNT(*) as jumlah FROM siswa WHERE MONTH(tglDaftar) = '" + bln.Text + "'";
            SqlCommand com = new SqlCommand(select1, con);
            SqlDataReader read = com.ExecuteReader(CommandBehavior.SingleRow);

            if (read.HasRows)
            {
                while (read.Read())
                {
                    jml.Text = read[0].ToString();
                }
            }
            read.Close();
            
        }

        private void btnTahun_Click(object sender, EventArgs e)
        {
            var select = "SELECT id as Id, nama as Nama, tglLahir as TglLahir, email as Email, nohp as No_HP, jenisKel as Gender, tglDaftar as Tgl_Daftar, alamat as Alamat FROM siswa WHERE YEAR(tglDaftar) = '" + tahun.Text + "'";

            string strConnection = Properties.Settings.Default.tugas1dbConnectionString;
            SqlConnection con = new SqlConnection(strConnection);
            con.Open();
            var dataAdapter = new SqlDataAdapter(select, con);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            tabelsiswa.DataSource = ds.Tables[0];
            tabelsiswa.ReadOnly = true;

            var select1 = "SELECT  COUNT(*) as jumlah FROM (SELECT * FROM siswa WHERE YEAR(tglDaftar) = '" + tahun.Text + "')";
            SqlCommand com = new SqlCommand(select, con);
            SqlDataReader read = com.ExecuteReader(CommandBehavior.SingleRow);

            if (read.HasRows)
            {
                while (read.Read())
                {
                    jml.Text = read[0].ToString();
                }
            }
        }

        private void btnKelas_Click(object sender, EventArgs e)
        {
            this.Hide();
            manajerkelas kls = new manajerkelas();
            kls.ShowDialog();
        }

        private void btnSnack_Click(object sender, EventArgs e)
        {
            this.Hide();
            manajersnack snc = new manajersnack();
            snc.ShowDialog();
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormLogin log = new FormLogin();
            log.ShowDialog();
        }
    }
}
