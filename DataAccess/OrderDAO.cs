using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class OrderDAO
    {
        public static object Instance { get; internal set; }

        public static List<Order> GetOrders()
        {
            var listOrders = new List<Order>();
            try
            {
                using (var context = new eStoreContext())
                {
                    listOrders = context.Orders.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listOrders;
        }

        public static Order GetOrderById(int orderId)
        {
            Order o = new Order();
            try
            {
                using (var context = new eStoreContext())
                {
                    o = context.Orders.SingleOrDefault(x => x.OrderId == orderId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return o;
        }

        public static void InsertOrder(Order o)
        {
            try
            {
                using (var context = new eStoreContext())
                {
                    context.Orders.Add(o);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateOrder(Order o)
        {
            try
            {
                using (var context = new eStoreContext())
                {
                    context.Entry<Order>(o).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteOrder(Order o)
        {
            try
            {
                using (var context = new eStoreContext())
                {
                    var o1 = context.Orders.SingleOrDefault(x => x.OrderId.Equals(o.OrderId));
                    context.Orders.Remove(o1);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
