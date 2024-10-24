using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class Form1 : Form
    {
        List<QuanLySinhVien> lst = new List<QuanLySinhVien>();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            QuanLySinhVien quanLySinhVien = new QuanLySinhVien();
            if (KiemTraDuLieu())
            {
                MessageBox.Show("Dữ liệu hợp lệ. Thêm Sinh Viên Thành Công.");
                quanLySinhVien.MaSV = txtMa.Text;
                quanLySinhVien.HoTen = txtTen.Text;
                quanLySinhVien.NgaySinh = txtNgaysinh.Text;
                quanLySinhVien.GioiTinh = cbGioitinh.Text;
                quanLySinhVien.Khoa = cbKhoa.Text;
                quanLySinhVien.DiaChi = txtDiachi.Text;
                quanLySinhVien.Email = txtEmail.Text;
                quanLySinhVien.Sdt = txtSdt.Text;
                lst.Add(quanLySinhVien);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = lst;
            }

        }

        // Check input 
        private bool KiemTraDuLieu()
        {
            string masv = txtMa.Text;
            string tensv = txtTen.Text;
            string ngaysinh = txtNgaysinh.Text;
            string gioitinh = cbGioitinh.Text;
            string khoa = cbKhoa.Text;
            string diachi = txtDiachi.Text;
            string email = txtEmail.Text;
            string sdt = txtSdt.Text;


            // Kiểm tra mã sinh viên
            if (string.IsNullOrEmpty(masv))
            {
                MessageBox.Show("Vui lòng nhập Mã sinh viên.");
                txtMa.Text = "";
                return false;
            }
            // Kiểm tra tên
            if (string.IsNullOrEmpty(tensv))
            {
                MessageBox.Show("Vui lòng nhập Tên sinh viên.");
                txtTen.Text = "";
                return false;
            }
            // Kiểm tra ngày sinh
            try
            {
                DateTime tempDate = DateTime.ParseExact(ngaysinh, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                if (tempDate > DateTime.Now)
                {
                    MessageBox.Show("Ngày sinh không hợp lệ.");
                    txtNgaysinh.Text = "";
                    return false;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Vui lòng nhập theo định dạng dd/MM/yyyy.");
                txtNgaysinh.Text = "";
                return false;
            }
            // Kiểm tra giới tính
            if (string.IsNullOrEmpty(gioitinh))
            {
                MessageBox.Show("Vui lòng chọn giới tính");
                cbGioitinh.Text = "";
                return false;
            }
            // Kiểm tra khoa
            if (string.IsNullOrEmpty(khoa))
            {
                MessageBox.Show("Vui lòng chọn khoa.");
                cbKhoa.Text = "";
                return false;
            }
            // Kiểm tra địa chỉ
            if (string.IsNullOrEmpty(diachi))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ");
                txtDiachi.Text = "";
                return false;
            }
            // Kiểm tra email
            try
            {
                MailAddress mailAddress = new MailAddress(email);
            }
            catch (FormatException)
            {
                MessageBox.Show("Địa chỉ email không hợp lệ.\n Vui lòng nhập lại.\n example@gmail.com");
                txtEmail.Text = "";
                return false;
            }

            // kiểm tra số điện thoại
            if (string.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại.");
                txtSdt.Text = "";
                return false;
            }
            foreach (char c in sdt)
            {
                if (!char.IsDigit(c))
                {
                    MessageBox.Show("Số điện thoại chỉ được nhập các chữ số.");
                    return false;
                }
            }
            return true;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtMa.Text = "";
            txtTen.Text = "";
            txtNgaysinh.Text = "";
            txtDiachi.Text = "";
            txtEmail.Text = "";
            cbKhoa.Text = "";
            cbGioitinh.Text = "";
            txtSdt.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                QuanLySinhVien selectedSinhVien = lst[e.RowIndex];
                txtMa.Text = selectedSinhVien.MaSV;
                txtTen.Text = selectedSinhVien.HoTen;
                txtNgaysinh.Text = selectedSinhVien.NgaySinh;
                txtDiachi.Text = selectedSinhVien.DiaChi;
                txtEmail.Text = selectedSinhVien.Email;
                cbGioitinh.Text = selectedSinhVien.GioiTinh;
                cbKhoa.Text = selectedSinhVien.Khoa;
                txtSdt.Text = selectedSinhVien.Sdt;
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hàng.");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu có hàng nào được chọn trong DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy chỉ số hàng đã chọn
                int rowIndex = dataGridView1.SelectedRows[0].Index;
                // Kiểm tra dữ liệu hợp lệ
                if (KiemTraDuLieu())
                {
                    // Cập nhật thông tin sinh viên
                    QuanLySinhVien selectedStudent = lst[rowIndex];
                    selectedStudent.MaSV = txtMa.Text;
                    selectedStudent.HoTen = txtTen.Text;
                    selectedStudent.NgaySinh = txtNgaysinh.Text;
                    selectedStudent.GioiTinh = cbGioitinh.Text;
                    selectedStudent.Khoa = cbKhoa.Text;
                    selectedStudent.DiaChi = txtDiachi.Text;
                    selectedStudent.Email = txtEmail.Text;
                    selectedStudent.Sdt = txtSdt.Text;
                    // Cập nhật DataGridView
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = lst;
                    MessageBox.Show("Thông tin sinh viên đã được cập nhật.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để sửa.");
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int RowIndex = dataGridView1.SelectedRows[0].Index;
                QuanLySinhVien sv = lst[RowIndex];
                lst.Remove(sv);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = lst;
                MessageBox.Show("Đã xóa sinh viên.");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hàng để xóa");
            }
        }
    }
}
