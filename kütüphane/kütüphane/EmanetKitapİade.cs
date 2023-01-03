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
    public partial class EmanetKitapİade : Form
    {
        public EmanetKitapİade()
        {
            InitializeComponent();
        }
        MySqlConnection bağlanti = new MySqlConnection("server=localhost;user=root;database=proje;port=3306;password=secret");
        DataSet daset = new DataSet();

        private void emanetlistele()
        {
            bağlanti.Open();
            MySqlDataAdapter adtr = new MySqlDataAdapter("select *from emanetkitaplar", bağlanti);
            adtr.Fill(daset, "emanetkitaplar");
            dataGridView1.DataSource = daset.Tables["emanetkitaplar"];
            bağlanti.Close();
        }

        private void EmanetKitapİade_Load(object sender, EventArgs e)
        {
            emanetlistele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["emanetkitaplar"].Clear();
            bağlanti.Open();
            MySqlDataAdapter adtr = new MySqlDataAdapter("select *from emanetkitaplar where tc like '%"+txtTC.Text+"'",bağlanti);
            adtr.Fill(daset,"emanetkitaplar");
            bağlanti.Close();
            if (txtTC.Text=="")
            {
                daset.Tables["emanetkitaplar"].Clear();
                emanetlistele();
            }
        }

        private void txtBarkodNo_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["emanetkitaplar"].Clear();
            bağlanti.Open();
            MySqlDataAdapter adtr = new MySqlDataAdapter("select *from emanetkitaplar where barkodno like '%" + txtBarkodNo.Text + "'", bağlanti);
            adtr.Fill(daset, "emanetkitaplar");
            bağlanti.Close();
            if (txtBarkodNo.Text == "")
            {
                daset.Tables["emanetkitaplar"].Clear();
                emanetlistele();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bağlanti.Open();
            MySqlCommand komut = new MySqlCommand("delete from emanetkitaplar where tc=@tc and barkodno=@barkodno",bağlanti);
            komut.Parameters.AddWithValue("@tc", dataGridView1.CurrentRow.Cells["tc"].Value.ToString());
            komut.Parameters.AddWithValue("@barkodno", dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString());
            komut.ExecuteNonQuery();
            MySqlCommand komut2 = new MySqlCommand("update kitap set stoksayısı=stoksayısı+'" + dataGridView1.CurrentRow.Cells["kitapsayısı"].Value.ToString()+"' where barkodno=@barkodno",bağlanti);
            komut2.Parameters.AddWithValue("@barkodno", dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString());
            komut2.ExecuteNonQuery();
            bağlanti.Close();
            MessageBox.Show("Kitaplar iade edildi.");
            daset.Tables["emanetkitaplar"].Clear();
            emanetlistele();
        }
    }
}
