# PHASE 1 – FRONTEND (GIAO DIỆN WPF)

## 1. Mục tiêu
Xây dựng giao diện người dùng bằng WPF (XAML + C#) với các thành phần:
- Form nhập liệu sinh viên
- ComboBox chọn chương trình đào tạo
- DataGrid hiển thị danh sách sinh viên
- Nút chức năng (Thêm)

---

## 2. Thiết kế Layout

### 2.1 Cửa sổ chính – `MainWindow.xaml`

```
┌──────────────────────────────────────────────────────────────┐
│  📋 QUẢN LÝ DANH SÁCH SINH VIÊN                    [Title]  │
├──────────────────────┬───────────────────────────────────────┤
│                      │                                       │
│  ┌─ NHẬP THÔNG TIN ─┐│  ┌─ DANH SÁCH SINH VIÊN ──────────┐ │
│  │                   ││  │                                 │ │
│  │  MSSV:            ││  │  ┌──────┬──────────┬──────────┐ │ │
│  │  ┌─────────────┐  ││  │  │ MSSV │ Họ Tên   │ CTDT     │ │ │
│  │  │ txtMSSV     │  ││  │  ├──────┼──────────┼──────────┤ │ │
│  │  └─────────────┘  ││  │  │ ...  │ ...      │ ...      │ │ │
│  │                   ││  │  │      │          │          │ │ │
│  │  Họ Tên:          ││  │  │      │          │          │ │ │
│  │  ┌─────────────┐  ││  │  │      │          │          │ │ │
│  │  │ txtHoTen    │  ││  │  │      │          │          │ │ │
│  │  └─────────────┘  ││  │  └──────┴──────────┴──────────┘ │ │
│  │                   ││  │                                 │ │
│  │  Chương trình ĐT: ││  └─────────────────────────────────┘ │
│  │  ┌─────────────┐  ││                                       │
│  │  │ cbCTDT  ▼   │  ││                                       │
│  │  └─────────────┘  ││                                       │
│  │                   ││                                       │
│  │  ┌─────────────┐  ││                                       │
│  │  │  THÊM  ▶    │  ││                                       │
│  │  └─────────────┘  ││                                       │
│  └───────────────────┘│                                       │
└──────────────────────┴───────────────────────────────────────┘
```

### 2.2 Kích thước & Thuộc tính cửa sổ

| Thuộc tính | Giá trị |
|---|---|
| Title | "Quản Lý Danh Sách Sinh Viên" |
| Width | 800 |
| Height | 500 |
| WindowStartupLocation | CenterScreen |
| ResizeMode | CanResize |

---

## 3. Chi tiết các Control

### 3.1 Form nhập liệu (Panel trái)

| # | Control | Name | Loại | Mô tả |
|---|---|---|---|---|
| 1 | Label | – | TextBlock | "MSSV:" |
| 2 | TextBox | `txtMSSV` | TextBox | Nhập mã số sinh viên |
| 3 | Label | – | TextBlock | "Họ Tên:" |
| 4 | TextBox | `txtHoTen` | TextBox | Nhập họ tên sinh viên |
| 5 | Label | – | TextBlock | "Chương trình ĐT:" |
| 6 | ComboBox | `cbCTDT` | ComboBox | Dropdown chọn CTDT |
| 7 | Button | `btnThem` | Button | Nút "Thêm" – lưu sinh viên |

**ComboBox `cbCTDT`**:
- `DisplayMemberPath` = `"TenCTDT"` (hiển thị tên CTDT)
- `SelectedValuePath` = `"MaCTDT"` (giá trị là mã CTDT)

### 3.2 DataGrid hiển thị (Panel phải)

| # | Cột | Binding | Header | Width |
|---|---|---|---|---|
| 1 | DataGridTextColumn | `{Binding MSSV}` | "MSSV" | 120 |
| 2 | DataGridTextColumn | `{Binding HoTen}` | "Họ Tên" | 200* |
| 3 | DataGridTextColumn | `{Binding TenCTDT}` | "Chương trình ĐT" | 150 |

- `AutoGenerateColumns` = `False`
- `IsReadOnly` = `True`
- `Name` = `dgSinhVien`

---

## 4. XAML Structure (Pseudocode)

```xml
<Window>
  <Grid>
    <Grid.ColumnDefinitions>
      <ColumnDefinition Width="280"/>    <!-- Form nhập -->
      <ColumnDefinition Width="*"/>      <!-- DataGrid -->
    </Grid.ColumnDefinitions>

    <!-- CỘT 0: Form nhập liệu -->
    <GroupBox Header="Nhập thông tin" Grid.Column="0">
      <StackPanel>
        <TextBlock Text="MSSV:"/>
        <TextBox Name="txtMSSV"/>
        <TextBlock Text="Họ Tên:"/>
        <TextBox Name="txtHoTen"/>
        <TextBlock Text="Chương trình ĐT:"/>
        <ComboBox Name="cbCTDT"
                  DisplayMemberPath="TenCTDT"
                  SelectedValuePath="MaCTDT"/>
        <Button Name="btnThem" Content="Thêm"
                Click="btnThem_Click"/>
      </StackPanel>
    </GroupBox>

    <!-- CỘT 1: DataGrid -->
    <GroupBox Header="Danh sách sinh viên" Grid.Column="1">
      <DataGrid Name="dgSinhVien"
                AutoGenerateColumns="False"
                IsReadOnly="True">
        <DataGrid.Columns>
          <DataGridTextColumn Header="MSSV"
                              Binding="{Binding MSSV}" Width="120"/>
          <DataGridTextColumn Header="Họ Tên"
                              Binding="{Binding HoTen}" Width="*"/>
          <DataGridTextColumn Header="Chương trình ĐT"
                              Binding="{Binding TenCTDT}" Width="150"/>
        </DataGrid.Columns>
      </DataGrid>
    </GroupBox>
  </Grid>
</Window>
```

---

## 5. Sự kiện (Events) cần xử lý

| # | Sự kiện | Trigger | Hành động |
|---|---|---|---|
| 1 | `Window_Loaded` | Cửa sổ mở | Load CTDT → ComboBox, Load SV → DataGrid |
| 2 | `btnThem_Click` | Nhấn nút "Thêm" | Validate → Insert DB → Refresh DataGrid → Clear form |

### 5.1 `Window_Loaded` – Pseudocode

```
1. Gọi DatabaseHelper.LoadCTDT()
2. Gán kết quả vào cbCTDT.ItemsSource
3. Chọn mục đầu tiên (SelectedIndex = 0)
4. Gọi DatabaseHelper.LoadSinhVien()
5. Gán kết quả vào dgSinhVien.ItemsSource
```

### 5.2 `btnThem_Click` – Pseudocode

```
1. Lấy giá trị: mssv = txtMSSV.Text, hoten = txtHoTen.Text, mactdt = cbCTDT.SelectedValue
2. Validate:
   - Nếu mssv rỗng → MessageBox "Vui lòng nhập MSSV"
   - Nếu hoten rỗng → MessageBox "Vui lòng nhập Họ Tên"
   - Nếu chưa chọn CTDT → MessageBox "Vui lòng chọn CTDT"
3. Gọi DatabaseHelper.ThemSinhVien(mssv, hoten, mactdt)
4. Nếu thành công:
   - MessageBox "Thêm sinh viên thành công!"
   - Refresh DataGrid (gọi lại LoadSinhVien)
   - Clear form: txtMSSV = "", txtHoTen = "", cbCTDT.SelectedIndex = 0
5. Nếu lỗi (ví dụ trùng MSSV):
   - MessageBox hiển thị lỗi
```

---

## 6. Styling cơ bản

| Phần tử | Style |
|---|---|
| Window | Background: `#F5F5F5` |
| GroupBox | Margin: `10`, Padding: `10` |
| TextBox | Margin: `0,0,0,10`, Height: `30` |
| ComboBox | Margin: `0,0,0,15`, Height: `30` |
| Button | Height: `35`, Background: `#2196F3`, Foreground: `White`, FontWeight: `Bold` |
| DataGrid | AlternatingRowBackground: `#E3F2FD` |

---

## 7. Files cần tạo (Phase này)

| # | File | Mô tả |
|---|---|---|
| 1 | `DS_SINH_VIEN.csproj` | Project file, khai báo .NET 8 + WPF |
| 2 | `App.xaml` | Application entry point |
| 3 | `App.xaml.cs` | App code-behind |
| 4 | `MainWindow.xaml` | Giao diện chính |
| 5 | `MainWindow.xaml.cs` | Code-behind (events, logic gọi DB) |
| 6 | `Models/CTDT.cs` | Model class CTDT |
| 7 | `Models/SinhVien.cs` | Model class SinhVien |

---

## 8. Checklist

- [x] Tạo `DS_SINH_VIEN.csproj` (net8.0-windows, WPF, MySql.Data)
- [x] Tạo `App.xaml` + `App.xaml.cs`
- [x] Tạo `Models/CTDT.cs`
- [x] Tạo `Models/SinhVien.cs`
- [x] Tạo `MainWindow.xaml` (layout 2 cột, form + DataGrid)
- [x] Tạo `MainWindow.xaml.cs` (Window_Loaded, btnThem_Click)
- [x] Review giao diện trước khi chuyển sang Phase 2
