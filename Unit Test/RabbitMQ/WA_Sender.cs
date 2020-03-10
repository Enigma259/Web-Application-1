using System;
using System.Collections.Generic;
using ClassLibrary1.Controller;
using ClassLibrary1.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPI.RabbitMQ;

namespace Unit_Test.RabbitMQ
{
    [TestClass]
    public class WA_Sender
    {
        #region Success functions

        private RMQ_Order rmq_order;
        private RMQ_OrderDetail rmq_orderdetail;
        private RMQ_Product rmq_product;
        private RMQ_User rmq_user;
        private RMQ_UserProduct rmq_userproduct;
        private RMQ_SpecialCalls rmq_specialcalls;
        private RMQ_Sender rmq_sender;

        private CTR_Order ctr_order;
        private CTR_OrderDetail ctr_orderdetail;
        private CTR_Product ctr_product;
        private CTR_UserProduct ctr_userproduct;
        private CustomUserController ctr_user;
        private List<int> products;
        private double product_price;

        #endregion

        #region Unit Test functions

        [TestInitialize]
        public void Initialize()
        {
            //Connects to classes that handles RabbitMQ
            rmq_order = RMQ_Order.GetInstance();
            rmq_orderdetail = RMQ_OrderDetail.GetInstance();
            rmq_product = RMQ_Product.GetInstance();
            rmq_user = RMQ_User.GetInstance();
            rmq_userproduct = RMQ_UserProduct.GetInstance();
            rmq_specialcalls = RMQ_SpecialCalls.GetInstance();
            rmq_sender = RMQ_Sender.GetInstance();

            //Connects to classes that lies in the Control layer.
            ctr_order = new CTR_Order();
            ctr_orderdetail = new CTR_OrderDetail();
            ctr_product = new CTR_Product();
            ctr_userproduct = new CTR_UserProduct();
            ctr_user = new CustomUserController();

            //Gets the products for the tests
            List<Product> part_products = ctr_product.ListAll();
            products = new List<int>();
            product_price = 0.0;

            //Calsulate the total price for the products
            foreach (Product product in part_products)
            {
                products.Add(product.Id);
                product_price += product.Price;
            }
        }

        #endregion

        #region Success functions

        [TestMethod]
        public void Success_TestRabbitMQSender()
        {
            int index = 0;
            string result;

            while (index < 1000)
            {
                TestOrders(index);
                index++;
            }

            result = TestOrders(index);

            Assert.AreEqual("Success", result);
        }

        [TestMethod]
        public void Success_SendRandomMessages()
        {
            int index;
            int max_index;
            string result = "";
            List<string> informations;

            index = 0;
            max_index = 25000;
            informations = new List<string>();


            while (index <= max_index)
            {
                if (index == 0)
                {
                    informations = new List<string>
                    {
                        "start Mississippi counting"
                    };
                    result = rmq_sender.SendMessage(informations);
                }

                else if (index == max_index)
                {
                    informations = new List<string>
                    {
                        "no more Mississippi counting"
                    };
                    result = rmq_sender.SendMessage(informations);
                }

                else
                {
                    informations = new List<string>
                    {
                        index.ToString() + " Mississippi"
                    };
                    result = rmq_sender.SendMessage(informations);
                }

                index++;
            }

            Assert.AreSame("Success", result, result);
        }

        #endregion

        #region Other functions

        public string TestOrders(int index)
        {
            string result;
            CustomUser user;

            Console.WriteLine("Creating User");
            user = new CustomUser
            {
                Username = "Egekilde_" + index.ToString(),
                Password = "Uden Brus",
                Email = "1026985@ucn.dk",
                Wallet = 250000,
                IsActive = true,
                LoggedIn = false
            };

            Console.WriteLine("Saving User");
            ctr_user.Create(user);

            Console.WriteLine("saving order and send message");
            result = ctr_order.Create(user.Username, products);
            rmq_specialcalls.NewOrders(user.Username);

            return result;
        }

        #endregion
    }
}