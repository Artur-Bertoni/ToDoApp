# Backend Documentation

## Overview
The backend for the "To Do List" application is built using ASP.NET Core (C#) and uses SQL Server as its database. It handles task management, including CRUD operations and deadline handling.

## Technologies Used
- **Framework**: ASP.NET Core (C#)
- **Database**: SQL Server (Entity Framework Core)

## Running the Backend Locally

### Prerequisites
- **.NET SDK** (version 6.0 or above) - [Download](https://dotnet.microsoft.com/pt-br/download/dotnet)
- **SQL Server** (if running locally outside Docker) - [Download](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
- **Entity Framework CLI Tools** (`dotnet-ef`) - Install by running:
   ```bash
   dotnet tool install --global dotnet-ef
   ```

### Running the Backend
1. Clone the project and navigate to the [`backend`](./) directory:
   ```bash
   cd backend
   ```

2. Ensure that you have a running SQL Server instance. You can adjust the connection string in [`appsettings.json`](./appsettings.json) to point to your local SQL Server.

3. Apply the latest migrations to your database:
   ```bash
   dotnet ef database update
   ```

4. Run the backend:
   ```bash
   dotnet run
   ```

By default, the backend will run on the port `5000`.

## API Documentation

### Base URL
The API is available at [`http://localhost:5000/api`](http://localhost:5000/api).

### [Endpoints](./Controllers/TasksController.cs)

- **GET** `/tasks`
    - Retrieves all tasks.

- **GET** `/tasks/{id}`
    - Retrieves a specific task by its ID.

- **POST** `/tasks`
    - Creates a new task. Example request body:
      ```json
      {
        "title": "New Task",
        "description": "Task details",
        "deadline": "2024-10-15T12:00:00"
      }
      ```

- **PUT** `/tasks/{id}`
    - Updates an existing task by its ID. Example request body:
      ```json
      {
        "title": "Updated Task",
        "description": "Updated details",
        "deadline": "2024-10-16T12:00:00"
      }
      ```

- **DELETE** `/tasks/{id}`
    - Deletes a task by its ID.
