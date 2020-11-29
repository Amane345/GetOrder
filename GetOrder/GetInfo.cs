using System;
using System.Collections.Generic;
using System.Text;

namespace Newegg
{
    class GetInfo
    {
        public int OrderNumber { get; set; }

        public string ShipToAddress1 { get; set; }

        public int ShipToZipCode { get; set; }
        public string ShipToCountryCode { get; set; }
        public string ShipToFirstName { get; set; }
    }
}
