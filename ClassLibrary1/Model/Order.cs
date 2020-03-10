namespace ClassLibrary1.Model
{
    /// <summary>
    /// This is the class Order.
    /// </summary>
    public class Order
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public bool IsShipped { get; set; }
        public string Username { get; set; }
    }
}