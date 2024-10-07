# TodoAPI

This is a simple Todo API built with .NET.

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version X.X or later)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or any other supported database

### Installation

1. Clone the repository:
    ```sh
    git clone https://github.com/yourusername/TodoAPIDotNet.git
    cd TodoAPIDotNet
    ```

2. Restore the dependencies:
    ```sh
    dotnet restore
    ```

3. Update the database connection string in `appsettings.json`.

4. Apply database migrations:
    ```sh
    dotnet ef database update
    ```

### Running the API

To run the API locally, use the following command:
```sh
dotnet run
```

The API will be available at `https://localhost:5000` or stated in the output of the command.


## API Endpoints

### Get all todos
```http
GET /api/todos
```

### Get a specific todo
```http
GET /api/todos/{id}
```

### Create a new todo
```http
POST /api/todos
```

### Update a todo
```http
PUT /api/todos/{id}
```

### Delete a todo
```http
DELETE /api/todos/{id}
```

## Contributing

Contributions are welcome! Please open an issue or submit a pull request.

## License

This project is licensed under the MIT License.