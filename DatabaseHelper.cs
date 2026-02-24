using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using DS_SINH_VIEN.Models;

namespace DS_SINH_VIEN
{
    public static class DatabaseHelper
    {
        // Connection String – SQL Server Express, Windows Authentication
        private const string ConnectionString =
            @"Server=localhost\SQLEXPRESS; Database=DS_SINH_VIEN; Trusted_Connection=True; TrustServerCertificate=True;";

        // Tạo và mở kết nối
        public static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        // Lấy danh sách Chương trình Đào tạo
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

        // Lấy danh sách Sinh viên (JOIN với CTDT)
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

        // Thêm sinh viên mới (Parameterized Query chống SQL Injection)
        public static bool ThemSinhVien(string mssv, string hoTen, int maCTDT)
        {
            using (SqlConnection conn = GetConnection())
            {
                string sql = "INSERT INTO SINHVIEN (MSSV, HoTen, MaCTDT) VALUES (@mssv, @hoten, @mactdt)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@mssv", mssv);
                cmd.Parameters.AddWithValue("@hoten", hoTen);
                cmd.Parameters.AddWithValue("@mactdt", maCTDT);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}
