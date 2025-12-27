using HandlerErrors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSDataAccessLayer
{
    public class clsProductData
    {
        public static int AddNewProduct(string ProductName, string Descriptions, decimal Price,
        int QuantityInStock,decimal TaxPercent, string ImageProduct, int? CreatedByUserID)
        {
            int ProductID = -1;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                //string Query = @"INSERT INTO Products (ProductName, Descriptions, Price, QuantityInStock, TaxPercent, ImageProduct, CreatedByUserID)
                //         VALUES (@ProductName, @Descriptions, @Price, @QuantityInStock, @TaxPercent, @ImageProduct, @CreatedByUserID)
                //         SELECT SCOPE_IDENTITY()";

                using (SqlCommand command = new SqlCommand("Products.Sp_AddNewProduct_POS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ProductName", ProductName);

                    if (!string.IsNullOrWhiteSpace(Descriptions))
                        command.Parameters.AddWithValue("@Descriptions", Descriptions);
                    else
                        command.Parameters.AddWithValue("@Descriptions", DBNull.Value);

                    command.Parameters.AddWithValue("@Price", Price);
                    command.Parameters.AddWithValue("@QuantityInStock", QuantityInStock);
                    command.Parameters.AddWithValue("@TaxPercent", TaxPercent);

                    if (!string.IsNullOrWhiteSpace(ImageProduct))
                        command.Parameters.AddWithValue("@ImageProduct", ImageProduct);
                    else
                        command.Parameters.AddWithValue("@ImageProduct", DBNull.Value);

                    if (CreatedByUserID != null)
                        command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                    else
                        command.Parameters.AddWithValue("@CreatedByUserID", DBNull.Value);

                    SqlParameter outputIdParam = new SqlParameter("@NewProductID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);


                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();

                        if (command.Parameters["@NewProductID"].Value != DBNull.Value)
                            ProductID = Convert.ToInt32(command.Parameters["@NewProductID"].Value);

                    }
                    catch (SqlException se)
                    {
                        // Handle Error Logging
                        //Console.WriteLine($"Error adding product: {ex.Message}");
                        LogException.logException(se.Message, EventLogEntryType.Error);
                    }
                    catch (Exception ex)
                    {
                        // Handle Error Logging
                        //Console.WriteLine($"Error adding product: {ex.Message}");
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                    }
                }
                return ProductID;
            }
        }

        public static bool UpdateProduct(int ProductID, string ProductName, string Descriptions,
            decimal Price, int QuantityInStock, decimal TaxPercent, string ImageProduct, int? CreatedByUserID)
        {
            int rowAffected = 0;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("Products.SP_UpdateProduct_POS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ProductID", ProductID);
                    command.Parameters.AddWithValue("@ProductName", ProductName);

                    if (!string.IsNullOrWhiteSpace(Descriptions))
                        command.Parameters.AddWithValue("@Descriptions", Descriptions);
                    else
                        command.Parameters.AddWithValue("@Descriptions", DBNull.Value);

                    command.Parameters.AddWithValue("@Price", Price);
                    command.Parameters.AddWithValue("@QuantityInStock", QuantityInStock);
                    command.Parameters.AddWithValue("@QuantityInStock", TaxPercent);

                    if (!string.IsNullOrWhiteSpace(ImageProduct))
                        command.Parameters.AddWithValue("@ImageProduct", ImageProduct);
                    else
                        command.Parameters.AddWithValue("@ImageProduct", DBNull.Value);

                    if( CreatedByUserID == null)
                        command.Parameters.AddWithValue("@CreatedByUserID", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                    command.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;


                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        rowAffected = (int)command.Parameters["@ReturnValue"].Value;
                    }
                    catch (SqlException se)
                    {
                        // Handle Error Logging
                        //Console.WriteLine($"Error updating product: {ex.Message}");
                        LogException.logException(se.Message, EventLogEntryType.Error);
                    }
                    catch (Exception ex)
                    {
                        // Handle Error Logging
                        //Console.WriteLine($"Error updating product: {ex.Message}");
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                    }
                }
            }
            return (rowAffected > 0);
        }

        public static bool DeleteProduct(int ProductID)
        {
            int rowAffected = 0;
            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                //string Query = @"DELETE FROM Products WHERE ProductID = @ProductID";
                using (SqlCommand command = new SqlCommand("Products.SP_DeleteProduct_POS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ProductID", ProductID);
                    command.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        rowAffected = (int)command.Parameters["@ReturnValue"].Value;
                    }
                    catch (SqlException se)
                    {
                        // Handle Error Logging
                        // Console.WriteLine($"Error deleting product: {ex.Message}");
                        LogException.logException(se.Message, EventLogEntryType.Error);
                    }
                    catch (Exception ex)
                    {
                        // Handle Error Logging
                        // Console.WriteLine($"Error deleting product: {ex.Message}");
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                    }
                }
            }
            return (rowAffected > 0);
        }

        public static DataTable GetAllProducts()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string Query = @"SELECT ProductID,ProductName,Descriptions,Price,QuantityInStock,
                                       CASE 
                                            WHEN QuantityInStock = 0 THEN 'Out of Stock'
                                            WHEN QuantityInStock <= 10 THEN 'Low Stock'
                                            ELSE 'In Stock'
                                       END AS StockStatus,
                                       TaxPercent,
                                       (Price *0.2) as PriceWithTax
                                 FROM Products 
                                 ORDER BY ProductName";
                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                            }
                        }
                    }
                    catch (SqlException se)
                    {
                        // Handle Error Logging
                        //Console.WriteLine($"Error getting products: {ex.Message}");
                        LogException.logException(se.Message, EventLogEntryType.Error);
                    }
                    catch (Exception ex)
                    {
                        // Handle Error Logging
                        //Console.WriteLine($"Error getting products: {ex.Message}");
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                    }
                }
            }
            return dt;
        }

        public static DataTable GetAllProducts2(int currentPage, int PAGE_SIZE)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
              
                using (SqlCommand command = new SqlCommand("Products.SP_GetAllProduct_POS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@PageNumber", SqlDbType.Int).Value = currentPage;
                    command.Parameters.Add("@RowsPerPage", SqlDbType.Int).Value = PAGE_SIZE;

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                            }
                        }
                    }
                    catch (SqlException se)
                    {
                        // Handle Error Logging
                        //Console.WriteLine($"Error getting products: {ex.Message}");
                        LogException.logException(se.Message, EventLogEntryType.Error);
                    }
                    catch (Exception ex)
                    {
                        // Handle Error Logging
                        //Console.WriteLine($"Error getting products: {ex.Message}");
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                    }
                }
            }
            return dt;
        }

        public static bool GetProductByProductID(int ProductID, ref string ProductName,
            ref string Descriptions, ref decimal Price, ref int QuantityInStock,ref decimal TaxPercent,
            ref string ImageProduct, ref int CreatedByUserID)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string Query = @"SELECT * FROM Products WHERE ProductID = @ProductID";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@ProductID", ProductID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;

                                ProductName = (string)reader["ProductName"];
                                Price = (decimal)reader["Price"];
                                QuantityInStock = (int)reader["QuantityInStock"];
                                TaxPercent = (decimal)reader["TaxPercent"];
                                CreatedByUserID = (reader["CreatedByUserID"] == DBNull.Value) ? -1 : (int)reader["CreatedByUserID"];

                                // Handle nullable fields
                                Descriptions = (reader["Descriptions"] == DBNull.Value) ? "" : (string)reader["Descriptions"];
                                ImageProduct = (reader["ImageProduct"] == DBNull.Value) ? "" : (string)reader["ImageProduct"];
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        IsFound = false;
                        // Handle Error Logging
                        //Console.WriteLine($"Error getting product by ID: {ex.Message}");
                    }
                }
            }
            return IsFound;
        }

        public static bool GetProductByProductName(string ProductName, ref int ProductID,
            ref string Descriptions, ref decimal Price, ref int QuantityInStock,ref decimal TaxPercent,
            ref string ImageProduct, ref int CreatedByUserID)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string Query = @"SELECT * FROM Products WHERE ProductName = @ProductName";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@ProductName", ProductName);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;

                                ProductID = (int)reader["ProductID"];
                                Price = (decimal)reader["Price"];
                                QuantityInStock = (int)reader["QuantityInStock"];
                                TaxPercent = (decimal)reader["TaxPercent"];
                                CreatedByUserID = (reader["CreatedByUserID"] == DBNull.Value) ? -1 :(int)reader["CreatedByUserID"];

                                // Handle nullable fields
                                Descriptions = (reader["Descriptions"] == DBNull.Value) ? "" : (string)reader["Descriptions"];
                                ImageProduct = (reader["ImageProduct"] == DBNull.Value) ? "" : (string)reader["ImageProduct"];
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        IsFound = false;
                        // Handle Error Logging
                        //Console.WriteLine($"Error getting product by name: {ex.Message}");
                    }
                }
            }
            return IsFound;
        }

        public static bool IsProductExists(string ProductName)
        {
            bool exists = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string Query = @"SELECT Found = 1 FROM Products WHERE ProductName = @ProductName";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@ProductName", ProductName);

                    try
                    {
                        connection.Open();
                        int count = (int)command.ExecuteScalar();
                        exists = (count > 0);
                    }
                    catch (Exception ex)
                    {
                        // Handle Error Logging
                        //Console.WriteLine($"Error checking product existence: {ex.Message}");
                    }
                }
            }
            return exists;
        }

        public static bool UpdateProductQuantity(int? ProductID, int NewQuantity)
        {
            int rowAffected = 0;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string Query = @"UPDATE Products SET QuantityInStock -= @QuantityInStock 
                                 WHERE ProductID = @ProductID";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@ProductID", ProductID);
                    command.Parameters.AddWithValue("@QuantityInStock", NewQuantity);

                    try
                    {
                        connection.Open();
                        rowAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Handle Error Logging
                        //Console.WriteLine($"Error updating product quantity: {ex.Message}");
                    }
                }
            }
            return (rowAffected > 0);
        }
    }
}
