# Record Management API

A modern .NET 8 Web API for managing records with full CRUD operations, built with Minimal APIs, OpenAPI documentation, and Microsoft Identity authentication.

## Features

- 🚀 **Minimal APIs**: Lightweight, high-performance endpoints
- 📖 **OpenAPI/Swagger**: Interactive API documentation  
- 🔐 **Microsoft Identity**: Enterprise-grade authentication (with dev bypass)
- 💾 **In-Memory Storage**: Quick development setup (easily replaceable with database)
- 🌐 **CORS Support**: Configured for frontend integration
- ❤️ **Health Checks**: Built-in monitoring endpoints
- 🔄 **RESTful Design**: Standard HTTP methods and status codes

## Data Model

Records contain the following fields based on the Excel data structure:
- **Date**: Record date
- **Name**: Record identifier  
- **Alpha, Beta, Gamma, Delta**: Numeric values for tracking
- **Milestone 1 & 2**: Start and completion dates for project milestones
- **Client Name**: Associated client information
- **Agent Name**: Responsible agent information

## Tech Stack

- **Framework**: .NET 8 with Minimal APIs
- **Language**: C# with top-level statements
- **Authentication**: Microsoft Identity Web
- **Documentation**: Swagger/OpenAPI
- **Storage**: In-memory (development) - ready for database integration
- **Validation**: Data Annotations
- **CORS**: ASP.NET Core CORS middleware

## Project Structure

```
src/
├── Models/
│   └── Record.cs                 # Data model with validation
├── DTOs/
│   └── RecordDTOs.cs            # Data transfer objects
├── Services/
│   └── RecordService.cs         # Business logic and in-memory storage
├── Endpoints/
│   └── RecordEndpoints.cs       # API endpoint definitions
Program.cs                       # Application configuration and startup
RecordManagement.Api.csproj      # Project file with dependencies
```

## API Endpoints

### Records
- `GET /api/records` - Get all records
- `GET /api/records/{id}` - Get record by ID
- `POST /api/records` - Create new record
- `PUT /api/records/{id}` - Update existing record  
- `DELETE /api/records/{id}` - Delete record

### System
- `GET /health` - Health check endpoint
- `GET /` - Swagger UI (development only)
- `GET /dev/login` - Development authentication bypass

## Getting Started

### Prerequisites

- .NET 8 SDK
- Visual Studio 2022 or VS Code
- Optional: Docker for containerization

### Installation

1. **Clone and navigate to the project**:
   ```bash
   cd d:\angular-simple-dotnetapi-test
   ```

2. **Restore dependencies**:
   ```bash
   dotnet restore
   ```

3. **Build the project**:
   ```bash
   dotnet build
   ```

4. **Run the application**:
   ```bash
   dotnet run
   ```

5. **Open Swagger UI**: Navigate to `http://localhost:5129` (or the port shown in console)

### Configuration

The application uses standard .NET configuration:

- **appsettings.json**: Production settings
- **appsettings.Development.json**: Development overrides
- **Environment variables**: Override any setting

Key configuration sections:
```json
{
  "AzureAd": {
    "Instance": "https://login.microsoftonline.com/",
    "TenantId": "your-tenant-id",
    "ClientId": "your-client-id"
  },
  "Cors": {
    "AllowedOrigins": ["http://localhost:4200"]
  }
}
```

## Sample Data

The application starts with sample records for testing:

```csharp
// Project Alpha - Complete milestones
{
  "name": "Project Alpha",
  "alpha": 125.50,
  "beta": 87.25,
  "gamma": 203.75,
  "delta": 156.00,
  "clientName": "Acme Corporation",
  "agentName": "John Smith"
}
```

## Authentication

### Development Mode
- Authentication is bypassed for easy testing
- Access `/dev/login` for development information
- All endpoints accessible without authentication

### Production Mode  
- Microsoft Identity authentication required
- JWT bearer tokens validated
- Configure Azure AD in `appsettings.json`

## Docker Support

### Build Image
```bash
docker build -t record-management-api .
```

### Run Container
```bash
docker run -p 8080:8080 record-management-api
```

### Environment Variables
```bash
docker run -p 8080:8080 \
  -e ASPNETCORE_ENVIRONMENT=Production \
  -e CORS_ORIGINS=http://localhost:4200 \
  record-management-api
```

## Kubernetes Deployment

```bash
kubectl apply -f k8s/api-deployment.yaml
```

The deployment includes:
- **Deployment**: 2 replicas with health checks
- **Service**: ClusterIP for internal communication
- **Environment**: Production configuration
- **Resources**: CPU and memory limits

## Database Integration

To replace in-memory storage with a database:

1. **Add Entity Framework packages**:
   ```bash
   dotnet add package Microsoft.EntityFrameworkCore.SqlServer
   dotnet add package Microsoft.EntityFrameworkCore.Tools
   ```

2. **Create DbContext**:
   ```csharp
   public class RecordDbContext : DbContext
   {
       public DbSet<Record> Records { get; set; }
       // Configure in Program.cs
   }
   ```

3. **Replace service registration**:
   ```csharp
   builder.Services.AddDbContext<RecordDbContext>(options =>
       options.UseSqlServer(connectionString));
   builder.Services.AddScoped<IRecordService, DatabaseRecordService>();
   ```

## Development Notes

- **Minimal APIs**: Uses the new .NET 6+ minimal API approach
- **Top-level statements**: Clean, concise Program.cs
- **Validation**: Automatic model validation with Data Annotations
- **OpenAPI**: Rich documentation with examples
- **CORS**: Configured for localhost development
- **Health checks**: Ready for monitoring and container orchestration

## Related Projects

- **Frontend**: `d:\angular-simple-test` - Angular application for record management

## Testing the API

### Using Swagger UI
1. Navigate to `http://localhost:5129`
2. Explore available endpoints
3. Test requests directly in the browser

### Using curl
```bash
# Get all records
curl -X GET http://localhost:5129/api/records

# Create a new record
curl -X POST http://localhost:5129/api/records \
  -H "Content-Type: application/json" \
  -d '{
    "date": "2024-01-01T00:00:00",
    "name": "Test Record",
    "alpha": 100.0,
    "beta": 200.0,
    "gamma": 300.0,
    "delta": 400.0,
    "clientName": "Test Client",
    "agentName": "Test Agent"
  }'
```

## Contributing

1. Follow .NET coding conventions
2. Write unit tests for new features
3. Update OpenAPI documentation
4. Follow RESTful API design principles

## License

This project is licensed under the MIT License.
