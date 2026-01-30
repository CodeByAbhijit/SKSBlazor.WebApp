# SKSBlazor Web App (.NET 8)

## Overview
This project is a Proof of Concept (POC) to modernize a legacy VB6 application
into a modern **.NET 8 Blazor Web App (Interactive Server)**.

The goal is to incrementally migrate:
- UI (VB6 Forms → Blazor Components)
- Business logic
- Data access (ADO → EF Core)

---

## Technology Stack
- .NET 8 (LTS)
- Blazor Web App (Interactive Server, Global Interactivity)
- ASP.NET Core
- Dependency Injection
- Bootstrap (UI)

---

## Current Features
- Login page with validation
- Navigation layout (menu, pages)
- Products listing page (UI only)
- Orders approval page (UI only)

---

## Screenshots

### Login
![Login](Docs/screenshots/login.png)

### Home
![Home](Docs/screenshots/home.png)

### Products
![Products](Docs/screenshots/products.png)

### Orders Approval
![Orders](Docs/screenshots/orders.png)

---

## Project Status
UI baseline completed  
Login flow validated  
CRUD + Database integration (EF Core) – **next phase**

---

## Next Steps
- Introduce EF Core with SQL Server
- Implement real CRUD for Products and Orders
- Add authentication & authorization
- Apply clean architecture principles

---

## Notes
This repository represents the **baseline state** before introducing database
and business logic changes.
