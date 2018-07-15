using System;
using System.Collections.Generic;
using System.Text;

namespace MedicineApiClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client("http://localhost:7000/api/medicines");

            var str = client.GetAllMedicinesAsync().Result;

            ;

            client.CreateMedicineAsync(new Medicine
            {
                Country = "AM",
                Description = "",
                Maker = "lfik",
                Name="aspirin",
                Units="10",
                ShelfLife = 10
            }).Wait();

            str = client.GetAllMedicinesAsync().Result;

            ;
        }
    }
}
