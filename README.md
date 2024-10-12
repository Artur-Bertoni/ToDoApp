# TO DO LIST Application

## Description
This is a "To Do List" web application built using Vue.js for the frontend and ASP.NET Core (C#) for the backend. The backend uses SQL Server for storing task data, and Docker is used for containerized deployment.

## Technologies Used
- **Frontend**: Vue.js
- **Backend**: ASP.NET Core (C#)
- **Database**: SQL Server (using Entity Framework Core)
- **Docker**: For containerization

## Prerequisites for running the Application
- **Docker** - Download and install from [Docker](https://www.docker.com/products/docker-desktop).

## Setting Up the Application Locally (Using Docker)

### Start the Application with Docker

Ensure Docker is running on your machine. In the root of the project, where the [`docker-compose.yml`](./docker-compose.yml) file is located, run the following command:

```bash
docker-compose up --build -d
```

This command will:
1. Build and start the **SQL Server** container on port `1433`.
2. Build and start the **backend** on port `5000`.
3. Build and start the **frontend** on port `80`.

You can access the application at [`http://localhost`](http://localhost).

## Documentation
For detailed instructions on running and developing the frontend or backend individually, refer to their specific documentation:
- [Backend Documentation](./backend/README.md)
- [Frontend Documentation](./frontend/README.md)