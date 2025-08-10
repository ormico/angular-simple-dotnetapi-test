# Launch Settings Configuration for RecordManagement.Api

## Overview
Enhanced launch settings have been added to the .NET project to provide multiple development scenarios and improved debugging experience.

## Configuration Details

### File Location
`src/RecordManagement.Api/Properties/launchSettings.json`

### Available Launch Profiles

#### 1. **HTTP Profile** (Default Development)
- **Port**: `http://localhost:5094`
- **Browser**: Auto-launches to Swagger UI
- **Environment**: Development
- **SSL**: Disabled for simple debugging

#### 2. **HTTPS Profile** (Secure Development)
- **Ports**: `https://localhost:7066` (primary), `http://localhost:5094` (fallback)
- **Browser**: Auto-launches to Swagger UI
- **Environment**: Development
- **SSL**: Enabled with dev certificates

#### 3. **IIS Express Profile**
- **Port**: `http://localhost:12346`
- **SSL Port**: `44358`
- **Browser**: Auto-launches to Swagger UI
- **Environment**: Development
- **Hosting**: IIS Express integration

#### 4. **Docker Profile**
- **Port**: Container port `8080`
- **Browser**: Auto-launches to Swagger UI via container mapping
- **Environment**: Containerized development
- **SSL**: Configurable

## Usage Examples

### Command Line Launch
```bash
# Launch with HTTP profile (default)
dotnet run --project "src\RecordManagement.Api"

# Launch with specific profile
dotnet run --project "src\RecordManagement.Api" --launch-profile http
dotnet run --project "src\RecordManagement.Api" --launch-profile https
dotnet run --project "src\RecordManagement.Api" --launch-profile "IIS Express"
```

### Visual Studio Integration
- **F5 Debug**: Uses default profile (HTTP)
- **Profile Dropdown**: Select from HTTP, HTTPS, IIS Express, or Docker
- **Multiple Startup Projects**: Configure alongside Angular frontend

### VS Code Integration
- **Run and Debug**: Profiles available in launch.json
- **Tasks Integration**: Can be used with VS Code tasks
- **Browser Launch**: Automatically opens Swagger documentation

## Key Features

### 🚀 **Auto-Launch Swagger**
- All profiles configured to open Swagger UI automatically
- Direct access to API documentation and testing interface
- No manual navigation required

### 🔧 **Development Optimized**
- Clear console messages with `dotnetRunMessages: true`
- Development environment variables pre-configured
- Port conflicts avoided with unique port assignments

### 🌐 **Multiple Hosting Options**
- **Kestrel**: Direct .NET hosting (HTTP/HTTPS profiles)
- **IIS Express**: Windows IIS development server
- **Docker**: Containerized development and testing

### 🛡️ **Security Configurations**
- HTTPS profile with proper SSL setup
- Anonymous authentication for development
- Environment-specific security settings

## Port Configuration Summary

| Profile | HTTP Port | HTTPS Port | Purpose |
|---------|-----------|------------|---------|
| HTTP | 5094 | - | Simple development |
| HTTPS | 5094 | 7066 | Secure development |
| IIS Express | 12346 | 44358 | IIS integration |
| Docker | 8080 | - | Container development |

## Integration with Angular Frontend

The launch settings are configured to work alongside the Angular frontend:
- **Angular**: `http://localhost:4200`
- **.NET API**: `http://localhost:5094`
- **CORS**: Pre-configured for Angular origin

## Benefits

✅ **Quick Development Start**: One command launches API with Swagger  
✅ **Multiple Environments**: Test different hosting scenarios  
✅ **Browser Integration**: Automatic documentation access  
✅ **Port Management**: Avoid conflicts with other services  
✅ **SSL Support**: Test secure connections easily  
✅ **Docker Ready**: Container development support  

## Next Steps

1. **Test all profiles** to ensure they work in your environment
2. **Configure VS Code** launch.json if using VS Code debugging
3. **Update Angular service** URLs if changing ports
4. **Add environment-specific settings** as needed

The launch settings provide a robust foundation for .NET API development across different scenarios and development tools.
