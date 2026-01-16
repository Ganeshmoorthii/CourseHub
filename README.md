# CourseHub Backend

CourseHub Backend is a cleanly architected **ASP.NET Core Web API** that powers the backend of an online course platform.  
The system is designed with **scalability, maintainability, and real-world backend practices** in mind.

It follows **Clean Architecture**, enforces separation of concerns, and supports **event-driven communication** and **advanced search capabilities**.

---

## Tech Stack

- **Language:** C#
- **Framework:** ASP.NET Core (.NET)
- **Architecture:** Clean Architecture
- **Database:** Relational DB (via EF Core)
- **ORM:** Entity Framework Core
- **Messaging:** Event Bus (asynchronous communication)
- **API Style:** REST
- **Documentation:** Swagger / OpenAPI

---

## Core Features

- Course management APIs  
- Course search with **multiple filters**  
- Event Bus integration for async messaging  
- Layered architecture with strict boundaries  
- Extensible and test-friendly design  
- Centralized exception handling  
- Consistent API response structure  

---

## Project Structure

```
CourseHub.Backend
│
├── CourseHub.API
│   ├── Controllers
│   ├── Middlewares
│   └── Program.cs
│
├── CourseHub.Application
│   ├── Interfaces
│   ├── UseCases / Services
│   └── DTOs
│
├── CourseHub.Domain
│   ├── Entities
│   ├── ValueObjects
│   └── Domain Contracts
│
├── CourseHub.Infrastructure
│   ├── Data
│   ├── Repositories
│   ├── EventBus
│   └── Persistence
│
└── CourseHub.Shared
    ├── Exceptions
    ├── Responses
    └── Utilities
```

---

## Architecture Principles

- Single Responsibility  
- Dependency Inversion  
- Explicit boundaries  
- No business logic in controllers  
- No EF Core leaking into API layer  

This is not a CRUD dump — it is intentionally structured backend code.

---

## Course Search API

Supports searching courses using multiple filters such as:
- Keyword  
- Category  
- Level  
- Price range  
- Status  

Example:
```
GET /api/courses/search?keyword=dotnet&level=advanced
```

Designed to be:
- Composable  
- Performant  
- Easy to extend  

---

## Event Bus Integration

The backend uses an **Event Bus** to publish and consume domain events for async workflows.

Typical use cases:
- Course created events  
- User enrollment events  
- Cross-service communication  

---

## Getting Started

### Prerequisites

- .NET SDK  
- SQL Server / configured relational DB  
- Visual Studio or VS Code  

### Run Locally

```bash
git clone https://github.com/Ganeshmoorthii/CourseHub-Backend.git
cd CourseHub-Backend
dotnet run --project CourseHub.API
```

Open:
```
http://localhost:<port>/swagger
```

---

## API Standards

- Proper HTTP status codes  
- Unified response wrapper  
- Global exception handling  
- Clear validation messages  

---

## Development Workflow

- Feature-based branching  
- Small, meaningful commits  
- No direct commits to main  

---

## Roadmap

- JWT Authentication  
- Pagination & sorting  
- Caching  
- CI/CD pipeline  
- Unit & integration tests  

---

## Maintainer

**Ganesh Moorthi**  
Backend Developer  
Repository: https://github.com/Ganeshmoorthii/CourseHub-Backend
