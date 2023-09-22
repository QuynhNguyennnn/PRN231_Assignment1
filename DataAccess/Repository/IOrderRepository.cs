using BusinessObject;
using System;
using System.Collections.Generic;

namespace DataAccess.repository
{
    public interface IOrderRepository
    {
        List<Order> GetOrders();
        Order GetOrderByID(int orderId);
        void DeleteOrder(Order order);
        void InsertOrder(Order order);
        void UpdateOrder(Order order);
        List<Order> SortOrder(DateTime dateStart, DateTime dateEnd);
    }
}
