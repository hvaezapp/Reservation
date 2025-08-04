#  Reservation 

This is a **Reservation System** built with **ASP.NET Core 9**, designed to handle room bookings efficiently and prevent duplicate or conflicting reservations.

The project uses **Vertical Slice Architecture** to achieve better separation of concerns and maintainability.  
To handle concurrency and avoid race conditions, it leverages **Redis** and **RedLock.net** for distributed locking to handle concurrency and prevent double booking scenario.  
It also implements the **Outbox Pattern** to ensure reliable and consistent delivery of notification messages to **RabbitMQ**, enabling eventual consistency in a distributed environment.

---

## ✨ Features

- Vertical Slice Architecture for better scalability and modular design  
- Prevents race conditions and duplicate reservations using Redis and RedLock  
- Reliable message delivery using the **Outbox Pattern** with **RabbitMQ**  
- Fluent input validation using **FluentValidation**  
- Data persistence using **Entity Framework Core** with **SQL Server**  
- Clean, extensible structure for handling different reservation scenarios

---

## ⚙️ Tech Stack

- **.NET 9**
- **ASP.NET Core**
- **Entity Framework Core**
- **SQL Server**
- **Redis** (for distributed locking)
- **RedLock.net**
- **RabbitMQ** (as message broker)
- **Outbox Pattern** (for reliable event/message delivery)
- **Background Worker** (to dispatch Outbox messages to RabbitMQ)
- **FluentValidation**

---

## 🚀 Getting Started

### Prerequisites

- .NET 9  
- SQL Server  
- Redis (use Docker to run Redis instance)  
- RabbitMQ (can also be run via Docker)

---
