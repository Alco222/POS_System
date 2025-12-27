using HandlerErrors;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
namespace POSDataAccessLayer
{
    public class clsPersonData
    {
        public static int? AddNewPerson(string FirstName, string LastName, string NationalNo, string Email, string Phone, byte Gender, string Address, DateTime DateOfBirth, string ImagePath)
        {
            int PersonID = -1;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("People.SP_AddNewPerson_POS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);
                    command.Parameters.AddWithValue("@Email", (object)Email ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Phone", Phone);
                    command.Parameters.AddWithValue("@Gender", Gender);
                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@ImagePerson", (object)ImagePath ?? DBNull.Value);

                    SqlParameter outputParam = new SqlParameter("@NewPersonID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParam);


                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();

                        PersonID = (outputParam.Value == DBNull.Value) ? -1 : Convert.ToInt32(outputParam.Value);

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
            return PersonID;
        }

        public static bool UpdatePerson(int? PersonID, string FirstName, string LastName, string NationalNo, string Email, string Phone, byte Gender, string Address, DateTime DateOfBirth, string ImagePath)
        {
            int rowAffected = 0;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("People.SP_UpdatePerson_POS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);
                    command.Parameters.AddWithValue("@Email", (object)Email ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Phone", Phone);
                    command.Parameters.AddWithValue("@Gender", Gender);
                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@ImagePerson", (object)ImagePath ?? DBNull.Value);

                    command.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;


                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        rowAffected = (int)command.Parameters["@ReturnValue"].Value;

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
            return (rowAffected > 0);
        }

        public static bool DeletePerson(int? PersonID)
        {
            int rowAffected = 0;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("People.SP_DeletePerson_POS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID",PersonID);
                    command.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        rowAffected = (int)command.Parameters["@ReturnValue"].Value;
                    }
                    catch (SqlException se)
                    {
                        // FK constraint violation
                        if (se.Number == 547)
                        {
                            // هذا سلوك طبيعي → لا نرمي Exception
                            return false;
                        }

                        // أي خطأ آخر = خطأ حقيقي
                        LogException.logException(se.ToString(), EventLogEntryType.Error);
                        return false;
                    }
                    catch (Exception ex)
                    {
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                    }
                }
            }
            return (rowAffected > 0);
        }

        public static DataTable GetAllPeople(int currentPage, int PAGE_SIZE)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("People.SP_GetAllPerson_POS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@PageNumber", SqlDbType.Int).Value = currentPage;
                    command.Parameters.Add("@RowsPerPage", SqlDbType.Int).Value = PAGE_SIZE;


                    try
                    {
                         connection.Open();
                        using (SqlDataReader reader =  command.ExecuteReader())
                        {
                            if (reader.HasRows)
                                dt.Load(reader);
                        }
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
            //Task.Delay(900000);
            return dt;
        }

        public static bool GetPersonByPersonID(int? PersonID, ref string FirstName, ref string LastName, ref string NationalNo, ref string Email, ref string Phone, ref byte Gender, ref string Address, ref DateTime DateOfBirth, ref string ImagePath)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string Query = @"SELECT * FROM People WHERE PersonID = @PersonID";

                using (SqlCommand command = new SqlCommand(Query, connection))
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
                                FirstName = reader["FirstName"].ToString();
                                LastName = reader["LastName"].ToString();
                                NationalNo = reader["NationalNo"].ToString();
                                Phone = reader["Phone"].ToString();
                                Email = reader["Email"] == DBNull.Value ? "" : reader["Email"].ToString();
                                Gender = (byte)reader["Gender"];
                                Address = reader["Address"].ToString();
                                DateOfBirth = (DateTime)reader["DateOfBirth"];
                                ImagePath = reader["ImagePerson"] == DBNull.Value ? "" : reader["ImagePerson"].ToString();
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

        public static bool GetPersonByNationalNo(ref int? PersonID, ref string FirstName, ref string LastName, string NationalNo, ref string Email, ref string Phone, ref byte Gender, ref string Address, ref DateTime DateOfBirth, ref string ImagePath)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string Query = @"SELECT * FROM People WHERE NationalNo = @NationalNo";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                PersonID = (int)reader["PersonID"];
                                FirstName = reader["FirstName"].ToString();
                                LastName = reader["LastName"].ToString();
                                Phone = reader["Phone"].ToString();
                                Email = reader["Email"] == DBNull.Value ? "" : reader["Email"].ToString();
                                Gender = (byte)reader["Gender"];
                                Address = reader["Address"].ToString();
                                DateOfBirth = (DateTime)reader["DateOfBirth"];
                                ImagePath = reader["ImagePerson"] == DBNull.Value ? "" : reader["ImagePerson"].ToString();
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

        public static bool IsPersonExists(int? PersonID)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string Query = @"SELECT 1 FROM People WHERE PersonID = @PersonID";

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
                    catch (Exception ex)
                    {
                        LogException.logException(ex.Message, EventLogEntryType.Error);
                    }
                }
            }
            return IsFound;
        }

        public static bool IsPersonExists(string NationalNo)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(DataAccessSetings.ConnectionString))
            {
                string Query = @"SELECT 1 FROM People WHERE NationalNo = @NationalNo";

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);

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
                    }
                }
            }
            return IsFound;
        }

    }
}

