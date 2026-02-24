namespace DS_SINH_VIEN.Models
{
    // Sinh viên
    public class SinhVien
    {
        public string MSSV { get; set; } = string.Empty;     // PK – Mã số sinh viên
        public string HoTen { get; set; } = string.Empty;    // Họ và tên
        public int MaCTDT { get; set; }                      // FK → CTDT
        public string TenCTDT { get; set; } = string.Empty;  // Tên CTDT (từ JOIN)
    }
}
