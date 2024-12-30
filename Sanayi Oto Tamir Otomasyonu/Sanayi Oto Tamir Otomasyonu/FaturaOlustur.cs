using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SanayiOtoTamir
{
    public partial class FaturaOlustur : Form
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=Berfin;Initial Catalog=ototamirsanayiDB;Integrated Security=True");

        public FaturaOlustur()
        {
            InitializeComponent();
            MusteriListesiGetir();
        }

        // Müşteri listesini ComboBox'a doldur
        private void MusteriListesiGetir()
        {
            try
            {
                baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT MusteriID, adsoyad FROM Tbl_AracKayit", baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbMusteri.DataSource = dt;
                cmbMusteri.DisplayMember = "adsoyad";
                cmbMusteri.ValueMember = "MusteriID";
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


        // Fatura kaydetme işlemi
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (cmbMusteri.SelectedValue == null)
            {
                MessageBox.Show("Lütfen bir müşteri seçin.");
                return;
            }

            try
            {
                baglanti.Open();

                string sorgu = "INSERT INTO Tbl_Faturalar (MusteriID, FaturaTarihi, ToplamTutar, Aciklama) " +
                               "VALUES (@MusteriID, @FaturaTarihi, @ToplamTutar, @Aciklama)";

                SqlCommand komut = new SqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@MusteriID", cmbMusteri.SelectedValue);
                komut.Parameters.AddWithValue("@FaturaTarihi", dtpFaturaTarihi.Value);
                komut.Parameters.AddWithValue("@ToplamTutar", decimal.Parse(txtToplamTutar.Text));
                komut.Parameters.AddWithValue("@Aciklama", richtxtAciklama.Text);

                komut.ExecuteNonQuery();
                MessageBox.Show("Fatura başarıyla kaydedildi.");
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

