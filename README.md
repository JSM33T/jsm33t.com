# 💼 jsm33t.com — Fullstack Repo

This is a fullstack portfolio application built with:

- ⚙️ [.NET Web API](/dotnet-api) — Core backend, authentication, comments, API logic
- 🐍 [FastAPI](/api-fastapi) — AI tagging, NLP, or analytics services
- 🧩 [Angular UI](/ui) — Frontend interface with modern Angular 20
- 🗃️ [PostgreSQL & Redis] — Used via Docker (optional)

---

## 📁 Project Structure

```
/dotnet-api/       → ASP.NET Core Web API (C#)
/api-fastapi/      → FastAPI (Python) for auxiliary services
/ui/               → Angular 20 frontend (renamed from 'frontend')
/infra/            → Deployment scripts, nginx config, Dockerfiles
/db/               → SQL backups, migrations, seeders
/postman/          → API testing collections + environments
```

---

## 🚀 Getting Started (Dev)

1. **Install Node, .NET, and Python**
2. Clone the repo:
   ```bash
   git clone https://github.com/yourname/portfolio-app.git
   cd portfolio-app
   ```
3. **Start services**:
   ```bash
   docker-compose up -d    # optional: postgres, redis
   ```

4. **Run individual projects**:

- 🔹 .NET API:
  ```bash
  cd dotnet-api
  dotnet run
  ```

- 🔹 FastAPI:
  ```bash
  cd api-fastapi
  uvicorn main:app --host 0.0.0.0 --port 8000
  ```

- 🔹 Angular UI:
  ```bash
  cd ui
  npm install
  ng serve
  ```

---

## 🧪 API Testing

Use `/postman/portfolio-api.postman_collection.json` for testing all APIs.

---

## 📜 License

This project is licensed under the [MIT License](./LICENSE).
