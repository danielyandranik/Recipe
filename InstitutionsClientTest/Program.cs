using InstitutionClient;
using InstitutionClient.Models;

namespace InstitutionsClientTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            var client = new Client("http://localhost:5700");


            // Get
            var hospitals = client.GetAllHospitalsAsync();

            var pharmacies = client.GetAllPharmaciesAsync();

            var id = 1;

            var suppliers = client.GetAllSuppliersAsync(id);
            
            var pharmMeds = client.GetAllPharmacyMedicinesAsync(id);

            var institutionInfo = client.GetInstitution(id);

            var pharmMedInfo = client.GetPharmacyMedicine(id);

            // Create

            var institutionCreationResponse = client.CreateInstitution(
                new Institution
                {
                    Id = -1,
                    Name = "Pharm",
                    License = "PharmLicense",
                    Description = "Desc",
                    OpenTime = "09.00",
                    CloseTime = "22.00",
                    Email = null,
                    Phone = null,
                    Owner = "PharmOwner",
                    Type = "pharmacy",
                    Address = new Address
                    {
                        Country = "Country",
                        State = "State",
                        City = "City",
                        PostalCode = "a11",
                        AddressLine = "aaaaa"
                    }
                });

            var pharmMedCreationResponse = client.CreatePharmacyMedicine(
                new PharmMedicine
                {
                    Id = -1,
                    MedicineId = 1,
                    PharmacyId = 3,
                    Price = 1000,
                    Quantity = 40
                });

            // Update

            var institutionUpdateResponse = client.UpdateInstitutionAsync(
                 new Institution
                 {
                     Id = -1,
                     Name = "Pharm",
                     License = "PharmLicense",
                     Description = "Desc_update",
                     OpenTime = "09.00",
                     CloseTime = "22.00",
                     Email = null,
                     Phone = "091-00-15-10",
                     Owner = "PharmOwner",
                     Type = "pharmacy",
                     Address = new Address
                     {
                         Country = "Country",
                         State = "State",
                         City = "City",
                         PostalCode = "a11_apdate",
                         AddressLine = "aaaaa"
                     }
                 });

            var priceUpdateResponse = client.UpdateMedicinePriceAsync(
                new MedicinePriceInfo
                {
                    Id = 2,
                    Price = 1500
                });

            var quantityUpdateResponse = client.UpdateMedicineQuantityAsync(
                new MedicineQuantityInfo
                {
                    Id = 2,
                    Quantity = 25
                });


            // Delete

            var institutionDeleteResponse = client.DeleteInstitutionAsync(id);

            var pharmMedDeleteResponse = client.DeletePharmacyMedicineAsync(id);

        }
    }
}
