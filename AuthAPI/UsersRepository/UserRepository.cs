using System.Collections.Generic;
using System.Threading.Tasks;
using DatabaseAccess.Repository;

namespace AuthAPI.UsersRepository
{
    /// <summary>
    /// User Repository
    /// </summary>
    public class UserRepository:IUserRepository
    {
        /// <summary>
        /// Repository
        /// </summary>
        private Repo<User> _repo;

        /// <summary>
        /// Creates new instance of User repository
        /// </summary>
        public UserRepository(Repo<User> repo)
        {
            this._repo = repo;
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
