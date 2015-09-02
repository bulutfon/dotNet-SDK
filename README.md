# Bulutfon .Net SDK
 
Bulutfon .Net SDK, ASP.NET MVC (4 ve üzeri) ve desktop (WinForms) projelerinin, Bulutfon OAuth ile kimlik doğrulaması (authentication) ve yetki alınması (authorization) işlemleri için gerekli fonksiyonları sağlar.

# Kurulum


# ASP.NET MVC

1- Bulutfon'u nuget ile projenize dahil edin ```Install-Package Bulutfon.MVC4```

2- Geliştirme ve testler için öncelikle projeye https desteği eklenmelidir. Bunun için:
  * Projeyi seçip ```Properties``` penceresine geçin
  * ```SSL Enabled```ı ```True``` olarak belirleyin
  * ```SSL URL```de https://localhost:44304/ gibi bir adres oluşacaktır, bu adresi kopyalayın
  * Projeye sağ tıklayıp menüden ```Properties```i seçin
  * Açılan pencerede soldan ```Web``` sayfasını seçin
  * ```Project URL``` kısmına kopyalamış olduğunuz adresi yapıştırın

3- *OAuth client*'lara *BulutfonWebClient* eklenmelidir. Bunun için:
  * *App_Start\AuthConfig.cs* dosyasını açın
  * ```RegisterAuth()``` metoduna aşağıdaki kodu ekleyin:
``` csharp
	TokenRefreshCallback refreshCallback = new TokenRefreshCallback(tokenRefreshed);
	BulutfonWebClient client = new BulutfonWebClient(
                clientId: "CLIENT_ID_NIZ", // Bulutfon servisindeki uygulamanın Client ID'si
                clientSecret: "CLIENT_ID_NIZ", // Bulutfon servisindeki uygulamanın Client Secret'ı 
				refreshCallback: refreshCallback // Expire olan token refreshlendiğinde tetiklenecek method. Kullanmayacaksanız null değer gönderebilirsiniz. 
				);
    OAuthWebSecurity.RegisterClient(client, "Bulutfon", null);

```

``` csharp
	// Token yenilendiğinde tetiklenecek refreshCallback methodu
	public static void tokenRefreshed(object sender, string access_token, string refreh_token) {
		// Do something
	}

```

4- Bulutfon API'sine erişim
  * Örneğin mesaj (SMS) listesini çekmek için:
    * ```HomeController``` içinde aşağıdaki metodu oluşturun:
``` csharp
        [Authorize]
        public ActionResult Messages() {
            var messages = BulutfonApi.GetMessages((Token)Session[Token.Key]);
            return View(messages);
        }
```
  * ```View```'u oluşturmak için ise:
    * Metot ismi üzerine sağ tıklayıp menüden ```Add View...```'u seçin
    * ```Create a strongly-typed view```u işaretleyin
    * ```Model class``` olarak ```Message (Bulutfon.Sdk.Models)``` seçin
    * ```Scaffold template``` olarak ```List```'i seçin
    * ```Add```'e tıklayın
5- Bulutfon sitesindeki uygulama ayarlarından redirect uri kısmını güncelleyin (https://localhost:44304/Account/ExternalLoginCallback gibi bir adres olması gerektir)

# DESKTOP (WINDOWS FORMS)

1- Bulutfon'u nuget ile projenize dahil edin ```Install-Package Bulutfon.OAuth.Win```

1- Login olmak için ilgili düğme ya da menünün koduna aşağıdaki satırları ekleyin:
``` csharp
            var loggedIn = LoginForm.Login(
                "CLIENT_ID_NIZ", // Bulutfon servisindeki uygulamanın Client ID'si
                "CLIENT_SECRET"), // Bulutfon servisindeki uygulamanın Client Secret'ı
                this); // ya da null
```
2- Verilere erişmek için gene BulutfonApi metotlarından yararlanabilirsiniz. Örneğin:
``` csharp
            if (loggedIn) {
                //button1.Enabled = false;
                dataGridView1.DataSource = BulutfonApi.GetDids(Authentication.Token);
            }
```
3- Bulutfon sitesindeki uygulama ayarlarından redirect uri kısmını güncelleyin (urn:ietf:wg:oauth:2.0:oob)

4- Token expire olduğunda otomatik yenilenecektir. Bu eventı yakalayıp yeni tokenlara erişmek istiyorsanız. Token alındıktan sonra aşağıdaki kodu ekleyebilirsiniz.
``` csharp
		{	
            if (loggedIn) {
                //button1.Enabled = false;
				Authentication.Token.RefreshCallback += TokensRefreshed;
                dataGridView1.DataSource = BulutfonApi.GetDids(Authentication.Token);
            }

		}
		void TokensRefreshed(object sender, string access_token, string refreh_token)
        {
            
        }
```

# BULUTFON API

```BulutfonApi``` statik sınıfı dahilinde, hem web (MVC), hem de WinForms ve diğer .NET projeleri içinde kullanılacak tüm API fonksiyonları mevcuttur.

Örneğin mesajlar (SMS) için,
* Mesaj listesi: ```GetMessages(/*token*/);```
* Mesaj detayları: ```GetMessage(id, /*token*/);```
* Mesaj gönderimi: ```SendSms(/*...*/);```
* vb.

# LİSANS

Telif Hakkı (c) 2015, Bulutfon Telekomünikasyon A.Ş.

Bu yazılım MIT lisansı ile dağıtılmaktadır.
http://opensource.org/licenses/MIT









