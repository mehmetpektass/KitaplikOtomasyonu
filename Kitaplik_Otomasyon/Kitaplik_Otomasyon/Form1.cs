using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Kitaplik_Otomasyon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection baglanti = new OleDbConnection
        ("Provider=Microsoft.ACE.OleDb.12.0;Data Source=Kitaplik.accdb");

        string durum = "";

        void listele()
        {
            DataTable dt=new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("select * From Kitaplar", baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

       
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand ekle_komutu = new OleDbCommand("insert into Kitaplar (KitapAd,Yazar,Tur,Sayfa,Durum) values (@a1,@a2,@a3,@a4,@a5)", baglanti);
            ekle_komutu.Parameters.AddWithValue("@a1", textBox2.Text);
            ekle_komutu.Parameters.AddWithValue("@a2", textBox3.Text);
            ekle_komutu.Parameters.AddWithValue("@a3", comboBox1.Text);
            ekle_komutu.Parameters.AddWithValue("@a4", textBox4.Text);
            ekle_komutu.Parameters.AddWithValue("@a5", durum);
            ekle_komutu.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ekleme İşlemi Başarıyla Gerçekleşti", "Yıldız Kütüphanesi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            durum = "0";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            durum = "1";
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            textBox1.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            comboBox1.Text=dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            if (dataGridView1.Rows[secilen].Cells[5].Value.ToString()=="False")
            {
                radioButton1.Checked = true;
               
            }
             else if (dataGridView1.Rows[secilen].Cells[5].Value.ToString() == "True")
            {
                radioButton2.Checked = true;
               
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand sil_komutu = new OleDbCommand("Delete From Kitaplar where KitapID=@a1", baglanti);
            sil_komutu.Parameters.AddWithValue("@a1", textBox1.Text);
            sil_komutu.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Silme İşlemi Başarıyla Gerçekleşti", "Yıldız Kütüphanesi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand guncelle_komutu = new OleDbCommand("Update Kitaplar set KitapAd=@a1,Yazar=@a2,Tur=@a3,Sayfa=@a4,Durum=@a5 where KitapID=@a6", baglanti);
            guncelle_komutu.Parameters.AddWithValue("@a1", textBox2.Text);
            guncelle_komutu.Parameters.AddWithValue("@a2", textBox3.Text);
            guncelle_komutu.Parameters.AddWithValue("@a3", comboBox1.Text);
            guncelle_komutu.Parameters.AddWithValue("@a4", textBox4.Text);
            guncelle_komutu.Parameters.AddWithValue("@a5", durum);
            guncelle_komutu.Parameters.AddWithValue("@a6", textBox1.Text);
            guncelle_komutu.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncelleme İşlemi Başarıyla Gerçekleşti", "Yıldız Kütüphanesi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OleDbCommand ara_komutu = new OleDbCommand("Select * From Kitaplar where KitapAd like '%" + textBox5.Text + "%'", baglanti);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(ara_komutu);
            da.Fill(dt);
            dataGridView1.DataSource=dt;
        }
    }
}
