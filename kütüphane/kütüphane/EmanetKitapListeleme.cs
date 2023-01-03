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
    public partial class EmanetKitapListeleme : Form
    {
        public EmanetKitapListeleme()
        {
            InitializeComponent();
        }
        MySqlConnection bağlanti = new MySqlConnection("server=localhost;user=root;database=proje;port=3306;password=secret");
        DataSet daset = new DataSet();

        private void EmanetKitapListeleme_Load(object sender, EventArgs e)
        {
            emanetlistele();
            comboBox1.SelectedIndex = 0;
        }

        private void emanetlistele()
        {
            bağlanti.Open();
            MySqlDataAdapter adtr = new MySqlDataAdapter("select *from emanetkitaplar", bağlanti);
            adtr.Fill(daset, "emanetkitaplar");
            dataGridView1.DataSource = daset.Tables["emanetkitaplar"];
            bağlanti.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            daset.Tables["emanetkitaplar"].Clear();
            if (comboBox1.SelectedIndex==0)
            {
                emanetlistele();
            }
            else if (comboBox1.SelectedIndex==1)
            {
                bağlanti.Open();
                MySqlDataAdapter adtr = new MySqlDataAdapter("select *from emanetkitaplar where '"+DateAndTime.Now.ToShortDateString()+"'>iadetarihi", bağlanti);
                adtr.Fill(daset, "emanetkitaplar");
                dataGridView1.DataSource = daset.Tables["emanetkitaplar"];
                bağlanti.Close();
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                bağlanti.Open();
                MySqlDataAdapter adtr = new MySqlDataAdapter("select *from emanetkitaplar where '" + DateAndTime.Now.ToShortDateString() + "'<=iadetarihi", bağlanti);
                adtr.Fill(daset, "emanetkitaplar");
                dataGridView1.DataSource = daset.Tables["emanetkitaplar"];
                bağlanti.Close();
            }
        }
    }
}
