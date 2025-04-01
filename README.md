# Patika - LMS Core Project
This project is a multi-layered Learning Management System (LMS) built with ASP.NET Core Web API. The project provides a backend infrastructure to manage educational content, enroll users in courses, and implement user authentication and authorization processes.

## 🚀 Project Purpose
The main purpose of this project is to apply the knowledge and skills acquired during the .NET Bootcamp by developing a real-world LMS backend system. It focuses on implementing core functionalities following clean and scalable architecture while aiming to:
- Practice multi-layered software architecture
- Apply Code First approach using Entity Framework Core
- Implement JWT-based authentication mechanisms
- Utilize ASP.NET Core features such as Dependency Injection, Action Filters, and Middleware
- Integrate essential enterprise-level practices like data encryption, global exception handling, and validation

## 🎯 Features
🔐 **JWT-based Authentication and Authorization:** Secure access to API endpoints using JSON Web Tokens.

🧩 **Modular Architecture:** Clean separation between layers: Data, Business, and Web API.

🎓 **Course & Module Management:** Ability to manage courses, modules, and enrollments.

👥 **User Management System:** Register, login, and role-based access control for users.

🔄 **Entity Framework Core with Code First Approach:** Seamless database generation and migrations.

🧪 **Global Exception Handling:** Centralized error handling using middleware.

🛡️ **Data Protection:** Sensitive data encryption and security mechanisms.

📌 **Validation & Action Filters:** Server-side model validation and request filtering.

🧰 **Dependency Injection:** Decoupled services using built-in .NET Core DI container.

📃 **Swagger Integration:** API documentation and testing via Swagger UI.

## 🔗 API Endpoints
### Auth
| Method | Endpoint | Description |
|--------|---------|-------------|
| **POST** | `/api/auth/register` | Register a new user |
| **POST** | `/api/auth/login` | User login and JWT token generation |
| **GET** | `/api/users/me` | Get current logged-in user info |

### Courses
| Method | Endpoint | Description |
|--------|---------|-------------|
| **POST** | `/api/course` | Create a new course |
| **GET** | `/api/courses` | Get all courses |
| **GET** | `/api/courses/{id}` | Get an existing course details |
| **PATCH** | `/api/courses/{id}/stars` | Update a star rating for an existing course |
| **PUT** | `/api/courses/{id}` | Update an existing course |
| **DELETE** | `/api/courses/{id}` | Delete an existing course |

### Features
| Method | Endpoint | Description |
|--------|---------|-------------|
| **POST** | `/api/features` | Create a new feature |

### Settings
| Method | Endpoint | Description |
|--------|---------|-------------|
| **PATCH** | `/api/settings` | Enable or disable maintenance mode |

## 🗄️ Entities
### BaseEntity
```c#
public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsDeleted { get; set; }
}

public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasQueryFilter(x => x.IsDeleted == false);
        // A filtering has been made that will be valid for all queries and other LINQ operations to be made on this database. Thus, we will never have to deal with soft deleted data again.
        builder.Property(x => x.ModifiedDate).IsRequired(false);
    }
}
```

### CourseEntity
```c#
public class CourseEntity : BaseEntity
{
    public string Name { get; set; }
    public int? Stars { get; set; }
    public EducationType EducationType { get; set; }
    // Relational Properties
    public ICollection<CourseFeatureEntity> CourseFeatures { get; set; }
    public ICollection<ModuleEntity> Modules { get; set; }
}

public class CourseConfiguration : BaseConfiguration<CourseEntity>
{
    public override void Configure(EntityTypeBuilder<CourseEntity> builder)
    {
        builder.Property(x => x.Stars).IsRequired(false);
        builder.Property(x => x.Name).IsRequired()
                                     .HasMaxLength(80);

        base.Configure(builder);
    }
}
```
### FeatureEntity
```c#
public class FeatureEntity : BaseEntity
{
    public string Title { get; set; }
    // Relational Properties
    public ICollection<CourseFeatureEntity> CourseFeatures { get; set; }
}

public class FeatureConfiguration : BaseConfiguration<FeatureEntity>
{
    public override void Configure(EntityTypeBuilder<FeatureEntity> builder)
    {
        base.Configure(builder);
    }
}
```

### CourseFeatureEntity
```c#
public class CourseFeatureEntity : BaseEntity
{
    public int CourseId { get; set; }
    public int FeatureId { get; set; }
    // Relational Properties
    public CourseEntity Course { get; set; }
    public FeatureEntity Feature { get; set; }
}

public class CourseFeatureConfiguration : BaseConfiguration<CourseFeatureEntity>
{
    public override void Configure(EntityTypeBuilder<CourseFeatureEntity> builder)
    {
        builder.Ignore(x => x.Id); // Id property'sini görmezden geldik, tabloya aktarılmayacak.
        builder.HasKey("CourseId", "FeatureId"); // Composite Key oluşturup yeni Primary Key olarak atadık.

        base.Configure(builder);
    }
}
```

