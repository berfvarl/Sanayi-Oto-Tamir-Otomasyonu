using System;
using SanayiOtoTamir;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sanayi_Oto_Tamir_Otomasyonu
{
    public partial class Anaform : Form
    {
        public Anaform()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            arackayitform frm = new arackayitform();
            frm.ShowDialog();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            TamirIslemleri frm = new TamirIslemleri();
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            odemeKayit frm = new odemeKayit();
            frm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            calisanlar frm = new calisanlar();
            frm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Raporlar frm = new Raporlar();
            frm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FaturaOlustur frm = new FaturaOlustur();
            frm.ShowDialog();
        }
    }
}
