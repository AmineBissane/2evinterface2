using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

public class DatabaseHelper
{
    private readonly string connectionString = "datasource=localhost;port=3306;username=root;password=root;database=northwind;";

    public MySqlConnection GetConnection()
    {
        try
        {
            Console.WriteLine("Creating database connection...");
            return new MySqlConnection(connectionString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating database connection: {ex.Message}");
            throw; 
        }
    }

    public string GetUserName(string username, string password)
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                Console.WriteLine("Database connection opened successfully.");

                string query = "SELECT name FROM users WHERE username = @username AND password = @password";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    Console.WriteLine("Executing GetUserName query...");

                    var result = command.ExecuteScalar();
                    Console.WriteLine(result != null
                        ? $"Query result: {result}"
                        : "No user found with the provided credentials.");

                    return result?.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetUserName: {ex.Message}");
            return null;
        }
    }

    public List<Tuple<string, string>> GetProductAndCategory()
    {
        var productsWithCategories = new List<Tuple<string, string>>();

        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "SELECT p.ProductName, c.CategoryName " +
                               "FROM products p " +
                               "JOIN categories c ON p.CategoryID = c.CategoryID"; 

                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var productName = reader.GetString(0);
                        var categoryName = reader.GetString(1);
                        productsWithCategories.Add(Tuple.Create(productName, categoryName));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetProductAndCategory: {ex.Message}");
        }

        return productsWithCategories;
    }
    public List<string> GetDistinctCategories()
    {
        var distinctCategories = new List<string>();

        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "SELECT DISTINCT CategoryName FROM categories"; // Query to get distinct categories

                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var categoryName = reader.GetString(0);
                        distinctCategories.Add(categoryName);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetDistinctCategories: {ex.Message}");
        }

        return distinctCategories;
    }
    public bool AddProduct(string productName, string categoryName)
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                string getCategoryIdQuery = "SELECT CategoryID FROM categories WHERE CategoryName = @categoryName";
                using (var command = new MySqlCommand(getCategoryIdQuery, connection))
                {
                    command.Parameters.AddWithValue("@categoryName", categoryName);
                    var categoryId = command.ExecuteScalar();

                    if (categoryId != null)
                    {
                        string insertQuery = "INSERT INTO products (ProductName, CategoryID) VALUES (@productName, @categoryId)";
                        using (var insertCommand = new MySqlCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@productName", productName);
                            insertCommand.Parameters.AddWithValue("@categoryId", categoryId);
                            int rowsAffected = insertCommand.ExecuteNonQuery();

                            return rowsAffected > 0; 
                        }
                    }
                    else
                    {
                        Console.WriteLine("Category not found.");
                        return false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in AddProduct: {ex.Message}");
            return false;
        }

    }
    public bool UpdateProduct(string currentName, string newName, string newCategory)
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "UPDATE products p " +
                               "JOIN categories c ON p.CategoryID = c.CategoryID " +
                               "SET p.ProductName = @newName, p.CategoryID = c.CategoryID " +
                               "WHERE p.ProductName = @currentName AND c.CategoryName = @newCategory";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@newName", newName);
                    command.Parameters.AddWithValue("@currentName", currentName);
                    command.Parameters.AddWithValue("@newCategory", newCategory);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating product: {ex.Message}");
            return false;
        }
    }

    public bool DeleteProduct(string productName)
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "DELETE FROM products WHERE ProductName = @productName";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@productName", productName);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting product: {ex.Message}");
            return false;
        }
    }

    public bool VerifyUser(string username, string password)
    {
        try
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM users WHERE username = @username AND password = @password";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    int userCount = Convert.ToInt32(command.ExecuteScalar());
                    return userCount > 0;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error verifying user: {ex.Message}");
            return false;
        }
    }




}
