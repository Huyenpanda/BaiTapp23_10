using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UngDungBanHang30_10
{
    public partial class Form1 : Form
    {
        private List<Product> products = new List<Product>();
        private ShoppingCart cart = new ShoppingCart();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Thiết lập các cột cho DataGridView

            dataGridViewProducts.Columns.Add("ProductName", "Tên Sản Phẩm");
            dataGridViewProducts.Columns.Add("ProductPrice", "Giá");
            dataGridViewProducts.Columns.Add("ProductQuantity", "Số Lượng");
            dataGridViewProducts.Columns.Add("ProductImage", "Ảnh Sản Phẩm");

            // Khởi tạo danh sách sản phẩm mẫu
            products.Add(new Product("Sản phẩm A", 100000, 10, null));
            products.Add(new Product("Sản phẩm B", 200000, 5, null));
            products.Add(new Product("Sản phẩm C", 300000, 8, null));

            // Thêm sản phẩm vào DataGridView
            foreach (var product in products)
            {
                dataGridViewProducts.Rows.Add(product.Name, product.Price, product.Quantity, product.Image);
            }
            dataGridViewCart.Columns.Add("ProductName", "Tên Sản Phẩm");
            dataGridViewCart.Columns.Add("ProductPrice", "Giá");
            dataGridViewCart.Columns.Add("ProductImage", "Ảnh Sản Phẩm");
            dataGridViewCart.Columns.Add("ProductQuantity", "Tổng Số Lượng");
            dataGridViewCart.Columns.Add("TotalPrice", "Tổng Giá Trị Đơn Hàng");
        }
        private void buttonAddToCart_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.CurrentRow != null)
            {
                var selectedProduct = products[dataGridViewProducts.CurrentRow.Index];

                // Lấy số lượng từ cột "Số Lượng" trong DataGridView
                int quantityToAdd = Convert.ToInt32(dataGridViewProducts.CurrentRow.Cells["ProductQuantity"].Value);

                // Thêm vào giỏ hàng
                cart.AddToCart(selectedProduct, quantityToAdd);
                UpdateCart();
            }
        }


        private void UpdateCart()
        {
            dataGridViewCart.Rows.Clear(); // Xóa tất cả các hàng hiện tại trong giỏ hàng
            foreach (var item in cart.GetCartItems())
            {
                // Thêm hàng vào DataGridView giỏ hàng
                dataGridViewCart.Rows.Add(item.Product.Name, item.Product.Price, item.Product.Image, item.Quantity, item.TotalPrice);
            }
        }


        private void buttonRemoveFromCart_Click(object sender, EventArgs e)
        {
            if (dataGridViewCart.CurrentRow != null)
            {
                var selectedCartItem = cart.GetCartItems()[dataGridViewCart.CurrentRow.Index].Product;
                cart.RemoveFromCart(selectedCartItem);
                UpdateCart();
            }
        }

        private void buttonCheckout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Xác nhận thanh toán! Tổng giá trị: " + cart.GetTotalPrice().ToString("C"));
            cart.ClearCart();
            UpdateCart();
        }

      



    }
}

