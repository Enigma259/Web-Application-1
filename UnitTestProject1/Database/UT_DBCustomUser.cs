using ClassLibrary1.Database;
using ClassLibrary1.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace UnitTestProject1.Database
{
    /// <summary>
    /// Summary description for UT_DBCustomUser
    /// </summary>
    [TestClass]
    public class UT_DBCustomUser
    {
        DBCustomUser user_db;
        List<string> usernames;

        /// <summary>
        /// Initiates the necessary things to the tests can work.
        /// </summary>
        [TestInitialize]
        public void ProductInitialize()
        {
            user_db = DBCustomUser.GetInstance();
            usernames = new List<string>();
        }

        /// <summary>
        /// Tests if you can create a user.
        /// </summary>
        [TestMethod]
        public void Success_CreateUser()
        {
            //Arrange
            CustomUser custom_user;
            PasswordHasher hasher;
            string result;

            //Act
            hasher = new PasswordHasher();
            custom_user = new CustomUser();
            custom_user.Username = "Test Create User";
            custom_user.Password = hasher.HashPassword("Test Create User");
            custom_user.Email = "Test Create User";
            custom_user.IsActive = true;
            custom_user.Wallet = 10.0;
            result = user_db.CreateUser(custom_user);
            usernames.Add(custom_user.Username);

            //Assert
            Assert.AreEqual("Success", result);
        }

        /// <summary>
        /// Tests if you can find a user.
        /// </summary>
        [TestMethod]
        public void Success_FindUserByUsername()
        {
            //Arrange
            CustomUser custom_user;
            CustomUser result;
            PasswordHasher hasher;

            //Act
            hasher = new PasswordHasher();
            custom_user = new CustomUser();
            custom_user.Username = "Test Find User By Username";
            custom_user.Password = hasher.HashPassword("Test Find User By Username");
            custom_user.Email = "Test Find User By Username";
            custom_user.IsActive = true;
            custom_user.Wallet = 10.0;
            user_db.CreateUser(custom_user);
            result = user_db.FindUserByUsername(custom_user.Username);
            usernames.Add(custom_user.Username);

            //Assert
            Assert.AreEqual(custom_user.Username, result.Username);
        }

        /// <summary>
        /// Tests if you can find a user.
        /// </summary>
        [TestMethod]
        public void Success_FindUserByEmail()
        {
            //Arrange
            CustomUser custom_user;
            CustomUser result;
            PasswordHasher hasher;

            //Act
            hasher = new PasswordHasher();
            custom_user = new CustomUser();
            custom_user.Username = "Test Find User By Email";
            custom_user.Password = hasher.HashPassword("Test Find User By Email");
            custom_user.Email = "Test Find User By Email";
            custom_user.IsActive = true;
            custom_user.Wallet = 10.0;
            user_db.CreateUser(custom_user);
            result = user_db.FindUserByEmail(custom_user.Email);
            usernames.Add(custom_user.Username);

            //Assert
            Assert.AreEqual(custom_user.Email, result.Email);
        }

        [TestMethod]
        public void Success_UpdateUser()
        {
            //Arrange
            CustomUser custom_user;
            PasswordHasher hasher;
            string result;

            //Act
            hasher = new PasswordHasher();
            custom_user = new CustomUser();
            custom_user.Username = "Test Update User - Username";
            custom_user.Password = hasher.HashPassword("Test Update User - Password");
            custom_user.Email = "Test Update User - Email";
            custom_user.IsActive = true;
            custom_user.Wallet = 10.0;
            result = user_db.CreateUser(custom_user);

            if(result.Equals("Success"))
            {
                custom_user.Password = hasher.HashPassword("Test Update User - Updated");
                custom_user.Email = "Test Update User - Updated";
                custom_user.IsActive = false;
                custom_user.Wallet = 5.0;

                user_db.UpdateUser(custom_user, "Increase");
            }

            usernames.Add(custom_user.Username);

            //Assert
            Assert.AreEqual("Success", result);
            Assert.AreEqual(15.0, user_db.FindUserByUsername(custom_user.Username).Wallet);
        }

        /// <summary>
        /// tests if you can create a user when there already is a user with the same username.
        /// </summary>
        [TestMethod]
        public void Fail_CreateUser()
        {
            //Arrange
            CustomUser custom_user;
            PasswordHasher hasher;
            string result;
            int index;
            int max_index;

            //Act
            hasher = new PasswordHasher();
            index = 0;
            max_index = 50;

            while (index < max_index)
            {
                custom_user = new CustomUser();
                custom_user.Username = "Test Create User - Fail " + index;
                custom_user.Password = hasher.HashPassword("Test Create User - Fail");
                custom_user.Email = "Test Create User - Fail";
                custom_user.IsActive = true;
                custom_user.Wallet = 10.0;
                user_db.CreateUser(custom_user);
                usernames.Add(custom_user.Username);
                index++;
            }

            index -= 5;

            custom_user = new CustomUser();
            custom_user.Username = "Test Create User - Fail " + index;
            custom_user.Password = hasher.HashPassword("Test Create User - Fail");
            custom_user.Email = "Test Create User - Fail";
            custom_user.IsActive = true;
            custom_user.Wallet = 10.0;
            result = user_db.CreateUser(custom_user);

            //Assert
            Assert.AreNotEqual("Success", result);
        }

        /// <summary>
        /// Cleans up after the custom user tests.
        /// </summary>
        [TestCleanup]
        public void CustomUserCleanup()
        {
            DatabaseString object_value = DatabaseString.GetInstance();
            
            string connectionString = object_value.GetConnectionString();

            foreach (string current_username in usernames)
            {
                using (var connection = new SqlConnection(connectionString))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "DELETE FROM Table_User WHERE username = @username";
                    cmd.Parameters.AddWithValue("username", current_username);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}