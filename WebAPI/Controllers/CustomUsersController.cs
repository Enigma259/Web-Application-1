using ClassLibrary1.Controller;
using ClassLibrary1.Model;
using System.Collections.Generic;
using System.Web.Http;

namespace WebAPI.Controllers
{
    /// <summary>
    /// This is the class CustomUsersController.
    /// </summary>
    public class CustomUsersController : ApiController
    {
        #region Instances

        CustomUserController ctrl = new CustomUserController();
        PasswordHasher pw = new PasswordHasher();

        #endregion

        #region IEnumerable functions

        // GET: api/CustomUser
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        #endregion

        #region Create functions

        /// <summary>
        /// Creates a user.
        /// </summary>
        /// <param name="val"></param>
        /// <returns>string</returns>
        [Route("api/CustomUsers/CreateCustomUser")]
        [HttpPost]
        public string AddCustomUser(CustomUser val)
        {
            return ctrl.Create(val);
        }

        #endregion

        #region Search functions

        /// <summary>
        /// Finds a user by its username.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>CustomUser</returns>
        [Route("api/CustomUsers/GetCustomUser/{name}")]
        [HttpGet]
        public CustomUser GetCustomUser(string name)
        {
            return ctrl.FindByUsername(name);
        }

        #endregion

        #region Session Key functions

        /// <summary>
        /// Assign a session key to a user.
        /// </summary>
        /// <param name="user"></param>
        [Route("api/CustomUsers/CreateSession")]
        [HttpPost]
        public void CreateSession(CustomUser user)
        {
            ctrl.AssignSessionKey(user);
        }

        /// <summary>
        /// Gets a session key from a user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("api/CustomUsers/GetSessionKey")]
        [HttpPost]
        public string GetSession(CustomUser user)
        {
            return ctrl.GetSessionKey(user);
        }

        #endregion

        #region Login functions

        /// <summary>
        /// Validates a password from a user.
        /// </summary>
        /// <param name="val"></param>
        /// <returns>bool</returns>
        [Route("api/CustomUsers/ValidatePassword")]
        [HttpPost]
        public bool ValidatePassword(CustomUser val)
        {
            return pw.ValidatePassword(val);
        }

        /// <summary>
        /// Logs a user in.
        /// </summary>
        /// <param name="user"></param>
        [Route("api/CustomUsers/LogIn")]
        [HttpPost]
        public void LogIn(CustomUser user)
        {
            ctrl.LogIn(user);
        }

        /// <summary>
        /// Logs a user out.
        /// </summary>
        /// <param name="user"></param>
        [Route("api/CustomUsers/LogOut")]
        [HttpPost]
        public void LogOut(CustomUser user)
        {
            ctrl.LogOut(user);
        }

        /// <summary>
        /// Checks if a user is logged in.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Route("api/CustomUsers/CheckLoginState")]
        [HttpGet]
        public bool CheckLoginState(string name)
        {
            return ctrl.FindByUsername(name).LoggedIn;
        }

        #endregion
    }
}