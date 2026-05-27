# HomeGrown API

ASP.NET Core Web API — REST backend for the HomeGrown platform.

## Stack

| Layer | Tech |
|-------|------|
| Framework | ASP.NET Core 10, C# |
| Database | PostgreSQL 16 (via Docker locally) |
| ORM | Entity Framework Core 9 + Npgsql |
| Auth | JWT Bearer + Google OAuth |
| Docs | Swagger UI (`/swagger` in dev) |

---

## Local Setup

### 1. Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

### 2. Start the database

```bash
# From repo root
docker compose up -d
```

This starts PostgreSQL on port 5432 and pgAdmin at http://localhost:5050.

### 3. Apply migrations

```bash
dotnet ef database update \
  --project src/HomeGrown.Infrastructure \
  --startup-project src/HomeGrown.API
```

### 4. Run the API

```bash
cd src/HomeGrown.API
dotnet run
```

API is available at:
- http://localhost:5000
- Swagger UI: http://localhost:5000/swagger

---

## Authentication Flow

```
POST /api/auth/register   → returns { accessToken, refreshToken, user }
POST /api/auth/login      → returns { accessToken, refreshToken, user }
POST /api/auth/refresh    → rotates tokens, returns new pair
POST /api/auth/logout     → revokes refresh token
```

Include the access token on every protected request:
```
Authorization: Bearer <accessToken>
```

Access tokens expire in **15 minutes**. Use the refresh token to get a new one.

---

## Key Endpoints

```
GET    /api/farms                  public
GET    /api/farms/{id}             public
POST   /api/farms                  Farmer role
PUT    /api/farms/{id}             Farmer (own) or Admin

GET    /api/products               public, supports ?category=&minPrice=&maxPrice=&inStockOnly=&sort=
GET    /api/products/{id}          public
POST   /api/products               Farmer role
PUT    /api/products/{id}          Farmer (own) or Admin
DELETE /api/products/{id}          Farmer (own) or Admin

GET    /api/farms/{id}/reviews     public
POST   /api/farms/{id}/reviews     Buyer role

GET    /api/orders                 authenticated (own orders)
POST   /api/orders                 Buyer role

POST   /api/subscribe              public

GET    /api/admin/users            Admin only
DELETE /api/admin/users/{id}       Admin only
```

---

## Project Structure

```
src/
├── HomeGrown.Core/            Domain models, interfaces, DTOs (no dependencies)
│   ├── Domain/Entities/
│   ├── Domain/Enums/
│   ├── Domain/Interfaces/
│   └── Application/DTOs/
├── HomeGrown.Infrastructure/  EF Core, repositories, migrations
│   └── Persistence/
└── HomeGrown.API/             Controllers, middleware, JWT, startup
    ├── Controllers/
    ├── Services/
    └── Middleware/

tests/
└── HomeGrown.Tests/           xUnit tests
```

---

## Branching

- `main` — production only, PRs required
- `dev` — active development
- `feature/*` — branch off dev, PR back to dev
