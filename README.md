# PandaList 🐼

A web application to track and organize your films, series, books, and more — built with ASP.NET Core and Blazor.

🌐 **Live demo:** [pandalist.onrender.com](https://pandalist.onrender.com)

---

## Features

- 📋 Create and manage personal lists (films, series, books, etc.)
- 🔐 User authentication and registration (ASP.NET Identity)
- 👤 Each user has their own private list
- 📱 Responsive design — works on desktop and mobile
- 🐳 Dockerized for easy deployment

---

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core (C#) |
| UI | Blazor Components + Razor Pages |
| Auth | ASP.NET Core Identity |
| ORM | Entity Framework Core |
| Database | SQL Server / SQLite |
| Deployment | Docker + Render |

---

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) (optional)

### Run locally

```bash
git clone https://github.com/JoseTomasPM/PandaList.git
cd PandaList
dotnet ef database update
dotnet run
```

Then open `https://localhost:5001` in your browser.

### Run with Docker

```bash
docker build -t pandalist .
docker run -p 8080:80 pandalist
```

---

## Project Structure

```
PandaList/
├── Components/       # Blazor components
├── Pages/            # Razor pages
├── Models/           # Data models
├── Data/             # DbContext and seed data
├── Migrations/       # EF Core migrations
├── Areas/Identity/   # Auth pages
└── wwwroot/          # Static files (CSS, JS)
```

---

## Author

**José Tomás Pérez Martínez** — [GitHub](https://github.com/JoseTomasPM)
