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
    public partial class FrmUrun : Form
    {
        public FrmUrun()
        {
            InitializeComponent();
        }

        DbEntityUrunEntities db = new DbEntityUrunEntities();
        private void FrmUrun_Load(object sender, EventArgs e)
        {
            var kategoriler = ( from x in db.Tbl_Kategori select new { x.ID, x.Ad }).ToList();
            //Arka planda çalışacak kısım id'ye göre çalışacak
            CmbKategori.ValueMember = "ID";
            //Ön kısımda bize gözücek olan da kategori adları olacak.
            CmbKategori.DisplayMember = "Ad";
            CmbKategori.DataSource = kategoriler;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtMarka.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            TxtStok.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtFiyat.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtDurum.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            CmbKategori.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();

        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = (from x in db.Tbl_Urun select new { x.UrunID, x.UrunAd, x.Marka, x.Stok, x.Fiyat, x.Tbl_Kategori.Ad, x.Durum }).ToList();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            Tbl_Urun t = new Tbl_Urun();
            t.UrunAd = TxtAd.Text;
            t.Marka = TxtMarka.Text;
            t.Stok = short.Parse(TxtStok.Text);
            t.Fiyat = decimal.Parse(TxtFiyat.Text);
            t.Durum = true;
            t.Kategori =int.Parse(CmbKategori.SelectedValue.ToString());
            db.Tbl_Urun.Add(t);
            db.SaveChanges();
            MessageBox.Show("Urun Kaydı Başarıyla Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt16(TxtID.Text);
            var kategori = db.Tbl_Urun.Find(id);
            db.Tbl_Urun.Remove(kategori);
            db.SaveChanges();
            MessageBox.Show("Urun Kaydı Başarıyla Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt16(TxtID.Text);
            var urun = db.Tbl_Urun.Find(id);
            urun.UrunAd = TxtAd.Text;
            urun.Stok = short.Parse(TxtStok.Text);
            urun.Marka = TxtMarka.Text;
            urun.Fiyat = decimal.Parse(TxtFiyat.Text);
            db.SaveChanges();
            MessageBox.Show("Urun Kaydı Başarıyla Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
