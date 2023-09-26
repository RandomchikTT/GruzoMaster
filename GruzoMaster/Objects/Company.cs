using System;

namespace GruzoMaster.Objects
{
    public class Company
    {
        public enum CompanyCountry : Int32
        {
            None = 0,
            Belarus = 1,
            Russia = 2,
            Litva = 3,
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
        public Int32 IdKey { get; set; }
    }
}
