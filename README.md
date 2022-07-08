# Introduction 
Данная библиотека создана для работы с [Megafon ATS](https://vats.megafon.ru/) и его [REST API](https://newinterface.megapbx.ru/SkinFiles/firma.megapbx.ru/Megafon3/crm_rest_api.pdf)

# Getting Started
Для создания клиента отправляющего запросы нужно добавить в ```Program.cs``` :
```
services.AddMegafonAtsClient(options =>
    {
        options.AtsName = megafonOptions["AtsName"];
        options.Token = megafonOptions["Token"];
    });
    
 ```
 В ```appsettings.json``` нужно добывить имя АТС и специальный токен установленый в личном кабинете АТС
 
 ```
   "MegafonAtsOptions": {
    "AtsName": "your ats name",
    "Token": "your ats token"
  }
 ```
 
 Для создания сервиса обрабатывающего запросы от АТС нужно добавить в ```Program.cs``` :
```
services.AddMegafonWebHooks<MyTestService>();
    
 ```

# Build and Test
Для запуска тестов нужно добавить свою кофигурацию АТС и изменить аргументы футкций на валидные для вашей АТС 

