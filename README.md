# Bulutfon .Net SDK
 
Bulutfon .Net SDK, ASP.NET MVC (4 ve üzeri) ve desktop (WinForms) projelerinin, Bulutfon OAuth ile kimlik doğrulaması (authentication) ve yetki alınması (authorization) işlemleri için gerekli fonksiyonları sağlar.

# Kurulum


# ASP.NET MVC

1- Geliştirme ve testler için öncelikle projeye https desteği eklenmelidir. Bunun için:
  * Proje seçilip ```Properties``` penceresine geçin
  * ```SSL Enabled```ı ```True``` olarak belirleyin
  * ```SSL URL```de https://localhost:44304/ gibi bir adres oluşacaktır, bu adresi kopyalayın
  * Projeye sağ tıklayıp menüden ```Properties```i seçin
  * Açılan pencerede soldan ```Web``` sayfasını seçin
  * ```Project URL``` kısmına kopyalamış olduğunuz adresi yapıştırın

2- *OAuth client*'lara *BulutfonWebClient* eklenmelidir. Bunun için:
  * *App_Start\AuthConfig.cs* dosyasını açın
  * ```RegisterAuth()``` metoduna aşağıdaki kodu ekleyin:
``` csharp
    OAuthWebSecurity.RegisterClient(new BulutfonWebClient(
        clientId:"CLIENT_ID_NIZ", // Bulutfon servisindeki uygulamanın Client ID'si
        clientSecret:"CLIENT_SECRET"), // Bulutfon servisindeki uygulamanın Client Secret'ı
        "Bulutfon", null);
```

