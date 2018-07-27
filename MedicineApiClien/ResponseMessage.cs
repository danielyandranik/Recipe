using System;
using System.Collections.Generic;
using System.Text;

namespace MedicineApiClient
{
    public class ResponseMessage<T>
    {
		public int StatusCode { get; set; }

        public bool IsSuccessStatusCode { get; set; }

		public T Result { get; set; }
    }
}
