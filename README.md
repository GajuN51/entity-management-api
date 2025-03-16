# Entity Management Backend (ASP.NET Core Web API)

A **RESTful API** built with ASP.NET Core Web API to manage entities like **Name, Surname, Age, Email, and Phone**.

## Features  
- **Add Records** → Add a new entity to the database  
- **Delete Records** → Delete an entity by name or by mobileNumber 
- **List Records** → Retrieve all stored entities  
- **Search Records** → Find entities by name  or by mobileNumber
- **Uses PostgreSQL (Dockerized)** for storage  
- **Entity Framework Core** for database operations  
- **Proper error handling and logging** implemented  

##  Technologies Used  
- **ASP.NET Core Web API**  
- **Entity Framework Core**  
- **PostgreSQL (via Docker)**  
- **Serilog** (for logging)  
- **Docker & Docker Compose**  

##  API Endpoints  

| Method | Endpoint | Description |
|--------|---------|-------------|
| POST | `/api/entity/add` | Adds a new record |
| DELETE | `/api/entity/delete/{name}` | Deletes a record by name |
| DELETE | `/api/entity/delete/{mobileNumber}` | Deletes a record by mobileNumber |
| GET | `/api/entity/list` | Retrieves all records |
| GET | `/api/entity/search/{name}` | Retrieves records by name |
| GET | `/api/entity/search/{mobileNumber}` | Retrieves records by mobileNumber |

##  Getting Started  

### 1️ Clone the Repository  
```sh
git clone https://github.com/GajuN51/EntityManagement-Backend.git
cd EntityManagement-Backend
```

### 2️ Setup the PostgreSQL Database (Dockerized)  

1. Ensure **Docker & Docker Compose** are installed  
2. Run the following command to start PostgreSQL:  
   ```sh
   docker-compose up -d
   ```
3. The database will be available at `localhost:5432`  

### 3️ Configure Database Connection  

Modify `appsettings.json`:  
```json
"ConnectionStrings": {
   "DefaultConnection": "Host=localhost;Port=5432;Database=DBname;Username=user'susername;Password=yourpassword"
}
```

### 4️ Apply Database Migrations  
```sh
dotnet tool install --global dotnet-ef(install the Entity Framework Core CLI tool globall)
dotnet ef migrations add InitialCreate(Create initial migration files)
dotnet ef database update
```

### 5️ Run the Application  
```sh
dotnet run
```
The API will be available at `http://localhost:5000`.



