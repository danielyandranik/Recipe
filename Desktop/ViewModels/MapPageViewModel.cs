using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using GalaSoft.MvvmLight;
using Desktop.Services;
using Desktop.Commands;
using GalaSoft.MvvmLight.Command;

namespace Desktop.ViewModels
{
    public class MapPageViewModel : ViewModelBase
    {
        private readonly PushPinService _pushPinService;

        private readonly UIElementCollection _pushPins;

        private readonly PushPinCommand _pushPinCommand;

        private readonly RelayCommand _closePopup;

        private string _name;

        private string _owner;

        private string _phone;

        private string _email;

        private string _address;

        private string _description;

        private bool _isOpen;

        public string Name
        {
            get => this._name;

            set => this.Set("Name", ref this._name, value);
        }

        public string Owner
        {
            get => this._owner;

            set => this.Set("Owner", ref this._owner, value);
        }

        public string Phone
        {
            get => this._phone;

            set => this.Set("Phone", ref this._phone, value);
        }

        public string Email
        {
            get => this._email;

            set => this.Set("Email", ref this._email, value);
        }

        public string Address
        {
            get => this._address;

            set => this.Set("Address", ref this._address, value);
        }

        public string Description
        {
            get => this._description;

            set => this.Set("Description", ref this._description, value);
        }

        public bool IsInstitutionInfoOpen
        {
            get => this._isOpen;

            set => this.Set("IsInstitutionInfoOpen", ref this._isOpen, value);
        }

        public UIElementCollection PushPins => this._pushPins;

        public ICommand PushPinCommand => this._pushPinCommand;

        public ICommand ClosePopup => this._closePopup;

        public MapPageViewModel(UIElementCollection pushPins)
        {
            this._pushPins = pushPins;
            this._pushPinService = new PushPinService(this);
            this._pushPinCommand = new PushPinCommand(this._pushPinService);
            this._closePopup = new RelayCommand(() => this.IsInstitutionInfoOpen = false, () => true);
        }
    }
}
