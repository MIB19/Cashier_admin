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
    public partial class adminkelas : Form
    {
        public adminkelas()
        {
            InitializeComponent();

            btnbatal.Enabled = false;
            btnsimpan.Enabled = false;
            btnhapus.Enabled = false;

            var select = "SELECT kelas.id as Id, kelas.nama as Nama, kelas.harga as Harga, kelas.keterangan as Keterangan, kelas.wifi as WIFI, kelas.ac as AC, pengajar.nama FROM kelas, pengajar WHERE kelas.idPengajar = pengajar.id";
            string strConnection = Properties.Settings.Default.tugas1dbConnectionString;
            SqlConnection con = new SqlConnection(strConnection);
            var dataAdapter = new SqlDataAdapter(select, con);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            tabelkelas.DataSource = ds.Tables[0];
            tabelkelas.ReadOnly = true;
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Hide();
            admindef adm = new admindef();
            adm.ShowDialog();
        }

        private void adminkelas_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'tugas1dbDataSet.pengajar' table. You can move, or remove it, as needed.
            this.pengajarTableAdapter.Fill(this.tugas1dbDataSet.pengajar);
            // TODO: This line of code loads data into the 'tugas1dbDataSet.kelas' table. You can move, or remove it, as needed.
            this.kelasTableAdapter.Fill(this.tugas1dbDataSet.kelas);

        }

        private void tabelkelas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string ace = "";
            string wifii = "";
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.tabelkelas.Rows[e.RowIndex];

                id.Text = row.Cells[0].Value.ToString();
                nama.Text = row.Cells[1].Value.ToString();
                harga.Text = row.Cells[2].Value.ToString();
                
                keterangan.Text = row.Cells[3].Value.ToString();
                ace = row.Cells[5].Value.ToString();
                wifii = row.Cells[4].Value.ToString();

                if (wifii == "ya")
                {
                    wifi.Checked = true;
                }
                else
                {
                    wifi.Checked = false;
                }

                if (ace == "ya")
                {
                    AC.Checked = true;
                }
                else
                {
                    AC.Checked = false;
                }

            }

            btntambah.Enabled = false;
            btnedit.Enabled = false;
            btnbatal.Enabled = true;
            btnsimpan.Enabled = true;
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

        private void reset()
        {
            id.Text = "";
            nama.Text = "";
            harga.Text = "";
            pengajar.Text = "";
            keterangan.Text = "";
            AC.Checked = false;
            wifi.Checked = false;

        }

        private void btnhapus_Click(object sender, EventArgs e)
        {
            var delete = "DELETE FROM kelas WHERE id = @id";
            var delete1 = "DELETE FROM kelas WHERE nama = @nama";

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

        private void databind()
        {
            var select = "SELECT kelas.id as Id, kelas.nama as Nama, kelas.harga as Harga, kelas.keterangan as Keterangan, kelas.wifi as WIFI, kelas.ac as AC, pengajar.nama FROM kelas, pengajar WHERE kelas.idPengajar = pengajar.id";

            string strConnection = Properties.Settings.Default.tugas1dbConnectionString;
            SqlConnection con = new SqlConnection(strConnection);
            var dataAdapter = new SqlDataAdapter(select, con);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            tabelkelas.DataSource = ds.Tables[0];
            tabelkelas.ReadOnly = true;
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            btntambah.Enabled = false;
            btnedit.Enabled = false;
            btnsimpan.Enabled = true;
            btnbatal.Enabled = true;
            btnhapus.Enabled = true;
        }

        private void btntambah_Click(object sender, EventArgs e)
        {
            string wifii = "";
            string ace = "";
            if(AC.Checked == true)
            {
                ace = "ya";
            }
            else
            {
                ace = ""; 
            }

            if (wifi.Checked == true)
            {
                wifii = "ya";
            }
            else
            {
                wifii = "";
            }

            if (nama.Text == "" ||  harga.Text == "" || keterangan.Text == "")
            {
                MessageBox.Show("Isi Data Dengan Benar !");
            }
            else
            {
                var insert = @"insert into kelas(nama, harga, keterangan, wifi, ac, idpengajar) values(@nama, @harga, @keterangan, @wifi, @ac, @idPengajar)";


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
                        command.Parameters.AddWithValue(@"harga", harga.Text);
                        command.Parameters.AddWithValue(@"keterangan", keterangan.Text);
                        command.Parameters.AddWithValue(@"wifi", wifii);
                        command.Parameters.AddWithValue(@"ac", ace);
                        command.Parameters.AddWithValue(@"idPengajar", pengajar.SelectedValue);
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

        private void btnsimpan_Click(object sender, EventArgs e)
        {
            string wifii = "";
            string ace = "";
            if (AC.Checked == true)
            {
                ace = "ya";
            }
            else
            {
                ace = "";
            }

            if (wifi.Checked == true)
            {
                wifii = "ya";
            }
            else
            {
                wifii = "";
            }
            var update = "UPDATE kelas SET nama=@nama, harga=@harga, keterangan=@keterangan, wifi=@wifi, ac=@ac, idPengajar=@idPengajar WHERE id=@id";
            var update1 = "UPDATE kelas SET harga=@harga, keterangan=@keterangan, wifi=@wifi, ac=@ac, idPengajar=@idPengajar WHERE nama=@nama";


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
                        command.Parameters.AddWithValue(@"harga", harga.Text);
                        command.Parameters.AddWithValue(@"keterangan", keterangan.Text);
                        command.Parameters.AddWithValue(@"wifi", wifii);
                        command.Parameters.AddWithValue(@"ac", ace);
                        command.Parameters.AddWithValue(@"idPengajar", pengajar.SelectedValue);
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        con.Open();
                        command = new SqlCommand(update, con);
                        command.Parameters.AddWithValue(@"id", id.Text);
                        command.Parameters.AddWithValue(@"nama", nama.Text);
                        command.Parameters.AddWithValue(@"harga", harga.Text);
                        command.Parameters.AddWithValue(@"keterangan", keterangan.Text);
                        command.Parameters.AddWithValue(@"wifi", wifii);
                        command.Parameters.AddWithValue(@"ac", ace);
                        command.Parameters.AddWithValue(@"idPengajar", pengajar.SelectedValue);
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

        private void harga_keypress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void nama_key(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);

        }
    }
}
