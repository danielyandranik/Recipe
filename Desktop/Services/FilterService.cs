using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MedicineApiClient;

namespace Desktop.Services
{
    /// <summary>
    /// Filter service
    /// </summary>
    public class FilterService
    {
        /// <summary>
        /// Cancellation token source for task
        /// </summary>
        private CancellationTokenSource _cts;

        /// <summary>
        /// Filters the medicines
        /// </summary>
        /// <param name="medicines">Medicines</param>
        /// <param name="predicate">Predicate</param>
        /// <returns>enumerable of medicines</returns>
        public async Task<IEnumerable<Medicine>> FilterAsync(IEnumerable<Medicine> medicines, Func<Medicine, bool> predicate)
        {
            // cancel the previous task if it exists
            this._cts?.Cancel();

            var cts = new CancellationTokenSource();
            var ct = cts.Token;

            this._cts = cts;

            // creating new task
            var task = new Task<IEnumerable<Medicine>>(() => medicines.Where(predicate), ct);

            // starting task
            task.Start();

            // returning enumerable of medicines
            return await task;
        }
    }
}
