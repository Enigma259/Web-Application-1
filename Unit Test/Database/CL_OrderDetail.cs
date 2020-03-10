using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary1.Database;
using ClassLibrary1.Model;
using System.Collections.Generic;

namespace Unit_Test.Database
{
    [TestClass]
    public class CL_OrderDetail
    {

        #region Instances

        private DBCustomUser db_user;
        private DB_Order db_order;
        private DB_Product db_product;
        private DB_OrderDetail db_orderdetail;
        private CustomUser user;
        private List<Order> orders;
        private List<Product> products;
        private List<OrderDetail> orderdetails;

        #endregion

        #region Unit Test functions

        [TestInitialize]
        public void Initialize()
        {
            db_product = DB_Product.GetInstance();
            db_order = DB_Order.GetInstance();
            db_orderdetail = DB_OrderDetail.GetInstance();
            db_user = DBCustomUser.GetInstance();
            int index = 0;

            //Creates test user
            user = new CustomUser
            {
                Username = "ClassLibrary1 - Test OrderDetail Database - User",
                Password = "ClassLibrary1 - Test OrderDetail Database - User",
                Email = "ClassLibrary1 - Test OrderDetail Database - User",
                Wallet = 5000000.0,
            };

            db_user.CreateUser(user);

            //loop for test products and test orders.
            while (index < 100)
            {
                //Creates test product
                Product product = new Product
                {
                    Name = "ClassLibrary1 - Test OrderDetail Database - Product",
                    Price = 1500.0
                };

                //creates test order.
                Order order = new Order
                {
                    Price = 100.0,
                    IsShipped = true,
                    Username = user.Username
                };

                db_order.CreateOrder(order);
                db_product.CreateProduct(product);
                index++;
            }

            products = db_product.FindProductByName("ClassLibrary1 - Test OrderDetail Database - Product");
            orders = db_order.FindOrderByUsername(user.Username);
            orderdetails = new List<OrderDetail>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            //Deletes test products
            foreach(Product product in products)
            {
                db_product.DeleteProduct(product.Id);
            }

            //Deletes test orders
            foreach(Order order in orders)
            {
                db_order.DeleteOrder(order.Id);
            }

            //Deletes test userproduct
            foreach (OrderDetail order_detail in orderdetails)
            {
                db_orderdetail.DeleteOrderDetail(order_detail);
            }

            db_user.DeleteUser(user.Username);
        }

        #endregion

        #region Success functions

        [TestMethod]
        public void Success_CreateOrderDetail()
        {
            //Arrange
            int index;
            string result;
            OrderDetail order_detail;

            //Act
            index = 99;

            order_detail = new OrderDetail
            {
                OrderId = orders[index].Id,
                ProductId = products[index].Id
            };

            result = db_orderdetail.CreateOrderDetail(order_detail);

            orderdetails.Add(order_detail);

            //Assert
            Assert.AreEqual("Success", result);
        }

        [TestMethod]
        public void Success_ListAllOrderDetail()
        {
            //Arrange
            int index;
            int max_index;
            OrderDetail order_detail;
            List<OrderDetail> result;

            //Act
            index = 1;
            max_index = 50 + index;

            while (index < max_index)
            {
                order_detail = new OrderDetail
                {
                    OrderId = orders[index].Id,
                    ProductId = products[index].Id
                };

                db_orderdetail.CreateOrderDetail(order_detail);

                orderdetails.Add(order_detail);

                index++;
            }

            result = db_orderdetail.ListAllOrderDetails();

            //Assert
            Assert.IsTrue(result.Count >= 50);
        }

        
        [TestMethod]
        public void Success_FindOrderDetailByProductId()
        {
            //Arrange
            int index;
            OrderDetail order_detail;
            List<OrderDetail> result;

            //Act
            index = 51;

            order_detail = new OrderDetail
            {
                OrderId = orders[index].Id,
                ProductId = products[index].Id
            };

            db_orderdetail.CreateOrderDetail(order_detail);

            orderdetails.Add(order_detail);

            result = db_orderdetail.FindOrderDetailByOrderId(order_detail.OrderId);

            //Assert
            Assert.AreEqual(order_detail.OrderId, result[0].OrderId);
        }

        [TestMethod]
        public void Success_FindOrderDetail()
        {
            //Arrange
            int index;
            OrderDetail order_detail;
            List<OrderDetail> result;

            //Act
            index = 52;

            order_detail = new OrderDetail
            {
                OrderId = orders[index].Id,
                ProductId = products[index].Id
            };

            db_orderdetail.CreateOrderDetail(order_detail);

            orderdetails.Add(order_detail);

            result = db_orderdetail.FindOrderDetailByOrderId(order_detail.OrderId);

            //Assert
            Assert.AreEqual(order_detail.ProductId, result[0].ProductId);
        }

        [TestMethod]
        public void Success_UpdateOrderDetail()
        {
            //Arrange
            int index;
            string result;
            OrderDetail order_detail_1;
            OrderDetail order_detail_2;

            //Act
            index = 53;

            order_detail_1 = new OrderDetail
            {
                OrderId = orders[index].Id,
                ProductId = products[index].Id
            };

            result = db_orderdetail.CreateOrderDetail(order_detail_1);

            if (result.Equals("Success"))
            {
                index++;
                order_detail_2 = new OrderDetail
                {
                    OrderId = orders[index].Id,
                    ProductId = products[index].Id
                };

                result = db_orderdetail.UpdateOrderDetail(order_detail_1, order_detail_2);

                orderdetails.Add(order_detail_1);
                orderdetails.Add(order_detail_2);
            }

            //Assert
            Assert.AreEqual("Success", result);
        }

        [TestMethod]
        public void Success_DeleteOrderDetail()
        {
            //Arrange
            int index;
            string result;
            OrderDetail order_detail;

            //Act
            index = 55;

            order_detail = new OrderDetail
            {
                OrderId = orders[index].Id,
                ProductId = products[index].Id
            };

            result = db_orderdetail.CreateOrderDetail(order_detail);

            if (result.Equals("Success"))
            {
                result = db_orderdetail.DeleteOrderDetail(order_detail);

                orderdetails.Add(order_detail);
            }

            //Assert
            Assert.AreEqual("Success", result);
        }

        #endregion
    }
}