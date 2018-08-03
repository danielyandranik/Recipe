using System;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Generic;
using System.Windows.Controls;
using Microsoft.Maps.MapControl.WPF;
using InstitutionClient;
using System.Net.Http;
using Desktop.Models;

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
        /// Pushpins of map
        /// </summary>
        private readonly UIElementCollection _pushPins;

        /// <summary>
        /// Geocode path
        /// </summary>
        private readonly string _geocodePath;

        /// <summary>
        /// Http client
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Creates new instance of <see cref="PushPinService"/>
        /// </summary>
        /// <param name="pushPins">PushPins</param>
        public PushPinService(UIElementCollection pushPins)
        {
            // setting fields
            this._institutionClient = ((App)App.Current).InstitutionClient;
            this._pushPins = pushPins;
            this._geocodePath = ConfigurationManager.AppSettings["GeocodePath"];
            this._httpClient = new HttpClient();
        }

        /// <summary>
        /// Adds puhspin to the map with the given institution type.
        /// </summary>
        /// <param name="institutionType">Institution type.</param>
        /// <returns>nothing</returns>
        public async Task AddPushPins(string institutionType)
        {
            this._pushPins.Clear();

            var addresses = default(IEnumerable<string>);

            if (institutionType == "Hospitals")
                addresses = await this.GetHospitalAddressesAsync();
            else
                addresses = await this.GetPharmacyAddressesAsync();

            if (addresses == null)
                return;

            foreach (var address in addresses)
            {
                var point = await this.GetGeocodes(address);

                if (point == null)
                    continue;

                var pushPin = new Pushpin
                {
                    Location = new Location(point.Latitude, point.Longitude)
                };

                this._pushPins.Add(pushPin);
            }
        }

        /// <summary>
        /// Gets addresses of all hospitals registered in Recipe system.
        /// </summary>
        /// <returns>enumerable of addresses</returns>
        public async Task<IEnumerable<string>> GetHospitalAddressesAsync()
        {
            var response = await this._institutionClient.GetAllHospitalsAsync();

            if (!response.IsSuccessStatusCode)
                return null;

            return response.Content.Select(hospital => hospital.Address);
        }

        /// <summary>
        /// Gets addresses of all pharmacies registered in Recipe system.
        /// </summary>
        /// <returns>enumerable of addresses</returns>
        public async Task<IEnumerable<string>> GetPharmacyAddressesAsync()
        {
            var response = await this._institutionClient.GetAllPharmaciesAsync();

            if (!response.IsSuccessStatusCode)
                return null;

            return response.Content.Select(pharmacy => pharmacy.Address);
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

    }
}
