# Simplified Branching & GitHub Actions Setup Guide - .NET API

This guide will help you set up a simplified branching strategy with automated CI/CD pipelines for your .NET 8 API.

## 🌳 Simplified Branching Strategy

This approach uses a single main branch with feature and release branches, avoiding the complexity of git-flow:

- **main**: Production-ready code (replaces both main and develop)
- **feature/***: New features (branch from main)
- **release/***: Release preparation (branch from main)
- **hotfix/***: Emergency fixes (branch from release branches)

## 🚀 Quick Setup

### 1. No Git-Flow Installation Needed

Unlike traditional git-flow, you'll manage branches manually using standard git commands:

```bash
# Create feature branch
git checkout main
git pull origin main
git checkout -b feature/add-user-endpoint

# Create release branch
git checkout main
git pull origin main
git checkout -b release/1.2.0

# Create hotfix branch (from release)
git checkout release/1.2.0
git checkout -b hotfix/fix-auth-bug
```

### 2. Repository Secrets Setup

Go to your GitHub repository → Settings → Secrets and variables → Actions, and add:

```
DOCKER_USERNAME=your_docker_username
DOCKER_PASSWORD=your_docker_password
AZURE_CLIENT_ID=your_azure_client_id (if using Azure)
AZURE_CLIENT_SECRET=your_azure_client_secret
AZURE_TENANT_ID=your_azure_tenant_id
STAGING_DEPLOY_URL=https://api-staging.yourapp.com
PRODUCTION_DEPLOY_URL=https://api.yourapp.com
DATABASE_CONNECTION_STRING=your_connection_string (if using external database)
```

### 3. Branch Protection Rules (Critical Setup)

Set up branch protection in GitHub → Settings → Branches:

**For `main` branch:**
- ✅ Require pull request reviews before merging (minimum 1 reviewer)
- ✅ Require status checks to pass before merging
- ✅ Required status checks:
  - `🟣 .NET API Validation`
  - `🔍 Code Quality & Security`
  - `🧪 Integration Tests`
  - `🐳 Docker Build & Security Scan`
  - `⚡ Performance Testing`
  - `📚 API Documentation`
- ✅ Require branches to be up to date before merging
- ✅ Require conversation resolution before merging
- ✅ Include administrators
- ✅ Restrict pushes that create files larger than 100MB

**For `release/*` branches (pattern):**
- ✅ Require status checks to pass before merging
- ✅ Require branches to be up to date before merging
- ✅ Same required status checks as main

**Rulesets for Branch Naming (Recommended):**
Go to Settings → Rules → Rulesets, create a new ruleset:
- **Target**: All branches
- **Rules**: 
  - Branch naming patterns: Only allow branches matching these patterns:
    - `main`
    - `feature/*`
    - `release/*`
    - `hotfix/*`

## 🔄 Simplified Workflow

### Daily Development

1. **Start your day:**
   ```bash
   git checkout main
   git pull origin main
   ```

2. **Create feature:**
   ```bash
   git checkout -b feature/add-user-authentication
   # Work on feature
   git add . && git commit -m "feat: add user authentication endpoint"
   git push origin feature/add-user-authentication
   ```

3. **Create pull request:**
   - Go to GitHub and create PR: `feature/add-user-authentication` → `main`
   - GitHub Actions will automatically run validation
   - Request review from team members

4. **Merge feature:**
   - After approval and successful checks, merge to main
   - Delete feature branch after merge

### Release Process

1. **Start release:**
   ```bash
   git checkout main
   git pull origin main
   git checkout -b release/1.2.0
   ```

2. **Prepare release:**
   ```bash
   # Update version in project file
   # Edit src/RecordManagement.Api/RecordManagement.Api.csproj
   # Update <Version>1.2.0</Version>
   
   # Update changelog, documentation, etc.
   git add .
   git commit -m "chore: prepare release 1.2.0"
   git push origin release/1.2.0
   ```

3. **Create release PR:**
   - Create pull request: `release/1.2.0` → `main`
   - GitHub Actions will run full validation
   - Tag and deployment happen automatically after merge

4. **Post-release:**
   - Keep release branch for potential hotfixes
   - Create new features from updated main

### Emergency Hotfix

1. **Start hotfix from release branch:**
   ```bash
   git checkout release/1.2.0  # or whatever release needs fixing
   git pull origin release/1.2.0
   git checkout -b hotfix/fix-security-vulnerability
   ```

2. **Fix issue:**
   ```bash
   # Make minimal fix
   git add . && git commit -m "fix: resolve security vulnerability in auth"
   git push origin hotfix/fix-security-vulnerability
   ```

3. **Create hotfix PR to release:**
   - Create PR: `hotfix/fix-security-vulnerability` → `release/1.2.0`
   - After merge, create another PR: `release/1.2.0` → `main`
   - Both PRs trigger appropriate validations and deployments

## 🤖 GitHub Actions Workflows

### 1. Pull Request Workflow (`pull-request.yml`)

**Triggers:** 
- Pull requests to `main` or `release/*` branches

**Features:**
- 🟣 .NET 8 build and testing
- 🧪 Integration tests with SQL Server
- 🔍 Code quality analysis
- 🛡️ Security scanning with vulnerability detection
- 📦 Build validation
- 🐳 Docker image building and security scan
- ⚡ Performance testing
- 📚 OpenAPI documentation generation

### 2. Release Workflow (`release.yml`)

**Triggers:** 
- Push to `main` branch (from merged release PRs)
- Git tags (v*)

**Features:**
- 🏗️ Multi-stage builds and deployments
- 🐳 Docker image publishing to GitHub Container Registry
- 🚀 Automated deployments to staging and production
- 📋 GitHub release creation with automated notes
- 🏷️ Semantic versioning
- 📧 Deployment notifications
- 🧪 Smoke tests and health checks

### 3. Branch Management Workflow (`branch-management.yml`)

**Triggers:** 
- Push to `main`, `release/*`, `hotfix/*`
- Pull requests to `main`, `release/*`

**Features:**
- 🌳 Branch naming validation
- 🔄 Main branch integration
- 🚀 Release preparation with version management
- 🚨 Hotfix validation
- 🏷️ Automatic version updates in .csproj files
- 📝 Release notes generation

## 📋 GitHub Configuration Checklist

### Required Repository Settings

1. **General Settings:**
   - [ ] Go to Settings → General
   - [ ] Under "Pull Requests", check:
     - ✅ Allow merge commits
     - ✅ Allow squash merging  
     - ✅ Allow rebase merging
     - ✅ Always suggest updating pull request branches
     - ✅ Automatically delete head branches

2. **Branch Protection for `main`:**
   - [ ] Go to Settings → Branches
   - [ ] Add rule for `main` branch:
     - ✅ Require pull request reviews before merging
     - ✅ Require status checks to pass before merging
     - ✅ Required status checks:
       - `🟣 .NET API Validation`
       - `🔍 Code Quality & Security`
       - `🧪 Integration Tests`
       - `🐳 Docker Build & Security Scan`
       - `⚡ Performance Testing`
       - `📚 API Documentation`
     - ✅ Require branches to be up to date before merging
     - ✅ Require conversation resolution before merging
     - ✅ Include administrators
     - ✅ Restrict pushes that create files larger than 100MB

3. **Branch Protection for `release/*` pattern:**
   - [ ] Add rule for `release/*` branches:
     - ✅ Require status checks to pass before merging
     - ✅ Require branches to be up to date before merging
     - ✅ Same required status checks as main

4. **Container Registry Setup:**
   - [ ] Go to Settings → Actions → General
   - [ ] Under "Workflow permissions":
     - ✅ Read and write permissions
     - ✅ Allow GitHub Actions to create and approve pull requests

5. **Environment Setup (Optional but Recommended):**
   - [ ] Go to Settings → Environments
   - [ ] Create environments: `staging` and `production`
   - [ ] Add protection rules for production:
     - ✅ Required reviewers
     - ✅ Wait timer (e.g., 5 minutes)

## 📊 .NET Specific Configurations

### Project File Version Management

Ensure your `.csproj` file includes version information:

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Version>1.0.0</Version>
    <AssemblyVersion>1.0.0.0</AssemblyVersion>
    <FileVersion>1.0.0.0</FileVersion>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>
  <!-- ... rest of project file ... -->
</Project>
```

### Docker Configuration

Your `Dockerfile` should be optimized for production:

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/RecordManagement.Api/RecordManagement.Api.csproj", "src/RecordManagement.Api/"]
RUN dotnet restore "src/RecordManagement.Api/RecordManagement.Api.csproj"
COPY . .
WORKDIR "/src/src/RecordManagement.Api"
RUN dotnet build "RecordManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RecordManagement.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RecordManagement.Api.dll"]
```

### Health Checks

Add health checks to your API:

```csharp
// In Program.cs
builder.Services.AddHealthChecks();

app.MapHealthChecks("/health");
app.MapHealthChecks("/health/ready");
app.MapHealthChecks("/health/live");
```

## 🔍 Troubleshooting

### Common Issues

1. **Docker build failures:**
   - Check Dockerfile path and context
   - Verify .NET version compatibility
   - Ensure all files are included in build context

2. **Test failures in CI:**
   - Check test database configuration
   - Verify environment variables
   - Review test isolation issues

3. **Deployment failures:**
   - Check deployment target configuration
   - Verify secrets and environment variables
   - Review networking and connectivity

4. **Version management issues:**
   - Ensure .csproj version is updated correctly
   - Check semantic versioning format
   - Verify git tag creation

### .NET Specific Debugging

1. **Build Issues:**
   ```bash
   # Debug build locally
   cd src/RecordManagement.Api
   dotnet build --verbosity detailed
   dotnet test --verbosity detailed
   ```

2. **Package Issues:**
   ```bash
   # Check package references
   dotnet list package --vulnerable
   dotnet list package --deprecated
   dotnet restore --verbosity detailed
   ```

3. **Runtime Issues:**
   ```bash
   # Check runtime configuration
   dotnet run --configuration Release
   # Check logs in container
   docker logs <container-id>
   ```

## 🎯 API-Specific Advantages

### Why This Approach Works for APIs:

1. **Continuous Integration:**
   - Every feature is tested against main
   - Database migrations are validated
   - API contracts are verified

2. **Environment Progression:**
   - Clear staging → production path
   - Automated smoke tests
   - Health check validation

3. **Version Management:**
   - Semantic versioning in .csproj
   - Docker image tagging
   - API versioning coordination

4. **Security Focus:**
   - Container vulnerability scanning
   - Dependency vulnerability checks
   - Authentication/authorization validation

## 🎉 Next Steps

1. **Configure GitHub Settings:** Follow the checklist above
2. **Set Up Environments:** Create staging and production environments
3. **Configure Monitoring:** Add Application Insights or similar
4. **Database Setup:** Configure connection strings and migrations
5. **Test the Workflow:** Create a test feature branch and PR

---

**Happy coding with simplified .NET API branching! 🚀**

This setup provides professional API development workflow with automated testing, deployment, and monitoring, while keeping the branching strategy simple and maintainable.
