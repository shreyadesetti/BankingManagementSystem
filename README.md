# Banking Management System

## Project Description

The Banking Management System is a full-stack web application developed using ASP.NET Core MVC, Entity Framework Core, and SQL Server. The application allows users to manage customers, branches, accounts, loans, and banking transactions through a modern and responsive user interface.

This project demonstrates database normalization, multi-table CRUD operations, one-to-many relationship management, transaction processing, server-side validation, dashboard analytics, and version control using Git.

---

# Technology Stack

| Technology | Used |
|---|---|
| Backend Framework | ASP.NET Core MVC |
| Programming Language | C# |
| Database | SQL Server |
| ORM | Entity Framework Core |
| Frontend | HTML5, CSS3, Bootstrap |
| Version Control | Git & GitHub |

---

# Features

## Customer Management
- Create, update, view, and delete customers
- Store customer personal information

## Branch Management
- Manage bank branches and locations

## Account Management
- Create multiple accounts for a single customer
- Supports one-to-many relationship between Customers and Accounts

## Loan Management
- Create and manage loans
- Associate loans with customers and branches

## Transaction Management
- Deposit and withdrawal operations
- SQL transaction logic implemented using Entity Framework transactions
- Account balances update automatically

## Dashboard Analytics
- Total customers
- Total branches
- Total accounts
- Total loans
- Total balance
- Average balance
- Total transactions
- Aggregate calculations using COUNT, SUM, and AVG

---

# One-to-Many Relationship Demonstration

This application demonstrates the following one-to-many relationships:

- One Customer can have multiple Accounts
- One Customer can have multiple Loans
- One Branch can manage multiple Accounts
- One Branch can manage multiple Loans

Example:

Customer:
- Ava Mitchell

Accounts:
- KS10001
- KS10002
- KS10003

This proves a one-to-many relationship between Customers and Accounts.

---

# Database Setup

## Step 1: Clone Repository
git clone <YOUR_GITHUB_REPOSITORY_URL>

---

## Step 2: Open Project

Open the project using:

- Visual Studio Code
OR
- Visual Studio

---

## Step 3: Configure SQL Server Connection

Update `appsettings.json`:

"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=BankingManagementDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}

---

## Step 4: Run Entity Framework Migration
dotnet ef database update

This command automatically creates the SQL Server database and tables.

---

# Running the Application
Run the project:
dotnet run

Application URL:
http://localhost:5125

---

# Screenshots Of Application

## Dashboard
<img width="1280" height="1305" alt="image" src="https://github.com/user-attachments/assets/d4242e71-6c9c-4f4a-8ee6-2def5e335a0a" />

## Customers Page
<img width="1275" height="1306" alt="image" src="https://github.com/user-attachments/assets/715db697-de26-4ed1-b6b1-a6172d74fe9e" />

## Accounts Page
<img width="1276" height="1309" alt="image" src="https://github.com/user-attachments/assets/d80ec68f-525f-4aa2-ba99-8e80b9895f3c" />

## Loans Page
<img width="1272" height="1302" alt="image" src="https://github.com/user-attachments/assets/92055f92-ae23-4b32-9335-5a49a179e937" />

## Transactions Page
<img width="1279" height="613" alt="image" src="https://github.com/user-attachments/assets/5a948222-740a-4088-9470-4e9ea03eb58a" />

# Data Validation

Server-side validation has been implemented using Data Annotations.

Examples:
- Required fields
- Email validation
- Positive balance validation
- Transaction amount validation

---

# Transaction Logic

The application uses Entity Framework Core database transactions for banking operations.

Example:
- Deposit money
- Update account balance
- Insert transaction record
- Commit transaction together

This ensures data consistency and integrity.

---

# Git Commit History

The repository contains multiple incremental commits demonstrating the project development lifecycle.

---

# Stack Change Disclosure

The original project requirements suggested Python and Flask/FastAPI.  
This project was implemented using ASP.NET Core MVC and SQL Server because of prior experience with the .NET ecosystem and Entity Framework Core.

---

# Author

Sri Shreya Desetti
A594F253
