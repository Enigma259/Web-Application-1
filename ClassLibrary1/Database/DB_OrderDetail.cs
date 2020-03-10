using ClassLibrary1.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ClassLibrary1.Database
{
    /// <summary>
    /// This is the class DB_OrderDetail.
    /// </summary>
    public sealed class DB_OrderDetail
    {
        #region Instances

        private static volatile DB_OrderDetail _instance;
        private static readonly object syncRoot = new object();
        private DatabaseString db;

        #endregion

        /// <summary>
        /// This is the conctructor for the class DB_OrderDetail.
        /// </summary>
        private DB_OrderDetail()
        {
            db = DatabaseString.GetInstance();
        }

        #region Singleton functions

        /// <summary>
        /// This is a multi threaded singleton for the class DB_OrderDetail.
        /// </summary>
        /// <returns>_instance</returns>
        public static DB_OrderDetail GetInstance()
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new DB_OrderDetail();
                    }
                }
            }

            return _instance;
        }

        #endregion

        #region Create functions

        /// <summary>
        /// Creates an order detail in the database.
        /// </summary>
        /// <param name="order_detail"></param>
        /// <returns>string</returns>
        public string CreateOrderDetail(OrderDetail order_detail)
        {
            string result;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "INSERT INTO Table_OrderDetail (orderId, productId) VALUES(@orderId, @productId)";
                    cmd.Parameters.AddWithValue("orderId", order_detail.OrderId);
                    cmd.Parameters.AddWithValue("productId", order_detail.ProductId);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }

                result = "Success";
            }

            catch (Exception exception)
            {
                result = exception.Message;
            }

            return result;
        }

        #endregion

        #region Search functions

        /// <summary>
        /// Lists all the order details from the databse.
        /// </summary>
        /// <returns>List<OrderDetail></returns>
        public List<OrderDetail> ListAllOrderDetails()
        {
            List<OrderDetail> result;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_OrderDetail";
                    var reader = cmd.ExecuteReader();

                    result = new List<OrderDetail>();

                    while (reader.Read())
                    {
                        OrderDetail order_detail = new OrderDetail
                        {
                            OrderId = reader.GetInt32(reader.GetOrdinal("orderId")),
                            ProductId = reader.GetInt32(reader.GetOrdinal("productId"))
                        };
                        result.Add(order_detail);
                    }

                    connection.Close();
                }
            }

            catch (Exception exception)
            {
                result = new List<OrderDetail>
                {
                    ExceptionMessage(exception)
                };
            }

            return result;
        }

        /// <summary>
        /// Finds a list of order details from the database by its order_id.
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns>List<OrderDetail></returns>
        public List<OrderDetail> FindOrderDetailByOrderId(int order_id)
        {
            List<OrderDetail> result;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_OrderDetail WHERE orderId = @orderId";
                    cmd.Parameters.AddWithValue("orderId", order_id);
                    var reader = cmd.ExecuteReader();

                    result = new List<OrderDetail>();

                    while (reader.Read())
                    {
                        OrderDetail order_detail = new OrderDetail
                        {
                            OrderId = reader.GetInt32(reader.GetOrdinal("orderId")),
                            ProductId = reader.GetInt32(reader.GetOrdinal("productId"))
                        };
                        result.Add(order_detail);
                    }

                    connection.Close();
                }
            }

            catch (Exception exception)
            {
                result = new List<OrderDetail>
                {
                    ExceptionMessage(exception)
                };
            }

            return result;
        }

        /// <summary>
        /// Finds a list of order details from the database by its product_id.
        /// </summary>
        /// <param name="product_id"></param>
        /// <returns>List<OrderDetail></returns>
        public List<OrderDetail> FindOrderDetailByProductId(int product_id)
        {
            List<OrderDetail> result;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_OrderDetail WHERE productId = @productId";
                    cmd.Parameters.AddWithValue("productId", product_id);
                    var reader = cmd.ExecuteReader();

                    result = new List<OrderDetail>();

                    while (reader.Read())
                    {
                        OrderDetail order_detail = new OrderDetail
                        {
                            OrderId = reader.GetInt32(reader.GetOrdinal("orderId")),
                            ProductId = reader.GetInt32(reader.GetOrdinal("productId"))
                        };
                        result.Add(order_detail);
                    }

                    connection.Close();
                }
            }

            catch (Exception exception)
            {
                result = new List<OrderDetail>
                {
                    ExceptionMessage(exception)
                };
            }

            return result;
        }

        /// <summary>
        /// Finds a list of order details from the database by its order_id.
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns>OrderDetail</returns>
        public OrderDetail FindOrderDetail(OrderDetail order_detail)
        {
            OrderDetail result;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_OrderDetail WHERE productId = @productId AND orderId = @orderId";
                    cmd.Parameters.AddWithValue("productId", order_detail.ProductId);
                    cmd.Parameters.AddWithValue("orderId", order_detail.OrderId);

                    var reader = cmd.ExecuteReader();

                    reader.Read();
                    result = new OrderDetail
                    {
                        OrderId = reader.GetInt32(reader.GetOrdinal("orderId")),
                        ProductId = reader.GetInt32(reader.GetOrdinal("productId"))
                    };

                    connection.Close();
                }
            }

            catch (Exception exception)
            {
                result = ExceptionMessage(exception);
            }

            return result;
        }

        #endregion

        #region Update functions

        /// <summary>
        /// Updates an order detail in the database.
        /// </summary>
        /// <param name="order_detail"></param>
        /// <returns>string</returns>
        public string UpdateOrderDetail(OrderDetail old_order_detail, OrderDetail new_order_detail)
        {
            string result;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "UPDATE Table_OrderDetail SET productId = @new_productId, orderId = @new_orderId WHERE productId = @old_productId AND orderId = @old_orderId";
                    cmd.Parameters.AddWithValue("new_productId", new_order_detail.ProductId);
                    cmd.Parameters.AddWithValue("new_orderId", new_order_detail.OrderId);
                    cmd.Parameters.AddWithValue("old_productId", old_order_detail.ProductId);
                    cmd.Parameters.AddWithValue("old_orderId", old_order_detail.OrderId);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }

                result = "Success";
            }

            catch (Exception exception)
            {
                result = exception.Message;
            }

            return result;
        }

        #endregion

        #region Delete functions

        /// <summary>
        /// Deletes an order detail from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>string</returns>
        public string DeleteOrderDetail(OrderDetail order_detail)
        {
            string result;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "DELETE FROM Table_OrderDetail WHERE productId = @productId AND orderId = @orderId";
                    cmd.Parameters.AddWithValue("productId", order_detail.ProductId);
                    cmd.Parameters.AddWithValue("orderId", order_detail.OrderId);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }

                result = "Success";
            }

            catch (Exception exception)
            {
                result = exception.Message;
            }

            return result;
        }

        #endregion

        #region Exception functions

        /// <summary>
        /// Returns an order detail as an failed order detail retrieval.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns>OrderDetail</returns>
        public OrderDetail ExceptionMessage(Exception exception)
        {
            OrderDetail order_detail = new OrderDetail
            {
                OrderId = -5,
                ProductId = -5
            };
            return order_detail;
        }

        #endregion
    }
}