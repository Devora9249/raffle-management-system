# Raffle Management System

A full-stack web application for managing online raffles (Chinese auctions), including administration, donors, users, and business workflows.

## Overview
The system enables organizations to manage online raffles end-to-end, with separate interfaces for administrators, donors, and regular users.  
It focuses on clean architecture, clear separation of concerns, and maintainable code.

## Tech Stack
### Frontend
- Angular
- TypeScript
- HTML, CSS

### Backend
- ASP.NET Core (C#)
- RESTful API
- Layered Architecture

### Database
- SQL Server
- Entity Framework Core

## Features
- Admin management panel
- User and donor interfaces
- Role-based authorization
- Raffle and donation management
- Business logic handled on the server side
- Secure RESTful communication between client and server

## Architecture
- Layered architecture (Controllers, Services, Repositories)
- Clean code principles
- Separation between Frontend and Backend
- Scalable and maintainable structure

## Authentication Flow
- Users authenticate via a login endpoint
- Upon successful authentication, the server issues a JWT token
- The token is stored on the client side
- Protected API endpoints validate the token
- Access is controlled using role-based authorization (Admin, Donor, User)

## Prerequisites
- Node.js
- Angular CLI
- .NET SDK
- SQL Server

### Run Frontend
```text
cd client
npm install
ng serve
```

### Run Backend
```text
cd server
dotnet restore
dotnet run
```

