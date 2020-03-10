using ClassLibrary1.Controller;
using ClassLibrary1.Model;
using System;
using System.Collections.Generic;

namespace OrderHandler.RabbitMQ
{
    /// <summary>
    /// This is the class RMQ_OrderDetail.
    /// </summary>
    public sealed class RMQ_OrderDetail
    {
        private static volatile RMQ_OrderDetail _instance;
        private static readonly object syncRoot = new object();
        private CTR_OrderDetail ctr_orderdetail;

        /// <summary>
        /// This is the constructor for the class RMQ_OrderDetail.
        /// </summary>
        private RMQ_OrderDetail()
        {
            ctr_orderdetail = new CTR_OrderDetail();
        }

        /// <summary>
        /// This is a multi threaded singleton for the class RMQ_OrderDetail.
        /// </summary>
        /// <returns>_instance</returns>
        public static RMQ_OrderDetail GetInstance()
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new RMQ_OrderDetail();
                    }
                }
            }

            return _instance;
        }

        /// <summary>
        /// Functions for managing order details.
        /// </summary>
        /// <param name="informations"></param>
        public void OrderDetailFunctions(List<string> informations)
        {
            int index = 1;
            List<string> new_informations = new List<string>();

            while (index < informations.Count)
            {
                if (index > 1)
                {
                    new_informations.Add(informations[index]);
                }

                index++;
            }

            switch (informations[1])
            {
                case "Create":
                    CreateOrderDetail(new_informations);
                    break;

                case "ListAll":
                    //insert code here
                    break;

                case "Find":
                    //insert code here
                    break;

                case "Update":
                    UpdateOrderDetail(new_informations);
                    break;

                case "Delete":
                    DeleteOrderDetail(new_informations);
                    break;

                default:
                    Console.WriteLine("The function is not available: " + informations[1]);
                    break;

            }
        }

        /// <summary>
        /// Creates an new order detail.
        /// </summary>
        /// <param name="informations"></param>
        public void CreateOrderDetail(List<string> informations)
        {
            OrderDetail order_detail = new OrderDetail
            {
                OrderId = Int32.Parse(informations[0]),
                ProductId = Int32.Parse(informations[1])
            };

            Console.WriteLine("Create Order Detail: " + ctr_orderdetail.Create(order_detail));
        }

        /// <summary>
        /// Updates an order detail.
        /// </summary>
        /// <param name="informations"></param>
        public void UpdateOrderDetail(List<string> informations)
        {
            OrderDetail old_order_detail = new OrderDetail
            {
                OrderId = Int32.Parse(informations[0]),
                ProductId = Int32.Parse(informations[1])
            };

            OrderDetail new_order_detail = new OrderDetail
            {
                OrderId = Int32.Parse(informations[2]),
                ProductId = Int32.Parse(informations[3])
            };

            Console.WriteLine("Create Order Detail: " + ctr_orderdetail.Update(old_order_detail, new_order_detail));
        }

        /// <summary>
        /// Deletes an order detail.
        /// </summary>
        /// <param name="informations"></param>
        public void DeleteOrderDetail(List<string> informations)
        {
            OrderDetail order_detail = new OrderDetail
            {
                OrderId = Int32.Parse(informations[0]),
                ProductId = Int32.Parse(informations[1])
            };

            Console.WriteLine("Create Order Detail: " + ctr_orderdetail.Delete(order_detail, "Order Detail"));
        }
    }
}