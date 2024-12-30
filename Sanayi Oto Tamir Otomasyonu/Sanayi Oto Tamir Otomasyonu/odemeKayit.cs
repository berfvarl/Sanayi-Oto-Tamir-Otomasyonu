using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SanayiOtoTamir
{
    public partial class odemeKayit : Form
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=Berfin;Initial Catalog=ototamirsanayiDB;Integrated Security=True");

        public odemeKayit()
        {
            InitializeComponent();
            MusteriVeAracBilgileriniGetir();
            OdemeBilgileriniGetir();
        }

        // Müşteri ve Araç bilgilerini ComboBox'lara dolduran metot
        private void MusteriVeAracBilgileriniGetir()
        {
            try
            {
                baglanti.Open();

                // Müşteri bilgilerini çek
                SqlCommand musteriKomut = new SqlCommand("SELECT DISTINCT adsoyad FROM Tbl_AracKayit", baglanti);
                SqlDataReader musteriOku = musteriKomut.ExecuteReader();
                while (musteriOku.Read())
                {
                    cmbMusteri.Items.Add(musteriOku["adsoyad"].ToString());
                }
                musteriOku.Close();

                // Araç bilgilerini çek
                SqlCommand aracKomut = new SqlCommand("SELECT DISTINCT plaka FROM Tbl_AracKayit", baglanti);
                SqlDataReader aracOku = aracKomut.ExecuteReader();
                while (aracOku.Read())
                {
                    cmbArac.Items.Add(aracOku["plaka"].ToString());
                }
                aracOku.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }

        // Ödeme bilgilerini DataGridView'e dolduran metot
        private void OdemeBilgileriniGetir()
        {
            try
            {
                baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Tbl_Odeme", baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGrid.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }

        // Kaydet butonuna tıklanınca yapılacak işlemler
        private void btnKaydet_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Veritabanı bağlantısını aç
                baglanti.Open();

                // SQL sorgusu oluştur
                string sorgu = "INSERT INTO Tbl_Odeme (plaka, Musteri, OdemeTutari, OdemeTarihi, OdemeTuru) " +
                               "VALUES (@plaka, @Musteri, @OdemeTutari, @OdemeTarihi, @OdemeTuru)";

                SqlCommand komut = new SqlCommand(sorgu, baglanti);

                // Formdan alınan değerleri parametrelere ekle
                komut.Parameters.AddWithValue("@Musteri", cmbMusteri.SelectedItem?.ToString());
                komut.Parameters.AddWithValue("@plaka", cmbArac.SelectedItem?.ToString());
                komut.Parameters.AddWithValue("@OdemeTarihi", odemeTarih.Value);
                komut.Parameters.AddWithValue("@OdemeTuru", odemeTur.SelectedItem?.ToString());
                komut.Parameters.AddWithValue("@OdemeTutari", txtName.Text);


                // Sorguyu çalıştır
                komut.ExecuteNonQuery();

                MessageBox.Show("Ödeme bilgisi başarıyla kaydedildi.");

                // Ödeme bilgilerini yeniden yükle
                OdemeBilgileriniGetir();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }



        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

