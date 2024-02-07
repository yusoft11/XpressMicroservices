using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.API.Common
{
    public class GenericResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
    public class GenericResponseApi
    {
        public string message { get; set; }
        public string code { get; set; }
    }
}
