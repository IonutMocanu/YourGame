# Your Game

> An interactive 3D simulator developed in **Unity**, connected to a robust **ASP.NET Core** backend, allowing users to manage a virtual account, purchase cars, and view them in a personalized garage.

![Unity](https://img.shields.io/badge/Client-Unity_2025-black?logo=unity)
![.NET](https://img.shields.io/badge/Backend-.NET_8-purple?logo=dotnet)
![Docker](https://img.shields.io/badge/Deploy-Docker-blue?logo=docker)
![SQLite](https://img.shields.io/badge/Database-SQLite-003B57?logo=sqlite)

## Description

This project represents a **Full-Stack** application applied in the context of video game development. Unlike standard games that save data locally, this simulator implements a real **Client-Server** architecture.

The game logic (Client) is separated from the business logic and data persistence (Server), with communication handled via HTTP requests to a secure **REST API**. Users can register, accumulate virtual currency, and purchase vehicles that remain permanently saved in the database.

## Key Features

### Client (Unity)
* **Authentication System:** Fully functional Login and Register (First Name, Last Name, Email validation).
* **3D Showroom & Garage:** Interactive car visualization (Low Poly style).
* **Reactive UI Interface:** Menus for viewing specifications (Speed, Price, Manufacturing Year).
* **Economic System:** User budget management and asset acquisition.
* **HTTP Networking:** Custom implementation of an `HttpClient` for asynchronous communication with the server.

### Backend (ASP.NET Core API)
* **RESTful Architecture:** Structured endpoints for Users and Cars (CRUD).
* **Data Persistence:** **SQLite** database managed via **Entity Framework Core**.
* **Dockerized:** The backend runs isolated in a Docker container for quick installation.
* **Swagger UI:** Automatic API documentation for rapid testing.

## Technologies Used

| Category | Technologies |
| :--- | :--- |
| **Frontend** | Unity Engine (C#), TextMeshPro (UI), Newtonsoft.Json |
| **Backend** | ASP.NET Core Web API (.NET 8), Entity Framework Core |
| **Database** | SQLite |
| **DevOps** | Docker, Docker Compose |

## Demo & Screenshots

### Video Presentation

https://github.com/user-attachments/assets/aa0bc856-095d-4ab3-99c4-52ba8d8851cd

https://github.com/user-attachments/assets/26e41d23-42d5-4c94-8c46-68dbef493d33

### Gallery

| Login Menu | Garage System | Swagger API |
|:---:|:---:|:---:|
| <img src="https://github.com/IonutMocanu/YourGame/blob/main/Readmephoto/MENU.jpeg" alt="Login Menu" width="1600"/> | <img src="https://github.com/IonutMocanu/YourGame/blob/main/Readmephoto/GARAGE.jpeg" alt="Garage System" width="1600"/> |<img src="https://github.com/IonutMocanu/YourGame/blob/main/Readmephoto/API.jpeg" alt="Swagger API" width="1600"/> |

## Installation and Running

The project is designed to run modularly. Follow the steps below:

## 1. Starting the Server (Backend)
We configured **Docker Compose** to eliminate the need for manually installing databases or complex SDKs.

### 1.1 Clone the repository
```bash
git clone [https://github.com/IonutMocanu/YourGame.git](https://github.com/IonutMocanu/YourGame.git)
```
### 1.2 Navigate to the root folder (where docker-compose.yml is located)
```bash
cd YourGame
```
### 1.3 Start the server
```bash
docker-compose up --build
```
The server will be accessible at: [http://localhost:7106/swagger/index.html](http://localhost:7106/swagger/index.html)

## 2.a Starting the Game (with Unity)
1. Open **Unity Hub**.
2. Click **Add Project** and select the `ProiectIS2` folder.
3. Open the main scene (e.g., `LoginScene`).
4. Ensure the server (Docker) is running.
5. Press **Play**.

## 2.b Starting the Game (without Unity) - Recommended
1. Go to the root folder.
2. Then go to `ProiectIS2` -> `BuildProfiles` -> `ProiectIS2`.

## API Structure

The backend exposes the following main routes:

### User Management
* `GET /api/User/{email}` - Returns the player's profile and their garage.
* `POST /api/User` - Registers a new player.
* `PUT /api/User/add-money` - Updates the financial balance.

### Car Management
* `GET /api/Car` - Returns the car catalog.
* `POST /api/Car/buy/{userId}` - Processes the transaction for purchasing a car.

## Development Team

Project created for the Software Engineering course by:

* [**Mocanu Andrei Ionuț**](https://www.linkedin.com/in/ionu%C8%9B-andrei-mocanu-785bb1258/)

* [**Stanciu Eric Andrei**](https://www.linkedin.com/in/eric-stanciu-5a7497259/)

* [**Durnea Theodora**](https://www.linkedin.com/in/theodora-durnea-122383140/)

---
© 2026 Your Game. All rights reserved.
