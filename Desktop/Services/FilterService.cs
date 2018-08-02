using MedicineApiClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Desktop.Services
{
    public class FilterService
    {
        private CancellationTokenSource _cts;

        public async Task<IEnumerable<Medicine>> FilterAsync(IEnumerable<Medicine> medicines, Func<Medicine, bool> predicate)
        {
            this._cts?.Cancel();

            var cts = new CancellationTokenSource();
            var ct = cts.Token;

            this._cts = cts;

            var task = new Task<IEnumerable<Medicine>>(() => medicines.Where(predicate), ct);

            task.Start();

            return await task;
        }
    }
}
