# Introduction 
Данная библиотека создана для работы с [Megafon ATS](https://vats.megafon.ru/) и его [REST API](https://api.megapbx.ru/#/docs/crmapi/v1/general)

# Getting Started
Для добавления фабрики клиентов запросы нужно добавить в ```Program.cs``` :
```
services.AddMegafonAtsClientFactory();
```
Для работы с клиентами нужно передать в класс объект типа IMegafonAtsClientFactory и вызвать метод Create передав в него опции АТС(название и токен)
```
var options = new MegafonAtsOptions 
{
    Name = "name",
    Key = "key"
}
var client = factory.Create<ClientType>(options);
```

Так же, клиенты можно создать с помощью следующих методов расширения: 
```
CreateCallClient(options);
CreateGroupsClient(options);
CreateHistoryClient(options);
CreateSubscriptionClient(options);
CreateUserClient(options);
```

 Для создания сервиса обрабатывающего запросы от АТС нужно добавить в ```Program.cs``` :
```
services.AddMegafonWebHooks<MyTestService>();    
 ```


