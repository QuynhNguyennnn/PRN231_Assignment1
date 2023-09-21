using BusinessObject;
using DataAccess.DAO;
using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public void CreateOrderDetail(OrderDetail o) => OrderDetailDAO.CreateOrderDetail(o);
        public void DeleteOrderDetail(OrderDetail o) => OrderDetailDAO.DeleteOrderDetail(o);
        public void UpdateOrderDetail(OrderDetail o) => OrderDetailDAO.UpdateOrderDetail(o);
        public OrderDetail GetOrderDetail(int productId, int orderId) => OrderDetailDAO.GetOrderDetail(productId, orderId);
        public List<OrderDetail> GetAllOrders(int orderId) => OrderDetailDAO.GetAllOrderDetails(orderId);
    }
}
