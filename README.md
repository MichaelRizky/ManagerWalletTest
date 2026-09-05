# Manager Wallet API

A secure digital wallet API built with **.NET 10**. The application provides JWT authentication, digital wallet management, top-up functionality, and peer-to-peer transfers using database transactions.

Documentation is available in two languages:

- [Bahasa Indonesia](#bahasa-indonesia)
- [English](#english)

---

## Bahasa Indonesia

Wallet API adalah aplikasi berbasis **.NET 10** yang menyediakan fitur:

- 🔐 Autentikasi menggunakan JWT
- 💰 Manajemen dompet digital
- 💳 Top-up saldo
- 💸 Transfer saldo antar pengguna
- 🔄 Transaksi database untuk menjaga konsistensi saldo
- 📖 Swagger UI untuk dokumentasi dan pengujian API

### Prasyarat

Pastikan perangkat Anda telah memiliki:

- .NET 10 SDK
- Microsoft SQL Server
- SQL Server Management Studio (SSMS)
- Entity Framework Core Tools (`dotnet-ef`)

### Konfigurasi Environment

Aplikasi menggunakan file `.env` untuk menyimpan konfigurasi dan secret aplikasi.

Buat file `.env` di direktori root project:

```env
JwtSecretKey=your-secure-jwt-secret-key-minimum-32-characters
DbUserId=your-database-user
DbPassword=your-database-password
```

#### Keterangan

| Variable | Deskripsi |
|---|---|
| `JwtSecretKey` | Kunci rahasia untuk menandatangani dan memverifikasi JWT. Gunakan minimal 32 karakter. |
| `DbUserId` | User ID yang digunakan untuk autentikasi koneksi ke SQL Server. |
| `DbPassword` | Password untuk akun database SQL Server. |

> ⚠️ **Security Notice**
>
> Jangan menggunakan secret key atau password database yang sebenarnya di dalam `README.md`.
>
> Jangan commit file `.env` ke repository. Tambahkan `.env` ke `.gitignore`.

Contoh `.gitignore`:

```gitignore
.env
```

### Instalasi

Clone repository dan masuk ke direktori project:

```bash
git clone <repository-url>
cd <project-directory>
```

Restore dependencies:

```bash
dotnet restore
```

### Database Migration

Pastikan SQL Server sedang berjalan dan konfigurasi database pada `.env` sudah benar.

Kemudian jalankan migration yang tersedia:

```bash
dotnet ef database update
```

Perintah tersebut akan menerapkan migration dan membuat tabel database yang diperlukan secara otomatis.

Jika `dotnet-ef` belum terinstal, jalankan:

```bash
dotnet tool install --global dotnet-ef
```

### Menjalankan Aplikasi

Jalankan aplikasi dengan:

```bash
dotnet run
```

Setelah aplikasi berhasil dijalankan, terminal akan menampilkan URL aplikasi.

Buka Swagger UI melalui:

```text
https://localhost:xxxx/swagger
```

atau:

```text
http://localhost:xxxx/swagger
```

Port `xxxx` mengikuti konfigurasi aplikasi yang ditampilkan pada terminal.

### API Documentation

Swagger UI menyediakan dokumentasi endpoint API sekaligus memungkinkan Anda menguji endpoint secara langsung.

Alur penggunaan API secara umum:

1. Register atau login menggunakan endpoint autentikasi.
2. Dapatkan JWT token.
3. Gunakan token tersebut pada endpoint yang membutuhkan autentikasi.
4. Lakukan top-up untuk menambah saldo.
5. Gunakan fitur transfer untuk mengirim saldo ke pengguna lain.

---

## English

Wallet API is a **.NET 10** application that provides secure digital wallet functionality, including:

- 🔐 JWT authentication
- 💰 Digital wallet management
- 💳 Balance top-up
- 💸 Peer-to-peer transfers
- 🔄 Database transactions for maintaining balance consistency
- 📖 Swagger UI for API documentation and testing

### Prerequisites

Make sure the following software is installed:

- .NET 10 SDK
- Microsoft SQL Server
- SQL Server Management Studio (SSMS)
- Entity Framework Core Tools (`dotnet-ef`)

### Environment Configuration

The application uses a `.env` file to manage application configuration and secrets.

Create a `.env` file in the project root:

```env
JwtSecretKey=your-secure-jwt-secret-key-minimum-32-characters
DbUserId=your-database-user
DbPassword=your-database-password
```

#### Environment Variables

| Variable | Description |
|---|---|
| `JwtSecretKey` | Cryptographic secret key used to sign and verify JWT tokens. Use at least 32 characters. |
| `DbUserId` | User ID used to authenticate the SQL Server database connection. |
| `DbPassword` | Password for the SQL Server database account. |

> ⚠️ **Security Notice**
>
> Never use actual secret keys or database passwords in `README.md`.
>
> Do not commit the `.env` file to the repository. Add `.env` to `.gitignore`.

Example `.gitignore`:

```gitignore
.env
```

### Installation

Clone the repository and navigate to the project directory:

```bash
git clone <repository-url>
cd <project-directory>
```

Restore the project dependencies:

```bash
dotnet restore
```

### Database Migration

Make sure SQL Server is running and the database configuration in `.env` is correct.

Apply the existing database migrations:

```bash
dotnet ef database update
```

This command applies the existing migrations and automatically creates the required database tables.

If `dotnet-ef` is not installed, install it globally:

```bash
dotnet tool install --global dotnet-ef
```

### Running the Application

Start the application with:

```bash
dotnet run
```

Once the application starts successfully, the terminal will display the application URL.

Open Swagger UI at:

```text
https://localhost:xxxx/swagger
```

or:

```text
http://localhost:xxxx/swagger
```

The `xxxx` port depends on the application configuration displayed in the terminal.

### API Documentation

Swagger UI provides interactive API documentation and allows you to test the available endpoints directly.

Typical API usage flow:

1. Register or log in using the authentication endpoint.
2. Obtain a JWT token.
3. Use the token to access authenticated endpoints.
4. Perform a top-up to add funds to the wallet.
5. Use the transfer feature to send funds to another user.

---

## License

This project is for development and testing purposes unless otherwise specified.
