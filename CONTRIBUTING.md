# Contributing to Library Management System

Thank you for your interest in contributing to the Library Management System! We welcome contributions from the community and are grateful for your help in making this project better.

## 📋 Table of Contents

- [Code of Conduct](#code-of-conduct)
- [Getting Started](#getting-started)
- [Development Workflow](#development-workflow)
- [Coding Standards](#coding-standards)
- [Testing Guidelines](#testing-guidelines)
- [Pull Request Process](#pull-request-process)
- [Reporting Issues](#reporting-issues)

## 🤝 Code of Conduct

This project follows a code of conduct to ensure a welcoming environment for all contributors. By participating, you agree to:

- Be respectful and inclusive
- Focus on constructive feedback
- Accept responsibility for mistakes
- Show empathy towards other contributors
- Help create a positive community

## 🚀 Getting Started

### Prerequisites

Before you begin, ensure you have:

- .NET 8 SDK or later
- SQL Server (2022 or later) or SQL Server Express
- Visual Studio 2022 or VS Code
- Git
- Docker (optional, for containerized development)

### Environment Setup

1. **Fork and Clone**:
   ```bash
   git clone https://github.com/your-username/library-management-system.git
   cd library-management-system
   ```

2. **Create Feature Branch**:
   ```bash
   git checkout -b feature/your-feature-name
   ```

3. **Database Setup**:
   ```sql
   CREATE DATABASE LibraryMS_Dev;
   ```

4. **Configuration**:
   Update `appsettings.Development.json` with your database connection string.

5. **Install Dependencies**:
   ```bash
   dotnet restore
   ```

6. **Run Migrations**:
   ```bash
   dotnet ef database update --startup-project LibarayMS.Presentation
   ```

7. **Build and Run**:
   ```bash
   dotnet build
   dotnet run --project LibarayMS.Presentation
   ```

## 🔄 Development Workflow

### 1. Choose an Issue
- Check the [Issues](../../issues) page for open tasks
- Comment on the issue to indicate you're working on it
- Create a new issue if you have a feature request or bug fix

### 2. Create Feature Branch
```bash
git checkout -b feature/issue-number-description
# or
git checkout -b bugfix/issue-number-description
# or
git checkout -b enhancement/issue-number-description
```

### 3. Implement Changes
- Follow the coding standards below
- Write comprehensive tests
- Update documentation as needed
- Ensure all tests pass

### 4. Commit Changes
```bash
git add .
git commit -m "feat: add new feature description

- What was changed
- Why it was changed
- How it was implemented

Closes #issue-number"
```

### 5. Push and Create PR
```bash
git push origin feature/your-feature-name
```
Then create a Pull Request on GitHub.

## 📝 Coding Standards

### C# Guidelines

- **Naming Conventions**:
  - Use PascalCase for classes, methods, and properties
  - Use camelCase for local variables and parameters
  - Use UPPER_CASE for constants
  - Prefix private fields with underscore: `_fieldName`

- **Code Structure**:
  ```csharp
  // Use regions for organizing code
  #region Fields
  private readonly IService _service;
  #endregion

  #region Constructor
  public MyClass(IService service)
  {
      _service = service;
  }
  #endregion

  #region Public Methods
  public void DoSomething()
  {
      // Implementation
  }
  #endregion
  ```

- **XML Documentation**:
  ```csharp
  /// <summary>
  /// Brief description of what the method/class does
  /// </summary>
  /// <param name="parameterName">Description of parameter</param>
  /// <returns>Description of return value</returns>
  public void MyMethod(string parameterName)
  {
      // Implementation
  }
  ```

- **Data Annotations**:
  ```csharp
  [Required(ErrorMessage = "Field is required")]
  [StringLength(100, MinimumLength = 2)]
  [Display(Name = "Display Name")]
  public string Name { get; set; }
  ```

### Clean Architecture Principles

- **Dependency Inversion**: High-level modules should not depend on low-level modules
- **Single Responsibility**: Each class should have one reason to change
- **Open/Closed**: Open for extension, closed for modification
- **Liskov Substitution**: Subtypes should be substitutable for their base types
- **Interface Segregation**: Clients should not be forced to depend on interfaces they don't use

### Project Structure

```
LibarayMS/
├── Domain/           # Core business entities and interfaces
├── Application/      # Application services and DTOs
├── Infrastructure/   # External dependencies (EF, repositories)
└── Presentation/     # API controllers and middleware
```

## 🧪 Testing Guidelines

### Unit Tests
- Write tests for all business logic
- Use xUnit for testing framework
- Follow AAA pattern (Arrange, Act, Assert)
- Mock external dependencies

```csharp
[Fact]
public async Task GetBookById_ExistingBook_ReturnsBook()
{
    // Arrange
    var bookId = 1;
    var expectedBook = new Book { Id = bookId, Title = "Test Book" };
    _mockRepository.Setup(r => r.GetByIdAsync(bookId))
                   .ReturnsAsync(expectedBook);

    // Act
    var result = await _bookService.GetByIdAsync(bookId);

    // Assert
    Assert.NotNull(result);
    Assert.Equal(expectedBook.Id, result.Id);
    Assert.Equal(expectedBook.Title, result.Title);
}
```

### Integration Tests
- Test complete workflows
- Use test database
- Cover API endpoints
- Test database operations

### Test Coverage
- Aim for at least 80% code coverage
- Cover happy path and error scenarios
- Test edge cases and boundary conditions

## 🔄 Pull Request Process

### Before Submitting
- [ ] All tests pass
- [ ] Code follows coding standards
- [ ] Documentation is updated
- [ ] Commit messages are clear and descriptive
- [ ] No sensitive information is included

### PR Template
```markdown
## Description
Brief description of the changes made.

## Type of Change
- [ ] Bug fix
- [ ] New feature
- [ ] Breaking change
- [ ] Documentation update

## Testing
- [ ] Unit tests added/updated
- [ ] Integration tests added/updated
- [ ] Manual testing completed

## Checklist
- [ ] Code follows project conventions
- [ ] Documentation updated
- [ ] Tests pass locally
- [ ] No breaking changes

## Related Issues
Closes #issue-number
```

### Review Process
1. **Automated Checks**: CI/CD pipeline runs tests and linting
2. **Code Review**: At least one maintainer reviews the code
3. **Approval**: PR is approved and merged
4. **Deployment**: Changes are deployed to staging/production

## 🐛 Reporting Issues

### Bug Reports
When reporting bugs, please include:

- **Title**: Clear, descriptive title
- **Description**: Detailed description of the issue
- **Steps to Reproduce**:
  1. Step 1
  2. Step 2
  3. Expected result vs actual result
- **Environment**: OS, .NET version, browser, etc.
- **Screenshots**: If applicable
- **Logs**: Error messages, stack traces

### Feature Requests
For new features, please include:

- **Title**: Clear, descriptive title
- **Description**: Detailed description of the proposed feature
- **Use Case**: Why is this feature needed?
- **Proposed Solution**: How should it work?
- **Alternatives**: Other solutions considered

## 📚 Additional Resources

- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [.NET Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [ASP.NET Core Web API](https://docs.microsoft.com/en-us/aspnet/core/web-api/)

## 📞 Getting Help

If you need help or have questions:

1. Check the [README.md](../README.md) file
2. Search existing [Issues](../../issues) and [Discussions](../../discussions)
3. Create a new [Discussion](../../discussions) for questions
4. Join our community chat (if available)

Thank you for contributing to the Library Management System! 🎉
