using System;
using System.Collections.Generic;

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
        /// <summary>
        /// ID в базе
        /// </summary>
        public Int32 IdKey { get; set; }
        /// <summary>
        /// Страна
        /// </summary>
        public CompanyCountry Country { get; set; }
        /// <summary>
        /// Город
        /// </summary>
        public String City { get; set; }
        /// <summary>
        /// Контакты компании
        /// </summary>
        public Dictionary<PhoneNumber, String> PhoneNumbers { get; set; }
        /// <summary>
        /// Название компании
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// Почта
        /// </summary>
        public String Email { get; set; }
        /// <summary>
        /// Банковские даннные
        /// </summary>
        public Dictionary<CompanyBankData, String> BankData { get; set; }
    }
}
