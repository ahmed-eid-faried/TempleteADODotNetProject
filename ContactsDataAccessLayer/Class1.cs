using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsDataAccessLayer
{
    public class clsContactDataAccess
    {
        //public int ContactID { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Email { get; set; }
        //public string Phone { get; set; }
        //public string Address { get; set; }
        //public int CountryID { get; set; }
        //public DateTime DateOfBirth { get; set; }
        //public string ImagePath { get; set; }
        public static bool GetContactInfoByID(int ContactID, ref string FirstName, ref string LastName,
           ref string Email, ref string Phone, ref string Address,
           ref DateTime DateOfBirth, ref int CountryID, ref string ImagePath)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "Select * From Contacts WHERE ContactID=@ContactID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ContactID", ContactID);
            bool isFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    isFound = true;
                    ContactID = (int)reader["ContactID"];
                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["LastName"];
                    Email = (string)reader["Email"];
                    Phone = (string)reader["Phone"];
                    Address = (string)reader["Address"];
                    CountryID = (int)reader["CountryID"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    //ImagePath: allows null in database so we should handle null
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("error ==>> " + e.Message);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static int AddNewContact(string FirstName, string LastName,
            string Email, string Phone, string Address,
            DateTime DateOfBirth, int CountryID, string ImagePath)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query2 = @"INSERT INTO Contacts
           (FirstName,
            LastName,
            Email,
            Phone,
            Address,
            CountryID,
            DateOfBirth,
            ImagePath)
     VALUES
           (@FirstName, 
            @LastName, 
            @Email, 
            @Phone, 
            @Address, 
            @CountryID,
            @DateOfBirth,
            @ImagePath);";
            SqlCommand command = new SqlCommand(query2, connection);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            if (ImagePath == "")
            {
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            int ContactID = -1;
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    ContactID = insertedID;
                    Console.WriteLine("Record inserted successfully.");
                }
                else
                {
                    Console.WriteLine("Record insertion failed.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error ==>> " + e.Message);
            }
            finally
            {
                connection.Close();
            }
            return ContactID;
        }
        public static bool UpdateContact(int ContactID, string FirstName, string LastName,
        string Email, string Phone, string Address, DateTime DateOfBirth, int CountryID, string ImagePath)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query2 = @"UPDATE Contacts
           SET
           Contacts.FirstName=@FirstName,
           Contacts.LastName=@LastName,
           Contacts.Email=@Email,
           Contacts.Phone=@Phone,
           Contacts.Address=@Address,
           Contacts.CountryID=@CountryID
           WHERE Contacts.ContactID=@ContactID";
            SqlCommand command = new SqlCommand(query2, connection);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone",Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            command.Parameters.AddWithValue("@ContactID", ContactID);
            bool IsUpdated = false;
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    //Console.WriteLine("Record Updated successfully.");
                    IsUpdated = true;
                }
                else
                {
                    //Console.WriteLine("Record Upadate failed.");
                    //IsUpdated = false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error ==>> " + e.Message);
            }
            finally
            {
                connection.Close();
            }
            return IsUpdated;
        }

        public static DataTable GetAllContacts()
        {
            DataTable t = new DataTable();
            return t;
        }
        public static bool DeleteContact(int ContactID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query2 = "delete from Contacts where  Contacts.contactID=@ContactID;";
            SqlCommand command = new SqlCommand(query2, connection);
            command.Parameters.AddWithValue("@ContactID", ContactID);
            bool IsDeleted = false;
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    //Console.WriteLine("Record Deleted successfully.");
                    IsDeleted = true;
                }
                else
                {
                    //Console.WriteLine("Record Delete failed.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error ==>> " + e.Message);
            }
            finally
            {
                connection.Close();
            }
            return IsDeleted;
        }
        public static bool IsContactExist(int ID)
        {
            return true;
        }
    }
}
