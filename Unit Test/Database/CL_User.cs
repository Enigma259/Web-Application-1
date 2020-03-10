using ClassLibrary1.Database;
using ClassLibrary1.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Unit_Test.Database
{
    [TestClass]
    public class CL_User
    {
        #region Instances

        private DBCustomUser db_user;
        private List<string> usernames;

        #endregion

        #region Unit Test functions

        [TestInitialize]
        public void Initialize()
        {
            db_user = DBCustomUser.GetInstance();
            usernames = new List<string>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            foreach (string username in usernames)
            {
                db_user.DeleteUser(username);
            }
        }

        #endregion

        #region Success functions

        [TestMethod]
        public void Success_CreateUser()
        {
            //Arrange
            string result;
            CustomUser user;

            //Act
            user = new CustomUser
            {
                Username = "ClassLibrary1 - Test user database - CreateUser",
                Password = "ClassLibrary1 - Test user database - CreateUser",
                Email = "ClassLibrary1 - Test user database - CreateUser",
                IsActive = true,
                Wallet = 500,
                LoggedIn = true
            };

            result = db_user.CreateUser(user);
            usernames.Add(user.Username);

            //Assert
            Assert.AreEqual("Success", result);
        }

        [TestMethod]
        public void Success_FindUserByUsername()
        {
            //Arrange
            CustomUser user;
            CustomUser result;

            //Act
            user = new CustomUser
            {
                Username = "ClassLibrary1 - Test user database - FindUserByUsername",
                Password = "ClassLibrary1 - Test user database - FindUserByUsername",
                Email = "ClassLibrary1 - Test user database - FindUserByUsername",
                IsActive = true,
                Wallet = 500,
                LoggedIn = true
            };

            db_user.CreateUser(user);
            usernames.Add(user.Username);

            result = db_user.FindUserByUsername(user.Username);

            //Assert
            Assert.AreEqual(user.Username, result.Username);
        }

        [TestMethod]
        public void Success_FindUserByPassword()
        {
            //Arrange
            CustomUser user;
            List<CustomUser> result;

            //Act
            user = new CustomUser
            {
                Username = "ClassLibrary1 - Test user database - FindUserByPassword",
                Password = "ClassLibrary1 - Test user database - FindUserByPassword",
                Email = "ClassLibrary1 - Test user database - FindUserByPassword",
                IsActive = true,
                Wallet = 500,
                LoggedIn = true
            };

            db_user.CreateUser(user);
            usernames.Add(user.Username);

            result = db_user.FindUserByPassword(user.Password);

            //Assert
            Assert.AreEqual(user.Password, result[0].Password);
        }

        [TestMethod]
        public void Success_FindUserByEmail()
        {
            //Arrange
            CustomUser user;
            CustomUser result;

            //Act
            user = new CustomUser
            {
                Username = "ClassLibrary1 - Test user database - FindUserByEmail",
                Password = "ClassLibrary1 - Test user database - FindUserByEmail",
                Email = "ClassLibrary1 - Test user database - FindUserByEmail",
                IsActive = true,
                Wallet = 500,
                LoggedIn = true
            };

            db_user.CreateUser(user);
            usernames.Add(user.Username);

            result = db_user.FindUserByEmail(user.Email);

            //Assert
            Assert.AreEqual(user.Email, result.Email);
        }

        [TestMethod]
        public void Success_UpdateUser()
        {
            //Arrange
            string result;
            CustomUser user;

            //Act
            user = new CustomUser
            {
                Username = "ClassLibrary1 - Test user database - UpdateUser",
                Password = "ClassLibrary1 - Test user database - UpdateUser",
                Email = "ClassLibrary1 - Test user database - UpdateUser",
                IsActive = true,
                Wallet = 500,
                LoggedIn = true
            };

            result = db_user.CreateUser(user);
            usernames.Add(user.Username);

            if (result.Equals("Success"))
            {
                user.IsActive = false;

                result = db_user.UpdateUser(user);
            }

            //Assert
            Assert.AreEqual("Success", result);
        }

        [TestMethod]
        public void Success_LogIn()
        {
            //Arrange
            string result;
            CustomUser user;

            //Act
            user = new CustomUser
            {
                Username = "ClassLibrary1 - Test user database - LogIn",
                Password = "ClassLibrary1 - Test user database - LogIn",
                Email = "ClassLibrary1 - Test user database - LogIn",
                IsActive = true,
                Wallet = 500,
                LoggedIn = false
            };

            result = db_user.CreateUser(user);
            usernames.Add(user.Username);

            if (result.Equals("Success"))
            {
                result = db_user.LogIn(user);
            }

            //Assert
            Assert.AreEqual("Success", result);
        }
        
        [TestMethod]
        public void Success_LogOut()
        {
            //Arrange
            string result;
            CustomUser user;

            //Act
            user = new CustomUser
            {
                Username = "ClassLibrary1 - Test user database - LogOut",
                Password = "ClassLibrary1 - Test user database - LogOut",
                Email = "ClassLibrary1 - Test user database - LogOut",
                IsActive = true,
                Wallet = 500,
                LoggedIn = true
            };

            result = db_user.CreateUser(user);
            usernames.Add(user.Username);

            if (result.Equals("Success"))
            {
                result = db_user.LogOut(user);
            }

            //Assert
            Assert.AreEqual("Success", result);
        }

        [TestMethod]
        public void Success_AssignSessionKey()
        {
            //Arrange
            string result;
            CustomUser user;

            //Act
            user = new CustomUser
            {
                Username = "ClassLibrary1 - Test user database - AssignSessionKey",
                Password = "ClassLibrary1 - Test user database - AssignSessionKey",
                Email = "ClassLibrary1 - Test user database - AssignSessionKey",
                IsActive = true,
                Wallet = 500,
                LoggedIn = true
            };

            result = db_user.CreateUser(user);
            usernames.Add(user.Username);

            if (result.Equals("Success"))
            {
                result = db_user.AssignSessionKey(user);
            }

            //Assert
            Assert.AreEqual("Success", result);
        }

        [TestMethod]
        public void Success_GetSessionKey()
        {
            //Arrange
            string result;
            CustomUser user;

            //Act
            user = new CustomUser
            {
                Username = "ClassLibrary1 - Test user database - AssignSessionKey",
                Password = "ClassLibrary1 - Test user database - AssignSessionKey",
                Email = "ClassLibrary1 - Test user database - AssignSessionKey",
                IsActive = true,
                Wallet = 500,
                LoggedIn = true
            };

            result = db_user.CreateUser(user);
            usernames.Add(user.Username);

            if (result.Equals("Success"))
            {
                result = db_user.AssignSessionKey(user);

                if (result.Equals("Success"))
                {
                    result = db_user.GetSessionKey(user);
                }
            }

            //Assert
            Assert.IsTrue(!result.Equals(null) && !result.Equals("") && !result.Equals("Success"));
        }

        #endregion
    }
}