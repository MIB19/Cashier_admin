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
    public partial class kasirsnack : Form
    {

        public string _nama;
        public string _snack;
        public string _tanggal;
        public string _jumlah;
        public string _total;
        public kasirsnack()
        {
            InitializeComponent();

            var select = "SELECT id as Id, nama as Nama, harga as Harga FROM snack";

            string strConnection = Properties.Settings.Default.tugas1dbConnectionString;
            SqlConnection con = new SqlConnection(strConnection);
            var dataAdapter = new SqlDataAdapter(select, con);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            tabelsnack.DataSource = ds.Tables[0];
            tabelsnack.ReadOnly = true;

            var select1 = "SELECT transSnack.id as Id, siswa.nama as Nama, snack.nama as Snack, transSnack.harga as Harga, transSnack.tglTrans as Tgl_Transaksi FROM transSnack, siswa, snack WHERE siswa.id = transSnack.idSiswa AND transSnack.idSnack = snack.id";

            
            var dataAdapter1 = new SqlDataAdapter(select1, con);

            var commandBuilder1 = new SqlCommandBuilder(dataAdapter1);
            var ds1 = new DataSet();
            dataAdapter.Fill(ds1);
            tabelTrans.DataSource = ds1.Tables[0];
            tabelTrans.ReadOnly = true;
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            Kasir ksr = new Kasir();
            ksr.ShowDialog();
        }

        private void kasirsnack_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'tugas1dbDataSet.siswa' table. You can move, or remove it, as needed.
            this.siswaTableAdapter.Fill(this.tugas1dbDataSet.siswa);
            // TODO: This line of code loads data into the 'tugas1dbDataSet.transSnack' table. You can move, or remove it, as needed.
            this.transSnackTableAdapter.Fill(this.tugas1dbDataSet.transSnack);
            // TODO: This line of code loads data into the 'tugas1dbDataSet.snack' table. You can move, or remove it, as needed.
            this.snackTableAdapter.Fill(this.tugas1dbDataSet.snack);

        }

        private void harga_key(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tabelsnack_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.tabelsnack.Rows[e.RowIndex];

                id.Text = row.Cells[0].Value.ToString();
                namasnack.Text = row.Cells[1].Value.ToString();
                transharga.Text = row.Cells[2].Value.ToString();
            }

            btntambah.Enabled = false;
        }

        private void btntambah_Click(object sender, EventArgs e)
        {
            if (addnama.Text == "" || harga.Text == "" )
            {
                MessageBox.Show("Isi Data Dengan Benar!");
            }
            else
            {
                var insert = @"INSERT into snack(nama, harga) values(@nama, @harga)";
                string strConnection = Properties.Settings.Default.tugas1dbConnectionString;
                SqlConnection con = new SqlConnection(strConnection);
                SqlCommand command;

                using (con)
                {
                    try
                    {
                        con.Open();
                        command = new SqlCommand(insert, con);
                        command.Parameters.AddWithValue(@"nama", addnama.Text);
                        command.Parameters.AddWithValue(@"harga", harga.Text);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                reset();
                data();
            }
            
        }

        private void reset()
        {
            addnama.Text = "";
            id.Text = "";
            harga.Text = "";
            namasiswa.Text = "";
            namasnack.Text = "";
            tgltrans.Text = "";
            transharga.Text = "";

            btntambah.Enabled = true;
            btnSimpan.Enabled = true;
        }

        private void data()
        {
            var select = "SELECT id as Id, nama as Nama, harga as Harga FROM snack";

            string strConnection = Properties.Settings.Default.tugas1dbConnectionString;
            SqlConnection con = new SqlConnection(strConnection);
            var dataAdapter = new SqlDataAdapter(select, con);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            tabelsnack.DataSource = ds.Tables[0];
            tabelsnack.ReadOnly = true;
        }

        private void data1()
        {
            var select = "SELECT transSnack.id as Id, siswa.nama as Nama, snack.nama as Snack, transSnack.harga as Harga, transSnack.tglTrans as Tgl_Transaksi FROM transSnack, siswa, snack WHERE siswa.id = transSnack.idSiswa AND transSnack.idSnack = snack.id";

            string strConnection = Properties.Settings.Default.tugas1dbConnectionString;
            SqlConnection con = new SqlConnection(strConnection);
            var dataAdapter = new SqlDataAdapter(select, con);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            tabelTrans.DataSource = ds.Tables[0];
            tabelTrans.ReadOnly = true;
        }

        private void snc_val(object sender, CancelEventArgs e)
        {
            String strConnection = Properties.Settings.Default.tugas1dbConnectionString;

            SqlConnection conn = new SqlConnection(strConnection);

            conn.Open();
            SqlCommand command = new SqlCommand("select nama,harga from snack where nama='" + namasnack.Text + "'", conn);
            // command.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if ((namasnack.Text == reader.GetString(0)))
                    {
                        transharga.Text = reader.GetString(1);
                    }
                }
            }
            reader.Close();
            conn.Close();
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            var insert = @"INSERT into transSnack(idSiswa, idSnack, harga, tglTrans) values(@idSiswa, @idSnack, @harga, @tglTrans)";
            string strConnection = Properties.Settings.Default.tugas1dbConnectionString;
            SqlConnection con = new SqlConnection(strConnection);
            SqlCommand command;
            int integer = Convert.ToInt32(transharga.Text);

            using (con)
            {
                try
                {
                    con.Open();
                    command = new SqlCommand(insert, con);
                    command.Parameters.AddWithValue(@"idSiswa", namasiswa.SelectedValue);
                    command.Parameters.AddWithValue(@"idSnack", namasnack.SelectedValue);
                    command.Parameters.AddWithValue(@"harga", integer);
                    command.Parameters.AddWithValue(@"tglTrans", tgltrans.Text);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            _nama = namasiswa.Text;
            _snack = namasnack.Text;
            _tanggal = tgltrans.Text;
            _jumlah = jumlah.Text;
            _total = transharga.Text;

            reset();
            data1();

            this.Hide();
            pemSnack pem = new pemSnack(this);
            pem.ShowDialog();
        }

        private void name_key(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);

        }

        private void jum_key(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void jumlah_Validating(object sender, CancelEventArgs e)
        {
            int integer = Convert.ToInt32(transharga.Text);
            int jml = Convert.ToInt32(jumlah.Text);

            int harga = integer * jml;

            transharga.Text = harga.ToString();
        }
    }
}
