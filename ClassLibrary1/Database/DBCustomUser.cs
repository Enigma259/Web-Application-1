using ClassLibrary1.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace ClassLibrary1.Database
{
    /// <summary>
    /// This is the class DBCustomUser.
    /// </summary>
    public class DBCustomUser
    {
        #region Instances

        private static volatile DBCustomUser _instance;
        private static object syncRoot = new object();
        private DatabaseString db;
        private PasswordHasher pw = new PasswordHasher();

        #endregion

        /// <summary>
        /// This is the constructor for the class DBCustomUser.
        /// </summary>
        private DBCustomUser()
        {
            db = DatabaseString.GetInstance();
        }

        #region Singleton funtions

        /// <summary>
        /// This is a multi threaded singleton for the class DBCustomUser.
        /// </summary>
        /// <returns>_instance</returns>
        public static DBCustomUser GetInstance()
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new DBCustomUser();
                    }
                }
            }

            return _instance;
        }

        #endregion

        #region Create functions

        /// <summary>
        /// Creas an user in the database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>string</returns>
        public string CreateUser(CustomUser user)
        {
            string result;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "INSERT INTO Table_User (username, userPassword, email, isActive, loggedIn, wallet) VALUES(@username, @userPassword, @email, @isActive, @loggedIn, @wallet)";
                    cmd.Parameters.AddWithValue("username", user.Username);
                    cmd.Parameters.AddWithValue("userPassword", user.Password);
                    cmd.Parameters.AddWithValue("email", user.Email);
                    cmd.Parameters.AddWithValue("isActive", user.IsActive);
                    cmd.Parameters.AddWithValue("loggedIn", user.IsActive);
                    cmd.Parameters.AddWithValue("wallet", user.Wallet);
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
        /// List all users from the database.
        /// </summary>
        /// <returns>List<CustomUser></returns>
        public List<CustomUser> ListAllUsers()
        {
            CustomUser user;
            List<CustomUser> result;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_User";
                    var reader = cmd.ExecuteReader();

                    result = new List<CustomUser>();

                    while (reader.Read())
                    {
                        user = new CustomUser
                        {
                            Username = reader.GetString(reader.GetOrdinal("username")),
                            Password = reader.GetString(reader.GetOrdinal("userPassword")),
                            Email = reader.GetString(reader.GetOrdinal("email")),
                            IsActive = reader.GetBoolean(reader.GetOrdinal("isActive")),
                            LoggedIn = reader.GetBoolean(reader.GetOrdinal("loggedIn")),
                            Wallet = reader.GetDouble(reader.GetOrdinal("wallet"))
                        };
                    }

                    connection.Close();
                }
            }

            catch (Exception exception)
            {
                result = new List<CustomUser>
                {
                    ExceptionMessage(exception)
                };
            }

            return result;
        }

        /// <summary>
        /// Finds an user by its username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>CustomUser</returns>
        public CustomUser FindUserByUsername(string username)
        {
            CustomUser result;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_User WHERE username = @input";
                    cmd.Parameters.AddWithValue("input", username);
                    var reader = cmd.ExecuteReader();
                    reader.Read();
                    result = new CustomUser
                    {
                        Username = reader.GetString(reader.GetOrdinal("username")),
                        Password = reader.GetString(reader.GetOrdinal("userPassword")),
                        Email = reader.GetString(reader.GetOrdinal("email")),
                        IsActive = reader.GetBoolean(reader.GetOrdinal("isActive")),
                        LoggedIn = reader.GetBoolean(reader.GetOrdinal("loggedIn")),
                        Wallet = reader.GetDouble(reader.GetOrdinal("wallet"))
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

        /// <summary>
        /// Finds a list of users by their passowrd.
        /// </summary>
        /// <param name="password"></param>
        /// <returns>List<CustomUser></returns>
        public List<CustomUser> FindUserByPassword(string password)
        {
            CustomUser user;
            List<CustomUser> result;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_User WHERE userPassword = @input";
                    cmd.Parameters.AddWithValue("input", password);
                    var reader = cmd.ExecuteReader();

                    result = new List<CustomUser>();

                    while (reader.Read())
                    {
                        user = new CustomUser
                        {
                            Username = reader.GetString(reader.GetOrdinal("username")),
                            Password = reader.GetString(reader.GetOrdinal("userPassword")),
                            Email = reader.GetString(reader.GetOrdinal("email")),
                            IsActive = reader.GetBoolean(reader.GetOrdinal("isActive")),
                            LoggedIn = reader.GetBoolean(reader.GetOrdinal("loggedIn")),
                            Wallet = reader.GetDouble(reader.GetOrdinal("wallet"))
                        };
                        result.Add(user);
                    }

                    connection.Close();
                }
            }

            catch (Exception exception)
            {
                result = new List<CustomUser>
                {
                    ExceptionMessage(exception)
                };
            }

            return result;
        }

        /// <summary>
        /// Finds an user by its email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>CustomUser</returns>
        public CustomUser FindUserByEmail(string email)
        {
            CustomUser result;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_User WHERE email = @input";
                    cmd.Parameters.AddWithValue("input", email);
                    var reader = cmd.ExecuteReader();
                    reader.Read();
                    result = new CustomUser
                    {
                        Username = reader.GetString(reader.GetOrdinal("username")),
                        Password = reader.GetString(reader.GetOrdinal("userPassword")),
                        Email = reader.GetString(reader.GetOrdinal("email")),
                        IsActive = reader.GetBoolean(reader.GetOrdinal("isActive")),
                        LoggedIn = reader.GetBoolean(reader.GetOrdinal("loggedIn")),
                        Wallet = reader.GetDouble(reader.GetOrdinal("wallet"))
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

        /// <summary>
        /// Finds a list of users by their is_active.
        /// </summary>
        /// <param name="is_active"></param>
        /// <returns>List<CustomUser></returns>
        public List<CustomUser> FindUserByIsactive(bool is_active)
        {
            CustomUser user;
            List<CustomUser> result;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_User WHERE isActive = inpput";
                    cmd.Parameters.AddWithValue("input", is_active);
                    var reader = cmd.ExecuteReader();

                    result = new List<CustomUser>();

                    while (reader.Read())
                    {
                        user = new CustomUser
                        {
                            Username = reader.GetString(reader.GetOrdinal("username")),
                            Password = reader.GetString(reader.GetOrdinal("userPassword")),
                            Email = reader.GetString(reader.GetOrdinal("email")),
                            IsActive = reader.GetBoolean(reader.GetOrdinal("isActive")),
                            LoggedIn = reader.GetBoolean(reader.GetOrdinal("loggedIn")),
                            Wallet = reader.GetDouble(reader.GetOrdinal("wallet"))
                        };
                        result.Add(user);
                    }

                    connection.Close();
                }
            }

            catch (Exception exception)
            {
                result = new List<CustomUser>
                {
                    ExceptionMessage(exception)
                };
            }

            return result;
        }

        /// <summary>
        /// Finds a list of users by their logged_in.
        /// </summary>
        /// <param name="logged_in"></param>
        /// <returns>List<CustomUser></returns>
        public List<CustomUser> FindUserByLoggedIn(bool logged_in)
        {
            CustomUser user;
            List<CustomUser> result;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_User WHERE loggedIn = inpput";
                    cmd.Parameters.AddWithValue("input", logged_in);
                    var reader = cmd.ExecuteReader();

                    result = new List<CustomUser>();

                    while (reader.Read())
                    {
                        user = new CustomUser
                        {
                            Username = reader.GetString(reader.GetOrdinal("username")),
                            Password = reader.GetString(reader.GetOrdinal("userPassword")),
                            Email = reader.GetString(reader.GetOrdinal("email")),
                            IsActive = reader.GetBoolean(reader.GetOrdinal("isActive")),
                            LoggedIn = reader.GetBoolean(reader.GetOrdinal("loggedIn")),
                            Wallet = reader.GetDouble(reader.GetOrdinal("wallet"))
                        };
                        result.Add(user);
                    }

                    connection.Close();
                }
            }

            catch (Exception exception)
            {
                result = new List<CustomUser>
                {
                    ExceptionMessage(exception)
                };
            }

            return result;
        }

        /// <summary>
        /// Finds a list of users by their wallet.
        /// </summary>
        /// <param name="wallet"></param>
        /// <returns>List<CustomUser></returns>
        public List<CustomUser> FindUserByWallet(double wallet)
        {
            CustomUser user;
            List<CustomUser> result;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_User WHERE wallet = inpput";
                    cmd.Parameters.AddWithValue("input", wallet);
                    var reader = cmd.ExecuteReader();

                    result = new List<CustomUser>();

                    while (reader.Read())
                    {
                        user = new CustomUser
                        {
                            Username = reader.GetString(reader.GetOrdinal("username")),
                            Password = reader.GetString(reader.GetOrdinal("userPassword")),
                            Email = reader.GetString(reader.GetOrdinal("email")),
                            IsActive = reader.GetBoolean(reader.GetOrdinal("isActive")),
                            LoggedIn = reader.GetBoolean(reader.GetOrdinal("loggedIn")),
                            Wallet = reader.GetDouble(reader.GetOrdinal("wallet"))
                        };
                        result.Add(user);
                    }

                    connection.Close();
                }
            }

            catch (Exception exception)
            {
                result = new List<CustomUser>
                {
                    ExceptionMessage(exception)
                };
            }

            return result;
        }

        /// <summary>
        /// Finds a list of user by their wallet and the wallet have to be higher than the given value.
        /// </summary>
        /// <param name="wallet"></param>
        /// <returns>List<CustomUser></returns>
        public List<CustomUser> FindUserByWalletHigher(double wallet)
        {
            CustomUser user;
            List<CustomUser> result;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_User WHERE wallet > inpput";
                    cmd.Parameters.AddWithValue("input", wallet);
                    var reader = cmd.ExecuteReader();

                    result = new List<CustomUser>();

                    while (reader.Read())
                    {
                        user = new CustomUser
                        {
                            Username = reader.GetString(reader.GetOrdinal("username")),
                            Password = reader.GetString(reader.GetOrdinal("userPassword")),
                            Email = reader.GetString(reader.GetOrdinal("email")),
                            IsActive = reader.GetBoolean(reader.GetOrdinal("isActive")),
                            LoggedIn = reader.GetBoolean(reader.GetOrdinal("loggedIn")),
                            Wallet = reader.GetDouble(reader.GetOrdinal("wallet"))
                        };
                        result.Add(user);
                    }

                    connection.Close();
                }
            }

            catch (Exception exception)
            {
                result = new List<CustomUser>
                {
                    ExceptionMessage(exception)
                };
            }

            return result;
        }

        /// <summary>
        /// Finds a list of user by their wallet and the wallet have to be lower than the given value.
        /// </summary>
        /// <param name="wallet"></param>
        /// <returns>List<CustomUser></returns>
        public List<CustomUser> FindUserByWalletLower(double wallet)
        {
            CustomUser user;
            List<CustomUser> result;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_User WHERE wallet < inpput";
                    cmd.Parameters.AddWithValue("input", wallet);
                    var reader = cmd.ExecuteReader();

                    result = new List<CustomUser>();

                    while (reader.Read())
                    {
                        user = new CustomUser
                        {
                            Username = reader.GetString(reader.GetOrdinal("username")),
                            Password = reader.GetString(reader.GetOrdinal("userPassword")),
                            Email = reader.GetString(reader.GetOrdinal("email")),
                            IsActive = reader.GetBoolean(reader.GetOrdinal("isActive")),
                            LoggedIn = reader.GetBoolean(reader.GetOrdinal("loggedIn")),
                            Wallet = reader.GetDouble(reader.GetOrdinal("wallet"))
                        };
                        result.Add(user);
                    }

                    connection.Close();
                }
            }

            catch (Exception exception)
            {
                result = new List<CustomUser>
                {
                    ExceptionMessage(exception)
                };
            }

            return result;
        }

        #endregion

        #region Update functions

        /// <summary>
        /// UPdates an user in the database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>string</returns>
        public string UpdateUser(CustomUser user)
        {
            string result;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "UPDATE Table_User SET userPassword = @userPassword, email = @email, isActive = @isActive, loggedIn = @loggedIn, wallet = @wallet WHERE username = @username";
                    cmd.Parameters.AddWithValue("username", user.Username);
                    cmd.Parameters.AddWithValue("userPassword", user.Password);
                    cmd.Parameters.AddWithValue("email", user.Email);
                    cmd.Parameters.AddWithValue("isActive", user.IsActive);
                    cmd.Parameters.AddWithValue("loggedIn", user.IsActive);
                    cmd.Parameters.AddWithValue("wallet", user.Wallet);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }

                result = "Success";
            }

            catch (Exception exception)
            {
                result = ExceptionMessage(exception).Email;
            }

            return result;
        }

        #endregion

        #region Delete functions

        /// <summary>
        /// Deletes an user from the database.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>string</returns>
        public string DeleteUser(string username)
        {
            string result;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "DELETE FROM Table_User WHERE username = @username";
                    cmd.Parameters.AddWithValue("username", username);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }

                result = "Success";
            }

            catch (Exception exception)
            {
                result = ExceptionMessage(exception).Email;
            }

            return result;
        }

        #endregion

        #region Exception functions

        /// <summary>
        /// Returns an user with an exception message in it.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns>CustomUser</returns>
        public CustomUser ExceptionMessage(Exception exception)
        {
            CustomUser user = new CustomUser
            {
                Username = "ERROR",
                Password = "ERROR",
                Email = exception.Message,
                IsActive = false,
                LoggedIn = false,
                Wallet = -50
                
            };

            return user;
        }

        #endregion

        #region Database functions

        /// <summary>
        /// Deletes all the users in the database.
        /// </summary>
        /// <returns>string</returns>
        public string TruncateTable()
        {
            string result;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "TRUNCATE TABLE Table_User;";
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

        #region Login functions

        /// <summary>
        /// Registers into a user in the database that the user has logged in.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>string</returns>
        public string LogIn(CustomUser user)
        {
            string result;

            try
            {
                if (pw.ValidatePassword(user))
                {
                    using (var connection = new SqlConnection(db.GetConnectionString()))
                    using (var cmd = connection.CreateCommand())
                    {
                        connection.Open();
                        cmd.CommandText = "UPDATE Table_user SET loggedIn = 1 WHERE username = @username";
                        cmd.Parameters.AddWithValue("username", user.Username);
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }
                    result = "Success";
                }

                else
                {
                    result = "Password not validated";
                }
            }

            catch (Exception exception)
            {
                result = exception.Message;
            }

            return result;
        }

        /// <summary>
        /// Registers into a user in the database that the user has logged out.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>string</returns>
        public string LogOut(CustomUser user)
        {
            string result;

            try
            {
                if (pw.ValidatePassword(user))
                {
                    using (var connection = new SqlConnection(db.GetConnectionString()))
                    using (var cmd = connection.CreateCommand())
                    {
                        connection.Open();
                        cmd.CommandText = "UPDATE Table_user SET loggedIn = 0 WHERE username = @username";
                        cmd.Parameters.AddWithValue("username", user.Username);
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }
                    result = "Success";
                }

                else
                {
                    result = "Password not validated";
                }
            }

            catch (Exception exception)
            {
                result = exception.Message;
            }

            return result;
        }

        #endregion

        #region Session Key functions

        /// <summary>
        /// Assigning a user a session key in the database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>string</returns>
        public string AssignSessionKey(CustomUser user)
        {
            string result;

            try
            {
                if (pw.ValidatePassword(user))
                {
                    using (var connection = new SqlConnection(db.GetConnectionString()))
                    using (var cmd = connection.CreateCommand())
                    {
                        connection.Open();
                        cmd.CommandText = "UPDATE Table_user SET sessionKey = @sessionKey WHERE username = @username";
                        cmd.Parameters.AddWithValue("sessionKey", RandomString(20));
                        cmd.Parameters.AddWithValue("username", user.Username);
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }
                    result = "Success";
                }

                else
                {
                    result = "Password not validated";
                }
            }

            catch (Exception exception)
            {
                result = exception.Message;
            }

            return result;
        }

        /// <summary>
        /// Gets the session key from a user in the database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>string</returns>
        public string GetSessionKey(CustomUser user)
        {
            CustomUser storedUser = new CustomUser();
            if (pw.ValidatePassword(user))
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_user WHERE username = @username";
                    cmd.Parameters.AddWithValue("username", user.Username);
                    cmd.ExecuteNonQuery();
                    var reader = cmd.ExecuteReader();
                    reader.Read();
                    storedUser.SessionKey = reader.GetString(reader.GetOrdinal("sessionKey"));
                    connection.Close();
                }
            }
            return storedUser.SessionKey;
        }

        #endregion

        #region Other functions

        /// <summary>
        /// returns a string with a random value.
        /// </summary>
        /// <param name="length"></param>
        /// <returns>string</returns>
        static string RandomString(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }

            return res.ToString();
        }

        #endregion
    }
}
