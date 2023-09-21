using BusinessObject;
using System.Collections.Generic;

namespace DataAccess.repository
{
    public class OrderRepository : IOrderRepository
    {
        public Order GetOrderByID(int orderId) => OrderDAO.GetOrderById(orderId);
        public List<Order> GetOrders() => OrderDAO.GetOrders();
        public void InsertOrder(Order order) => OrderDAO.InsertOrder(order);
        public void DeleteOrder(Order order) => OrderDAO.DeleteOrder(order);
        public void UpdateOrder(Order order) => OrderDAO.UpdateOrder(order);
    }
}
