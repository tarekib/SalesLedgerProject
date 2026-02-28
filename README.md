# SalesLedger — Sales Module

A small ERP-style Sales module built with ASP.NET Web Forms (.NET Framework 4.8) and Entity Framework 6.  
Manages Sales Orders, Invoices, Payments, Inventory, and General Ledger (GL) Transactions.

---

## Quick Start

### Prerequisites

| Tool | Version |
|------|---------|
| Visual Studio | 2022 (or later) with .NET Framework 4.8 workload |
| SQL Server | 2019+ (LocalDB, Express, or Developer) |

### Run the Project

1. **Clone** the repository
2. **Open** `SalesLedger/SalesLedger.sln` in Visual Studio
3. **Update the connection string** in `SalesLedger/Web.config` if your SQL Server instance is not `(local)`:
   ```xml
   <add name="SalesLedgerDb"
        connectionString="Data Source=(local);Initial Catalog=SalesLedgerDb;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True"
        providerName="System.Data.SqlClient" />
   ```
   Replace `(local)` with your server name (e.g., `.\SQLEXPRESS`, `localhost`, etc.).  
   For SQL Authentication use `User ID=...;Password=...` instead of `Integrated Security=True`.
4. **Build** the solution (NuGet packages restore automatically)
5. **Set `SalesLedger` as the startup project** and press **F5** (or Ctrl+F5)
6. The database `SalesLedgerDb` is **created automatically on first run** and seeded with sample data

> **No manual migration, no SQL scripts needed.** Entity Framework's `CreateDatabaseIfNotExists` initializer handles everything.

### Sample Data (Auto-Seeded)

| Entity | Data |
|--------|------|
| Customers | Tarek Ibrahim, Test Customer |
| Items | Item A ($100, 50 qty), Item B ($250, 20 qty), Item C ($75, 100 qty) |
| Sales Orders | 2 open orders with lines — ready to test the full workflow |

### Test the Full Workflow

1. Go to **Sales Orders** → click **Release** on an order → creates an invoice
2. Go to **Invoices** → click **Post** → subtracts inventory, creates GL entry
3. Click **Register Payment** on the posted invoice → enter amount → creates payment + GL entry
4. Check **Inventory** to see updated stock levels
5. Check **Journal Entries** to see all GL transactions with balanced debit/credit lines

---

## Tech Stack

| Layer | Technology |
|-------|------------|
| **Framework** | .NET Framework 4.8 |
| **Presentation** | ASP.NET Web Forms, Bootstrap 5, Google Fonts (Inter) |
| **Business Logic** | C# service classes with LINQ |
| **Data Access** | Entity Framework 6 (Code First), Repository + Unit of Work pattern |
| **Database** | Microsoft SQL Server |
| **Language** | C# 7.3 |

---

## Project Architecture

```
SalesLedgerProject/
│
├── SalesLedger/                        # Presentation layer (Web Forms)
│   ├── Site.Master                     # Master page — navigation, global CSS theme
│   ├── Default.aspx                    # Dashboard with KPI cards and quick actions
│   ├── SalesOrders/
│   │   ├── List.aspx                   # List SOs, Release → Invoice action
│   │   └── Create.aspx                 # Create new SO with dynamic line items
│   ├── Invoices/
│   │   ├── List.aspx                   # List invoices, Post / Register Payment
│   │   └── Create.aspx                 # Preview and create invoice from SO
│   ├── Payments/
│   │   ├── List.aspx                   # List all payments
│   │   └── Create.aspx                 # Register payment with invoice selection
│   ├── Inventory/
│   │   └── List.aspx                   # View current item stock levels
│   ├── Transactions/
│   │   └── List.aspx                   # Journal entries list + drill-down to lines
│   └── Web.config                      # Connection string, EF configuration
│
├── SalesLedger.Business/               # Business logic layer
│   ├── Services/
│   │   ├── SalesOrderService.cs        # SO creation, listing, customer/item lookups
│   │   ├── InvoiceService.cs           # Invoice creation, posting, GL generation
│   │   └── PaymentService.cs           # Payment + GL posting, inventory/GL queries
│   └── Models/
│       ├── SalesOrderDtos.cs           # View models for SO operations
│       ├── InvoiceDtos.cs              # View models for invoice operations
│       └── PaymentDtos.cs              # View models for payment, inventory, GL
│
├── SalesLedger.DataAccess/             # Data access layer
│   ├── Entities/                       # EF Code First entity classes
│   │   ├── Customer.cs                 # Customer(Id, Name)
│   │   ├── Item.cs                     # Item(Id, Name, UnitPrice, OnHandQuantity)
│   │   ├── SalesOrder.cs               # SalesOrder(Id, CustomerId, Date, Status)
│   │   ├── SalesOrderLine.cs           # SalesOrderLine(SalesOrderId, ItemId, Qty, UnitPrice)
│   │   ├── Invoice.cs                  # Invoice(Id, SalesOrderId, CustomerId, Date, Totals, Status)
│   │   ├── InvoiceLine.cs              # InvoiceLine(Id, InvoiceId, ItemId, Qty, UnitPrice, ...)
│   │   ├── Payment.cs                  # Payment(Id, CustomerId, InvoiceId, Date, Amount)
│   │   ├── GLTransaction.cs            # GLTransaction(Id, Date)
│   │   └── GLTransactionLine.cs        # GLTransactionLine(Id, GLTransactionId, Account, Debit, Credit)
│   ├── Interfaces/                     # Repository abstractions
│   ├── Repositories/                   # Repository + UnitOfWork implementations
│   ├── SalesLedgerDbContext.cs         # DbContext with Fluent API configuration
│   └── SalesLedgerDbInitializer.cs     # Auto-creates DB and seeds sample data
│
├── SQL/                                # SQL artifacts
│   ├── sp_PostInvoice.sql              # Stored procedure for invoice posting
│   ├── indexes.sql                     # Index definitions with justification
│   └── open_invoices_by_customer.sql   # JOIN query for open invoices
│
└── SalesLedger.sln
```

