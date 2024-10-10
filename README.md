# TO DO LIST Application

## Description
This is a "To Do List" web application built using Vue.js for the frontend and ASP.NET Core (C#) for the backend. The backend uses SQL Server for storing task data, which can now be managed using Docker for easier setup.

## Technologies Used
- **Frontend**: Vue.js
- **Backend**: ASP.NET Core (C#)
- **Database**: SQL Server (using Entity Framework Core)

## Prerequisites
1. **Node.js** - Download and install from https://nodejs.org/en/download/
2. **.NET Core SDK** - Download and install from https://dotnet.microsoft.com/download
3. **Docker** - Download and install from https://www.docker.com/products/docker-desktop (for running SQL Server locally via Docker)

## How to Run Locally

### 1. Backend (ASP.NET Core - C#)

#### Setup with Docker (SQL Server)
1. Ensure Docker is running on your machine.

2. In the root of the project, where the `docker-compose.yml` file is located, start the SQL Server container:
    ```bash
    docker-compose up -d
    ```

   This will start a SQL Server container on port `1433`. The container will run in the background.

#### Installing `dotnet-ef` Locally
The `dotnet-ef` tool is required to manage database migrations. Follow these steps to ensure it's installed:

1. Navigate to the `backend` folder:
    ```bash
    cd backend
    ```
   
2. Initialize the tool manifest (only needed the first time):
    ```bash
    dotnet new tool-manifest
    ```

3. Install the `dotnet-ef` tool:
    ```bash
    dotnet tool install dotnet-ef
    ```

#### Running the Backend
Continue into the `backend` folder and run the following commands:

1. Restore the dependencies:
    ```bash
    dotnet restore
    ```

2. **Create the initial migration** (only needed the first time setting up the project):
    ```bash
    dotnet ef migrations add InitialCreate
    ```

3. **Apply the migration** to create/update the database schema:
    ```bash
    dotnet ef database update
    ```

4. Run the backend server:
    ```bash
    dotnet run
    ```

   The backend will run on `http://localhost:5000`, and the database schema will be automatically updated if needed.

### 3. Frontend (Vue.js)
1. Navigate to the `frontend` folder:
    ```bash
    cd frontend
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

### 4. Database Setup
- The backend is configured to use a SQL Server instance running in Docker.
- The connection string is already set to connect to this local SQL Server instance on `localhost:1433` using the credentials defined in the `docker-compose.yml`.
- Migrations are applied automatically when the application starts, and the database will be created/updated without needing to run additional commands.