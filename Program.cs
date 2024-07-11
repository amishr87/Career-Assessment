using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        // Connection string to your Azure SQL Database
        string connectionString = "Server=tcp:test-form.database.windows.net,1433;Initial Catalog=TestForm;Persist Security Info=False;User ID=testformadmin;Password=admin@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        // Query to select data from your table
        string query = "SELECT TOP 100 * FROM dbo.UserInfo";

        try
        {
            // Establishing connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Creating SQL command
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Executing the command and reading the data
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Checking if there is data available
                        if (reader.HasRows)
                        {
                            // Reading the first row
                            reader.Read();

                            // Loop through each column in the row
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                // Retrieve column name and value
                                string columnName = reader.GetName(i);
                                object columnValue = reader.GetValue(i);

                                // Print column name and value
                                Console.WriteLine($"{columnName}: {columnValue}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No data found.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Handling any exceptions that occur
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
