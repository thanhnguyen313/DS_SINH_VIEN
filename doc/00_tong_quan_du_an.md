# TỔNG QUAN DỰ ÁN – QUẢN LÝ DANH SÁCH SINH VIÊN

## Mục tiêu
Xây dựng ứng dụng WPF (C#) kết nối MySQL, cho phép:
- Quản lý danh mục **Chương trình đào tạo** (CTDT)
- **Thêm** và **hiển thị** danh sách sinh viên

## Công nghệ
| Thành phần | Công nghệ |
|---|---|
| Frontend (UI) | WPF – XAML + C# |
| Backend (Logic) | C# (.NET 8) |
| Database | MySQL 8.x |
| ORM / Driver | MySql.Data (ADO.NET) |

## Cấu trúc Database

```
┌──────────────┐       ┌──────────────────────┐
│    CTDT      │       │     SINHVIEN          │
├──────────────┤       ├──────────────────────┤
│ MaCTDT (PK)  │◄──────│ MaCTDT (FK)          │
│ TenCTDT      │       │ MSSV (PK)            │
└──────────────┘       │ HoTen                │
                       └──────────────────────┘
```

## Các giai đoạn thực hiện

| # | Giai đoạn | File plan | Mô tả |
|---|---|---|---|
| 1 | **Frontend (UI)** | [01_frontend_plan.md](./01_frontend_plan.md) | Thiết kế giao diện WPF |
| 2 | **Backend (DB + Logic)** | [02_backend_plan.md](./02_backend_plan.md) | Schema, kết nối, xử lý dữ liệu |
| 3 | **Tích hợp & Kiểm thử** | [03_integration_plan.md](./03_integration_plan.md) | Ghép nối, test, hoàn thiện |

## Cấu trúc thư mục dự án

```
DS_SINH_VIEN/
├── doc/                          # Tài liệu kế hoạch
│   ├── 00_tong_quan_du_an.md
│   ├── 01_frontend_plan.md
│   ├── 02_backend_plan.md
│   └── 03_integration_plan.md
├── Models/
│   ├── CTDT.cs                   # Model chương trình đào tạo
│   └── SinhVien.cs               # Model sinh viên
├── DatabaseHelper.cs             # Lớp kết nối & thao tác DB
├── MainWindow.xaml               # Giao diện chính (XAML)
├── MainWindow.xaml.cs            # Code-behind
├── App.xaml                      # Application entry
├── App.xaml.cs
├── DS_SINH_VIEN.csproj           # Project file
└── database.sql                  # Script tạo DB
```
