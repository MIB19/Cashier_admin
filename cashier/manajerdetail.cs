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
using System.Windows.Forms.DataVisualization.Charting;

namespace Tugas1
{
    public partial class manajerdetail : Form
    {
        public manajerdetail()
        {
            InitializeComponent();

            var select = "SELECT kelas.nama as Kelas, COUNT(*) as Jumlah, SUM(totBiaya) as Pemasukan FROM transKelas, kelas WHERE transKelas.idKelas = kelas.id GROUP BY kelas.nama";

            string strConnection = Properties.Settings.Default.tugas1dbConnectionString;
            SqlConnection con = new SqlConnection(strConnection);
            var dataAdapter = new SqlDataAdapter(select, con);

            //var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            tabelKelas.DataSource = ds.Tables[0];
            tabelKelas.ReadOnly = true;

            chart1.Series["Jumlah"].XValueMember = "Kelas";
            chart1.Series["Jumlah"].YValueMembers = "Jumlah";
            chart1.DataSource = ds.Tables[0];
            chart1.DataBind();
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            manajerkelas mnj = new manajerkelas();
            mnj.ShowDialog();
        }
    }
}
