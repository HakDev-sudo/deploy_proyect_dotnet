FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj files and restore
COPY ["ArcheryAcademy.API/ArcheryAcademy.API.csproj", "ArcheryAcademy.API/"]
COPY ["ArcheryAcademy.Application/ArcheryAcademy.Application.csproj", "ArcheryAcademy.Application/"]
COPY ["ArcheryAcademy.Domain/ArcheryAcademy.Domain.csproj", "ArcheryAcademy.Domain/"]
COPY ["ArcheryAcademy.Infrastructure/ArcheryAcademy.Infrastructure.csproj", "ArcheryAcademy.Infrastructure/"]

RUN dotnet restore "ArcheryAcademy.API/ArcheryAcademy.API.csproj"

# Copy everything else and publish
COPY . .
RUN dotnet publish "ArcheryAcademy.API/ArcheryAcademy.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Environment variables (override in production)
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:80

# Database
ENV ConnectionStrings__DefaultConnection=""

# JWT Settings
ENV JwtSettings__Secret=""
ENV JwtSettings__Issuer="ArcheryAcademy"
ENV JwtSettings__Audience="ArcheryAcademyUsers"
ENV JwtSettings__ExpiryMinutes="120"

# Google OAuth (Calendar)
ENV Google__ClientId=""
ENV Google__ClientSecret=""
ENV Google__RedirectUri=""

# Azure Blob Storage (Certificates)
ENV AzureStorage__ConnectionString=""
ENV AzureStorage__ContainerName="certificados"

EXPOSE 80

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ArcheryAcademy.API.dll"]
