using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAccess.Repository;
using DatabaseAccess.SpExecuters;

namespace AuthAPI.UsersRepository
{
    /// <summary>
    /// User Repository
    /// </summary>
    public class UserRepository:IUserRepository
    {
        /// <summary>
        /// Stored procedure executer
        /// </summary>
        private Repo<User, SpExecuter> _repo;

        /// <summary>
        /// Creates new instance of User repository
        /// </summary>
        public UserRepository()
        {
            this._repo = new Repo<User, SpExecuter>("UserMap.xml");
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
                return (User)this._repo.ExecuteOperation("GetUserByName",
                    new[]
                    {
                        new KeyValuePair<string,object>("username",userName)
                    });
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
                return (User)this._repo.ExecuteOperation("GetUserById",
                    new[]
                    {
                        new KeyValuePair<string,object>("id",id)
                    });
            });

            task.Start();

            return task;
        }
    }
}
