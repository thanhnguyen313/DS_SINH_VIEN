namespace DS_SINH_VIEN.Models
{
    /// <summary>
    /// Model đại diện cho Sinh viên
    /// </summary>
    public class SinhVien
    {
        /// <summary>
        /// Mã số sinh viên (Primary Key)
        /// </summary>
        public string MSSV { get; set; } = string.Empty;

        /// <summary>
        /// Họ và tên sinh viên
        /// </summary>
        public string HoTen { get; set; } = string.Empty;

        /// <summary>
        /// Mã chương trình đào tạo (Foreign Key → CTDT)
        /// </summary>
        public int MaCTDT { get; set; }

        /// <summary>
        /// Tên chương trình đào tạo (dùng để hiển thị trên DataGrid, lấy từ JOIN)
        /// </summary>
        public string TenCTDT { get; set; } = string.Empty;
    }
}
