# DoubleV Technical Test — .NET + Angular + SQL Server (Docker)

Proyecto para la prueba técnica:

- Base de datos SQL Server con tablas **Personas** y **Usuario** + **Stored Procedure** para consultar personas creadas.
- API REST en **.NET** con **Clean Architecture** + **MediatR** + repositorios.
- Frontend **Angular** (básico) con **TailwindCSS**.
- Flujo: **Registro → Login (JWT) → Listado de Personas (protegido)**.

---

## Tecnologías

### Backend
- .NET (API REST)
- Clean Architecture (Domain / Application / Infrastructure / API)
- MediatR (Commands/Handlers)
- Entity Framework Core (ORM; **sin migrations**, DB creada por script) No se usaron migraciones debido a que se pidio un store procedure, por lo que opte por hacer la creacion de la DB con todas sus tablas desde un inicio en un solo script
- JWT 

### Base de datos
- SQL Server 2022 (Docker)
- Script de inicialización crea:
  - `dbo.Personas`
  - `dbo.Usuario` (FK a Personas)
  - Columnas calculadas persistidas
  - Índices únicos
  - Stored procedure: `dbo.sp_Personas_Creadas`

### Frontend
- Angular
- TailwindCSS
- Vistas:
  - Registro (crea Persona + Usuario)
  - Login (obtiene token JWT)
  - Personas (lista usando endpoint protegido)

---

## Requisitos
- Docker Desktop
- .NET SDK (7/8 recomendado)
- Node.js (18+ recomendado)
- Angular CLI

---

## 1) Levantar Base de Datos (Docker)

Desde la raíz del repo (donde está `docker-compose.yml`):

```bash
docker compose up -d
docker compose ps (lectura)
```

### Conexión (DBeaver / SSMS / Azure Data Studio)
- Host: `localhost`
- Port: `1433`
- User: `sa`
- Password: la configurada en `.env`
- DB: `DoubleVDb`

> Nota:
> - `docker compose down` **NO borra** datos (si hay volumen).
> - `docker compose down -v` **SÍ borra** el volumen y reinicia la DB desde cero.
> - **EL ARCHIVO .env DEBE CREARSE EN LA RAIZ DEL PROYECTO BACKEND backend/.env**
---

## 2) Configurar Backend (appsettings.Development.json)

En `DoubleV.API/appsettings.Development.json`:

```json
{
  "AllowedOrigins": "http://localhost:4200",
  "ConnectionStrings": {
    "SQLConnectionStrings": "Server=localhost,1433;Database=DoubleVDb;User Id=sa;Password=TU_PASSWORD;TrustServerCertificate=True;Encrypt=True;"
  },
  "Jwt": {
    "Key": "SUPER_SECRET_KEY_LOCAL_1234567890",
    "Issuer": "DoubleV",
    "Audience": "DoubleV.Front",
    "ExpiresMinutes": 60
  }
}
```
> - **EL ARCHIVO .env DEBE CREARSE EN LA RAIZ DEL PROYECTO BACKEND backend/.env**

- Reemplaza `TU_PASSWORD` por la contraseña real que definiste en `.env` / `docker-compose.yml`.

---

## 3) Levantar Backend (API)

Desde la carpeta de la API:

```bash
cd backend/DoubleV.API
dotnet run
```

Abrir Swagger (revisa el puerto exacto en consola “Now listening on”):
- `http://localhost:5076/swagger`
- o `https://localhost:5076/swagger`

> Si en Angular aparece `net::ERR_SSL_PROTOCOL_ERROR`, normalmente es porque estás llamando `https://` a un puerto que sirve `http://`.

---

## 4) Levantar Frontend (Angular)

```bash
cd frontend/doublev-front
npm install
ng serve
```

Frontend:
- `http://localhost:4200`

Configura la URL del API en `src/environments/environment.ts`:

```ts
export const environment = {
  apiUrl: 'http://localhost:5076/api'
};
```

---

## 5) Endpoints principales

> Las rutas exactas pueden variar; validar en Swagger.

- Registro (crea Persona + Usuario)
  - `POST /api/Users/Register`

- Login (devuelve JWT)
  - `POST /api/Login/Login`
  - Retorna `{ token, expiresIn, user }`

- Personas (protegido; usa Stored Procedure)
  - `GET /api/Persons/Created`
  - Header requerido:
    - `Authorization: Bearer <token>`

---

## 6) Resetear DB (desde cero)

```bash
docker compose down -v
docker compose up -d
```

---

## Autor
- Nombre: _Sebastian Gutierrez Perez_
- Repo: _https://github.com/sebastiangt06/doubleVpartners_
