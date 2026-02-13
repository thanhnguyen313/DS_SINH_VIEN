using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DS_SINH_VIEN.Models;

namespace DS_SINH_VIEN
{
    /// <summary>
    /// Lớp helper tĩnh để kết nối và thao tác với MySQL Database
    /// </summary>
    public static class DatabaseHelper
    {
        // ====================================================================
        // CONNECTION STRING
        // ⚠️ Thay đổi Pwd nếu MySQL có mật khẩu
        // ====================================================================
        private const string ConnectionString =
            "Server=localhost; Port=3306; Database=DS_SINH_VIEN; Uid=root; Pwd=; CharSet=utf8mb4;";

        /// <summary>
        /// Tạo và mở kết nối tới MySQL
        /// </summary>
        /// <returns>MySqlConnection đã mở</returns>
        public static MySqlConnection GetConnection()
        {
            MySqlConnection connection = new MySqlConnection(ConnectionString);
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

            using (MySqlConnection conn = GetConnection())
            {
                string sql = "SELECT MaCTDT, TenCTDT FROM CTDT ORDER BY MaCTDT";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        danhSach.Add(new CTDT
                        {
                            MaCTDT = reader.GetInt32("MaCTDT"),
                            TenCTDT = reader.GetString("TenCTDT")
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

            using (MySqlConnection conn = GetConnection())
            {
                string sql = @"
                    SELECT sv.MSSV, sv.HoTen, sv.MaCTDT, ct.TenCTDT
                    FROM SINHVIEN sv
                    INNER JOIN CTDT ct ON sv.MaCTDT = ct.MaCTDT
                    ORDER BY sv.MSSV";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        danhSach.Add(new SinhVien
                        {
                            MSSV = reader.GetString("MSSV"),
                            HoTen = reader.GetString("HoTen"),
                            MaCTDT = reader.GetInt32("MaCTDT"),
                            TenCTDT = reader.GetString("TenCTDT")
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
            using (MySqlConnection conn = GetConnection())
            {
                string sql = "INSERT INTO SINHVIEN (MSSV, HoTen, MaCTDT) VALUES (@mssv, @hoten, @mactdt)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

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
