namespace ClassLibrary1.Database
{
    /// <summary>
    /// This is ths class DatabaseString.
    /// </summary>
    public class DatabaseString
    {
        #region Instances

        private static volatile DatabaseString _instance;
        private static object syncRoot = new object();
        private string data_source;
        private string initial_catalog;
        private string persist_security_info;
        private string user_id;
        private string user_password;

        #endregion

        /// <summary>
        /// This is the constructor for the class DatabaseString.
        /// </summary>
        private DatabaseString()
        {
            this.data_source = "kraka.ucn.dk";
            this.initial_catalog = "psua0218_1026970";
            this.persist_security_info = "True";
            this.user_id = "psua0218_1026970";
            this.user_password = "Password1!";
        }

        #region Singleton functions

        /// <summary>
        /// This is a multi threaded singleton for the class DatabaseString.
        /// </summary>
        /// <returns>_instance</returns>
        public static DatabaseString GetInstance()
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new DatabaseString();
                    }
                }
            }

            return _instance;
        }

        #endregion

        #region Get Functions

        /// <summary>
        /// Returns the value of the instance data_source.
        /// </summary>
        /// <returns>string</returns>
        public string GetDataSource()
        {
            return data_source;
        }

        /// <summary>
        /// Returns the value of the instance initial_catalog.
        /// </summary>
        /// <returns>string</returns>
        public string GetInitialCatalog()
        {
            return initial_catalog;
        }

        /// <summary>
        /// Returns the value of the instance persist_security_info.
        /// </summary>
        /// <returns>string</returns>
        public string GetPersistSecurityInfo()
        {
            return persist_security_info;
        }

        /// <summary>
        /// Returns the value of the instance user_id.
        /// </summary>
        /// <returns>string</returns>
        public string GetUserId()
        {
            return user_id;
        }

        /// <summary>
        /// Returns the value of the instance user_password.
        /// </summary>
        /// <returns>string</returns>
        public string GetUserPassword()
        {
            return user_password;
        }

        /// <summary>
        /// Returns the coneection string to the database.
        /// </summary>
        /// <returns>string</returns>fcfc
        public string GetConnectionString()
        {
            string result = "";
            string seperator = ";";

            result += "Data Source=" + GetDataSource() + seperator;
            result += "Initial Catalog=" + GetInitialCatalog() + seperator;
            result += "Persist Security Info=" + GetPersistSecurityInfo() + seperator;
            result += "User ID=" + GetUserId() + seperator;
            result += "Password=" + GetUserPassword() + seperator;

            return result;
        }

        #endregion

        #region Set Functions

        /// <summary>
        /// Changes the value of the instance data_source.
        /// </summary>
        /// <param name="data_source"></param>
        public void SetDataSource(string data_source)
        {
            this.data_source = data_source;
        }

        /// <summary>
        /// Changes the value of the instance initial_catalog.
        /// </summary>
        /// <param name="initial_catalog"></param>
        public void SetInitialCatalog(string initial_catalog)
        {
            this.initial_catalog = initial_catalog;
        }

        /// <summary>
        /// Changes the value of the instance persist_security_info.
        /// </summary>
        /// <param name="persist_security_info"></param>
        public void SetPersistSecurityInfo(bool persist_security_info)
        {
            if (persist_security_info)
            {
                this.persist_security_info = "True";
            }

            else
            {
                this.persist_security_info = "False";
            }
        }

        /// <summary>
        /// Changes the value of the instance user_id.
        /// </summary>
        /// <param name="user_id"></param>
        public void SetUserId(string user_id)
        {
            this.user_id = user_id;
        }

        /// <summary>
        /// Changes the value of the instance user_password.
        /// </summary>
        /// <param name="user_password"></param>
        public void SetUserPassword(string user_password)
        {
            this.user_password = user_password;
        }

        #endregion
    }
}