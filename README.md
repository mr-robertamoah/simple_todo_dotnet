# Todo API


Welcome to the Todo API! This API allows you to manage a list of todo items, including creating, reading, updating, and deleting todo items.

## Features

- Create a new todo
- Retrieve a list of todos
- Update an existing todo
- Deleting a todo
- User registration
- User login
- User logout



## Endpoints
- **Method:** `POST`

    ```json
### Create a new task
        "title": "Task title",

    }
- **URL:** `/api/tasks`
- **Response:**
- **Method:** `POST`
    {
- **Request Body:**
        "title": "Task title",
    ```json
        "completed": false
    {
    ```
          "description": "Task description"

    }
- **Method:** `GET`
    ```
    ```json
- **Response:**
        {
    ```json
            "title": "Task title",
    {
            "completed": false
        "id": 1,
        ...
        "title": "Task title",
    ```
          "completed": false

    }
- **Method:** `PUT`
    ```
    ```json

        "title": "Updated title",
### Retrieve a list of tasks
        "completed": true

    ```
- **URL:** `/api/tasks`
    ```json
- **Method:** `GET`
        "id": 1,
- **Response:**
        "description": "Updated description",
    ```json
    }
    [

        {

            "id": 1,
- **Method:** `DELETE`
            "title": "Task title",

            "description": "Task description",

            "completed": false

        },
     ```sh
        ...
     ```
    ]
     ```sh
    ```
     ```

     ```sh
### Update an existing task
     ```

     ```sh
- **URL:** `/api/tasks/{id}`
     ```
- - **Request Body:**

    ```json

    {

        "title": "Updated title",        "description": "Updated description",        "completed": true    }    ```- **Response:**    ```json    {        "id": 1,        "title": "Updated title",        "description": "Updated description",        "completed": true    }    ```### Delete a task- **URL:** `/api/tasks/{id}`- **Method:** `DELETE`- **Response:** `204 No Content`### Register a new user- **URL:** `/api/auth/register`- **Method:** `POST`- **Request Body:**    ```json    {        "username": "yourusername",        "password": "yourpassword"    }    ```- **Response:**    ```json    {        "message": "User registered successfully"    }    ```### Login a user- **URL:** `/api/auth/login`- **Method:** `POST`- **Request Body:**    ```json    {
        "username": "yourusername",
        "password": "yourpassword"
    }
    ```
- **Response:**
    ```json
    {
        "token": "your.jwt.token"
    }
    ```

### Logout a user

- **URL:** `/api/auth/logout`
- **Method:** `POST`
- **Response:**
    ```json
    {
        "message": "User logged out successfully"
    }
    ```

## Getting Started

To run the Todo API locally, follow these steps:

1. Clone the repository:
     ```sh
     git clone https://github.com/yourusername/TodoAPIDotNet.git
     ```
2. Navigate to the project directory:
     ```sh
     cd TodoAPIDotNet
     ```
3. Restore dependencies:
     ```sh
     dotnet restore
     ```
4. Run the application:
     ```sh
     dotnet run
     ```

## Contributing

Contributions are welcome! Please open an issue or submit a pull request.

## License

This project is licensed under the MIT License.