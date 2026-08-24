# IrongridC2System

## install for produser
```bash
dotnet add package Confluent.Kafka
dotnet add package Microsoft.Extensions.Configuration.Json
dotnet add package Microsoft.Extensions.DependencyInjection
```

## install for consumer
```bash
dotnet add package Confluent.Kafka
dotnet add package Microsoft.Extensions.Configuration.Json
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package Microsoft.EntityFrameworkCore.Design--version 8.0.0
dotnet add package Pomelo.EntityFrameworkCore.MySql--version 8.0.0
```

## install for api
```bash
dotnet add package Microsoft.Extensions.Configuration.Json
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package Microsoft.EntityFrameworkCore.Design--version 8.0.0
dotnet add package Pomelo.EntityFrameworkCore.MySql--version 8.0.0
dotnet add package StackExchange.Redis
```

## for sql file (seed)
```bash
docker exec -i db mysql -u root - proot <seed_database.sql
```

## for docker 
```bash
docker compose up -d
```

## to run the projct
```bash
docker compose up -d
docker exec -i db mysql -u root - proot <seed_database.sql
cd IronGridC2Produser
dotnet add package Confluent.Kafka
dotnet add package Microsoft.Extensions.Configuration.Json
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet run
cd ..
cd IronGridC2Consumer
dotnet add package Confluent.Kafka
dotnet add package Microsoft.Extensions.Configuration.Json
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package Microsoft.EntityFrameworkCore.Design--version 8.0.0
dotnet add package Pomelo.EntityFrameworkCore.MySql--version 8.0.0
dotnet run
cd ..
cd IronGridC2Api
dotnet add package Microsoft.Extensions.Configuration.Json
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package Microsoft.EntityFrameworkCore.Design--version 8.0.0
dotnet add package Pomelo.EntityFrameworkCore.MySql--version 8.0.0
dotnet add package StackExchange.Redis
dotnet watch run
```
