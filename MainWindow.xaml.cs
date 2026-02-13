using System;
using System.Collections.Generic;
using System.Windows;
using DS_SINH_VIEN.Models;

namespace DS_SINH_VIEN
{
    /// <summary>
    /// Code-behind cho MainWindow.xaml
    /// Xử lý sự kiện: Load dữ liệu, Thêm sinh viên
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sự kiện khi cửa sổ được load
        /// - Load danh sách CTDT vào ComboBox
        /// - Load danh sách Sinh viên vào DataGrid
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Load danh sách Chương trình Đào tạo vào ComboBox
                LoadComboBoxCTDT();

                // Load danh sách Sinh viên vào DataGrid
                LoadDataGridSinhVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi khi tải dữ liệu:\n{ex.Message}\n\nVui lòng kiểm tra kết nối MySQL.",
                    "Lỗi kết nối",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Load danh sách CTDT từ Database vào ComboBox
        /// </summary>
        private void LoadComboBoxCTDT()
        {
            List<CTDT> danhSachCTDT = DatabaseHelper.LoadCTDT();
            cbCTDT.ItemsSource = danhSachCTDT;

            // Chọn mục đầu tiên mặc định
            if (danhSachCTDT.Count > 0)
            {
                cbCTDT.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Load danh sách Sinh viên từ Database vào DataGrid
        /// </summary>
        private void LoadDataGridSinhVien()
        {
            List<SinhVien> danhSachSV = DatabaseHelper.LoadSinhVien();
            dgSinhVien.ItemsSource = danhSachSV;
        }

        /// <summary>
        /// Sự kiện khi nhấn nút "Thêm"
        /// Validate → Insert vào DB → Refresh DataGrid → Clear form
        /// </summary>
        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            // ------- VALIDATE -------
            string mssv = txtMSSV.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();

            if (string.IsNullOrEmpty(mssv))
            {
                MessageBox.Show(
                    "Vui lòng nhập Mã số sinh viên (MSSV).",
                    "Thiếu thông tin",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                txtMSSV.Focus();
                return;
            }

            if (string.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show(
                    "Vui lòng nhập Họ và Tên.",
                    "Thiếu thông tin",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                txtHoTen.Focus();
                return;
            }

            if (cbCTDT.SelectedValue == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn Chương trình đào tạo.",
                    "Thiếu thông tin",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                cbCTDT.Focus();
                return;
            }

            int maCTDT = (int)cbCTDT.SelectedValue;

            // ------- THÊM VÀO DATABASE -------
            try
            {
                bool ketQua = DatabaseHelper.ThemSinhVien(mssv, hoTen, maCTDT);

                if (ketQua)
                {
                    MessageBox.Show(
                        $"Thêm sinh viên {mssv} thành công!",
                        "Thành công",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);

                    // Refresh DataGrid
                    LoadDataGridSinhVien();

                    // Clear form để nhập tiếp
                    ClearForm();
                }
                else
                {
                    MessageBox.Show(
                        "Không thể thêm sinh viên. Vui lòng thử lại.",
                        "Lỗi",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi trùng khóa chính (Duplicate MSSV)
                if (ex.Message.Contains("Duplicate", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show(
                        $"MSSV '{mssv}' đã tồn tại trong hệ thống!\nVui lòng nhập MSSV khác.",
                        "Trùng MSSV",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                    txtMSSV.Focus();
                    txtMSSV.SelectAll();
                }
                else
                {
                    MessageBox.Show(
                        $"Lỗi khi thêm sinh viên:\n{ex.Message}",
                        "Lỗi",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Xóa trắng form nhập liệu sau khi thêm thành công
        /// </summary>
        private void ClearForm()
        {
            txtMSSV.Text = string.Empty;
            txtHoTen.Text = string.Empty;
            cbCTDT.SelectedIndex = 0;
            txtMSSV.Focus();
        }
    }
}
