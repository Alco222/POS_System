using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandlerErrors;

namespace POSDataAccessLayer
{
    public class clsCustormerData
    {
        public static int? AddNewCustomer(string CustomerName, DateTime RegisterDate, int? CreatedByUserID, string TaxNumber, int LoyaltyPoints, int? PersonID, bool IsActive)
        {
            int? CustomerID =null;
          
            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                //string Query = @"INSERT INTO  Customers(CustomerName,RegisterDate,LoyaltyPoints,TaxNumber,PersonID,CreatedByUserID,IsActive)
                //         Values(@CustomerName,@RegisterDate,@LoyaltyPoints,@TaxNumber,@PersonID,@CreatedByUserID,@IsActive)
                //         SELECT SCOPE_IDENTITY()";

                using (SqlCommand command = new SqlCommand("Customer.SP_AddNewCustomer_POS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CustomerName", CustomerName);
                    command.Parameters.AddWithValue("@RegisterDate", RegisterDate);
                    if (CreatedByUserID.HasValue)
                        command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID.Value);
                    else
                        command.Parameters.AddWithValue("@CreatedByUserID", DBNull.Value);

                    command.Parameters.AddWithValue("@LoyaltyPoints", LoyaltyPoints);
                    command.Parameters.AddWithValue("@TaxNumber", TaxNumber);
                    command.Parameters.AddWithValue("@PersonID", PersonID.Value);
                    command.Parameters.AddWithValue("@IsActive", IsActive);

                   SqlParameter outputIdParam = new SqlParameter("@NewCustomerID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();

                        if (command.Parameters["@NewCustomerID"].Value != DBNull.Value)
                            CustomerID = Convert.ToInt32(command.Parameters["@NewCustomerID"].Value);

                    }
                    catch (SqlException se)
                    {
                        LogException.logException(se.Message, EventLogEntryType.Error);
                    }
                    catch (Exception ex)
                    {
                        // Handler Error Logges
                        CustomerID = null;
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                    }
                }
            }
            return CustomerID;
        }

        public static bool UpdateCustomer(int? CustomerID,string CustomerName, DateTime RegisterDate, int? CreatedByUserID, string TaxNumber, int LoyaltyPoints, int? PersonID, bool IsActive)
        {
            int rowAffected = 0;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("Customer.SP_UpdateCustomer_POS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CustomerName", CustomerName);
                    command.Parameters.AddWithValue("@RegisterDate", RegisterDate);
                    if (CreatedByUserID.HasValue)
                        command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID.Value);
                    else
                        command.Parameters.AddWithValue("@CreatedByUserID", DBNull.Value);

                    command.Parameters.AddWithValue("@LoyaltyPoints", LoyaltyPoints);
                    command.Parameters.AddWithValue("@TaxNumber", TaxNumber);

                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@IsActive", IsActive);
                    command.Parameters.AddWithValue("@CustomerID", CustomerID);

                    command.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        rowAffected = (int)command.Parameters["@ReturnValue"].Value;
                    }
                    catch (SqlException se)
                    {
                        // Handler Error Logges
                        LogException.logException(se.Message, EventLogEntryType.Error);
                    }
                    catch (Exception ex)
                    {
                        // Handler Error Logges
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                    }
                }
                return (rowAffected > 0);
            }
        }

        public static bool DeleteCustomer(int? CustomerID)
        {
            int rowAffected = 0;
            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                //string Query = @"Delete From Customers where CustomerID = @CustomerID";
                using (SqlCommand command = new SqlCommand("Customer.SP_DeleteCustomer_POS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CustomerID", CustomerID);
                    command.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        rowAffected = (int)command.Parameters["@ReturnValue"].Value;
                    }
                    catch (SqlException se)
                    {
                        rowAffected = 0;
                        // Handler Error Logges
                        LogException.logException(se.Message, EventLogEntryType.Error);
                    }
                    catch (Exception ex)
                    {
                        rowAffected = 0;
                        // Handler Error Logges
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                    }
                }
            }
            return (rowAffected > 0);
        }

        public static bool GetCustomerByID(int? CustomerID,ref string CustomerName, ref DateTime RegisterDate, ref int? CreatedByUserID, ref string TaxNumber, ref int LoyaltyPoints, ref int? PersonID, ref bool IsActive)
        {
            
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string Query = @"SELECT * From Customers where CustomerID = @CustomerID";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                   
                    command.Parameters.AddWithValue("@CustomerID", CustomerID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                CustomerName = (string)reader["CustomerName"];
                                RegisterDate = (DateTime)reader["RegisterDate"];
                                if (reader["CreatedByUserID"] == DBNull.Value)
                                    CreatedByUserID =null;
                                else
                                    CreatedByUserID = (int)reader["CreatedByUserID"];

                                LoyaltyPoints = (int)reader["LoyaltyPoints"];
                                TaxNumber = (string)reader["TaxNumber"];
                                PersonID = (int)reader["PersonID"];
                                IsActive = (bool)reader["IsActive"];

                            }
                            else
                                IsFound = false;
                        }
                    }
                    catch (SqlException ex)
                    {
                        IsFound = false;
                        // Handler Error Logges
                    }
                }
            }
            return IsFound;
        }

        public static bool GetCustomerByName(ref int? CustomerID,string CustomerName, ref DateTime RegisterDate, ref int? CreatedByUserID, ref string TaxNumber, ref int LoyaltyPoints, ref int? PersonID, ref bool IsActive)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string Query = @"SELECT * From Customers where CustomerName = @CustomerName";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {

                    command.Parameters.AddWithValue("@CustomerName", CustomerName);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                if (reader["CustomerID"] == DBNull.Value)
                                     CustomerID = null;
                                else
                                     CustomerID = (int)reader["CustomerID"];

                                RegisterDate = (DateTime)reader["RegisterDate"];
                                if (reader["CreatedByUserID"] == DBNull.Value)
                                    CreatedByUserID = null;
                                else
                                    CreatedByUserID = (int)reader["CreatedByUserID"];

                                LoyaltyPoints = (int)reader["LoyaltyPoints"];
                                TaxNumber = (string)reader["TaxNumber"];
                                PersonID = (int)reader["PersonID"];
                                IsActive = (bool)reader["IsActive"];

                            }
                            else
                                IsFound = false;
                        }
                    }
                    catch (SqlException ex)
                    {
                        IsFound = false;
                        // Handler Error Logges
                    }
                }
            }
            return IsFound;
        }

        public static bool IsCustomerExist(int? PersonID)
         {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string Query = @"SELECT Found = 1 From Customers where PersonID = @PersonID";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                           IsFound = reader.HasRows;
                        }
                    }
                    catch (SqlException ex)
                    {
                        IsFound = false;
                        // Handler Error Logges
                    }
                }
            }
            return IsFound;
        }

        public static bool IsCustomerExist(string TaxNumber)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string Query = @"SELECT Found = 1 From Customers where TaxNumber = @TaxNumber";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@TaxNumber", TaxNumber);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            IsFound = reader.HasRows;
                        }
                    }
                    catch (SqlException ex)
                    {
                        IsFound = false;
                        // Handler Error Logges
                    }
                }
            }
            return IsFound;
        }

        public static DataTable GetAllCustomer()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string Query = @"SELECT c.CustomerID,c.CustomerName,
                                        p.NationalNo,c.TaxNumber,c.LoyaltyPoints,
                                        c.RegisterDate,c.IsActive
                                 FROM Customers c
                                 INNER JOIN People p ON c.PersonID = p.PersonID";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                   
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                                dt.Load(reader);
                        }
                    }
                    catch (SqlException se)
                    {
                        // Handler Error Logges
                        LogException.logException(se.Message, EventLogEntryType.Error);
                    }
                    catch (Exception ex)
                    {
                        // Handler Error Logges
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                    }
                }
            }
            return dt;
        }

        public static DataTable GetAllCustomer2(int currentPage, int PAGE_SIZE)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {  
                using (SqlCommand command = new SqlCommand("Customer.SP_GetAllCustomer_POS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@PageNumber", SqlDbType.Int).Value = currentPage;
                    command.Parameters.Add("@RowsPerPage", SqlDbType.Int).Value = PAGE_SIZE;
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.HasRows)
                                dt.Load(reader);
                        }
                    }
                    catch (SqlException se)
                    {
                        // Handler Error Logges
                        LogException.logException(se.Message, EventLogEntryType.Error);
                    }
                    catch (Exception ex)
                    {
                        // Handler Error Logges
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                    }
                }
            }
            return dt;
        }
    
    }
}
