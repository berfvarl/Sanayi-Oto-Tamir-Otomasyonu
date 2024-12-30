using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SanayiOtoTamir
{
    public partial class Raporlar : Form
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=Berfin;Initial Catalog=ototamirsanayiDB;Integrated Security=True");

        public Raporlar()
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
                SqlCommand komut = new SqlCommand("SELECT DISTINCT MusteriID, adsoyad FROM Tbl_AracKayit", baglanti);
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    cmbMusteri.Items.Add(new { Text = dr["adsoyad"].ToString(), Value = dr["MusteriID"].ToString() });
                }
                cmbMusteri.DisplayMember = "Text";
                cmbMusteri.ValueMember = "Value";
                dr.Close();
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


        // Faturaları listele
        //private void btnListele_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        baglanti.Open();
        //        string sorgu = "SELECT FaturaID, adsoyad AS Musteri, FaturaTarihi, ToplamTutar, Aciklama " +
        //                       "FROM Tbl_Faturalar " +
        //                       "INNER JOIN Tbl_AracKayit ON Tbl_Faturalar.MusteriID = Tbl_AracKayit.MusteriID " +
        //                       "WHERE (@MusteriID IS NULL OR Tbl_Faturalar.MusteriID = @MusteriID) " +
        //                       "AND FaturaTarihi BETWEEN @BaslangicTarihi AND @BitisTarihi";

        //        SqlCommand komut = new SqlCommand(sorgu, baglanti);
        //        komut.Parameters.AddWithValue("@MusteriID", cmbMusteri.SelectedItem == null ? (object)DBNull.Value : ((dynamic)cmbMusteri.SelectedItem).Value);
        //        komut.Parameters.AddWithValue("@BaslangicTarihi", dtpBaslangic.Value);
        //        komut.Parameters.AddWithValue("@BitisTarihi", dtpBitis.Value);

        //        SqlDataAdapter da = new SqlDataAdapter(komut);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        dataGridView1.DataSource = dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Hata: " + ex.Message);
        //    }
        //    finally
        //    {
        //        baglanti.Close();
        //    }
        //}

        //private void btnListele_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        baglanti.Open();

        //        // Fatura verilerini getir
        //        string sorgu = "SELECT FaturaID, adsoyad AS Musteri, FaturaTarihi, ToplamTutar, Aciklama " +
        //                       "FROM Tbl_Faturalar " +
        //                       "INNER JOIN Tbl_AracKayit ON Tbl_Faturalar.MusteriID = Tbl_AracKayit.MusteriID " +
        //                       "WHERE (@MusteriID IS NULL OR Tbl_Faturalar.MusteriID = @MusteriID) " +
        //                       "AND FaturaTarihi BETWEEN @BaslangicTarihi AND @BitisTarihi";

        //        SqlCommand komut = new SqlCommand(sorgu, baglanti);
        //        komut.Parameters.AddWithValue("@MusteriID", cmbMusteri.SelectedItem == null ? (object)DBNull.Value : ((dynamic)cmbMusteri.SelectedItem).Value);
        //        komut.Parameters.AddWithValue("@BaslangicTarihi", dtpBaslangic.Value);
        //        komut.Parameters.AddWithValue("@BitisTarihi", dtpBitis.Value);

        //        SqlDataAdapter da = new SqlDataAdapter(komut);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        dataGridView1.DataSource = dt;

        //        // Özet bilgileri getir
        //        string ozetSorgu = "SELECT COUNT(FaturaID) AS FaturaSayisi, SUM(ToplamTutar) AS ToplamTutar " +
        //                           "FROM Tbl_Faturalar " +
        //                           "WHERE (@MusteriID IS NULL OR MusteriID = @MusteriID) " +
        //                           "AND FaturaTarihi BETWEEN @BaslangicTarihi AND @BitisTarihi";

        //        SqlCommand ozetKomut = new SqlCommand(ozetSorgu, baglanti);
        //        ozetKomut.Parameters.AddWithValue("@MusteriID", cmbMusteri.SelectedItem == null ? (object)DBNull.Value : ((dynamic)cmbMusteri.SelectedItem).Value);
        //        ozetKomut.Parameters.AddWithValue("@BaslangicTarihi", dtpBaslangic.Value);
        //        ozetKomut.Parameters.AddWithValue("@BitisTarihi", dtpBitis.Value);

        //        SqlDataReader ozetReader = ozetKomut.ExecuteReader();
        //        if (ozetReader.Read())
        //        {
        //            lblFaturaSayisi.Text = $"Fatura Sayısı: {ozetReader["FaturaSayisi"]}";
        //            lblToplamTutar.Text = $"Toplam Tutar: {ozetReader["ToplamTutar"]:C}";
        //        }
        //        ozetReader.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Hata: " + ex.Message);
        //    }
        //    finally
        //    {
        //        baglanti.Close();
        //    }
        //}


        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Listele_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();

                // Fatura verilerini getir
                string sorgu = "SELECT FaturaID, adsoyad AS Musteri, FaturaTarihi, ToplamTutar, Aciklama " +
                               "FROM Tbl_Faturalar " +
                               "INNER JOIN Tbl_AracKayit ON Tbl_Faturalar.MusteriID = Tbl_AracKayit.MusteriID " +
                               "WHERE (@MusteriID IS NULL OR Tbl_Faturalar.MusteriID = @MusteriID) " +
                               "AND FaturaTarihi BETWEEN @BaslangicTarihi AND @BitisTarihi";

                SqlCommand komut = new SqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@MusteriID", cmbMusteri.SelectedItem == null ? (object)DBNull.Value : ((dynamic)cmbMusteri.SelectedItem).Value);
                komut.Parameters.AddWithValue("@BaslangicTarihi", dtpBaslangic.Value);
                komut.Parameters.AddWithValue("@BitisTarihi", dtpBitis.Value);

                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                // Özet bilgileri getir
                string ozetSorgu = "SELECT COUNT(FaturaID) AS FaturaSayisi, SUM(ToplamTutar) AS ToplamTutar " +
                                   "FROM Tbl_Faturalar " +
                                   "WHERE (@MusteriID IS NULL OR MusteriID = @MusteriID) " +
                                   "AND FaturaTarihi BETWEEN @BaslangicTarihi AND @BitisTarihi";

                SqlCommand ozetKomut = new SqlCommand(ozetSorgu, baglanti);
                ozetKomut.Parameters.AddWithValue("@MusteriID", cmbMusteri.SelectedItem == null ? (object)DBNull.Value : ((dynamic)cmbMusteri.SelectedItem).Value);
                ozetKomut.Parameters.AddWithValue("@BaslangicTarihi", dtpBaslangic.Value);
                ozetKomut.Parameters.AddWithValue("@BitisTarihi", dtpBitis.Value);

                SqlDataReader ozetReader = ozetKomut.ExecuteReader();
                if (ozetReader.Read())
                {
                    lblFaturaSayisi.Text = $"Fatura Sayısı: {ozetReader["FaturaSayisi"]}";
                    lblToplamTutar.Text = $"Toplam Tutar: {ozetReader["ToplamTutar"]:C}";
                }
                ozetReader.Close();
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
    }
}


