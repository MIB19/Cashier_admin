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
    public partial class kasirkelas : Form
    {

        public string _nama;
        public string _total;
        public string _jumlah;
        public string _ttl;
        public string _nm;

        

        public kasirkelas()
        {
            InitializeComponent();

            var select = "SELECT transKelas.id as Id, siswa.nama as Nama, kelas.nama as Kelas, transKelas.tglTrans as Tgl_Trans, transKelas.totBiaya as Total, transKelas.jmlDp as DP, transKelas.biayaKrg as Sisa, transKelas.tglLunas as Pelunasan FROM transKelas, siswa, kelas WHERE siswa.id = transKelas.idSiswa AND transKelas.idKelas = kelas.id";

            string strConnection = Properties.Settings.Default.tugas1dbConnectionString;
            SqlConnection con = new SqlConnection(strConnection);
            var dataAdapter = new SqlDataAdapter(select, con);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            tabeltrans.DataSource = ds.Tables[0];
            tabeltrans.ReadOnly = true;
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            Kasir ksr = new Kasir();
            ksr.ShowDialog();
        }

        private void kasirkelas_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'tugas1dbDataSet.siswa' table. You can move, or remove it, as needed.
            this.siswaTableAdapter.Fill(this.tugas1dbDataSet.siswa);
            // TODO: This line of code loads data into the 'tugas1dbDataSet.kelas' table. You can move, or remove it, as needed.
            this.kelasTableAdapter.Fill(this.tugas1dbDataSet.kelas);
            // TODO: This line of code loads data into the 'tugas1dbDataSet.transKelas' table. You can move, or remove it, as needed.
            this.transKelasTableAdapter.Fill(this.tugas1dbDataSet.transKelas);

        }

        private void kelas_val(object sender, CancelEventArgs e)
        {
            String strConnection = Properties.Settings.Default.tugas1dbConnectionString;

            SqlConnection conn = new SqlConnection(strConnection);

            conn.Open();
            SqlCommand command = new SqlCommand("select nama,harga from kelas where nama='" + pilKelas.Text + "'", conn);
            // command.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
           
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if ((pilKelas.Text == reader.GetString(0)))
                    {
                        totBiaya.Text = reader.GetString(1);
                    }
                }
            }
            reader.Close();
            conn.Close();
        }

        private void dp_key(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

           // int total = int.Parse(totBiaya.Text);
            //int depe = int.Parse(dp.Text);
           // int jumlah = total - depe;

            //sisaPem.Text = jumlah.ToString();
        }

        private void dp_val(object sender, CancelEventArgs e)
        {
            int total = int.Parse(totBiaya.Text);
            int depe = int.Parse(dp.Text);
            int jumlah = total - depe;

            sisaPem.Text = jumlah.ToString();
        }

        private void btntambah_Click(object sender, EventArgs e)
        {
            var insert = @"insert into transKelas(idSiswa, idKelas, tglTrans, totBiaya, jmlDp, biayaKrg) values(@idSiswa, @idKelas, @tglTrans, @totBiaya, @jmlDp, @biayaKrg)";


            string strConnection = Properties.Settings.Default.tugas1dbConnectionString;
            SqlConnection con = new SqlConnection(strConnection);
            SqlCommand command;
            int integer = Convert.ToInt32(totBiaya.Text);
            using (con)
            {
                try
                {
                    con.Open();
                    command = new SqlCommand(insert, con);
                    command.Parameters.AddWithValue(@"idSiswa", nama.SelectedValue);
                    command.Parameters.AddWithValue(@"idKelas", pilKelas.SelectedValue);
                    command.Parameters.AddWithValue(@"tglTrans", tglTrans.Text);
                    command.Parameters.AddWithValue(@"totBiaya", integer);
                    command.Parameters.AddWithValue(@"jmlDp", dp.Text);
                    command.Parameters.AddWithValue(@"biayaKrg", sisaPem.Text);
                    command.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (sisaPem.Text == "0")
            {
                SqlConnection con1 = new SqlConnection(strConnection);
                con1.Open();
                SqlCommand command1 = new SqlCommand("SELECT siswa.nama, transKelas.totBiaya FROM siswa,transKelas where idSiswa='" + nama.SelectedValue + "' AND siswa.id = transKelas.idSiswa", con1);
                // command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command1.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        _nama = reader[0].ToString();
                        _total = reader[1].ToString();
                    }
                }
                if (pilKelas.Text == "standart")
                {
                    _jumlah = "3600000";
                }else if(pilKelas.Text == "regular")
                {
                    _jumlah = "2800000";
                }else if (pilKelas.Text == "luxury")
                {
                    _jumlah = "5000000";
                }
                this.Hide();
                PemKelas pem = new PemKelas(this);
                pem.ShowDialog();
            }

            reset();
            databind();
        }

        private void databind()
        {
            var select = "SELECT transKelas.id as Id, siswa.nama as Nama, kelas.nama as Kelas, transKelas.tglTrans as Tgl_Transaksi, transKelas.totBiaya as Total, transKelas.jmlDp as DP, transKelas.biayaKrg as Sisa, transKelas.tglLunas as Pelunasan FROM transKelas, siswa, kelas WHERE siswa.id = transKelas.idSiswa AND transKelas.idKelas = kelas.id";

            string strConnection = Properties.Settings.Default.tugas1dbConnectionString;
            SqlConnection con = new SqlConnection(strConnection);
            var dataAdapter = new SqlDataAdapter(select, con);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            tabeltrans.DataSource = ds.Tables[0];
            tabeltrans.ReadOnly = true;
        }

        private void reset()
        {
            id.Text = "";
            nama.Text = "";
            pilKelas.Text = "";
            tglTrans.Text = "";
            totBiaya.Text = "";
            dp.Text = "";
            sisaPem.Text = "";
            namaPel.Text = "";
            tglPel.Text = "";
            sisa.Text = "";
        }

        private void tabeltrans_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.tabeltrans.Rows[e.RowIndex];

                id.Text = row.Cells[0].Value.ToString();
                namaPel.Text = row.Cells[1].Value.ToString();
                sisa.Text = row.Cells[6].Value.ToString();
                
            }

            btntambah.Enabled = false;
        }

        private void btnsimpan_Click(object sender, EventArgs e)
        {
            var update = "UPDATE transKelas SET biayaKrg = '0' , tglLunas=@tglLunas WHERE id = @id";
            var update1 = "UPDATE transKelas SET biayaKrg = '0' , tglLunas=@tglLunas WHERE idSiswa = @idSiswa";

            string strConnection = Properties.Settings.Default.tugas1dbConnectionString;
            SqlConnection con = new SqlConnection(strConnection);
            SqlCommand command;

            using (con)
            {
                try
                {
                    if(id.Text == "")
                    {
                        con.Open();
                        command = new SqlCommand(update1, con);
                        command.Parameters.AddWithValue("@tglLunas", tglPel.Text);
                        command.Parameters.AddWithValue("@idSiswa", namaPel.SelectedValue);
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        con.Open();
                        command = new SqlCommand(update, con);
                        command.Parameters.AddWithValue("@tglLunas", tglPel.Text);
                        command.Parameters.AddWithValue("@id", id.Text);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            _nm = namaPel.Text;
            
            databind();

           
            SqlConnection con1 = new SqlConnection(strConnection);
            con1.Open();
            SqlCommand command1 = new SqlCommand("SELECT totBiaya FROM transKelas where idSiswa='" + namaPel.SelectedValue + "'", con1);
            // command.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = command1.ExecuteReader(CommandBehavior.SingleRow);
            
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    
                    _ttl = reader[0].ToString();
                }
            }

            this.Hide();
            PemKelas pem = new PemKelas(this);
            pem.ShowDialog();
        }
    }
}
    