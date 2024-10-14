# Todo API


Welcome to the Todo API! This API allows you to manage a list of todo items, including creating, reading, updating, and deleting todo items.

## Features

- User registration
- User login
- User logout
- Deleting User account
- Create a new todo
- Retrieve a list of todos
- Update an existing todo
- Deleting a todo



## Endpoints

### Create a user account
- **URL:** `/api/user/register`
- **Method:** `POST`
- **Request Body:**
    ```json
    {
        "username": "user",
        "email": "user@example.com",
        "name": "Robert Amoah",
        "password": "Password@123",
        "passwordConfirmation": "Password@123"
    }
    ```
- **Response:**
    ```json
    { 
        "message": "User successfully Registered.", 
        "token ": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c2VyIiwic2lkIjoiOTBjYWFjMTItZjExZC00MjUwLWFmNjgtMDg1N2E2ZDg2MTlhIiwiZXhwIjoxNzI4OTA2MjQzLCJpc3MiOiJZb3VySXNzdWVyIiwiYXVkIjoiWW91ckF1ZGllbmNlIn0.9MkiVNC7u3_uE2ZiyT7cG_oZ90Zf639c41fLUCGy5gc"
    }
    ```

### Login into user account
- **URL:** `/api/user/login`
- **Method:** `POST`
- **Request Body:**
    ```json
    {
        "username": "user", // required when email is null
        "email": "user@example.com", // required when username is null
        "password": "Password@123",
    }
    ```
- **Response:**
    ```json
    { 
        "message": "User successfully logged in.", 
        "token ": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c2VyIiwic2lkIjoiOTBjYWFjMTItZjExZC00MjUwLWFmNjgtMDg1N2E2ZDg2MTlhIiwiZXhwIjoxNzI4OTA2MjQzLCJpc3MiOiJZb3VySXNzdWVyIiwiYXVkIjoiWW91ckF1ZGllbmNlIn0.9MkiVNC7u3_uE2ZiyT7cG_oZ90Zf639c41fLUCGy5gc"
    }
    ```

### Get user information
- **URL:** `/api/user`
- **Method:** `GET`
- **Request Auth:**
    ```Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c2VyIiwic2lkIjoiOTBjYWFjMTItZjExZC00MjUwLWFmNjgtMDg1N2E2ZDg2MTlhIiwiZXhwIjoxNzI4OTA2MjQzLCJpc3MiOiJZb3VySXNzdWVyIiwiYXVkIjoiWW91ckF1ZGllbmNlIn0.9MkiVNC7u3_uE2ZiyT7cG_oZ90Zf639c41fLUCGy5gc```
- **Request Body:**
    ```empty```
- **Response:**
    ```json
    { 
        "message": "Successfully retrieved the information of User with id: {account id}.",
        "data": {
            "id": "string",
            "name": "string",
            "username": "string",
            "email": "string",
        }
    }
    ```
    
### Logout of user account
- **URL:** `/api/user/logout`
- **Method:** `POST`
- **Request Auth:**
    ```Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c2VyIiwic2lkIjoiOTBjYWFjMTItZjExZC00MjUwLWFmNjgtMDg1N2E2ZDg2MTlhIiwiZXhwIjoxNzI4OTA2MjQzLCJpc3MiOiJZb3VySXNzdWVyIiwiYXVkIjoiWW91ckF1ZGllbmNlIn0.9MkiVNC7u3_uE2ZiyT7cG_oZ90Zf639c41fLUCGy5gc```
- **Request Body:**
    ```empty```
- **Response:**
    ```json
    { 
        "message": "Successfully logged out.", 
    }
    ```

### Delete user account
- **URL:** `/api/user`
- **Method:** `DELETE`
- **Request Auth:**
    ```Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c2VyIiwic2lkIjoiOTBjYWFjMTItZjExZC00MjUwLWFmNjgtMDg1N2E2ZDg2MTlhIiwiZXhwIjoxNzI4OTA2MjQzLCJpc3MiOiJZb3VySXNzdWVyIiwiYXVkIjoiWW91ckF1ZGllbmNlIn0.9MkiVNC7u3_uE2ZiyT7cG_oZ90Zf639c41fLUCGy5gc```
- **Request Body:**
    ```empty```
- **Response:**
    ```json
    { 
        "message": "Your User account has been successfully deleted.", 
    }
    ```

### Create todo
- **URL:** `/api/todo`
- **Method:** `POST`
- **Request Auth:**
    ```Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c2VyIiwic2lkIjoiOTBjYWFjMTItZjExZC00MjUwLWFmNjgtMDg1N2E2ZDg2MTlhIiwiZXhwIjoxNzI4OTA2MjQzLCJpc3MiOiJZb3VySXNzdWVyIiwiYXVkIjoiWW91ckF1ZGllbmNlIn0.9MkiVNC7u3_uE2ZiyT7cG_oZ90Zf639c41fLUCGy5gc```
