using Microsoft.VisualBasic;
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
    public partial class kitapekle : Form
    {
        public kitapekle()
        {
            InitializeComponent();
        }
        
        MySqlConnection bağlanti = new MySqlConnection("server=localhost;user=root;database=proje;port=3306;password=secret");

        private void kitapekle_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bağlanti.Open();

            MySqlCommand komut = new MySqlCommand("insert into kitap(barkodno,kitapadı,yazarı,yayınevi,sayfasayısı,turu,stoksayısı,rafno,kayıttarihi) values(@barkodno,@kitapadı,@yazarı,@yayınevi,@sayfasayısı,@turu,@stoksayısı,@rafno,@kayıttarihi)", bağlanti);
            komut.Parameters.AddWithValue("@barkodno",txtBarkodNo.Text);
            komut.Parameters.AddWithValue("@kitapadı",txtKitapAdı.Text);
            komut.Parameters.AddWithValue("@yazarı", txtYazar.Text);
            komut.Parameters.AddWithValue("@yayınevi", txtYayınevi.Text);
            komut.Parameters.AddWithValue("@sayfasayısı", txtSayfaSayısı.Text);
            komut.Parameters.AddWithValue("@turu", comboTuru.Text);
            komut.Parameters.AddWithValue("@stoksayısı", txtStokSayısı.Text);
            komut.Parameters.AddWithValue("@rafno", txtRafNo.Text);
            komut.Parameters.AddWithValue("@kayıttarihi",DateAndTime.Now.ToShortDateString());

            komut.ExecuteNonQuery();
            bağlanti.Close();
            MessageBox.Show("Kitap kaydı yapıldı.");
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
