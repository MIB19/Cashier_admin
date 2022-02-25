using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tugas1
{
    public partial class adminpengajar : Form
    {
        public adminpengajar()
        {
            InitializeComponent();

            btnbatal.Enabled = false;
            btnSimpan.Enabled = false;
            btnHapus.Enabled = false;

            var select = "SELECT id as Id, nama as Nama, tglLahir as TglLahir, nohp as No_HP, email as Email, jenisKel as Gender, tglMasuk as Tgl_Masuk, alamat as Alamat FROM pengajar";

            string strConnection = Properties.Settings.Default.tugas1dbConnectionString;
            SqlConnection con = new SqlConnection(strConnection);
            var dataAdapter = new SqlDataAdapter(select, con);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            tabelpengajar.DataSource = ds.Tables[0];
            tabelpengajar.ReadOnly = true;
        }

        private void databind()
        {
            var select = "SELECT id as Id, nama as Nama, tglLahir as TglLahir, nohp as No_HP, email as Email, jenisKel as Gender, tglMasuk as Tgl_Masuk, alamat as Alamat FROM pengajar";

            string strConnection = Properties.Settings.Default.tugas1dbConnectionString;
            SqlConnection con = new SqlConnection(strConnection);
            var dataAdapter = new SqlDataAdapter(select, con);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            tabelpengajar.DataSource = ds.Tables[0];
            tabelpengajar.ReadOnly = true;

            
        }

        private void reset()
        {
            id.Text = "";
            nama.Text = "";
            tglLahir.Text = "";
            nohp.Text = "";
            email.Text = "";
            tanggalmasuk.Text = "";
            jenisKel.Text = "";
            alamat.Text = "";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            admindef adm = new admindef();
            adm.ShowDialog();
        }

        private void adminpengajar_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'tugas1dbDataSet.pengajar' table. You can move, or remove it, as needed.
            this.pengajarTableAdapter.Fill(this.tugas1dbDataSet.pengajar);

        }

        private void tabelpengajar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.tabelpengajar.Rows[e.RowIndex];

                id.Text = row.Cells[0].Value.ToString();
                nama.Text = row.Cells[1].Value.ToString();
                tglLahir.Text = row.Cells[2].Value.ToString();
                nohp.Text = row.Cells[3].Value.ToString();
                email.Text = row.Cells[4].Value.ToString();
                tanggalmasuk.Text = row.Cells[6].Value.ToString();
                jenisKel.Text = row.Cells[5].Value.ToString();
                alamat.Text = row.Cells[7].Value.ToString();
            }

            btnTambah.Enabled = false;
            btnEdit.Enabled = false;
            btnbatal.Enabled = true;
            btnSimpan.Enabled = true;
            btnHapus.Enabled = true;
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            var delete = "DELETE FROM pengajar WHERE id = @id";
            var delete1 = "DELETE FROM pengajar WHERE nama = @nama";

            string strConnection = Properties.Settings.Default.tugas1dbConnectionString;
            SqlConnection con = new SqlConnection(strConnection);
            SqlCommand command;
            using (con)
            {
                try
                {
                    if (id.Text == "")
                    {
                        con.Open();
                        command = new SqlCommand(delete1, con);

                        command.Parameters.AddWithValue("@nama", nama.Text);
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        con.Open();
                        command = new SqlCommand(delete, con);
                        command.Parameters.AddWithValue("@id", id.Text);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            MessageBox.Show("Delete Berhasil !", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            databind();
            reset();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnTambah.Enabled = false;
            btnEdit.Enabled = false;
            btnSimpan.Enabled = true;
            btnbatal.Enabled = true;
            btnHapus.Enabled = true;
        }

        private void btnbatal_Click(object sender, EventArgs e)
        {
            btnTambah.Enabled = true;
            btnEdit.Enabled = true;
            btnSimpan.Enabled = false;
            btnHapus.Enabled = false;
            btnbatal.Enabled = false;

            reset();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {

            if (nama.Text == "" || tglLahir.Text == "" || nohp.Text == "" || email.Text == "" || tanggalmasuk.Text == "" || jenisKel.Text == "")
            {
                MessageBox.Show("Isi Data Dengan Benar !");
            }
            else
            {
                var insert = @"insert into pengajar(nama, tglLahir, nohp, email, jenisKel, tglMasuk, alamat) values(@nama, @tglLahir, @nohp, @email, @jenisKel, @tglMasuk, @alamat)";


                string strConnection = Properties.Settings.Default.tugas1dbConnectionString;
                SqlConnection con = new SqlConnection(strConnection);
                SqlCommand command;

                using (con)
                {
                    try
                    {
                        con.Open();
                        command = new SqlCommand(insert, con);
                        command.Parameters.AddWithValue(@"nama", nama.Text);
                        command.Parameters.AddWithValue(@"tglLahir", tglLahir.Text);
                        command.Parameters.AddWithValue(@"nohp", nohp.Text);
                        command.Parameters.AddWithValue(@"email", email.Text);
                        command.Parameters.AddWithValue(@"tglMasuk", tanggalmasuk.Text);
                        command.Parameters.AddWithValue(@"jenisKel", jenisKel.Text);
                        command.Parameters.AddWithValue(@"alamat", alamat.Text);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                reset();
                databind();
            }
            
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            var update = "UPDATE pengajar SET nama=@nama, tglLahir=@tglLahir, nohp=@nohp, email=@email, jenisKel=@jenisKel, tglMasuk=@tglMasuk, alamat=@alamat WHERE id=@id";
            var update1 = "UPDATE pengajar SET tglLahir=@tglLahir, nohp=@nohp, email=@email, jenisKel=@jenisKel, tglMasuk=@tglMasuk, alamat=@alamat WHERE nama=@nama";


            string strConnection = Properties.Settings.Default.tugas1dbConnectionString;
            SqlConnection con = new SqlConnection(strConnection);
            SqlCommand command;

            using (con)
            {
                try
                {
                    if (id.Text == "")
                    {
                        con.Open();
                        command = new SqlCommand(update1, con);
                        command.Parameters.AddWithValue(@"nama", nama.Text);
                        command.Parameters.AddWithValue(@"tglLahir", tglLahir.Text);
                        command.Parameters.AddWithValue(@"nohp", nohp.Text);
                        command.Parameters.AddWithValue(@"email", email.Text);
                        command.Parameters.AddWithValue(@"tglMasuk", tanggalmasuk.Text);
                        command.Parameters.AddWithValue(@"jenisKel", jenisKel.Text);
                        command.Parameters.AddWithValue(@"alamat", alamat.Text);
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        con.Open();
                        command = new SqlCommand(update, con);
                        command.Parameters.AddWithValue(@"id", id.Text);
                        command.Parameters.AddWithValue(@"nama", nama.Text);
                        command.Parameters.AddWithValue(@"tglLahir", tglLahir.Text);
                        command.Parameters.AddWithValue(@"nohp", nohp.Text);
                        command.Parameters.AddWithValue(@"email", email.Text);
                        command.Parameters.AddWithValue(@"tglMasuk", tanggalmasuk.Text);
                        command.Parameters.AddWithValue(@"jenisKel", jenisKel.Text);
                        command.Parameters.AddWithValue(@"alamat", alamat.Text);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            reset();
            databind();
        }

        private void email_valid(object sender, CancelEventArgs e)
        {
            Regex reg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            if (email.Text.Length > 0)
            {
                if (!reg.IsMatch(email.Text))
                {
                    MessageBox.Show("Masukkan Email Dengan Benar !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    email.SelectAll();
                    e.Cancel = true;
                }
            }
        }

        private void nohp_key(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void nama_key(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);

        }

        private void no_val(object sender, CancelEventArgs e)
        {
            if (nohp.TextLength < 9)
            {
                MessageBox.Show("Input hanya 9 - 14 Karakter", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nohp.Text = "";
                nohp.Focus();
            }
            else if (nohp.TextLength > 14)
            {
                MessageBox.Show("Input hanya 9 - 14 Karakter", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nohp.Text = "";
                nohp.Focus();
            }
        }

        
    }
}
