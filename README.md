# Customer-API
## http://localhost:8080/swagger/index.html

## docker-compose -f "docker-compose.yml" up -d --build

### DataBase (SQL SERVER):
<details>
  <summary>Tables</summary>

```sql
CREATE DATABASE Customer;

IF NOT EXISTS (SELECT * FROM sysobjects WHERE NAME='TB_CUSTOMERS' AND xtype='U')
    CREATE TABLE TB_CUSTOMERS (
        CustomerId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
        Name NVARCHAR(255) NOT NULL,
        Email NVARCHAR(255) NOT NULL,
        Age INT NOT NULL,
        Phone INT NOT NULL,
        Document NVARCHAR(255) NOT NULL,
        Password NVARCHAR(255) NOT NULL
    )
go
```
</details>