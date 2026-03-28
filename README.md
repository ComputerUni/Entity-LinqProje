# EntityProjeUygulama

Bu doküman, "EntityProjeUygulama" Windows Forms uygulamasının proje içeriğini ve çalışma mantığını ayrıntılı şekilde açıklar. Sadece proje ile ilgili teknik detaylar verilmiştir.

- Proje tipi: Windows Forms (C#)
- Hedef framework: .NET Framework 4.7.2
- C# sürümü: 7.3
- Veri erişimi: Entity Framework (Database First - EDMX)

Görseller (proje içinde `assets` veya `images` klasörü kullanılabilir):

- `assets/form1.png` - Kategori yönetimi ekranı
- `assets/frmUrun.png` - Ürün yönetimi ekranı
- `assets/frmIstatistik.png` - İstatistik paneli

![Kategori Yönetimi](assets/form1.png)

![Ürün Yönetimi](assets/frmUrun.png)

![İstatistikler](assets/frmIstatistik.png)

-----------------------------------------------------------------

## Genel Amaç

Uygulama, basit bir ürün ve kategori yönetim arayüzü sağlar. Kategori CRUD (Listele, Ekle, Sil, Güncelle), ürün yönetimi ve istatistiksel özet ekranları içerir. Veri işlemleri Entity Framework üzerinden doğrudan veritabanı ile gerçekleştirilir.

## Kullanılan Teknolojiler

- Windows Forms (WinForms)
- C# 7.3
- .NET Framework 4.7.2
- Entity Framework (EDMX - Database First)

## Proje Yapısı (Özet)

- `Form1.cs`  
  - Kategori yönetim ekranı. `DbEntityUrunEntities` context kullanılarak `Tbl_Kategori` tablosuna yönelik CRUD operasyonları içerir. Öne çıkan metotlar: `BtnListele_Click`, `BtnEkle_Click`, `BtnSil_Click`, `BtnGuncelle_Click`, `dataGridView1_CellClick`.

- `FrmUrun.cs`  
  - Ürün ekleme, listeleme, silme ve güncelleme ekranı. Alanlar: `ID`, `Ürün Adı`, `Marka`, `Stok`, `Fiyat`, `Durum`, `Kategori` (ComboBox ile seçim).

- `FrmIstatistik.cs`  
  - Veritabanı özetlerinin gösterildiği istatistik paneli. Örnek göstergeler: toplam kategori sayısı, toplam ürün sayısı, toplam stok, en yüksek/en düşük fiyatlı ürün, kasa toplamı vb.

- `FrmAnaForm.cs`  
  - Uygulama ana menüsü / gezinme

- `FrmGiris.cs`  
  - Giriş (varsa) veya kimlik doğrulama ekranı

- `Model1.edmx`  
  - Entity Framework EDMX modeli (Database First). Veritabanı tabloları ve ilişkileri burada temsil edilir.

- `Program.cs`  
  - Uygulamanın başlangıç noktası

## Veritabanı Modeli (Kısa Özet)

- Database First yaklaşımıyla EDMX dosyası kullanılır; EDmx üzerinden context (`DbEntityUrunEntities`) ve entity sınıfları üretilir.
- Örnek tablolar:
  - `Tbl_Kategori` (ID, Ad)
  - `Tbl_Urun` (ID, UrunAd, Marka, Stok, Fiyat, Durum, KategoriID)
  - Ek tablolar: müşteri/satış/istatistik tabloları proje içinde bulunabilir.

## CRUD İşleyişi (Kategori Örneği - `Form1.cs`)

- Listeleme: `db.Tbl_Kategori.ToList()` sonuçları `dataGridView1.DataSource` olarak atanır.
- Ekleme: Yeni `Tbl_Kategori` nesnesi oluşturulur, `Ad` set edilir, `db.Tbl_Kategori.Add(t)` ve `db.SaveChanges()` ile veritabanına kaydedilir.
- Silme: `int id = Convert.ToInt16(txtID.Text)` ile ID alınır; `db.Tbl_Kategori.Find(id)` ile kayıt bulunur; `Remove` ve `SaveChanges()` ile silinir.
- Güncelleme: `Find(id)` ile kayıt çekilip ilgili alanlar güncellenir, `SaveChanges()` ile değişiklik kaydedilir.
- DataGridView seçiminden TextBox'a değer atama: `dataGridView1_CellClick` içinde seçili satırın hücre değerleri TextBox'lara aktarılır.

## Kullanıcı Arayüzü (UI)

- Formlar klasik WinForms kontrolleriyle (TextBox, Button, ComboBox, DataGridView) tasarlanmıştır.
- İstatistik ekranında renkli kartlar ve büyük fontlarla özet değerler gösterilir.
- İşlem sonrası kullanıcı bilgilendirmeleri `MessageBox.Show(...)` ile yapılır.

## Bağlantı ve Çalıştırma

1. Visual Studio ile çözümü (`.sln`) açın.
2. `Model1.edmx` veya `App.config` içindeki connection string değerlerini, kendi veritabanı bağlantınıza göre doğrulayın.
3. Veritabanı mevcutsa uygulamayı çalıştırın; yoksa veritabanını oluşturun veya mevcut bir veritabanına bağlayın.
4. Uygulama başlatıldığında ana formdan kategori/ürün/istatistik ekranlarına erişin.

## Veri Akışı ve İş Mantığı

- UI üzerinden girilen veriler Entity Framework context aracılığıyla veritabanına aktarılır.
- Listeleme işlemleri `ToList()` ile veritabanından çekilen koleksiyonların DataGridView'e atanmasıyla yapılır.
- Güncelleme ve silme işlemlerinde önce ilgili kayıt `Find` ile getirilir, değişiklik sonrası `SaveChanges()` çağrılır.
- İstatistikler LINQ sorguları (ör. `Count()`, `Sum()`, `Max()`, `Min()`) ile hesaplanır.

## Önemli Notlar

- EDMX (Database First) model kullanıldığından veritabanı şemasında yapılan değişiklikler sonrasında EDMX'in güncellenmesi gerekir.
- DataGridView hücre indekslerine göre değer alımı yapıldığından sütun sırası değişirse kodun güncellenmesi gerekebilir.
- `DbEntityUrunEntities` context üzerinden yapılan işlemler doğrudan veritabanına etki eder; işlem öncesi gerekli doğrulamalar UI tarafında yapılmalıdır (ör. boş alan kontrolleri).

-----------------------------------------------------------------

Bu README sadece proje dosyası ve uygulama çalışma mantığı ile ilgili teknik bilgileri içerir. Görselleri proje içine ekleyip yukarıdaki yolları kullanarak README'de gösterim sağlayabilirsiniz.