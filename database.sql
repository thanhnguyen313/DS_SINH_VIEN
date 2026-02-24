-- ============================================================
-- SCRIPT TẠO DATABASE CHO ỨNG DỤNG QUẢN LÝ DANH SÁCH SINH VIÊN
-- Công nghệ: SQL Server
-- Ngày tạo : 2026-02-24
-- ============================================================

-- ============================================================
-- BƯỚC 1: TẠO DATABASE
-- ============================================================
-- Đóng tất cả kết nối đến DB cũ (nếu có) rồi xóa
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'DS_SINH_VIEN')
BEGIN
    ALTER DATABASE DS_SINH_VIEN SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE DS_SINH_VIEN;
END
GO

-- Tạo database mới
CREATE DATABASE DS_SINH_VIEN;
GO

-- Sử dụng database vừa tạo
USE DS_SINH_VIEN;
GO

-- ============================================================
-- BƯỚC 2: TẠO BẢNG CTDT (Chương trình Đào tạo) – Bảng danh mục
-- ============================================================
CREATE TABLE CTDT (
    MaCTDT  INT IDENTITY(1,1) PRIMARY KEY,   -- Mã CTDT, tự tăng
    TenCTDT NVARCHAR(100)     NOT NULL        -- Tên chương trình đào tạo
);
GO

-- ============================================================
-- BƯỚC 3: TẠO BẢNG SINHVIEN – Bảng chính
-- ============================================================
CREATE TABLE SINHVIEN (
    MSSV    VARCHAR(20)       PRIMARY KEY,    -- Mã số sinh viên
    HoTen   NVARCHAR(100)     NOT NULL,       -- Họ và tên sinh viên
    MaCTDT  INT               NOT NULL,       -- Mã CTDT (FK)

    -- Ràng buộc khóa ngoại
    CONSTRAINT FK_SinhVien_CTDT
        FOREIGN KEY (MaCTDT) REFERENCES CTDT(MaCTDT)
        ON UPDATE CASCADE
        ON DELETE NO ACTION
);
GO

-- ============================================================
-- BƯỚC 4: CHÈN DỮ LIỆU MẪU – Chương trình Đào tạo
-- ============================================================
INSERT INTO CTDT (TenCTDT) VALUES
    (N'CTC - Chính quy Truyền thống'),
    (N'CLC - Chất lượng cao'),
    (N'CNTT - Công nghệ Thông tin'),
    (N'KTPM - Kỹ thuật Phần mềm');
GO

-- ============================================================
-- BƯỚC 5: CHÈN DỮ LIỆU MẪU – Sinh viên (để test)
-- ============================================================
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

-- ============================================================
-- BƯỚC 6: KIỂM TRA DỮ LIỆU
-- ============================================================
PRINT N'--- Danh sách Chương trình Đào tạo ---';
SELECT * FROM CTDT;

PRINT N'--- Danh sách Sinh viên ---';
SELECT sv.MSSV, sv.HoTen, ct.TenCTDT
FROM SINHVIEN sv
INNER JOIN CTDT ct ON sv.MaCTDT = ct.MaCTDT
ORDER BY sv.MSSV;
GO