using Desktop.Commands;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.ViewModels
{
    public class HospitalAdminApprovalViewModel : ViewModelBase
    {
        private ObservableCollection<object> waitingCollection;

        public ObservableCollection<object> WaitingCollection
        {
            get => this.waitingCollection;

            set => this.Set("WaitingCollection", ref this.waitingCollection, value);
        }

        public ApproveDoctorCommand ApproveDoctorCommand { get; }

        public HospitalAdminApprovalViewModel()
        {
            this.ApproveDoctorCommand = new ApproveDoctorCommand();
        }
    }
}
