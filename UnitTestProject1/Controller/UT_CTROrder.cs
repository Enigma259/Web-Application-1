using ClassLibrary1.Controller;
using ClassLibrary1.Database;
using ClassLibrary1.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace UnitTestProject1.Controller
{
    [TestClass]
    public class UT_CTROrder
    {
        private CTROrder order_ctr;
        private CustomUserController user_ctr;
        private CustomUser user;
        private PasswordHasher password_hash;

        [TestInitialize]
        public void OrderInitialize()
        {
            order_ctr = CTROrder.GetInstance();
            user_ctr = CustomUserController.GetInstance();
            password_hash = new PasswordHasher();
            user = new CustomUser();

            user.Username = "Test User - Username";
            user.Password = password_hash.HashPassword("Test User - Password");
            user.Email = "Test User - Email";
            user.Wallet = 500.0;
            user.IsActive = true;

            user_ctr.Create(user);
        }

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
            result = order_ctr.Create(order);

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
            max_index = 50;

            while (index < max_index)
            {
                order = new Order();
                order.Price = 10.0 + index;
                order.IsShipped = true;
                order.Username = user.Username;
                order_ctr.Create(order);
                index++;
            }

            result = order_ctr.ListAll();

            //Assert
            Assert.IsTrue(result.Count >= max_index);
        }

        [TestMethod]
        public void Success_FindOrderById()
        {
            //Arrange
            Order order;
            Order result;

            //Act
            order = new Order();
            order.Price = 100.0;
            order.IsShipped = true;
            order.Username = user.Username;
            order_ctr.Create(order);
            order.Id = order_ctr.FindByPrice(order.Price, "Equal")[0].Id;
            result = order_ctr.FindById(order.Id);

            //Assert
            Assert.AreEqual(order, result);
        }

        [TestMethod]
        public void Success_FindOrderByPrice()
        {
            Order order;
            Order result;

            //Act
            order = new Order();
            order.Price = 500.0;
            order.IsShipped = true;
            order.Username = user.Username;
            order_ctr.Create(order);
            result = order_ctr.FindByPrice(order.Price, "Equal")[0];

            //Assert
            Assert.AreEqual(result.Price, order.Price);
        }

        [TestMethod]
        public void Success_FindOrderByPriceHigher()
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
                order.Price = 1001.0 + index;
                order.IsShipped = true;
                order.Username = user.Username;
                order_ctr.Create(order);
                index++;
            }

            result = order_ctr.FindByPrice(1025.0, "Higher");

            //Assert
            Assert.IsTrue(result.Count >= 25);
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
                order_ctr.Create(order);
                index++;
            }

            result = order_ctr.FindByPrice(976.0, "Lower");

            //Assert
            Assert.IsTrue(result.Count >= 25);
        }

        [TestMethod]
        public void Success_FindOrderByIsShipped()
        {
            Order order;
            Order result;

            //Act
            order = new Order();
            order.Price = 500.0;
            order.IsShipped = false;
            order.Username = user.Username;
            order_ctr.Create(order);
            result = order_ctr.FindByIsShipped(order.IsShipped)[0];

            //Assert
            Assert.AreEqual(result.IsShipped, order.IsShipped);
        }

        [TestMethod]
        public void Success_FindOrderByUsername()
        {
            Order order;
            Order result;

            //Act
            order = new Order();
            order.Price = 500.0;
            order.IsShipped = false;
            order.Username = user.Username;
            order_ctr.Create(order);
            result = order_ctr.FindByUsername(order.Username)[0];

            //Assert
            Assert.AreEqual(result.Username, order.Username);
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
            result = order_ctr.Create(order);

            if (result.Equals("Success"))
            {
                order.Price = 500000.0;
                result = order_ctr.Update(order);
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
            result = order_ctr.Create(order);

            if (result.Equals("Success"))
            {
                order.Id = order_ctr.FindByPrice(order.Price, "Equal")[0].Id;
                result = order_ctr.Delete(order.Id);
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
            order.Username = "Alakazam798ForeverAndEver";
            result = order_ctr.Create(order);

            //Assert
            Assert.AreNotEqual("Success", result);
        }

        [TestCleanup]
        public void OrderCleanup()
        {
            DatabaseString object_value = DatabaseString.GetInstance();
            string connectionString = object_value.GetConnectionString();
            List<Order> orders = order_ctr.FindByUsername(user.Username);

            foreach (Order order in orders)
            {
                order_ctr.Delete(order.Id);
            }

            using (var connection = new SqlConnection(connectionString))
            using (var cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandText = "DELETE FROM CustomAuthentication WHERE username = @username";
                cmd.Parameters.AddWithValue("username", user.Username);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
