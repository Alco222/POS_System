using HandlerErrors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSDataAccessLayer
{
    public class clsInvoiceData
    {
        public static int AddNewInvoice(DateTime InvoiceDate, decimal TotalAmount, int? CustomerID, string PaymentMethod,
                   int? CreatedByUserID, DataTable InvoiceItem, decimal SubTotal = 0, decimal TotalTax = 0,
                   decimal TotalDiscount = 0,string Status = "Issued")
        {
            int InvoiceID = -1;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("Invoice.Sp_AddNewInvoice_POS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@InvoiceDate", InvoiceDate);
                    command.Parameters.AddWithValue("@TotalAmount", TotalAmount);
                    command.Parameters.AddWithValue("@CustomerID", CustomerID);
                    command.Parameters.AddWithValue("@SubTotal", SubTotal);
                    command.Parameters.AddWithValue("@TotalTax", TotalTax);
                    command.Parameters.AddWithValue("@TotalDiscount", TotalDiscount);
                    command.Parameters.AddWithValue("@PaymentMethod", PaymentMethod);
                    command.Parameters.AddWithValue("@Status", Status);

                    if (CreatedByUserID.HasValue)
                        command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID.Value);
                    else
                        command.Parameters.AddWithValue("@CreatedByUserID", DBNull.Value);

                    SqlParameter tvpParam = new SqlParameter("@Items", SqlDbType.Structured);
                    tvpParam.TypeName = "Invoice.InvoiceItemType";
                    tvpParam.Value = InvoiceItem;
                    command.Parameters.Add(tvpParam);

                    SqlParameter outputParam = new SqlParameter("@NewInvoiceID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParam);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();

                        InvoiceID = (outputParam.Value == DBNull.Value) ? -1 : Convert.ToInt32(outputParam.Value);
                    }
                    catch (SqlException se)
                    {
                        // Handle Error Logging
                        LogException.logException(se.Message, System.Diagnostics.EventLogEntryType.Error);
                    }
                    catch (Exception ex)
                    {
                        // Handle Error Logging
                        LogException.logException(ex.Message, System.Diagnostics.EventLogEntryType.Error);
                    }
                }
                return InvoiceID;
            }
        }

        public static bool UpdateInvoice(int InvoiceID, DateTime InvoiceDate, decimal TotalAmount,
            int? CustomerID, decimal SubTotal, decimal TotalTax, decimal TotalDiscount,
            string PaymentMethod, int? CreatedByUserID, DataTable InvoiceItem,string Status)
        {
            bool Success = false ;
            Status = "Issued";
            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
               

                using (SqlCommand command = new SqlCommand("Invoice.Sp_UpdateInvoice_POS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@InvoiceID", InvoiceID);
                    command.Parameters.AddWithValue("@InvoiceDate", InvoiceDate);
                    command.Parameters.AddWithValue("@TotalAmount", TotalAmount);
                    command.Parameters.AddWithValue("@CustomerID", CustomerID);
                    command.Parameters.AddWithValue("@SubTotal", SubTotal);
                    command.Parameters.AddWithValue("@TotalTax", TotalTax);
                    command.Parameters.AddWithValue("@TotalDiscount", TotalDiscount);
                    command.Parameters.AddWithValue("@PaymentMethod", PaymentMethod);
                    command.Parameters.AddWithValue("@Status", Status);
                    if (CreatedByUserID != null)
                        command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                    else
                        command.Parameters.AddWithValue("@CreatedByUserID", DBNull.Value);

                    SqlParameter tvpParam = new SqlParameter("@Items", SqlDbType.Structured);
                    tvpParam.TypeName = "Invoice.InvoiceItemType";
                    tvpParam.Value = InvoiceItem;
                    command.Parameters.Add(tvpParam);

                    SqlParameter successParam = new SqlParameter("@Success", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(successParam);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();

                        Success = successParam.Value != DBNull.Value &&
                                   Convert.ToBoolean(successParam.Value);
                    }
                    catch (SqlException se)
                    {
                        // Handle Error Logging
                        LogException.logException(se.Message, System.Diagnostics.EventLogEntryType.Error);
                    }
                    catch (Exception ex)
                    {
                        // Handle Error Logging
                        LogException.logException(ex.Message, System.Diagnostics.EventLogEntryType.Error);
                    }
                }
            }
            return Success;
        
        }

        public static bool DeleteInvoice(int InvoiceID)
        {
            int rowAffected = 0;
            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                //string Query = @"DELETE FROM Invoices WHERE InvoiceID = @InvoiceID";
                using (SqlCommand command = new SqlCommand("Invoice.Sp_DeleteInvoice_POS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@InvoiceID", InvoiceID);
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
                        //Console.WriteLine($"SQL Error deleting invoice: {se.Message}");
                        LogException.logException(se.Message, System.Diagnostics.EventLogEntryType.Error);
                    }
                    catch (Exception ex)
                    {
                        // Handle Error Logging
                        //Console.WriteLine($"Error deleting invoice: {ex.Message}");
                        LogException.logException(ex.Message, System.Diagnostics.EventLogEntryType.Error);
                    }
                }
            }
            return (rowAffected > 0);
        }

        public static bool CanceledInvoice(int InvoiceID)
        {
            int rowAffected = 0;
            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                //string Query = @"DELETE FROM Invoices WHERE InvoiceID = @InvoiceID";
                using (SqlCommand command = new SqlCommand("Invoice.Sp_CanceledInvoice_POS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@InvoiceID", InvoiceID);
                    try
                    {
                        connection.Open();
                        rowAffected = command.ExecuteNonQuery();
                    }
                    catch (SqlException se)
                    {
                        // Handle Error Logging
                        //Console.WriteLine($"SQL Error deleting invoice: {se.Message}");
                        LogException.logException(se.Message, System.Diagnostics.EventLogEntryType.Error);
                    }
                    catch (Exception ex)
                    {
                        // Handle Error Logging
                        //Console.WriteLine($"Error deleting invoice: {ex.Message}");
                        LogException.logException(ex.Message, System.Diagnostics.EventLogEntryType.Error);
                    }
                }
            }
            return (rowAffected > 0);
        }

        public static DataTable GetAllInvoices(int currentPage, int PAGE_SIZE)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("Invoice.SP_GetAllInvoice_POS ", connection))
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
                    catch (Exception ex)
                    {
                        // Handle Error Logging
                        //Console.WriteLine($"Error getting invoices: {ex.Message}");
                    }
                }
            }
            return dt;
        }

        public static bool GetInvoiceByInvoiceID(int InvoiceID, ref DateTime InvoiceDate,
            ref decimal TotalAmount, ref int? CustomerID, ref decimal SubTotal,
            ref decimal TotalTax, ref decimal TotalDiscount, ref string PaymentMethod,
            ref int? CreatedByUserID)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string Query = @"SELECT * FROM Invoices WHERE InvoiceID = @InvoiceID";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@InvoiceID", InvoiceID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;

                                InvoiceDate = (DateTime)reader["InvoiceDate"];
                                TotalAmount = (decimal)reader["TotalAmount"];
                                CustomerID = (int)reader["CustomerID"];
                                PaymentMethod = (string)reader["PaymentMethod"];
                                CreatedByUserID = (reader["CreateByUserID"] == DBNull.Value) ? -1 : (int)reader["CreateByUserID"];


                                // Handle optional fields that might be added later
                                SubTotal = reader["SubTotal"] == DBNull.Value ? 0 : (decimal)reader["SubTotal"];
                                TotalTax = reader["TotalTax"] == DBNull.Value ? 0 : (decimal)reader["TotalTax"];
                                TotalDiscount = reader["TotalDiscount"] == DBNull.Value ? 0 : (decimal)reader["TotalDiscount"];
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        IsFound = false;
                        // Handle Error Logging
                        //Console.WriteLine($"Error getting invoice by ID: {ex.Message}");
                    }
                }
            }
            return IsFound;
        }

        public static DataTable GetInvoicesByCustomerID(int? CustomerID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string Query = @"SELECT InvoiceID, InvoiceDate, TotalAmount, PaymentMethod
                             FROM Invoices 
                             WHERE CustomerID = @CustomerID
                             ORDER BY InvoiceDate DESC";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerID", CustomerID);

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
                        // Handle Error Logging
                        //Console.WriteLine($"Error getting customer invoices: {ex.Message}");
                    }
                }
            }
            return dt;
        }

        public static DataTable GetInvoicesByDateRange(DateTime startDate, DateTime endDate)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string Query = @"SELECT i.InvoiceID, i.InvoiceDate, i.TotalAmount, i.PaymentMethod,
                                    p.FirstName + ' ' + p.LastName AS CustomerName
                             FROM Invoices i
                             INNER JOIN Customers c ON i.CustomerID = c.CustomerID
                             INNER JOIN People p ON c.PersonID = p.PersonID
                             WHERE i.InvoiceDate BETWEEN @StartDate AND @EndDate
                             ORDER BY i.InvoiceDate";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);

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
                        // Handle Error Logging
                        //Console.WriteLine($"Error getting invoices by date range: {ex.Message}");
                    }
                }
            }
            return dt;
        }
    }
}
