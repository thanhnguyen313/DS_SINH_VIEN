namespace DS_SINH_VIEN.Models
{
    // Chương trình Đào tạo
    public class CTDT
    {
        public int MaCTDT { get; set; }         // PK – Mã CTDT
        public string TenCTDT { get; set; } = string.Empty;  // Tên CTDT

        public override string ToString() => TenCTDT;
    }
}
