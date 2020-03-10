namespace ClassLibrary1.Model
{
    /// <summary>
    /// This is the class CustomUser.
    /// </summary>
    public class CustomUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public double Wallet { get; set; }
        public bool IsActive = true;
        public bool LoggedIn = false;
        public string SessionKey;
    }
}