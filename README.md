# 🛒 Etic E-Commerce Platform

Modern, professional e-commerce platform built with ASP.NET Core 6.0 MVC.

## 🚀 Technologies

- **Backend:** ASP.NET Core 6.0 MVC
- **ORM:** Entity Framework Core 6.0
- **Database:** SQL Server
- **Architecture:** Repository Pattern, Clean Architecture
- **Security:** SHA256 Password Hashing

## 📦 Project Structure

```
Etic/
├── Etic.Web/          # MVC Web Application
├── Etic.Business/     # Business Logic Layer
├── Etic.Data/         # Data Access Layer
├── Etic.Entities/     # Domain Models
└── Etic.Core/         # Core Utilities & Base Classes
```

## 🎯 Features

- ✅ User Authentication & Authorization
- ✅ Product Management with Categories
- ✅ Shopping Cart
- ✅ Order Management
- ✅ Multi-Address Support
- ✅ Soft Delete Pattern
- ✅ Audit Trail (CreatedBy, UpdatedBy, DeletedBy)
- ✅ Professional Database Design

## 🛠️ Setup

### Prerequisites
- .NET 6.0 SDK
- SQL Server (LocalDB or Express)

### Installation

1. **Clone the repository**
   ```bash
   git clone [your-repo-url]
   cd Etic
   ```

2. **Update Database Connection**
   
   Update connection string in `Etic.Data/EticContext.cs`:
   ```csharp
   optionsBuilder.UseSqlServer("Your-Connection-String");
   ```

3. **Apply Migrations**
   ```bash
   cd Etic.Data
   dotnet ef database update --startup-project ..\Etic.Web\
   ```

4. **Add Seed Data**
   ```bash
   sqlcmd -S your-server -d EticDB -E -i "..\Database\SeedData.sql"
   ```

5. **Run the Application**
   ```bash
   cd ..\Etic.Web
   dotnet run
   ```

   Navigate to: `https://localhost:7005`

## 🔑 Default Admin Account

- **Email:** admin@etic.com
- **Password:** 123456

## 📊 Database Features

- **BaseEntity Pattern** - Common audit fields across all entities
- **Soft Delete** - Records marked as deleted, not physically removed
- **Enums** - Type-safe status management (UserStatus, OrderStatus, PaymentType)
- **Indexes** - Performance optimized queries
- **Relationships** - Properly configured FK relationships with CASCADE/RESTRICT
- **GUID Security** - Basket IDs use GUID for security

## 📝 License

This project is for educational purposes.

## 👨‍💻 Developer

Built with ❤️ using professional .NET practices

