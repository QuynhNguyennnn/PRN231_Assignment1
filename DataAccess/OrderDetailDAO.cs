using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class OrderDetailDAO
    {
        public static List<OrderDetail> GetAllOrderDetails(int orderId)
        {
            List<OrderDetail> list = new List<OrderDetail>();
            try
            {
                using (var context = new eStoreContext())
                {
                    list = context.OrderDetails.Where(o => o.OrderId == orderId).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public static OrderDetail GetOrderDetail(int productId, int orderId)
        {
            OrderDetail o = new OrderDetail();
            try
            {
                using (var context = new eStoreContext())
                {
                    o = context.OrderDetails.SingleOrDefault(o => o.ProductId == productId && o.OrderId == orderId);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return o;
        }

        public static void CreateOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                using (var context = new eStoreContext())
                {
                    context.OrderDetails.Add(orderDetail);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                using (var context = new eStoreContext())
                {
                    context.Entry<OrderDetail>(orderDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeleteOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                using (var context = new eStoreContext())
                {
                    var _orderDetail = context.OrderDetails.SingleOrDefault(o => o.ProductId == orderDetail.ProductId && o.OrderId == orderDetail.OrderId);
                    context.OrderDetails.Remove(_orderDetail);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
