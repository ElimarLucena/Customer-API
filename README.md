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
    CustomerId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Age INT NOT NULL,
    Phone INT NOT NULL,
    Document NVARCHAR(255) NOT NULL,
    Password NVARCHAR(255) NOT NULL
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
