# TO DO LIST Application

## Description
This is a "To Do List" web application built using Vue.js for the frontend and ASP.NET Core (C#) for the backend. The backend uses SQL Server for storing task data, which can now be managed using Docker for easier setup.

## Technologies Used
- **Frontend**: Vue.js
- **Backend**: ASP.NET Core (C#)
- **Database**: SQL Server (using Entity Framework Core)

## Prerequisites
- **Docker** - Download and install from [Docker](https://www.docker.com/products/docker-desktop) (required for running the project)

## How to Run Locally (Using Docker)

### 1. Start the Application with Docker

Ensure Docker is running on your machine. In the root of the project, where the `docker-compose.yml` file is located, run the following command:

```bash
docker-compose up --build -d
```

This command will:
1. Build and start the **SQL Server** container on port `1433`.
2. Build and start the **backend** on port `5000`.
3. Build and start the **frontend** on port `80`.

You can access the application at:
- **Frontend**: [http://localhost](http://localhost)
- **Backend**: [http://localhost:5000](http://localhost:5000)

### 2. Database Migrations

The backend is configured to automatically **apply migrations** at runtime when the application starts. This means that any pending migrations will be applied to the SQL Server database automatically when you start the backend.

#### Creating New Migrations

If you need to create new migrations (e.g., after modifying the data model), follow these steps:

1. Open a terminal and navigate to the `backend` folder:
   ```bash
   cd backend
   ```

2. Use the `dotnet-ef` tool (installed via the Docker environment) to create a new migration:
   ```bash
   docker-compose run --rm backend dotnet ef migrations add <MigrationName>
   ```

   Replace `<MigrationName>` with an appropriate name for your migration (e.g., `AddDueDateToTask`).


3. The new migration will be automatically applied the next time the backend is restarted. You can restart the backend by running:
   ```bash
   docker-compose restart backend
   ```
   
## Frontend Development

If you need to make changes to the frontend and want to run the development server (with hot-reloading), you can use the following steps, but keep in mind that this will only work if you got `Node.js` and `npm` installed in your machine:

1. Navigate to the `frontend` folder:
   ```bash
   cd frontend
   ```

2. Install the Node.js dependencies (this step is needed if you're running outside the Docker container):
   ```bash
   npm install
   ```

3. Run the frontend development server:
   ```bash
   npm run serve
   ```

   The frontend will be available at `http://localhost:8080` in development mode.

## Backend Development

You can access the backend API directly at `http://localhost:5000`. Any changes to the backend code will require rebuilding the Docker container:

```bash
docker-compose up --build -d
```

### Database Setup (Runtime Migrations)

- The backend uses a SQL Server instance running in Docker. The connection string is set to connect to this local SQL Server instance using the credentials defined in the `docker-compose.yml`.
- Migrations are automatically applied during runtime using the `context.Database.Migrate()` method, so the database schema will be updated automatically.