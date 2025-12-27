using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSDataAccessLayer
{
    public class clsInvoiceItemsData
    {
        /*public static int AddInvoiceItem(int Quantity, decimal UnitPrice, decimal LineTotal,  int? ProductID,
                                 int? InvoiceID, int? CreatedByUserID,decimal DiscountPercent, decimal DiscountAmount = 0)
        {
            int InvoiceItemID = -1;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string Query = @"INSERT INTO InvoiceItems
                                (InvoiceID, ProductID, Quantity, UnitPrice, 
                                 DiscountAmount,DiscountPercent, LineTotal, CreatedByUserID)
                             VALUES
                                (@InvoiceID, @ProductID, @Quantity, @UnitPrice, 
                                 @DiscountAmount,@DiscountPercent, @LineTotal, @CreatedByUserID)
                             SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@InvoiceID", InvoiceID);
                    command.Parameters.AddWithValue("@ProductID", ProductID);
                    command.Parameters.AddWithValue("@Quantity", Quantity);
                    command.Parameters.AddWithValue("@UnitPrice", UnitPrice);
                    command.Parameters.AddWithValue("@DiscountAmount", DiscountAmount);
                    command.Parameters.AddWithValue("@DiscountPercent", DiscountPercent);
                    command.Parameters.AddWithValue("@LineTotal", LineTotal);
                    if (CreatedByUserID.HasValue)
                        command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID.Value);
                    else
                        command.Parameters.AddWithValue("@CreatedByUserID", DBNull.Value);


                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            // SCOPE_IDENTITY could be decimal => convert safely
                            InvoiceItemID = Convert.ToInt32(result);
                        }
                    }
                    catch (SqlException ex)
                    {
                        // TODO: Log the error (ex.Message) for debugging
                    }
                }
            }

            return InvoiceItemID;
        }

        public static bool UpdateInvoiceItem(int InvoiceItemID, int Quantity, decimal UnitPrice, decimal LineTotal,
            int? ProductID, int? InvoiceID, int? CreatedByUserID, decimal DiscountPercent, decimal DiscountAmount = 0)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string Query = @"UPDATE InvoiceItems
                             SET Quantity = @Quantity,
                                 UnitPrice = @UnitPrice,
                                 LineTotal = @LineTotal,
                                 ProductID = @ProductID,
                                 InvoiceID = @InvoiceID,
                                 DiscountAmount = @DiscountAmount,
                                 DiscountPercent = @DiscountPercent,
                                 CreatedByUserID = @CreatedByUserID
                             WHERE InvoiceItemID = @InvoiceItemID";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@InvoiceItemID", InvoiceItemID);
                    command.Parameters.AddWithValue("@Quantity", Quantity);
                    command.Parameters.AddWithValue("@UnitPrice", UnitPrice);
                    command.Parameters.AddWithValue("@LineTotal", LineTotal);
                    command.Parameters.AddWithValue("@ProductID", ProductID);
                    command.Parameters.AddWithValue("@InvoiceID", InvoiceID);
                    command.Parameters.AddWithValue("@DiscountAmount", DiscountAmount);
                    command.Parameters.AddWithValue("@DiscountPercent", DiscountPercent);

                    if (CreatedByUserID != null)
                        command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                    else
                        command.Parameters.AddWithValue("@CreatedByUserID", DBNull.Value);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // TODO: Log error
                    }
                }
            }

            return (rowsAffected > 0);
        }

        public static bool DeleteInvoiceItem(int InvoiceItemID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string Query = @"DELETE FROM InvoiceItems WHERE InvoiceItemID = @InvoiceItemID";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@InvoiceItemID", InvoiceItemID);
                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // TODO: Log error
                    }
                }
            }

            return (rowsAffected > 0);
        }

        // Useful: delete all items for a given invoice (when deleting an invoice)
        public static bool DeleteInvoiceItemsByInvoiceID(int InvoiceID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string Query = @"DELETE FROM InvoiceItems WHERE InvoiceID = @InvoiceID";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@InvoiceID", InvoiceID);
                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // TODO: Log error
                    }
                }
            }

            return (rowsAffected > 0);
        }*/

        public static DataTable GetInvoiceItemsByInvoiceID(int? InvoiceID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string Query = @"SELECT ii.InvoiceItemID,p.ProductName,p.Descriptions,ii.Quantity, ii.UnitPrice,
                                        p.TaxPercent * 100 As TaxPercent,ii.DiscountPercent * 100 As DiscountPercent,
                                       ii.DiscountAmount,ii.LineTotal,ii.InvoiceID
                             FROM InvoiceItems ii
                             LEFT JOIN Products p ON ii.ProductID = p.ProductID
                             WHERE ii.InvoiceID = @InvoiceID";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@InvoiceID", InvoiceID);
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
                    catch (Exception ex)
                    {
                        // TODO: Log error
                    }
                }
            }

            return dt;
        }

        public static bool GetInvoiceItemByID(int InvoiceItemID, ref int Quantity, ref decimal UnitPrice,
            ref decimal LineTotal, ref int? ProductID, ref int InvoiceID, ref int? CreatedByUserID, ref decimal DiscountPercent, ref decimal DiscountAmount)
        {
            bool found = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string Query = @"SELECT * FROM InvoiceItems WHERE InvoiceItemID = @InvoiceItemID";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@InvoiceItemID", InvoiceItemID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                found = true;
                                Quantity = reader["Quantity"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Quantity"]);
                                UnitPrice = reader["UnitPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["UnitPrice"]);
                                LineTotal = reader["LineTotal"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["LineTotal"]);
                                ProductID = reader["ProductID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["ProductID"]);
                                InvoiceID = reader["InvoiceID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["InvoiceID"]);
                                CreatedByUserID = reader["CreatedByUserID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["CreatedByUserID"]);
                                DiscountAmount = reader["DiscountAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["DiscountAmount"]);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // TODO: Log error
                        found = false;
                    }
                }
            }

            return found;
        }
    }
}
