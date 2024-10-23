using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Winform_Them_Sua_Xoa2310
{
    public partial class Form1 : Form
    {
        private List<SanPham> danhSachSanPham = new List<SanPham>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = false; // Cho phép chỉnh sửa ô
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Chọn cả dòng khi click
            dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2; // Cho phép chỉnh sửa khi nhấn phím

            // Thêm các loại sản phẩm vào ComboBox LoaiSP
            cbLoaiSP.Items.AddRange(new string[] { "Áo", "Quần", "Trang sức", "Khác" });
            // Cấu hình DataGridView
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = danhSachSanPham;
        }

        // Thêm sản phẩm
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra thông tin nhập vào
                string maSP = tbMaSP.Text;
                string tenSP = tbTenSP.Text;
                if (string.IsNullOrWhiteSpace(maSP) || string.IsNullOrWhiteSpace(tenSP))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin sản phẩm.");
                    return;
                }

                // Xử lý đơn giá
                if (!decimal.TryParse(tbDonGia.Text, out decimal donGia))
                {
                    MessageBox.Show("Đơn giá không hợp lệ.");
                    return;
                }

                // Tạo đối tượng sản phẩm mới
                SanPham sanPham = new SanPham
                {
                    MaSP = maSP,
                    TenSP = tenSP,
                    DonGia = donGia,
                    HinhAnh = tbHinhAnh.Text,
                    MoTaNgan = tbMoTaNgan.Text,
                    MoTaChiTiet = tbMoTaChiTiet.Text,
                    LoaiSP = cbLoaiSP.SelectedItem?.ToString() // Kiểm tra nếu loại sản phẩm có chọn
                };

                danhSachSanPham.Add(sanPham); // Thêm sản phẩm vào danh sách

                // Cập nhật lại DataGridView
                CapNhatDataGridView();

                // Làm mới các ô nhập liệu
                btnLamMoi_Click(sender, e); // Thay vì gọi LamMoi(), sử dụng btnLamMoi_Click
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // Cập nhật DataGridView
        private void CapNhatDataGridView()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = danhSachSanPham;
        }

        // Làm mới form nhập liệu
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            // Xóa các TextBox
            tbMaSP.Clear();
            tbTenSP.Clear();
            tbDonGia.Clear();
            tbHinhAnh.Clear();
            tbMoTaNgan.Clear();
            tbMoTaChiTiet.Clear();

            // Đặt lại ComboBox về giá trị mặc định
            cbLoaiSP.SelectedIndex = -1;
        }

        // Sửa sản phẩm
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                // Lấy mã sản phẩm hiện tại từ DataGridView
                string maSP = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                // Tìm sản phẩm trong danh sách
                var sanPham = danhSachSanPham.FirstOrDefault(sp => sp.MaSP == maSP);
                if (sanPham != null)
                {
                    // Cập nhật lại thông tin
                    sanPham.TenSP = tbTenSP.Text;
                    if (decimal.TryParse(tbDonGia.Text, out decimal donGia))
                    {
                        sanPham.DonGia = donGia;
                    }
                    sanPham.HinhAnh = tbHinhAnh.Text;
                    sanPham.MoTaNgan = tbMoTaNgan.Text;
                    sanPham.MoTaChiTiet = tbMoTaChiTiet.Text;
                    sanPham.LoaiSP = cbLoaiSP.SelectedItem?.ToString();

                    // Cập nhật lại DataGridView
                    CapNhatDataGridView();
                    btnLamMoi_Click(sender, e); // Gọi làm mới sau khi sửa
                }
            }
        }

        // Xóa sản phẩm
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                // Lấy mã sản phẩm từ hàng đang chọn
                string maSP = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                // Xóa sản phẩm khỏi danh sách
                danhSachSanPham.RemoveAll(sp => sp.MaSP == maSP);

                // Cập nhật lại DataGridView
                CapNhatDataGridView();
                btnLamMoi_Click(sender, e); // Gọi làm mới sau khi xóa
            }
        }

        // Tìm kiếm sản phẩm theo mã hoặc tên
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = tbTimKiem.Text.ToLower();

            // Lọc danh sách theo từ khóa
            var ketQua = danhSachSanPham
                         .Where(sp => sp.MaSP.ToLower().Contains(tuKhoa) || sp.TenSP.ToLower().Contains(tuKhoa))
                         .ToList();

            // Hiển thị kết quả tìm kiếm
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ketQua;
        }

        // Khi chọn một hàng trong DataGridView, hiển thị thông tin lên các TextBox
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy sản phẩm từ hàng được chọn
                var sanPham = danhSachSanPham[e.RowIndex];
                tbMaSP.Text = sanPham.MaSP;
                tbTenSP.Text = sanPham.TenSP;
                tbDonGia.Text = sanPham.DonGia.ToString();
                tbHinhAnh.Text = sanPham.HinhAnh;
                tbMoTaNgan.Text = sanPham.MoTaNgan;
                tbMoTaChiTiet.Text = sanPham.MoTaChiTiet;
                cbLoaiSP.SelectedItem = sanPham.LoaiSP;
            }
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var sanPham = danhSachSanPham[e.RowIndex];

                // Lấy dữ liệu từ các ô đã chỉnh sửa
                sanPham.MaSP = dataGridView1.Rows[e.RowIndex].Cells["MaSP"].Value.ToString();
                sanPham.TenSP = dataGridView1.Rows[e.RowIndex].Cells["TenSP"].Value.ToString();
                sanPham.DonGia = decimal.Parse(dataGridView1.Rows[e.RowIndex].Cells["DonGia"].Value.ToString());
                sanPham.HinhAnh = dataGridView1.Rows[e.RowIndex].Cells["HinhAnh"].Value.ToString();
                sanPham.MoTaNgan = dataGridView1.Rows[e.RowIndex].Cells["MoTaNgan"].Value.ToString();
                sanPham.MoTaChiTiet = dataGridView1.Rows[e.RowIndex].Cells["MoTaChiTiet"].Value.ToString();
                sanPham.LoaiSP = dataGridView1.Rows[e.RowIndex].Cells["LoaiSP"].Value.ToString();
            }
        }

        private void tbMoTaChiTiet_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
