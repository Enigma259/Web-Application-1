using ClassLibrary1.Database;
using ClassLibrary1.Model;
using System.Collections.Generic;

namespace ClassLibrary1.Controller
{
    /// <summary>
    /// This is the class CTR_OrderDetail.
    /// </summary>
    public sealed class CTR_OrderDetail
    {
        #region Instances

        private DB_OrderDetail db_orderDetail;

        #endregion

        /// <summary>
        /// This is the constructor for the class CTR_OrderDetail.
        /// </summary>
        public CTR_OrderDetail()
        {
            this.db_orderDetail = DB_OrderDetail.GetInstance();
        }

        #region Create functions

        /// <summary>
        /// Creates an order detail.
        /// </summary>
        /// <param name="order_detail"></param>
        /// <returns>string</returns>
        public string Create(OrderDetail order_detail)
        {
            return db_orderDetail.CreateOrderDetail(order_detail);
        }

        #endregion

        #region Search functions

        /// <summary>
        /// List all the order details.
        /// </summary>
        /// <returns>List<OrderDetail></returns>
        public List<OrderDetail> ListAll()
        {
            return db_orderDetail.ListAllOrderDetails();
        }

        /// <summary>
        /// Finds a list of order details by their id, orderId, or productId
        /// </summary>
        /// <param name="Order_detail"></param>
        /// <param name="id_type"></param>
        /// <returns>List<OrderDetail></returns>
        public List<OrderDetail> Find(OrderDetail Order_detail, string id_type)
        {
            List<OrderDetail> result;

            switch (id_type)
            {
                case "Order Id":
                    result = db_orderDetail.FindOrderDetailByOrderId(Order_detail.OrderId);
                    break;

                case "Product Id":
                    result = db_orderDetail.FindOrderDetailByProductId(Order_detail.ProductId);
                    break;

                case "Both":
                    result = new List<OrderDetail>
                    {
                        db_orderDetail.FindOrderDetail(Order_detail)
                    };
                    break;

                default:
                    result = new List<OrderDetail>();
                    break;
            }

            return result;
        }

        #endregion

        #region Update functions

        /// <summary>
        /// Updates an order detail.
        /// </summary>
        /// <param name="order_detail"></param>
        /// <returns>string</returns>
        public string Update(OrderDetail old_order_detail, OrderDetail new_order_detail)
        {
            return db_orderDetail.UpdateOrderDetail(old_order_detail, new_order_detail);
        }

        #endregion

        #region Delete functions

        /// <summary>
        /// Deletes an order detail.
        /// </summary>
        /// <param name="Order_detail"></param>
        /// <param name="call_origin"></param>
        /// <returns>string</returns>
        public string Delete(OrderDetail order_detail, string call_origin)
        {
            string result;

            result = db_orderDetail.DeleteOrderDetail(order_detail);

            return result;
        }

        #endregion
    }
}