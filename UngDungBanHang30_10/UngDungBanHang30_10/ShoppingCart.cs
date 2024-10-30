using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UngDungBanHang30_10
{
    public class ShoppingCart
    {
        private List<ShoppingCartItem> cartItems = new List<ShoppingCartItem>();

        public void AddToCart(Product product, int quantity)
        {
            var existingItem = cartItems.FirstOrDefault(item => item.Product == product);
            if (existingItem != null)
            {
                // Cộng dồn số lượng nếu sản phẩm đã có trong giỏ
                existingItem.Quantity += quantity;
                // Không cần thiết phải cập nhật lại TotalPrice ở đây vì đã có thuộc tính TotalPrice
            }
            else
            {
                // Thêm mới sản phẩm vào giỏ
                cartItems.Add(new ShoppingCartItem(product, quantity)); // Khởi tạo ShoppingCartItem mới
            }
        }


        public void RemoveFromCart(Product product)
        {
            var itemToRemove = cartItems.FirstOrDefault(item => item.Product == product);
            if (itemToRemove != null)
            {
                cartItems.Remove(itemToRemove);
            }
        }

        public List<ShoppingCartItem> GetCartItems()
        {
            return cartItems;
        }

        public decimal GetTotalPrice()
        {
            return cartItems.Sum(item => item.TotalPrice);
        }

        public void ClearCart()
        {
            cartItems.Clear();
        }
    }

   

}
