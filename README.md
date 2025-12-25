# Library Management System (LibarayMS)

A comprehensive REST API for managing library operations including book management, member registration, borrowing records, fines, and user authentication. Built with .NET 10 and Entity Framework Core.

## 📋 Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Prerequisites](#prerequisites)
- [Installation & Setup](#installation--setup)
- [API Documentation](#api-documentation)
- [Database Schema](#database-schema)
- [Project Structure](#project-structure)
- [Usage Examples](#usage-examples)
- [Testing](#testing)
- [Contributing](#contributing)
- [License](#license)

## ✨ Features

### 🔐 Authentication & Authorization
- JWT-based authentication
- Role-based access control (Admin, Member, Guest)
- Secure password hashing with BCrypt

### 📚 Book Management
- CRUD operations for books
- Category-based organization
- Advanced search functionality
- ISBN validation
- Copy availability tracking

### 👥 Member Management
- Member registration and profile management
- Borrowing limits and history
- Role-based permissions
- Email and phone validation

### 📖 Borrowing System
- Book borrowing and return tracking
- Due date management
- Automatic fine calculation
- Borrowing history

### 💰 Fine Management
- Automatic fine generation for overdue books
- Fine payment tracking
- Fine amount calculation based on days overdue

### 🏷️ Category Management
- Book categorization system
- Hierarchical organization
- Category-based book filtering

## 🛠️ Tech Stack

### Backend
- **Framework**: .NET 10 (ASP.NET Core Web API)
- **Language**: C# 12
- **ORM**: Entity Framework Core 10
- **Database**: SQL Server
- **Authentication**: JWT Bearer Tokens
- **Validation**: Data Annotations
- **Documentation**: Scalar/OpenAPI

### Architecture
- **Pattern**: Clean Architecture
- **Layers**: Domain, Application, Infrastructure, Presentation
- **Dependency Injection**: Built-in .NET DI Container
- **Repository Pattern**: Generic repositories with specific implementations

### Development Tools
- **IDE**: Visual Studio 2026
- **Version Control**: Git
- **API Testing**: Postman / Scalar
- **Database Tools**: SQL Server Management Studio

## 📋 Prerequisites

Before running this application, make sure you have the following installed:

- **.NET 810 SDK** 
- **SQL Server** (2026) or **SQL Server Express**
- **Visual Studio 2026** (recommended) or **VS Code**
- **Git** (for version control)

## 🚀 Installation & Setup

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/library-management-system.git
cd library-management-system
```

### 2. Database Setup

1. **Create Database**:
   ```sql
   CREATE DATABASE LibraryMS;
   ```

2. **Update Connection String**:
   Edit `LibarayMS.Presentation/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=LibraryMS;Trusted_Connection=True;TrustServerCertificate=True;"
     },
     "JwtSettings": {
       "Issuer": "LibraryManagementSystem",
       "Audience": "LibraryUsers",
       "SigningKey": "your-super-secret-key-here-at-least-32-characters-long",
       "ExpiryMinutes": 60
     }
   }
   ```

### 3. Run Database Migrations

```bash
# Navigate to the Infrastructure project
cd LibraryMS.Infrastructure

# Apply migrations
dotnet ef database update --startup-project ../LibarayMS.Presentation
```

### 4. Build and Run

#### Option A: Local Development
```bash
# From the root directory
dotnet build LibarayMS.sln
dotnet run --project LibarayMS.Presentation
```

#### Option B: Docker (Recommended)
```bash
# Using Docker Compose
docker-compose up --build

# For development with hot reload
docker-compose -f docker-compose.yml -f docker-compose.override.yml up --build
```

The API will be available at:
- **Local**: `https://localhost:5001` or `http://localhost:5000`
- **Docker**: `http://localhost:8080` (production) or `http://localhost:5000` (development)

### 5. Docker Setup (Optional)

#### Production Setup
```bash
# Build and run with Docker Compose
docker-compose up --build -d

# View logs
docker-compose logs -f libraryms_api

# Stop services
docker-compose down
```

#### Development Setup
```bash
# Run with hot reload for development
docker-compose -f docker-compose.yml -f docker-compose.override.yml up --build

# Access the API at http://localhost:5000
```

#### Database Access
```bash
# Connect to SQL Server container
docker exec -it libraryms_sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P YourStrong!Passw0rd
```

### 6. Access API Documentation

- **Saclar UI**: `https://localhost:5001/scalar`
- **API Documentation**: `https://localhost:5001/scalar/v1/.json`

## 📚 API Documentation

### Authentication Endpoints

#### POST /api/account/register
Register a new library member.

**Request Body:**
```json
{
  "name": "John Doe",
  "email": "john.doe@example.com",
  "phone": "+1234567890",
  "password": "securepassword123",
  "maxBorrowLimit": 5
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiration": "2024-01-01T12:00:00Z",
  "member": {
    "id": 1,
    "name": "John Doe",
    "email": "john.doe@example.com",
    "phone": "+1234567890",
    "role": "Member",
    "createdAt": "2024-01-01T10:00:00Z",
    "maxBorrowLimit": 5
  }
}
```

#### POST /api/account/login
Authenticate a member.

**Request Body:**
```json
{
  "email": "john.doe@example.com",
  "password": "securepassword123"
}
```

### Book Management Endpoints

#### GET /api/books
Get all books (with optional search and pagination).

#### GET /api/books/{id}
Get a specific book by ID.

#### GET /api/books/search?query={searchTerm}
Search books by title, author, or ISBN.

#### GET /api/books/category/{categoryId}
Get all books in a specific category.

#### POST /api/books
Add a new book (Admin only).

#### PUT /api/books/{id}
Update book information (Admin only).

#### DELETE /api/books/{id}
Remove a book (Admin only).

### Member Management Endpoints

#### GET /api/members
Get all members (Admin only).

#### GET /api/members/{id}
Get member details.

#### PUT /api/members/{id}
Update member information.

#### DELETE /api/members/{id}
Delete a member (Admin only).

#### GET /api/members/{id}/active-borrows
Get active borrow count for a member.

### Borrowing System Endpoints

#### GET /api/borrowrecords
Get all borrow records (Admin only).

#### GET /api/borrowrecords/{id}
Get specific borrow record.

#### GET /api/borrowrecords/book/{bookId}
Get borrow records for a specific book.

#### GET /api/borrowrecords/member/{memberId}
Get borrow records for a specific member.

#### POST /api/borrowrecords
Borrow a book.

#### PUT /api/borrowrecords/{id}/return
Return a borrowed book.

### Fine Management Endpoints

#### GET /api/fines
Get all fines (Admin only).

#### GET /api/fines/{id}
Get specific fine details.

#### GET /api/fines/borrowrecord/{borrowRecordId}
Get fines for a specific borrow record.

#### POST /api/fines
Create a new fine (Admin only).

#### PUT /api/fines/{id}
Update fine information (Admin only).

#### PUT /api/fines/{id}/pay
Mark a fine as paid.

#### DELETE /api/fines/{id}
Delete a fine (Admin only).

### Category Management Endpoints

#### GET /api/categories
Get all categories.

#### GET /api/categories/{id}
Get specific category.

#### POST /api/categories
Create a new category (Admin only).

#### PUT /api/categories/{id}
Update category (Admin only).

#### DELETE /api/categories/{id}
Delete category (Admin only).

## 🗄️ Database Schema

### Core Entities

#### Members
- `Id` (Primary Key)
- `Name` (Required, 2-100 chars)
- `Email` (Required, Unique, Valid Email)
- `Phone` (Required, 10-20 chars)
- `PasswordHash` (Required)
- `Role` (Required, Default: "Member")
- `CreatedAt` (Required)
- `MaxBorrowLimit` (Required, 1-20)

#### Books
- `Id` (Primary Key)
- `Title` (Required, 1-200 chars)
- `Author` (Required, 1-100 chars)
- `ISBN` (Required, 10-20 chars, Valid ISBN)
- `PublishedDate` (Required)
- `CopiesAvailable` (Required, 0-1000)
- `CategoryId` (Foreign Key)

#### Categories
- `Id` (Primary Key)
- `Name` (Required, 2-50 chars, Letters only)

#### BorrowRecords
- `Id` (Primary Key)
- `BookId` (Foreign Key)
- `MemberId` (Foreign Key)
- `BorrowedAt` (Required)
- `DueDate` (Required)
- `ReturnedAt` (Optional)
- `FineAmount` (Required, 0+)

#### Fines
- `Id` (Primary Key)
- `BorrowRecordId` (Foreign Key)
- `Amount` (Required, 0.01+)
- `IsPaid` (Required, Default: false)
- `PaidAt` (Optional)

## 🏗️ Project Structure

```
LibarayMS/
├── LibararyMS.Domain/                    # Domain Layer
│   ├── Entities/                         # Domain Entities
│   │   ├── Book.cs
│   │   ├── BorrowRecord.cs
│   │   ├── Category.cs
│   │   ├── Fine.cs
│   │   └── Member.cs
│   └── Repoistries/                      # Repository Interfaces
│       ├── IMainRepoistery.cs
│       ├── IUnitOfWork.cs
│       ├── IAccountRepoistory.cs
│       ├── IBookRepoistory.cs
│       ├── IBorrowRepoistory.cs
│       ├── IMemberRepoistory.cs
│       └── ...
├── LibraryMS.Application/                # Application Layer
│   ├── DTOs/                             # Data Transfer Objects
│   │   ├── Auth/
│   │   ├── Books/
│   │   ├── BorrowRecords/
│   │   ├── Categories/
│   │   ├── Fines/
│   │   └── Members/
│   ├── Interfaces/                       # Service Interfaces
│   ├── Services/                         # Business Logic Services
│   ├── Helpers/                          # Utility Classes
│   ├── Mapping/                          # AutoMapper Profiles
│   └── ApplicationDependency.cs          # DI Configuration
├── LibraryMS.Infrastructure/             # Infrastructure Layer
│   ├── Data/
│   │   └── AppDbContext.cs               # EF Core Context
│   ├── Repoistries/                      # Repository Implementations
│   └── InfrastructureDependency.cs       # DI Configuration
└── LibarayMS.Presentation/                # Presentation Layer
    ├── Controllers/                      # API Controllers
    ├── appsettings.json                  # Configuration
    ├── Program.cs                        # Application Entry Point
    └── Properties/
```

## 📖 Usage Examples

### 1. Member Registration & Authentication

```bash
# Register a new member
curl -X POST https://localhost:5001/api/account/register \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Alice Johnson",
    "email": "alice@example.com",
    "phone": "+1987654321",
    "password": "password123",
    "maxBorrowLimit": 3
  }'

# Login
curl -X POST https://localhost:5001/api/account/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "alice@example.com",
    "password": "password123"
  }'
```

### 2. Book Management

```bash
# Add a new book (Admin only)
curl -X POST https://localhost:5001/api/books \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -d '{
    "title": "Clean Code",
    "author": "Robert C. Martin",
    "isbn": "978-0132350884",
    "publishedDate": "2008-08-01",
    "copiesAvailable": 5,
    "categoryId": 1
  }'

# Search books
curl "https://localhost:5001/api/books/search?query=clean"
```

### 3. Borrowing a Book

```bash
# Borrow a book
curl -X POST https://localhost:5001/api/borrowrecords \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -d '{
    "bookId": 1,
    "memberId": 1,
    "dueDate": "2024-02-01T00:00:00Z"
  }'

# Return a book
curl -X PUT https://localhost:5001/api/borrowrecords/1/return \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -d '{
    "returnedAt": "2024-01-25T14:30:00Z"
  }'
```

## 🧪 Testing

### Running Tests

```bash
# Run all tests
dotnet test

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"
```

### API Testing with Postman

1. Import the Swagger collection from `/swagger/v1/swagger.json`
2. Set up environment variables for:
   - `base_url`: `https://localhost:5001`
   - `jwt_token`: (obtained from login/register)

### Manual Testing Checklist

- [ ] User registration and login
- [ ] JWT token validation
- [ ] CRUD operations for all entities
- [ ] Authorization and role-based access
- [ ] Data validation and error handling
- [ ] Search and filtering functionality
- [ ] Fine calculation for overdue books

## 🤝 Contributing

We welcome contributions! Please see our [Contributing Guide](CONTRIBUTING.md) for detailed information on:

- Development setup and workflow
- Coding standards and best practices
- Testing guidelines
- Pull request process

**Quick Start:**
1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Make your changes following our coding standards
4. Add tests and update documentation
5. Submit a pull request

### Development Guidelines

- Follow Clean Architecture principles
- Write comprehensive unit tests
- Use meaningful commit messages
- Update documentation for API changes
- Ensure all tests pass before submitting PR

### Code Style

- Use C# naming conventions
- Add XML documentation comments
- Use Data Annotations for validation
- Follow SOLID principles
- Keep methods focused and small

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 📞 Support

For support, email support@libraryms.com or create an issue in this repository.

## 🔄 Version History

### v1.0.0 (Current)
- Initial release with full library management functionality
- JWT authentication and authorization
- Complete CRUD operations for all entities
- Automated fine calculation system
- Comprehensive API documentation

---

**Built with ❤️ using .NET 8 and Clean Architecture**
