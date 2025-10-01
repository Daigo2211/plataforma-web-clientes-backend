# Sistema de Gestión de Clientes - Backend

API REST para gestión de catálogo de clientes con archivos adjuntos.

## Stack Técnico

- .NET 8 + C#
- Entity Framework Core 8.0.10 + SQL Server
- AutoMapper 15.0.1
- FluentValidation
- Swagger (Swashbuckle 9.0.5)

## Arquitectura - Clean Architecture

```
src/
├── Maqui.API/              # Controllers, Middleware, Program.cs
├── Maqui.Application/      # DTOs, Services, Validators, Mappings
├── Maqui.Domain/           # Entidades, Enums, Interfaces
└── Maqui.Infrastructure/   # DbContext, Repositories, FileService
```

## Setup Rápido

### Prerrequisitos
- .NET 8 SDK
- SQL Server Express o LocalDB

### Instalación

```bash
# 1. Clonar
git clone <url-del-repo>
cd Maqui-web-backend

# 2. Restaurar paquetes
dotnet restore

# 3. Configurar BD
cp src/Maqui.API/appsettings.example.json src/Maqui.API/appsettings.json
# Editar connection string en appsettings.json

# 4. Crear BD y aplicar migraciones
cd src/Maqui.API
dotnet ef database update --project ../Maqui.Infrastructure

# 5. Ejecutar
dotnet run
```

Swagger disponible en: `https://localhost:{puerto}/swagger`

## Endpoints

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/clientes` | Listar clientes activos |
| GET | `/api/clientes/{id}` | Obtener cliente por ID |
| POST | `/api/clientes` | Crear cliente (multipart/form-data) |
| PUT | `/api/clientes/{id}` | Actualizar cliente (multipart/form-data) |
| DELETE | `/api/clientes/{id}` | Eliminado lógico |

## Validaciones

- **DNI**: 8 dígitos numéricos
- **RUC**: 11 dígitos numéricos  
- **Carnet**: alfanumérico (max 20 caracteres)
- **Hoja de vida**: PDF, máximo 5MB
- **Foto**: JPG/JPEG
- **NumeroDocumento**: único en BD
- **Edición**: No se puede cambiar TipoDocumento ni NumeroDocumento

## Estructura BD

**Tabla: Clientes**

| Campo | Tipo | Restricciones |
|-------|------|---------------|
| Id | int | PK, Identity |
| Nombres | nvarchar(100) | NOT NULL |
| Apellidos | nvarchar(100) | NOT NULL |
| FechaNacimiento | date | NOT NULL |
| TipoDocumento | nvarchar(50) | NOT NULL |
| NumeroDocumento | nvarchar(20) | UNIQUE |
| RutaHojaVida | nvarchar(500) | NOT NULL |
| RutaFoto | nvarchar(500) | NOT NULL |
| EsActivo | bit | DEFAULT(1) |
| FechaCreacion | datetime2(7) | NOT NULL |
| FechaModificacion | datetime2(7) | NULL |

## Scripts EF Core

```bash
# Nueva migración
dotnet ef migrations add NombreMigracion --project src/Maqui.Infrastructure --startup-project src/Maqui.API

# Aplicar migraciones
dotnet ef database update --project src/Maqui.Infrastructure --startup-project src/Maqui.API

# Revertir migración
dotnet ef migrations remove --project src/Maqui.Infrastructure --startup-project src/Maqui.API
```

## Patrones de Diseño

- **Repository Pattern**: Abstracción de acceso a datos
- **Service Layer**: Lógica de negocio separada
- **Dependency Injection**: Inyección por constructor
- **DTO Pattern**: Separación entidades/contratos
- **Strategy Pattern**: FileService intercambiable

## Principios SOLID

- **SRP**: Cada clase una responsabilidad
- **OCP**: Extensible vía interfaces
- **LSP**: Implementaciones intercambiables
- **ISP**: Interfaces específicas
- **DIP**: Dependencia de abstracciones

## Frontend

👉 https://github.com/Daigo2211/plataforma-web-clientes-frontend.git

## Autor

Prueba técnica Full Stack - MaquiMas