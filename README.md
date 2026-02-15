# DevConnect

### Intern IT Kennisplatform & Discussieomgeving

DevConnect is split into two runnable projects:

- `src/DevConnectApi.Api`: ASP.NET Core Web API with Swagger and in-memory seed data.
- `src/DevConnectWeb.Blazor`: Blazor front-end that can call the API Swagger endpoint.

## API

The API exposes:

- `api/articles` for article CRUD
- `api/forums/threads` for forum threads and comments

Swagger UI is available at `/swagger`.

## Blazor

The Blazor app includes a home page with a button to validate API reachability.
Configure the API URL in `src/DevConnectWeb.Blazor/appsettings.json` (`ApiBaseUrl`).
