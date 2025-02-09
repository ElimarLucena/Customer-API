<h1 align="center">Customer WebApi :information_desk_person: </h1>

<p align="center">WebApi responsible for managing customer information, such as registration, update, search and deletion information.</p>

<details>
  <summary>
    <h3>:wrench: Recommended tools</h3>
  </summary>
  <ul>
    <li>
      <strong>Visual Studio 2022</strong>
    </li>
    <li>
      <strong>Visual Studio Code</strong>
    </li>
    <li>
      <strong>Azure Data Studio</strong>
    </li>
    <li>
      <strong>Docker Desktop</strong>
    </li>
  </ul>
</details>


<details>
  <summary>
    <h3>:running: Run Project</h3>
  </summary>
  <p><strong>Steps:</strong></p>
  <p><strong>1.1 - Run Docker Compose command:</strong></p>

```Dockerfile
  docker-compose -f "docker-compose.yml" up -d --build
```
  <p><strong>1.2 - Run Link in your browser:</strong></p>
  
```js
  http://localhost:8080/swagger/index.html
```
</details>


<details>
  <summary>
    <h3>:test_tube: Run Integrations Tests</h3>
  </summary>
  <p><strong>Steps:</strong></p>
  <p><strong>1.1 - Run Docker Compose command:</strong></p>

```Dockerfile
  docker-compose -f "docker-compose-test.yml" up -d --build
```
  <p><strong>1.2 - Navigate to test project directory:</strong></p>
  
```PowerShell
  cd .\tests\IntegrationTests\
```
  <p><strong>1.3 - Run command:</strong></p>
  
```csharp
  dotnet test
```
</details>


<details>
  <summary>
    <h3>:blue_book: DataBase</h3>
  </summary>
  <p>
    <strong>Tables:</strong>
  </p>

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
