-- schema.sql
-- Banking Management System
-- Final SQL Schema in 3rd Normal Form

CREATE TABLE Branches (
    BranchId INT IDENTITY(1,1) PRIMARY KEY,
    BranchName NVARCHAR(100) NOT NULL,
    BranchCode NVARCHAR(20) NOT NULL UNIQUE,
    Address NVARCHAR(200) NOT NULL,
    City NVARCHAR(100) NOT NULL,
    State NVARCHAR(100) NOT NULL,
    ZipCode NVARCHAR(20) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME2 NULL
);

CREATE TABLE Customers (
    CustomerId INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) NOT NULL UNIQUE,
    PhoneNumber NVARCHAR(20) NOT NULL,
    Address NVARCHAR(200) NOT NULL,
    DateOfBirth DATE NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME2 NULL
);

CREATE TABLE Accounts (
    AccountId INT IDENTITY(1,1) PRIMARY KEY,
    AccountNumber NVARCHAR(30) NOT NULL UNIQUE,
    CustomerId INT NOT NULL,
    BranchId INT NOT NULL,
    AccountType NVARCHAR(50) NOT NULL,
    Balance DECIMAL(18,2) NOT NULL DEFAULT 0,
    OpenedDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME2 NULL,

    CONSTRAINT FK_Accounts_Customers
        FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId),

    CONSTRAINT FK_Accounts_Branches
        FOREIGN KEY (BranchId) REFERENCES Branches(BranchId),

    CONSTRAINT CK_Accounts_Balance
        CHECK (Balance >= 0),

    CONSTRAINT CK_Accounts_AccountType
        CHECK (AccountType IN ('Savings', 'Checking'))
);

CREATE TABLE Loans (
    LoanId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT NOT NULL,
    BranchId INT NOT NULL,
    LoanType NVARCHAR(50) NOT NULL,
    LoanAmount DECIMAL(18,2) NOT NULL,
    InterestRate DECIMAL(5,2) NOT NULL,
    LoanStartDate DATE NOT NULL,
    LoanEndDate DATE NOT NULL,
    LoanStatus NVARCHAR(50) NOT NULL DEFAULT 'Active',
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME2 NULL,

    CONSTRAINT FK_Loans_Customers
        FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId),

    CONSTRAINT FK_Loans_Branches
        FOREIGN KEY (BranchId) REFERENCES Branches(BranchId),

    CONSTRAINT CK_Loans_LoanAmount
        CHECK (LoanAmount > 0),

    CONSTRAINT CK_Loans_InterestRate
        CHECK (InterestRate >= 0),

    CONSTRAINT CK_Loans_LoanStatus
        CHECK (LoanStatus IN ('Active', 'Closed', 'Pending', 'Rejected')),

    CONSTRAINT CK_Loans_DateRange
        CHECK (LoanEndDate > LoanStartDate)
);

CREATE TABLE BankTransactions (
    TransactionId INT IDENTITY(1,1) PRIMARY KEY,
    AccountId INT NOT NULL,
    TransactionType NVARCHAR(50) NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    TransactionDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    Description NVARCHAR(250) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_BankTransactions_Accounts
        FOREIGN KEY (AccountId) REFERENCES Accounts(AccountId),

    CONSTRAINT CK_BankTransactions_Amount
        CHECK (Amount > 0),

    CONSTRAINT CK_BankTransactions_TransactionType
        CHECK (TransactionType IN ('Deposit', 'Withdrawal'))
);
