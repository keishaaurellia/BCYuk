using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project___BcYuk
{
    internal class Support
    {
        public static List<Product> Products = new List<Product>();
        public static string userFullName;
        public static string userPassword;
        public static string userPhoneNumber;
        public static string ClassId;
        public static int userID = -1;

        //public static DataClasses1DataContext dbContext = new DataClasses1DataContext();
    }

    public partial class Product
    {
        public int productID { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string category { get; set; }
        public string image { get; set; }
        public int stock { get; set; }
        public int quantity { get; set; } = 1;
        public string note { get; set; } = "-";

        public string NormalizedNote => string.IsNullOrWhiteSpace(note) ? "-" : note.Trim();

    }

    public class ProductDTO
    {
        public int productID { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public decimal? quantity { get; set; }
        public string image { get; set; }
        public string note { get; set; }
    }


}
