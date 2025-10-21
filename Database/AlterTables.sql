-- ============================================
-- TABLO DÜZELTME SCRIPT'İ (ALTER TABLE)
-- ============================================
-- Amaç: Eksik kolonları eklemek
-- Tarih: 21 Ekim 2024
-- ============================================

-- ============================================
-- 1. USERS TABLOSU DÜZELTMELERİ
-- ============================================

-- FullName yerine FirstName ve LastName kullanacağız
-- Bu daha profesyonel ve esnek bir yaklaşım

-- FirstName kolonunu ekle
ALTER TABLE Users
ADD FirstName nvarchar(100) NULL;
-- ☝️ ALTER TABLE = Tabloyu değiştir
-- ADD = Yeni kolon ekle
-- nvarchar(100) = Metin tipi, max 100 karakter
-- NULL = Bu kolon boş olabilir (zorunlu değil)

-- LastName kolonunu ekle
ALTER TABLE Users
ADD LastName nvarchar(100) NULL;

-- Phone kolonunu ekle
ALTER TABLE Users
ADD Phone nvarchar(20) NULL;
-- ☝️ nvarchar(20) = Telefon için yeterli (0555 123 45 67)

-- IsAdmin kolonunu ekle
ALTER TABLE Users
ADD IsAdmin bit NOT NULL DEFAULT 0;
-- ☝️ bit = Boolean (0 veya 1)
-- NOT NULL = Boş olamaz (her kullanıcının bir değeri olmalı)
-- DEFAULT 0 = Varsayılan değer 0 (admin değil)

PRINT 'Users tablosu güncellendi! ✅'
-- ☝️ PRINT = Konsola mesaj yaz

GO
-- ☝️ GO = Bu bölüm bitti, sonrakine geç

-- ============================================
-- 2. PRODUCTS TABLOSU DÜZELTMELERİ
-- ============================================

-- ProductDescription → Description olarak da kullanabiliriz ama ProductDescription daha açık
-- Yeni kolonlar: Stock ve IsActive

-- Stock (Stok adedi)
ALTER TABLE Products
ADD Stock int NOT NULL DEFAULT 0;
-- ☝️ int = Tam sayı
-- DEFAULT 0 = Varsayılan stok 0

-- IsActive (Satışta mı?)
ALTER TABLE Products
ADD IsActive bit NOT NULL DEFAULT 1;
-- ☝️ DEFAULT 1 = Varsayılan olarak aktif

-- Description kolonu ekleyelim (daha okunabilir)
ALTER TABLE Products
ADD Description nvarchar(500) NULL;
-- ☝️ nvarchar(500) = Uzun açıklama için

PRINT 'Products tablosu güncellendi! ✅'

GO

-- ============================================
-- 3. SLIDERS TABLOSU DÜZELTMELERİ
-- ============================================

-- Header1 → Title (daha anlaşılır)
-- Header2 → Description
-- ImageUrl → ImagePath (daha standart)
-- Bunları kullanmaya devam edelim ama ekstra kolonlar ekleyelim

-- Title kolonu (Header1 yerine kullanabiliriz)
ALTER TABLE Sliders
ADD Title nvarchar(200) NULL;

-- Description kolonu (Header2 yerine)
ALTER TABLE Sliders
ADD Description nvarchar(500) NULL;

-- IsActive kolonu
ALTER TABLE Sliders
ADD IsActive bit NOT NULL DEFAULT 1;
-- ☝️ Varsayılan olarak aktif

-- Link kolonu (ProductLink var ama Link daha genel)
ALTER TABLE Sliders
ADD Link nvarchar(500) NULL;

PRINT 'Sliders tablosu güncellendi! ✅'

GO

-- ============================================
-- 4. KATEGORİLER İÇİN EK BİLGİ
-- ============================================

-- Categories zaten iyi durumda!
-- Name, SeoLink, IconName, Sort, ParentId var
-- Eksik bir şey yok ✅

PRINT 'Categories tablosu kontrol edildi - Sorun yok! ✅'

GO

-- ============================================
-- TAMAMLANDI! ✅
-- ============================================

PRINT ''
PRINT '================================================'
PRINT 'TÜM TABLOLAR GÜNCELLENDİ!'
PRINT '================================================'
PRINT 'Users: +4 kolon (FirstName, LastName, Phone, IsAdmin)'
PRINT 'Products: +3 kolon (Stock, IsActive, Description)'
PRINT 'Sliders: +4 kolon (Title, Description, IsActive, Link)'
PRINT '================================================'

-- ============================================
-- 📚 ÖĞRENME NOTU: ALTER TABLE
-- ============================================
-- ALTER TABLE üç şekilde kullanılır:
-- 1. ADD    → Yeni kolon ekle
-- 2. DROP   → Kolon sil
-- 3. ALTER  → Kolon tipini değiştir

-- Örnek:
-- ALTER TABLE Products DROP COLUMN OldColumn;  (Sil)
-- ALTER TABLE Products ALTER COLUMN Price decimal(18,2);  (Değiştir)
-- ============================================

