# Career OS — PostgreSQL Database Setup

## Overview

Career OS uses PostgreSQL as its relational database management system (RDBMS).

PostgreSQL will provide persistent storage for application data, including goals, skills, projects, and learning sessions.

The database will eventually be accessed by the ASP.NET Core REST API through Entity Framework Core (EF Core) and the Npgsql database provider.

## Development Environment

- Operating System: Ubuntu 26.04
- Database: PostgreSQL
- Database Server: localhost
- Default Port: 5432
- Database Name: `career_os`
- Application Role: `career_os_dev`
- Database Client: `psql`

## 1. Installing PostgreSQL

Update the package repositories:

```bash
sudo apt update
```

Install PostgreSQL Server and Client:

```bash
sudo apt install postgresql postgresql-client
```

Verify the installation:

```bash
psql --version
```

Check whether PostgreSQL is accepting connections:

```bash
pg_isready
```

If PostgreSQL is not running, start the service:

```bash
sudo systemctl start postgresql
```

## 2. Accessing PostgreSQL

PostgreSQL creates an administrative role named `postgres` during installation.

Connect using the PostgreSQL administrative account:

```bash
sudo -u postgres psql
```

Useful commands inside `psql`:

```sql
SELECT version();
SELECT current_user;
```

List databases:

```text
\l
```

List roles:

```text
\du
```

Exit PostgreSQL:

```text
\q
```

Commands beginning with `\` are PostgreSQL `psql` meta-commands rather than standard SQL statements.

## 3. Creating the Application Role

A dedicated role is used for Career OS instead of connecting with the administrative `postgres` account.

Connect as the PostgreSQL administrator:

```bash
sudo -u postgres psql
```

Create the application role:

```sql
CREATE ROLE career_os_dev WITH LOGIN;
```

Assign a password:

```text
\password career_os_dev
```

Verify the role exists:

```text
\du
```

The application role does not require PostgreSQL superuser privileges.

**Security:** Do not store the database password in this document or commit it to GitHub.

## 4. Creating the Database

Create the Career OS database:

```sql
CREATE DATABASE career_os OWNER career_os_dev;
```

Verify the database exists:

```text
\l
```

Expected configuration:

| Property | Value |
|---|---|
| Database | `career_os` |
| Owner | `career_os_dev` |
| Host | `localhost` |
| Port | `5432` |

The database is owned by the dedicated application role.

## 5. Connecting to the Database

Exit the administrative session:

```text
\q
```

Connect using the Career OS role:

```bash
psql -h localhost -U career_os_dev -d career_os
```

Enter the role's password when prompted.

Verify the active database:

```sql
SELECT current_database();
```

Expected result:

```text
career_os
```

Verify the active user:

```sql
SELECT current_user;
```

Expected result:

```text
career_os_dev
```

Display connection information:

```text
\conninfo
```

## 6. SQL Practice

A temporary table was created to practice PostgreSQL SQL operations.

```sql
CREATE TEMP TABLE sql_practice (
    id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    title TEXT NOT NULL,
    is_completed BOOLEAN NOT NULL DEFAULT FALSE
);
```

This exercise covered:

- Creating tables
- PostgreSQL `TEXT` and `BOOLEAN` data types
- Identity columns
- Primary keys
- INSERT operations
- SELECT queries
- UPDATE operations
- DELETE operations

### Important PostgreSQL Concepts

**Identity columns:** `GENERATED ALWAYS AS IDENTITY` provides automatically generated values, similar to SQL Server's `IDENTITY`.

**TEXT:** Stores variable-length strings without requiring an explicit maximum length.

**BOOLEAN:** Stores `TRUE` or `FALSE`, unlike SQL Server's `BIT` type.

**Temporary tables:** Exist only within the database session and are automatically removed when the session ends.

The SQL practice table is not part of the permanent Career OS database schema.

## 7. Connection Strings

ASP.NET Core will eventually connect to PostgreSQL using a connection string.

Example format:

```text
Host=localhost;Port=5432;Database=career_os;Username=career_os_dev;Password=<PASSWORD>
```

The connection string identifies:

- Database server
- Port
- Database name
- Database role
- Authentication credentials

Actual passwords must not be committed to source control.

During development, connection credentials will be managed using .NET User Secrets.

## 8. Current Status

Completed:

- [x] PostgreSQL installed on Ubuntu
- [x] PostgreSQL Server verified
- [x] Dedicated application role created
- [x] Career OS database created
- [x] Database ownership configured
- [x] Connection authenticated successfully
- [x] Basic SQL CRUD operations practiced

Pending:

- [ ] Install Entity Framework Core packages
- [ ] Install Npgsql provider
- [ ] Configure database connection in ASP.NET Core
- [ ] Create the application's `DbContext`
- [ ] Generate database migrations
- [ ] Replace in-memory Goals storage with PostgreSQL
- [ ] Verify persistent data after restarting the application

## 9. Next Steps

Introduce Entity Framework Core and connect the ASP.NET Core application to PostgreSQL.

The goal is to replace the current in-memory data collection with database-backed storage.

Database tables will be managed through EF Core migrations instead of being created manually.