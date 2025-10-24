


#  Contract Monthly Claim System

###  Prototype Web Application — Part 2 (.NET Core MVC)


##  Overview

The **Contract Monthly Claim System (CMCS)** is a .NET Core MVC web application designed to help **Lecturers, Programme Coordinators, and Academic Managers** efficiently manage, track, and approve teaching claims.

The system provides a secure and user-friendly interface where lecturers can **submit and track claims**, coordinators can **verify** them, and managers can **approve or reject** claims — all connected to a **SQL LocalDB** database for data persistence.



##  Features Implemented

###  Lecturer Features

* Submit claims through a clean, easy-to-use form.
* Upload supporting documents (PDF, DOCX, XLSX) with file validation and size restrictions (max 5MB).
* View all submitted claims with their **real-time statuses** (Pending, Approved, or Rejected).
* Receive instant success/error feedback messages after submission.

### 🧑‍💼 Programme Coordinator Features

* View **all pending claims**.
* Verify details and update claim statuses.
* Approve or reject claims with a single click.

### 👨‍💼 Academic Manager Features

* View verified claims from coordinators.
* Approve or reject with confirmation and visual indicators.
* Automatically update lecturer claim status after approval.

###  Tracking System

* Claim status is dynamically displayed using **Bootstrap badges**:
  🟡 *Pending*, 🟢 *Approved*, 🔴 *Rejected*, 🔵 *Verified*
* Transparent status flow across all user roles.

###  SQL LocalDB Integration

* Database and tables (`Users`, `Claims`) are automatically created via the `sql_query.cs` model.
* Uses `System.Data.SqlClient` for direct database connections.
* Includes schema for relationships between lecturers and their claims.

###  Unit Testing

* Unit tests validate calculation logic and claim status updates.
* Ensures system reliability and correctness.
* Implemented using **xUnit** framework in `ContractMonthlyClaimSystem.Tests`.

### ⚙️ Error Handling

* Handles SQL and application exceptions gracefully.
* Displays meaningful user messages instead of technical errors.

---

## Database Setup

The `sql_query.cs` model automatically initializes:

* LocalDB instance: `claim_system`
* Database: `claims_database`
* Tables:

  * **Users:** stores lecturer and staff information
  * **Claims:** stores claim submissions linked to `Users`

### Example Auto-Generated Tables

**Users Table**

| Column     | Type         | Description                      |
| ---------- | ------------ | -------------------------------- |
| userID     | INT          | Primary key                      |
| full_names | VARCHAR(150) | Lecturer’s name                  |
| surname    | VARCHAR(150) | Lecturer’s surname               |
| email      | VARCHAR(150) | User email                       |
| role       | VARCHAR(150) | Lecturer / Coordinator / Manager |
| password   | VARCHAR(150) | Hashed password                  |
| date       | DATE         | Date of registration             |

**Claims Table**

| Column               | Type         | Description                   |
| -------------------- | ------------ | ----------------------------- |
| claimID              | INT          | Primary key                   |
| number_of_sessions   | INT          | Number of sessions worked     |
| number_of_hours      | INT          | Total hours                   |
| amount_of_rate       | INT          | Rate per hour                 |
| module_name          | VARCHAR(150) | Module title                  |
| faculty_name         | VARCHAR(150) | Faculty name                  |
| supporting_documents | VARCHAR(150) | File path                     |
| claim_status         | VARCHAR(100) | Claim status                  |
| creating_date        | DATE         | Submission date               |
| lecturerID           | INT          | Foreign key referencing Users |



## 🚀 How to Run the Application

### 1️⃣ Prerequisites

Ensure you have:

* Visual Studio 2022 or later
* .NET Core SDK 6.0+
* SQL Server LocalDB

### 2️⃣ Run Setup

1. Open the solution in Visual Studio.
2. Run the project — this automatically creates:

   * LocalDB instance (`claim_system`)
   * Database (`claims_database`)
   * Required tables (`Users`, `Claims`)
3. The home page will load the **navigation bar** linking to:

   * Home
   * Submit Claim
   * Track Claim
   * Coordinator Review
   * Manager Approval


## 🧪 Unit Testing

**Framework:** xUnit
**Location:** `ContractMonthlyClaimSystem.Tests/ClaimTests.cs`

Example:

```csharp
[Fact]
public void Calculate_Total_Amount_ShouldBeCorrect()
{
    var claim = new Claim { number_of_hours = 10, amount_of_rate = 200 };
    var total = claim.number_of_hours * claim.amount_of_rate;
    Assert.Equal(2000, total);
}
```

Run via:

```
Test Explorer → Run All Tests
```

✅ Expected: All tests pass successfully.

## 🧭 Version Control (GitHub)

**Repository Name:** `ContractMonthlyClaimSystem`
