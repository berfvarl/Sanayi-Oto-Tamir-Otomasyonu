using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sanayi_Oto_Tamir_Otomasyonu
{
    public partial class arackayitform : Form
    {
        public arackayitform()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=Berfin;Initial Catalog=ototamirsanayiDB;Integrated Security=True");
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            using (SqlCommand kayit = new SqlCommand("INSERT INTO Tbl_AracKayit (plaka, markamodel, uretimyili, renk, adsoyad, telefon, eposta, arizaturu, tamirkaydi, aracgeldigitti, MusteriID) VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)", baglanti))
            {
                kayit.Parameters.AddWithValue("@p1", textBox1.Text);
                kayit.Parameters.AddWithValue("@p2", textBox2.Text);
                kayit.Parameters.AddWithValue("@p3", textBox3.Text);
                kayit.Parameters.AddWithValue("@p4", textBox4.Text);
                kayit.Parameters.AddWithValue("@p5", textBox8.Text);
                kayit.Parameters.AddWithValue("@p6", textBox7.Text);
                kayit.Parameters.AddWithValue("@p7", textBox6.Text);
                kayit.Parameters.AddWithValue("@p8", textBox11.Text);
                kayit.Parameters.AddWithValue("@p9", textBox10.Text);
                kayit.Parameters.AddWithValue("@p10", textBox9.Text);
                kayit.Parameters.AddWithValue("@p11", textBox12.Text);
                kayit.ExecuteNonQuery();
            }


            MessageBox.Show("Araç Kayıt İşlemi Başarılı");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        
    }
}
