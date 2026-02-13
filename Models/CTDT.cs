namespace DS_SINH_VIEN.Models
{
    /// <summary>
    /// Model đại diện cho Chương trình Đào tạo
    /// </summary>
    public class CTDT
    {
        /// <summary>
        /// Mã chương trình đào tạo (Primary Key)
        /// </summary>
        public int MaCTDT { get; set; }

        /// <summary>
        /// Tên chương trình đào tạo
        /// </summary>
        public string TenCTDT { get; set; } = string.Empty;

        /// <summary>
        /// Override ToString để hiển thị trong ComboBox nếu cần
        /// </summary>
        public override string ToString()
        {
            return TenCTDT;
        }
    }
}
