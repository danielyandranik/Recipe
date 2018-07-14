using MedicineAPI.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineAPI.Context
{
    interface IMedicineConext
    {
        IMongoCollection<Medicine> Medicines { get; }
    }
}
