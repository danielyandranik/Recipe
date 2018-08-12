using System.Threading.Tasks;

namespace Desktop.Interfaces
{
    /// <summary>
    /// Interface for Load Service
    /// </summary>
    public interface ILoadService
    {
        /// <summary>
        /// Loads content
        /// </summary>
        /// <returns>loading task</returns>
        Task Load();
    }
}
