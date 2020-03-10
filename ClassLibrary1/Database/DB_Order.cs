using ClassLibrary1.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ClassLibrary1.Database
{
    /// <summary>
    /// This is the class DB_Order.
    /// </summary>
    public sealed class DB_Order
    {
        #region Instances

        private static volatile DB_Order _instance;
        private static object syncRoot = new object();
        private DatabaseString db;

        #endregion

        /// <summary>
        /// This is the constructor for the class DBOrder.
        /// </summary>
        private DB_Order()
        {
            db = DatabaseString.GetInstance();
        }

        #region Singleton funtions

        /// <summary>
        /// This is a multi threaded singleton for the class DBOrder.
        /// </summary>
        /// <returns>_instance</returns>
        public static DB_Order GetInstance()
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new DB_Order();
                    }
                }
            }

            return _instance;
        }

        #endregion

        #region Create funtions

        /// <summary>
        /// Creates am order in the database.
        /// </summary>
        /// <param name="order"></param>
        /// <returns>string</returns>
        public string CreateOrder(Order order)
        {
            string result;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "INSERT INTO Table_Order (price, isShipped, username) VALUES(@price, @isShipped, @username)";
                    cmd.Parameters.AddWithValue("price", order.Price);
                    cmd.Parameters.AddWithValue("isShipped", order.IsShipped);
                    cmd.Parameters.AddWithValue("username", order.Username);
                    cmd.ExecuteNonQuery();
                    connection.Close();

                    result = "Success";
                }
            }

            catch (Exception exception)
            {
                result = OrderException(exception).Username;
            }

            return result;
        }

        #endregion

        #region Search funtions

        /// <summary>
        /// List all orders from the database.
        /// </summary>
        /// <returns>List<Order></returns>
        public List<Order> ListAllOrders()
        {
            List<Order> orders;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_Order";

                    var reader = cmd.ExecuteReader();
                    orders = new List<Order>();

                    while (reader.Read())
                    {
                        Order order = new Order();
                        order.Id = reader.GetInt32(reader.GetOrdinal("id"));
                        order.Price = reader.GetDouble(reader.GetOrdinal("price"));
                        order.IsShipped = reader.GetBoolean(reader.GetOrdinal("isShipped"));
                        order.Username = reader.GetString(reader.GetOrdinal("username"));
                        orders.Add(order);
                    }

                    connection.Close();
                }
            }

            catch (Exception exception)
            {
                orders = new List<Order>
                {
                    OrderException(exception)
                };
            }

            return orders;
        }

        /// <summary>
        /// Finds an order in the database from its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Order</returns>
        public Order FindOrderById(int id)
        {
            Order order;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_Order WHERE id = @input";
                    cmd.Parameters.AddWithValue("input", id);

                    var reader = cmd.ExecuteReader();
                    reader.Read();
                    order = new Order
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        Price = reader.GetDouble(reader.GetOrdinal("price")),
                        IsShipped = reader.GetBoolean(reader.GetOrdinal("isShipped")),
                        Username = reader.GetString(reader.GetOrdinal("username"))
                    };

                    connection.Close();
                }
            }

            catch (Exception exception)
            {
                order = OrderException(exception);
            }

            return order;
        }

        /// <summary>
        /// Finds a list of orders in the database by their price.
        /// </summary>
        /// <param name="price"></param>
        /// <returns>List<Order></returns>
        public List<Order> FindOrderByPrice(double price)
        {
            List<Order> orders;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_Order WHERE price = @input";
                    cmd.Parameters.AddWithValue("input", price);

                    var reader = cmd.ExecuteReader();
                    orders = new List<Order>();

                    while (reader.Read())
                    {
                        Order order = new Order
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Price = reader.GetDouble(reader.GetOrdinal("price")),
                            IsShipped = reader.GetBoolean(reader.GetOrdinal("isShipped")),
                            Username = reader.GetString(reader.GetOrdinal("username"))
                        };
                        orders.Add(order);
                    }

                    connection.Close();
                }
            }

            catch (Exception exception)
            {
                orders = new List<Order>
                {
                    OrderException(exception)
                };
            }

            return orders;
        }

        /// <summary>
        /// Finds a list of orders in the database that is higher than the given price.
        /// </summary>
        /// <param name="price"></param>
        /// <returns>List<Order></returns>
        public List<Order> FindOrderByPriceHigher(double price)
        {
            List<Order> orders;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_Order WHERE price > @input";
                    cmd.Parameters.AddWithValue("input", price);

                    var reader = cmd.ExecuteReader();
                    orders = new List<Order>();

                    while (reader.Read())
                    {
                        Order order = new Order
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Price = reader.GetDouble(reader.GetOrdinal("price")),
                            IsShipped = reader.GetBoolean(reader.GetOrdinal("isShipped")),
                            Username = reader.GetString(reader.GetOrdinal("username"))
                        };
                        orders.Add(order);
                    }

                    connection.Close();
                }
            }

            catch (Exception exception)
            {
                orders = new List<Order>
                {
                    OrderException(exception)
                };
            }

            return orders;
        }

        /// <summary>
        /// Finds a list of orders in the database that is lower than the given price.
        /// </summary>
        /// <param name="price"></param>
        /// <returns>List<Order></returns>
        public List<Order> FindOrderByPriceLower(double price)
        {
            List<Order> orders;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_Order WHERE price < @input";
                    cmd.Parameters.AddWithValue("input", price);

                    var reader = cmd.ExecuteReader();
                    orders = new List<Order>();

                    while (reader.Read())
                    {
                        Order order = new Order
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Price = reader.GetDouble(reader.GetOrdinal("price")),
                            IsShipped = reader.GetBoolean(reader.GetOrdinal("isShipped")),
                            Username = reader.GetString(reader.GetOrdinal("username"))
                        };
                        orders.Add(order);
                    }

                    connection.Close();
                }
            }

            catch (Exception exception)
            {
                orders = new List<Order>
                {
                    OrderException(exception)
                };
            }

            return orders;
        }

        /// <summary>
        /// Finds a list of orders in the database by their is_shipped.
        /// </summary>
        /// <param name="is_shipped"></param>
        /// <returns>List<Order></returns>
        public List<Order> FindOrderByIsShipped(bool is_shipped)
        {
            List<Order> orders;


            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_Order WHERE isShipped = @input";
                    cmd.Parameters.AddWithValue("input", is_shipped);

                    var reader = cmd.ExecuteReader();
                    orders = new List<Order>();

                    while (reader.Read())
                    {
                        Order order = new Order
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Price = reader.GetDouble(reader.GetOrdinal("price")),
                            IsShipped = reader.GetBoolean(reader.GetOrdinal("isShipped")),
                            Username = reader.GetString(reader.GetOrdinal("username"))
                        };
                        orders.Add(order);
                    }

                    connection.Close();
                }
            }

            catch (Exception exception)
            {
                orders = new List<Order>
                {
                    OrderException(exception)
                };
            }

            return orders;
        }

        /// <summary>
        /// Finds a list of orders in the database by their username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>List<Order></returns>
        public List<Order> FindOrderByUsername(string username)
        {
            List<Order> orders;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_Order WHERE username = @input";
                    cmd.Parameters.AddWithValue("input", username);

                    var reader = cmd.ExecuteReader();
                    orders = new List<Order>();

                    while (reader.Read())
                    {
                        Order order = new Order
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Price = reader.GetDouble(reader.GetOrdinal("price")),
                            IsShipped = reader.GetBoolean(reader.GetOrdinal("isShipped")),
                            Username = reader.GetString(reader.GetOrdinal("username"))
                        };
                        orders.Add(order);
                    }

                    connection.Close();
                }
            }

            catch (Exception exception)
            {
                orders = new List<Order>
                {
                    OrderException(exception)
                };
            }

            return orders;
        }

        #endregion

        #region Update funtions

        /// <summary>
        /// UPdates an order in the database.
        /// </summary>
        /// <param name="order"></param>
        /// <returns>string</returns>
        public string UpdateOrder(Order order)
        {
            string result;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "UPDATE Table_Order SET price = @price, isShipped = @isShipped, username = @username WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", order.Id);
                    cmd.Parameters.AddWithValue("price", order.Price);
                    cmd.Parameters.AddWithValue("isShipped", order.IsShipped);
                    cmd.Parameters.AddWithValue("username", order.Username);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }

                result = "Success";
            }

            catch (Exception exception)
            {
                result = OrderException(exception).Username;
            }

            return result;
        }

        #endregion

        #region Delete funtions

        /// <summary>
        /// Deletes an order in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>string</returns>
        public string DeleteOrder(int id)
        {
            string result;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "DELETE FROM Table_Order WHERE id = @input";
                    cmd.Parameters.AddWithValue("input", id);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }

                result = "Success";
            }

            catch (Exception exception)
            {
                result = OrderException(exception).Username;
            }

            return result;
        }

        #endregion

        #region Exception funtions

        /// <summary>
        /// Returns a product with a exception message in it.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns>Order</returns>
        private Order OrderException(Exception exception)
        {
            Order product = new Order();

            product.Id = -5;
            product.Price = -5.0;
            product.IsShipped = false;
            product.Username = exception.Message;

            return product;
        }

        #endregion
    }
}