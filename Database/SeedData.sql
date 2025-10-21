-- ============================================
-- ETIC E-TİCARET - İLK VERİLER (SEED DATA)
-- ============================================
-- Bu dosya veritabanına ilk verileri ekler
-- ============================================

-- ============================================
-- 1. KATEGORİLER
-- ============================================

-- INSERT INTO = Tabloya veri ekle
-- Categories = Kategori tablosu
-- (Name, SeoLink) = Hangi kolonlara veri ekleyeceğiz
-- VALUES = Eklenecek veriler

INSERT INTO Categories (Name, SeoLink, Sort, CreatedDate, IsDeleted) VALUES
('Elektronik', 'elektronik', 1, GETDATE(), 0),
-- ☝️ Name: Kategori adı (sitede görünür)
-- SeoLink: URL'de kullanılır (örn: etic.com/kategori/elektronik)
-- Sort: Sıralama numarası (menüde hangi sırada görünsün)
-- CreatedDate: Oluşturulma tarihi (BaseEntity'den)
-- IsDeleted: 0 = Silinmemiş (BaseEntity'den)

('Bilgisayar', 'bilgisayar', 2, GETDATE(), 0),
('Telefon', 'telefon', 3, GETDATE(), 0),
('Televizyon', 'televizyon', 4, GETDATE(), 0),
('Aksesuar', 'aksesuar', 5, GETDATE(), 0);

-- Sonuç: 5 kategori eklendi ✅

-- ============================================
-- 2. KULLANICILAR
-- ============================================

-- Admin kullanıcı ekleyelim (admin paneline girmek için)

INSERT INTO Users (FirstName, LastName, FullName, Email, PasswordHash, Phone, IsAdmin, Status, RegisterDate, CreatedDate, IsDeleted) VALUES
('Admin', 'User', 'Admin User', 'admin@etic.com', 
'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', -- SHA256 hash of "123456"
'5551234567', 1, 1, GETDATE(), GETDATE(), 0);
-- ☝️ FirstName = İsim
-- LastName = Soyisim
-- FullName = Tam isim
-- PasswordHash = Şifrenin hash'lenmiş hali (güvenlik için!)
-- Phone = Telefon
-- IsAdmin = 1 demek bu kullanıcı admin
-- Status = 1 = Aktif kullanıcı
-- RegisterDate = Kayıt tarihi
-- CreatedDate = Oluşturulma tarihi (BaseEntity'den)
-- IsDeleted = 0 = Silinmemiş (BaseEntity'den)
-- GETDATE() = Şu anki tarih/saat

-- Sonuç: 1 admin kullanıcı eklendi ✅
-- Giriş: admin@etic.com / 123456 (şifre otomatik hash'lenecek)

-- ============================================
-- 3. SLIDERLAR (Anasayfa Resimleri)
-- ============================================

-- Slider = Anasayfada kayan resimler

INSERT INTO Sliders (Title, Description, ImageUrl, Link, IsActive, Sort, Name, CreatedDate, IsDeleted) VALUES
('Yeni Sezon Ürünleri', 'En yeni elektronik ürünler burada!', '/images/slider1.jpg', '/kategori/elektronik', 1, 1, 'Slider 1', GETDATE(), 0),
-- ☝️ Title: Slider başlığı
-- Description: Açıklama
-- ImageUrl: Resim yolu
-- Link: Tıklanınca nereye gidecek
-- IsActive: 1 = aktif
-- Sort: Sıralama
-- Name: Slider adı
-- CreatedDate: Oluşturulma tarihi (BaseEntity'den)
-- IsDeleted: 0 = Silinmemiş (BaseEntity'den)

('Bilgisayarlarda İndirim', '%50 ye varan indirimler!', '/images/slider2.jpg', '/kategori/bilgisayar', 1, 2, 'Slider 2', GETDATE(), 0),
('Telefonlarda Kampanya', 'Tüm telefonlar uygun fiyatlarla!', '/images/slider3.jpg', '/kategori/telefon', 1, 3, 'Slider 3', GETDATE(), 0);

-- Sonuç: 3 slider eklendi ✅

-- ============================================
-- 4. ÜRÜNLER
-- ============================================

-- Şimdi ürün ekleyelim

INSERT INTO Products (Name, Description, ProductDescription, Price, Stock, IsActive, SeoLink, CreatedDate, IsDeleted) VALUES
-- Bilgisayar Ürünleri
('Dell Laptop XPS 15', 'Yüksek performanslı laptop', 'Yüksek performanslı laptop, 16GB RAM, 512GB SSD', 25000.00, 10, 1, 'dell-laptop-xps-15', GETDATE(), 0),
-- ☝️ Name: Ürün adı
-- Description: Kısa açıklama
-- ProductDescription: Detaylı açıklama
-- Price: Fiyat
-- Stock: Stok adedi
-- IsActive: 1 = satışta
-- SeoLink: URL'de kullanılır
-- CreatedDate: Oluşturulma tarihi (BaseEntity'den)
-- IsDeleted: 0 = Silinmemiş (BaseEntity'den)

('Logitech Mouse MX Master', 'Kablosuz mouse', 'Kablosuz mouse, ergonomik tasarım', 800.00, 50, 1, 'logitech-mouse-mx-master', GETDATE(), 0),
('Logitech Klavye K380', 'Bluetooth klavye', 'Bluetooth klavye, çoklu cihaz desteği', 600.00, 30, 1, 'logitech-klavye-k380', GETDATE(), 0),

-- Telefon Ürünleri
('iPhone 14 Pro', 'Apple iPhone 14 Pro', 'Apple iPhone 14 Pro, 256GB, Uzay Grisi', 45000.00, 15, 1, 'iphone-14-pro', GETDATE(), 0),
('Samsung Galaxy S23', 'Samsung Galaxy S23', 'Samsung Galaxy S23, 128GB, Phantom Black', 35000.00, 20, 1, 'samsung-galaxy-s23', GETDATE(), 0),

-- Televizyon Ürünleri
('Samsung 55" Smart TV', '4K UHD Smart TV', '4K UHD Smart TV, HDR desteği', 15000.00, 8, 1, 'samsung-55-smart-tv', GETDATE(), 0),
('LG 65" OLED TV', 'OLED TV', 'OLED TV, Dolby Atmos, WebOS', 35000.00, 5, 1, 'lg-65-oled-tv', GETDATE(), 0),

-- Aksesuar Ürünleri
('Anker PowerBank 20000mAh', 'Taşınabilir şarj cihazı', 'Hızlı şarj destekli taşınabilir şarj cihazı, 20000mAh', 500.00, 100, 1, 'anker-powerbank-20000', GETDATE(), 0),
('Apple AirPods Pro 2', 'Kablosuz kulaklık', 'Aktif gürültü önleme, kablosuz şarj, H2 chip', 8000.00, 25, 1, 'apple-airpods-pro-2', GETDATE(), 0),
('Xiaomi Mi Band 7', 'Akıllı bileklik', 'Akıllı bileklik, kalp atış takibi, uyku takibi', 1200.00, 60, 1, 'xiaomi-mi-band-7', GETDATE(), 0);

-- Sonuç: 10 ürün eklendi ✅

-- ============================================
-- 5. ÜRÜN-KATEGORİ İLİŞKİSİ
-- ============================================

-- Her ürünü kategoriye atamamız lazım
-- ProductCategories tablosu = Hangi ürün hangi kategoride?

-- Önce ürünlerin ve kategorilerin ID'lerini bulalım

-- Bilgisayar kategorisindeki ürünler (Dell Laptop, Mouse, Klavye)
INSERT INTO ProductCategories (ProductId, CategoryId)
SELECT p.Id, c.Id 
FROM Products p, Categories c
WHERE p.Name IN ('Dell Laptop XPS 15', 'Logitech Mouse MX Master', 'Logitech Klavye K380')
AND c.Name = 'Bilgisayar';
-- ☝️ Bu komut ne yapıyor?
-- Products tablosundan ürünleri al (Dell, Logitech...)
-- Categories tablosundan Bilgisayar kategorisini al
-- İkisinin ID'lerini ProductCategories tablosuna ekle
-- Yani: Bu ürünler "Bilgisayar" kategorisinde!

-- Telefon kategorisindeki ürünler
INSERT INTO ProductCategories (ProductId, CategoryId)
SELECT p.Id, c.Id 
FROM Products p, Categories c
WHERE p.Name IN ('iPhone 14 Pro', 'Samsung Galaxy S23')
AND c.Name = 'Telefon';

-- Televizyon kategorisindeki ürünler
INSERT INTO ProductCategories (ProductId, CategoryId)
SELECT p.Id, c.Id 
FROM Products p, Categories c
WHERE p.Name IN ('Samsung 55" Smart TV', 'LG 65" OLED TV')
AND c.Name = 'Televizyon';

-- Aksesuar kategorisindeki ürünler
INSERT INTO ProductCategories (ProductId, CategoryId)
SELECT p.Id, c.Id 
FROM Products p, Categories c
WHERE p.Name IN ('Anker PowerBank 20000mAh', 'Apple AirPods Pro 2', 'Xiaomi Mi Band 7')
AND c.Name = 'Aksesuar';

-- Elektronik kategorisi (tüm ürünler aynı zamanda elektronik)
INSERT INTO ProductCategories (ProductId, CategoryId)
SELECT p.Id, c.Id 
FROM Products p, Categories c
WHERE c.Name = 'Elektronik';
-- ☝️ Tüm ürünler Elektronik kategorisinde de görünsün

-- Sonuç: Ürünler kategorilere atandı ✅

-- ============================================
-- TAMAMLANDI! ✅
-- ============================================

-- Eklenenler:
-- ✅ 5 Kategori
-- ✅ 1 Admin Kullanıcı (admin@etic.com / 123456)
-- ✅ 3 Slider
-- ✅ 10 Ürün
-- ✅ Ürün-Kategori ilişkileri

