using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using DS_SINH_VIEN.Models;

namespace DS_SINH_VIEN
{
    /// <summary>
    /// Lớp helper tĩnh để kết nối và thao tác với SQL Server Database
    /// </summary>
    public static class DatabaseHelper
    {
        // ====================================================================
        // CONNECTION STRING
        // ⚠️ Thay đổi Server name nếu SQL Server instance khác
        // ====================================================================
        private const string ConnectionString =
            @"Server=localhost\SQLEXPRESS; Database=DS_SINH_VIEN; Trusted_Connection=True; TrustServerCertificate=True;";

        /// <summary>
        /// Tạo và mở kết nối tới SQL Server
        /// </summary>
        /// <returns>SqlConnection đã mở</returns>
        public static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        /// <summary>
        /// Lấy danh sách Chương trình Đào tạo từ bảng CTDT
        /// </summary>
        /// <returns>List các đối tượng CTDT</returns>
        public static List<CTDT> LoadCTDT()
        {
            List<CTDT> danhSach = new List<CTDT>();

            using (SqlConnection conn = GetConnection())
            {
                string sql = "SELECT MaCTDT, TenCTDT FROM CTDT ORDER BY MaCTDT";
                SqlCommand cmd = new SqlCommand(sql, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        danhSach.Add(new CTDT
                        {
                            MaCTDT = reader.GetInt32(reader.GetOrdinal("MaCTDT")),
                            TenCTDT = reader.GetString(reader.GetOrdinal("TenCTDT"))
                        });
                    }
                }
            }

            return danhSach;
        }

        /// <summary>
        /// Lấy danh sách Sinh viên (có JOIN với CTDT để lấy tên chương trình)
        /// </summary>
        /// <returns>List các đối tượng SinhVien</returns>
        public static List<SinhVien> LoadSinhVien()
        {
            List<SinhVien> danhSach = new List<SinhVien>();

            using (SqlConnection conn = GetConnection())
            {
                string sql = @"
                    SELECT sv.MSSV, sv.HoTen, sv.MaCTDT, ct.TenCTDT
                    FROM SINHVIEN sv
                    INNER JOIN CTDT ct ON sv.MaCTDT = ct.MaCTDT
                    ORDER BY sv.MSSV";

                SqlCommand cmd = new SqlCommand(sql, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        danhSach.Add(new SinhVien
                        {
                            MSSV = reader.GetString(reader.GetOrdinal("MSSV")),
                            HoTen = reader.GetString(reader.GetOrdinal("HoTen")),
                            MaCTDT = reader.GetInt32(reader.GetOrdinal("MaCTDT")),
                            TenCTDT = reader.GetString(reader.GetOrdinal("TenCTDT"))
                        });
                    }
                }
            }

            return danhSach;
        }

        /// <summary>
        /// Thêm một sinh viên mới vào bảng SINHVIEN
        /// Sử dụng Parameterized Query để chống SQL Injection
        /// </summary>
        /// <param name="mssv">Mã số sinh viên</param>
        /// <param name="hoTen">Họ và tên</param>
        /// <param name="maCTDT">Mã chương trình đào tạo</param>
        /// <returns>true nếu thêm thành công</returns>
        public static bool ThemSinhVien(string mssv, string hoTen, int maCTDT)
        {
            using (SqlConnection conn = GetConnection())
            {
                string sql = "INSERT INTO SINHVIEN (MSSV, HoTen, MaCTDT) VALUES (@mssv, @hoten, @mactdt)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                // Parameterized query – an toàn, chống SQL Injection
                cmd.Parameters.AddWithValue("@mssv", mssv);
                cmd.Parameters.AddWithValue("@hoten", hoTen);
                cmd.Parameters.AddWithValue("@mactdt", maCTDT);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}
