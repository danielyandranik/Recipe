using System.Collections.Generic;
using System.Data.SqlClient;
using DatabaseAccess;

/******** Tests with some stored procedures ********/
 
namespace Test
{
    class Program
    {
        static string GetUsersDbCnnString()
        {
            return new SqlConnectionStringBuilder
            {
                DataSource = "(local)",
                InitialCatalog = "UsersDB",
                IntegratedSecurity = true
            }.ConnectionString;
        }

        static string GetProfilesDbCnnString()
        {
            return new SqlConnectionStringBuilder
            {
                DataSource = "(local)",
                InitialCatalog = "ProfilesDb",
                IntegratedSecurity = true
            }.ConnectionString;
        }

        static void Main(string[] args)
        {
            var affectedRowsAfterCreate = TestCreateUser();

            var list = TestGetUsers();

            var affectedRowsAfterCreateProfile = TestCreateDoctorProfiel();
        }

        static int TestCreateUser()
        {
            var dataAccess = new SpExecuter(GetUsersDbCnnString());

            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("firstName","Pablo"),
                new KeyValuePair<string, object>("lastName","Avagian"),
                new KeyValuePair<string, object>("middleName","Andre"),
                new KeyValuePair<string, object>("username","pablo_us"),
                new KeyValuePair<string, object>("birthdate","1975-02-14"),
                new KeyValuePair<string, object>("email","pablo.avagian@gmail.com"),
                new KeyValuePair<string, object>("password","password"),
                new KeyValuePair<string, object>("phone","055954464223"),
                new KeyValuePair<string, object>("sex","M")
            };

            var affectedRows = dataAccess.ExecuteSpNonQuery("uspCreateUser", parameters);

            return affectedRows;
        }

        static IEnumerable<User> TestGetUsers()
        {
            var dataAccess = new SpExecuter(GetUsersDbCnnString());

            var list = dataAccess.ExecuteSp<User>("uspGetUsers");

            return list;
        }

        static int TestCreateDoctorProfiel()
        {
            var dataAcess = new SpExecuter(GetProfilesDbCnnString());

            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("userId",2),
                new KeyValuePair<string, object>("license","doctor license"),
                new KeyValuePair<string, object>("hospitalId",4533)
            };

            var affectedRows = dataAcess.ExecuteSpNonQuery("uspCreateDoctorProfile", parameters);

            return affectedRows; 
        }
    }
}