### Design Decisions

| Decision | Rationale |
|----------|-----------|
| **Three-layer architecture** | Clear separation — Presentation → Business → Data Access. The web project never touches `DbContext` directly. |
| **Repository + Unit of Work** | Abstracts EF behind interfaces. A single `UnitOfWork` wraps all operations in one transaction (e.g., posting an invoice updates stock, creates GL entries, and changes status atomically). |
| **Service layer** | Business rules (validation, calculations, status transitions, GL posting) live in service classes, keeping code-behind thin. |
| **Code First + Fluent API** | Relationships, precision, and constraints are configured centrally in `OnModelCreating`, keeping entity classes clean. |
| **DTOs** | The business layer returns DTOs (not EF entities) to the presentation layer, preventing lazy-loading issues and keeping the contract explicit. |
| **CreateDatabaseIfNotExists** | Simplest initializer — creates the DB and seeds on first run. No migration commands needed for reviewers. |

---

## Entities

| Entity | Fields |
|--------|--------|
| **Customer** | Id, Name |
| **Item** | Id, Name, UnitPrice, OnHandQuantity |
| **SalesOrder** | Id, CustomerId, Date, Status |
| **SalesOrderLine** | SalesOrderId, ItemId (composite PK), Qty, UnitPrice |
| **Invoice** | Id, SalesOrderId, CustomerId, Date, NetTotal, TaxTotal, GrossTotal, AmountPaid, Status |
| **InvoiceLine** | Id, InvoiceId, ItemId, Qty, UnitPrice, LineTotal, TaxAmount |
| **Payment** | Id, CustomerId, InvoiceId, Date, Amount |
| **GLTransaction** | Id, Date |
| **GLTransactionLine** | Id, GLTransactionId, Account, Debit, Credit |

---

## Modules & Workflow

### 1. Sales Orders

- Create a sales order (status: **Open**) with one or more lines
- No accounting impact at this stage

### 2. Release SO → Invoice

- Release an open SO to generate an invoice
- Invoice lines copied from SO lines; totals calculated:
  - `NetTotal = Σ(Qty × UnitPrice)`
  - `TaxTotal = NetTotal × 0.11` (fixed 11% VAT)
  - `GrossTotal = NetTotal + TaxTotal`
- SO status → **Released**, Invoice status → **Open**

### 3. Post Invoice

- Subtracts `OnHandQuantity` for each item on the invoice
- Creates a balanced GL transaction:

  | Account | Debit | Credit |
  |---------|-------|--------|
  | 1100 AR | GrossTotal | — |
  | 4000 Sales | — | NetTotal |
  | 2100 VAT Payable | — | TaxTotal |

- Invoice status → **Posted**

### 4. Receive Payment

- Select a posted or partially paid invoice
- Enter payment amount (validated ≤ balance due)
- Creates a GL transaction:

  | Account | Debit | Credit |
  |---------|-------|--------|
  | 1000 Cash | Amount | — |
  | 1100 AR | — | Amount |

- Invoice status → **Paid** (fully paid) or **PartiallyPaid**

### Status Flow

```
Sales Order:  Open ──→ Released
Invoice:      Open ──→ Posted ──→ PartiallyPaid ──→ Paid
```

### Supporting Views

| View | Purpose |
|------|---------|
| **Inventory** | Real-time stock levels per item (updated after invoice posting) |
| **Journal Entries** | All GL entries with balanced/unbalanced indicator and drill-down to lines |

---

## Common Issues

| Issue | Solution |
|-------|----------|
| **Connection string error** | Update `Data Source` in `Web.config` to match your SQL Server instance |
| **Login failed** | Check SQL Server is running, credentials are correct, and SQL/Windows auth mode matches |
| **Database not created** | Ensure the SQL user has `CREATE DATABASE` permission |
| **Model changed error** | Delete the `SalesLedgerDb` database and restart — it will be recreated |
