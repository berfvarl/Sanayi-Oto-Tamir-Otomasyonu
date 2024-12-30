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
    public partial class FrmAcilis : Form
    {
        public FrmAcilis()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=Berfin;Initial Catalog=ototamirsanayiDB;Integrated Security=True");
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
      
                //Kullanıcı Giriş İşlemleri
                baglanti.Open();
                SqlCommand sorgula = new SqlCommand("select KADI,KSİFRE, ADSOYAD from TBL_Kullanicilar WHERE KADI=@username AND KSİFRE=@password", baglanti);
                sorgula.Parameters.AddWithValue("@username", txtKullaniciAdi.Text);
                sorgula.Parameters.AddWithValue("@password", txtSifre.Text);
                SqlDataReader oku = sorgula.ExecuteReader();
                if (oku.Read()) //Okuma işlemi başarılı ise
                {
                //MessageBox.Show("Giriş Başarılı");
                Anaform frm = new Anaform(); //anaformu aç
            
                frm.Show(); //göster
                this.Hide();
            }
            else
            {
                MessageBox.Show("KULLANICI KAYDI BULUNAMADI! KULLANICI ADI YA DA ŞİFRE HATALI");

            }

            baglanti.Close();
        }
        }
    }
