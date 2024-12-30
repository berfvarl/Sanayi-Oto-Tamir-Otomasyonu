using System.Data.SqlClient;
using System.Windows.Forms;
using System;

namespace SanayiOtoTamir
{
    public partial class TamirIslemleri : Form
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=BERFIN;Initial Catalog=ototamirsanayiDB;Integrated Security=True");


        public TamirIslemleri()
        {
            InitializeComponent();
            ArizaTurleriniGetir(); // Form yüklendiğinde arıza türlerini listeye ekle
        }

        // Arıza türlerini SQL'den alıp lstTamirIslemleri'ne ekleyen metot
        private void ArizaTurleriniGetir()
        {
            try
            {
                // Veritabanı bağlantısını aç
                baglanti.Open();

                // SQL sorgusu oluştur
                string sorgu = "SELECT arizaturu FROM dbo.Tbl_AracKayit";

                SqlCommand komut = new SqlCommand(sorgu, baglanti);

                // SQL sorgusunu çalıştır ve sonuçları oku
                SqlDataReader okuyucu = komut.ExecuteReader();

                // lstTamirIslemleri'ni temizle (varsa eski veriler silinir)
                lstTamirIslemleri.Items.Clear();

                // Sonuçları lstTamirIslemleri'ne ekle
                while (okuyucu.Read())
                {
                    lstTamirIslemleri.Items.Add(okuyucu["arizaturu"].ToString());
                }

                okuyucu.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                // Veritabanı bağlantısını kapat
                baglanti.Close();
            }
        }

        private void btnHesapla_Click(object sender, EventArgs e)
        {
            try
            {
                // Başlangıç tarihi ve gün değerlerini al
                DateTime baslangicTarihi = dtpBaslangicTarihi.Value;
                int gun = int.Parse(txtGun.Text);

                // Tahmini bitiş tarihini hesapla
                DateTime bitisTarihi = baslangicTarihi.AddDays(gun);

                // Bitiş tarihini label5'e yazdır
                lblBitis.Text = bitisTarihi.ToString("dd MMMM yyyy");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                // Veritabanı bağlantısını aç
                baglanti.Open();

                // SQL sorgusu oluştur
                string sorgu = "INSERT INTO Tbl_TamirIslemleri (Durum, BaslangicTarihi, Gun, BitisTarihi, YapilacakIslemler) " +
                               "VALUES (@Durum, @BaslangicTarihi, @Gun, @BitisTarihi, @YapilacakIslemler)";

                SqlCommand komut = new SqlCommand(sorgu, baglanti);

                // Formdan alınan değerleri parametrelere ekle
                komut.Parameters.AddWithValue("@Durum", cmbDurum.SelectedItem?.ToString());
                komut.Parameters.AddWithValue("@BaslangicTarihi", dtpBaslangicTarihi.Value);
                komut.Parameters.AddWithValue("@Gun", int.Parse(txtGun.Text));
                komut.Parameters.AddWithValue("@BitisTarihi", lblBitis.Text);
                komut.Parameters.AddWithValue("@YapilacakIslemler", lstTamirIslemleri.Text);

                // Sorguyu çalıştır
                komut.ExecuteNonQuery();

                MessageBox.Show("Kayıt başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                // Bağlantıyı kapat
                baglanti.Close();
            }


        }

        private void btnHesapla_Click_1(object sender, EventArgs e)
        {

            try
            {
                // Başlangıç tarihini ve gün sayısını al
                DateTime baslangicTarihi = dtpBaslangicTarihi.Value;
                int gun;

                // Gün sayısının geçerli bir sayı olup olmadığını kontrol et
                if (!int.TryParse(txtGun.Text, out gun))
                {
                    MessageBox.Show("Lütfen geçerli bir gün sayısı girin.");
                    return;
                }

                // Tahmini bitiş tarihini hesapla
                DateTime bitisTarihi = baslangicTarihi.AddDays(gun);

                // Bitiş tarihini lblBitis'e yazdır
                lblBitis.Text = bitisTarihi.ToString("dd MMMM yyyy");
            }
            catch (Exception ex)
            {
                // Hata durumunda mesaj göster
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Veritabanı bağlantısını aç
                baglanti.Open();

                // SQL sorgusu oluştur
                string sorgu = "INSERT INTO Tbl_TamirIslemleri (Durum, BaslangicTarihi, Gun, BitisTarihi, YapilacakIslemler) " +
                               "VALUES (@Durum, @BaslangicTarihi, @Gun, @BitisTarihi, @YapilacakIslemler)";

                SqlCommand komut = new SqlCommand(sorgu, baglanti);

                // Formdan alınan değerleri parametrelere ekle
                komut.Parameters.AddWithValue("@Durum", cmbDurum.SelectedItem?.ToString()); // Tamir Durum Takibi
                komut.Parameters.AddWithValue("@BaslangicTarihi", dtpBaslangicTarihi.Value); // Başlangıç Tarihi
                komut.Parameters.AddWithValue("@Gun", int.Parse(txtGun.Text)); // Gün
                komut.Parameters.AddWithValue("@BitisTarihi", lblBitis.Text); // Bitiş Tarihi
                komut.Parameters.AddWithValue("@YapilacakIslemler", lstTamirIslemleri.SelectedItem?.ToString()); // Seçilen işlem

                // Sorguyu çalıştır
                komut.ExecuteNonQuery();

                // Başarılı işlem mesajı
                MessageBox.Show("Kayıt başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                // Hata mesajı
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                // Veritabanı bağlantısını kapat
                baglanti.Close();
            }

            if (cmbDurum.SelectedItem == null || string.IsNullOrWhiteSpace(txtGun.Text) || lstTamirIslemleri.SelectedItem == null)
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!");
                return;
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

