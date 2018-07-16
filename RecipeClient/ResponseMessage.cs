using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeClient
{
    public class ResponseMessage<T>
    {
		public int StatusCode { get; set; }

        public bool IsSuccessStatusCode { get; set; }

		public T Content { get; set; }
    }
}
