using System;

namespace Nogupe.Web.Models
{
    public class ErrorViewModel
    {
        public int StatusCode { get; set; } = 400;
        public string Message { get; set; }
        public string Details { get; set; }
    }
}
