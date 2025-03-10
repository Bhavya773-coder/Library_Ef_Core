# My API Project (EF Core)

![EF Core API](https://img.shields.io/badge/EF%20Core-API-blue.svg)

## 📌 Overview
This is a powerful API built using **Entity Framework Core**. It provides a robust and scalable architecture for handling database operations efficiently.

## 🚀 Features
- Built with **ASP.NET Core** and **EF Core**
- Uses **Code-First Migrations**
- RESTful API with proper **CRUD operations**
- **JWT Authentication** (if applicable)
- **Swagger UI** for API documentation
- **Repository Pattern** for clean data access

## 🛠️ Installation

### Prerequisites
- .NET SDK installed ([Download](https://dotnet.microsoft.com/))
- SQL Server
- Visual Studio / VS Code

### Setup
1. Clone this repository:
   ```bash
   git clone https://github.com/Bhavya773-coder/Library_Ef_Core.git
   cd Library_Ef_Core
   ```
2. Install dependencies:
   ```bash
   dotnet restore
   ```
3. Configure your database connection in **appsettings.json**:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=your_server;Database=your_db;User Id=your_user;Password=your_password;"
   }
   ```
4. Apply EF Core migrations:
   ```bash
   dotnet ef database update
   ```
5. Run the API:
   ```bash
   dotnet run
   ```

## 🔥 API Endpoints
| Method | Endpoint | Description |
|--------|---------|-------------|
| GET | `/api/items` | Get all items |
| GET | `/api/items/{id}` | Get item by ID |
| POST | `/api/items` | Create a new item |
| PUT | `/api/items/{id}` | Update an item |
| DELETE | `/api/items/{id}` | Delete an item |

## 📖 Documentation
Once the API is running, open Swagger UI:
```
http://localhost:5181/
```

## 🛠 Technologies Used
- **ASP.NET Core**
- **Entity Framework Core**
- **SQL Server /**
- **Swagger UI**


## 🤝 Contributing
1. Fork the repo
2. Create a new branch (`feature/new-feature`)
3. Commit your changes (`git commit -m 'Added a new feature'`)
4. Push to the branch (`git push origin feature/new-feature`)
5. Open a pull request


---
🚀 Happy Coding!
