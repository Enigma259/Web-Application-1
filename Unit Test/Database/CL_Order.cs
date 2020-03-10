using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary1.Database;
using ClassLibrary1.Model;
using System.Collections.Generic;

namespace Unit_Test.Database
{
    [TestClass]
    public class CL_Order
    {
        #region Instances

        private DBCustomUser db_user;
        private DB_Order db_order;
        private CustomUser user;

        #endregion

        #region Unit Test functions

        [TestInitialize]
        public void Initialize()
        {
            db_order = DB_Order.GetInstance();
            db_user = DBCustomUser.GetInstance();

            //Creates test user
            user = new CustomUser
            {
                Username = "Henrik Jacobsen",
                Password = "Lenovo Y700",
                Email = "1026985@ucn.dk",
                LoggedIn = true,
                IsActive = true,
                Wallet = 5000000.0,
            };

            db_user.CreateUser(user);
        }

        [TestCleanup]
        public void Cleanup()
        {
            List<Order> orders = db_order.FindOrderByUsername(user.Username);

            foreach (Order order in orders)
            {
                db_order.DeleteOrder(order.Id);
            }

            db_user.DeleteUser(user.Username);
        }

        #endregion

        #region Success functions

        [TestMethod]
        public void Success_CreateOrder()
        {
            //Arrange
            string result;
            Order order;

            //Act
            order = new Order
            {
                Price = 100.0,
                IsShipped = true,
                Username = user.Username
            };

            result = db_order.CreateOrder(order);

            //Assert
            Assert.AreEqual("Success", result);
        }

        [TestMethod]
        public void Success_ListAllOrders()
        {
            //Arrange
            int index;
            int max_index;
            List<Order> result;

            //Act
            index = 0;
            max_index = 100;

            while (index < max_index)
            {
                Order order = new Order
                {
                    Price = 101.0 + index,
                    IsShipped = true,
                    Username = user.Username
                };

                db_order.CreateOrder(order);

                index++;
            }

            result = db_order.ListAllOrders();

            //Assert
            Assert.IsTrue(result.Count >= max_index);
        }

        [TestMethod]
        public void Success_FindOrderById()
        {
            //Arrange
            int index;
            int max_index;
            Order order;
            Order result;

            //Act
            index = 0;
            max_index = 100;

            while(index < max_index)
            {
                order = new Order
                {
                    Price = 201.0,
                    IsShipped = true,
                    Username = user.Username
                };

                db_order.CreateOrder(order);
                index++;
            }

            order = db_order.FindOrderByPrice(201.0)[50];
            result = db_order.FindOrderById(order.Id);

            //Assert
            Assert.AreEqual(order.Username, result.Username);
        }

        [TestMethod]
        public void Success_FindOrderByPrice()
        {
            //Arrange
            int index;
            int max_index;
            List<Order> result;

            //Act
            index = 0;
            max_index = 100;

            while (index < max_index)
            {
                Order order = new Order
                {
                    Price = 301.0 + index,
                    IsShipped = true,
                    Username = user.Username
                };

                db_order.CreateOrder(order);

                index++;
            }

            result = db_order.FindOrderByPrice(350.0);

            //Assert
            Assert.AreEqual(350.0, result[0].Price);
        }

        [TestMethod]
        public void Success_FindOrderByPriceHigher()
        {
            //Arrange
            int index;
            int max_index;
            List<Order> result;

            //Act
            index = 0;
            max_index = 100;

            while (index < max_index)
            {
                Order order = new Order
                {
                    Price = 401.0 + index,
                    IsShipped = true,
                    Username = user.Username
                };

                db_order.CreateOrder(order);

                index++;
            }

            result = db_order.FindOrderByPriceHigher(450.0);

            //Assert
            Assert.IsTrue(result.Count >= 50);
        }

        [TestMethod]
        public void Success_FindOrderByPriceLower()
        {
            //Arrange
            int index;
            int max_index;
            List<Order> result;

            //Act
            index = 0;
            max_index = 100;

            while (index < max_index)
            {
                Order order = new Order
                {
                    Price = 501.0 + index,
                    IsShipped = true,
                    Username = user.Username
                };

                db_order.CreateOrder(order);

                index++;
            }

            result = db_order.FindOrderByPriceHigher(550.0);

            //Assert
            Assert.IsTrue(result.Count >= 50, result.Count.ToString());
        }

        [TestMethod]
        public void Success_FindOrderByIsShipped()
        {
            //Arrange
            int index;
            int max_index;
            List<Order> result;

            //Act
            index = 0;
            max_index = 100;

            while (index < max_index)
            {
                Order order = new Order
                {
                    Price = 601.0 + index,
                    IsShipped = false,
                    Username = user.Username
                };

                db_order.CreateOrder(order);

                index++;
            }

            result = db_order.FindOrderByIsShipped(false);

            //Assert
            Assert.IsTrue(result.Count >= max_index);
        }

        [TestMethod]
        public void Success_FindOrderByUsername()
        {
            //Arrange
            int index;
            int max_index;
            List<Order> result;

            //Act
            index = 0;
            max_index = 100;

            while (index < max_index)
            {
                Order order = new Order
                {
                    Price = 701.0 + index,
                    IsShipped = false,
                    Username = user.Username
                };

                db_order.CreateOrder(order);

                index++;
            }

            result = db_order.FindOrderByUsername(user.Username);

            //Assert
            Assert.AreEqual(user.Username, result[0].Username);
        }

        [TestMethod]
        public void Success_UpdateOrders()
        {
            //Arrange
            string result;
            Order order;

            //Act
            order = new Order
            {
                Price = 801.0,
                IsShipped = true,
                Username = user.Username
            };

            result = db_order.CreateOrder(order);

            if(result.Equals("Success"))
            {
                order.Id = db_order.FindOrderByPrice(order.Price)[0].Id;
                order.Price += 0.5;

                result = db_order.UpdateOrder(order);
            }

            //Assert
            Assert.AreEqual("Success", result);
        }

        [TestMethod]
        public void Success_DeleteOrders()
        {
            //Arrange
            string result;
            Order order;

            //Act
            order = new Order
            {
                Price = 802.0,
                IsShipped = true,
                Username = user.Username
            };

            result = db_order.CreateOrder(order);

            if (result.Equals("Success"))
            {
                order.Id = db_order.FindOrderByPrice(order.Price)[0].Id;

                result = db_order.DeleteOrder(order.Id);
            }

            //Assert
            Assert.AreEqual("Success", result);
        }

        #endregion
    }
}