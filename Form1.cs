using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace EntityProjeUygulama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DbEntityUrunEntities db = new DbEntityUrunEntities();
        private void BtnListele_Click(object sender, EventArgs e)
        {
            var kategoriler = db.Tbl_Kategori.ToList();
            dataGridView1.DataSource = kategoriler;
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            Tbl_Kategori t = new Tbl_Kategori();
            t.Ad = txtAd.Text;
            db.Tbl_Kategori.Add(t);
            db.SaveChanges();
            MessageBox.Show("Kategori Kaydı Başarıyla Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt16(txtID.Text);
            var kategori = db.Tbl_Kategori.Find(id);
            db.Tbl_Kategori.Remove(kategori);
            db.SaveChanges();
            MessageBox.Show("Kategori Kaydı Başarıyla Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt16(txtID.Text);
            var kategori = db.Tbl_Kategori.Find(id);
            kategori.Ad = txtAd.Text;
            db.SaveChanges();
            MessageBox.Show("Kategori Kaydı Başarıyla Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
