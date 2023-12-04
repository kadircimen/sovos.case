
## Sovos Case Study 
Bu repo, Sovos Case Study çözümünü içermektedir.


## Kullanılan Teknolojiler

**.Net 7, Postgre Sql, Elasticsearch, PG Admin, Kibana UI, Docker**

  ## Projeler

Proje, `app` ve `core` olmak üzere iki ana dizine ayrılmıştır.

### App

Bu dizin, uygulamanın çalışma zamanıyla ilgili projeleri içerir.

- `Sovos.CaseStudy.Api`: Uygulamanın giriş noktası olan bu proje, dış dünya ile iletişim kurmak için API katmanını içerir. RESTful prensiplerine uygun endpoint'ler sunar.
Hangfire, Swagger, Hangfire gibi önemli bağımlılıklar içerir. 

- `Sovos.CaseStudy.Application`: İş mantığını barındıran bu katman, API ve veritabanı arasında köprü görevi görür.

- `Sovos.CaseStudy.Domain`: Uygulamanın iş kurallarını ve nesne modellerini içerir. Veritabanı ile etkileşimde bulunmak için gereken entity'leri tanımlar.

- `Sovos.CaseStudy.Persistence`: Veritabanı işlemleri için gerekli olan konfigürasyon ve erişim mekanizmalarını içerir. Hangfire ve Entity Framework Core PostgreSQL ile veritabanı işlemlerini yönetir.
### Core

Bu dizin, genel amaçlı, uygulama genelinde kullanılabilen yardımcı kütüphaneleri ve servisleri içerir.

- `Core.Application`: Tüm uygulama boyunca kullanılacak ortak servisler ve iş mantığı için bir temel sağlar.
MediatR ve FluentValidation gibi bağımlılıklar kullanarak iş mantığı işlemlerini yürütür.

- `Core.CrossCuttingConcerns`: Kesişen işleri (logging, caching, validation vs.) yönetmek için kullanılan yapıları içerir.
Serilog ve FluentValidation ile kesişen işleri yönetir. (Loglama Elasticsearch'e kaydedilir)

- `Core.Mailing`: E-posta gönderme işlemlerini yürüten uygulama içi servislerin yer aldığı projedir.
MailKit ve MimeKit ile e-posta gönderme işlevselliğini sağlar.

- `Core.Persistence`: Uygulamanın veritabanı erişim katmanlarında kullanılacak ortak mekanizmaları içerir.
Veritabanı erişim katmanları için genel mekanizmaları içerir.

## Kurulum

VERİTABANLARI KULLANMAK İÇİN BİLGİSAYARINIZDA DOCKER KURULU OLMALIDIR.

Projenin root'unda `docker` isimli klasörün içerisinde veritabanları ve UI'ları içeren `docker-compose` yaml file'ı bulumaktadır. 

Console'dan yaml file'ın bulunduğu path'e gidip
`docker-compose up` komutu ile konteynerların ayağa kaldırılması gerekmektedir.

`docker-compose up` komutu çalıştırıldıktan sonra çalışması beklenilen konteynerlar aşağıdaki gibidir:

- `SovosCasePosgreDb` : Projenin main database'i olarak kullanılmaktadır.
- `SovosCasePgAdmin` : Projenin main database'inin UI'ı.
- `SovosCaseElasticDb`: Logların tutulduğu Elasticsearch veritabanı.
- `SovosCaseKibanaUi`: Logların tutulduğu Elasticsearch veritabanı için IU.



## Postgre Sql Bilgileri
Main Database: SovosCaseDb
User: sovoscaseuser
Password: caseuser123*

## PG Admin Bilgileri
Email: case@sovos.com
Password: caseuser123*

## Elasticsearch Bilgileri
Username: sovoselsusr
Password: s0v0s3l4st!c

#### Tüm konteynerlar için volume'lar ve iletişim kurmaları için network tanımlanmıştır.

### Volumes
sovoscasedb_volume
sovoscasepgadmin_volume
sovoscaseelastic_volume
sovoscasekibana_volume

### Network
sovoscase_network


## DB Migration
Persistence/Migrations klasörü içerisinde son migration ayarları bulunmaktadır. Postgre db konteynerı ayağa kalktıktan sonra proje çalıştırıldığında migration yapacak ve tanımlanan entityler db'ye yansıyacaktır.



## Kullanılan örnek JSON data

```
{
"InvoiceId": "SVS202300000001",
"SenderTitle": "Gönderici Firma",
"ReceiverTitle": "Alıcı Firma",
"Date": "2023-01-05",
"Email": "receiver@test.com",
"InvoiceLine": [
{
"Id": 1,
"Name": "1.Ürün",
"Quantity": 5,
"UnitCode": "Adet",
"UnitPrice": 10
},
{
"Id": 2,
"Name": "2.Ürün",
"Quantity": 2,
"UnitCode": "Litre",
"UnitPrice": 3
},
{
"Id": 3,
"Name": "3.Ürün",
"Quantity": 25,
"UnitCode": "Kilogram",
"UnitPrice": 2
}
]}

```

  
## Genel bilgiler

- Fluent validation ile validation işlemleri.
- Mediator ile CQRS pattern uygulaması.
- Serilog & Elasticsearch ile loglama yapılması.
- Exception middleware aracılığı ile exception yönetimi yapılması.
- Efektif EntityframeworkCore kullanılması.
- AutoMapper ile mapping işlemlerinin yapılması.
- Hangfire ile zamanlanmış görev tanımlaması.
- Veritaban ve UI'larının docker ile konteynerizasyonunun yapılması.
- Docker Compose file ile konteynerların yönetilebilirliği.