## 📝 TodoApp

A modern fullstack Todo List management system built using clean architecture principles.

🔧 Tech Stack
- ⚙️ Backend: ASP.NET Core 8 Web API (Clean Architecture)
- 🌐 Frontend: React + Tailwind CSS
- 🗄️ Database: SQLite
- 🧪 Testing: xUnit + Moq


## 🚀 Features

- ✅ Add, read, update, complete, and delete todos
- 🔍 Global filtering, pagination & sorting
- 🗂️ Filter by status: Pending, Completed
- 🛑 Modal confirmations using SweetAlert2
- 🧪 Unit testing for API logic with xUnit & Moq
- 🧱 Clean Architecture: separation of concerns across layers

## 🗂️ Project Structure

TodoApp/
├── TodoApp.API/             # Web API entry point
├── TodoApp.Application/     # Application layer: services, DTOs
├── TodoApp.Core/            # Core models
├── TodoApp.Infrastructure/  # SQLite DB context, repositories, seed data
└── TodoApp.Tests/           # Unit tests (xUnit + Moq)
todo-frontend/               # React + Tailwind CSS frontend
README.md                   # Project documentation


## ⚙️ Backend Setup (ASP.NET Core)

1. Open the solution: `TodoApp.API.sln` in **Visual Studio** 
2. Open terminal:

   ```bash
   cd TodoApp.API
   dotnet run
   ```

## 💻 Frontend Setup

1. Open terminal and go to the frontend directory:

   ```bash
   cd todo-frontend
   ```

2. Install packages:

   ```bash
   npm install
   ```

3. Start the dev server:

   ```bash
   npm start
   ```

4.The frontend will run on http://localhost:3000 by default.


## 🧪 Running Unit Tests (Backend)

Run backend unit tests (xUnit + Moq):

```bash
dotnet test
```