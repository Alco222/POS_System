using System;
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
    public class clsUserData
    {
        public static int AddNewUser(string UserName, string PasswordHash, int RoleID, bool IsActive, int PersonID,DateTime CreatedAt,DateTime? UpdateAt)
        {
            int UserID = -1;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("Users.SP_AddNewUser_POS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@PasswordHash", PasswordHash);
                    command.Parameters.AddWithValue("@RoleID", RoleID);
                    command.Parameters.AddWithValue("@IsActive", IsActive);
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("CreatedAt", CreatedAt);
                    if (UpdateAt.HasValue)
                        command.Parameters.AddWithValue("@UpdateAt", UpdateAt.Value);
                    else
                        command.Parameters.AddWithValue("@UpdateAt", DBNull.Value);
                    SqlParameter outputIdParam = new SqlParameter("@NewUserID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();

                        UserID = (outputIdParam.Value == DBNull.Value) ? -1 : Convert.ToInt32(outputIdParam.Value);
                    }
                    catch (SqlException se)
                    {
                        LogException.logException(se.Message, EventLogEntryType.Error);

                    }
                    catch (Exception ex)
                    {
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                    }
                }
            }
            return UserID;
        }

        public static bool UpdateUser(int UserID, string UserName, int RoleID, bool IsActive,DateTime? UpdateAt)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("Users.SP_UpdateUser_POS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@RoleID", RoleID);
                    command.Parameters.AddWithValue("@IsActive", IsActive);
                    command.Parameters.AddWithValue("@UserID", UserID);
                    if (UpdateAt.HasValue)
                        command.Parameters.AddWithValue("@UpdateAt", UpdateAt.Value);
                    else
                        command.Parameters.AddWithValue("@UpdateAt", DBNull.Value);

                    command.Parameters.Add("@RowsAffected", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        rowsAffected = (int)command.Parameters["@RowsAffected"].Value;
                    }
                    catch (SqlException se)
                    {
                        LogException.logException(se.Message, EventLogEntryType.Error);
                    }
                    catch (Exception ex)
                    {
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                    }
                }
            }
            return rowsAffected > 0;
        }

        public static bool DeleteUser(int UserID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                //string query = "DELETE FROM Users WHERE UserID = @UserID";

                using (SqlCommand command = new SqlCommand("Users.SP_DeleteUer_POS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", UserID);
                    command.Parameters.Add("@RowsAffected", SqlDbType.Int).Direction = ParameterDirection.Output;

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        rowsAffected = (int)command.Parameters["@RowsAffected"].Value;
                    }
                    catch (SqlException se)
                    {
                        LogException.logException(se.Message, EventLogEntryType.Error);
                    }
                    catch (Exception ex)
                    {
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                    }
                }
            }
            return rowsAffected > 0;
        }

        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string query = @"SELECT 
                            UserID, UserName, RoleID,
                            IsActive, CreatedAt, PersonID
                         FROM Users";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                    }
                }
            }
            return dt;
        }

        public static DataTable GetAllUsers2(int currentPage, int PAGE_SIZE)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                //string query = @"SELECT 
                //            UserID, UserName, RoleID,
                //            IsActive, CreatedAt, PersonID
                //         FROM Users";

                using (SqlCommand command = new SqlCommand("Users.SP_GetAllUser_POS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PageNumber", currentPage);
                    command.Parameters.AddWithValue("@RowsPerPage", PAGE_SIZE);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.HasRows)
                               dt.Load(reader);
                        }
                    }
                    catch(SqlException se)
                    {
                        LogException.logException(se.Message, EventLogEntryType.Error);
                    }
                    catch (Exception ex)
                    {
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                    }
                }
            }
            return dt;
        }

        public static bool GetUserByUserID(int UserID,
            ref string UserName, ref string PasswordHash,
            ref int RoleID, ref bool IsActive, ref DateTime CreatedAt, ref int PersonID, ref DateTime? UpdateAt)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string query = "SELECT * FROM Users WHERE UserID = @UserID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", UserID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;

                                UserName = (string)reader["UserName"];
                                PasswordHash = (string)reader["PasswordHash"];
                                RoleID = (int)reader["RoleID"];
                                IsActive = (bool)reader["IsActive"];
                                CreatedAt = (DateTime)reader["CreatedAt"];
                                PersonID = (int)reader["PersonID"];
                                if (reader["UpdateAt"] != DBNull.Value)
                                    UpdateAt = (DateTime)reader["UpdateAt"];
                                else
                                    UpdateAt = null;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                    }
                }
            }

            return IsFound;
        }

        public static bool GetUserByUserName(
            ref int UserID, string UserName, ref string PasswordHash,
            ref int RoleID, ref bool IsActive, ref DateTime CreatedAt, ref int PersonID,ref DateTime? UpdateAt)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string query = "SELECT * FROM Users WHERE UserName = @UserName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", UserName);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;

                                UserID = (int)reader["UserID"];
                                PasswordHash = (string)reader["PasswordHash"];
                                RoleID = (int)reader["RoleID"];
                                IsActive = (bool)reader["IsActive"];
                                CreatedAt = (DateTime)reader["CreatedAt"];
                                PersonID = (int)reader["PersonID"];
                                if (reader["UpdateAt"] != DBNull.Value)
                                    UpdateAt = (DateTime)reader["UpdateAt"];
                                else
                                    UpdateAt = null;

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                    }
                }
            }

            return IsFound;
        }

        public static bool GetUserByPersonID(int PersonID,
            ref int UserID, ref string UserName, ref string PasswordHash,
            ref int RoleID, ref bool IsActive, ref DateTime CreatedAt,ref DateTime? UpdateAt)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string query = "SELECT * FROM Users WHERE PersonID = @PersonID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;

                                UserID = (int)reader["UserID"];
                                UserName = (string)reader["UserName"];
                                PasswordHash = (string)reader["PasswordHash"];
                                RoleID = (int)reader["RoleID"];
                                IsActive = (bool)reader["IsActive"];
                                CreatedAt = (DateTime)reader["CreatedAt"];
                                if (reader["UpdateAt"] != DBNull.Value)
                                    UpdateAt = (DateTime)reader["UpdateAt"];
                                else
                                    UpdateAt = null;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                    }
                }
            }

            return IsFound;
        }

        public static bool LoginUser(ref int UserID, string UserName, string PasswordHash, ref int RolID, ref bool IsActive,ref DateTime CreateAt,ref int PersonID)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string query = "Select * From Users Where UserName = @UserName And PasswordHash = @PasswordHash";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PasswordHash", PasswordHash);
                    command.Parameters.AddWithValue("@UserName", UserName);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                UserID = (int)reader["UserID"];
                                RolID = (int)reader["RoleID"];
                                IsActive = (bool)reader["IsActive"];
                                PersonID = (int)reader["PersonID"];
                            }
                            else
                            {
                                IsFound = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                        IsFound = false;
                    }
                }
            }
            return IsFound;
        }

        public static async Task<(bool IsFound, int UserID, bool IsActive, int PersonID)> LoginUserAsync(string UserName, string PasswordHash)
        {
            int UserID = -1;
            bool IsActive = false;
            int PersonID = -1;
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string query = "Select * From Users Where UserName = @UserName And PasswordHash = @PasswordHash";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PasswordHash", PasswordHash);
                    command.Parameters.AddWithValue("@UserName", UserName);

                    try
                    {
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                IsFound = true;
                                UserID = (int)reader["UserID"];
                                IsActive = (bool)reader["IsActive"];
                                PersonID = (int)reader["PersonID"];
                            }
                            else
                            {
                                IsFound = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                        IsFound = false;
                    }
                }
            }
            return (IsFound, UserID, IsActive, PersonID);
        }

        public static bool IsUserExists(string UserName)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string query = "Select * From Users Where UserName = @UserName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", UserName);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            IsFound = reader.HasRows;
                        }
                    }
                    catch (Exception ex)
                    {
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                        IsFound = false;
                    }
                }
            }
            return IsFound;
        }

        public static bool IsUserExists(int PersonID)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string query = "Select Found = 1 From Users Where PersonID = @PersonID";

                using (SqlCommand command = new SqlCommand(query, connection))
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
                    catch (Exception ex)
                    {
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                        IsFound = false;
                    }
                }
            }
            return IsFound;
        }

        public static bool ChangePassword(int UserID, string NewPasswordHash)
        {
            bool rowsAffected = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string query = @"UPDATE Users
                         SET PasswordHash = @PasswordHash
                         WHERE UserID = @UserID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PasswordHash", NewPasswordHash);
                    command.Parameters.AddWithValue("@UserID", UserID);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery() > 0;
                    }
                    catch (Exception ex)
                    {
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                    }
                }
            }

            return rowsAffected;
        }
    }
}
