Sure! Here is a comprehensive README documentation for your loyalty system API project:

---

# Loyalty System API

This project is a minimal loyalty system API built with .NET Core that allows users to earn points. The project includes functionalities such as logging, database access, request validation, authentication, caching, unit testing, and containerization.

## Features

- **API Development**: Developed using .NET Core.
- **Logging**: Integrated with Serilog for logging.
- **Database**: Uses Entity Framework Core (EFCore) for database access with a simple user and points table.
- **Validation**: Uses FluentValidation for request validation.
- **Unit Testing**: Unit tests written using XUnit and Moq.
- **Authentication**: Implemented OAuth2 authentication.
- **Caching**: Integrated with Redis for caching user points.
- **Containerization**: Containerized using Docker.

## Prerequisites

- [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
- [Docker](https://www.docker.com/get-started)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Redis](https://redis.io/download)

## Getting Started

### Clone the Repository

```sh
git clone https://github.com/yourusername/loyaltysystemapi.git
cd loyaltysystemapi
```

### Configure the Project

Update the `appsettings.json` file with your SQL Server and Redis connection strings.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your SQL Server connection string here",
    "Redis": "Your Redis connection string here"
  },
  "OAuth": {
    "ClientId": "Your OAuth Client ID",
    "ClientSecret": "Your OAuth Client Secret",
    "AuthorizationEndpoint": "Your OAuth Authorization Endpoint",
    "TokenEndpoint": "Your OAuth Token Endpoint"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}
```

### Build and Run Locally

#### Running with .NET Core

1. **Build the Project**

    ```sh
    dotnet build
    ```

2. **Run the Project**

    ```sh
    dotnet run
    ```

The application will start and listen on `http://localhost:5000`.

### Running with Docker

1. **Build the Docker Image**

    ```sh
    docker build -t loyaltysystemapi .
    ```

2. **Run the Docker Container**

    ```sh
    docker run -d -p 8080:80 --name loyaltysystemapi_container loyaltysystemapi
    ```

The application will start and listen on `http://localhost:8080`.

## API Endpoints

### Earn Points

- **Endpoint**: `POST /api/users/{id}/earn`
- **Description**: Adds points to a user’s balance.
- **Request Body**:

    ```json
    {
      "points": 10
    }
    ```

- **Response**:

    ```json
    {
      "id": 1,
      "name": "John Doe",
      "points": 20
    }
    ```

### Authentication

This API uses OAuth2 for authentication. Make sure to configure your OAuth2 provider details in the `appsettings.json`.

## Unit Testing

Unit tests are written using XUnit and Moq. To run the unit tests, execute the following command:

```sh
dotnet test
```

## Project Structure

```
LoyaltySystemAPI/
├── Controllers/
│   └── UsersController.cs
├── Data/
│   └── ApplicationDbContext.cs
├── Models/
│   ├── User.cs
│   └── Points.cs
├── Validators/
│   └── EarnPointsRequestValidator.cs
├── Program.cs
├── Startup.cs
├── appsettings.json
├── appsettings.Development.json
├── Dockerfile
└── LoyaltySystemAPI.csproj
```

## Contributing

Feel free to open issues or submit pull requests.

## License

This project is licensed under the MIT License.

---

This README provides a comprehensive guide to setting up and running the loyalty system API, including details about features, prerequisites, setup instructions, API endpoints, and project structure.