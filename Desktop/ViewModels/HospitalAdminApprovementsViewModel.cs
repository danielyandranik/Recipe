using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace Desktop.ViewModels
{
    class HospitalAdminApprovementsViewModel : ViewModelBase
    {
        private ObservableCollection<DoctorInfo> waitingCollection;

        public ObservableCollection<DoctorInfo> WaitingCollection
        {
            get => this.waitingCollection;
            set => this.Set("WaitingCollection", ref this.waitingCollection, value);
        }
    }
}
