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
    public partial class Ana_sayfa : Form
    {
        public Ana_sayfa()
        {
            InitializeComponent();


            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            kayıtol kayıt = new kayıtol();
            kayıt.ShowDialog();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Kullanıcılistele kullanıcıliste = new Kullanıcılistele();
            kullanıcıliste.ShowDialog();
        }

        private void Ana_sayfa_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            kitapekle kitapekle = new kitapekle();
            kitapekle.ShowDialog(); 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            KitapListeleme kitapListeleme = new KitapListeleme();
            kitapListeleme.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            EmanetKitap emanetKitap = new EmanetKitap();
            emanetKitap.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            EmanetKitapListeleme emanetkitaplistele = new EmanetKitapListeleme();
            emanetkitaplistele.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            EmanetKitapİade emanetkitapiade = new EmanetKitapİade();
            emanetkitapiade.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Sıralama sıralama = new Sıralama();
            sıralama.ShowDialog();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            HavaDurumu havadurumu = new HavaDurumu();
            havadurumu.ShowDialog();
        }
    }
}
