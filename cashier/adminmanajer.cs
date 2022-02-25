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
    public partial class adminmanajer : Form
    {
        public adminmanajer()
        {
            InitializeComponent();

            btnBatal.Enabled = false;
            btnSimpan.Enabled = false;
            btnHapus.Enabled = false;

            var select = "SELECT id as Id, nama as Nama, password as Password, tglLahir as TglLahir, nohp as No_HP, email as Email, tglMasuk as TglMasuk, jenisKel as Gender, alamat as Alamat, status as Status FROM login WHERE status = 'manajer'";

            string strConnection = Properties.Settings.Default.tugas1dbConnectionString;
            SqlConnection con = new SqlConnection(strConnection);
            var dataAdapter = new SqlDataAdapter(select, con);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            tabelmanajer.DataSource = ds.Tables[0];
            tabelmanajer.ReadOnly = true;

            nohp.MaxLength = 14;
            
        }

        private void databind()
        {
            var select = "SELECT id as Id, nama as Nama, password as Password, tglLahir as TglLahir, nohp as No_HP, email as Email, tglMasuk as TglMasuk, jenisKel as Gender, alamat as Alamat, status as Status FROM login WHERE status = 'manajer'";

            string strConnection = Properties.Settings.Default.tugas1dbConnectionString;
            SqlConnection con = new SqlConnection(strConnection);
            var dataAdapter = new SqlDataAdapter(select, con);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            tabelmanajer.DataSource = ds.Tables[0];
            tabelmanajer.ReadOnly = true;
        }

        private void reset()
        {
            id.Text = "";
            nama.Text = "";
            password.Text = "";
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

        private void btnBatal_Click(object sender, EventArgs e)
        {
            btnTambah.Enabled = true;
            btnEdit.Enabled = true;
            btnSimpan.Enabled = false;
            btnHapus.Enabled = false;
            btnBatal.Enabled = false;

            reset();
        }

        private void tabelmanajer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.tabelmanajer.Rows[e.RowIndex];

                id.Text = row.Cells[0].Value.ToString();
                nama.Text = row.Cells[1].Value.ToString();
                password.Text = row.Cells[2].Value.ToString();
                tglLahir.Text = row.Cells[3].Value.ToString();
                nohp.Text = row.Cells[4].Value.ToString();
                email.Text = row.Cells[5].Value.ToString();
                tanggalmasuk.Text = row.Cells[6].Value.ToString();
                jenisKel.Text = row.Cells[7].Value.ToString();
                alamat.Text = row.Cells[8].Value.ToString();
            }

            btnTambah.Enabled = false;
            btnEdit.Enabled = false;
            btnBatal.Enabled = true;
            btnSimpan.Enabled = true;
            btnHapus.Enabled = true;
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            var delete = "DELETE FROM login WHERE id = @id";
            var delete1 = "DELETE FROM login WHERE nama = @nama";

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
            btnBatal.Enabled = true;
            btnHapus.Enabled = true;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (nama.Text == "" || password.Text == "" || tglLahir.Text == "" || nohp.Text == "" || email.Text == "" || tanggalmasuk.Text == "" || jenisKel.Text == "")
            {
                MessageBox.Show("Isi Data Dengan Benar !");
            }
            else
            {
                var insert = @"insert into login(nama, password, tglLahir, nohp, email, tglMasuk, jenisKel, alamat, status) values(@nama, @password, @tglLahir, @nohp, @email, @tglMasuk, @jenisKel, @alamat, 'manajer')";


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
                        command.Parameters.AddWithValue(@"password", password.Text);
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
            var update = "UPDATE login SET nama=@nama, password=@password, tglLahir=@tglLahir, nohp=@nohp, email=@email, tglMasuk=@tglMasuk, jenisKel=@jenisKel, alamat=@alamat WHERE id=@id";
            var update1 = "UPDATE login SET password=@password, tglLahir=@tglLahir, nohp=@nohp, email=@email, tglMasuk=@tglMasuk, jenisKel=@jenisKel, alamat=@alamat WHERE nama=@nama";


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
                        command.Parameters.AddWithValue(@"password", password.Text);
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
                        command.Parameters.AddWithValue(@"password", password.Text);
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

        private void email_validate(object sender, CancelEventArgs e)
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
