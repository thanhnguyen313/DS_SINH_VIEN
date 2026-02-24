# Quản Lý Danh Sách Sinh Viên

Ứng dụng desktop quản lý danh sách sinh viên, xây dựng bằng **WPF (.NET 8)** kết nối **SQL Server Express**.

> **Môn học:** SE104 – Nhập môn Công nghệ Phần mềm

---

## Tính năng

- Xem danh sách sinh viên (MSSV, Họ tên, Chương trình đào tạo)
- Thêm sinh viên mới với validate dữ liệu
- Phân loại theo Chương trình Đào tạo (CTC, CLC, CNTT, KTPM)
- Giao diện hiện đại với sidebar gradient, hover effect

---

## Công nghệ

| Thành phần | Công nghệ |
|------------|-----------|
| Framework  | .NET 8 – WPF |
| Database   | SQL Server Express |
| Data Access | Microsoft.Data.SqlClient 5.2.2 |
| Ngôn ngữ  | C#, XAML, SQL |

---

## Cấu trúc dự án

```
DS_SINH_VIEN/
├── App.xaml / App.xaml.cs          # Entry point
├── MainWindow.xaml / .xaml.cs      # Giao diện chính + xử lý sự kiện
├── DatabaseHelper.cs               # Kết nối SQL Server, truy vấn dữ liệu
├── Models/
│   ├── CTDT.cs                     # Model: Chương trình Đào tạo
│   └── SinhVien.cs                 # Model: Sinh viên
├── database.sql                    # Script tạo database + dữ liệu mẫu
└── DS_SINH_VIEN.csproj             # Project file
```

---

## Cài đặt & Chạy

### Yêu cầu

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- SQL Server Management Studio (SSMS) – khuyến nghị

### Bước 1: Tạo Database

1. Mở **SSMS**, kết nối đến `localhost\SQLEXPRESS`
2. Mở file `database.sql`
3. Nhấn **Execute** (F5) để tạo database và dữ liệu mẫu

### Bước 2: Kiểm tra Connection String

Mở file `DatabaseHelper.cs`, kiểm tra dòng:

```csharp
private const string ConnectionString =
    @"Server=localhost\SQLEXPRESS; Database=DS_SINH_VIEN; Trusted_Connection=True; TrustServerCertificate=True;";
```

- Nếu dùng **Windows Authentication** (mặc định): giữ nguyên
- Nếu dùng **SQL Authentication**: đổi thành:
  ```
  Server=localhost\SQLEXPRESS; Database=DS_SINH_VIEN; User Id=sa; Password=<mật khẩu>; TrustServerCertificate=True;
  ```

### Bước 3: Build & Run

```bash
dotnet restore
dotnet build
dotnet run
```

Hoặc mở `DS_SINH_VIEN.sln` bằng **Visual Studio** rồi nhấn **F5**.

---

## Database

### Bảng CTDT

| Cột     | Kiểu          | Mô tả                      |
|---------|---------------|-----------------------------|
| MaCTDT  | INT (PK, AI)  | Mã chương trình đào tạo    |
| TenCTDT | NVARCHAR(100)  | Tên chương trình đào tạo   |

### Bảng SINHVIEN

| Cột    | Kiểu          | Mô tả                        |
|--------|---------------|-------------------------------|
| MSSV   | VARCHAR(20) PK | Mã số sinh viên              |
| HoTen  | NVARCHAR(100)  | Họ và tên                    |
| MaCTDT | INT (FK)       | FK → CTDT.MaCTDT             |

---

## Tác giả

Thành Nguyên - Nhập môn CNPM
