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
    public partial class KitapListeleme : Form
    {
        public KitapListeleme()
        {
            InitializeComponent();
        }
        MySqlCommand komut;
        void bilgilerigetir()
        {


            bağlanti = new MySqlConnection("server = localhost; user=root;database=proje;port=3306;password=secret");
            bağlanti.Open();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT *FROM kitap", bağlanti);
            DataTable tablo = new();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            bağlanti.Close();


        }

        MySqlConnection bağlanti = new MySqlConnection("server=localhost;user=root;database=proje;port=3306;password=secret");
        DataSet daset = new DataSet();
        private void kitaplistele()
        {
            bağlanti.Open();
            MySqlDataAdapter adtr = new MySqlDataAdapter("select * from kitap", bağlanti);
            adtr.Fill(daset, "kitap");
            dataGridView1.DataSource = daset.Tables["kitap"];
            bağlanti.Close();
        }

        private void KitapListeleme_Load(object sender, EventArgs e)
        {
            kitaplistele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            bağlanti.Open();
            string sorgu = "DELETE FROM kitap WHERE barkodno=@barkodno";
            komut = new MySqlCommand(sorgu, bağlanti);
            komut.Parameters.AddWithValue("@barkodno", Convert.ToString(txtBarkodNo.Text));
            komut.ExecuteNonQuery();
            bağlanti.Close();
            MessageBox.Show("Silmek istediğine emin misiniz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            bilgilerigetir();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {

            bağlanti.Open();

            komut = new MySqlCommand("UPDATE kitap SET kitapadı=@kitapadı,yazarı=@yazarı,yayınevi=@yayınevi,sayfasayısı=@sayfasayısı,turu=@turu,stoksayısı=@stoksayısı,rafno=@rafno WHERE barkodno=@barkodno ", bağlanti);

            komut.Parameters.AddWithValue("@barkodno", txtBarkodNo.Text);
            komut.Parameters.AddWithValue("@kitapadı", txtKitapAdı.Text);
            komut.Parameters.AddWithValue("@yazarı", txtYazar.Text);
            komut.Parameters.AddWithValue("@yayınevi", txtYayınevi.Text);
            komut.Parameters.AddWithValue("@sayfasayısı", txtSayfaSayısı.Text);
            komut.Parameters.AddWithValue("@turu", comboTuru.Text);
            komut.Parameters.AddWithValue("@stoksayısı", txtStokSayısı.Text);
            komut.Parameters.AddWithValue("@rafno", txtRafNo.Text);

            komut.ExecuteNonQuery();
            bağlanti.Close();
            MessageBox.Show("Güncellleme işlemi gerçekleşti");
            bilgilerigetir();
        }

        private void txtBarkodAra_TextChanged(object sender, EventArgs e)
        {
            bağlanti.Open();
            string sorgu = "Select * from kitap where barkodno Like '%" + txtBarkodAra.Text + "%'";
            MySqlDataAdapter da = new MySqlDataAdapter(sorgu, bağlanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            bağlanti.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtBarkodNo.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtKitapAdı.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtYazar.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtYayınevi.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtSayfaSayısı.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            comboTuru.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtStokSayısı.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txtRafNo.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
