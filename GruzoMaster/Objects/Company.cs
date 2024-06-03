using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        /// <summary>
        /// Получение компании по его ID
        /// </summary>
        /// <param name="id">Уникальный айди из Базы Данных</param>
        /// <returns>Обьект класса Company</returns>
        public static async Task<Company> GetCompanyById(Int32 id)
        {
            try
            {
                DataTable dataTable = await MySQL.QueryRead($"SELECT * FROM `companies` WHERE `id`={id}");
                Company company = null;
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    company = new Company()
                    {
                        IdKey = Convert.ToInt32(row["id"]),
                        Name = Convert.ToString(row["Name"]),
                        City = Convert.ToString(row["City"]),
                        Email = Convert.ToString(row["Email"]),
                        Country = (Company.CompanyCountry)Convert.ToInt32(row["Country"]),
                        PhoneNumbers = JsonConvert.DeserializeObject<Dictionary<PhoneNumber, String>>(row["Contacts"].ToString()),
                        BankData = JsonConvert.DeserializeObject<Dictionary<CompanyBankData, String>>(row["BankData"].ToString()),
                    };
                }
                return company;
            }
            catch (Exception ex) { MessageBox.Show("GetCompanyById: " + ex.ToString()); return null; }
        }
        /// <summary>
        /// Возвращает список всех компаний
        /// </summary>
        /// <returns>Список всех компаний</returns>
        public static async Task<List<Company>> GetCompanies()
        {
            try
            {
                DataTable dataTable = await MySQL.QueryRead($"SELECT * FROM `companies`");
                List<Company> companies = new List<Company>();
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Company company = new Company()
                        {
                            IdKey = Convert.ToInt32(row["id"]),
                            Name = Convert.ToString(row["Name"]),
                            City = Convert.ToString(row["City"]),
                            Email = Convert.ToString(row["Email"]),
                            Country = (Company.CompanyCountry)Convert.ToInt32(row["Country"]),
                            PhoneNumbers = JsonConvert.DeserializeObject<Dictionary<PhoneNumber, String>>(row["Contacts"].ToString()),
                            BankData = JsonConvert.DeserializeObject<Dictionary<CompanyBankData, String>>(row["BankData"].ToString()),
                        };
                        companies.Add(company);
                    }
                }
                return companies;
            }
            catch (Exception ex) { MessageBox.Show("GetCompanies: " + ex.ToString()); return new List<Company>(); }
        }
    }
}
