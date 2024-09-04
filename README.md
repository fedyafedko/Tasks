# TestAssignment Luna Edge .NET

 .NET backend service that manages a simple task management system with user authentication.

## Setup instructions

### Prerequisites

- .NET 8
- MS SQL Server
- Docker
- docker compose

### Steps

#### Variant with Docker

1. Clone the repository:

   
    git clone https://github.com/fedyafedko/Tasks
    
2. Go to the desired folder:

   
    cd Tasks-BE
    
3. Up docker-compose:

   
    docker-compose up --build
    
#### Variant without Docker

1. Clone the repository:

   
    git clone https://github.com/fedyafedko/Tasks
    
2. Open solution:

   
    open Task-BE.sln
    
3. Run project

## API documentation

```sh
POST http://localhost:8080/api/users/register
```
   Registers a new user by providing a username, email, and password. This endpoint creates a user account and stores the credentials for future authentication.
   
```sh
POST http://localhost:8080/api/users/login
```
   Authenticates a user using their email and password. On successful authentication, returns a JWT token for session management and authorization.

```sh
POST http://localhost:8080/api/tasks
```
   Creates a new task for the authenticated user. The user must provide details such as the task's title, description, due date, status, and priority.
```sh
GET http://localhost:8080/api/tasks?Status={Status}&DueDate={DueDate}&Priority={Priority}&Sorting={Sorting}&Page={Page}&PageSize={PageSize}
```
   Fetches a list of tasks for the authenticated user. Supports optional filtering by status, due date, and priority. Allows sorting by status and due date, and includes pagination for managing large task lists.
```sh
GET http://localhost:8080/api/tasks/{id}
```
   Retrieves the details of a specific task using its unique ID. Requires authentication to ensure the task belongs to the user making the request.
```sh
PUT http://localhost:8080/api/tasks/{id}
```
   Updates the details of an existing task. The task's ID is used to identify which task to update. The user must be authenticated to modify their tasks.

```sh
DELETE http://localhost:8080/api/tasks/{id}
```
   Deletes a specific task by its ID. The operation requires user authentication to ensure that only the owner of the task can delete it.

## Architecture and Design Choices for the API

#### Three-Layer Architecture
  The project is structured using a Three-Layer Architecture, which separates the application into three distinct layers: Presentation Layer, Business Logic Layer (BLL), and Data Access Layer (DAL). This architectural choice enhances the maintainability, scalability, and testability of the application.

#### Design Patterns and Principles

1. Dependency Injection: 

   
    The project utilizes Dependency Injection (DI) to manage dependencies between classes. DI promotes loose coupling and enhances the testability of the application by allowing dependencies to be injected rather than hard-coded.
    
2. Automapper:

   
    Automapper is employed for mapping between domain models and DTOs. This reduces boilerplate code and ensures consistent mapping logic throughout the application.
    
3. Repository Pattern:

   
    The repository pattern is used in the Data Access Layer to encapsulate database operations. This pattern provides a clean API for data access, abstracting the data storage mechanism from the business logic.

#### Security Considerations

1. JWT Authentication: 

   
    The application uses JSON Web Tokens (JWT) for authentication, ensuring secure and stateless communication between the client and server.
    
2. Validation and Error Handling:

   
    Input validation is performed both at the API controller level and within the business logic, ensuring that only valid data is processed. FluentValidation are used where necessary.
