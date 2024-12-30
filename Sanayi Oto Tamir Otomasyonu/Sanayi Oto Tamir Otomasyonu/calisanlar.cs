using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SanayiOtoTamir
{
    public partial class calisanlar : Form
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=Berfin;Initial Catalog=ototamirsanayiDB;Integrated Security=True");

        public calisanlar()
        {
            InitializeComponent();
            CalisanlariGetir();
        }

        // Çalışanları DataGridView'e yükleme
        private void CalisanlariGetir()
        {
            try
            {
                baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Tbl_Calisan", baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridCalisanlar.DataSource = dt;
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

        private void btnKaydet_Click_1(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("INSERT INTO Tbl_Calisan (Ad, Soyad, Telefon, Email, Departman, Cinsiyet, IseGirisTarihi, Maas) VALUES (@Ad, @Soyad, @Telefon, @Email, @Departman, @Cinsiyet, @IseGirisTarihi, @Maas)", baglanti);
                komut.Parameters.AddWithValue("@Ad", txtCalisanAd.Text);
                komut.Parameters.AddWithValue("@Soyad", txtCalisanSoyad.Text);
                komut.Parameters.AddWithValue("@Telefon", txtTelefon.Text);
                komut.Parameters.AddWithValue("@Email", txtEmail.Text);
                komut.Parameters.AddWithValue("@Departman", txtDepartman.Text);
                komut.Parameters.AddWithValue("@Cinsiyet", cmbCinsiyet.SelectedItem?.ToString());
                komut.Parameters.AddWithValue("@IseGirisTarihi", dtpBaslangic.Value);
                komut.Parameters.AddWithValue("@Maas", txtMaas.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Çalışan başarıyla eklendi.");
                CalisanlariGetir();
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

        private void btnGuncelle_Click_1(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("UPDATE Tbl_Calisan SET Ad=@Ad, Soyad=@Soyad, Telefon=@Telefon, Email=@Email, Departman=@Departman, Cinsiyet=@Cinsiyet, IseGirisTarihi=@IseGirisTarihi, Maas=@Maas WHERE CalisanID=@CalisanID", baglanti);
                komut.Parameters.AddWithValue("@Ad", txtCalisanAd.Text);
                komut.Parameters.AddWithValue("@Soyad", txtCalisanSoyad.Text);
                komut.Parameters.AddWithValue("@Telefon", txtTelefon.Text);
                komut.Parameters.AddWithValue("@Email", txtEmail.Text);
                komut.Parameters.AddWithValue("@Departman", txtDepartman.Text);
                komut.Parameters.AddWithValue("@Cinsiyet", cmbCinsiyet.SelectedItem?.ToString());
                komut.Parameters.AddWithValue("@IseGirisTarihi", dtpBaslangic.Value);
                komut.Parameters.AddWithValue("@Maas", txtMaas.Text);
                komut.Parameters.AddWithValue("@CalisanID", dataGridCalisanlar.CurrentRow.Cells["CalisanID"].Value);
                komut.ExecuteNonQuery();
                MessageBox.Show("Çalışan bilgisi başarıyla güncellendi.");
                CalisanlariGetir();
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

        private void btnSil_Click_1(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("DELETE FROM Tbl_Calisan WHERE CalisanID=@CalisanID", baglanti);
                komut.Parameters.AddWithValue("@CalisanID", dataGridCalisanlar.CurrentRow.Cells["CalisanID"].Value);
                komut.ExecuteNonQuery();
                MessageBox.Show("Çalışan başarıyla silindi.");
                CalisanlariGetir();
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

        // Yeni buton için SQL sorgusunu çalıştıran metod
        //private void btnQueryExecute_Click(object sender, EventArgs e)
        //{
        //    string query = txtQuery.Text; // TextBox'tan sorguyu al

        //    // Veritabanına bağlanarak sorguyu çalıştırıyoruz
        //    try
        //    {
        //        baglanti.Open();
        //        SqlDataAdapter da = new SqlDataAdapter(query, baglanti);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        dataGridCalisanlar.DataSource = dt; // DataGridView'a veri yüklüyoruz
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

        private void btnQueryExecute_Click_1(object sender, EventArgs e)
        {
            string query = txtQuery.Text; // TextBox'tan sorguyu al

            // Veritabanına bağlanarak sorguyu çalıştırıyoruz
            try
            {
                baglanti.Open();
                SqlDataAdapter da = new SqlDataAdapter(query, baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridCalisanlar.DataSource = dt; // DataGridView'a veri yüklüyoruz
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


