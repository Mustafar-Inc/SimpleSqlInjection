using System.Data.SqlClient;

namespace VulnerableApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"data source=YOUR_SERVER_NAME; database=ProductDB; integrated security=SSPI";

            Console.WriteLine("Enter the product name to search:");
            string productName = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // This is a vulnerable way to construct SQL queries
                var sqlQuery = "SELECT * FROM Products WHERE Name LIKE '" + productName + "%'";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["ID"]}, Name: {reader["Name"]}, Price: {reader["Price"]}, Quantity: {reader["QuantityAvailable"]}");
                }
            }
        }
    }
}
