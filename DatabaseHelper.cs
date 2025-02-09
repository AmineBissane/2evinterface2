using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace 2evinterfaces
{
    public class DatabaseHelper
    {
        private readonly string connectionString = "datasource=localhost;port=3306;username=root;password=root;database=northwind;";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public string GetUserName(string username, string password)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    string query = "SELECT name FROM users WHERE username = @username AND password = @password";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);

                        var result = command.ExecuteScalar();
                        return result?.ToString();
                    }
                }
            }
            catch
            {
                return null;
            }
        public List<string> GetProductNames()
        {
            var productNames = new List<string>();

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        Console.WriteLine("Database connection opened successfully for GetProductNames.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to open database connection.");
                        return productNames;
                    }

                    string query = "SELECT ProductName FROM products";
                    using (var command = new MySqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        Console.WriteLine("Executing GetProductNames query...");

                        if (!reader.HasRows)
                        {
                            Console.WriteLine("No products found in the database.");
                        }

                        while (reader.Read())
                        {
                            var productName = reader.GetString(0);
                            Console.WriteLine($"Fetched product: {productName}");
                            productNames.Add(productName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetProductNames: {ex.Message}");
            }

            return productNames;
        }

    }
    
}
}
