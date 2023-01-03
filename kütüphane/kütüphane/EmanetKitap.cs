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
    public partial class EmanetKitap : Form
    {
        public EmanetKitap()
        {
            InitializeComponent();
        }
        MySqlConnection bağlanti = new MySqlConnection("server=localhost;user=root;database=proje;port=3306;password=secret");
        DataSet daset = new DataSet();
        private MySqlCommand komut5;

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void sepetlistele()
        {
            bağlanti.Open();
            MySqlDataAdapter adtr = new MySqlDataAdapter("select *from sepet", bağlanti);
            adtr.Fill(daset, "sepet");
            dataGridView1.DataSource = daset.Tables["sepet"];
            bağlanti.Close();
        }
        private void kitapsayısı()
        {
            bağlanti.Open();
            MySqlCommand komut = new MySqlCommand("select sum(kitapsayısı)from sepet", bağlanti);
            lblKitapSayı.Text = komut.ExecuteScalar().ToString();
            bağlanti.Close();
        }

        private void EmanetKitap_Load(object sender, EventArgs e)
        {
            sepetlistele();
            kitapsayısı();

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            bağlanti.Open();
            MySqlCommand komut = new MySqlCommand("insert into sepet(barkodno,kitapadı,yazarı,yayınevi,sayfasayısı,kitapsayısı,teslimtarihi,iadetarihi) values(@barkodno,@kitapadı,@yazarı,@yayınevi,@sayfasayısı,@kitapsayısı,@teslimtarihi,@iadetarihi)", bağlanti);
            komut.Parameters.AddWithValue("@barkodno", txtBarkodNo.Text);
            komut.Parameters.AddWithValue("@kitapadı", txtKitapAdı.Text);
            komut.Parameters.AddWithValue("@yazarı", txtYazarı.Text);
            komut.Parameters.AddWithValue("@yayınevi", txtYayınevi.Text);
            komut.Parameters.AddWithValue("@sayfasayısı", txtSayfaSayısı.Text);
            komut.Parameters.AddWithValue("@kitapsayısı", txtKitapSayısı.Text);
            komut.Parameters.AddWithValue("@teslimtarihi", dateTimePicker1.Text);
            komut.Parameters.AddWithValue("@iadetarihi", dateTimePicker2.Text);
            komut.ExecuteNonQuery();
            bağlanti.Close();
            MessageBox.Show("Kitaplar sepete eklendi","Ekleme İşlemi");
            daset.Tables["sepet"].Clear();
            sepetlistele();
            lblKitapSayı.Text = "";
            kitapsayısı();

            foreach(Control item in grpKitapBilgi.Controls)
            {
                if (item is TextBox)
                {
                   if(item!=txtKitapSayısı)
                    {
                        item.Text = "";
                    }
                }
            }
        }

        private void txtTC_TextChanged(object sender, EventArgs e)
        {
            bağlanti.Open();
            MySqlCommand komut = new MySqlCommand("select *from uyeekle where tc='"+txtTC.Text+"'", bağlanti);
            MySqlDataReader read = komut.ExecuteReader();
            while(read.Read())
            {
                txtAd.Text = read["Ad"].ToString();
                txtSoyadı.Text = read["Soyad"].ToString();
                txtTelefon.Text = read["Telefon"].ToString();
            }
            bağlanti.Close();

            bağlanti.Open();
            MySqlCommand komut2 = new MySqlCommand("select sum(kitapsayısı) from emanetkitaplar", bağlanti);
            lblKayıtlıKitapSayı.Text = komut2.ExecuteScalar().ToString();
            bağlanti.Close();
                

            if(txtTC.Text=="")
            {
                foreach(Control item in grpÜyeBilgi.Controls)
                {
                    if(item is TextBox)
                    {
                        item.Text = "";
                        lblKayıtlıKitapSayı.Text = "";
                    }
                    
                }
            }
        }

        private void txtBarkodNo_TextChanged(object sender, EventArgs e)
        {
            bağlanti.Open();
            MySqlCommand komut = new MySqlCommand("select *from kitap where barkodno like '"+txtBarkodNo.Text+"'" ,bağlanti);
            MySqlDataReader read=komut.ExecuteReader();
            while (read.Read())
            {
                txtKitapAdı.Text = read["kitapadı"].ToString();
                txtYazarı.Text = read["yazarı"].ToString();
                txtYayınevi.Text = read["yayınevi"].ToString();
                txtSayfaSayısı.Text = read["sayfasayısı"].ToString();

            }
            bağlanti.Close();
            if(txtBarkodNo.Text=="")
            {
                foreach (Control item in grpKitapBilgi.Controls)
                {
                    if (item is TextBox)
                    {
                        if (item != txtKitapSayısı)
                        {
                            item.Text = "";
                        }
                    }
                }
            }
            
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            bağlanti.Open();
            string sorgu = "DELETE FROM sepet WHERE barkodno=@barkodno";
            komut5 = new MySqlCommand(sorgu, bağlanti);
            komut5.Parameters.AddWithValue("@barkodno", Convert.ToString(txtBarkodNo.Text));
            komut5.ExecuteNonQuery();
            
            bağlanti.Close();
            MessageBox.Show("Silme işlemi başarıyla gerçekleşti","Silme işlemi");
            daset.Tables["sepet"].Clear();
            sepetlistele();
            lblKitapSayı.Text = "";
            kitapsayısı();


        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void btnTeslimEt_Click(object sender, EventArgs e)
        {
            if(lblKitapSayı.Text!="")
            {
                 
                
                    if (txtTC.Text!= "" && txtAd.Text!="" && txtSoyadı.Text!="" && txtTelefon.Text!="")
                        
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
                        {
                            bağlanti.Open();
                            MySqlCommand komut = new MySqlCommand("insert into emanetkitaplar(tc,ad,soyad,telefon,barkodno,kitapadı,yazarı,yayınevi,sayfasayısı,kitapsayısı,teslimtarihi,iadetarihi) values(@tc,@ad,@soyad,@telefon,@barkodno,@kitapadı,@yazarı,@yayınevi,@sayfasayısı,@kitapsayısı,@teslimtarihi,@iadetarihi)", bağlanti);

                            komut.Parameters.AddWithValue("@tc", txtTC.Text);
                            komut.Parameters.AddWithValue("@ad", txtAd.Text);
                            komut.Parameters.AddWithValue("@soyad", txtSoyadı.Text);
                            komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
                            komut.Parameters.AddWithValue("barkodno", dataGridView1.Rows[i].Cells["barkodno"].Value.ToString());
                            komut.Parameters.AddWithValue("kitapadı", dataGridView1.Rows[i].Cells["kitapadı"].Value.ToString());
                            komut.Parameters.AddWithValue("yazarı", dataGridView1.Rows[i].Cells["yazarı"].Value.ToString());
                            komut.Parameters.AddWithValue("yayınevi", dataGridView1.Rows[i].Cells["yayınevi"].Value.ToString());
                            komut.Parameters.AddWithValue("sayfasayısı", dataGridView1.Rows[i].Cells["sayfasayısı"].Value.ToString());
                            komut.Parameters.AddWithValue("kitapsayısı", int.Parse(dataGridView1.Rows[i].Cells["kitapsayısı"].Value.ToString()));
                            komut.Parameters.AddWithValue("teslimtarihi", dataGridView1.Rows[i].Cells["teslimtarihi"].Value.ToString());
                            komut.Parameters.AddWithValue("iadetarihi", dataGridView1.Rows[i].Cells["iadetarihi"].Value.ToString());
                            komut.ExecuteNonQuery();
                          
                            MySqlCommand komut3 = new MySqlCommand("update kitap set stoksayısı=stoksayısı-'" + int.Parse(dataGridView1.Rows[i].Cells["kitapsayısı"].Value.ToString()) + "' where barkodno='" + dataGridView1.Rows[i].Cells["barkodno"].Value.ToString()+ "' ", bağlanti);
                            komut3.ExecuteNonQuery();

                            bağlanti.Close();

                            bağlanti.Open();
                            MySqlCommand komut4 = new MySqlCommand("delete from  sepet", bağlanti);
                            komut4.ExecuteNonQuery();
                            bağlanti.Close();
                            MessageBox.Show("Kitaplar emanet edildi");
                            daset.Tables["sepet"].Clear();
                            sepetlistele();
                            txtTC.Text = "";
                            lblKitapSayı.Text = "";
                            kitapsayısı();
                            lblKayıtlıKitapSayı.Text = "";

                              




                        }



                        lblKitapSayı.Text = "";
                        kitapsayısı();
                    }
                    else
                    {
                        MessageBox.Show("Önce üye ismi seçmeniz gerekir!!! ","Uyarı");
                    }
                
                
                
                
            }
            else
            {
                MessageBox.Show("Önce sepete kitap eklenmelidir!!!", "Uyarı");
            }





            lblKitapSayı.Text = "";
            kitapsayısı();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //txtBarkodNo.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            //txtKitapAdı.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            //txtYazarı.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            // txtYayınevi.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            //txtSayfaSayısı.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            // txtKitapSayısı.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            //dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            // dateTimePicker2.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
        }
    }
}
