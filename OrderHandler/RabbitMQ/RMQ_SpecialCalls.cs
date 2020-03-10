using ClassLibrary1.Controller;
using ClassLibrary1.Model;
using System;
using System.Collections.Generic;

namespace OrderHandler.RabbitMQ
{
    /// <summary>
    /// This is the class RMQ_SpecialCalls
    /// </summary>
    public sealed class RMQ_SpecialCalls
    {
        private static volatile RMQ_SpecialCalls _instance;
        private static readonly object syncRoot = new object();

        private CTR_Order ctr_order;
        private CTR_OrderDetail ctr_orderdetail;
        private CTR_Product ctr_product;
        private CustomUserController ctr_user;
        private CTR_UserProduct ctr_userproduct;

        /// <summary>
        /// This is the constructor for the class RMQ_SpecialCalls.
        /// </summary>
        private RMQ_SpecialCalls()
        {
            this.ctr_order = new CTR_Order();
            this.ctr_orderdetail = new CTR_OrderDetail();
            this.ctr_product = new CTR_Product();
            this.ctr_user = new CustomUserController();
            this.ctr_userproduct = new CTR_UserProduct();
        }

        /// <summary>
        /// This is a multi threaded singleton for the class RMQ_SpecialCalls.
        /// </summary>
        /// <returns>_instance</returns>
        public static RMQ_SpecialCalls GetInstance()
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new RMQ_SpecialCalls();
                    }
                }
            }

            return _instance;
        }

        /// <summary>
        /// Functions for managing special calls.
        /// </summary>
        /// <param name="informations"></param>
        public void SpecialFunctions(List<string> informations)
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

            switch(informations[1])
            {
                case "Log In":
                    Login(new_informations);
                    break;

                case "Log Out":
                    Logout(new_informations);
                    break;

                case "Assign Session Key":
                    AssignSessionKey(new_informations);
                    break;

                case "Get Session Key":
                    //insert code here
                    break;

                case "Ship Orders":
                    ShipOrders(new_informations[0]);
                    break;

            }
        }

        /// <summary>
        /// Logs a user in to the system.
        /// </summary>
        /// <param name="informations"></param>
        public void Login(List<string> informations)
        {
            CustomUser user = new CustomUser
            {
                Username = informations[0],
                Password = informations[1],
                Email = informations[2],
                Wallet = Double.Parse(informations[3])
            };

            Console.WriteLine("Login a user");
            ctr_user.LogIn(user);
        }

        /// <summary>
        /// Logs a user out of the system.
        /// </summary>
        /// <param name="informations"></param>
        public void Logout(List<string> informations)
        {
            CustomUser user = new CustomUser
            {
                Username = informations[0],
                Password = informations[1],
                Email = informations[2],
                Wallet = Double.Parse(informations[3])
            };

            Console.WriteLine("Logout a user");
            ctr_user.LogIn(user);
        }

        /// <summary>
        /// Assigning a session key to a user.
        /// </summary>
        /// <param name="informations"></param>
        public void AssignSessionKey(List<string> informations)
        {
            CustomUser user = new CustomUser
            {
                Username = informations[0],
                Password = informations[1],
                Email = informations[2],
                Wallet = Double.Parse(informations[3])
            };

            Console.WriteLine("Assign a session key to a user");
            ctr_user.AssignSessionKey(user);
        }

        /// <summary>
        /// Ships all orders connected to a given user that isn't shipped.
        /// </summary>
        /// <param name="username"></param>
        public void ShipOrders(string username)
        {
            int index;
            Order order;
            List<OrderDetail> order_details;
            List<Order> orders;

            //Find orders connected to the given username
            orders = ctr_order.FindByUsername(username);

            index = 0;
            //sort all orders from the list that already is shipped
            while (index < orders.Count)
            {
                order = orders[index];

                if (order.IsShipped)
                {
                    orders.Remove(order);
                }

                else
                {
                    index++;
                }
            }

            //ships the unshipped orders to the user.
            foreach (Order current_order in orders)
            {
                OrderDetail temp_od = new OrderDetail
                {
                    OrderId = current_order.Id
                };

                order_details = ctr_orderdetail.Find(temp_od, "Order Id");

                foreach (OrderDetail order_detail in order_details)
                {
                    UserProduct user_product = new UserProduct
                    {
                        Username = username,
                        ProductId = order_detail.ProductId,
                        IsActive = false
                    };

                    Console.WriteLine("Create User Product: " + ctr_userproduct.Create(user_product));
                }

                //mark the order as shipped.
                current_order.IsShipped = true;
                Console.WriteLine("Update Order: " + ctr_order.Update(current_order));

                //Updates the wallet of the user.
                CustomUser user = ctr_user.FindByUsername(current_order.Username);
                user.Wallet -= current_order.Price;
                Console.WriteLine("Update User wallet: " + ctr_user.Update(user));
            }
        }
    }
}