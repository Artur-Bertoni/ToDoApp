# TO DO LIST Application

## Overview
This is a web application for managing tasks efficiently. Users can add, edit, remove, and mark tasks as completed, as well as assign deadlines. The application uses **Vue.js** for the frontend and **ASP.NET Core (C#)** for the backend. Task data is stored in **SQL Server**, and everything (frontend, backend, and database) is managed via **Docker** for easy setup.

## Technologies Used
- **Frontend**: Vue.js
- **Backend**: ASP.NET Core (C#)
- **Database**: SQL Server (via Entity Framework Core)
- **Containerization**: Docker (orchestrating SQL Server, backend, and frontend)

## Prerequisites
Ensure you have **Docker** installed on your machine to run the entire application locally:
1. **Docker** - [Download Docker](https://www.docker.com/products/docker-desktop)

## Running the Project Locally

All services (frontend, backend, and SQL Server database) will be handled by Docker. You only need to run the `docker-compose` command, and Docker will take care of building and running everything.

### Steps:
1. Clone the repository and navigate to the project directory.
2. Ensure Docker is running on your machine.
3. Run the following command to build and start the entire project:
    ```bash
    docker-compose up --build
    ```
   This command will:
   - Build and run the **SQL Server** database container (on port `1433`).
   - Build and run the **backend** (ASP.NET Core) on `http://localhost:5000`.
   - Build and serve the **frontend** (Vue.js) on `http://localhost:8080`.

### What Happens Automatically:
- **SQL Server**: A container with SQL Server will be started on port `1433`.
- **Backend**:
   - The backend will automatically restore dependencies.
   - Any database migrations will be automatically applied to set up the database schema.
   - The backend will run on `http://localhost:5000`.
- **Frontend**: The frontend server will be built and served via an NGINX container, available at `http://localhost:8080`.

### 4. Shutting Down the Application
To stop and remove the containers, you can run:
```bash
docker-compose down
```

---

### Additional Information:
- No manual steps are needed to install Node.js, .NET SDK, or manage database migrations.
- Everything (dependencies, migrations, builds) is handled by Docker during the `docker-compose up` process.