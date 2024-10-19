using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsDataAccessLayer
{
    static class clsDataAccessSettings
    {
        public static string ConnectionString = "Server=.;Database=ContactsDB2;User Id=sa;Password=123456;";
    }
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
        public static bool GetContactInfoByID(int ID, ref string FirstName, ref string LastName,
           ref string Email, ref string Phone, ref string Address,
           ref DateTime DateOfBirth, ref int CountryID, ref string ImagePath)
        {
            return true;
        }
        public static int AddNewContact(string FirstName, string LastName,
            string Email, string Phone, string Address,
            DateTime DateOfBirth, int CountryID, string ImagePath)
        {
            return 0;
        }
        public static bool UpdateContact(int ID, string FirstName, string LastName,
        string Email, string Phone, string Address, DateTime DateOfBirth, int CountryID, string ImagePath)
        {
            return true;
        }

        public static DataTable GetAllContacts()
        {
            DataTable t = new DataTable();
            return t;
        }

    }
}
