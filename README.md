# 💳 ebanx-assignment
C# / .NET – Ebanx Assignment

---

## 🧱 App Architecture

### 📦 Application Project
Contains all the business rules and orchestrates their execution:
- `BankAccountRepository`
- `BankAccountService`
- `EventService`
- `DTOs`

### 🧠 Domain Project
Contains all domain entities and domain-specific logic:
- `BankAccount`
- `Event`
- `Exceptions`

### 🌐 WebApi Project
The interface exposed to clients to consume the data:
- Controllers:
    - `BankAccountsController`
    - `EventsController`

---

## 🚀 How to Start the Web API

1. Make sure **Docker** is installed on your machine.
2. Run the following command:
   ```bash
   docker compose up -d
3. Open: http://localhost:8080
