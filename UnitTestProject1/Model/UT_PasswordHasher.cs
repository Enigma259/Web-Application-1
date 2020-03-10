using ClassLibrary1.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.Model
{
    [TestClass]
    public class UT_PasswordHasher
    {
        /// <summary>
        /// Checks if you hash the same password twice you will get the same hashed password back.
        /// </summary>
        [TestMethod]
        public void Success_HashPassword()
        {
            //Arrange
            PasswordHasher password_hasher;
            string password;
            string hash_passowrd_1;
            string hash_passowrd_2;

            //Act
            password_hasher = new PasswordHasher();
            password = "123456789";
            hash_passowrd_1 = password_hasher.HashPassword(password);
            hash_passowrd_2 = password_hasher.HashPassword(password);

            //Assert
            Assert.AreEqual(hash_passowrd_1, hash_passowrd_2);
        }
    }
}
