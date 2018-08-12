using System;
using System.Xml.Linq;
using System.Net.Http;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Generic;
using Microsoft.Maps.MapControl.WPF;
using InstitutionClient;
using InstitutionClient.Models;
using Desktop.Models;
using Desktop.ViewModels;

namespace Desktop.Services
{
    /// <summary>
    /// Push pin service
    /// </summary>
    public class PushPinService
    {
        /// <summary>
        /// Institution client
        /// </summary>
        private readonly Client _institutionClient;

        /// <summary>
        /// Map page view model
        /// </summary>
        private readonly MapPageViewModel _vm;

        /// <summary>
        /// Geocode path
        /// </summary>
        private readonly string _geocodePath;

        /// <summary>
        /// Http client
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Institutions
        /// </summary>
        private readonly Dictionary<Pushpin, Institution> _institutions;

        /// <summary>
        /// Creates new instance of <see cref="PushPinService"/>
        /// </summary>
        /// <param name="pushPins">PushPins</param>
        public PushPinService(MapPageViewModel vm)
        {
            // setting fields
            this._vm = vm;
            this._institutionClient = ((App)App.Current).InstitutionClient;
            this._geocodePath = ConfigurationManager.AppSettings["GeocodePath"];
            this._httpClient = new HttpClient();
            this._institutions = new Dictionary<Pushpin, Institution>();
        }

        /// <summary>
        /// Adds puhspin to the map with the given institution type.
        /// </summary>
        /// <param name="institutionType">Institution type.</param>
        /// <returns>nothing</returns>
        public async Task AddPushPins(string institutionType)
        {
            this.Reset();

            var institutions = default(IEnumerable<Institution>);

            var type = (string)App.Current.Resources[institutionType];

            if (type == "hospital")
                institutions = await this.GetHospitalAddressesAsync();
            else
                institutions = await this.GetPharmacyAddressesAsync();

            if (institutions == null)
                return;

            foreach (var institution in institutions)
            {
                var point = await this.GetGeocodes(institution.Address);

                if (point == null)
                    continue;

                var pushPin = new Pushpin
                {
                    Location = new Location(point.Latitude, point.Longitude)
                };

                pushPin.PreviewMouseDown += this.ShowPopup;

                this._institutions.Add(pushPin, institution);

                this._vm.PushPins.Add(pushPin);
            }
        }

        /// <summary>
        /// Gets addresses of all hospitals registered in Recipe system.
        /// </summary>
        /// <returns>enumerable of addresses</returns>
        private async Task<IEnumerable<Institution>> GetHospitalAddressesAsync()
        {
            var response = await this._institutionClient.GetAllHospitalsAsync();

            if (!response.IsSuccessStatusCode)
                return null;

            return response.Content;
        }

        /// <summary>
        /// Gets addresses of all pharmacies registered in Recipe system.
        /// </summary>
        /// <returns>enumerable of addresses</returns>
        private async Task<IEnumerable<Institution>> GetPharmacyAddressesAsync()
        {
            var response = await this._institutionClient.GetAllPharmaciesAsync();

            if (!response.IsSuccessStatusCode)
                return null;

            return response.Content;
        }

        /// <summary>
        /// Gets geocodes of the given address
        /// </summary>
        /// <param name="address">Address</param>
        /// <returns>Point representing geocodes.</returns>
        private async Task<Point> GetGeocodes(string address)
        {
            var requestUri = $"{this._geocodePath}?address={Uri.EscapeDataString(address)}&sensor=false";

            var response = await this._httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
                return null;

            var stream = await response.Content.ReadAsStreamAsync();

            var xdoc = XDocument.Load(stream);

            var result = xdoc.Element("GeocodeResponse").Element("result");

            var location = result?.Element("geometry")?.Element("location");

            if (location == null)
                return null;

            if (!double.TryParse(location.Element("lat").Value, out var lat) ||
                !double.TryParse(location.Element("lng").Value, out var lng))
                return null;

            return new Point
            {
                Latitude = lat,
                Longitude = lng
            };
        }

        /// <summary>
        /// Resets pushpins
        /// </summary>
        private void Reset()
        {
            var pushPins = this._vm.PushPins;

            foreach(var pushPin in pushPins)
            {
                var pushPinObject = pushPin as Pushpin;

                pushPinObject.PreviewMouseDown -= this.ShowPopup;
            }

            pushPins.Clear();
            this._institutions.Clear();
        }

        /// <summary>
        /// Event handler for pushpin PreviewMouseDown
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event argument</param>
        private void ShowPopup(object sender, MouseButtonEventArgs e)
        {
            var pushPin = sender as Pushpin;

            var institution = this._institutions[pushPin];

            this._vm.Address = institution.Address;
            this._vm.Description = institution.Description;
            this._vm.Email = institution.Email;
            this._vm.Name = institution.Name;
            this._vm.Owner = institution.Owner;
            this._vm.Phone = institution.Phone;
            this._vm.IsInstitutionInfoOpen = true;
        }
    }
}
