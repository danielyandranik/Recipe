using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;
using System.Configuration;
using InstitutionClient;
using Microsoft.Maps.MapControl.WPF;
using System.Net.Http;
using Desktop.Models;

namespace Desktop.Services
{
    public class PushPinService
    {
        private readonly Client _institutionClient;

        private readonly UIElementCollection _pushPins;

        private readonly string _geocodePath;

        private readonly HttpClient _httpClient;

        public PushPinService(UIElementCollection uIElementCollection)
        {
            this._institutionClient = ((App)App.Current).InstitutionClient;
            this._pushPins = uIElementCollection;
            this._geocodePath = ConfigurationManager.AppSettings["GeocodePath"];
            this._httpClient = new HttpClient();
        }

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

            foreach(var address in addresses)
            {
                var point = await this.GetGeocodes(address);

                if (point == null)
                    continue;

                var pushPin = new Pushpin();
                pushPin.Location = new Location(point.Latitude,point.Longitude);

                this._pushPins.Add(pushPin);
            }
        }

        public async Task<IEnumerable<string>> GetHospitalAddressesAsync()
        {
            var response = await this._institutionClient.GetAllHospitalsAsync();

            if (!response.IsSuccessStatusCode)
                return null;

            return response.Content.Select(hospital => hospital.Address);
        }

        public async Task<IEnumerable<string>> GetPharmacyAddressesAsync()
        {
            var response = await this._institutionClient.GetAllPharmaciesAsync();

            if (!response.IsSuccessStatusCode)
                return null;

            return response.Content.Select(pharmacy => pharmacy.Address);
        }

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
