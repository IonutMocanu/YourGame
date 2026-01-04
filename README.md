# Your Game

> Un simulator 3D interactiv dezvoltat în **Unity**, conectat la un backend robust **ASP.NET Core**, care permite utilizatorilor să gestioneze un cont virtual, să achiziționeze mașini și să le vizualizeze într-un garaj personalizat.

![Unity](https://img.shields.io/badge/Client-Unity_2025-black?logo=unity)
![.NET](https://img.shields.io/badge/Backend-.NET_8-purple?logo=dotnet)
![Docker](https://img.shields.io/badge/Deploy-Docker-blue?logo=docker)
![SQLite](https://img.shields.io/badge/Database-SQLite-003B57?logo=sqlite)

## Descriere

Acest proiect reprezintă o aplicație **Full-Stack** aplicată în contextul dezvoltării de jocuri video. Spre deosebire de jocurile standard care salvează datele local, acest simulator implementează o arhitectură **Client-Server** reală.

Logica jocului (Client) este separată de logica de business și persistența datelor (Server), comunicarea realizându-se prin cereri HTTP către un **REST API** securizat. Utilizatorii se pot înregistra, pot acumula bani virtuali și pot cumpăra vehicule care rămân salvate permanent în baza de date.

## Funcționalități Principale

### Client (Unity)
* **Sistem de Autentificare:** Login și Register complet funcționale (validare Nume, Prenume, Email).
* **Showroom & Garaj 3D:** Vizualizare interactivă a mașinilor (stil Low Poly).
* **Interfață UI Reactivă:** Meniuri pentru vizualizare specificații (Viteză, Preț, An Fabricație).
* **Sistem Economic:** Gestionarea bugetului utilizatorului și achiziția de bunuri.
* **HTTP Networking:** Implementare custom a unui `HttpClient` pentru comunicarea asincronă cu serverul.

### Backend (ASP.NET Core API)
* **RESTful Architecture:** Endpoints structurate pentru Useri și Mașini (CRUD).
* **Persistență Date:** Bază de date **SQLite** gestionată prin **Entity Framework Core**.
* **Dockerized:** Backend-ul rulează izolat într-un container Docker pentru o instalare rapidă.
* **Swagger UI:** Documentație automată a API-ului pentru testare rapidă.

## Tehnologii Utilizate

| Categorie | Tehnologii |
| :--- | :--- |
| **Frontend** | Unity Engine (C#), TextMeshPro (UI), Newtonsoft.Json |
| **Backend** | ASP.NET Core Web API (.NET 8), Entity Framework Core |
| **Database** | SQLite |
| **DevOps** | Docker, Docker Compose |

## Demo & Screenshots

### Prezentare Video
*(Adaugă aici link-ul către videoclipul de pe YouTube, ex: [Vizualizează Demo](https://youtube.com/...))*
<video width="320" height="240" controls>
  <source src="https://github.com/IonutMocanu/YourGame/blob/main/Readmephoto/Joc.mp4" type="video/mp4">
Your browser does not support the video tag.
</video>

### Galerie


| Login Menu | Garage System | Swagger API |
|:---:|:---:|:---:|
| <img src="https://github.com/IonutMocanu/YourGame/blob/main/Readmephoto/MENU.jpeg" alt="isolated" width="1600"/> | <img src="https://github.com/IonutMocanu/YourGame/blob/main/Readmephoto/GARAGE.jpeg" alt="isolated" width="1600"/> |<img src="https://github.com/IonutMocanu/YourGame/blob/main/Readmephoto/API.jpeg" alt="isolated" width="1600"/> |


## Instalare și Rulare

Proiectul este gândit să ruleze modular. Urmează pașii de mai jos:

## 1. Pornirea Serverului (Backend)
Am configurat **Docker Compose** pentru a elimina nevoia de a instala baze de date sau SDK-uri complexe manual.

### 1.1 Clonează repository-ul
```bash
git clone https://github.com/IonutMocanu/YourGame.git
```
### 1.2 Navighează în folderul rădăcină (unde este docker-compose.yml)
```bash
cd YourGame
```
### 1.3 Pornește serverul
```bash
docker-compose up --build
```
Serverul va fi accesibil la: [http://localhost:7106/swagger/index.html](http://localhost:7106/swagger/index.html)

## 2.a Pornirea Jocului (cu Unity)
1. Deschide **Unity Hub**.
2. Apasă **Add Project** și selectează folderul `ProiectIS2`.
3. Deschide scena principală (ex: `LoginScene`).
4. Asigură-te că serverul (Docker) rulează.
5. Apasă **Play** ▶️.

## 2.b Pornirea Jocului (fără Unity) - recomandată
1. Mergi în folderul rădăcinp.
2. Apoi ProiectIS2->BuildProfiles->ProiectIS2.

## Structura API

Backend-ul expune următoarele rute principale:

### User Management
* `GET /api/User/{email}` - Returnează profilul jucătorului și garajul acestuia.
* `POST /api/User` - Înregistrează un jucător nou.
* `PUT /api/User/add-money` - Actualizează balanța financiară.

### Car Management
* `GET /api/Car` - Returnează catalogul de mașini.
* `POST /api/Car/buy/{userId}` - Procesează tranzacția de cumpărare a unei mașini.

## Echipa de Dezvoltare

Proiect realizat în cadrul cursului de Inginerie Software de către:

* [**Mocanu Andrei Ionuț**](https://www.linkedin.com/in/ionu%C8%9B-andrei-mocanu-785bb1258/)
* [**Stanciu Eric Andrei**](https://www.linkedin.com/in/eric-stanciu-5a7497259/)
* [**Durnea Theodora**](https://www.linkedin.com/in/theodora-durnea-122383140/)

---
© 2026 Your Game. Toate drepturile rezervate.
