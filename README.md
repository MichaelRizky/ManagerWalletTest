# Manager Wallet Project

A full-stack digital wallet application built with a **.NET 10** Backend API and a **Nuxt.js** Frontend. The application provides secure authentication, digital wallet management, top-up functionality, and peer-to-peer transfers.

Documentation is available in two languages:

- [Bahasa Indonesia](#bahasa-indonesia)
- [English](#english)

---

## Bahasa Indonesia

Proyek ini terbagi menjadi dua bagian utama:
1. **Backend**: RESTful API yang dibangun menggunakan .NET 10.
2. **Frontend**: Antarmuka web interaktif yang dibangun menggunakan Nuxt.js.

### 🚀 Backend (API)

Backend aplikasi menyediakan fitur:

- 🔐 Autentikasi menggunakan JWT
- 💰 Manajemen dompet digital
- 💳 Top-up saldo
- 💸 Transfer saldo antar pengguna
- 🔄 Transaksi database untuk menjaga konsistensi saldo
- 📖 Swagger UI untuk dokumentasi dan pengujian API

#### Prasyarat Backend

Pastikan perangkat Anda telah memiliki:
- .NET 10 SDK
- Microsoft SQL Server
- SQL Server Management Studio (SSMS)
- Entity Framework Core Tools (`dotnet-ef`)

#### Konfigurasi Environment Backend

Aplikasi menggunakan file `.env` untuk menyimpan konfigurasi dan secret aplikasi.
Buat file `.env` di direktori `Backend`:

```env
JwtSecretKey=your-secure-jwt-secret-key-minimum-32-characters
DbUserId=your-database-user
DbPassword=your-database-password
```

| Variable | Deskripsi |
|---|---|
| `JwtSecretKey` | Kunci rahasia untuk menandatangani dan memverifikasi JWT. Gunakan minimal 32 karakter. |
| `DbUserId` | User ID yang digunakan untuk autentikasi koneksi ke SQL Server. |
| `DbPassword` | Password untuk akun database SQL Server. |

> ⚠️ **Security Notice**: Jangan commit file `.env` ke repository.

#### Instalasi & Menjalankan Backend

1. Buka terminal dan masuk ke direktori Backend:
   ```bash
   cd Backend
   ```
2. Restore dependencies:
   ```bash
   dotnet restore
   ```
3. Lakukan Database Migration (pastikan `.env` dan SQL Server siap):
   ```bash
   dotnet ef database update
   ```
   *(Jika `dotnet-ef` belum terinstal, jalankan `dotnet tool install --global dotnet-ef`)*
4. Jalankan aplikasi:
   ```bash
   dotnet run
   ```
5. Akses Swagger UI untuk menguji API di URL yang muncul di terminal (contoh: `https://localhost:xxxx/swagger`).

---

### 🖥️ Frontend (Web)

Frontend aplikasi merupakan antarmuka pengguna yang dibangun menggunakan **Nuxt.js** yang terhubung dengan API Backend.

#### Prasyarat Frontend
- Node.js (versi 18 atau terbaru disarankan)
- npm, yarn, pnpm, atau bun

#### Instalasi & Menjalankan Frontend

1. Buka terminal dan masuk ke direktori Frontend:
   ```bash
   cd Frontend
   ```
2. Instal dependencies:
   ```bash
   npm install
   ```
   *(Atau gunakan package manager lain: `yarn install`, `pnpm install`, `bun install`)*
3. Jalankan development server:
   ```bash
   npm run dev
   ```
4. Buka browser dan akses aplikasi melalui URL yang ditampilkan di terminal (biasanya `http://localhost:3000`).

---

## English

This project is divided into two main parts:
1. **Backend**: A RESTful API built with .NET 10.
2. **Frontend**: An interactive web interface built with Nuxt.js.

### 🚀 Backend (API)

The Backend provides secure digital wallet functionality, including:

- 🔐 JWT authentication
- 💰 Digital wallet management
- 💳 Balance top-up
- 💸 Peer-to-peer transfers
- 🔄 Database transactions for maintaining balance consistency
- 📖 Swagger UI for API documentation and testing

#### Backend Prerequisites

Make sure the following software is installed:
- .NET 10 SDK
- Microsoft SQL Server
- SQL Server Management Studio (SSMS)
- Entity Framework Core Tools (`dotnet-ef`)

#### Backend Environment Configuration

The application uses a `.env` file to manage application configuration and secrets.
Create a `.env` file in the `Backend` directory:

```env
JwtSecretKey=your-secure-jwt-secret-key-minimum-32-characters
DbUserId=your-database-user
DbPassword=your-database-password
```

| Variable | Description |
|---|---|
| `JwtSecretKey` | Cryptographic secret key used to sign and verify JWT tokens. Use at least 32 characters. |
| `DbUserId` | User ID used to authenticate the SQL Server database connection. |
| `DbPassword` | Password for the SQL Server database account. |

> ⚠️ **Security Notice**: Do not commit the `.env` file to the repository.

#### Backend Installation & Running

1. Open your terminal and navigate to the Backend directory:
   ```bash
   cd Backend
   ```
2. Restore project dependencies:
   ```bash
   dotnet restore
   ```
3. Apply Database Migrations (ensure `.env` and SQL Server are ready):
   ```bash
   dotnet ef database update
   ```
   *(If `dotnet-ef` is not installed, install it globally: `dotnet tool install --global dotnet-ef`)*
4. Run the application:
   ```bash
   dotnet run
   ```
5. Access Swagger UI to test the API at the URL displayed in your terminal (e.g., `https://localhost:xxxx/swagger`).

---

### 🖥️ Frontend (Web)

The Frontend is the user interface built with **Nuxt.js** that communicates with the Backend API.

#### Frontend Prerequisites
- Node.js (version 18 or latest recommended)
- npm, yarn, pnpm, or bun

#### Frontend Installation & Running

1. Open your terminal and navigate to the Frontend directory:
   ```bash
   cd Frontend
   ```
2. Install dependencies:
   ```bash
   npm install
   ```
   *(Or use your preferred package manager: `yarn install`, `pnpm install`, `bun install`)*
3. Start the development server:
   ```bash
   npm run dev
   ```
4. Open your browser and navigate to the displayed URL (usually `http://localhost:3000`).

---

## License

This project is for development and testing purposes unless otherwise specified.
