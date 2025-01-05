## Shared Market Fullstack

One fullstack project for a shared marketplace, created by ReactJS and DotNet Web API.

`docker pull mcr.microsoft.com/azure-sql-edge`

`docker run -e 'ACCEPT_EULA=1' -e 'MSSQL_SA_PASSWORD=ABCabc666' -p 1433:1433 --name sqlserver -d mcr.microsoft.com/azure-sql-edge`

`dotnet tool install --global dotnet-ef --version 8.0.11`

`dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.11`

`dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 8.0.11`

`dotnet ef migrations add init`

`dotnet ef database update`

'Code to an abstraction(interface)'