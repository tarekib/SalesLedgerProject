# SalesLedger

`SalesLedger` is a .NET Framework 4.8 solution with:
- `SalesLedger` (Web project)
- `SalesLedger.Business` (Business layer)
- `SalesLedger.DataAccess` (EF6 data access)

## Prerequisites

- Visual Studio 2022 (with `.NET Framework 4.8` workload)
- SQL Server (Database Engine) installed locally or reachable on network
- SQL login available (or use Windows Authentication by changing the connection string)
- NuGet package restore enabled

## Clone and open

1. Clone repository.
2. Open solution in Visual Studio.
3. Restore NuGet packages (automatic on build, or via NuGet restore).

## Database configuration (important)

Before first run, update connection string `SalesLedgerDb` in both files:
- `SalesLedger/Web.config`
- `SalesLedger.DataAccess/App.config`

Use your own SQL Server instance name (examples):
- `localhost`
- `.\SQLEXPRESS`
- `<YOUR-PC-NAME>`
- `<YOUR-SERVER>\<INSTANCE>`

### SQL Authentication example

`Data Source=<YOUR_SERVER_OR_INSTANCE>;Initial Catalog=SalesLedgerDb;User ID=<SQL_USER>;Password=<SQL_PASSWORD>;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True`

### Windows Authentication example

`Data Source=<YOUR_SERVER_OR_INSTANCE>;Initial Catalog=SalesLedgerDb;Integrated Security=True;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True`

## Create database and seed data

This project uses Entity Framework 6 code-first migrations configuration in `SalesLedger.DataAccess`.

### Option A (recommended)

Run EF database update. EF will create/update schema and run seed.

### Option B (EF6 command line)

From PowerShell, run EF6 tool:
- `SalesLedger/packages/EntityFramework.6.4.4/tools/net45/any/ef6.exe`

Command pattern:

`ef6 database update --assembly <path to SalesLedger.DataAccess.dll> --project-dir <path to SalesLedger.DataAccess> --config <path to SalesLedger.DataAccess/App.config> --connection-string-name SalesLedgerDb --migrations-config SalesLedger.DataAccess.Migrations.Configuration`

## Seeded sample data

Seed inserts sample records for:
- Customers
- Items
- Sales Orders + Sales Order Lines
- Invoices + Invoice Lines
- Payments
- GL Transactions + GL Transaction Lines

## Run the application

1. Set `SalesLedger` as startup project.
2. Build solution.
3. Run (`F5` / IIS Express).

## Connect with SSMS

Use the same SQL Server instance and credentials from your connection string.

After connecting, refresh `Databases` and confirm `SalesLedgerDb` exists.

## Git ignore and tracked artifacts

Repository includes `.gitignore` to exclude:
- `.vs/`
- `bin/`, `obj/`, `Debug/`, `Release/`
- user/temp/log/test artifacts

If build artifacts were previously tracked, untrack once with:

`git rm -r --cached <artifact paths>`

then commit.

## Common issues

### `No connection string named 'SalesLedgerDb' could be found`

Ensure `SalesLedgerDb` exists in:
- `SalesLedger.DataAccess/App.config`
- `SalesLedger/Web.config`

### SQL login failed

- Confirm SQL Server is running.
- Confirm SQL Authentication mode is enabled (if using SQL login).
- Confirm login/password are correct.
- Confirm server/instance name in connection string.

### Database not visible in SSMS

- Connect to the same instance used in `Data Source`.
- Refresh `Databases`.
