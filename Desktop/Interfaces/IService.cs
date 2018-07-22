using System.Threading.Tasks;

namespace Desktop.Interfaces
{
    /// <summary>
    /// Interface for services
    /// </summary>
    /// <typeparam name="T">Type of result</typeparam>
    public interface IService<T>
    {
        /// <summary>
        /// Executes the service
        /// </summary>
        /// <param name="parameter">parameter</param>
        /// <returns>task</returns>
        Task<T> Execute(object parameter);
    }
}
