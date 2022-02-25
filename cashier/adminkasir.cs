using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tugas1
{
    public partial class adminkasir : Form
    {
        public adminkasir()
        {
            InitializeComponent();

            btnbatal.Enabled = false;
            btnsimpan.Enabled = false;
            btnhapus.Enabled = false;

            var select = "SELECT id as Id, nama as Nama, password as Password, tglLahir as TglLahir, nohp as No_HP, email as Email, tglMasuk as TglMasuk, jenisKel as Gender, alamat as Alamat, status FROM login WHERE status = 'kasir'";

            string strConnection = Properties.Settings.Default.tugas1dbConnectionString;
            SqlConnection con = new SqlConnection(strConnection);
            var dataAdapter = new SqlDataAdapter(select, con);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            tabelkasir.DataSource = ds.Tables[0];
            tabelkasir.ReadOnly = true;

            
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Hide();
            admindef def = new admindef();
            def.ShowDialog();
        }

        private void tabelkasir_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.tabelkasir.Rows[e.RowIndex];

                id.Text = row.Cells[0].Value.ToString();
                nama.Text = row.Cells[1].Value.ToString();
                password.Text = row.Cells[2].Value.ToString();
                tglLahir.Text = row.Cells[3].Value.ToString();
                nohp.Text = row.Cells[4].Value.ToString();
                email.Text = row.Cells[5].Value.ToString();
                tanggalmasuk.Text = row.Cells[6].Value.ToString();
                jeniskel.Text = row.Cells[7].Value.ToString();
                alamat.Text = row.Cells[8].Value.ToString();
            }

            btntambah.Enabled = false;
            btnedit.Enabled = false;
            btnbatal.Enabled = true;
            btnsimpan.Enabled = true;
            btnhapus.Enabled = true;
        }

        private void btnhapus_Click(object sender, EventArgs e)
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
                    if(id.Text == "")
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
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            MessageBox.Show("Delete Berhasil !","Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            databind();
            reset();
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            btntambah.Enabled = false;
            btnedit.Enabled = false;
            btnsimpan.Enabled = true;
            btnbatal.Enabled = true;
            btnhapus.Enabled = true;
        }

        private void btnbatal_Click(object sender, EventArgs e)
        {
            btntambah.Enabled = true;
            btnedit.Enabled = true;
            btnsimpan.Enabled = false;
            btnhapus.Enabled = false;
            btnbatal.Enabled = false;

            reset();
        }

        private void btntambah_Click(object sender, EventArgs e)
        {
            if (nama.Text == "" || password.Text == "" || tglLahir.Text == "" || nohp.Text == "" || email.Text == "" || tanggalmasuk.Text == "" || jeniskel.Text == "")
            {
                MessageBox.Show("Isi Data Dengan Benar !");
            }
            else
            {
                var insert = @"insert into login(nama, password, tglLahir, nohp, email, tglMasuk, jenisKel, alamat, status) values(@nama, @password, @tglLahir, @nohp, @email, @tglMasuk, @jenisKel, @alamat, 'kasir')";


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
                        command.Parameters.AddWithValue(@"jenisKel", jeniskel.Text);
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

        private void databind()
        {
            var select = "SELECT id as ID, nama as Nama, password as Password, tglLahir as TglLahir, nohp as No_HP, email as Email, tglMasuk as TglMasuk, jenisKel as Gender, alamat as Alamat, status as Status FROM login WHERE status = 'kasir'";

            string strConnection = Properties.Settings.Default.tugas1dbConnectionString;
            SqlConnection con = new SqlConnection(strConnection);
            var dataAdapter = new SqlDataAdapter(select, con);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            tabelkasir.DataSource = ds.Tables[0];
            tabelkasir.ReadOnly = true;
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
            jeniskel.Text = "";
            alamat.Text = "";
        }

        private void email_(object sender, CancelEventArgs e)
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

        private void no_key(object sender, KeyPressEventArgs e)
        {
          
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
      
        }

        private void btnsimpan_Click(object sender, EventArgs e)
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
                    if(id.Text == "")
                    {
                        con.Open();
                        command = new SqlCommand(update1, con);
                        command.Parameters.AddWithValue(@"nama", nama.Text);
                        command.Parameters.AddWithValue(@"password", password.Text);
                        command.Parameters.AddWithValue(@"tglLahir", tglLahir.Text);
                        command.Parameters.AddWithValue(@"nohp", nohp.Text);
                        command.Parameters.AddWithValue(@"email", email.Text);
                        command.Parameters.AddWithValue(@"tglMasuk", tanggalmasuk.Text);
                        command.Parameters.AddWithValue(@"jenisKel", jeniskel.Text);
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
                        command.Parameters.AddWithValue(@"jenisKel", jeniskel.Text);
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

        private void nama_key(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void no_val(object sender, CancelEventArgs e)
        {
            if(nohp.TextLength < 9)
            {
                MessageBox.Show("Input hanya 9 - 14 Karakter", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nohp.Text = "";
                nohp.Focus();
            }else if(nohp.TextLength > 14)
            {
                MessageBox.Show("Input hanya 9 - 14 Karakter", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nohp.Text = "";
                nohp.Focus();
            }
        }

        private void password_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
