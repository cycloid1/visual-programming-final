using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;



namespace kütüphane
{
    public partial class Kullanıcılistele : Form
    {
        public Kullanıcılistele() => InitializeComponent();

        MySqlCommand komut;

        MySqlConnection bağlanti = new MySqlConnection("server = localhost; user=root;database=proje;port=3306;password=secret");
        void bilgilerigetir()
        {
            

            bağlanti = new MySqlConnection("server = localhost; user=root;database=proje;port=3306;password=secret");
            bağlanti.Open();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT *FROM uyeekle", bağlanti);
            DataTable tablo = new();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            bağlanti.Close();


        }



        private void Form3_Load(object sender, EventArgs e)
        {
            bilgilerigetir();
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTC.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtYaş.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtCinsiyet.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtTelefon.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();

        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            bağlanti.Open();
            string sorgu = "UPDATE uyeekle SET Ad=@Ad,Soyad=@Soyad,Yaş=@Yaş,Cinsiyet=@Cinsiyet,Telefon=@Telefon,Email=@Email WHERE tc=@tc ";
            komut = new MySqlCommand(sorgu, bağlanti);
            
            komut.Parameters.AddWithValue("@Ad", txtAd.Text);
            komut.Parameters.AddWithValue("@Soyad", txtSoyad.Text);
            komut.Parameters.AddWithValue("@Yaş", txtYaş.Text);
            komut.Parameters.AddWithValue("@Cinsiyet", txtCinsiyet.Text);
            komut.Parameters.AddWithValue("@Telefon", txtTelefon.Text);
            komut.Parameters.AddWithValue("@Email", txtEmail.Text);
            komut.Parameters.AddWithValue("@tc", txtTC.Text);

            komut.ExecuteNonQuery();
            bağlanti.Close();
            bilgilerigetir();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            bağlanti.Open();
            string sorgu = "DELETE FROM uyeekle WHERE tc=@tc";
            komut = new MySqlCommand(sorgu, bağlanti);
            komut.Parameters.AddWithValue("@tc", Convert.ToString(txtTC.Text));
            komut.ExecuteNonQuery();
            bağlanti.Close();
            MessageBox.Show("Silmek istediğine emin misiniz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            bilgilerigetir();
        }
      DataSet daset = new();
        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            
            bağlanti.Open();
            string sorgu = "Select * from uyeekle where tc Like '%" + txtAra.Text + "%'";
            MySqlDataAdapter da = new MySqlDataAdapter(sorgu, bağlanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            bağlanti.Close();
        }

        private void btnİptal_Click(object sender, EventArgs e)
        {
            this.Close();
       
        
        }
            



    }
}
    


