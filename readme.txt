How to Set It Up
Download the folder TradingApp_Win64 and open it.

Run Run.bat.

A new webpage should open at: http://localhost:5000/swagger/index.html.

If it doesn't open automatically, copy and paste the address into your browser.

A console window will also open, displaying all logs.



Used Exchange
This app connects to ByBit for the following tasks:
Order book subscription
Order placement
Trade execution



System Architecture
The app runs on Microsoft .NET 9 and uses:
EntityFramework.InMemory for in-memory storage simulation
ASP.NET for the web application
It follows a modular monolith architecture structured as follows:

BootStrapper
  └── Bootstrapper -> Entry point of the app (ASP.NET WebApp)

Modules
  ├── MarketData       -> Connects to ByBit's order book WebSocket feed
  ├── Orders           -> Places orders and handles trade executions via ByBit API
  ├── Risk             -> Runs pre-trade risk constraint checks
  └── StateTracking    -> Tracks positions, order rejections, trades, PnL, etc.

Shared
  ├── SharedAbstraction     -> Shared interfaces for inter-module communication
  └── SharedInfrastructure  -> Infrastructure implementations of the abstractions
Each module aims to follow DDD (Domain-Driven Design) and Clean Architecture principles, and is composed of 4–5 projects:

API: Entry point of the module
Application: Application services and event handlers
Domain: Pure domain model code, no infrastructure dependencies
Infrastructure: Repositories and infrastructure service implementations
Test: (Optional) Example test project — I usually only unit test the Domain layer

For more information about this architecture approach, see:
Microsoft DDD Microservices Architecture Guide



Production Readiness
⚠ This version is not meant for production use.
It uses an in-memory database, which is wiped clean on every restart.
Due to time constraints (less than 4 days), some compromises were made in design and robustness in favor of rapid development.
That said, the app includes a Dockerfile and can be deployed as a container.
For production, you can host it using container-based services such as:Azure App Services, AWS Elastic Beanstalk


