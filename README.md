## Shared Market Fullstack

One fullstack project for a shared marketplace, created by ReactJS and DotNet Web API.

`docker run --name postgres -e POSTGRES_PASSWORD=ABCabc123! -p 5431:5432 -d postgres`

`dotnet tool install --global dotnet-ef --version 8.0.11`

`dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.11`

`dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 8.0.11`

`dotnet ef migrations add init`

`dotnet ef database update`