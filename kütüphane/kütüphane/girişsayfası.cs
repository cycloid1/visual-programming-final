using System.Data.SqlClient;

namespace kütüphane
{
    public partial class Girişsayfası : Form
    {
        public Girişsayfası()
        {
            InitializeComponent();
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            kayıtol kayıt = new kayıtol();
            kayıt.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "admin" && textBox2.Text == "123")
            {
                Ana_sayfa anasayfa = new Ana_sayfa();
                anasayfa.Show();
            }
        }
    }
}