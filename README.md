
# Phonecontact-Microservices

Mikroservisler aracılığıyla telefon rehberi uygulaması.

## Contact Microservice
* Veriler MongoDb veritabanına Contact ve Communication collectionları ile kaydediliyor
* Collectionlar ayrı controllerlar ile crud işlemlerini gerçekleştiriyor.
* Restful ile Report Microservice'den rapor talebi düzenleniyor

## Report Microservice
* Rapor verileri MongoDb veritabanında kaydediliyor.
* Gelen rapor istekleri RabbitMQ ile kuyruğa alınarak BackgroundService'de event olarak gönderilip ilgili servisi tetikliyor.
## Kullanılan Teknolojiler

* .Net Core
* RabbitMQ
* MongoDB
  
