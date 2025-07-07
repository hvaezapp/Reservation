#  Reservation 

This is a ** Reservation System ** built with **ASP.NET Core 9**, designed to handle room bookings efficiently and prevent duplicate or conflicting reservations.

The project uses **Vertical Slice Architecture** to achieve better separation of concerns and maintainability.  
To handle concurrency and avoid race conditions, it leverages **Redis** and **RedLock.net** for distributed locking.

---

## ✨ Features

- Vertical Slice Architecture for better scalability and modular design
- Prevents race conditions and duplicate reservations using Redis and RedLock
- Fluent input validation using **FluentValidation**
- Data persistence using **Entity Framework Core** with **SQL Server**
- Clean, extensible structure for handling different reservation scenarios

---

## ⚙️ Tech Stack

- **.NET 9**
- **ASP.NET Core**
- **Entity Framework Core**
- **SQL Server**
- **Redis** (for distributed lock)
- **RedLock.net** (distributed locking library)
- **FluentValidation**

---

## 🚀 Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- SQL Server 
- Redis (use Docker to run redis instance)

---
