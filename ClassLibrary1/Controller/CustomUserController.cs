using ClassLibrary1.Database;
using ClassLibrary1.Model;
using System.Collections.Generic;

namespace ClassLibrary1.Controller
{
    /// <summary>
    /// This is the class CustomUserController.
    /// </summary>
    public class CustomUserController
    {
        #region Instances

        private DBCustomUser db_user;
        private PasswordHasher password_hasher;

        #endregion

        /// <summary>
        /// This is the constructor for the class CustomUserController.
        /// </summary>
        public CustomUserController()
        {
            db_user = DBCustomUser.GetInstance();
            password_hasher = new PasswordHasher();
        }

        #region Create functions

        /// <summary>
        /// Creates a user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>string</returns>
        public string Create(CustomUser user)
        {
            return db_user.CreateUser(user);
        }

        #endregion

        #region Search functions

        /// <summary>
        /// List all users.
        /// </summary>
        /// <returns>List<CustomUser></returns>
        public List<CustomUser> ListAll()
        {
            return db_user.ListAllUsers();
        }

        /// <summary>
        /// finds a user by its username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>CustomUser</returns>
        public CustomUser FindByUsername(string username)
        {
            return db_user.FindUserByUsername(username);
        }

        /// <summary>
        /// finds a list of users by their password.
        /// </summary>
        /// <param name="password"></param>
        /// <returns>List<CustomUser></returns>
        public List<CustomUser> FindByPassword(string password)
        {
            return db_user.FindUserByPassword(password);
        }

        /// <summary>
        /// finds a list of users by their email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>CustomUser</returns>
        public CustomUser FindByEmail(string email)
        {
            return db_user.FindUserByEmail(email);
        }

        /// <summary>
        /// finds a list of users by their is_active.
        /// </summary>
        /// <param name="is_active"></param>
        /// <returns>List<CustomUser></returns>
        public List<CustomUser> FindByIsActive(bool is_active)
        {
            return db_user.FindUserByIsactive(is_active);
        }

        /// <summary>
        /// finds a list of users by their logged_in.
        /// </summary>
        /// <param name="logged_in"></param>
        /// <returns>List<CustomUser></returns>
        public List<CustomUser> FindByLoggedIn(bool logged_in)
        {
            return db_user.FindUserByLoggedIn(logged_in);
        }

        /// <summary>
        /// finds a list of users by their wallet.
        /// </summary>
        /// <param name="wallet"></param>
        /// <param name="where"></param>
        /// <returns>List<CustomUser></returns>
        public List<CustomUser> FindByWallet(double wallet, string where)
        {
            List<CustomUser> result;

            switch (where)
            {
                case "Equal":
                    result = db_user.FindUserByWallet(wallet);
                    break;

                case "Higher":
                    result = db_user.FindUserByWalletHigher(wallet);
                    break;

                case "Lower":
                    result = db_user.FindUserByWalletLower(wallet);
                    break;

                default:
                    result = new List<CustomUser>();
                    break;
            }

            return result;
        }

        #endregion

        #region Update functions

        /// <summary>
        /// Updates a user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>string</returns>
        public string Update(CustomUser user)
        {
            CustomUser new_user = user;

            return db_user.UpdateUser(new_user);
        }

        #endregion

        #region Delete functions

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>string</returns>
        public string Delete(string username)
        {
            string result;

            result = db_user.DeleteUser(username);

            return result;
        }

        #endregion

        #region Login functions

        /// <summary>
        /// Log a user in
        /// </summary>
        /// <param name="user"></param>
        public void LogIn(CustomUser user)
        {
            db_user.LogIn(user);
        }

        /// <summary>
        /// Log a user out
        /// </summary>
        /// <param name="user"></param>
        public void LogOut(CustomUser user)
        {
            db_user.LogOut(user);
        }

        #endregion

        #region Session Key functions

        /// <summary>
        /// assign a session key to a user.
        /// </summary>
        /// <param name="user"></param>
        public void AssignSessionKey(CustomUser user)
        {
            db_user.AssignSessionKey(user);
        }

        /// <summary>
        /// gets a session key from a user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>string</returns>
        public string GetSessionKey(CustomUser user)
        {
            return db_user.GetSessionKey(user);
        }

        #endregion
    }
}