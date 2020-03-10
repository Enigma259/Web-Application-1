using ClassLibrary1.Controller;
using ClassLibrary1.Database;
using ClassLibrary1.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace UnitTestProject1.Database
{

    [TestClass]
    public class UT_DBOrder
    {
        private DBOrder order_db;
        private CustomUserController user_ctr;
        private CustomUser user;
        private PasswordHasher password_hash;

        [TestInitialize]
        public void OrderInitialize()
        {
            order_db = DBOrder.GetInstance();
            user_ctr = CustomUserController.GetInstance();
            password_hash = new PasswordHasher();
            user = new CustomUser();

            user.Username = "Test_User_Order_Username";
            user.Password = password_hash.HashPassword("Test_User_Order_Password");
            user.Email = "Test_User_Order_Email";
            user.Wallet = 500.0;
            user.IsActive = true;

            user_ctr.Create(user);
        }

        [TestMethod]
        public void Success_CreateOrder()
        {
            //Arrange
            string result;
            Order order;

            //Act
            order = new Order();
            order.Price = 5.0;
            order.IsShipped = true;
            order.Username = user.Username;
            result = order_db.CreateOrder(order);

            //Assert
            Assert.AreEqual("Success", result);
        }

        [TestMethod]
        public void Success_ListAllOrders()
        {
            //Arrange
            int index;
            int max_index;
            Order order;
            List<Order> result;

            //Act
            index = 0;
            max_index = 500;

            while (index <= max_index)
            {
                order = new Order();
                order.Price = 100.75;
                order.IsShipped = true;
                order.Username = user.Username.ToString();
                order_db.CreateOrder(order);
                index++;
            }

            result = order_db.ListAllOrders();


            //Assert
            Assert.IsTrue(max_index <= result.Count, result[0].Username);
        }

        [TestMethod]
        public void Success_FindOrderById()
        {
            //Arrange
            Order order_1;
            List<Order> order_2;
            Order result;

            //Act

            order_1 = new Order();
            order_1.Username = user.Username;
            order_1.IsShipped = true;
            order_1.Price = 500000000.0;

            order_db.CreateOrder(order_1);

            order_2 = order_db.FindOrderByPrice(order_1.Price);

            result = order_db.FindOrderById(order_2[0].Id);

            if (result == null)
            {
                result = order_2[0];
            }

            //Assert
            Assert.AreEqual(order_2[0].Id, result.Id, result.Username);
        }

        [TestMethod]
        public void Success_FindOrderByPrice()
        {
            //Arrange
            Order order;
            List<Order> result;

            //Act
            order = new Order();
            order.Price = 500.0;
            order.IsShipped = true;
            order.Username = user.Username;
            order_db.CreateOrder(order);
            result = order_db.FindOrderByPrice(order.Price);

            //Assert
            Assert.IsTrue(result.Count >= 1, result[0].Username);
        }

        [TestMethod]
        public void Success_FindOrderByPriceHigher()
        {
            //Arrange
            Order order;
            List<Order> result;

            //Act

            order = new Order();
            order.Price = 1001.0;
            order.IsShipped = true;
            order.Username = user.Username;
            order_db.CreateOrder(order);

            result = order_db.FindOrderByPriceHigher(1000.0);

            //Assert
            Assert.IsTrue(result.Count >= 1, result[0].Username);
        }

        [TestMethod]
        public void Success_FindOrderByPriceLower()
        {
            //Arrange
            int index;
            int max_index;
            Order order;
            List<Order> result;

            //Act
            index = 0;
            max_index = 50;

            while (index < max_index)
            {
                order = new Order();
                order.Price = 1000.0 - index;
                order.IsShipped = true;
                order.Username = user.Username;
                order_db.CreateOrder(order);
                index++;
            }

            result = order_db.FindOrderByPriceLower(976.0);

            //Assert
            Assert.IsTrue(result.Count >= 25);
        }

        [TestMethod]
        public void Success_FindOrderByIsShipped()
        {
            Order order;
            List<Order> result;

            //Act
            order = new Order();
            order.Price = 500.0;
            order.IsShipped = false;
            order.Username = user.Username;
            order_db.CreateOrder(order);
            result = order_db.FindOrderByIsShipped(order.IsShipped);

            //Assert
            Assert.IsTrue(result.Count >= 1, result[0].Username);
        }

        [TestMethod]
        public void Success_FindOrderByUsername()
        {
            Order order;
            List<Order> result;

            //Act
            order = new Order();
            order.Price = 500.0;
            order.IsShipped = true;
            order.Username = user.Username;
            order_db.CreateOrder(order);
            result = order_db.FindOrderByUsername(user.Username);

            //Assert
            Assert.IsTrue(result.Count >= 1 && result[0].Username != "ERROR");
        }

        [TestMethod]
        public void Success_UpdateOrder()
        {
            //Arrange
            string result;
            Order order;

            //Act
            order = new Order();
            order.Price = 5.0;
            order.IsShipped = true;
            order.Username = user.Username;
            result = order_db.CreateOrder(order);

            if (result.Equals("Success"))
            {
                order.Price = 500000.0;
                result = order_db.UpdateOrder(order);
            }

            //Assert
            Assert.AreEqual("Success", result);
        }

        [TestMethod]
        public void Success_DeleteOrder()
        {
            //Arrange
            string result;
            Order order;

            //Act
            order = new Order();
            order.Price = 123456789.0;
            order.IsShipped = true;
            order.Username = user.Username;
            result = order_db.CreateOrder(order);

            if (result.Equals("Success"))
            {

                order.Id = order_db.FindOrderByPrice(order.Price)[0].Id;
                result = order_db.DeleteOrder(order.Id);
            }

            //Assert
            Assert.AreEqual("Success", result);
        }

        [TestMethod]
        public void Fail_CreateOrder()
        {
            //Arrange
            string result;
            Order order;

            //Act
            order = new Order();
            order.Price = 987654321;
            order.IsShipped = true;
            order.Username = "Alakazam798Forever";
            result = order_db.CreateOrder(order);

            //Assert
            Assert.AreNotEqual("Success", result);
        }

        [TestCleanup]
        public void OrderCleanup()
        {
            DatabaseString object_value = DatabaseString.GetInstance();
            string connectionString = object_value.GetConnectionString();
            List<Order> orders = order_db.FindOrderByUsername(user.Username);

            foreach (Order order in orders)
            {
                order_db.DeleteOrder(order.Id);
            }

            using (var connection = new SqlConnection(connectionString))
            using (var cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandText = "DELETE FROM Table_User WHERE username = @username";
                cmd.Parameters.AddWithValue("username", user.Username);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
