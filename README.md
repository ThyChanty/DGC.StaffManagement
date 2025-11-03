# Staff Management Backend

This is the **ASP.NET Core backend application** for managing staff members. It provides APIs for creating, reading, updating, and deleting staff records with support for filtering, pagination, and validation.

---

## Table of Contents

- [Tech Stack](#tech-stack)  
- [Features](#features)  
- [Project Structure](#project-structure)  
- [Getting Started](#getting-started)  
- [Available Scripts](#available-scripts)  
- [Configuration](#configuration)  
- [API Endpoints](#api-endpoints)  
- [Notes](#notes)

---

## Tech Stack

- **ASP.NET Core 9**  
- **Entity Framework Core** for database ORM  
- **SQL Server** as database  
- **FluentValidation** for input validation  
- **NLog** for logging  

---

## Features

- CRUD operations for staff members.  
- Filter staff by name, gender, and status.  
- Pagination support.  
- Detailed staff view.  
- Validation for required fields (name, gender, birth date).  
- Proper handling of enum fields like gender (1 = Male, 2 = Female).  
- Error handling and standardized API responses.  

---

## Project Structure

```
src/
 ├─ Controllers/
 │   └─ StaffController.cs
 ├─ Domain/
 │   ├─ Entities/
 │   │   └─ Staff.cs
 │   ├─ Enums/
 │   │   └─ GenderEnum.cs
 │   └─ ValueObjects/
 ├─ Infrastructure/
 │   ├─ Data/
 │   │   └─ ApplicationDbContext.cs
 │   └─ Repositories/
 ├─ Application/
 │   ├─ Commands/
 │   │   ├─ CreateStaffCommand.cs
 │   │   └─ UpdateStaffCommand.cs
 │   ├─ Queries/
 │   │   └─ GetStaffQuery.cs
 │   └─ Services/
 │       └─ StaffService.cs
 ├─ Program.cs
 ├─ appsettings.json
 └─ appsettings.Development.json
```

---

## Getting Started

### 1. Clone the repository

```bash
git clone <repository-url>
cd backend
```

### 2. Restore dependencies

```bash
dotnet restore
```

### 3. Run migrations

```bash
dotnet ef migrations add InitialCreate --startup-project src/DGC.StaffManagement.WebApi --project src/DGC.StaffManagement.Infrastructure -o Migrations
```

```bash
dotnet ef database update --startup-project src/DGC.StaffManagement.WebApi --project src/DGC.StaffManagement.Infrastructure -o Migrations
```

### 4. Start the backend server

```bash
dotnet run --project src/DGC.StaffManagement.WebApi
```

The API will be available at `http://localhost:5122/api`.

---

## Available Scripts

- `dotnet run` – start the development server  
- `dotnet build` – build the project  
- `dotnet test` – run unit tests  
- `dotnet ef migrations add <Name>` – add a new EF migration  
- `dotnet ef database update` – apply migrations to the database  

---

## Configuration

- **appsettings.json** – main configuration file  
- **Database connection** – configure connection string to SQL Server  


Example connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=staffdb;Username=postgres;Password=yourpassword"
}
```

---

## API Endpoints

| Method | Endpoint                | Description                     |
|--------|------------------------|---------------------------------|
| GET    | /api/staff             | Get all staff (with pagination) |
| GET    | /api/staff/{id}        | Get staff by ID                 |
| POST   | /api/staff             | Create new staff                |
| PUT    | /api/staff/{id}        | Update existing staff           |
| DELETE | /api/staff/{id}        | Delete staff                    |

### Notes on Gender Field
- Male = 1  
- Female = 2  
- Backend always stores and returns gender as an integer to ensure consistency with frontend.

---

## Notes

- Validation ensures required fields are provided.  
- Proper error messages are returned in a consistent format.  
- Ensure backend and frontend agree on enum values for gender.  
- Use FluentValidation for additional field rules if needed.  

---

This README can be updated with more API examples, authentication setup, or deployment instructions as needed.

