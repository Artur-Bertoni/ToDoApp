
# TO DO LIST Application

## Description
This is a "To Do List" web application built using Vue.js for the frontend and ASP.NET Core (C#) for the backend. The backend uses SQL Server for storing task data.

## Technologies Used
- **Frontend**: Vue.js
- **Backend**: ASP.NET Core (C#)
- **Database**: SQL Server (using Entity Framework Core)

## Prerequisites
1. **Node.js** - Download and install from https://nodejs.org/en/download/
2. **.NET Core SDK** - Download and install from https://dotnet.microsoft.com/download
3. **SQL Server** - Download and install SQL Server Express or use Azure SQL Database

## How to Run Locally

### Backend (ASP.NET Core - C#)
1. Navigate to the `CSharpBackend` folder:
    ```bash
    cd CSharpBackend
    ```

2. Restore the dependencies:
    ```bash
    dotnet restore
    ```

3. Apply the migrations to create the database and table for tasks:
    ```bash
    dotnet ef database update
    ```

4. Run the backend server:
    ```bash
    dotnet run
    ```

   The backend will run on `http://localhost:5001`.

### Frontend (Vue.js)
1. Navigate to the `VueFrontend` folder:
    ```bash
    cd VueFrontend
    ```

2. Install the Node.js dependencies:
    ```bash
    npm install
    ```

3. Run the frontend development server:
    ```bash
    npm run serve
    ```

   The frontend will be available at `http://localhost:8080`.

### Database Setup
- The backend is configured to use a local SQL Server instance. The default connection string is set to use LocalDB (`(localdb)\mssqllocaldb`). Ensure SQL Server is running locally.
- The database and table for tasks will be created automatically when running `dotnet ef database update`.

## Version Control
- Ensure you commit your changes to a GitHub repository and share the link for evaluation.
