# 📱 Mobile Login & Register Module (Flutter)

This module contains the mobile login and register functionality developed with **Flutter**.

---

## 🚀 Getting Started

### 1. Prerequisites

Make sure you have the following installed:

- Flutter SDK (version >= 3.32.0)
- Android Studio / VS Code
- Android Emulator or real device

To check your environment:
```bash
flutter doctor

2. Installation
Clone the repository and navigate to the mobile directory:

git clone https://github.com/omerfarukekmekci/vbt_internship_project.git
cd vbt_internship_project/mobile
flutter pub get

----------------

3. Running the App
To run on emulator or connected device:
flutter run

To run on a specific device:
flutter devices     
flutter run -d emulator-5554   # or use your device ID

-----------------

4. Features
Responsive Login & Register UI

Navigation between screens

Email/password input with controllers

Future-ready for backend integration (API ready)

----------------

📁 Directory Structure

mobile/
├── lib/
│   ├── main.dart
│   └── screens/
│       ├── login_screen.dart
│       └── register_screen.dart
├── pubspec.yaml
├── README.md

--------------
Backend için SQL Server Kurulumu ve LocalDB Kullanımı
1. SQL Server LocalDB Nedir?
LocalDB, SQL Server’ın hafif, kolay kurulan bir versiyonudur.

Geliştirme ve test amaçlı kullanılır, tam SQL Server kurulumu gerektirmez.

Projede LocalDB kullanılmışsa, kurulumu oldukça basittir.

2. LocalDB Kurulumu
Adım 1: SQL Server Express LocalDB’yi indir
Microsoft’un resmi sitesinden SQL Server Express 2019 veya 2022 indir.

Kurulum sırasında sadece “LocalDB” özelliğini seç.

Adım 2: LocalDB’yi kontrol et
Komut satırını aç (CMD veya PowerShell)

sqllocaldb info yaz ve Enter’a bas.

Eğer LocalDB kuruluysa, mevcut instance’lar listelenir (örn. MSSQLLocalDB)

Adım 3: LocalDB instance başlat (gerekirse)
sqllocaldb start MSSQLLocalDB komutu ile LocalDB’yi başlatabilirsin.

Adım 4: Bağlantı string’i
Projede bağlantı stringi şöyle olur:
Server=(localdb)\MSSQLLocalDB;Database=InternPortalDb;Trusted_Connection=True;
Bu string, projenin appsettings.json veya appsettings.Development.json içinde tanımlı olmalı.


3. Tam SQL Server Kurulumu (Opsiyonel)
Adım 1: SQL Server Express veya Developer Edition’ı indir
SQL Server Express veya Developer Edition’ı indirip kur.

Adım 2: SQL Server Management Studio (SSMS) indir
Veritabanlarını yönetmek için SSMS yükle.

Adım 3: Veritabanı oluştur
SSMS ile SQL Server’a bağlan, yeni bir veritabanı oluştur (örneğin InternPortalDb).

Adım 4: Connection string ayarla
Projede connection string olarak şunu kullanabilirsin:
Server=localhost;Database=InternPortalDb;Trusted_Connection=True;



4. Ek Notlar
Backend projesini açtıktan sonra, Entity Framework migration’larını çalıştırarak veritabanını otomatik oluşturabilir.
Örnek komutlar:
dotnet ef database update
Eğer migration yoksa, öncelikle migration oluştur:
dotnet ef migrations add InitialCreate
dotnet ef database update
Bunları yapabilmek için .NET SDK ve EF Core CLI yüklü olmalı.



--------------,

📸 Screenshots

[Login Screen](assets/login.png)

[Register Screen](assets/register.png)

[Home Screen](assets/home.png)

-------------


