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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();

            
        }

        string role = "";
        public bool IsAuthentic()
        {
            try
            {
                String strConnection = Properties.Settings.Default.tugas1dbConnectionString;

                SqlConnection conn = new SqlConnection(strConnection);

                conn.Open();
                SqlCommand command = new SqlCommand("select nama,password,status from login where nama='" + txtUsername.Text + "' AND password='" + txtPassword.Text + "'", conn);
               // command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if((txtUsername.Text == reader.GetString(0)) && (txtPassword.Text == reader.GetString(1)))
                        {
                            role = reader.GetString(2);
                            return true;
                        }
                    }
                }
                reader.Close();
                conn.Close();
            }
            catch(Exception xcp)
            {
                MessageBox.Show(xcp.ToString());
            }
            return false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (IsAuthentic())
            {
                MessageBox.Show("Login Berhasil", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                if(role == "admin")
                {
                    this.Hide();
                    admindef adm = new admindef();
                    adm.ShowDialog();
                } else if(role == "kasir")
                {
                    this.Hide();
                    Kasir ksr = new Kasir();
                    ksr.ShowDialog();
                }else if(role == "manajer")
                {
                    this.Hide();
                    manajer mjr = new manajer();
                    mjr.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("USERNAME ATAU PASSWORD ANDA SALAH !");
                txtUsername.Focus();
            }

            //if (txtUsername.Text == "" || txtPassword.Text == "")
            //{
            //    MessageBox.Show("Isi Username dan Password dengan Benar");
            //    txtUsername.Focus();
            //}
            //else
            //{
            //    String strConnection = Properties.Settings.Default.tugas1dbConnectionString;

            //    SqlConnection conn = new SqlConnection(strConnection);



            //    conn.Open();
            //    SqlCommand command = new SqlCommand("select nama,password,status from login where nama_lengkap='" + txtUsername.Text + "' AND nohp='" + txtPassword.Text + "'", conn);
            //    SqlDataAdapter DA = new SqlDataAdapter(command);
            //    DataTable dt = new DataTable();
            //    DA.Fill(dt);

            //    if (dt.Rows.Count > 0)
            //    {
            //        foreach (DataRow dr in dt.Rows)
            //        {
            //            if (dr["status"].ToString() == "admin")
            //            {
            //                MessageBox.Show("Login Sukses! Wellcome Siswa!");
            //                this.Hide();
            //                admindef adm = new admindef();
            //                adm.ShowDialog();
            //            }
            //            else if (dr["status"].ToString() == "2")
            //            {
            //                MessageBox.Show("Login Sukses! Wellcome Pengajar!");
            //                //this.Hide();
            //                //PengajarMenu fpengajar = new PengajarMenu();
            //                //fpengajar.ShowDialog();
            //            }
            //            else if (dr["status"].ToString() == "3")
            //            {
            //                MessageBox.Show("Login Sukses! Wellcome Manajer!");
            //                //this.Hide();
            //                //ManajerHome fmanajer = new ManajerHome();
            //                //fmanajer.ShowDialog();
            //            }
            //            else
            //            {
            //                MessageBox.Show("Masukkan Username/ Password dengan benar");
            //            }
            //        }
            //    }



            //}
        }

        
    }
}