### UserEntity
```c#
public class UserEntity : BaseEntity
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public UserType UserType { get; set; }
    // Relational Properties
    public ICollection<EnrollmentEntity> Enrollments { get; set; }
}

public class UserConfiguration : BaseConfiguration<UserEntity>
{
    public override void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.Property(x => x.FirstName).IsRequired()
                                          .HasMaxLength(40);
        builder.Property(x => x.LastName).IsRequired()
                                          .HasMaxLength(40);

        base.Configure(builder);
    }
}
```

### EnrollmentEntity
```c#
public class EnrollmentEntity : BaseEntity
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int StudentCount { get; set; }
    public int ModuleId { get; set; }
    public int UserId { get; set; }
    // Relational Properties
    public UserEntity User { get; set; }
    public ModuleEntity Module { get; set; }
}

public class EnrollmentConfiguration : BaseConfiguration<EnrollmentEntity>
{
    public override void Configure(EntityTypeBuilder<EnrollmentEntity> builder)
    {
        base.Configure(builder);
    }
}
```

### ModuleEntity
```c#
public class ModuleEntity : BaseEntity
{
    public string ModuleName { get; set; }
    public int CourseId { get; set; }
    // Relational Properties
    public ICollection<EnrollmentEntity> Enrollments { get; set; }
    public CourseEntity Course { get; set; }
}

public class ModuleConfiguration : BaseConfiguration<ModuleEntity>
{
    public override void Configure(EntityTypeBuilder<ModuleEntity> builder)
    {
        base.Configure(builder);
    }
}
```

### SettingEntity
```c#
public class SettingEntity : BaseEntity
{
    public bool MaintenenceMode { get; set; }
}
```


## 📊 DbContext Configuration
```c#
public class PatikaLMSCoreProjectDbContext : DbContext
{
    public PatikaLMSCoreProjectDbContext(DbContextOptions<PatikaLMSCoreProjectDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply configurations
        modelBuilder.ApplyConfiguration(new FeatureConfiguration());
        modelBuilder.ApplyConfiguration(new CourseConfiguration());
        modelBuilder.ApplyConfiguration(new CourseFeatureConfiguration());
        modelBuilder.ApplyConfiguration(new EnrollmentConfiguration());
        modelBuilder.ApplyConfiguration(new ModuleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        modelBuilder.Entity<SettingEntity>().HasData(
            new SettingEntity
            {
                Id = 1,
                MaintenenceMode = false
            }
        );

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<FeatureEntity> Features => Set<FeatureEntity>();
    public DbSet<CourseEntity> Courses => Set<CourseEntity>();
    public DbSet<CourseFeatureEntity> CourseFeatures => Set<CourseFeatureEntity>();
    public DbSet<EnrollmentEntity> Enrollments => Set<EnrollmentEntity>();
    public DbSet<ModuleEntity> Modules => Set<ModuleEntity>();
    public DbSet<SettingEntity> Settings => Set<SettingEntity>();
}
```

## 🛠️ Technologies Used
- ASP.NET Core Web API – Backend API development
- Entity Framework Core – ORM and Code First Migrations
- SQL Server – Relational database
- JWT – Token-based authentication
- ASP.NET Core Custom Identity – User and role management
- Swagger / Swashbuckle – API documentation and testing
- Fluent Validation – Model validation
- Middleware – Custom global exception handling and request filtering
- Action Filters – Pre- and post-processing of controller actions
- Dependency Injection – Built-in DI container for service management
- Data Protection – Encryption and secure data handling

## 🏗️ Installation and Execution

Follow these steps to run the application:

1. **Clone the project:**
   ```bash
   git clone https://github.com/ulaskarakas/PatikaLMSCoreProject.git
   ```
2. **Install .NET SDK:**
   - If not already installed, download the appropriate version from the [.NET SDK](https://dotnet.microsoft.com/download) page.

3. **Navigate to the project directory and run the application:**
   ```bash
   cd PatikaLMSCoreProject
   ```
4. **Install Dependencies:**
   ```bash
   dotnet restore
   ```
5. **Update appsettings.json with your database connection string**
   ```bash
     {
      "ConnectionStrings": {
        "Default": "Server=.\\SQLEXPRESS; Database=PatikaLMSCoreProjectDb; Trusted_Connection=true; TrustServerCertificate=true;"
      }
    }
   ```
5. **Update the Database:**
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
6. **Run the Application:**
   ```bash
   dotnet run
   ```

## 🤝 Contributing
To contribute to this project, please submit a **Pull Request** or create an **Issue**.

## ⚖️ License
This project is licensed under the MIT License.

## 📬 Contact
For any questions or suggestions, feel free to contact me:

[![Gmail](https://ziadoua.github.io/m3-Markdown-Badges/badges/Gmail/gmail1.svg)](mailto:ulaskarakas95@gmail.com)
[![LinkedIn](https://ziadoua.github.io/m3-Markdown-Badges/badges/LinkedIn/linkedin1.svg)](https://www.linkedin.com/in/ulas-karakas/)
