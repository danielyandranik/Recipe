using MedicineApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Commands
{
    class AddMedicineCommand : AsyncCommand<Medicine, bool>
    {
        public AddMedicineCommand(Func<Medicine, Task<bool>> executeMethod, Func<Medicine, bool> canExecuteMethod) : base(executeMethod, canExecuteMethod)
        {
        }

        public async override void Execute(object parameter)
        {
            await this.ExecuteAsync((Medicine)parameter);
        }
    }
}
