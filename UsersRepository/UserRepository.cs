using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAccess;

namespace UsersRepository
{
    /// <summary>
    /// User Repository
    /// </summary>
    public class UserRepository:IUserRepository
    {
        /// <summary>
        /// Stored procedure executer
        /// </summary>
        private SpExecuter spExecuter;

        /// <summary>
        /// Creates new instance of User repository
        /// </summary>
        public UserRepository()
        {
            var cnnStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = "(local)",
                InitialCatalog = "UsersDB",
                IntegratedSecurity = true
            };

            // constructing stored procedure executer
            this.spExecuter = new SpExecuter(cnnStringBuilder.ConnectionString);
        }

        /// <summary>
        /// Finds user by username
        /// </summary>
        /// <param name="userName">Username</param>
        /// <returns>user</returns>
        public Task<User> FindAsync(string userName)
        {
            var task = new Task<User>(() =>
            {
                return this.spExecuter.ExecuteSp<User>(
                    "uspGetUserByUsername",
                    new[] { new KeyValuePair<string, object>("userName", userName) })
                    .First();
            });

            task.Start();

            return task;
        }

        /// <summary>
        /// Finds user by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Id</returns>
        public Task<User> FindAsync(int id)
        {
            var task = new Task<User>(() =>
            {
                return this.spExecuter.ExecuteSp<User>(
                    "uspGetUserById",
                    new[] { new KeyValuePair<string, object>("UserId", id) })
                    .First();
            });

            task.Start();

            return task;
        }
    }
}