- **Request Body:**
    ```json
    {
        "title": "another todo",
        "description": "description of todo",
        "dueDate": "2024-10-14T03:11:13.272Z",
        "priority": 5
    }
    ```
- **Response:**
    ```json
    { 
        "message": "Todo successfully created.", 
    }
    ```

### Retrieve all todos of a user
- **URL:** `/api/todo`
- **Method:** `GET`
- **Request Auth:**
    ```Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c2VyIiwic2lkIjoiOTBjYWFjMTItZjExZC00MjUwLWFmNjgtMDg1N2E2ZDg2MTlhIiwiZXhwIjoxNzI4OTA2MjQzLCJpc3MiOiJZb3VySXNzdWVyIiwiYXVkIjoiWW91ckF1ZGllbmNlIn0.9MkiVNC7u3_uE2ZiyT7cG_oZ90Zf639c41fLUCGy5gc```
- **Request Body:**
    ```empty```
- **Response:**
    ```json
    {
        "message": "Successfully retrieved all Todo items.",
        "todos": [
            {
                "id": "1e2f2310-ccfb-4b04-7091-08dcec01c56d",
                "title": "new todo",
                "description": "description of todo",
                "isComplete": false,
                "dueDate": "0001-01-01T00:00:00",
                "priority": 5,
                "createdAt": "2024-10-14T03:39:08.8980457",
                "updatedAt": "0001-01-01T00:00:00"
            },
            {
                "id": "a52b07de-33e2-4b1b-097b-08dcec3d8c26",
                "title": "another todo",
                "description": "description of todo",
                "isComplete": false,
                "dueDate": "0001-01-01T00:00:00",
                "priority": 5,
                "createdAt": "2024-10-14T10:47:06.7742708",
                "updatedAt": "0001-01-01T00:00:00"
            }
        ]
    }
    ```

### Update an existing todo
- **URL:** `/api/todo/{id}`
- **Method:** `PUT`
- **Request Auth:**
    ```Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c2VyIiwic2lkIjoiOTBjYWFjMTItZjExZC00MjUwLWFmNjgtMDg1N2E2ZDg2MTlhIiwiZXhwIjoxNzI4OTA2MjQzLCJpc3MiOiJZb3VySXNzdWVyIiwiYXVkIjoiWW91ckF1ZGllbmNlIn0.9MkiVNC7u3_uE2ZiyT7cG_oZ90Zf639c41fLUCGy5gc```
- **Request Body:**
    ```json
    {
        "title": "another todo updated",
        "dueDate": "2024-10-14T03:11:13.272Z",
        "priority": 4
    }
    ```
- **Response:**
    ```json
    { 
        "message": "Todo item with id a52b07de-33e2-4b1b-097b-08dcec3d8c26 successfully updated.", 
    }
    ```

### Get a todo
- **URL:** `/api/todo/{id}`
- **Method:** `GET`
- **Request Auth:**
    ```Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c2VyIiwic2lkIjoiOTBjYWFjMTItZjExZC00MjUwLWFmNjgtMDg1N2E2ZDg2MTlhIiwiZXhwIjoxNzI4OTA2MjQzLCJpc3MiOiJZb3VySXNzdWVyIiwiYXVkIjoiWW91ckF1ZGllbmNlIn0.9MkiVNC7u3_uE2ZiyT7cG_oZ90Zf639c41fLUCGy5gc```
- **Request Body:**
    ```empty```
- **Response:**
    ```json
    { 
        "message": "Successfully retrieved Todo with id: a52b07de-33e2-4b1b-097b-08dcec3d8c26.",
        "data": {
            "id": "a52b07de-33e2-4b1b-097b-08dcec3d8c26",
            "title": "another todo",
            "description": "description of todo",
            "isComplete": false,
            "dueDate": "0001-01-01T00:00:00",
            "priority": 5,
            "createdAt": "2024-10-14T10:47:06.7742708",
            "updatedAt": "0001-01-01T00:00:00"
        }
    }
    ```

### Delete an existing todo
- **URL:** `/api/todo/{id}`
- **Method:** `DELETE`
- **Request Auth:**
    ```Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c2VyIiwic2lkIjoiOTBjYWFjMTItZjExZC00MjUwLWFmNjgtMDg1N2E2ZDg2MTlhIiwiZXhwIjoxNzI4OTA2MjQzLCJpc3MiOiJZb3VySXNzdWVyIiwiYXVkIjoiWW91ckF1ZGllbmNlIn0.9MkiVNC7u3_uE2ZiyT7cG_oZ90Zf639c41fLUCGy5gc```
- **Request Body:**
    ```empty```
- **Response:**
    ```json
    { 
        "message": "Your User account has been successfully deleted.", 
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