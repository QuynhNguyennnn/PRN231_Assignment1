using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interface
{
    public interface IOrderDetailRepository
    {
        void CreateOrderDetail(OrderDetail o);
        void DeleteOrderDetail(OrderDetail o);
        void UpdateOrderDetail(OrderDetail o);
        OrderDetail GetOrderDetail(int productId, int orderId);
        List<OrderDetail> GetAllOrders(int orderId);
    }
}
