using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruzoMaster.Objects
{
    public class Company
    {
        public enum CompanyCountry : Int32
        {
            Belarus = 0,
            Russia = 1,
            Litva = 2,
        }
        public static String GetCountryRussianName(CompanyCountry companyCountry)
        {
            switch (companyCountry)
            {
                case CompanyCountry.Belarus:
                    return "Беларусь";
                case CompanyCountry.Russia:
                    return "Россия";
                case CompanyCountry.Litva:
                    return "Литва";
                default:
                    return "Беларусь";
            }
        }
    }
}
