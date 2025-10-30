<h1 align="center">Customer WebApi :information_desk_person: </h1>

<p align="center">WebApi responsible for managing all customer information, such as registration, update, search and deletion information.</p>

<details>
  <summary>
    <h3>:wrench: Recommended tools</h3>
  </summary>
  <ul>
    <li>
      <a href="https://visualstudio.microsoft.com/vs/" target="_blank" title="https://visualstudio.microsoft.com/vs/">
        Visual Studio 2022
      </a>
    </li>
    <li>
      <a href="https://code.visualstudio.com/" target="_blank" title="https://code.visualstudio.com/">
        Visual Studio Code
      </a>
    </li>
    <li>
      <a href="https://azure.microsoft.com/pt-br/products/data-studio" target="_blank" title="https://azure.microsoft.com/pt-br/products/data-studio">
        Azure Data Studio
      </a>
    </li>
    <li>
      <a href="https://www.docker.com/products/docker-desktop/" target="_blank" title="https://www.docker.com/products/docker-desktop/">
        Docker Desktop
      </a>
    </li>
  </ul>
</details>


<details>
  <summary>
    <h3>:blue_book: DataBase</h3>
  </summary>
  <p>
    <strong>Tables:</strong>
  </p>

```sql
CREATE DATABASE CUSTOMER;

USE CUSTOMER;

CREATE TABLE TB_CUSTOMERS (
    CUSTOMER_ID UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    NAME NVARCHAR(255) NOT NULL,
    EMAIL NVARCHAR(255) NOT NULL,
    AGE INT NOT NULL,
    PHONE INT NOT NULL,
    DOCUMENT NVARCHAR(255) NOT NULL,
    PASSWORD NVARCHAR(255) NOT NULL,
    CREATED_AT DATETIME2 DEFAULT GETDATE(),
    UPDATED_AT DATETIME2
);
```
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
> Attention! Before making requests, make sure that the database and its tables have been created correctly.
  <p><strong>1.2 - Access the Link in your browser:</strong></p>
  
```js
  http://localhost:8080/swagger/index.html
```
</details>


<details>
  <summary>
    <h3>:test_tube: Run Unit Tests</h3>
  </summary>
  <p><strong>Steps:</strong></p>
  <p><strong>1.1 - Run command:</strong></p>
  
```csharp
  dotnet test ./tests/UnitTests/UnitTests.csproj
```
</details>


<details>
  <summary>
    <h3>:test_tube: Run Integration Tests</h3>
  </summary>
  <p><strong>Steps:</strong></p>
  <p><strong>1.1 - Run command:</strong></p>
  
```csharp
  dotnet test ./tests/IntegrationTests/IntegrationTests.csproj
```
</details>

<details>
  <summary>
    <h3>🕵️ Observability</h3>
  </summary>
  <details>
  <summary>
    <h3>Jaeger</h3>
  </summary>
  <p><strong>1.1 - Access the documentation:</strong></p>
    <a href="https://www.jaegertracing.io/" target="_blank" title="https://www.jaegertracing.io/">
          https://www.jaegertracing.io/
    </a>
    <p></p>
  <p><strong>1.2 - Access the Jaeger UI:</strong></p>
  <a href="http://localhost:16686" target="_blank" title="http://localhost:16686">
        http://localhost:16686
  </a>
  </details>
</details>
