using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruzoMaster
{
    public class DriverInfo
    {
        public String FullName { get; set; }
        public DateTime BirthDay { get; set; }
        public DateTime MedSpavka { get; set; }
        public List<License> ListLicense { get; set; }
        public String SerialPassport { get; set; }
        public String NumberPassport { get; set; }
        public Dictionary<PhoneNumber, String> PhoneNumbers { get; set; }
        public String Address { get; set; }
        public Int32 IdKey { get; set; }
    }
}
