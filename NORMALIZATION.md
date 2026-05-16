# Database Normalization Report
## Banking Management System

---

# Introduction

The Banking Management System database was analyzed and normalized to Third Normal Form (3NF) to eliminate redundancy, improve data integrity, and prevent update, insertion, and deletion anomalies.

The database contains the following main entities:

- Customers
- Branches
- Accounts
- Loans
- BankTransactions

---

# Original Functional Dependencies

## Customers Table
CustomerId → FirstName, LastName, Phone, Email, Address, DateOfBirth, CreatedAt, UpdatedAt

---

## Branches Table
BranchId → BranchName, City, State, ManagerName, CreatedAt, UpdatedAt


---

## Accounts Table
AccountId → AccountNumber, CustomerId, BranchId, AccountType, Balance, OpenDate, AccountStatus, CreatedAt, UpdatedAt


Also:
AccountNumber → AccountId

because account numbers are unique.

---

## Loans Table
LoanId → CustomerId, BranchId, LoanType, LoanAmount, InterestRate, StartDate, EndDate, LoanStatus, CreatedAt, UpdatedAt


---

## BankTransactions Table
TransactionId → AccountId, TransactionType, Amount, TransactionDate, Description, CreatedAt, UpdatedAt

---

# Anomaly Identification

Before normalization, combining customer, account, branch, and loan information into a single table would create multiple anomalies.

## 1. Update Anomaly

If customer information is repeated across multiple account rows, updating a customer address would require updating many records.

Example:
Customer "Ava Mitchell" appears in multiple account rows.
Changing the address requires updating every row manually.

This may cause inconsistent data.

---

## 2. Insertion Anomaly

A new customer could not be added unless an account already existed.

Example:
Cannot insert a customer without also inserting account information.

---

## 3. Deletion Anomaly

Deleting the last account of a customer could accidentally remove customer information.

Example:
Deleting the final account row may remove customer data entirely.

---

# First Normal Form (1NF)

The database satisfies 1NF because:

- Each table has a primary key.
- Each column contains atomic values.
- No repeating groups exist.

Example:
One customer record stores only one phone number and one email.

---

# Second Normal Form (2NF)

The database satisfies 2NF because:

- All non-key attributes fully depend on the primary key.
- No partial dependencies exist.

Example:
AccountType, Balance, and OpenDate depend entirely on AccountId.

---

# Third Normal Form (3NF)

The database satisfies 3NF because:

- No transitive dependencies exist.
- Non-key attributes depend only on the primary key.

Example:
Customer details are stored only in Customers table.
Branch details are stored only in Branches table.
Accounts table stores only foreign keys referencing these entities.

This removes redundancy and improves maintainability.

---

# Decomposition Process

The original conceptual design could have stored all banking information in one large table:

Customer + Account + Branch + Loan + Transaction

This structure would create redundancy and anomalies.

The database was decomposed into separate normalized tables:

| Table | Purpose |
|---|---|
| Customers | Stores customer information |
| Branches | Stores branch information |
| Accounts | Stores account information |
| Loans | Stores loan information |
| BankTransactions | Stores banking transactions |

Relationships were implemented using foreign keys.

---

# Final Relational Schema

## Customers
Customers(
    CustomerId PK,
    FirstName,
    LastName,
    Phone,
    Email,
    Address,
    DateOfBirth,
    CreatedAt,
    UpdatedAt
)

---

## Branches
Branches(
    BranchId PK,
    BranchName,
    City,
    State,
    ManagerName,
    CreatedAt,
    UpdatedAt
)
---

## Accounts
Accounts(
    AccountId PK,
    AccountNumber UNIQUE,
    CustomerId FK,
    BranchId FK,
    AccountType,
    Balance,
    OpenDate,
    AccountStatus,
    CreatedAt,
    UpdatedAt
)
---

## Loans
Loans(
    LoanId PK,
    CustomerId FK,
    BranchId FK,
    LoanType,
    LoanAmount,
    InterestRate,
    StartDate,
    EndDate,
    LoanStatus,
    CreatedAt,
    UpdatedAt
)
---

## BankTransactions
BankTransactions(
    TransactionId PK,
    AccountId FK,
    TransactionType,
    Amount,
    TransactionDate,
    Description,
    CreatedAt,
    UpdatedAt
)
---

# Relationship Summary

## One-to-Many Relationships

### Customers → Accounts

One customer can have multiple accounts.

---

### Customers → Loans

One customer can have multiple loans.

---

### Branches → Accounts

One branch can manage multiple accounts.

---

### Branches → Loans

One branch can manage multiple loans.

---

### Accounts → BankTransactions

One account can have multiple transactions.

---

# Conclusion

The Banking Management System database was successfully normalized to Third Normal Form (3NF).

Benefits achieved:

- Reduced redundancy
- Improved data consistency
- Prevention of update, insertion, and deletion anomalies
- Better scalability and maintainability
- Strong relational integrity using primary and foreign keys

The final database structure supports efficient CRUD operations, transaction processing, and dashboard analytics in the Banking Management System application.
