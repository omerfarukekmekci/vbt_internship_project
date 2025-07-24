# Teknik Kaynak Listesi ve Teslim Kılavuzu

Bu kaynak listesi, projeleri geliştirecek stajyerlere yol göstermek amacıyla hazırlanmıştır. Her teknoloji alanı için GitHub repoları, video eğitimler, resmi dokümantasyonlar ve yapay zekadan (AI) yardım istemek için kullanılabilecek örnek "prompt"lar içerir.

---

## Teknik Kaynaklar

### **Backend**

#### Node.js & Express.js
* **GitHub Repoları:**
    * [Express.js Başlangıç Şablonu (TypeScript ile)](https://github.com/santiq/bulletproof-nodejs)
    * [Prisma Örnek Projeler](https://github.com/prisma/prisma-examples)
* **Video Kaynakları:**
    * [Express JS Crash Course (Traversy Media)](https://www.youtube.com/watch?v=gnsO8-xJ8rs) (İngilizce)
    * [Node.js Full Course (freeCodeCamp)](https://www.youtube.com/watch?v=Oe421JkE9fk) (İngilizce)
* **Resmi Dokümantasyonlar:**
    * [Express.js Resmi Sitesi](https://expressjs.com/)
    * [Prisma ORM Dokümantasyonu](https://www.prisma.io/docs/)

#### .NET
* **GitHub Repoları:**
    * [.NET Minimal API Örnekleri](https://github.com/dotnet/aspnetcore/tree/main/src/Http/Http.Results/samples)
    * [Clean Architecture .NET Şablonu](https://github.com/jasontaylordev/CleanArchitecture)
    * [.NET Core Web API JWT Authentication Örneği](https://github.com/cornflourblue/dotnet-5-jwt-api)
* **Video Kaynakları:**
    * [C# 101 (Engin Demiroğ)](https://www.youtube.com/watch?v=vyc_i-5kY4A) (Türkçe)
    * [.NET Core REST API From Scratch (freeCodeCamp)](https://www.youtube.com/watch?v=SXy3fS_6x6U) (İngilizce)
    * [JWT Authentication in ASP.NET Core (Nick Chapsas)](https://www.youtube.com/watch?v=mgeuh8k3I4g) (İngilizce)
* **Resmi Dokümantasyonlar:**
    * [ASP.NET Core Dokümantasyonu](https://docs.microsoft.com/en-us/aspnet/core/)
    * [Entity Framework Core Dokümantasyonu](https://docs.microsoft.com/en-us/ef/core/)

### **Frontend**

#### React
* **GitHub Repoları:**
    * [Vite + React + Tailwind CSS Başlangıç Şablonu](https://github.com/theodorusclarence/vite-react-tailwind-starter)
    * [React - JWT Authentication & Authorization Example](https://github.com/bezkoder/react-jwt-auth)
* **Video Kaynakları:**
    * [React Crash Course (Traversy Media)](https://www.youtube.com/watch?v=sBws8MSXN7A) (İngilizce)
    * [React Dersleri (Yazılım Bilimi)](https://www.youtube.com/watch?v=4_i-0nC6aKk&list=PLurn6mxdB1JEXlbbvPefG3I5K2p_T0v4j) (Türkçe)
    * [React Authentication with Context API and Hooks (Web Dev Simplified)](https://www.youtube.com/watch?v=pK_d_A-0-28) (İngilizce)
* **Resmi Dokümantasyonlar:**
    * [React Resmi Dokümantasyonu (Yeni)](https://react.dev/)
    * [Tailwind CSS Dokümantasyonu](https://tailwindcss.com/docs/installation)
    * [React Router - Auth Örneği](https://reactrouter.com/en/main/examples/auth)

#### Angular
* **GitHub Repoları:**
    * [Angular Material Örnekleri](https://github.com/angular/components)
* **Video Kaynakları:**
    * [Angular for Beginners (freeCodeCamp)](https://www.youtube.com/watch?v=3qBXWUpoPHo) (İngilizce)
* **Resmi Dokümantasyonlar:**
    * [Angular Resmi Dokümantasyonu](https://angular.io/docs)

### **Mobil (Flutter)**

* **GitHub Repoları:**
    * [Flutter Örnek Projeler](https://github.com/flutter/samples)
    * [Flutter - Firebase Auth Örnekleri](https://github.com/firebase/flutterfire/tree/master/packages/firebase_auth/firebase_auth/example)
* **Video Kaynakları:**
    * [Flutter Course for Beginners (freeCodeCamp)](https://www.youtube.com/watch?v=pTJJsmejUOQ) (İngilizce)
    * [Flutter Authentication with Provider (Code With Andrea)](https://www.youtube.com/watch?v=pAnE_sR7z1s) (İngilizce)
* **Resmi Dokümantasyonlar:**
    * [Flutter Resmi Dokümantasyonu](https://docs.flutter.dev/)
    * [Firebase Authentication for Flutter](https://firebase.flutter.dev/docs/auth/overview/)
    * [Flutter - Storing key-value data on disk](https://docs.flutter.dev/cookbook/persistence/key-value)

### **Yapay Zeka (Python & FastAPI)**

* **GitHub Repoları:**
    * [Full Stack FastAPI Başlangıç Şablonu](https://github.com/tiangolo/full-stack-fastapi-postgresql)
* **Video Kaynakları:**
    * [FastAPI - Full Course (freeCodeCamp)](https://www.youtube.com/watch?v=-ykeT6kk4I) (İngilizce)
* **Resmi Dokümantasyonlar:**
    * [FastAPI Dokümantasyonu](https://fastapi.tiangolo.com/)
    * [Google Gemini API Dokümantasyonu](https://ai.google.dev/docs)
    * [OpenAI API Dokümantasyonu](https://platform.openai.com/docs)

---

## Proje Teslim Kılavuzu

### **1. Etkili Bir README.md Dosyası Hazırlama**

`README.md` dosyası, projenizin vitrinidir. Projenizi ziyaret eden birinin, projenin ne hakkında olduğunu, nasıl çalıştığını ve neler yaptığınızı hızlıca anlamasını sağlar. Projenizin ana dizininde `README.md` adında bir dosya oluşturun ve aşağıdaki şablonu doldurun:

```markdown
# Proje Adı: [Buraya Proje Adını Yazın]

[Buraya projenin kısa bir tanımını ekleyin. Bu proje ne yapar? Hangi problemi çözer?]

## Kullanılan Teknolojiler
* [Teknoloji 1]
* [Teknoloji 2]
* [Teknoloji 3]

## Kurulum ve Çalıştırma
Projeyi kendi bilgisayarınızda çalıştırmak için aşağıdaki adımları izleyin:

1.  Bu repoyu klonlayın: `git clone [REPO_URL]`
2.  Proje dizinine gidin: `cd [PROJE_KLASORU]`
3.  Gerekli paketleri yükleyin: `npm install` (veya `dotnet restore`, `pip install -r requirements.txt`)
4.  Projeyi başlatın: `npm run dev` (veya `dotnet run`)

## Ekran Görüntüleri

[Projenizin ana sayfasından, detay sayfasından veya önemli bulduğunuz diğer ekranlardan görüntüler ekleyin.]

![Ana Sayfa Ekran Görüntüsü](link_veya_dosya_yolu)
![Detay Sayfası Ekran Görüntüsü](link_veya_dosya_yolu)