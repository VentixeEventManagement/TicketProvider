TicketProvider API
A modern, RESTful microservice for ticket management built with .NET 8. This API enables you to create, retrieve, update, and delete tickets for events, following best practices for microservice architecture.
https://img.shields.io/badge/.NET-8.0-512BD4 https://img.shields.io/badge/API-REST-8A2BE2 https://img.shields.io/badge/Swagger-Documented-85EA2D
---
📋 Features
•	Full ticket management: create, read, update, and delete operations
•	Associate tickets with specific events
•	RESTful API design with standard HTTP methods and status codes
•	Interactive Swagger documentation
•	Layered architecture: clear separation of controller, business, and data layers
•	Entity Framework Core with SQL Server support
•	Comprehensive integration testing
•	CORS enabled for all origins
---
🚀 Getting Started
Prerequisites:
•	.NET 8 SDK (https://dotnet.microsoft.com/download/dotnet/8.0)
•	SQL Server (LocalDB is configured by default)
Installation:
1.	Clone the repository:
git clone https://github.com/your-username/ticket-provider.git
cd ticket-provider
2.	Restore dependencies:
dotnet restore
3.	Apply database migrations:
dotnet ef database update --project TicketProvider
4.	Run the application:
dotnet run --project TicketProvider
5.	Access Swagger UI:
HTTP: http://localhost:5240/swagger
HTTPS: https://localhost:7022/swagger
---
📖 API Documentation
Endpoints:
•	GET /api/ticket — Retrieve all tickets
•	GET /api/ticket/{id} — Retrieve a specific ticket by ID
•	GET /api/ticket/by-event/{eventId} — Retrieve all tickets for a specific event
•	POST /api/ticket — Create a new ticket
•	PUT /api/ticket/{id} — Update an existing ticket
•	DELETE /api/ticket/{id} — Delete a specific ticket
Sample Request (Create Ticket):
To create a ticket, send a POST request to /api/ticket with a JSON body like:
{ "eventId": 1, "holderName": "Jane Doe", "holderEmail": "jane.doe@example.com", "price": 42.00 }
---
🏗️ Architecture
The project uses a layered architecture:
•	API Layer: Controllers handle HTTP requests and responses
•	Business Layer: Services implement business logic and domain models
•	Data Access Layer: Repositories manage data persistence
Key Components:
•	TicketController: Exposes REST endpoints for ticket operations
•	TicketService: Implements business logic for ticket management
•	TicketRepository: Handles data access for tickets
•	Ticket Model: Domain model representing a ticket in the system
---
⚙️ Configuration
Connection strings and environment settings can be modified in appsettings.json. For example, you might have:
{
"ConnectionStrings": {
"DefaultConnection": "Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=True;..."
},
"Logging": {
"LogLevel": {
"Default": "Information",
"Microsoft.AspNetCore": "Warning"
}
}
}
---
🧪 Running Tests
To run all tests, execute the following commands:
cd TicketProvider.Tests
dotnet test
---
📄 License
This project is licensed under the MIT License. See the LICENSE file for details.
---
🤝 Contributing
1.	Fork the repository
2.	Create your feature branch: git checkout -b feature/AmazingFeature
3.	Commit your changes: git commit -m 'Add AmazingFeature'
4.	Push to the branch: git push origin feature/AmazingFeature
5.	Open a Pull Request
---
📞 Contact
Project Link: https://github.com/your-username/ticket-provider
---
Built with .NET 8 and designed for modern microservice environments.
---

