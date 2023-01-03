using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySqlConnector;


namespace kütüphane
{
    public partial class kayıtol : Form
    {
        public kayıtol()
        {
            InitializeComponent();
            string connStr = "server=localhost;user=root;database=proje;port=3306;password=secret";
            MySqlConnection bağlanti = new MySqlConnection(connStr);
           

        }
        

        private void Form2_Load(object sender, EventArgs e )
        {
           

        }

        private void btnKayıtol_Click(object sender, EventArgs e)
        {
            string connStr = "server=localhost;user=root;database=proje;port=3306;password=secret";
            MySqlConnection bağlanti = new MySqlConnection(connStr);

            bağlanti.Open();

            MySqlCommand komut = new MySqlCommand("insert into uyeekle(tc,Ad,Soyad,Yaş,Cinsiyet,Telefon,Email) values(@tc,@Ad,@Soyad,@Yaş,@Cinsiyet,@Telefon,@Email)",bağlanti);
            komut.Parameters.AddWithValue("@tc", txtTC.Text);
            komut.Parameters.AddWithValue("@Ad", txtAdı.Text);
            komut.Parameters.AddWithValue("@Soyad", txtSoyadı.Text);
            komut.Parameters.AddWithValue("@Yaş", txtYaş.Text);
            komut.Parameters.AddWithValue("@Cinsiyet", txtCinsiyet.Text);
            komut.Parameters.AddWithValue("@Telefon", txtTelefon.Text);
            komut.Parameters.AddWithValue("@Email", txtEmail.Text);
            komut.ExecuteNonQuery();
            bağlanti.Close();
            MessageBox.Show("Kayıt işlemi başarılı bir şekilde gerçekleşti.");
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {

                    item.Text = "";
                }
            }
        }
    }
}
