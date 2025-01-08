## Shared Market Fullstack

One fullstack project for a shared marketplace, created by ReactJS and DotNet Web API.

`docker pull mcr.microsoft.com/azure-sql-edge`

`docker run -e 'ACCEPT_EULA=1' -e 'MSSQL_SA_PASSWORD=MyStrongPass123' -e 'MSSQL_PID=Developer' -e 'MSSQL_USER=SA' -p 1433:1433 -d --name sql mcr.microsoft.com/azure-sql-edge`

`docker run -e 'ACCEPT_EULA=1' -e 'MSSQL_SA_PASSWORD=MyStrongPass123' -e 'MSSQL_PID=Developer' -e 'MSSQL_USER=SA' -p 1433:1433 -d --name sql mcr.microsoft.com/azure-sql-edge`

`dotnet tool install --global dotnet-ef --version 8.0.11`

`dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.11`

`dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 8.0.11`

`dotnet ef migrations add Init`

`dotnet ef database update`

`dotnet ef migrations add Identity`

`dotnet ef database update`

'Code to an abstraction(interface)'