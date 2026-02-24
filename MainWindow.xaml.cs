using System;
using System.Collections.Generic;
using System.Windows;
using DS_SINH_VIEN.Models;

namespace DS_SINH_VIEN
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Khi cửa sổ được load: nạp CTDT vào ComboBox + SV vào DataGrid
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadComboBoxCTDT();
                LoadDataGridSinhVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi khi tải dữ liệu:\n{ex.Message}\n\nVui lòng kiểm tra kết nối SQL Server.",
                    "Lỗi kết nối",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        // Nạp danh sách Chương trình Đào tạo vào ComboBox
        private void LoadComboBoxCTDT()
        {
            List<CTDT> danhSachCTDT = DatabaseHelper.LoadCTDT();
            cbCTDT.ItemsSource = danhSachCTDT;

            if (danhSachCTDT.Count > 0)
                cbCTDT.SelectedIndex = 0;
        }

        // Nạp danh sách Sinh viên vào DataGrid
        private void LoadDataGridSinhVien()
        {
            List<SinhVien> danhSachSV = DatabaseHelper.LoadSinhVien();
            dgSinhVien.ItemsSource = danhSachSV;
        }

        // Xử lý nút "Thêm Sinh Viên"
        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            // Validate dữ liệu
            string mssv = txtMSSV.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();

            if (string.IsNullOrEmpty(mssv))
            {
                MessageBox.Show("Vui lòng nhập Mã số sinh viên.", "Thiếu thông tin",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtMSSV.Focus();
                return;
            }

            if (string.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show("Vui lòng nhập Họ và Tên.", "Thiếu thông tin",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                txtHoTen.Focus();
                return;
            }

            if (cbCTDT.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Chương trình đào tạo.", "Thiếu thông tin",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                cbCTDT.Focus();
                return;
            }

            int maCTDT = (int)cbCTDT.SelectedValue;

            // Thêm vào Database
            try
            {
                bool ketQua = DatabaseHelper.ThemSinhVien(mssv, hoTen, maCTDT);

                if (ketQua)
                {
                    MessageBox.Show($"Thêm sinh viên {mssv} thành công!", "Thành công",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    LoadDataGridSinhVien();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Không thể thêm sinh viên. Vui lòng thử lại.", "Lỗi",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate", StringComparison.OrdinalIgnoreCase) ||
                    ex.Message.Contains("PRIMARY KEY", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show($"MSSV '{mssv}' đã tồn tại!\nVui lòng nhập MSSV khác.",
                        "Trùng MSSV", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtMSSV.Focus();
                    txtMSSV.SelectAll();
                }
                else
                {
                    MessageBox.Show($"Lỗi khi thêm sinh viên:\n{ex.Message}", "Lỗi",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Xóa form nhập liệu sau khi thêm thành công
        private void ClearForm()
        {
            txtMSSV.Text = string.Empty;
            txtHoTen.Text = string.Empty;
            cbCTDT.SelectedIndex = 0;
            txtMSSV.Focus();
        }
    }
}
