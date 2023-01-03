using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kütüphane
{
    public partial class Sıralama : Form
    {
        public Sıralama()
        {
            InitializeComponent();
        }
        MySqlConnection bağlanti = new MySqlConnection("server=localhost;user=root;database=proje;port=3306;password=secret");
        DataSet daset = new DataSet();
        private void Sıralama_Load(object sender, EventArgs e)
        {
            bağlanti.Open();
            MySqlDataAdapter adtr = new MySqlDataAdapter("select *from emanetkitaplar order by kitapsayısı desc", bağlanti);
            adtr.Fill(daset, "emanetkitaplar");
            dataGridView1.DataSource = daset.Tables["emanetkitaplar"];
            bağlanti.Close();
        }
    }
}
