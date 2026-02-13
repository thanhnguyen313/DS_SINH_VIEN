-- ============================================================
-- SCRIPT TẠO DATABASE CHO ỨNG DỤNG QUẢN LÝ DANH SÁCH SINH VIÊN
-- Công nghệ: MySQL 8.x
-- ============================================================
-- Tạo database (nếu chưa tồn tại)
CREATE DATABASE IF NOT EXISTS DS_SINH_VIEN CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
-- Sử dụng database
USE DS_SINH_VIEN;
-- ============================================================
-- BẢNG 1: CTDT (Chương trình Đào tạo) – Bảng danh mục
-- ============================================================
CREATE TABLE IF NOT EXISTS CTDT (
    MaCTDT INT AUTO_INCREMENT PRIMARY KEY COMMENT 'Mã chương trình đào tạo',
    TenCTDT NVARCHAR(100) NOT NULL COMMENT 'Tên chương trình đào tạo'
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;
-- ============================================================
-- BẢNG 2: SINHVIEN – Bảng chính
-- ============================================================
CREATE TABLE IF NOT EXISTS SINHVIEN (
    MSSV VARCHAR(20) PRIMARY KEY COMMENT 'Mã số sinh viên',
    HoTen NVARCHAR(100) NOT NULL COMMENT 'Họ và tên sinh viên',
    MaCTDT INT NOT NULL COMMENT 'Mã chương trình đào tạo (FK)',
    FOREIGN KEY (MaCTDT) REFERENCES CTDT(MaCTDT) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_unicode_ci;
-- ============================================================
-- DỮ LIỆU MẪU – Chương trình đào tạo
-- ============================================================
INSERT INTO CTDT (TenCTDT)
VALUES ('CTC - Chính quy Truyền thống'),
    ('CLC - Chất lượng cao'),
    ('CNTT - Công nghệ Thông tin'),
    ('KTPM - Kỹ thuật Phần mềm');
-- ============================================================
-- KIỂM TRA DỮ LIỆU
-- ============================================================
SELECT *
FROM CTDT;