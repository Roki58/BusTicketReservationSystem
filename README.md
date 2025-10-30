# 🚌 Bus Ticket Reservation System

<div align="center">

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet)
![Angular](https://img.shields.io/badge/Angular-19-DD0031?style=for-the-badge&logo=angular)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16-4169E1?style=for-the-badge&logo=postgresql)
![License](https://img.shields.io/badge/License-MIT-green?style=for-the-badge)

**A full-stack bus ticket booking system built with Clean Architecture and DDD principles**

[Features](#features) • [Demo](#demo) • [Installation](#installation) • [Usage](#usage) • [Architecture](#architecture) • [Testing](#testing)

</div>

---

## 📋 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Architecture](#architecture)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Configuration](#configuration)
- [Running the Application](#running-the-application)
- [API Documentation](#api-documentation)
- [Testing](#testing)
- [Project Structure](#project-structure)
- [Screenshots](#screenshots)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)

---

## 🎯 Overview

The **Bus Ticket Reservation System** is a modern, full-stack web application that enables users to search for available buses, view seat layouts, and book tickets seamlessly. Built following industry best practices with Clean Architecture and Domain-Driven Design (DDD), this system demonstrates professional-grade software development.

### 🎓 Academic Project
This project was developed as part of the **Internship Batch-3 Full-stack (.NET) Developer Assignment** for Wafi Solution, showcasing expertise in:
- Clean Architecture & DDD principles
- RESTful API design
- Responsive UI/UX
- Comprehensive unit testing
- Modern development practices

---

## ✨ Features

### 🔍 **Bus Search**
- Search buses by origin, destination, and date
- Real-time seat availability
- Multiple bus operators
- Price comparison
- Journey duration display

### 🪑 **Seat Selection**
- Interactive seat layout
- Color-coded seat status (Available, Booked, Sold)
- Real-time seat updates
- Multiple boarding/dropping points
- Visual seat map

### 📝 **Booking Management**
- Easy passenger details entry
- Mobile number validation
- Instant booking confirmation
- Unique booking reference generation
- Booking history

### 🎨 **User Experience**
- Responsive design (Mobile, Tablet, Desktop)
- Intuitive navigation
- Real-time feedback
- Loading indicators
- Error handling with user-friendly messages

---

## 🛠 Tech Stack

### Backend
| Technology | Version | Purpose |
|------------|---------|---------|
| .NET | 9.0 | Framework |
| C# | 12.0 | Programming Language |
| Entity Framework Core | 9.0.0 | ORM |
| PostgreSQL | 16+ | Database |
| xUnit | 2.9.0 | Testing Framework |
| Moq | 4.20.70 | Mocking Library |
| Swagger | 6.8.0 | API Documentation |

### Frontend
| Technology | Version | Purpose |
|------------|---------|---------|
| Angular | 19.0.0 | Frontend Framework |
| TypeScript | 5.6.2 | Programming Language |
| Bootstrap | 5.3.2 | UI Framework |
| RxJS | 7.8.0 | Reactive Programming |

### Development Tools
- **Visual Studio 2022** - Backend IDE
- **VS Code** - Frontend IDE
- **pgAdmin** - Database Management
- **Swagger** - API Testing

---

## 🏗 Architecture

### Clean Architecture Layers

```
┌─────────────────────────────────────────────────────────┐
│                      Presentation                        │
│                  (WebApi + Angular)                      │
├─────────────────────────────────────────────────────────┤
│                      Application                         │
│              (Business Logic + Use Cases)                │
├─────────────────────────────────────────────────────────┤
│                 Application.Contracts                    │
│                  (DTOs + Interfaces)                     │
├─────────────────────────────────────────────────────────┤
│                    Infrastructure                        │
│           (EF Core + Repository Implementations)         │
├─────────────────────────────────────────────────────────┤
│                        Domain                            │
│         (Entities + Value Objects + Services)            │
└─────────────────────────────────────────────────────────┘
```

### Design Patterns Used
- ✅ **Repository Pattern** - Data access abstraction
- ✅ **Dependency Injection** - Loose coupling
- ✅ **CQRS** - Separation of concerns
- ✅ **Domain Services** - Business logic encapsulation
- ✅ **DTO Pattern** - Data transfer optimization
- ✅ **Unit of Work** - Transaction management

---

## 📦 Prerequisites

Before you begin, ensure you have the following installed:

### Required Software

| Software | Version | Download Link |
|----------|---------|---------------|
| .NET SDK | 9.0+ | [Download](https://dotnet.microsoft.com/download/dotnet/9.0) |
| Node.js | 20+ | [Download](https://nodejs.org/) |
| PostgreSQL | 16+ | [Download](https://www.postgresql.org/download/) |
| Angular CLI | 19+ | `npm install -g @angular/cli` |

### Verify Installation

```bash
# Check .NET version
dotnet --version
# Expected: 9.0.x

# Check Node.js version
node --version
# Expected: v20.x.x

# Check Angular CLI
ng version
# Expected: Angular CLI 19.x.x

# Check PostgreSQL
psql --version
# Expected: psql (PostgreSQL) 16.x
```

---

## 🚀 Installation

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/bus-ticket-reservation.git
cd bus-ticket-reservation
```

### 2. Backend Setup

```bash
# Restore NuGet packages
dotnet restore

# Navigate to WebApi project
cd src/BusTicketReservation.WebApi

# Install Entity Framework tools (if not already installed)
dotnet tool install --global dotnet-ef
```

### 3. Frontend Setup

```bash
# Navigate to Angular app
cd ClientApp

# Install npm packages
npm install
```

---

## ⚙️ Configuration

### 1. Database Configuration

Create PostgreSQL database:

```sql
CREATE DATABASE BusTicketDB;
```

### 2. Update Connection String

Edit `src/BusTicketReservation.WebApi/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=BusTicketDB;Username=postgres;Password=YOUR_PASSWORD"
  }
}
```

### 3. Apply Database Migrations

```bash
cd src/BusTicketReservation.WebApi

# Create migration
dotnet ef migrations add InitialCreate --project ../BusTicketReservation.Infrastructure

# Update database
dotnet ef database update
```

### 4. Seed Sample Data

Sample data will be automatically seeded when you run the application for the first time. This includes:
- 4 Bus operators (Green Line, Shyamoli, Ena Transport, Hanif)
- 2 Routes (Dhaka-Chittagong, Dhaka-Sylhet)
- 5 Bus schedules with different timings
- 40 seats per bus (automatically generated)

---

## 🏃 Running the Application

### Development Mode

#### Start Backend (Terminal 1)

```bash
cd src/BusTicketReservation.WebApi
dotnet run

# Or with auto-reload
dotnet watch run
```

Backend will start at:
- **HTTPS:** https://localhost:7001
- **HTTP:** http://localhost:5001
- **Swagger:** https://localhost:7001/swagger

#### Start Frontend (Terminal 2)

```bash
cd ClientApp
ng serve

# Or with auto-open
ng serve --open
```

Frontend will start at:
- **App:** http://localhost:4200

### Production Build

#### Backend

```bash
cd src/BusTicketReservation.WebApi
dotnet publish -c Release -o ./publish
```

#### Frontend

```bash
cd ClientApp
ng build --configuration production
```

Output will be in `dist/client-app/`

---

## 📚 API Documentation

### Swagger UI

When the backend is running, access interactive API documentation at:
- https://localhost:7001/swagger

### API Endpoints

#### Search Controller

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/search` | Search available buses |

**Query Parameters:**
- `from` (string, required) - Origin city
- `to` (string, required) - Destination city
- `date` (DateTime, required) - Journey date

**Example:**
```bash
GET /api/search?from=Dhaka&to=Chittagong&date=2025-10-29
```

#### Booking Controller

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/booking/seat-plan/{scheduleId}` | Get seat layout |
| POST | `/api/booking/book-seat` | Book a seat |

**Book Seat Request Body:**
```json
{
  "busScheduleId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "seatId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "passengerName": "Roki",
  "mobileNumber": "01712345678",
  "boardingPoint": "Dhaka",
  "droppingPoint": "Chittagong"
}
```

---

## 🧪 Testing

### Run Unit Tests

```bash
cd src/BusTicketReservation.Tests
dotnet test

# With detailed output
dotnet test --logger "console;verbosity=detailed"

# With coverage
dotnet test /p:CollectCoverage=true
```
### Unit Test Result :<img width="950" height="538" alt="Test" src="https://github.com/user-attachments/assets/ad324c57-bc41-41a0-9198-a0e268adeb15" />


### Test Coverage

The project includes comprehensive unit tests for:
- ✅ Search Service (bus availability, seat calculation)
- ✅ Booking Service (seat booking, validation)
- ✅ Domain Services (seat state transitions)

**Test Results:**
```
Test Run Successful.
Total tests: 12
     Passed: 12
 Total time: 2.5s
```

### Manual Testing

#### Using Swagger
1. Navigate to (https://localhost:7032/swagger/index.html)
2. Try the `/api/search` endpoint
3. Copy a `busScheduleId` from the response
4. Use it to get seat plan
5. Book a seat using the `/api/booking/book-seat` endpoint
 
#### Using Frontend Link : http://localhost:4200/  

#### Using Postman

Import the Postman collection (available in `/docs` folder) or manually test:

**Search Buses:**
```bash
curl -X GET "https://localhost:7001/api/search?from=Dhaka&to=Chittagong&date=2025-10-29"
```

**Get Seat Plan:**
```bash
curl -X GET "https://localhost:7001/api/booking/seat-plan/{scheduleId}"
```

**Book Seat:**
```bash
curl -X POST "https://localhost:7001/api/booking/book-seat" \
  -H "Content-Type: application/json" \
  -d '{
    "busScheduleId": "guid-here",
    "seatId": "guid-here",
    "passengerName": "John Doe",
    "mobileNumber": "01712345678",
    "boardingPoint": "Kallayanpur",
    "droppingPoint": "GEC Circle"
  }'
```

---

## 📁 Project Structure

```
BusTicketReservation/
│
├── src/
│   ├── BusTicketReservation.Domain/              # Domain layer
│   │   ├── Entities/                             # Domain entities
│   │   │   ├── Bus.cs
│   │   │   ├── Route.cs
│   │   │   ├── BusSchedule.cs
│   │   │   ├── Seat.cs
│   │   │   ├── Passenger.cs
│   │   │   └── Ticket.cs
│   │   ├── ValueObjects/                         # Value objects
│   │   │   └── SeatStatus.cs
│   │   └── Services/                             # Domain services
│   │       └── ISeatDomainService.cs
│   │
│   ├── BusTicketReservation.Application.Contracts/ # Application contracts
│   │   ├── DTOs/                                 # Data transfer objects
│   │   │   ├── AvailableBusDto.cs
│   │   │   ├── SeatDto.cs
│   │   │   ├── SeatPlanDto.cs
│   │   │   ├── BookSeatInputDto.cs
│   │   │   └── BookSeatResultDto.cs
│   │   └── Interfaces/                           # Service & repository interfaces
│   │       ├── ISearchService.cs
│   │       ├── IBookingService.cs
│   │       └── Repositories/
│   │
│   ├── BusTicketReservation.Application/         # Application layer
│   │   └── Services/                             # Application services
│   │       ├── SearchService.cs
│   │       ├── BookingService.cs
│   │       └── SeatDomainService.cs
│   │
│   ├── BusTicketReservation.Infrastructure/      # Infrastructure layer
│   │   ├── Data/                                 # Database context
│   │   │   └── BusTicketDbContext.cs
│   │   └── Repositories/                         # Repository implementations
│   │       ├── BusRepository.cs
│   │       ├── RouteRepository.cs
│   │       ├── BusScheduleRepository.cs
│   │       ├── SeatRepository.cs
│   │       ├── PassengerRepository.cs
│   │       └── TicketRepository.cs
│   │
│   ├── BusTicketReservation.WebApi/              # Presentation layer
│   │   ├── Controllers/                          # API controllers
│   │   │   ├── SearchController.cs
│   │   │   └── BookingController.cs
│   │   ├── Program.cs                            # Application entry point
│   │   └── appsettings.json                      # Configuration
│   │
│   └── BusTicketReservation.Tests/               # Test project
│       └── Services/                             # Unit tests
│           ├── SearchServiceTests.cs
│           ├── BookingServiceTests.cs
│           └── SeatDomainServiceTests.cs
│
├── ClientApp/                                     # Angular frontend
│   ├── src/
│   │   ├── app/
│   │   │   ├── components/                       # UI components
│   │   │   │   ├── search-buses/
│   │   │   │   ├── seat-layout/
│   │   │   │   └── booking-confirmation/
│   │   │   ├── models/                           # TypeScript models
│   │   │   │   ├── available-bus.model.ts
│   │   │   │   ├── seat.model.ts
│   │   │   │   └── booking.model.ts
│   │   │   ├── services/                         # HTTP services
│   │   │   │   ├── bus.service.ts
│   │   │   │   └── booking.service.ts
│   │   │   ├── app.component.ts                  # Root component
│   │   │   ├── app.config.ts                     # App configuration
│   │   │   └── app.routes.ts                     # Routing configuration
│   │   ├── environments/                         # Environment configs
│   │   ├── styles.css                            # Global styles
│   │   └── index.html                            # HTML entry point
│   ├── angular.json                              # Angular configuration
│   └── package.json                              # NPM dependencies
│
├── BusTicketReservation.sln                      # Solution file
└── README.md                                     # This file
```

---

## 📸 Screenshots

### 🔍 Bus Search Page
![Search Page](https://github.com/Roki58/BusTicketReservationSystem/blob/96986c0792f4c8e8aebff7c044fdf1beb5457ab1/Search-Page.PNG)
*Search for available buses by selecting origin, destination, and travel date*

### 📋 Available Buses List
![Bus List](https://github.com/Roki58/BusTicketReservationSystem/blob/b539faad49efc0f3120ea94d830b7535fc547077/Avaialable%20bus.PNG)
*View all available buses with real-time seat availability and pricing*

### 🪑 Seat Selection
![Seat Layout](https://github.com/Roki58/BusTicketReservationSystem/blob/842ee244f81c3504b6af5eeec84e4e6f4dd7858d/seat-selection.PNG)
*Interactive seat layout with color-coded availability status*

### 📝 Booking Form
![Booking Form](https://github.com/Roki58/BusTicketReservationSystem/blob/842ee244f81c3504b6af5eeec84e4e6f4dd7858d/Seatbook.PNG)
*Enter passenger details and select boarding/dropping points*

### ✅ Booking Confirmation
![Confirmation](https://github.com/Roki58/BusTicketReservationSystem/blob/842ee244f81c3504b6af5eeec84e4e6f4dd7858d/psenger.PNG)
*Get instant confirmation with unique booking reference*

---

## 🗄️ Database Schema

### Entity Relationship Diagram

```
┌─────────────┐         ┌──────────────┐         ┌────────────┐
│    Buses    │────1:N──│ BusSchedules │────N:1──│   Routes   │
└─────────────┘         └──────────────┘         └────────────┘
      │                        │
      │ 1:N                    │ 1:N
      │                        │
┌─────────────┐         ┌──────────────┐
│    Seats    │         │   Tickets    │
└─────────────┘         └──────────────┘
      │                        │
      └────────N:1────┬────────┘
                      │
                      │ N:1
               ┌──────────────┐
               │  Passengers  │
               └──────────────┘
```

### Tables

| Table | Description | Key Fields |
|-------|-------------|------------|
| Buses | Bus information | CompanyName, BusName, TotalSeats |
| Routes | Route details | FromCity, ToCity, BoardingPoints |
| BusSchedules | Schedule information | JourneyDate, DepartureTime, Price |
| Seats | Seat layout | SeatNumber, RowNumber, SeatType |
| Passengers | Passenger details | Name, MobileNumber |
| Tickets | Booking records | BookingReference, Status, Price |

---

## 🎓 Learning Outcomes

This project demonstrates proficiency in:

### Backend Development
- ✅ Clean Architecture implementation
- ✅ Domain-Driven Design (DDD)
- ✅ Repository Pattern
- ✅ Dependency Injection
- ✅ Entity Framework Core
- ✅ RESTful API design
- ✅ Unit Testing with xUnit & Moq
- ✅ Async/Await programming
- ✅ Transaction management

### Frontend Development
- ✅ Angular 19 framework
- ✅ Component-based architecture
- ✅ Reactive programming with RxJS
- ✅ HTTP client services
- ✅ Routing and navigation
- ✅ Responsive design
- ✅ Bootstrap integration

### Software Engineering
- ✅ SOLID principles
- ✅ Separation of concerns
- ✅ Code organization
- ✅ Error handling
- ✅ Documentation
- ✅ Git version control

---

## 🚧 Future Enhancements

### Phase 2 Features
- [ ] User authentication & authorization
- [ ] Payment gateway integration
- [ ] Email/SMS notifications
- [ ] Booking cancellation
- [ ] Admin dashboard
- [ ] Real-time bus tracking
- [ ] Review & rating system
- [ ] Multi-language support

### Technical Improvements
- [ ] Redis caching
- [ ] SignalR for real-time updates
- [ ] Docker containerization
- [ ] CI/CD pipeline
- [ ] Performance optimization
- [ ] Load testing
- [ ] Security enhancements

---

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Coding Standards
- Follow C# coding conventions
- Use meaningful variable names
- Write unit tests for new features
- Update documentation

---

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

```
MIT License

Copyright (c) 2025 Bus Ticket Reservation System

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
```

---

## 👨‍💻 Author

**Your Name**
- GitHub: [Roki](https://github.com/Roki58)
- LinkedIn: [Ahosanul Roki](https://www.linkedin.com/in/ahosanul-hasan-roki-51b3101b7)
- Email: ahosanulroki@gmail.com.com

---

## 🙏 Acknowledgments

- **Wafi Solution** - For providing the internship opportunity
- **Microsoft** - For .NET and documentation
- **Angular Team** - For the amazing framework
- **PostgreSQL** - For the robust database
- **Bootstrap** - For the UI components
- **Open Source Community** - For invaluable resources

