-- Tạo Database cho ứng dụng Quản lý Danh sách Sinh viên
-- SQL Server Express

-- Xóa database cũ (nếu có)
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'DS_SINH_VIEN')
BEGIN
    ALTER DATABASE DS_SINH_VIEN SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE DS_SINH_VIEN;
END
GO

-- Tạo database
CREATE DATABASE DS_SINH_VIEN;
GO

USE DS_SINH_VIEN;
GO

-- Bảng CTDT (Chương trình Đào tạo)
CREATE TABLE CTDT (
    MaCTDT  INT IDENTITY(1,1) PRIMARY KEY,
    TenCTDT NVARCHAR(100)     NOT NULL
);
GO

-- Bảng SINHVIEN
CREATE TABLE SINHVIEN (
    MSSV    VARCHAR(20)       PRIMARY KEY,
    HoTen   NVARCHAR(100)     NOT NULL,
    MaCTDT  INT               NOT NULL,

    CONSTRAINT FK_SinhVien_CTDT
        FOREIGN KEY (MaCTDT) REFERENCES CTDT(MaCTDT)
        ON UPDATE CASCADE
        ON DELETE NO ACTION
);
GO

-- Dữ liệu mẫu: Chương trình Đào tạo
INSERT INTO CTDT (TenCTDT) VALUES
    (N'CTC - Chính quy Truyền thống'),
    (N'CLC - Chất lượng cao'),
    (N'CNTT - Công nghệ Thông tin'),
    (N'KTPM - Kỹ thuật Phần mềm');
GO

-- Dữ liệu mẫu: Sinh viên (MSSV format: 2x52xxxx)
INSERT INTO SINHVIEN (MSSV, HoTen, MaCTDT) VALUES
    ('23520001', N'Nguyễn Văn An',        1),
    ('23520002', N'Trần Thị Bích',        2),
    ('23520003', N'Lê Hoàng Cường',       3),
    ('23520004', N'Phạm Minh Đức',        4),
    ('23520005', N'Võ Thị Hương',         1),
    ('24520006', N'Đặng Quốc Huy',       2),
    ('24520007', N'Huỳnh Ngọc Lan',      3),
    ('24520008', N'Bùi Thanh Mai',        4),
    ('24520009', N'Hoàng Anh Tuấn',      1),
    ('24520010', N'Lý Thị Ngọc',          2),
    ('25520011', N'Trương Minh Phát',     3),
    ('25520012', N'Ngô Thị Quỳnh',       4),
    ('25520013', N'Đinh Công Sơn',        1),
    ('25520014', N'Vũ Thị Thanh',         2),
    ('25520015', N'Đỗ Hữu Uy',           3);
GO

-- Kiểm tra
SELECT * FROM CTDT;

SELECT sv.MSSV, sv.HoTen, ct.TenCTDT
FROM SINHVIEN sv
INNER JOIN CTDT ct ON sv.MaCTDT = ct.MaCTDT
ORDER BY sv.MSSV;
GO